using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEbppJfexportBillQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEbppJfexportBillQueryModel : AlipayObject
    {
        /// <summary>
        /// 支付宝的业务订单号，具有唯一性和幂等性
        /// </summary>
        [JsonProperty("bill_no")]
        [XmlElement("bill_no")]
        public string BillNo { get; set; }

        /// <summary>
        /// 业务类型英文名称
        /// </summary>
        [JsonProperty("biz_type")]
        [XmlElement("biz_type")]
        public string BizType { get; set; }

        /// <summary>
        /// 拓展字段，json串(key-value对)
        /// </summary>
        [JsonProperty("extend_field")]
        [XmlElement("extend_field")]
        public string ExtendField { get; set; }
    }
}
