using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiCateringOrderInfoVerifyModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiCateringOrderInfoVerifyModel : AlipayObject
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        [JsonProperty("shop_id")]
        [XmlElement("shop_id")]
        public string ShopId { get; set; }

        /// <summary>
        /// 用户核销码中的核销数值串
        /// </summary>
        [JsonProperty("verify_order_id")]
        [XmlElement("verify_order_id")]
        public string VerifyOrderId { get; set; }
    }
}
