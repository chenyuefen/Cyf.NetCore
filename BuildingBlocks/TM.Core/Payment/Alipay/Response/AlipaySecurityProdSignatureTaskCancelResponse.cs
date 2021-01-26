using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdSignatureTaskCancelResponse.
    /// </summary>
    public class AlipaySecurityProdSignatureTaskCancelResponse : AlipayResponse
    {
        /// <summary>
        /// 是否更新成功
        /// </summary>
        [JsonProperty("success")]
        [XmlElement("success")]
        public bool Success { get; set; }
    }
}
