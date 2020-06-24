using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyf.Swagger
{
    public static class ApiVersionExt
    {
        public static IServiceCollection AddApiVersionExt(this IServiceCollection services)
        {
            services
                .AddApiVersioning(option =>
                {
                    option.AssumeDefaultVersionWhenUnspecified = true;
                    option.ReportApiVersions = false;
                    option.DefaultApiVersion = new ApiVersion(1, 0);
                })
                .AddVersionedApiExplorer(option =>
                {
                    option.GroupNameFormat = "'v'VVV";
                    option.AssumeDefaultVersionWhenUnspecified = true;
                });
            return services;
        }
    }
}
