using System.Collections.Generic;
using TM.Infrastructure;

namespace TM.Core.Logs.Abstractions
{
    /// <summary>
    /// 日志转换器
    /// </summary>
    public interface ILogConvert
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <returns></returns>
        List<Item> To();
    }
}
