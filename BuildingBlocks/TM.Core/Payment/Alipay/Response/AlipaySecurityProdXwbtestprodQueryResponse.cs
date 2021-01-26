using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityProdXwbtestprodQueryResponse.
    /// </summary>
    public class AlipaySecurityProdXwbtestprodQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 11000
        /// </summary>
        [JsonProperty("out_a")]
        [XmlElement("out_a")]
        public string OutA { get; set; }

        /// <summary>
        /// xxxx
        /// </summary>
        [JsonProperty("out_b")]
        [XmlElement("out_b")]
        public long OutB { get; set; }
    }
}
