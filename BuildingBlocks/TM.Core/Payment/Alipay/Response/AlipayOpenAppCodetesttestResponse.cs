using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenAppCodetesttestResponse.
    /// </summary>
    public class AlipayOpenAppCodetesttestResponse : AlipayResponse
    {
        /// <summary>
        /// 测试测试测试
        /// </summary>
        [JsonProperty("testtesttest")]
        [XmlElement("testtesttest")]
        public string Testtesttest { get; set; }
    }
}
