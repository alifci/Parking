using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Domain.Domain.Card;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Service.CardServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Parking.Domain.Helpers.Response;

namespace Parking.Area.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CardController : BaseController
    {
        #region Fields
        private readonly ICardService _cardService;
        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardService"></param>
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Intanace of Card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<CardVM>>), 200)]
        public IActionResult Get([FromRoute] int id)
        {
            var result = _cardService.GetById(id);
            return Ok(result);
        }


        /// <summary>
        /// List of Cards
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelPaging<CardVM>>), 200)]
        public IActionResult GetAll([FromQuery] PagingParams pagingParams, [FromQuery] string key)
        {
            var result = _cardService.GetAll(pagingParams, key);
            return Ok(result);
        }


        /// <summary>
        /// Create Card
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<CardVM>>), 200)]
        public async Task<IActionResult> Create([FromBody] CardModel model)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Code = 400,
                        Success = false,
                        Error = GetErrorsOfModelState(ModelState)
                    },
                    Data = null
                });

            var result = await _cardService.Create(model);
            return Ok(result);
        }



        /// <summary>
        /// Update Card
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<CardVM>>), 200)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] CardUpdateModel model)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseHelper<OutputModelOneItem<CardVM>>()
                {
                    Message = new Message()
                    {
                        Code = 400,
                        Success = false,
                        Error = GetErrorsOfModelState(ModelState)
                    },
                    Data = null
                });

            var result = await _cardService.Update(id, model);
            return Ok(result);
        }


        /// <summary>
        /// Delete Card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<CardVM>>), 200)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _cardService.Delete(id);
            return Ok(result);
        }


        #endregion
    }
}
