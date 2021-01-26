using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace TM.Infrastructure.Helpers
{
    /// <summary>
    /// 使用DI
    /// </summary>
    public class ServiceResolve
    {
        private static IServiceProvider _serviceProvider = null;

        /// <summary>
        /// services.BuildServiceProvider()
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void SetServiceResolve(IServiceProvider serviceProvider)
        {
            //IServiceProvider serviceProvider1 = new ServiceCollection().BuildServiceProvider();
            _serviceProvider = serviceProvider;
        }

        public static T Resolve<T>() where T : class
        {
            return _serviceProvider.GetService<T>();
        }

        public static T ResolveS<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        public static IOptions<T> ResolveOption<T>() where T : class, new()
        {
            //var serviceProvider = new ServiceCollection().BuildServiceProvider();
            return _serviceProvider.GetService<IOptions<T>>();
        }

        public static T ResolveA<T>() where T : class
        {
            return _serviceProvider == null ? null : ActivatorUtilities.GetServiceOrCreateInstance<T>(_serviceProvider);
        }
    }

    public static class DI
    {
        public static IServiceCollection Services { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }
    }

    public static class DIExtensions
    {
        public static IServiceCollection AddTfDI(this IServiceCollection services)
        {
            DI.Services = services;
            return services;
        }

        public static IApplicationBuilder UseTfDI(this IApplicationBuilder builder)
        {
            DI.ServiceProvider = builder.ApplicationServices;
            return builder;
        }
    }


}
