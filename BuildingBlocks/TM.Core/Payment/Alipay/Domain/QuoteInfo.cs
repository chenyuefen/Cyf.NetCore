using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// QuoteInfo Data Structure.
    /// </summary>
    [Serializable]
    public class QuoteInfo : AlipayObject
    {
        /// <summary>
        /// 238810000000049704774
        /// </summary>
        [JsonProperty("company_id")]
        [XmlElement("company_id")]
        public string CompanyId { get; set; }

        /// <summary>
        /// 报价ID
        /// </summary>
        [JsonProperty("quote_biz_id")]
        [XmlElement("quote_biz_id")]
        public string QuoteBizId { get; set; }
    }
}
