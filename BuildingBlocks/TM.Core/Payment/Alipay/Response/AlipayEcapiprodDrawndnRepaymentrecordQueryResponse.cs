using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcapiprodDrawndnRepaymentrecordQueryResponse.
    /// </summary>
    public class AlipayEcapiprodDrawndnRepaymentrecordQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 还款流水
        /// </summary>
        [JsonProperty("repayment_records")]
        [XmlArray("repayment_records")]
        [XmlArrayItem("repayment_records")]
        public List<RepaymentRecords> RepaymentRecords { get; set; }

        /// <summary>
        /// 唯一标识这次请求
        /// </summary>
        [JsonProperty("request_id")]
        [XmlElement("request_id")]
        public string RequestId { get; set; }
    }
}
