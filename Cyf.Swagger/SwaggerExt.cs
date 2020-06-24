using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cyf.Swagger
{
    public static class SwaggerExt
    {
        public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider();
                var apiVersionDescriptionProvider = provider.GetRequiredService<IApiVersionDescriptionProvider>();
                provider.Dispose();
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName,
                         new OpenApiInfo()
                         {
                             Title = $"官方接口 v{description.ApiVersion}",
                             Version = description.ApiVersion.ToString(),
                         }
                    );
                }

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                });
                //Json Token认证方式，此方式为全局添加
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                        Reference = new OpenApiReference(){
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        } },
                        Array.Empty<string>() }
                });

                c.OperationFilter<SwaggerOperationFilter>();
                //c.DocumentFilter<SwaggerEnumFilter>();
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml");
                //c.IncludeXmlComments(xmlPath, true);
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Cyf.WebApiCore.xml");
                c.IncludeXmlComments(xmlPath, true);
            });
            return services;
        }
        public static void UseSwaggerExt(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
