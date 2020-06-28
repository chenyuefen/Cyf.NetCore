using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cyf.Cors
{
    public static class CorsExt
    {
        /// <summary>
        /// 跨域请求
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsExt(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    //builder.WithOrigins(configuration["Cors:Origins"].Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.AllowCredentials()
                    ;
                });
            });
            return services;
        }

        public static IApplicationBuilder UseCorsExt(this IApplicationBuilder app)
        {
            app.UseCors("any");
            return app;
        }
    }

}
