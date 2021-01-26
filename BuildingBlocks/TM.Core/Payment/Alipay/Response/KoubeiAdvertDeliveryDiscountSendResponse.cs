using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiAdvertDeliveryDiscountSendResponse.
    /// </summary>
    public class KoubeiAdvertDeliveryDiscountSendResponse : AlipayResponse
    {
        /// <summary>
        /// 权益详细信息：  partnerId：商户ID（用于打开手机钱包券详情）
        /// </summary>
        [JsonProperty("benefit_detail")]
        [XmlElement("benefit_detail")]
        public string BenefitDetail { get; set; }

        /// <summary>
        /// 广告id对应的权益id
        /// </summary>
        [JsonProperty("benefit_id")]
        [XmlElement("benefit_id")]
        public string BenefitId { get; set; }
    }
}
