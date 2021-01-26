using TM.Infrastructure.TencentCloud.Entities.Base;

namespace TM.Infrastructure.TencentCloud.Entities.Vod.Response
{
    public class TranscodeTemplateJsonResult : WxJsonResult
    {
        /// <summary>
        /// 转码模板信息
        /// </summary>
        public TranscodeTemplate data { get; set; }
    }
}
