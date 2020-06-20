using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers
{
    public static class EasyCachingExt
    {
        /// <summary>
        /// 注册缓存
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEasyCachingExt(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEasyCaching(options =>
                {
                    options.UseInMemory("default");
                    options.UseCSRedis(config =>
                    {
                        config.DBConfig.ConnectionStrings = new List<string> { configuration.GetConnectionString("csredis") };
                    }, "csredis");
                });
            return services;
        }
    }
}
