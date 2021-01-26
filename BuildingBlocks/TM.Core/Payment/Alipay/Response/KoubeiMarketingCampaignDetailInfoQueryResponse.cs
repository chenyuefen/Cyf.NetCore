using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignDetailInfoQueryResponse.
    /// </summary>
    public class KoubeiMarketingCampaignDetailInfoQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 适用门店:门店与门店之间用“,”隔开
        /// </summary>
        [JsonProperty("limit_shops")]
        [XmlElement("limit_shops")]
        public string LimitShops { get; set; }
    }
}
