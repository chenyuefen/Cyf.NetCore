using System;
using TM.Infrastructure.Configs;

namespace TM.Infrastructure.TencentCloud.Config
{
    public class WxPayConfig
    {
        /// <summary>
        /// 商户号
        /// </summary>
        public static string mchId = ConfigHelper.Configuration["WeChatPay:MchId"];

        /// <summary>
        /// 密钥
        /// </summary>
        public static string key = ConfigHelper.Configuration["WeChatPay:Key"];

        /// <summary>
        /// 微信支付证书路径
        /// </summary>
        public static string cerPath = $"{Environment.CurrentDirectory}/{ConfigHelper.Configuration["WeChatPay:Certificate"]}";

        /// <summary>
        /// RSA 公钥路径
        /// </summary>
        public static string pempath = $"{Environment.CurrentDirectory}/{ConfigHelper.Configuration["WeChatPay:RsaPublicKey"]}";

    }
}