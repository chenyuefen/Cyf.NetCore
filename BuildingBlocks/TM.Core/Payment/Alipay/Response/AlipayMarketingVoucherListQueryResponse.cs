using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingVoucherListQueryResponse.
    /// </summary>
    public class AlipayMarketingVoucherListQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 券列表，一定不为null，但是size可以为0
        /// </summary>
        [JsonProperty("vouchers")]
        [XmlArray("vouchers")]
        [XmlArrayItem("voucher_lite_info")]
        public List<VoucherLiteInfo> Vouchers { get; set; }
    }
}
