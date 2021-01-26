using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using TM.Core.Data.SqlSugar.Abstractions;

namespace TM.Core.Data.SqlSugar
{
    /// <summary>
    /// EasyCaching缓存扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 注册SugarBase操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        public static IServiceCollection AddSugarBase(this IServiceCollection services, IConfiguration configuration, Action<ConnectionConfig> configAction,
            ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            if (services == null)
                throw new ArgumentException(nameof(services));


            var config = SqlSugarConfig.GetConnectionString(configuration);
            configAction.Invoke(config);

            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(x =>
                    {
                        return new SugarBase(config);
                    });
                    break;

                case ServiceLifetime.Scoped:
                    services.AddScoped(x =>
                    {
                        return new SugarBase(config);
                    });
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(x =>
                    {
                        return new SugarBase(config);
                    });
                    break;

                default:
                    services.AddScoped(x =>
                    {
                        return new SugarBase(config);
                    });
                    break;
            }

            return services;
        }

        /// <summary>
        /// 注册SugarBase操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        public static IServiceCollection AddSqlServerSugarBase(this IServiceCollection services, IConfiguration configuration, Action<ConnectionConfig> configAction, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            if (services == null)
                throw new ArgumentException(nameof(services));

            var config = ShopMallConfig.DbCofig;
            configAction.Invoke(config);

            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(x =>
                    {
                        return new SqlServerSugarBase(config.ConnectionString);
                    });
                    break;

                case ServiceLifetime.Scoped:
                    services.AddScoped(x =>
                    {
                        return new SqlServerSugarBase(config.ConnectionString);
                    });
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(x =>
                    {
                        return new SqlServerSugarBase(config.ConnectionString);
                    });
                    break;

                default:
                    services.AddScoped(x =>
                    {
                        return new SqlServerSugarBase(config.ConnectionString);
                    });
                    break;
            }

            return services;
        }

    }
}
