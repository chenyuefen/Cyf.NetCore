using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppYufalingsanyaowubYufalingsanyaowubQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenAppYufalingsanyaowubYufalingsanyaowubQueryModel : AlipayObject
    {
        /// <summary>
        /// yufaa
        /// </summary>
        [JsonProperty("yufaa")]
        [XmlElement("yufaa")]
        public string Yufaa { get; set; }
    }
}
