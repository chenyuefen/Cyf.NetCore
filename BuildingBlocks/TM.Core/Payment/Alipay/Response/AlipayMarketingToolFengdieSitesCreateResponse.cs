using Newtonsoft.Json;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieSitesCreateResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieSitesCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 创建站点的返回值模型
        /// </summary>
        [JsonProperty("data")]
        [XmlElement("data")]
        public FengdieActivityCreateModel Data { get; set; }
    }
}
