using System;

namespace TM.Core.Cahe.Dto
{
    public class IMCacheDto
    {

    }

    #region IM聊天室缓存数据对象
    [Serializable]
    public class IMGroupCacheDto
    {
        /// <summary>
        /// 专属直播间ID
        /// </summary>
        //public string RoomId { get; set; }

        /// <summary>
        /// 聊天室在线人数
        /// </summary>
        public int OnlineUserCount { get; set; }
    }
    #endregion
}
