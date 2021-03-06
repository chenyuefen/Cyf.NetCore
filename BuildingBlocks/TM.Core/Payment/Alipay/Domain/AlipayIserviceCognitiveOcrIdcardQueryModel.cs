using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayIserviceCognitiveOcrIdcardQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayIserviceCognitiveOcrIdcardQueryModel : AlipayObject
    {
        /// <summary>
        /// 身份证图片base64编码内容
        /// </summary>
        [JsonProperty("image_content")]
        [XmlElement("image_content")]
        public string ImageContent { get; set; }
    }
}
