using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// InsOption Data Structure.
    /// </summary>
    [Serializable]
    public class InsOption : AlipayObject
    {
        /// <summary>
        /// 保额key
        /// </summary>
        [JsonProperty("coverage")]
        [XmlElement("coverage")]
        public string Coverage { get; set; }

        /// <summary>
        /// 保额value
        /// </summary>
        [JsonProperty("coverage_text")]
        [XmlElement("coverage_text")]
        public string CoverageText { get; set; }
    }
}
