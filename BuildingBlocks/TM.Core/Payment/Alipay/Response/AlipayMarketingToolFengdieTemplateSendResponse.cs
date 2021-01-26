using Newtonsoft.Json;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieTemplateSendResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieTemplateSendResponse : AlipayResponse
    {
        /// <summary>
        /// 分配模板的操作是否成功
        /// </summary>
        [JsonProperty("data")]
        [XmlElement("data")]
        public FengdieSuccessRespModel Data { get; set; }
    }
}
