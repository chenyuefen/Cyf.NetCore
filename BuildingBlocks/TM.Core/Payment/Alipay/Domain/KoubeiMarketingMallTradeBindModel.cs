using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiMarketingMallTradeBindModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiMarketingMallTradeBindModel : AlipayObject
    {
        /// <summary>
        /// 用户的授权动作：auth授权，unAuth取消授权
        /// </summary>
        [JsonProperty("action")]
        [XmlElement("action")]
        public string Action { get; set; }

        /// <summary>
        /// 蚂蚁统一会员ID
        /// </summary>
        [JsonProperty("user_id")]
        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}
