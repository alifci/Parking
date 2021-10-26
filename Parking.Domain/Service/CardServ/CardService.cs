using AutoMapper;
using Parking.Domain.Domain.Card;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Helpers.Response;
using Parking.Entity.Entity;
using Parking.Entity.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Domain.Service.CardServ
{
    public class CardService : ICardService
    {
        private IUnitOfWork _unitOfWork;

        public CardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseHelper<OutputModelOneItem<CardVM>> GetById(int id)
        {
            var includes = "Car";
            var model = _unitOfWork.CardRepository
                .Get(includes: includes, expression: c => c.Id == id).FirstOrDefault();

            if (model == null)
                return new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 404,
                        Error = "not found"
                    },
                };

            var viewModel = Mapper.Map<CardVM>(model);

            return new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<CardVM>()
                    {
                        Item = Mapper.Map<CardVM>(viewModel),
                    }
                };

        }


        public ResponseHelper<OutputModelPaging<CardVM>> GetAll(PagingParams pagingParams, string key)
        {
            var includes = "Car";
            var query = _unitOfWork.CardRepository.Get(includes:includes);

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(x => x.Car.Brand.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Car.Model.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Car.PlateNumber.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Employee.Name.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Employee.Position.Contains(key, StringComparison.OrdinalIgnoreCase));

            var modelsPagedList = new PagedList<Card>(
                query, pagingParams.PageNumber, pagingParams.PageSize);

            var viewModels = modelsPagedList.List.Select(m => Mapper.Map<CardVM>(m)).ToList();

            return new ResponseHelper<OutputModelPaging<CardVM>>()
            {
                Message = new Message()
                {
                    Success = true,
                    Code = 200
                },
                Data = new OutputModelPaging<CardVM>()
                {
                    Items = viewModels,
                    Paging = modelsPagedList.GetHeader(),
                }
            };
        }

        public async Task<ResponseHelper<OutputModelOneItem<CardVM>>> Create(CardModel model)
        {
            var entity = Mapper.Map<Card>(model);

            //welcome credit of 10$
            entity.Credit = 10;

            var created = await _unitOfWork.CardRepository.Create(entity);
            var res = await _unitOfWork.SaveChangesAsync();


            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<CardVM>()
                    {
                        Item = Mapper.Map<CardVM>(created),
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 500,
                        Error = "Error while saving try again later"
                    }
                };
        }

        public async Task<ResponseHelper<OutputModelOneItem<CardVM>>> Update(int id, CardUpdateModel model)
        {
            var entity = _unitOfWork.CardRepository
                .Get(x => x.Id == id).FirstOrDefault();

            if (entity == null)
                return new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 404,
                        Error = "not found"
                    }
                };

            entity.Credit = model.Credit;
            entity.EmoployeeId = model.EmoployeeId;

            var updated = _unitOfWork.CardRepository.Update(entity);
            var res = await _unitOfWork.SaveChangesAsync();

            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<CardVM>()
                    {
                        Item = Mapper.Map<CardVM>(updated),
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 500,
                        Error = "Error while saving try again later"
                    }
                };
        }

        public async Task<ResponseHelper<OutputModelOneItem<CardVM>>> Delete(int id)
        {
            _unitOfWork.CardRepository.Delete(id);
            var res = await _unitOfWork.SaveChangesAsync();

            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<CardVM>>()
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
