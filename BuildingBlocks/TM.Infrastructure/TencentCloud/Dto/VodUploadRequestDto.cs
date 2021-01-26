
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：VodUploadRequestDto
// 文件功能描述：
// 
// 创建者：庄欣锴
// 创建时间：2020年5月27日
// 
//----------------------------------------------------------------*/

namespace TM.Infrastructure.TencentCloud.Dto
{
    public class VodUploadRequestDto
    {

    }

    #region 云点播拉取上传请求

    public class VodPullUploadRequest
    {
        /// <summary>
        /// 要拉取的媒体 URL，暂不支持拉取 HLS 和 Dash 格式。 支持的扩展名详见[媒体类型]
        /// </summary>
        public string MediaUrl { get; set; }

        /// <summary>
        /// 媒体名称。==>目前传短视频Id
        /// </summary>
        public string MediaName { get; set; }

        /// <summary>
        /// 任务流名称
        /// </summary>
        public string Procedure { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int ClassId { get; set; }
    }

    #endregion
}