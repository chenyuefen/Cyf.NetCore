using EasyCaching.Core;
using EasyCaching.CSRedis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;

namespace Helpers
{
    public static class RedisExt
    {
        public static IServiceCollection AddCsRedisHelper(this IServiceCollection services, IConfiguration configuration)
        {
            var csredis = new CSRedis.CSRedisClient(configuration.GetConnectionString("csredis"));
            //var csredis = new CSRedis.CSRedisClient(new Func<string, string>(s => "39.100.197.236:6380/6385"),
            //    "39.100.197.236:6380,defaultDatabase=13,poolsize=50,ssl=false,writeBuffer=10240",
            //    "39.100.197.236:6381,defaultDatabase=13,poolsize=50,ssl=false,writeBuffer=10240",
            //    "39.100.197.236:6382,defaultDatabase=13,poolsize=50,ssl=false,writeBuffer=10240",
            //    "39.100.197.236:6383,defaultDatabase=13,poolsize=50,ssl=false,writeBuffer=10240",
            //    "39.100.197.236:6384,defaultDatabase=13,poolsize=50,ssl=false,writeBuffer=10240",
            //    "39.100.197.236:6385,defaultDatabase=13,poolsize=50,ssl=false,writeBuffer=10240"
            //    );
            RedisHelper.Initialization(csredis);
            return services;
        }

        /// <summary>
        /// 和EasyCaching共用同一个csredis实例,必须先注册AddEasyCachingExt
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddCsRedisHelperByEasyCaching(this IApplicationBuilder app)
        {
            var client = app.ApplicationServices.GetRequiredService<EasyCachingCSRedisClient>();
            if (client != null)
            {
                RedisHelper.Initialization(client);
            }
            else
            {
                Log.Error("初始化RedisHelper失败");
            }

            return app;
        }

        public static IServiceCollection AddCsRedisByEasyCaching(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var csredis = provider.GetService<EasyCachingCSRedisClient>();
            RedisHelper.Initialization(csredis);
            return services;
        }
    }
}
