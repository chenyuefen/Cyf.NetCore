using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEcoEntertainmentOrderUploadModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEcoEntertainmentOrderUploadModel : AlipayObject
    {
        /// <summary>
        /// 数娱充值ISV订单回流模型
        /// </summary>
        [JsonProperty("entertainment_order_info")]
        [XmlElement("entertainment_order_info")]
        public EntertainmentOrderInfo EntertainmentOrderInfo { get; set; }
    }
}
