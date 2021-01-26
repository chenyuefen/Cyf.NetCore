using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceCityfacilitatorDepositQueryResponse.
    /// </summary>
    public class AlipayCommerceCityfacilitatorDepositQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 待圈存明细
        /// </summary>
        [JsonProperty("recharge_bills")]
        [XmlArray("recharge_bills")]
        [XmlArrayItem("recharge_bill")]
        public List<RechargeBill> RechargeBills { get; set; }
    }
}
