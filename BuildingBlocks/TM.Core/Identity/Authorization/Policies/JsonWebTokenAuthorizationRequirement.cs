using System;
using System.Collections.Generic;
using TM.Core.Identity.JwtBearer;
using TM.Core.Models;

namespace TM.Core.Identity.Authorization.Policies
{
    /// <summary>
    /// JWT授权请求
    /// </summary>
    public class JsonWebTokenAuthorizationRequirement : IJsonWebTokenAuthorizationRequirement
    {
        /// <summary>
        /// 校验负载
        /// </summary>
        protected internal Func<IDictionary<string, string>, JwtOptions, Code> ValidatePayload = (a, b) => Code.Ok;

        /// <summary>
        /// 设置校验函数
        /// </summary>
        /// <param name="func">校验函数</param>
        public virtual IJsonWebTokenAuthorizationRequirement SetValidateFunc(Func<IDictionary<string, string>, JwtOptions, Code> func)
        {
            ValidatePayload = func;
            return this;
        }
    }
}
