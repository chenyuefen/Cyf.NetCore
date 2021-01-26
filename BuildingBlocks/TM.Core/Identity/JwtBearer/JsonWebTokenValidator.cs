using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TM.Core.Models;
using TM.Infrastructure.Extensions.Common;
using TM.Infrastructure.Json;

namespace TM.Core.Identity.JwtBearer
{
    /// <summary>
    /// Jwt令牌校验器
    /// </summary>
    public class JsonWebTokenValidator : IJsonWebTokenValidator //internal sealed
    {
        private IHttpContextAccessor _accessor;
        /// <summary>
        /// Jwt选项配置
        /// </summary>
        private readonly JwtOptions _options;
        public JsonWebTokenValidator(IHttpContextAccessor accessor, IOptions<JwtOptions> options)
        {
            _accessor = accessor;
            _options = options.Value;
        }
        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="encodeJwt">加密后的Jwt令牌</param>
        /// <param name="options">Jwt选项配置</param>
        /// <param name="validatePayload">校验负载</param>
        public Code Validate(string encodeJwt, JwtOptions options, Func<IDictionary<string, string>, JwtOptions, Code> validatePayload)
        {
            if (string.IsNullOrWhiteSpace(options.Secret))
                throw new ArgumentNullException(nameof(options.Secret),
                    $@"{nameof(options.Secret)}为Null或空字符串。请在""appsettings.json""配置""{nameof(JwtOptions)}""节点及其子节点""{nameof(JwtOptions.Secret)}""");
            var jwtArray = encodeJwt.Split('.');
            if (jwtArray.Length < 3)
                return Code.HttpRequestError;
            var header = JsonUtil.ToObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArray[0]));
            var payload = JsonUtil.ToObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArray[1]));

            // 首先验证签名是否正确
            var hs256 = new HMACSHA256(Encoding.UTF8.GetBytes(options.Secret));
            var sign = Base64UrlEncoder.Encode(
                hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArray[0], ".", jwtArray[1]))));
            // 签名不正确直接返回
            if (!string.Equals(jwtArray[2], sign))
                return Code.HttpRequestError;
            // 其次验证是否在有效期内
            var now = ToUnixEpochDate(DateTime.UtcNow);
            if (!(now >= long.Parse(payload["nbf"].ToString()) && now < long.Parse(payload["exp"].ToString())))
                return Code.TokenInvalid;
            // 进行自定义验证
            return validatePayload(payload, options);
        }

        /// <summary>
        /// 单独获取负载
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public Dictionary<string, string> Payload()
        {
            var token = GetToken();
            if (token.IsEmpty())
                throw new ArgumentNullException(nameof(token),
                   $@"{nameof(token)}为Null或空字符串");
            var jwtArray = token.Split('.');
            if (jwtArray.Length < 3)
                throw new ArgumentNullException(nameof(jwtArray),
                    $@"{nameof(jwtArray)}token格式有误");
            return JsonUtil.ToObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArray[1]));
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            var result = _accessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            if (!result || string.IsNullOrWhiteSpace(authorizationHeader))
                return null;
            return authorizationHeader.ToString().Split(' ').Last().Trim();
        }


        /// <summary>
        /// 生成时间戳
        /// </summary>
        private long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

    }
}
