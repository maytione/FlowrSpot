using FlowrSpot.WebApi.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FlowrSpot.WebApi.Filters
{
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!context.MethodInfo.GetCustomAttributes(true).Any(x => x is AllowAnonymousAttribute) &&
              !context.MethodInfo.DeclaringType!.GetCustomAttributes(true).Any(x => x is AllowAnonymousAttribute))
            {
                var scheme = ConfigureSwaggerOptions.GetJwtSecurityScheme();
                operation.Security = new List<OpenApiSecurityRequirement> {
                    new() { {
                        scheme, Array.Empty<string>()
                      }
                    }
                  };
            }

        }
    }
}
