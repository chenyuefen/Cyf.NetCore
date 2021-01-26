using System.Collections.Generic;
using System.Threading.Tasks;

namespace TM.Core.Identity.JwtBearer
{
    /// <summary>
    /// Jwt令牌构建器
    /// </summary>
    public interface IJsonWebTokenBuilder
    {
        /// <summary>
        /// 创建令牌
        /// </summary>
        /// <param name="payload">负载</param>
        Task<JsonWebToken> CreateAsync(IDictionary<string, string> payload);

        /// <summary>
        /// 创建令牌
        /// </summary>
        /// <param name="payload">负载</param>
        /// <param name="options">Jwt选项配置</param>
        Task<JsonWebToken> CreateAsync(IDictionary<string, string> payload, JwtOptions options);

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="refreshToken">刷新令牌</param>
        Task<JsonWebToken> RefreshAsync(string refreshToken);

        /// <summary>
        /// 利用刷新令牌获取原有Payload
        /// </summary>
        /// <param name="token">刷新令牌</param>
        Task<Dictionary<string, string>> GetPayloadByRefreshTokenAsync(string refreshToken);

    }
}
