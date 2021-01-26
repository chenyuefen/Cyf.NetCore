using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TM.Core.Identity.JwtBearer;
using TM.Core.Models;
using TM.Infrastructure.Extensions.Common;

namespace TM.Core.Identity.Authorization.Middlewares
{
    /// <summary>
    /// JWT客户授权中间件
    /// </summary>
    public class JsonWebTokenCustomerAuthorizeMiddleware
    {
        /// <summary>
        /// 方法
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Jwt选项配置
        /// </summary>
        private readonly JwtOptions _options;

        /// <summary>
        /// 校验内容
        /// </summary>
        private readonly Func<IDictionary<string, string>, JwtOptions, Code> _validatePayload;

        /// <summary>
        /// 匿名访问路径列表
        /// </summary>
        private readonly IList<string> _anonymousPathList;

        /// <summary>
        /// Jwt令牌校验器
        /// </summary>
        private readonly IJsonWebTokenValidator _tokenValidator;

        /// <summary>
        /// 初始化一个<see cref="JsonWebTokenCustomerAuthorizeMiddleware"/>类型的实例
        /// </summary>
        /// <param name="next">方法</param>
        /// <param name="options">Jwt选项配置</param>
        /// <param name="tokenValidator">Jwt令牌校验器</param>
        /// <param name="validatePayload">校验负载</param>
        /// <param name="anonymousPathList">匿名访问路径列表</param>
        public JsonWebTokenCustomerAuthorizeMiddleware(
            RequestDelegate next
            , IOptions<JwtOptions> options
            , IJsonWebTokenValidator tokenValidator
            , Func<IDictionary<string, string>, JwtOptions, Code> validatePayload
            , IList<string> anonymousPathList)
        {
            _next = next;
            _options = options.Value;
            _tokenValidator = tokenValidator;
            _validatePayload = validatePayload;
            _anonymousPathList = anonymousPathList;
        }

        /// <summary>
        /// 执行中间件拦截逻辑
        /// </summary>
        /// <param name="context">Http上下文</param>
        public async Task Invoke(HttpContext context)
        {
            // 如果是匿名访问路径，则直接跳过
            if (_anonymousPathList.Contains(context.Request.Path.Value))
            {
                await _next(context);
                return;
            }
            var result = context.Request.Headers.TryGetValue("Authorization", out var authStr);
            if (!result || string.IsNullOrWhiteSpace(authStr.ToString()))
                throw new UnauthorizedAccessException("未授权，请传递Header头的Authorization参数");
            // 校验以及自定义校验
            var codeResult = _tokenValidator.Validate(authStr.ToString().Substring("Bearer ".Length).Trim(), _options,
                _validatePayload);
            if (codeResult == Code.Ok)
                await _next(context);
            else
            {
                var content = new AuthorizeResult(codeResult, codeResult.Description());
                switch (codeResult)
                {
                    case Code.Ok:
                        break;
                    case Code.Unauthorized:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await context.Response.WriteAsync(content.ToJson());
                        break;
                    case Code.TokenInvalid:
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        await context.Response.WriteAsync(content.ToJson());
                        break;
                    case Code.Forbidden:
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        await context.Response.WriteAsync(content.ToJson());
                        break;
                    case Code.NoFound:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsync(content.ToJson());
                        break;
                    case Code.MethodNotAllowed:
                        context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        await context.Response.WriteAsync(content.ToJson());
                        break;
                    case Code.HttpRequestError:
                        context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        await context.Response.WriteAsync(content.ToJson());
                        break;
                    case Code.Locked:
                        context.Response.StatusCode = (int)HttpStatusCode.Locked;
                        await context.Response.WriteAsync(content.ToJson());
                        break;
                    case Code.Error:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync(content.ToJson());
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync(content.ToJson());
                        break;
                }
                return;
            }


            //if (!result)
            //{
            //    //var content = new AuthorizeResult(codeResult, codeResult.Description());
            //    //await context.Response.WriteAsync(content.ToJson()); 
            //    ////context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //    //return;

            //    throw new UnauthorizedAccessException("验证失败，请查看传递的参数是否正确或是否有权限访问该地址。");
            //}

            //await _next(context);
        }
    }
}
