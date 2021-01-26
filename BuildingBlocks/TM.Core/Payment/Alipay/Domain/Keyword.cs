using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// Keyword Data Structure.
    /// </summary>
    [Serializable]
    public class Keyword : AlipayObject
    {
        /// <summary>
        /// 当前文字颜色
        /// </summary>
        [JsonProperty("color")]
        [XmlElement("color")]
        public string Color { get; set; }

        /// <summary>
        /// 模板中占位符的值
        /// </summary>
        [JsonProperty("value")]
        [XmlElement("value")]
        public string Value { get; set; }
    }
}
