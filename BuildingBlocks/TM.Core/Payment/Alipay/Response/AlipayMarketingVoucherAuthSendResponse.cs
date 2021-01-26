using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingVoucherAuthSendResponse.
    /// </summary>
    public class AlipayMarketingVoucherAuthSendResponse : AlipayResponse
    {
        /// <summary>
        /// 券ID
        /// </summary>
        [JsonProperty("voucher_id")]
        [XmlElement("voucher_id")]
        public string VoucherId { get; set; }
    }
}
