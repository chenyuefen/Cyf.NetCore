using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TM.Core.Data.EF.Abstractions;
using TM.Core.Domains;
using TM.Infrastructure.Extensions.Bases;

namespace TM.Core.Data.EF.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<DbContextBase>(options);
            services.AddScoped<DbContext, DbContextBase>();
            AddDefault(services);
            return services;
        }
        public static IServiceCollection AddDatabase<T>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options) where T : DbContextBase
        {
            services.AddDbContextPool<T>(options);
            services.AddScoped<DbContext, T>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWork<T>, UnitOfWork<T>>();
            //services.AddScoped<IEfCoreRepository<T>, EfCoreRepository<T>>();
            AddDefault(services);
            return services;
        }

        #region private function
        private static void AddDefault(IServiceCollection services)
        {
            //services.ScaleDependy(typeof(IEfCoreRepository<>));
            services.AddScoped(typeof(IEfCoreRepository<,>), typeof(EfCoreRepository<,>));
            //services.AddScoped(typeof(IEfCoreRepository<,>));
            //services.AddScoped(typeof(IEfCoreRepository<,>), typeof(EfCoreRepository<,>));
        }
        //auto di
        private static IServiceCollection ScaleDependy(this IServiceCollection services, Type baseType)
        {
            var allAssemblies = AppDomain.CurrentDomain.GetCurrentPathAssembly();
            foreach (var assembly in allAssemblies)
            {
                var types = assembly.GetTypes()
                    .Where(type => type.IsClass
                                   && type.BaseType != null
                                   && type.HasImplementedRawGeneric(baseType));
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();

                    var interfaceType = interfaces.FirstOrDefault(x => x.Name == $"I{type.Name}");
                    if (interfaceType == null)
                    {
                        interfaceType = type;
                    }
                    ServiceDescriptor serviceDescriptor =
                        new ServiceDescriptor(interfaceType, type, ServiceLifetime.Scoped);
                    if (!services.Contains(serviceDescriptor))
                    {
                        services.Add(serviceDescriptor);
                    }
                }
            }

            return services;
        }
        #endregion
    }
}
