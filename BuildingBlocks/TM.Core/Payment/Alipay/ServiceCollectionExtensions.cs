using Microsoft.Extensions.DependencyInjection;
using System;

namespace TM.Core.Payment.Alipay
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAlipay(
            this IServiceCollection services)
        {
            services.AddAlipay(setupAction: null);
        }

        public static void AddAlipay(
            this IServiceCollection services,
            Action<AlipayOptions> setupAction)
        {
            services.AddSingleton<AlipayClient>();
            services.AddSingleton<AlipayNotifyClient>();
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }
        }
    }
}