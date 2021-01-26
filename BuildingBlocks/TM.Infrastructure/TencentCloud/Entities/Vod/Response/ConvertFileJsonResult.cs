using TM.Infrastructure.TencentCloud.Entities.Base;

namespace TM.Infrastructure.TencentCloud.Entities.Vod.Response
{
    public class ConvertFileJsonResult : WxJsonResult
    {
        /// <summary>
        /// 视频转码ID
        /// </summary>
        public string vodTaskId { get; set; }
    }
}
