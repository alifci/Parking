using AutoMapper;
using Parking.Domain.Domain.Employee;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Helpers.Response;
using Parking.Entity.Entity;
using Parking.Entity.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Domain.Service.EmployeeServ
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResponseHelper<OutputModelOneItem<EmployeeVM>> GetById(int id)
        {
            var includes = "Card,Card.Car";
            var model = _unitOfWork.EmployeeRepository
                .Get(includes:includes ,expression: c => c.Id == id).FirstOrDefault();

            if (model == null)
                return new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 404,
                        Error = "not found"
                    },
                };

            var viewModel = Mapper.Map<EmployeeVM>(model);

            return new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<EmployeeVM>()
                    {
                        Item = Mapper.Map<EmployeeVM>(viewModel),
                    }
                };

        }


        public ResponseHelper<OutputModelPaging<EmployeeVM>> GetAll(PagingParams pagingParams, string key)
        {
            var includes = "Card,Card.Car";
            var query = _unitOfWork.EmployeeRepository.Get(includes:includes);

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(x => x.Name.Contains(key, StringComparison.OrdinalIgnoreCase)
                || x.Position.Contains(key, StringComparison.OrdinalIgnoreCase));

            var modelsPagedList = new PagedList<Employee>(
                query, pagingParams.PageNumber, pagingParams.PageSize);

            var viewModels = modelsPagedList.List.Select(m => Mapper.Map<EmployeeVM>(m)).ToList();

            return new ResponseHelper<OutputModelPaging<EmployeeVM>>()
            {
                Message = new Message()
                {
                    Success = true,
                    Code = 200
                },
                Data = new OutputModelPaging<EmployeeVM>()
                {
                    Items = viewModels,
                    Paging = modelsPagedList.GetHeader(),
                }
            };
        }

        public async Task<ResponseHelper<OutputModelOneItem<EmployeeVM>>> Create(EmployeeModel model)
        {
            var entity = Mapper.Map<Employee>(model);
            var created = await _unitOfWork.EmployeeRepository.Create(entity);
            var res = await _unitOfWork.SaveChangesAsync();


            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<EmployeeVM>()
                    {
                        Item = Mapper.Map<EmployeeVM>(created),
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 500,
                        Error = "Error while saving try again later"
                    }
                };
        }

        public async Task<ResponseHelper<OutputModelOneItem<EmployeeVM>>> Update(int id,EmployeeModel model)
        {
            var entity = _unitOfWork.EmployeeRepository
                .Get(x => x.Id == id).FirstOrDefault();

            if (entity == null)
                return new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 404,
                        Error = "not found"
                    }
                };

            entity.Name = model.Name;
            entity.Position = model.Position;
            entity.Age = model.Age;

            var updated = _unitOfWork.EmployeeRepository.Update(entity);
            var res = await _unitOfWork.SaveChangesAsync();

            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    },
                    Data = new OutputModelOneItem<EmployeeVM>()
                    {
                        Item = Mapper.Map<EmployeeVM>(updated),
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Success = false,
                        Code = 500,
                        Error = "Error while saving try again later"
                    }
                };
        }

        public async Task<ResponseHelper<OutputModelOneItem<EmployeeVM>>> Delete(int id)
        {
            _unitOfWork.EmployeeRepository.Delete(id);
            var res = await _unitOfWork.SaveChangesAsync();

            if (res > 0)
                return new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Success = true,
                        Code = 200
                    }
                };
            else
                return new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
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
