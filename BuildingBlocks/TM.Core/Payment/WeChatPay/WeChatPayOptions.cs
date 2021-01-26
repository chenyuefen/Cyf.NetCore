namespace TM.Core.Payment.WeChatPay
{
    public class WeChatPayOptions
    {
        /// <summary>
        /// 应用账号(公众账号ID/小程序ID/企业号CorpId)
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// API秘钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// API证书文件 文件路径或文件的Base64串
        /// </summary>
        public string Certificate { get; set; }

        /// <summary>
        /// RSA公钥 企业付款到银行卡
        /// </summary>
        public string RsaPublicKey { get; set; }

        /// <summary>
        /// 通知地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 退款通知地址
        /// </summary>
        public string RefundNotifyUrl { get; set; }
    }
}
