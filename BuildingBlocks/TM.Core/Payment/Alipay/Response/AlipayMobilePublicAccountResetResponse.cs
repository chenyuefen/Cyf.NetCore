using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMobilePublicAccountResetResponse.
    /// </summary>
    public class AlipayMobilePublicAccountResetResponse : AlipayResponse
    {
        /// <summary>
        /// 这是新账户绑定成功后产生的协议号
        /// </summary>
        [JsonProperty("agreement_id")]
        [XmlElement("agreement_id")]
        public string AgreementId { get; set; }

        /// <summary>
        /// 结果码
        /// </summary>
        [JsonProperty("code")]
        [XmlElement("code")]
        public new string Code { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        [JsonProperty("msg")]
        [XmlElement("msg")]
        public new string Msg { get; set; }
    }
}
