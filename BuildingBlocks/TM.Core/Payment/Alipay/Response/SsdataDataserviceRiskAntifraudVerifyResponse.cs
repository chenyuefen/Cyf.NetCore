using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// SsdataDataserviceRiskAntifraudVerifyResponse.
    /// </summary>
    public class SsdataDataserviceRiskAntifraudVerifyResponse : AlipayResponse
    {
        /// <summary>
        /// 蚁盾对于每一次请求返回的业务号。后续可以通过此业务号进行对账
        /// </summary>
        [JsonProperty("biz_no")]
        [XmlElement("biz_no")]
        public string BizNo { get; set; }

        /// <summary>
        /// 用户唯一请求id
        /// </summary>
        [JsonProperty("unique_id")]
        [XmlElement("unique_id")]
        public string UniqueId { get; set; }

        /// <summary>
        /// <a href="https://doc.open.alipay.com/docs/doc.htm?treeId=272&articleId=105917&docType=1">验证code列表</a>
        /// </summary>
        [JsonProperty("verify_code")]
        [XmlElement("verify_code")]
        public string VerifyCode { get; set; }
    }
}
