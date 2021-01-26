using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicContactFollowBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicContactFollowBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 联系人关注者列表
        /// </summary>
        [JsonProperty("contact_follow_list")]
        [XmlArray("contact_follow_list")]
        [XmlArrayItem("contact_follower")]
        public List<ContactFollower> ContactFollowList { get; set; }
    }
}
