using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiShopMallMemberQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiShopMallMemberQueryModel : AlipayObject
    {
        /// <summary>
        /// 商圈ID
        /// </summary>
        [JsonProperty("mall_id")]
        [XmlElement("mall_id")]
        public string MallId { get; set; }

        /// <summary>
        /// 蚂蚁统一会员ID
        /// </summary>
        [JsonProperty("user_id")]
        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}
