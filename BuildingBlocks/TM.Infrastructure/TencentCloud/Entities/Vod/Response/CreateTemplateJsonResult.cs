using TM.Infrastructure.TencentCloud.Entities.Base;

namespace TM.Infrastructure.TencentCloud.Entities.Vod.Response
{
    public class CreateTemplateJsonResult : WxJsonResult
    {
        /// <summary>
        /// 返回的数据信息
        /// </summary>
        public CreateResult data { get; set; }
    }

    /// <summary>
    /// 返回的数据信息
    /// </summary>
    public class CreateResult
    {
        /// <summary>
        /// 转码模板ID
        /// </summary>
        public int definition { get; set; }
    }
}
