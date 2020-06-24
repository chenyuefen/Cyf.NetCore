using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Cyf.Swagger
{
    /// <summary>
    /// Swagger 过滤器
    /// </summary>
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters)
            {
                if (!parameter.Name.Equals("version")) continue;
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                if (parameter.Description == null)
                {
                    parameter.Description = "填写版本号如:1";
                    //parameter.Default = context.ApiDescription.GroupName.Replace("v", "");
                }
            }
        }
    }
}
