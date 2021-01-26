﻿using System;

namespace TM.Core.Identity.JwtBearer
{
    /// <summary>
    /// 刷新Token信息
    /// </summary>
    [Serializable]
    public class RefreshToken
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 标识值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndUtcTime { get; set; }
    }
}
