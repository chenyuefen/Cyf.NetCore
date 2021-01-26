using Newtonsoft.Json;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiAdvertDeliveryDiscountQueryResponse.
    /// </summary>
    public class KoubeiAdvertDeliveryDiscountQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 广告投放出去的优惠信息
        /// </summary>
        [JsonProperty("discount")]
        [XmlElement("discount")]
        public DiscountInfo Discount { get; set; }
    }
}
