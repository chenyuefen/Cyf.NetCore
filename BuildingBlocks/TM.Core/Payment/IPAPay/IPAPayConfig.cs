namespace TM.Core.Payment.IPAPay
{
    /// <summary>
    /// IPA支付配置
    /// </summary>
    public static class IPAPayConfig
    {
        /// <summary>
        /// 正式购买地址
        /// </summary>
        public readonly static string VERIFY_URL = "https://buy.itunes.apple.com/verifyReceipt";

        /// <summary>
        /// 沙盒测试购买地址
        /// </summary>
        public readonly static string SANDBOX_URL = "https://sandbox.itunes.apple.com/verifyReceipt";
    }
}
