using AutoMapper;
using Parking.Domain.Domain.Car;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Helpers.Response;
using Parking.Entity.Entity;
using Parking.Entity.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Domain.Service.CarServ
{
    public class CarService : ICarService
    {
        private IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseHelper<OutputModelOneItem<CarVM>> GetById(int id)
        {
            var model = _unitOfWork.CarRepository
                .Get(c => c.Id == id).FirstOrDefault();

            if (model == null)
                return new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 404,
                        Error = "not found"
                    },
                };

            var viewModel = Mapper.Map<CarVM>(model);

            return new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<CarVM>()
                    {
                        Item = Mapper.Map<CarVM>(viewModel),
                    }
                };

        }


        public ResponseHelper<OutputModelPaging<CarVM>> GetAll(PagingParams pagingParams, string key)
        {
            var query = _unitOfWork.CarRepository.Get();

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(x => x.Brand.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Model.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.PlateNumber.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Card.Employee.Name.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Card.Employee.Position.Contains(key, StringComparison.OrdinalIgnoreCase));

            var modelsPagedList = new PagedList<Car>(
                query, pagingParams.PageNumber, pagingParams.PageSize);

            var viewModels = modelsPagedList.List.Select(m => Mapper.Map<CarVM>(m)).ToList();

            return new ResponseHelper<OutputModelPaging<CarVM>>()
            {
                Message = new Message()
                {
                    Success = true,
                    Code = 200
                },
                Data = new OutputModelPaging<CarVM>()
                {
                    Items = viewModels,
                    Paging = modelsPagedList.GetHeader(),
                }
            };
        }

        public async Task<ResponseHelper<OutputModelOneItem<CarVM>>> Create(CarModel model)
        {
            var entity = Mapper.Map<Car>(model);
            var created = await _unitOfWork.CarRepository.Create(entity);
            var res = await _unitOfWork.SaveChangesAsync();


            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<CarVM>()
                    {
                        Item = Mapper.Map<CarVM>(created),
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 500,
                        Error = "Error while saving try again later"
                    }
                };
        }

        public async Task<ResponseHelper<OutputModelOneItem<CarVM>>> Update(int id,CarModel model)
        {
            var entity = _unitOfWork.CarRepository
                .Get(x => x.Id == id).FirstOrDefault();

            if (entity == null)
                return new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 404,
                        Error = "not found"
                    }
                };

            entity.Brand = model.Brand;
            entity.Model = model.Model;
            entity.PlateNumber = model.PlateNumber;

            var updated = _unitOfWork.CarRepository.Update(entity);
            var res = await _unitOfWork.SaveChangesAsync();

            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<CarVM>()
                    {
                        Item = Mapper.Map<CarVM>(updated),
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 500,
                        Error = "Error while saving try again later"
                    }
                };
        }

        public async Task<ResponseHelper<OutputModelOneItem<CarVM>>> Delete(int id)
        {
            _unitOfWork.CarRepository.Delete(id);
            var res = await _unitOfWork.SaveChangesAsync();

            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<CarVM>>()
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
