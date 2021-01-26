using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiAdvertCommissionBillQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiAdvertCommissionBillQueryModel : AlipayObject
    {
        /// <summary>
        /// 账期(格式为yyyyMM)
        /// </summary>
        [JsonProperty("date")]
        [XmlElement("date")]
        public string Date { get; set; }

        /// <summary>
        /// 账单类型  deal-交易账单  settle-结算账单
        /// </summary>
        [JsonProperty("type")]
        [XmlElement("type")]
        public string Type { get; set; }
    }
}
