using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicTopicBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicTopicBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 营销位列表
        /// </summary>
        [JsonProperty("topic_list")]
        [XmlArray("topic_list")]
        [XmlArrayItem("topic")]
        public List<Topic> TopicList { get; set; }
    }
}
