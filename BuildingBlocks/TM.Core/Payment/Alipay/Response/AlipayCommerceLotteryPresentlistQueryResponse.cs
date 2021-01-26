using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceLotteryPresentlistQueryResponse.
    /// </summary>
    public class AlipayCommerceLotteryPresentlistQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 列表内容
        /// </summary>
        [JsonProperty("results")]
        [XmlArray("results")]
        [XmlArrayItem("lottery_present")]
        public List<LotteryPresent> Results { get; set; }

        /// <summary>
        /// 返回的列表的大小
        /// </summary>
        [JsonProperty("total_result")]
        [XmlElement("total_result")]
        public long TotalResult { get; set; }
    }
}
