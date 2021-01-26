using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenServicemarketOrderQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenServicemarketOrderQueryModel : AlipayObject
    {
        /// <summary>
        /// 商户订单ID号
        /// </summary>
        [JsonProperty("commodity_order_id")]
        [XmlElement("commodity_order_id")]
        public string CommodityOrderId { get; set; }

        /// <summary>
        /// 从第几页开始查询
        /// </summary>
        [JsonProperty("start_page")]
        [XmlElement("start_page")]
        public string StartPage { get; set; }
    }
}
