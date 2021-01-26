using System;

namespace TM.Core.Cahe.Dto
{
    /// <summary>
    /// 秒杀redis缓存dto
    /// </summary>
    public class SeckillCacheDto
    {

        /// <summary>
        /// 库存数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 锁定库存
        /// </summary>
        public int LockQuantity { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }

    /// <summary>
    /// 秒杀订单预扣库存缓存dto
    /// </summary>
    public class SeckillOrderWithHoldStockDto
    {
        /// <summary>
        /// 预扣库存数量
        /// </summary>
        public int WithHoleQuantity { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
}
