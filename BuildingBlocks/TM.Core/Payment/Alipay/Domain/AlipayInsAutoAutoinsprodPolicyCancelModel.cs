using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayInsAutoAutoinsprodPolicyCancelModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayInsAutoAutoinsprodPolicyCancelModel : AlipayObject
    {
        /// <summary>
        /// 车险订单号
        /// </summary>
        [JsonProperty("trade_biz_id")]
        [XmlElement("trade_biz_id")]
        public string TradeBizId { get; set; }
    }
}
