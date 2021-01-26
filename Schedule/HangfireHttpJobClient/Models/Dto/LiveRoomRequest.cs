
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：LiveRoomRequest
// 文件功能描述：
// 
// 创建者：庄欣锴
// 创建时间：2020年6月13日
// 
//----------------------------------------------------------------*/

using System;

namespace HangfireHttpJobClient.Models.Dto
{
    public class LiveRoomRequest
    {

    }
    
    #region 更新直播间状态
        
    public class AddRoomStatusUpdateJobRequest
    {
        public string RoomId { get; set; }
        
        /// <summary>
        /// 直播间变更后状态
        /// </summary>
        public int ToStatus { get; set; }

        /// <summary>
        /// 分钟数
        /// </summary>
        public int Minute { get; set; }
    }

    #endregion
}