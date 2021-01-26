using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayAccountExrateSentimentDataSendModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayAccountExrateSentimentDataSendModel : AlipayObject
    {
        /// <summary>
        /// 国家制裁提交数据内容
        /// </summary>
        [JsonProperty("content")]
        [XmlElement("content")]
        public string Content { get; set; }
    }
}
