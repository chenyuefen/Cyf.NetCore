using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipaySocialBaseChatGinvSendModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipaySocialBaseChatGinvSendModel : AlipayObject
    {
        /// <summary>
        /// 群id
        /// </summary>
        [JsonProperty("group_id")]
        [XmlElement("group_id")]
        public string GroupId { get; set; }

        /// <summary>
        /// 邀请的好友id列表，最多50人
        /// </summary>
        [JsonProperty("uids")]
        [XmlArray("uids")]
        [XmlArrayItem("string")]
        public List<string> Uids { get; set; }
    }
}
