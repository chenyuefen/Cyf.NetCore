using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiMarketingToolPointsQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiMarketingToolPointsQueryModel : AlipayObject
    {
        /// <summary>
        /// 活动积分帐户
        /// </summary>
        [JsonProperty("activity_account")]
        [XmlElement("activity_account")]
        public string ActivityAccount { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty("user_id")]
        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}
