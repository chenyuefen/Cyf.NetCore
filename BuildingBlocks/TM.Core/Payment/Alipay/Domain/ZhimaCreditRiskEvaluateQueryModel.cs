using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// ZhimaCreditRiskEvaluateQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class ZhimaCreditRiskEvaluateQueryModel : AlipayObject
    {
        /// <summary>
        /// 扩展字段，用来标识用户的其他描述信息，是一个JSON串
        /// </summary>
        [JsonProperty("extend_info")]
        [XmlElement("extend_info")]
        public string ExtendInfo { get; set; }

        /// <summary>
        /// 产品码[直接传示例值]
        /// </summary>
        [JsonProperty("product_code")]
        [XmlElement("product_code")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 场景码，用来标识一套特定的风控策略
        /// </summary>
        [JsonProperty("scene_code")]
        [XmlElement("scene_code")]
        public string SceneCode { get; set; }

        /// <summary>
        /// 商户请求的唯一标志，长度64位以内字符串，仅限字母数字下划线组合。该标识作为业务调用的唯一标识，商户要保证其业务唯一性，使用相同transaction_id的查询，芝麻在一段时间内（一般为1天）返回首次查询结果，超过有效期的查询即为无效并返回异常，有效期内的重复查询不重新计费
        /// </summary>
        [JsonProperty("transaction_id")]
        [XmlElement("transaction_id")]
        public string TransactionId { get; set; }
    }
}