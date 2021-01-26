using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayInsAutoAutoinsprodUserCertifyResponse.
    /// </summary>
    public class AlipayInsAutoAutoinsprodUserCertifyResponse : AlipayResponse
    {
        /// <summary>
        /// 验证结果
        /// </summary>
        [JsonProperty("agent_cert_result")]
        [XmlElement("agent_cert_result")]
        public string AgentCertResult { get; set; }
    }
}
