using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayBossProdArrangementOfflineQueryResponse.
    /// </summary>
    public class AlipayBossProdArrangementOfflineQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 商户的签约状态
        /// </summary>
        [JsonProperty("sign_status")]
        [XmlElement("sign_status")]
        public string SignStatus { get; set; }
    }
}
