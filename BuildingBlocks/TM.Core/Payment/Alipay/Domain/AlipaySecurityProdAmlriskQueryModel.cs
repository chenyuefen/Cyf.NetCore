using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipaySecurityProdAmlriskQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipaySecurityProdAmlriskQueryModel : AlipayObject
    {
        /// <summary>
        /// 办公地址
        /// </summary>
        [JsonProperty("business_address")]
        [XmlElement("business_address")]
        public string BusinessAddress { get; set; }

        /// <summary>
        /// 标识该次反洗钱风险分析事件请求的id，商户应保证此id唯一。
        /// </summary>
        [JsonProperty("event_id")]
        [XmlElement("event_id")]
        public string EventId { get; set; }

        /// <summary>
        /// 与商户相关个体的信息列表，可以有0个到多个。
        /// </summary>
        [JsonProperty("individual_list")]
        [XmlArray("individual_list")]
        [XmlArrayItem("individual_info")]
        public List<IndividualInfo> IndividualList { get; set; }

        /// <summary>
        /// 公司名称、类型、性质
        /// </summary>
        [JsonProperty("legal_name")]
        [XmlElement("legal_name")]
        public string LegalName { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        [JsonProperty("merchant_id")]
        [XmlElement("merchant_id")]
        public string MerchantId { get; set; }

        /// <summary>
        /// 该商户准入申请的id
        /// </summary>
        [JsonProperty("order_id")]
        [XmlElement("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// 公司注册地址
        /// </summary>
        [JsonProperty("registered_address")]
        [XmlElement("registered_address")]
        public string RegisteredAddress { get; set; }

        /// <summary>
        /// 公司注册号
        /// </summary>
        [JsonProperty("registration_number")]
        [XmlElement("registration_number")]
        public string RegistrationNumber { get; set; }
    }
}
