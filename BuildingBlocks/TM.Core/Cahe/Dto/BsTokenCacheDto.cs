using System;

namespace TM.Core.Cahe.Dto
{
    public class BsTokenCacheDto
    {
    }

    #region 小店后台登陆缓存
    public class BsUserTokenCacheDto
    {
        public string Token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
    #endregion

    #region 总后台登陆token缓存
    public class BsGenUserTokenCacheDto
    {
        public string Token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
    #endregion
}
