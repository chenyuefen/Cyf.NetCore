using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCampaignActivityOfflineTriggerResponse.
    /// </summary>
    public class AlipayMarketingCampaignActivityOfflineTriggerResponse : AlipayResponse
    {
        /// <summary>
        /// 外部奖品ID
        /// </summary>
        [JsonProperty("out_prize_id")]
        [XmlElement("out_prize_id")]
        public string OutPrizeId { get; set; }
    }
}
