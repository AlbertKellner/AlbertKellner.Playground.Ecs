using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Playground.Application.Infrastructure.Filter
{
    public class AddCustomHeaderOnOpenApiFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "CorrelationId",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "uuid" }
            });
        }
    }
}
