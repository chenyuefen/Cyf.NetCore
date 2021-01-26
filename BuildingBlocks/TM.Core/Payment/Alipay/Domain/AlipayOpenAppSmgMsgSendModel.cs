using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppSmgMsgSendModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenAppSmgMsgSendModel : AlipayObject
    {
        /// <summary>
        /// 5555
        /// </summary>
        [JsonProperty("numberone")]
        [XmlElement("numberone")]
        public string Numberone { get; set; }

        /// <summary>
        /// 22
        /// </summary>
        [JsonProperty("numbertowe")]
        [XmlElement("numbertowe")]
        public string Numbertowe { get; set; }
    }
}
