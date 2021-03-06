using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayIserviceCognitiveOcrTablesQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayIserviceCognitiveOcrTablesQueryModel : AlipayObject
    {
        /// <summary>
        /// 图片base64后内容，大小控制在1M以内
        /// </summary>
        [JsonProperty("image_content")]
        [XmlElement("image_content")]
        public string ImageContent { get; set; }
    }
}
