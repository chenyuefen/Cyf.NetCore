using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// PrizeCustomMenu Data Structure.
    /// </summary>
    [Serializable]
    public class PrizeCustomMenu : AlipayObject
    {
        /// <summary>
        /// 菜单详情
        /// </summary>
        [JsonProperty("text")]
        [XmlElement("text")]
        public string Text { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }
    }
}
