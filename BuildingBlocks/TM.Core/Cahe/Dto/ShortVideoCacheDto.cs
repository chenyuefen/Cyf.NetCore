/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：LiveShortVideoService
// 文件功能描述：
// 短视频缓存dto
//
// 创建者：庄欣锴
// 创建时间：2020-04-13 09:54
// 
//----------------------------------------------------------------*/
using System;

namespace TM.Core.Cahe.Dto
{
    public class ShortVideoCacheDto
    {
    }

    #region 短视频点赞缓存
    public class ShortVideoThunbsUpCache
    {
        /// <summary>
        /// 短视频点赞数
        /// </summary>
        public int ThumbsUpCount { get; set; }
    }
    #endregion

    #region 短视频播放量缓存
    public class ShortVideoPlayCountCache
    {
        /// <summary>
        /// 短视频播放量
        /// </summary>
        public int PlayCount { get; set; }
    }
    #endregion

    #region 短视频列表下标缓存
    [Serializable]
    public class ShortVideoIndexListCache
    {
        public long Id { get; set; }
        public int VideoIndex { get; set; }
    }
    #endregion

    #region 短视频浏览记录
    [Serializable]
    public class ShortVideoRecordListCache
    {
        public long Id { get; set; }
    }

    #endregion
}
