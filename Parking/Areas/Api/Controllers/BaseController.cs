using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Parking.Area.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        protected string GetErrorsOfModelState(ModelStateDictionary modelState)
        {
            string errors = "";
            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors += error.ErrorMessage + ", ";
                }
            }
            return errors;
        }
    }
}
