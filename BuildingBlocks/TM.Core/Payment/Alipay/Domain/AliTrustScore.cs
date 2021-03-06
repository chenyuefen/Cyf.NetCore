using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AliTrustScore Data Structure.
    /// </summary>
    [Serializable]
    public class AliTrustScore : AlipayObject
    {
        /// <summary>
        /// 芝麻分
        /// </summary>
        [JsonProperty("score")]
        [XmlElement("score")]
        public long Score { get; set; }
    }
}
