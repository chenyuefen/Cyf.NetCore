using TM.Infrastructure.TencentCloud.Entities.Base;

namespace TM.Infrastructure.TencentCloud.Entities.Vod.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class VodFileConvertJsonResult : WxJsonResult
    {
        /// <summary>
        /// 任务id
        /// </summary>
        public string vodTaskId { get; set; }
    }
}
