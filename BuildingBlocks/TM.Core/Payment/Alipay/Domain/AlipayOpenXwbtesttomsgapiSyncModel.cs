using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenXwbtesttomsgapiSyncModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenXwbtesttomsgapiSyncModel : AlipayObject
    {
        /// <summary>
        /// 1
        /// </summary>
        [JsonProperty("xwb")]
        [XmlElement("xwb")]
        public string Xwb { get; set; }
    }
}
