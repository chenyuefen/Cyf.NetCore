using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcapiprodDrawndnFeerecordQueryResponse.
    /// </summary>
    public class AlipayEcapiprodDrawndnFeerecordQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 费用交易流水
        /// </summary>
        [JsonProperty("fee_records")]
        [XmlArray("fee_records")]
        [XmlArrayItem("fee_records")]
        public List<FeeRecords> FeeRecords { get; set; }

        /// <summary>
        /// 唯一标识这次请求
        /// </summary>
        [JsonProperty("request_id")]
        [XmlElement("request_id")]
        public string RequestId { get; set; }
    }
}
