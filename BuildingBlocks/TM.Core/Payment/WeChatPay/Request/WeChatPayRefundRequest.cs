using System.Collections.Generic;
using TM.Core.Payment.WeChatPay.Response;

namespace TM.Core.Payment.WeChatPay.Request
{
    /// <summary>
    /// 申请退款
    /// </summary>
    public class WeChatPayRefundRequest : IWeChatPayCertificateRequest<WeChatPayRefundResponse>
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 子商户公众账号ID
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 子商户号
        /// </summary>
        public string SubMchId { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public int TotalFee { get; set; }

        /// <summary>
        /// 申请退款金额
        /// </summary>
        public int RefundFee { get; set; }

        /// <summary>
        /// 退款货币种类
        /// </summary>
        public string RefundFeeType { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public string RefundDesc { get; set; }

        /// <summary>
        /// 退款资金来源
        /// </summary>
        public string RefundAccount { get; set; }

        /// <summary>
        /// 退款结果通知url
        /// </summary>
        public string NotifyUrl { get; set; }

        #region IWeChatPayCertificateRequest Members

        public string GetRequestUrl()
        {
            return "https://api.mch.weixin.qq.com/secapi/pay/refund";
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new WeChatPayDictionary()
            {
                { "appid", AppId },
                { "sub_appid", SubAppId },
                { "sub_mch_id", SubMchId },
                { "transaction_id", TransactionId },
                { "out_trade_no", OutTradeNo },
                { "out_refund_no", OutRefundNo },
                { "total_fee", TotalFee },
                { "refund_fee", RefundFee },
                { "refund_fee_type", RefundFeeType },
                { "refund_desc", RefundDesc },
                { "refund_account", RefundAccount },
                { "notify_url", NotifyUrl },
            };
            return parameters;
        }

        #endregion
    }
}
