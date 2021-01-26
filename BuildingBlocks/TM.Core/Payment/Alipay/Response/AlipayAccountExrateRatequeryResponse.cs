using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayAccountExrateRatequeryResponse.
    /// </summary>
    public class AlipayAccountExrateRatequeryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询到的汇率对象列表，如果没有查询到则返回空列表
        /// </summary>
        [JsonProperty("rate_query_response_list")]
        [XmlArray("rate_query_response_list")]
        [XmlArrayItem("ex_ref_rate_info_v_o")]
        public List<ExRefRateInfoVO> RateQueryResponseList { get; set; }
    }
}
