using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Parking.Domain.Helpers.Response;

namespace Parking.Middlewares
{
    /// <summary>
    /// Middleware - error handling
    /// </summary>
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);

            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new
            {
                error = new
                {
                    message = exception.Message,
                    exception = exception.GetType().Name
                }
            });
            //serilog
            Log.Error("ERROR FOUND", result);

            var output = JsonConvert.SerializeObject(new Message
            {
                Code = 500,
                Success = false,
                Error = "Error during execution"
            });

            await response.WriteAsync(output);
        }
    }
}
