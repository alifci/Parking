using Parking.Domain.Domain.HighwayGatePassing;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Helpers.Response;
using Parking.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Domain.Service.HighwayGatePassingServ
{
    public interface IHighwayGatePassingService
    {
        ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>> GetById(int id);
        ResponseHelper<OutputModelPaging<HighwayGatePassingVM>> GetAll(PagingParams pagingParams, string key);
        Task<ResponseHelper<OutputModelOneItem<HighwayGatePassingWithRemainBalanceVM>>> Create(HighwayGatePassingModel model);
        Task<ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>> Update(int id, HighwayGatePassingUpdateModel model);
        Task<ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>> Delete(int id);
    }
}
