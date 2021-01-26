using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using TM.Core.Identity.JwtBearer;
using TM.Core.Models;
using TM.Infrastructure.Extensions.Common;
//using AuthorizationHandlerContext = Microsoft.AspNetCore.Mvc.Filters.FilterContext;

namespace TM.Core.Identity.Authorization.Policies
{
    /// <summary>
    /// Jwt授权处理器
    /// </summary>
    public class JsonWebTokenAuthorizationHandler : AuthorizationHandler<JsonWebTokenAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _accessor;
        /// <summary>
        /// Jwt选项配置
        /// </summary>
        private readonly JwtOptions _options;

        /// <summary>
        /// Jwt令牌校验器
        /// </summary>
        private readonly IJsonWebTokenValidator _tokenValidator;

        /// <summary>
        /// Jwt令牌存储器
        /// </summary>
        private readonly IJsonWebTokenStore _tokenStore;

        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        private IAuthenticationSchemeProvider _schemes { get; set; }

        /// <summary>
        /// 初始化一个<see cref="JsonWebTokenAuthorizationHandler"/>类型的实例
        /// </summary>
        /// <param name="options">Jwt选项配置</param>
        /// <param name="tokenValidator">Jwt令牌校验器</param>
        /// <param name="tokenStore">Jwt令牌存储器</param>
        public JsonWebTokenAuthorizationHandler(
            IHttpContextAccessor accessor,
            IOptions<JwtOptions> options
            , IJsonWebTokenValidator tokenValidator
            , IJsonWebTokenStore tokenStore
            , IAuthenticationSchemeProvider schemes)
        {
            _accessor = accessor;
            _options = options.Value;
            _tokenValidator = tokenValidator;
            _tokenStore = tokenStore;
            _schemes = schemes;
        }

        /// <summary>
        /// 重载异步处理
        /// </summary>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, JsonWebTokenAuthorizationRequirement requirement)
        {
            if (_options.ThrowEnabled)
            {
                await ThrowExceptionHandleAsync(context, requirement);
                return;
            }
            await ResultHandleAsync(context, requirement);

        }

        /// <summary>
        /// 抛异常处理方式
        /// </summary>
        protected virtual async Task ThrowExceptionHandleAsync(AuthorizationHandlerContext context,
            JsonWebTokenAuthorizationRequirement requirement)
        {
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var filterContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext);
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext)?.HttpContext;

            if (httpContext == null)
            {
                httpContext = _accessor.HttpContext;
            }

            // 未登录而被拒绝
            var result = httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            if (!result || string.IsNullOrWhiteSpace(authorizationHeader))
                throw new UnauthorizedAccessException("未授权，请传递Header头的Authorization参数");
            var token = authorizationHeader.ToString().Split(' ').Last().Trim();
            if (!await _tokenStore.ExistsTokenAsync(token))
                throw new UnauthorizedAccessException("未授权，无效参数");
            var codeResult = _tokenValidator.Validate(token, _options, requirement.ValidatePayload);
            if (codeResult != Code.Ok)
                throw new UnauthorizedAccessException("验证失败，请查看传递的参数是否正确或是否有权限访问该地址。");
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
                return;
            context.Succeed(requirement);
        }

        /// <summary>
        /// 结果处理方式
        /// </summary>
        protected virtual async Task ResultHandleAsync(AuthorizationHandlerContext context,
            JsonWebTokenAuthorizationRequirement requirement)
        {
            /*
             * .netcore3.0 启用EndpointRouting后，权限filter不再添加到ActionDescriptor ，而将权限直接作为中间件运行，
             * 同时所有filter都会添加到endpoint.Metadata。因此，文中的
             * context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext不再成立。
             * 
             * 解决方案有两个：
             * 
             * 首先必须在 controller 上进行配置 Authorize ，可以策略授权，也可以角色等基本授权
             * 
             * 1、开启公约， startup 中的全局授权过滤公约：o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
             * 
             * 2、不开启公约，使用 IHttpContextAccessor ，也能实现效果，但是不能自定义返回格式，详细看下边配置；
             * 
             * 3. 或者不用EndpointRouting，app.UseMvc
             */
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var filterContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext);
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext)?.HttpContext;

            if (httpContext == null)
            {
                httpContext = _accessor.HttpContext;
            }

            if (httpContext == null)
            {
                filterContext.Result = new AuthorizeResult(Code.Error,
                      Code.Error.Description());
                context.Fail();
                return;
            }
            var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            var handlerSchemes = await _schemes.GetRequestHandlerSchemesAsync();
            foreach (var scheme in handlerSchemes)
            {
                if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                {
                    context.Fail();
                    return;
                }
            }
            //判断请求是否拥有凭据，即有没有登录
            var defaultAuthenticate = await _schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate == null)
            {
                filterContext.Result = new AuthorizeResult(Code.Unauthorized,
                    Code.Unauthorized.Description());
                context.Succeed(requirement);
                return;
            }

            //验证是否成功，此步已经代表验证通过了，单点登录或许可以不用接着走下去
            //但是由于直播App/小程序异"地"登录后会清除旧的token缓存以达到剔除登录的目的，所以要进行获取token校验
            var authResult = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
            //result?.Principal不为空即登录成功
            if (authResult?.Principal == null)
            {
                filterContext.Result = new AuthorizeResult(Code.Unauthorized,
                    Code.Unauthorized.Description());
                context.Succeed(requirement);
                return;
            }
            httpContext.User = authResult.Principal;

            //角色权限之类的判断 
            //if (requirement.Permissions.GroupBy(g => g.Url).Where(w => w.Key?.ToLower() == questUrl).Count() > 0)

            // 未登录而被拒绝
            var result = httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            //暂时不对url上传token的支持
            //token = httpContext.Request.Query["access_token"];
            if (!result || string.IsNullOrWhiteSpace(authorizationHeader))
            {
                filterContext.Result = new AuthorizeResult(Code.HttpRequestError,
                    Code.Unauthorized.Description());
                context.Succeed(requirement);
                return;
            }

            var token = authorizationHeader.ToString().Split(' ').Last().Trim();
            if (!await _tokenStore.ExistsTokenAsync(token))
            {
                filterContext.Result = new AuthorizeResult(Code.Unauthorized,
                    Code.Unauthorized.Description());
                context.Succeed(requirement);
                return;
            }
            var codeResult = _tokenValidator.Validate(token, _options, requirement.ValidatePayload);
            if (codeResult != Code.Ok)
            {
                filterContext.Result = new AuthorizeResult(codeResult,
                    codeResult.Description());
                context.Succeed(requirement);
                return;
            }

            // 登录超时
            var accessToken = await _tokenStore.GetTokenAsync(token);
            //这个是防止redis过期了但是没有自动清除。之前同事测试过出现过期了redis自动清除机制没有将其删掉的情况。
            //redis过期后，如果进行get/set的动作，会触发被动删除，返回空（除非没有设置redis过期时间，才依赖此字段判断）
            if (accessToken.IsExpired())
            {
                filterContext.Result = new AuthorizeResult(Code.TokenInvalid,
                    Code.TokenInvalid.Description());
                context.Succeed(requirement);
                return;
            }

            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
                return;
            context.Succeed(requirement);
        }



    }
}
