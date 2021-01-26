using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiItemDescription Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiItemDescription : AlipayObject
    {
        /// <summary>
        /// 标题下的描述列表，列表类型，每项不得为空,最多10项，总长度不能超过2600个中文字符
        /// </summary>
        [JsonProperty("details")]
        [XmlArray("details")]
        [XmlArrayItem("string")]
        public List<string> Details { get; set; }

        /// <summary>
        /// 描述标题，不得超过40个中文字符
        /// </summary>
        [JsonProperty("title")]
        [XmlElement("title")]
        public string Title { get; set; }
    }
}
