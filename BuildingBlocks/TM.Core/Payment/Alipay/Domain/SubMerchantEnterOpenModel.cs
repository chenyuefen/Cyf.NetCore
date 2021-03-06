using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// SubMerchantEnterOpenModel Data Structure.
    /// </summary>
    [Serializable]
    public class SubMerchantEnterOpenModel : AlipayObject
    {
        /// <summary>
        /// 商户门店税号。
        /// </summary>
        [JsonProperty("register_no")]
        [XmlElement("register_no")]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 商户门店全称。
        /// </summary>
        [JsonProperty("sub_m_name")]
        [XmlElement("sub_m_name")]
        public string SubMName { get; set; }

        /// <summary>
        /// 商户门店简称，只能由大写字母、下划线、数字组成，且必须以大写字母开头。
        /// </summary>
        [JsonProperty("sub_m_short_name")]
        [XmlElement("sub_m_short_name")]
        public string SubMShortName { get; set; }
    }
}
