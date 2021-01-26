using System;

namespace TM.Core.Cahe.Dto
{
    /// <summary>
    /// 商品规格缓存
    /// </summary>
    public class SkuCacheDto
    {
    }

    /// <summary>
    /// 商品规格锁定库存
    /// </summary>
    public class SkuLockQuantityCacheDto
    {
        public int LockQuantity { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }

    /// <summary>
    /// 创建直播预告/短视频 商品缓存
    /// </summary>
    public class NoticeSpuCacheDto
    {
        public long SpuId { get; set; }

        public long? SkuId { get; set; }
    }
}
