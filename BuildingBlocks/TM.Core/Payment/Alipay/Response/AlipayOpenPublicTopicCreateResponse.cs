using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicTopicCreateResponse.
    /// </summary>
    public class AlipayOpenPublicTopicCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 营销位id
        /// </summary>
        [JsonProperty("topic_id")]
        [XmlElement("topic_id")]
        public string TopicId { get; set; }
    }
}
