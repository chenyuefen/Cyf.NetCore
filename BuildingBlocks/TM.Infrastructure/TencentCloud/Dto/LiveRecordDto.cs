/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：LiveRecordDto
// 文件功能描述：
// 直播录制请求响应dto
//
// 创建者：庄欣锴
// 创建时间：2020年5月12日17:15:18
// 
//----------------------------------------------------------------*/

namespace TM.Infrastructure.TencentCloud.Dto
{
    public class LiveRecordDto
    {

    }

    #region 直播开启录制视频

    public class CreateLiveRecordRequest
    {
        /// <summary>
        /// 流名称。
        /// </summary>
        public string StreamName { get; set; }

        /// <summary>
        /// 推流路径，与推流和播放地址中的 AppName保持一致，默认为 live。
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 推流域名。多域名推流必须设置。
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 录制结束时间。中国标准时间，需要 URLEncode(rfc3986)。
        /// 其设置的结束时间不应超过当前时间+30分钟，如果设置的结束时间超过当前时间+30分钟或者小于当前时间或者不设置该参数，则实际结束时间为当前时间+30分钟。
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 录制类型。 “video” : 音视频录制【默认】。 “audio” : 纯音频录制。 在定时录制模式或实时视频录制模式下，该参数均有效，不区分大小写。
        /// </summary>
        public string RecordType { get; set; }

        /// <summary>
        /// 录制文件格式。其值为： “flv”【默认】,“hls”,”mp4”,“aac”,”mp3”。 在定时录制模式或实时视频录制模式下，该参数均有效，不区分大小写。
        /// </summary>
        public string FileFormat { get; set; }

        /// <summary>
        /// 开启实时视频录制模式标志。
        /// 0：不开启实时视频录制模式，即定时录制模式【默认】
        /// 1：开启实时视频录制模式。
        /// </summary>
        public string Highlight { get; set; }

        /// <summary>
        /// 录制流参数。当前支持以下参数：
        /// record_interval - 录制分片时长，单位 秒，1800 - 7200。
        /// storage_time - 录制文件存储时长，单位 秒。
        /// eg. record_interval=3600&storage_time=2592000。
        /// 注：参数需要url encode。 在定时录制模式或实时视频录制模式下，该参数均有效。
        /// </summary>
        public string StreamParam { get; set; }
    }

    #endregion
}