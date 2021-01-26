using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// PeriodInfo Data Structure.
    /// </summary>
    [Serializable]
    public class PeriodInfo : AlipayObject
    {
        /// <summary>
        /// 单位
        /// </summary>
        [JsonProperty("dimension")]
        [XmlElement("dimension")]
        public string Dimension { get; set; }

        /// <summary>
        /// 周期值
        /// </summary>
        [JsonProperty("value")]
        [XmlElement("value")]
        public long Value { get; set; }
    }
}
