using AutoMapper;
using Parking.Domain.Domain.HighwayGatePassing;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Helpers.Response;
using Parking.Entity.Entity;
using Parking.Entity.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Domain.Service.HighwayGatePassingServ
{
    public class HighwayGatePassingService : IHighwayGatePassingService
    {
        private IUnitOfWork _unitOfWork;

        public HighwayGatePassingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>> GetById(int id)
        {
            var model = _unitOfWork.HighwayGatePassingRepository
                .Get(c => c.Id == id).FirstOrDefault();

            if (model == null)
                return new ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 404,
                        Error = "not found"
                    },
                };

            var viewModel = Mapper.Map<HighwayGatePassingVM>(model);

            return new ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>()
            {
                Message = new Message()
                {
                    Success = true,
                    Code = 200
                },
                Data = new OutputModelOneItem<HighwayGatePassingVM>()
                {
                    Item = Mapper.Map<HighwayGatePassingVM>(viewModel),
                }
            };

        }


        public ResponseHelper<OutputModelPaging<HighwayGatePassingVM>> GetAll(PagingParams pagingParams, string key)
        {
            var query = _unitOfWork.HighwayGatePassingRepository.Get();

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(x => x.Car.Brand.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Car.Model.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Car.PlateNumber.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Car.Card.Employee.Name.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Car.Card.Employee.Position.Contains(key, StringComparison.OrdinalIgnoreCase));

            var modelsPagedList = new PagedList<HighwayGatePassing>(
                query, pagingParams.PageNumber, pagingParams.PageSize);

            var viewModels = modelsPagedList.List.Select(m => Mapper.Map<HighwayGatePassingVM>(m)).ToList();

            return new ResponseHelper<OutputModelPaging<HighwayGatePassingVM>>()
            {
                Message = new Message()
                {
                    Success = true,
                    Code = 200
                },
                Data = new OutputModelPaging<HighwayGatePassingVM>()
                {
                    Items = viewModels,
                    Paging = modelsPagedList.GetHeader(),
                }
            };
        }

        public async Task<ResponseHelper<OutputModelOneItem<HighwayGatePassingWithRemainBalanceVM>>> Create(HighwayGatePassingModel model)
        {
            var card = _unitOfWork.CardRepository.Get(x => x.CarId == model.CarId)
                .FirstOrDefault();

            if (card == null)
                return new ResponseHelper<OutputModelOneItem<HighwayGatePassingWithRemainBalanceVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 400,
                        Error = "No cards belonges to this car"
                    }
                };

            var entity = Mapper.Map<HighwayGatePassing>(model);

            //If the car passes through the highway gate 2 times within 1 minute
            //then we only charge the card once (second pass is free)
            //if three within 1 minute then two charged the second one free and
            //the first and the third charged and so on
            var lastPassing = _unitOfWork.HighwayGatePassingRepository
                .Get(x => x.CarId == model.CarId)
                .OrderByDescending(x => x.Created).FirstOrDefault();

            if (lastPassing != null && !lastPassing.IsFree
                && DateTime.Now.Subtract(lastPassing.Created) <= new TimeSpan(0, 1, 0))
            {
                entity.Cost = 0;
                entity.IsFree = true;
            }
            else
            {
                entity.Cost = 4;
                entity.IsFree = false;
            }

            if (!entity.IsFree)
                card.Credit = card.Credit - entity.Cost;

            HighwayGatePassing created = new HighwayGatePassing();

            using (var transaction = _unitOfWork.db.Database.BeginTransaction())
            {
                try
                {
                    if (!entity.IsFree)
                    {
                        card = _unitOfWork.CardRepository.Update(card);
                        await _unitOfWork.SaveChangesAsync();
                    }

                    created = await _unitOfWork.HighwayGatePassingRepository.Create(entity);
                    await _unitOfWork.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new ResponseHelper<OutputModelOneItem<HighwayGatePassingWithRemainBalanceVM>>()
                    {
                        Message = new Message()
                        {
                            Success = false,
                            Code = 500,
                            Error = "Error while saving try again later"
                        }
                    };
                }
            }

            var createdPassWithBalance = new HighwayGatePassingWithRemainBalanceVM()
            {
                Id = created.Id,
                Created = created.Created,
                CarId = created.CarId,
                Cost = created.Cost,
                IsFree = created.IsFree,
                CardRemainingBalance = card.Credit
            };


            return new ResponseHelper<OutputModelOneItem<HighwayGatePassingWithRemainBalanceVM>>()
            {
                Message = new Message()
                {
                    Success = true,
                    Code = 200
                },
                Data = new OutputModelOneItem<HighwayGatePassingWithRemainBalanceVM>()
                {
                    Item = Mapper.Map<HighwayGatePassingWithRemainBalanceVM>(createdPassWithBalance),
                }
            };
        }

        public async Task<ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>> Update(int id, HighwayGatePassingUpdateModel model)
        {
            var entity = _unitOfWork.HighwayGatePassingRepository
                .Get(x => x.Id == id).FirstOrDefault();

            if (entity == null)
                return new ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 404,
                        Error = "not found"
                    }
                };

            entity.CarId = model.CarId;
            entity.Cost = model.Cost;
            entity.IsFree = model.IsFree;

            var updated = _unitOfWork.HighwayGatePassingRepository.Update(entity);
            var res = await _unitOfWork.SaveChangesAsync();

            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<HighwayGatePassingVM>()
                    {
                        Item = Mapper.Map<HighwayGatePassingVM>(updated),
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 500,
                        Error = "Error while saving try again later"
                    }
                };
        }

        public async Task<ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>> Delete(int id)
        {
            _unitOfWork.HighwayGatePassingRepository.Delete(id);
            var res = await _unitOfWork.SaveChangesAsync();

            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 500,
                        Error = "Error while saving try again later"
                    }
                };
        }

    }
}
