using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceTransportOfflinepayRecordVerifyModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayCommerceTransportOfflinepayRecordVerifyModel : AlipayObject
    {
        /// <summary>
        /// 原始脱机记录信息
        /// </summary>
        [JsonProperty("record")]
        [XmlElement("record")]
        public string Record { get; set; }
    }
}
