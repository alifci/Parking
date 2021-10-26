using Parking.Domain.Domain.Employee;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Helpers.Response;
using Parking.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Domain.Service.EmployeeServ
{
    public interface IEmployeeService
    {
        ResponseHelper<OutputModelOneItem<EmployeeVM>> GetById(int id);
        ResponseHelper<OutputModelPaging<EmployeeVM>> GetAll(PagingParams pagingParams, string key);
        Task<ResponseHelper<OutputModelOneItem<EmployeeVM>>> Create(EmployeeModel model);
        Task<ResponseHelper<OutputModelOneItem<EmployeeVM>>> Update(int id, EmployeeModel  model);
        Task<ResponseHelper<OutputModelOneItem<EmployeeVM>>> Delete(int id);
    }
}
