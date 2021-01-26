using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicMessageContentModifyResponse.
    /// </summary>
    public class AlipayOpenPublicMessageContentModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 内容id
        /// </summary>
        [JsonProperty("content_id")]
        [XmlElement("content_id")]
        public string ContentId { get; set; }

        /// <summary>
        /// 内容详情内链url
        /// </summary>
        [JsonProperty("content_url")]
        [XmlElement("content_url")]
        public string ContentUrl { get; set; }
    }
}
