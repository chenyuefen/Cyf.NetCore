using System;
using TM.Infrastructure.TencentCloud.Entities.Base.Interface;
using TM.Infrastructure.TencentCloud.Enums;

namespace TM.Infrastructure.TencentCloud.Entities.Base
{
    /// <summary>
    /// 包含 errorcode 的 Json 返回结果接口
    /// </summary>
    public interface IWxJsonResult : IJsonResult
    {
        /// <summary>
        /// 返回结果代码
        /// </summary>
        ReturnCode code { get; set; }
    }

    /// <summary>
    /// JSON 返回结果
    /// </summary>
    [Serializable]
    public class WxJsonResult : BaseJsonResult
    {
        public ReturnCode code { get; set; }

        /// <summary>
        /// 返回消息代码数字（同code枚举值）
        /// </summary>
        public override int ErrorCodeValue { get { return (int)code; } }


        public override string ToString()
        {
            return string.Format("WxJsonResult：{{code:'{0}',errcode_name:'{1}',message:'{2}'}}",
                (int)code, code.ToString(), message);
        }
    }
}
