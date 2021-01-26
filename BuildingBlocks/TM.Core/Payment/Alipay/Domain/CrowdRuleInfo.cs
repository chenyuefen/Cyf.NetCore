using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// CrowdRuleInfo Data Structure.
    /// </summary>
    [Serializable]
    public class CrowdRuleInfo : AlipayObject
    {
        /// <summary>
        /// 规则描述
        /// </summary>
        [JsonProperty("ruledesc")]
        [XmlElement("ruledesc")]
        public string Ruledesc { get; set; }

        /// <summary>
        /// 规则id
        /// </summary>
        [JsonProperty("ruleid")]
        [XmlElement("ruleid")]
        public string Ruleid { get; set; }

        /// <summary>
        /// 圈人规则的状态
        /// </summary>
        [JsonProperty("status")]
        [XmlElement("status")]
        public string Status { get; set; }
    }
}
