using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoMycarDataExternalSendResponse.
    /// </summary>
    public class AlipayEcoMycarDataExternalSendResponse : AlipayResponse
    {
        /// <summary>
        /// 20
        /// </summary>
        [JsonProperty("external_system_name")]
        [XmlElement("external_system_name")]
        public string ExternalSystemName { get; set; }

        /// <summary>
        /// outter_response
        /// </summary>
        [JsonProperty("process_result")]
        [XmlElement("process_result")]
        public string ProcessResult { get; set; }
    }
}
