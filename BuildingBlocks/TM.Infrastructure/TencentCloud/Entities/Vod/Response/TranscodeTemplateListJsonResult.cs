using System.Collections.Generic;
using TM.Infrastructure.TencentCloud.Entities.Base;

namespace TM.Infrastructure.TencentCloud.Entities.Vod.Response
{
    /// <summary>
    /// 查询转码模板列表
    /// </summary>
    public class TranscodeTemplateListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 转码模板列表
        /// </summary>
        public List<TranscodeTemplate> data { get; set; }
    }
}
