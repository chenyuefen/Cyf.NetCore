using TM.Infrastructure.TencentCloud.Entities.Vod.Common;

namespace TM.Infrastructure.TencentCloud.Entities.Vod.Response
{

    /// <summary>
    /// 转码模板列表
    /// </summary>
    public class TranscodeTemplate
    {
        /// <summary>
        /// 转码模板的名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 容器类型，例如 m4a，mp4 等
        /// </summary>
        public string container { get; set; }

        /// <summary>
        /// 对该模板的描述
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 模板创建时间（Unix时间戳）
        /// </summary>
        public int createTime { get; set; }

        /// <summary>
        /// 模板信息最近更新时间（Unix时间戳）
        /// </summary>
        public int updateTime { get; set; }

        /// <summary>
        /// 去除视频数据，1为去除，0为保留，默认为0
        /// </summary>
        public int isFiltrateVideo { get; set; }

        /// <summary>
        /// 去除音频数据，1为去除，0为保留，默认为0
        /// </summary>
        public int isFiltrateAudio { get; set; }

        /// <summary>
        /// 参见视频流配置参数，当 isFiltrateVideo 为1，则该字段将被忽略
        /// </summary>
        public VideoInfo video { get; set; }

        /// <summary>
        /// 参见音频流配置参数，若 isFiltrateAudio 为1，则该字段将被忽略
        /// </summary>
        public AudioInfo audio { get; set; }

        /// <summary>
        /// 是否为系统预置转码模板，1表示是，0表示否
        /// </summary>
        public int onPremise { get; set; }

        /// <summary>
        /// 是否为默认模板，1表示是，0表示否
        /// </summary>
        public int status { get; set; }
    }
}
