
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：CloudLiveRequestDto
// 文件功能描述：
// 
// 创建者：庄欣锴
// 创建时间：2020年6月6日
// 
//----------------------------------------------------------------*/

using System;
using System.ComponentModel.DataAnnotations;

namespace TM.Infrastructure.TencentCloud.Dto
{
    public class CloudLiveRequestDto
    {

    }

    #region 断开直播流

    public class DropLiveStreamRequestDto
    {
        /// <summary>
        /// 流名称。
        /// 本项目为直播间房间号
        /// </summary>
        public string StreamName { get; set; }
    }

    #endregion

    #region 禁推直播流

    public class ForbidLiveStreamRequestDto
    {
        /// <summary>
        /// 流名称。
        /// 本项目为直播间房间号
        /// </summary>
        [Required(ErrorMessage = "直播间房间号不能为空")]
        public string StreamName { get; set; }

        /// <summary>
        /// 恢复流的时间。UTC 格式，例如：2018-11-29T19:00:00Z。
        /// 1. 默认禁播7天，且最长支持禁播90天
        /// </summary>
        [Required(ErrorMessage = "恢复时间不能为空")]
        public DateTime? ResumeTime { get; set; }

        /// <summary>
        /// 禁推原因。
        /// </summary>
        [Required(ErrorMessage = "禁推原因不能为空")]
        public string Reason { get; set; }
    }

    #endregion

    #region 恢复直播推流

    public class ResumeLiveStreamRequestDto
    {
        /// <summary>
        /// 流名称。
        /// 本项目为直播间房间号
        /// </summary>
        public string StreamName { get; set; }
    }

    #endregion
}