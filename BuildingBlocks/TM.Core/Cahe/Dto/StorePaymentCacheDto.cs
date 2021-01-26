/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：StorePaymentCacheDto
// 文件功能描述：
// 小店缴费流水缓存实体
//
// 创建者：庄欣锴
// 创建时间：2020-04-01 09:54
// 
//----------------------------------------------------------------*/
using System;

namespace TM.Core.Cahe.Dto
{
    public class StorePaymentCacheDto
    {
        /// <summary>
        /// 用户编号Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 小店编号
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 实际付款金额  单位：分
        /// </summary>
        public int PayAmount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
