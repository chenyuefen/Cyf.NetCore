namespace TM.Infrastructure.TencentCloud.Entities.Vod.Common
{
    /// <summary>
    /// 视频数据
    /// </summary>
    public class VideoInfo
    {
        /// <summary>
        /// 视频流的编码格式， 目前有：libx264（H.264 编码），libx265（H.265 编码）
        /// </summary>
        public int codec { get; set; }

        /// <summary>
        /// 帧率，单位：Hz
        /// </summary>
        public float fps { get; set; }

        /// <summary>
        /// 分辨率开启自适应：1为开启，0为关闭。 若为1，则width的值用于较长边，height的值用于较短边, 默认为1
        /// </summary>
        public int resolutionSelfAdapting { get; set; }

        /// <summary>
        /// 视频流宽度的最大值，如果该值为0，height 非0，则按比例缩放；反之，若 height 为0，则表示同源。 单位：px
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// 视频流高度的最大值，如果该值为0，width 非0，则按比例缩放, 反之，若 width 为0，则表示同源。单位：px
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 视频流的码率，单位：kbps
        /// </summary>
        public int bitrate { get; set; }

        /// <summary>
        /// 视频关键帧最小间隔，取值范围1~10，单位：秒
        /// </summary>
        public int minGop { get; set; }

        /// <summary>
        /// 视频关键帧最大间隔，取值范围1~10，单位：秒
        /// </summary>
        public int maxGop { get; set; }

        /// <summary>
        /// 视频编码档次，取值 baseline，main，high
        /// </summary>
        public string videoProfile { get; set; }

        /// <summary>
        /// 视频色度空间，取值 yuv420p，yuv420p10le
        /// </summary>
        public string colorSpace { get; set; }

        /// <summary>
        /// 视频去隔行模式，1：去隔行，0：保持视频隔行模式
        /// </summary>
        public int deinterlaced { get; set; }

        /// <summary>
        /// 视频编码模式，0表示 one pass，1表示 two pass
        /// </summary>
        public int videoRateControl { get; set; }

        /// <summary>
        /// 视频降噪参数
        /// </summary>
        public Denoise denoise { get; set; }
    }

    /// <summary>
    /// 是否启用视频降噪处理， 1：启用，0：不启用
    /// </summary>
    public class Denoise
    {
        public int enable { get; set; }
    }
}
