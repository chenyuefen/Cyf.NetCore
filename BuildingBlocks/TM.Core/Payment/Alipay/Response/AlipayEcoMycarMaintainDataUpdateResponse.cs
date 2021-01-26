using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoMycarMaintainDataUpdateResponse.
    /// </summary>
    public class AlipayEcoMycarMaintainDataUpdateResponse : AlipayResponse
    {
        /// <summary>
        /// 具体返回的处理结果
        /// </summary>
        [JsonProperty("info")]
        [XmlElement("info")]
        public string Info { get; set; }
    }
}
