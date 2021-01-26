using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayTrustUserAuthSendResponse.
    /// </summary>
    public class AlipayTrustUserAuthSendResponse : AlipayResponse
    {
        /// <summary>
        /// 当授权通知发送成功时，为T；否则用业务错误码表示
        /// </summary>
        [JsonProperty("result")]
        [XmlElement("result")]
        public string Result { get; set; }
    }
}
