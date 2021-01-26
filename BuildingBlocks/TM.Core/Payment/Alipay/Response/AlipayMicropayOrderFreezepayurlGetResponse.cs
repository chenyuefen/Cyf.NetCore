using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMicropayOrderFreezepayurlGetResponse.
    /// </summary>
    public class AlipayMicropayOrderFreezepayurlGetResponse : AlipayResponse
    {
        /// <summary>
        /// 支付冻结金的地址
        /// </summary>
        [JsonProperty("pay_freeze_url")]
        [XmlElement("pay_freeze_url")]
        public string PayFreezeUrl { get; set; }
    }
}
