using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCdpAdvertiseCreateResponse.
    /// </summary>
    public class AlipayMarketingCdpAdvertiseCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 创建广告唯一标识
        /// </summary>
        [JsonProperty("ad_id")]
        [XmlElement("ad_id")]
        public string AdId { get; set; }
    }
}
