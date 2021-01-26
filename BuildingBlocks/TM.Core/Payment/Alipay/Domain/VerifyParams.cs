using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// VerifyParams Data Structure.
    /// </summary>
    [Serializable]
    public class VerifyParams : AlipayObject
    {
        /// <summary>
        /// 用户证件号后4位
        /// </summary>
        [JsonProperty("cert_no")]
        [XmlElement("cert_no")]
        public string CertNo { get; set; }
    }
}
