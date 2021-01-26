using Microsoft.Extensions.DependencyInjection;
using System;

namespace TM.Core.Payment.UnionPay
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUnionPay(
            this IServiceCollection services)
        {
            services.AddUnionPay(setupAction: null);
        }

        public static void AddUnionPay(
            this IServiceCollection services,
            Action<UnionPayOptions> setupAction)
        {
            services.AddSingleton<UnionPayClient>();
            services.AddSingleton<UnionPayNotifyClient>();
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }
        }
    }
}