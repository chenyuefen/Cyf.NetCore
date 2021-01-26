using System.Collections.Generic;
using TM.Infrastructure.TencentCloud.Entities.Base;

namespace TM.Infrastructure.TencentCloud.Entities.Vod.Response
{
    public class VideoInfonJsonResult : WxJsonResult
    {
        /// <summary>
        /// 视频基础信息
        /// </summary>
        public BasicInfo basicInfo { get; set; }

        /// <summary>
        /// 视频转码结果信息
        /// </summary>
        public transcodeInfo transcodeInfo { get; set; }

        /// <summary>
        /// 视频元信息   腾讯接口返回null  待处理
        /// </summary>
        public MetaData metaData { get; set; }
    }

    /// <summary>
    /// 视频基础信息
    /// </summary>
    public class BasicInfo
    {
        /// <summary>
        /// 视频名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 视频大小。单位：字节
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 视频文件总大小（视频为 HLS 时，大小是 m3u8 和 ts 文件大小的总和）。单位：字节
        /// </summary>
        public int totalSize { get; set; }

        /// <summary>
        /// 视频时长。单位：秒
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// 视频描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 视频状态， normal：正常
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 视频创建时间（Unix时间戳）
        /// </summary>
        public int createTime { get; set; }

        /// <summary>
        /// 视频信息最近更新时间（Unix时间戳）
        /// </summary>
        public int updateTime { get; set; }

        /// <summary>
        /// 视频过期时间（Unix时间戳），视频过期之后，该视频及其所有附属对象（转码结果、雪碧图等）将都被删除
        /// </summary>
        public int expireTime { get; set; }

        /// <summary>
        /// 视频分类Id
        /// </summary>
        public int classificationId { get; set; }

        /// <summary>
        /// 视频分类名称
        /// </summary>
        public string classificationName { get; set; }

        /// <summary>
        /// 播放器Id
        /// </summary>
        public int playerId { get; set; }

        /// <summary>
        /// 视频封面图片地址
        /// </summary>
        public string coverUrl { get; set; }

        /// <summary>
        /// 视频封装格式，例如mp4, flv等
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 视频源文件地址
        /// </summary>
        public string sourceVideoUrl { get; set; }
    }

    /// <summary>
    /// 视频转码结果信息
    /// </summary>
    public class transcodeInfo
    {
        /// <summary>
        /// 转码的视频是否IDR对齐。0：不对齐；1对齐。
        /// </summary>
        public int idrAlignment { get; set; }

        /// <summary>
        /// 各规格的转码信息集合，每个元素代表一个规格的转码结果
        /// </summary>
        public List<transcode> transcodeList { get; set; }
    }

    /// <summary>
    /// 各规格的转码信息集合
    /// </summary>
    public class transcode
    {
        /// <summary>
        /// 转码后的视频文件地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 参见 转码参数模板
        /// </summary>
        public int definition { get; set; }

        /// <summary>
        /// 视频时长。单位：秒
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// Float表示的视频时长，更精确。单位：秒
        /// </summary>
        public float floatDuration { get; set; }

        /// <summary>
        /// 视频大小。单位：字节
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 视频文件总大小（视频为 HLS 时，大小是 m3u8 和 ts 文件大小的总和）。单位：字节
        /// </summary>
        public int totalSize { get; set; }

        /// <summary>
        /// 视频流码率平均值与音频流码率平均值之和。 单位：bps
        /// </summary>
        public int bitrate { get; set; }

        /// <summary>
        /// 视频流高度的最大值。单位：px
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 视频流宽度的最大值。单位：px
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// 容器类型，例如m4a，mp4等
        /// </summary>
        public string container { get; set; }

        /// <summary>
        /// 视频的md5值
        /// </summary>
        public string md5 { get; set; }

        /// <summary>
        /// 视频流信息
        /// </summary>
        public List<videoStream> videoStreamList { get; set; }

        /// <summary>
        /// 音频流信息
        /// </summary>
        public List<audioStream> audioStreamList { get; set; }
    }

    /// <summary>
    /// 视频流信息
    /// </summary>
    public class videoStream
    {
        /// <summary>
        /// 视频流码率平均值与音频流码率平均值之和。 单位：bps
        /// </summary>
        public int bitrate { get; set; }

        /// <summary>
        /// 编码解码器
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 每秒帧数  英尺/秒（feet per second）
        /// </summary>
        public int fps { get; set; }

        /// <summary>
        /// 视频高度
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 视频宽度
        /// </summary>
        public int width { get; set; }
    }

    /// <summary>
    /// 音频流信息
    /// </summary>
    public class audioStream
    {
        /// <summary>
        /// 音频流码率平均值。 单位：bps
        /// </summary>
        public int bitrate { get; set; }

        /// <summary>
        /// 编码解码器
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 取样频率
        /// </summary>
        public int samplingRate { get; set; }
    }

    /// <summary>
    /// 视频元信息
    /// </summary>
    public class MetaData
    {
        /// <summary>
        /// 视频大小。单位：字节
        /// </summary>
        public int size { get; set; }

        /// <summary>
        /// 容器类型，例如m4a，mp4等
        /// </summary>
        public string container { get; set; }

        /// <summary>
        /// 视频流码率平均值与音频流码率平均值之和。 单位：bps
        /// </summary>
        public int bitrate { get; set; }

        /// <summary>
        /// 视频流高度的最大值。单位：px
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 视频流宽度的最大值。单位：px
        /// </summary>
        public int width { get; set; }

        /// <summary>
        ///  视频的md5值，目前暂不支持
        /// </summary>
        public string md5 { get; set; }

        /// <summary>
        /// 视频时长。单位：秒
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// Float表示的视频时长，更精确。单位：秒
        /// </summary>
        public float floatDuration { get; set; }

        /// <summary>
        /// 视频拍摄时的选择角度。单位：度
        /// </summary>
        public int rotate { get; set; }

        /// <summary>
        /// 视频文件总大小（视频为 HLS 时，大小是 m3u8 和 ts 文件大小的总和）。单位：字节
        /// </summary>
        public int totalSize { get; set; }
    }
}
