using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiAdvertDeliveryDiscountQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiAdvertDeliveryDiscountQueryModel : AlipayObject
    {
        /// <summary>
        /// 优惠id，调用优惠查询接口时，需要提供“优惠id”
        /// </summary>
        [JsonProperty("item_id")]
        [XmlElement("item_id")]
        public string ItemId { get; set; }
    }
}
