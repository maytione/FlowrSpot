using FlowrSpot.WebApi.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FlowrSpot.Application.Common.Enums;
using FlowrSpot.Application.Common.Models;

namespace FlowrSpot.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleModelStateError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values
             .SelectMany(v => v.Errors)
             .Select(e => e.ErrorMessage)
             .ToList();
            return BadRequest(errors);
        }


        protected IActionResult HandleErrorResponse(string error)
        {
            var errors = new HashSet<Error>
            {
                new() { Message = error }
            };
            return HandleErrorResponse(errors);
        }

        protected IActionResult HandleErrorResponseBadId()
        {
            string error = "ID in the request body does not match the route parameter.";
            var errors = new HashSet<Error>
            {
                new() { Message = error }
            };
            return HandleErrorResponse(errors);

        }

        protected IActionResult HandleErrorResponse(HashSet<Error> errors)
        {
            var apiError = new ApiError() { Message = "Unknown" };

            if (errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var error = errors.FirstOrDefault(e => e.Code == ErrorCode.NotFound);

                apiError.Code = 404;
                apiError.Message = "Not Found";
                apiError.Timestamp = DateTime.Now;
                apiError.Errors.Add(error.Message);

                return NotFound(apiError);
            }

            apiError.Code = 400;
            apiError.Message = "Bad request";
            apiError.Timestamp = DateTime.Now;
            foreach (var e in errors)
            {
                apiError.Errors.Add(e.Message);
            }

            return StatusCode(400, apiError);
        }
    }
}
