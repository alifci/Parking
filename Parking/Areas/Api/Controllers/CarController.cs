using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Domain.Domain.Car;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Service.CarServ;
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
    public class CarController : BaseController
    {
        #region Fields
        private readonly ICarService _carService;
        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="carService"></param>
        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Intanace of Car
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<CarVM>>), 200)]
        public IActionResult Get([FromRoute] int id)
        {
            var result = _carService.GetById(id);
            return Ok(result);
        }


        /// <summary>
        /// List of Cars
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelPaging<CarVM>>), 200)]
        public IActionResult GetAll([FromQuery] PagingParams pagingParams, [FromQuery] string key)
        {
            var result = _carService.GetAll(pagingParams, key);
            return Ok(result);
        }


        /// <summary>
        /// Create Car
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<CarVM>>), 200)]
        public async Task<IActionResult> Create([FromBody] CarModel model)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Code = 400,
                        Success = false,
                        Error = GetErrorsOfModelState(ModelState)
                    },
                    Data = null
                });

            var result = await _carService.Create(model);
            return Ok(result);
        }



        /// <summary>
        /// Update Car
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<CarVM>>), 200)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] CarModel model)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseHelper<OutputModelOneItem<CarVM>>()
                {
                    Message = new Message()
                    {
                        Code = 400,
                        Success = false,
                        Error = GetErrorsOfModelState(ModelState)
                    },
                    Data = null
                });

            var result = await _carService.Update(id, model);
            return Ok(result);
        }


        /// <summary>
        /// Delete Car
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<CarVM>>), 200)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _carService.Delete(id);
            return Ok(result);
        }


        #endregion
    }
}
