using Newtonsoft.Json;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataserviceChinaremodelQueryResponse.
    /// </summary>
    public class AlipayDataDataserviceChinaremodelQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 中再核保模型查询结果
        /// </summary>
        [JsonProperty("result")]
        [XmlElement("result")]
        public AlipayChinareModelResult Result { get; set; }
    }
}
