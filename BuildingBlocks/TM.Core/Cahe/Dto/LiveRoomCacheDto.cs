/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：LiveRoomCacheDto
// 文件功能描述：
// 开播直播间缓存dto
//
// 创建者：庄欣锴
// 创建时间：2020年5月14日13:48:38
// 
//----------------------------------------------------------------*/

using System;

namespace TM.Core.Cahe.Dto
{
    [Serializable]
    public class LiveRoomCacheDto
    {
        /// <summary>
        /// 直播间id
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 直播间下标识
        /// </summary>
        public int RoomIndex { get; set; }
    }

    [Serializable]
    public class LiveReocrdCacheDto
    {
        public string RoomId { get; set; }

        /// <summary>
        /// 直播间下标识
        /// </summary>
        public int RoomIndex { get; set; }
    }
}