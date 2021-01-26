using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// OrderDetailResult Data Structure.
    /// </summary>
    [Serializable]
    public class OrderDetailResult : AlipayObject
    {
        /// <summary>
        /// 应用唯一标识
        /// </summary>
        [JsonProperty("app_id")]
        [XmlElement("app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [JsonProperty("out_trade_no")]
        [XmlElement("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 公用回传参数，如果请求时传递了该参数，则返回给商户时会回传该参数。
        /// </summary>
        [JsonProperty("passback_params")]
        [XmlElement("passback_params")]
        public string PassbackParams { get; set; }

        /// <summary>
        /// 卖家支付宝用户ID。
        /// </summary>
        [JsonProperty("seller_id")]
        [XmlElement("seller_id")]
        public string SellerId { get; set; }

        /// <summary>
        /// 订单标题
        /// </summary>
        [JsonProperty("subject")]
        [XmlElement("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 订单总金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000]
        /// </summary>
        [JsonProperty("total_amount")]
        [XmlElement("total_amount")]
        public string TotalAmount { get; set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [JsonProperty("trade_no")]
        [XmlElement("trade_no")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 交易状态，有以下几种取值：  1. TRADE_SUCCESS：付款成功  2. TRADE_FINISHED：交易完成  3. WAIT_BUYER_PAY：等待支付  4. TRADE_CLOSED：交易关闭
        /// </summary>
        [JsonProperty("trade_status")]
        [XmlElement("trade_status")]
        public string TradeStatus { get; set; }
    }
}
