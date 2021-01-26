using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenPublicLifeMsgRecallModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenPublicLifeMsgRecallModel : AlipayObject
    {
        /// <summary>
        /// 消息id
        /// </summary>
        [JsonProperty("message_id")]
        [XmlElement("message_id")]
        public string MessageId { get; set; }
    }
}
