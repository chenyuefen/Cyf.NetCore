using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEcoMycarParkingOrderRefundModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEcoMycarParkingOrderRefundModel : AlipayObject
    {
        /// <summary>
        /// 代扣时返回的支付宝支付交易流水号，系统唯一
        /// </summary>
        [JsonProperty("order_no")]
        [XmlElement("order_no")]
        public string OrderNo { get; set; }

        /// <summary>
        /// ISV代扣订单号，ISV唯一
        /// </summary>
        [JsonProperty("out_order_no")]
        [XmlElement("out_order_no")]
        public string OutOrderNo { get; set; }

        /// <summary>
        /// 外部申请退款请求流水，ISV唯一
        /// </summary>
        [JsonProperty("out_refund_no")]
        [XmlElement("out_refund_no")]
        public string OutRefundNo { get; set; }

        /// <summary>
        /// 退款金额，保留小数点后两位
        /// </summary>
        [JsonProperty("refund_fee")]
        [XmlElement("refund_fee")]
        public string RefundFee { get; set; }

        /// <summary>
        /// 退款理由
        /// </summary>
        [JsonProperty("refund_reason")]
        [XmlElement("refund_reason")]
        public string RefundReason { get; set; }
    }
}
