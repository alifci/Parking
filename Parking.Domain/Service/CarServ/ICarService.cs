using Parking.Domain.Domain.Car;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Helpers.Response;
using Parking.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Domain.Service.CarServ
{
    public interface ICarService
    {
        ResponseHelper<OutputModelOneItem<CarVM>> GetById(int id);
        ResponseHelper<OutputModelPaging<CarVM>> GetAll(PagingParams pagingParams, string key);
        Task<ResponseHelper<OutputModelOneItem<CarVM>>> Create(CarModel model);
        Task<ResponseHelper<OutputModelOneItem<CarVM>>> Update(int id, CarModel  model);
        Task<ResponseHelper<OutputModelOneItem<CarVM>>> Delete(int id);
    }
}
