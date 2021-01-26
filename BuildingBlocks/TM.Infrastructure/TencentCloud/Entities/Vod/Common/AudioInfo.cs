namespace TM.Infrastructure.TencentCloud.Entities.Vod.Common
{
    /// <summary>
    /// 音频数据
    /// </summary>
    public class AudioInfo
    {
        /// <summary>
        /// 音频流的编码格式，目前有：1、libfdk_aac（更适合 mp4 和 hls）  2、libmp3lame（更适合 flv）  3、mp2
        /// </summary>
        public string codec { get; set; }

        /// <summary>
        /// 音频流的码率，单位：kbps
        /// </summary>
        public int bitrate { get; set; }

        /// <summary>
        /// 音频通道方式，1：双通道，2：双通道，6：立体声
        /// </summary>
        public string soundSystem { get; set; }

        /// <summary>
        /// 音频流的采样率。 单位：Hz
        /// </summary>
        public int sampleRate { get; set; }

        /// <summary>
        /// 音频重新采样参数，如果音频声道数超过2个，重新采样为2声道。目前只支持 soxr
        /// </summary>
        public string audioResampler { get; set; }

        /// <summary>
        /// 1表示音频重新采样时，如果声道数大于4个，在重新采样的基础上，再为中央声道做增益补偿。
        /// </summary>
        public int audioDownmixMode { get; set; }

        /// <summary>
        /// 音频编码档次（仅当 codec 为 libfdk_aac 时有效），取值有 aac_lc，aac_he 和 aac_he_v2 三种档次
        /// </summary>
        public string audioProfile { get; set; }
    }
}
