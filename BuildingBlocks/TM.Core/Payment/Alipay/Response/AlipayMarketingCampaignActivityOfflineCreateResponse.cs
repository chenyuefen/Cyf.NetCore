using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignActivityOfflineCreateResponse.
    /// </summary>
    public class AlipayMarketingCampaignActivityOfflineCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 创建成功的活动id
        /// </summary>
        [JsonProperty("camp_id")]
        [XmlElement("camp_id")]
        public string CampId { get; set; }

        /// <summary>
        /// 创建成功的券模版id
        /// </summary>
        [JsonProperty("prize_id")]
        [XmlElement("prize_id")]
        public string PrizeId { get; set; }
    }
}
