using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FlowrSpot.WebApi.Exceptions;

namespace FlowrSpot.WebApi.Filters
{
    public class AppExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var apiError = new ApiError
            {
                Code = 500,
                Message = "Internal Server Error",
                Timestamp = DateTime.Now
            };

            apiError.Errors.Add(context.Exception.Message);

            context.Result = new JsonResult(apiError) { StatusCode = 500 };
        }
    }
}
