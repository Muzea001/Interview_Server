using Interview_Server.DTOs;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var formFileParameters = context.ApiDescription.ParameterDescriptions
            .Where(p => p.Type == typeof(IFormFile) || p.Type == typeof(IFormFileCollection))
            .ToList();

        if (formFileParameters.Any())
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                { "dto", new OpenApiSchema { Type = "object", Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(RegisterDTO) } } },
                                { "profileImage", new OpenApiSchema { Type = "string", Format = "binary" } }
                            }
                        }
                    }
                }
            };
        }
    }
}
