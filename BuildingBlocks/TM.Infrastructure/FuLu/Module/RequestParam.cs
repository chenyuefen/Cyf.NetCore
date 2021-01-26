using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Infrastructure.FuLu.Module
{
    public class RequestParam
    {
        /// <summary>
        /// 开放平台分配给商户的app_key
        /// </summary>
        public string app_key { get; set; }
        /// <summary>
        /// 接口方法名称
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 时间戳，格式为：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 调用的接口版本
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 接口请求或响应格式
        /// </summary>
        public string format { get; set; }
        /// <summary>
        /// 请求使用的编码格式 默认utf-8
        /// </summary>
        public string charset { get; set; } = "utf-8";
        /// <summary>
        /// 签名加密类型，目前仅支持md5  默认md5
        /// </summary>
        public string sign_type { get; set; } = "md5";
        /// <summary>
        /// 签名串，签名规则详见右侧《常见问题》中的“ 3.签名计算规则说明 ”
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 授权码，固定值为“”
        /// </summary>
        public string app_auth_token { get; set; }
        /// <summary>
        /// 请求参数集合（注意：该参数是以json字符串的形式传输）
        /// </summary>
        public string biz_content { get; set; }
    }
}
