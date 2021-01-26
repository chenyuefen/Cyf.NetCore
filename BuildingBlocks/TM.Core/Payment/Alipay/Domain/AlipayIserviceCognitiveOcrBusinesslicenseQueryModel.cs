using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayIserviceCognitiveOcrBusinesslicenseQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayIserviceCognitiveOcrBusinesslicenseQueryModel : AlipayObject
    {
        /// <summary>
        /// 营业执照图片base64加密后内容
        /// </summary>
        [JsonProperty("image_content")]
        [XmlElement("image_content")]
        public string ImageContent { get; set; }
    }
}
