using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using TM.Core.Identity.Authorization.Middlewares;
using TM.Core.Identity.Authorization.Policies;
using TM.Core.Identity.JwtBearer;
using TM.Core.Identity.JwtBearer.Internal;

namespace TM.Core.Identity
{
    /// <summary>
    /// 扩展服务
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 注册Jwt客户授权中间件
        /// </summary>
        /// <param name="builder">应用程序生成器</param>
        /// <param name="action">操作</param>
        public static IApplicationBuilder UseJwtAuthorize(this IApplicationBuilder builder,
            Action<IJsonWebTokenCustomerAuthorizeOption> action = null)
        {
            var option =
                builder.ApplicationServices.GetService<IJsonWebTokenCustomerAuthorizeOption>() as
                    JsonWebTokenCustomerAuthorizeOption ?? new JsonWebTokenCustomerAuthorizeOption();

            action?.Invoke(option);
            return builder.UseMiddleware<JsonWebTokenCustomerAuthorizeMiddleware>(option.ValidatePayload,
                option.AnonymousPaths);
        }
        /// <summary>
        /// 注册Jwt服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(o => configuration.GetSection(nameof(JwtOptions)).Bind(o));
            services.TryAddScoped<IJsonWebTokenBuilder, JsonWebTokenBuilder>();
            services.TryAddScoped<IJsonWebTokenStore, JsonWebTokenStore>();
            services.TryAddSingleton<IJsonWebTokenValidator, JsonWebTokenValidator>();

            services.TryAddScoped<IJsonWebTokenCustomerAuthorizeOption, JsonWebTokenCustomerAuthorizeOption>();
            services.TryAddScoped<IJsonWebTokenAuthorizationRequirement, JsonWebTokenAuthorizationRequirement>();
            services.TryAddScoped<IAuthorizationHandler, JsonWebTokenAuthorizationHandler>();
        }
    }
}