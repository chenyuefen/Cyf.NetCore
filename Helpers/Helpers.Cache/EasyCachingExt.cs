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
        /// 注册缓存,内存和redis
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEasyCachingExt(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEasyCaching(options =>
                {
                    options.UseInMemory(x =>
                    {
                        x.DBConfig.SizeLimit = 2000;
                    }, "default");
                    if (string.IsNullOrEmpty(configuration.GetConnectionString("csredis"))) throw new ArgumentNullException("easyCaching csredis config");
                    options.UseCSRedis(config =>
                    {
                        config.DBConfig.ConnectionStrings = new List<string> { configuration.GetConnectionString("csredis") };
                    }, "csredis");
                });
            return services;
        }
    }
}
