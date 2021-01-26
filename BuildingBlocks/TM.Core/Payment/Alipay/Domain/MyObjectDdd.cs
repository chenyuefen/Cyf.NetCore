using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// MyObjectDdd Data Structure.
    /// </summary>
    [Serializable]
    public class MyObjectDdd : AlipayObject
    {
        /// <summary>
        /// xxx
        /// </summary>
        [JsonProperty("param")]
        [XmlElement("param")]
        public string Param { get; set; }
    }
}
