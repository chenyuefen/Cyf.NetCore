using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// QueryInstBillDetail Data Structure.
    /// </summary>
    [Serializable]
    public class QueryInstBillDetail : AlipayObject
    {
        /// <summary>
        /// 明细key
        /// </summary>
        [JsonProperty("key")]
        [XmlElement("key")]
        public string Key { get; set; }

        /// <summary>
        /// 明细描述
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 明细对应值
        /// </summary>
        [JsonProperty("value")]
        [XmlElement("value")]
        public string Value { get; set; }
    }
}
