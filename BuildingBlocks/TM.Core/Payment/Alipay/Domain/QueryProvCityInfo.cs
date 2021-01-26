using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// QueryProvCityInfo Data Structure.
    /// </summary>
    [Serializable]
    public class QueryProvCityInfo : AlipayObject
    {
        /// <summary>
        /// 省市编号
        /// </summary>
        [JsonProperty("adcode")]
        [XmlElement("adcode")]
        public string Adcode { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        [JsonProperty("name")]
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
