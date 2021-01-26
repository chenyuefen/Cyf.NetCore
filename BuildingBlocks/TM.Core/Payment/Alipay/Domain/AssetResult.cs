using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AssetResult Data Structure.
    /// </summary>
    [Serializable]
    public class AssetResult : AlipayObject
    {
        /// <summary>
        /// 订单明细ID
        /// </summary>
        [JsonProperty("assign_item_id")]
        [XmlElement("assign_item_id")]
        public string AssignItemId { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("error_code")]
        [XmlElement("error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        [JsonProperty("error_desc")]
        [XmlElement("error_desc")]
        public string ErrorDesc { get; set; }

        /// <summary>
        /// 是否处理成功
        /// </summary>
        [JsonProperty("success")]
        [XmlElement("success")]
        public bool Success { get; set; }
    }
}
