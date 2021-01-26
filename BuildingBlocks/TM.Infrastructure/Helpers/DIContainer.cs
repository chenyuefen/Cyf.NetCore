using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace TM.Infrastructure.Helpers
{
    public class DIContainer
    {
        private static IServiceProvider _serviceProvider = null;

        /// <summary>
        /// services.BuildServiceProvider()
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Rigister(IServiceProvider serviceProvider)
        {
            //IServiceProvider serviceProvider1 = new ServiceCollection().BuildServiceProvider();
            _serviceProvider = serviceProvider;
        }

        public static T Resolve<T>() where T : class
        {
            return _serviceProvider.GetService<T>();
        }

        public static T RequireResolve<T>() where T : class
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
}
