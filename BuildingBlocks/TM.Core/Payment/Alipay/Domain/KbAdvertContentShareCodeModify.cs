using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KbAdvertContentShareCodeModify Data Structure.
    /// </summary>
    [Serializable]
    public class KbAdvertContentShareCodeModify : AlipayObject
    {
        /// <summary>
        /// 宣传展示标题（不能超过30个字符）
        /// </summary>
        [JsonProperty("display_title")]
        [XmlElement("display_title")]
        public string DisplayTitle { get; set; }
    }
}
