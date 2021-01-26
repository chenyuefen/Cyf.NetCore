﻿using System.Collections.Generic;

namespace TM.Core.Payment.WeChatPay
{
    public interface IWeChatPayCalcRequest
    {
        /// <summary>
        /// 获取所有的Key-Value形式的文本请求参数字典。其中：
        /// Key: 请求参数名
        /// Value: 请求参数文本值
        /// </summary>
        /// <returns>文本请求参数字典</returns>
        IDictionary<string, string> GetParameters();
    }
}
