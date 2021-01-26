using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// InteligentBudgetInfo Data Structure.
    /// </summary>
    [Serializable]
    public class InteligentBudgetInfo : AlipayObject
    {
        /// <summary>
        /// 预算数量
        /// </summary>
        [JsonProperty("budget_total")]
        [XmlElement("budget_total")]
        public string BudgetTotal { get; set; }

        /// <summary>
        /// 预算类型，枚举（QUANTITY：数量预算）
        /// </summary>
        [JsonProperty("budget_type")]
        [XmlElement("budget_type")]
        public string BudgetType { get; set; }
    }
}
