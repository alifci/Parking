using Parking.Domain.Domain.Card;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Helpers.Response;
using Parking.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Domain.Service.CardServ
{
    public interface ICardService
    {
        ResponseHelper<OutputModelOneItem<CardVM>> GetById(int id);
        ResponseHelper<OutputModelPaging<CardVM>> GetAll(PagingParams pagingParams, string key);
        Task<ResponseHelper<OutputModelOneItem<CardVM>>> Create(CardModel model);
        Task<ResponseHelper<OutputModelOneItem<CardVM>>> Update(int id, CardUpdateModel model);
        Task<ResponseHelper<OutputModelOneItem<CardVM>>> Delete(int id);
    }
}
