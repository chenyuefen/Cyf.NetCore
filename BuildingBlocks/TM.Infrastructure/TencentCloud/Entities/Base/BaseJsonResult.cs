using System;
using TM.Infrastructure.TencentCloud.Entities.Base.Interface;

namespace TM.Infrastructure.TencentCloud.Entities.Base
{
    /// <summary>
    /// 所有xxJsonResult（基类）的基类
    /// </summary>
    [Serializable]
    public abstract class BaseJsonResult : IJsonResult
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 状态  成功：Success
        /// </summary>
        public string codeDesc { get; set; }

        /// <summary>
        /// errcode的
        /// </summary>
        public abstract int ErrorCodeValue { get; }
    }
}
