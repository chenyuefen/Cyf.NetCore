using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// SsdataDataserviceMetainfoSyncModel Data Structure.
    /// </summary>
    [Serializable]
    public class SsdataDataserviceMetainfoSyncModel : AlipayObject
    {
        /// <summary>
        /// 元数据信息
        /// </summary>
        [JsonProperty("meta_info")]
        [XmlElement("meta_info")]
        public string MetaInfo { get; set; }
    }
}
