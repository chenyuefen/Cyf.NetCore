using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayBossCsDatacollectSendModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayBossCsDatacollectSendModel : AlipayObject
    {
        /// <summary>
        /// 上数提交数据内容
        /// </summary>
        [JsonProperty("content")]
        [XmlElement("content")]
        public string Content { get; set; }
    }
}
