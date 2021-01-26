using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// InsClaimPolicy Data Structure.
    /// </summary>
    [Serializable]
    public class InsClaimPolicy : AlipayObject
    {
        /// <summary>
        /// 保单号
        /// </summary>
        [JsonProperty("policy_no")]
        [XmlElement("policy_no")]
        public string PolicyNo { get; set; }
    }
}
