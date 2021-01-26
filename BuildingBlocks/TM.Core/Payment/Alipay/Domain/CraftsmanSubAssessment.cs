using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// CraftsmanSubAssessment Data Structure.
    /// </summary>
    [Serializable]
    public class CraftsmanSubAssessment : AlipayObject
    {
        /// <summary>
        /// 子评分
        /// </summary>
        [JsonProperty("score")]
        [XmlElement("score")]
        public long Score { get; set; }

        /// <summary>
        /// 子评分项名
        /// </summary>
        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }
    }
}
