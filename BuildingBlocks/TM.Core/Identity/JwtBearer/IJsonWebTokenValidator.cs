using System;
using System.Collections.Generic;
using TM.Core.Models;

namespace TM.Core.Identity.JwtBearer
{
    /// <summary>
    /// Jwt令牌校验器
    /// </summary>
    public interface IJsonWebTokenValidator
    {
        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="encodeJwt">加密后的Jwt令牌</param>
        /// <param name="options">Jwt选项配置</param>
        /// <param name="validatePayload">校验负载</param>
        Code Validate(string encodeJwt, JwtOptions options,
            Func<IDictionary<string, string>, JwtOptions, Code> validatePayload);

        /// <summary>
        /// 获取负载
        /// </summary>
        /// <param name="encodeJwt">加密后的Jwt令牌</param>
        /// <param name="options">Jwt选项配置</param>
        /// <returns></returns>
        Dictionary<string, string> Payload();

    }
}
