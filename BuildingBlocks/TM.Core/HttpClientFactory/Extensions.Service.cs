
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TM.Core.HttpClientFactory
{
    public static class Extensions
    {
        // <summary>
        /// 注册Http操作
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddHttp(this IServiceCollection services)
        {
            services.TryAddScoped<IHttpRequest, HttpRequest>();
            return services;
        }
    }
}
