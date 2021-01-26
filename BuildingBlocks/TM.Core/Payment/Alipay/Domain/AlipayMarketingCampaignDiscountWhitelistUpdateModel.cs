using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingCampaignDiscountWhitelistUpdateModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayMarketingCampaignDiscountWhitelistUpdateModel : AlipayObject
    {
        /// <summary>
        /// 活动id
        /// </summary>
        [JsonProperty("camp_id")]
        [XmlElement("camp_id")]
        public string CampId { get; set; }

        /// <summary>
        /// 白名单。逗号分隔开
        /// </summary>
        [JsonProperty("user_white_list")]
        [XmlElement("user_white_list")]
        public string UserWhiteList { get; set; }
    }
}
