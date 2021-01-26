using System;
using System.Collections.Generic;
using TM.Core.Identity.JwtBearer;
using TM.Core.Models;
using TM.Infrastructure.Extensions.Collections;

namespace TM.Core.Identity.Authorization.Middlewares
{
    /// <summary>
    /// Jwt客户授权配置
    /// </summary>
    public class JsonWebTokenCustomerAuthorizeOption : IJsonWebTokenCustomerAuthorizeOption
    {
        /// <summary>
        /// 匿名访问路径列表
        /// </summary>
        protected internal readonly List<string> AnonymousPaths = new List<string>();

        /// <summary>
        /// 校验负载
        /// </summary>
        protected internal Func<IDictionary<string, string>, JwtOptions, Code> ValidatePayload = (a, b) => Code.Ok;

        /// <summary>
        /// 初始化一个<see cref="JsonWebTokenCustomerAuthorizeOption"/>类型的实例
        /// </summary>
        public JsonWebTokenCustomerAuthorizeOption() { }

        /// <summary>
        /// 设置匿名访问路径
        /// </summary>
        /// <param name="urls">路径列表</param>
        public List<string> SetAnonymousPaths(IList<string> urls)
        {
            urls.ForEach(url =>
            {
                AnonymousPaths.Add(url);
            });
            return AnonymousPaths;
        }

        /// <summary>
        /// 设置校验函数
        /// </summary>
        /// <param name="func">校验函数</param>
        public void SetValidateFunc(Func<IDictionary<string, string>, JwtOptions, Code> func) => ValidatePayload = func;
    }
}
