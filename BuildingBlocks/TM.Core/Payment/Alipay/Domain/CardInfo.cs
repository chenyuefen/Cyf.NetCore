using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// CardInfo Data Structure.
    /// </summary>
    [Serializable]
    public class CardInfo : AlipayObject
    {
        /// <summary>
        /// 领取时间
        /// </summary>
        [JsonProperty("taken_time")]
        [XmlElement("taken_time")]
        public string TakenTime { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [JsonProperty("user_name")]
        [XmlElement("user_name")]
        public string UserName { get; set; }
    }
}
