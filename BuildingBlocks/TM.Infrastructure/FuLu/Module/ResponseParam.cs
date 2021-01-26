using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Infrastructure.FuLu.Module
{
    public class ResponseParam
    {
        /// <summary>
        /// 返回码，详见底部《业务错误码》
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回码描述，详见底部《业务错误码》
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 响应结果
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 签名串，签名规则详见右侧《常见问题》中的“ 3.签名计算规则说明 ”
        /// </summary>
        public string sign { get; set; }
    }
}
