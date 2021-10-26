using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Domain.Domain.HighwayGatePassing;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Service.HighwayGatePassingServ;
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
    public class HighwayGatePassingController : BaseController
    {
        #region Fields
        private readonly IHighwayGatePassingService _HighwayGatePassingService;
        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HighwayGatePassingService"></param>
        public HighwayGatePassingController(IHighwayGatePassingService HighwayGatePassingService)
        {
            _HighwayGatePassingService = HighwayGatePassingService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Intanace of HighwayGatePassing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>), 200)]
        public IActionResult Get([FromRoute] int id)
        {
            var result = _HighwayGatePassingService.GetById(id);
            return Ok(result);
        }


        /// <summary>
        /// List of HighwayGatePassinges
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelPaging<HighwayGatePassingVM>>), 200)]
        public IActionResult GetAll([FromQuery] PagingParams pagingParams, [FromQuery] string key)
        {
            var result = _HighwayGatePassingService.GetAll(pagingParams, key);
            return Ok(result);
        }


        /// <summary>
        /// Create HighwayGatePassing when passing through the highway gate (by car ID)
        /// and returns remaining balance in the card.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<HighwayGatePassingWithRemainBalanceVM>>), 200)]
        public async Task<IActionResult> Create([FromBody] HighwayGatePassingModel model)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseHelper<OutputModelOneItem<HighwayGatePassingWithRemainBalanceVM>>()
                {
                    Message = new Message()
                    {
                        Code = 400,
                        Success = false,
                        Error = GetErrorsOfModelState(ModelState)
                    },
                    Data = null
                });

            var result = await _HighwayGatePassingService.Create(model);
            return Ok(result);
        }



        /// <summary>
        /// Update HighwayGatePassing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>), 200)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] HighwayGatePassingUpdateModel model)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>()
                {
                    Message = new Message()
                    {
                        Code = 400,
                        Success = false,
                        Error = GetErrorsOfModelState(ModelState)
                    },
                    Data = null
                });

            var result = await _HighwayGatePassingService.Update(id, model);
            return Ok(result);
        }


        /// <summary>
        /// Delete HighwayGatePassing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<HighwayGatePassingVM>>), 200)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _HighwayGatePassingService.Delete(id);
            return Ok(result);
        }


        #endregion
    }
}
