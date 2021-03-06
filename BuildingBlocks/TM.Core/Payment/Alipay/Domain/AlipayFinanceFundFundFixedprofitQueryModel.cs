using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayFinanceFundFundFixedprofitQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayFinanceFundFundFixedprofitQueryModel : AlipayObject
    {
        /// <summary>
        /// 基金代码
        /// </summary>
        [JsonProperty("fund_code")]
        [XmlElement("fund_code")]
        public string FundCode { get; set; }
    }
}
