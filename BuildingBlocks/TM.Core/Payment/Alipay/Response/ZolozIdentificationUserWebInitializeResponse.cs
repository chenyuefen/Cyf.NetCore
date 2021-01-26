using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// ZolozIdentificationUserWebInitializeResponse.
    /// </summary>
    public class ZolozIdentificationUserWebInitializeResponse : AlipayResponse
    {
        /// <summary>
        /// 扩展结果
        /// </summary>
        [JsonProperty("extern_info")]
        [XmlElement("extern_info")]
        public string ExternInfo { get; set; }

        /// <summary>
        /// 刷脸认证的唯一标识
        /// </summary>
        [JsonProperty("zim_id")]
        [XmlElement("zim_id")]
        public string ZimId { get; set; }
    }
}
