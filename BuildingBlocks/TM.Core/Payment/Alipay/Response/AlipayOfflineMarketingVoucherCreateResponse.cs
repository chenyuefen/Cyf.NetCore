using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOfflineMarketingVoucherCreateResponse.
    /// </summary>
    public class AlipayOfflineMarketingVoucherCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 活动id，过渡方案的兼容字段
        /// </summary>
        [JsonProperty("activity_id")]
        [XmlElement("activity_id")]
        public string ActivityId { get; set; }

        /// <summary>
        /// 券模板id
        /// </summary>
        [JsonProperty("voucher_id")]
        [XmlElement("voucher_id")]
        public string VoucherId { get; set; }
    }
}
