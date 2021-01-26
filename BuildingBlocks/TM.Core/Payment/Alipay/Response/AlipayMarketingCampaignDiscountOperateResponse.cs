using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignDiscountOperateResponse.
    /// </summary>
    public class AlipayMarketingCampaignDiscountOperateResponse : AlipayResponse
    {
        /// <summary>
        /// 123
        /// </summary>
        [JsonProperty("camp_id")]
        [XmlElement("camp_id")]
        public string CampId { get; set; }
    }
}
