using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// InstallmentValue Data Structure.
    /// </summary>
    [Serializable]
    public class InstallmentValue : AlipayObject
    {
        /// <summary>
        /// 分段值
        /// </summary>
        [JsonProperty("installment_values")]
        [XmlArray("installment_values")]
        [XmlArrayItem("installment_meta_info")]
        public List<InstallmentMetaInfo> InstallmentValues { get; set; }
    }
}
