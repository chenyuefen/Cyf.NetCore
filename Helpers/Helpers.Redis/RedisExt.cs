using EasyCaching.Core;
using EasyCaching.CSRedis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Helpers
{
    public static class RedisExt
    {
        public static IServiceCollection AddCsRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var csredis = new CSRedis.CSRedisClient(configuration.GetConnectionString("csredis"));
            RedisHelper.Initialization(csredis);
            //services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
            return services;
        }

        public static IApplicationBuilder UseCsRedisByEasyCaching(IApplicationBuilder app)
        {
            var client = app.ApplicationServices.GetRequiredService<EasyCachingCSRedisClient>();
            RedisHelper.Initialization(client);
            return app;
        }

        public static IServiceCollection AddCsRedisByEasyCaching(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = services.BuildServiceProvider();
            var csredis = provider.GetService<EasyCachingCSRedisClient>();
            RedisHelper.Initialization(csredis);
            return services;
        }
    }
}
