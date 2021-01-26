using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOfflineMarketingVoucherModifyResponse.
    /// </summary>
    public class AlipayOfflineMarketingVoucherModifyResponse : AlipayResponse
    {
        /// <summary>
        /// 券id
        /// </summary>
        [JsonProperty("voucher_id")]
        [XmlElement("voucher_id")]
        public string VoucherId { get; set; }

        /// <summary>
        /// 券模板状态,EFFECTIVE=生效，INVALID=失效
        /// </summary>
        [JsonProperty("voucher_status")]
        [XmlElement("voucher_status")]
        public string VoucherStatus { get; set; }
    }
}
