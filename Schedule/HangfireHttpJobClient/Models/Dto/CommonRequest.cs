
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：CommonRequest
// 文件功能描述：
// 
// 创建者：林洁标
// 创建时间：2020年11月24日
// 
//----------------------------------------------------------------*/

using System;

namespace HangfireHttpJobClient.Models.Dto
{
    public class CommonRequest
    {
    }

    public class ActivityJobRequest
    {
        /// <summary>
        /// 活动编号 
        /// </summary>
        public long ActivityId { get; set; }
        /// <summary>
        /// 延迟时间(分)
        /// </summary>
        public int DelayFromMinutes { get; set; }
    }
}