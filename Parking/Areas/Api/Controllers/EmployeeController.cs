using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Domain.Domain.Employee;
using Parking.Domain.Helpers.Pagination;
using Parking.Domain.Service.EmployeeServ;
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
    public class EmployeeController : BaseController
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeService"></param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Intanace of Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<EmployeeVM>>), 200)]
        public IActionResult Get([FromRoute] int id)
        {
            var result = _employeeService.GetById(id);
            return Ok(result);
        }


        /// <summary>
        /// List of Employees
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelPaging<EmployeeVM>>), 200)]
        public IActionResult GetAll([FromQuery] PagingParams pagingParams, [FromQuery] string key)
        {
            var result = _employeeService.GetAll(pagingParams, key);
            return Ok(result);
        }


        /// <summary>
        /// Create Employee
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<EmployeeVM>>), 200)]
        public async Task<IActionResult> Create([FromBody] EmployeeModel model)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Code = 400,
                        Success = false,
                        Error = GetErrorsOfModelState(ModelState)
                    },
                    Data = null
                });

            var result = await _employeeService.Create(model);
            return Ok(result);
        }



        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<EmployeeVM>>), 200)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] EmployeeModel model)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseHelper<OutputModelOneItem<EmployeeVM>>()
                {
                    Message = new Message()
                    {
                        Code = 400,
                        Success = false,
                        Error = GetErrorsOfModelState(ModelState)
                    },
                    Data = null
                });

            var result = await _employeeService.Update(id, model);
            return Ok(result);
        }


        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseHelper<OutputModelOneItem<EmployeeVM>>), 200)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _employeeService.Delete(id);
            return Ok(result);
        }


        #endregion
    }
}
