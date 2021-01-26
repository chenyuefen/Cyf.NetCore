using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace TM.Core.Data.Dapper
{
    /// <summary>
    /// EasyCaching缓存扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 注册ConnectionBase操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        public static IServiceCollection AddConnectBase(this IServiceCollection services, IConfiguration configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (services == null)
                throw new ArgumentException(nameof(services));

            if (configuration == null)
                throw new ArgumentException(nameof(configuration));

            var dbConfigurations = configuration.GetSection("ConnectionStrings").Get<List<DbConnectionOption>>();
            var dbType = configuration.GetSection("DBType").Get<string>();
            if (dbConfigurations != null)
            {
                foreach (var dbConfiguration in dbConfigurations)
                {
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(x =>
                            {
                                return new ConnectionBase(dbConfiguration.Value, dbType);
                            });
                            break;

                        case ServiceLifetime.Scoped:
                            services.AddScoped(x =>
                            {
                                return new ConnectionBase(dbConfiguration.Value, dbType);
                            });
                            break;

                        case ServiceLifetime.Transient:
                            services.AddTransient(x =>
                            {
                                return new ConnectionBase(dbConfiguration.Value, dbType);
                            });
                            break;

                        default:
                            services.AddScoped(x =>
                            {
                                return new ConnectionBase(dbConfiguration.Value, dbType);
                            });
                            break;
                    }
                }
            }

            return services;
        }
    }
}
