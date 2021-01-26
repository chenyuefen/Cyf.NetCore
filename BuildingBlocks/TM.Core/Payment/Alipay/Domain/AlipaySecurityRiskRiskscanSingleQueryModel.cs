using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipaySecurityRiskRiskscanSingleQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipaySecurityRiskRiskscanSingleQueryModel : AlipayObject
    {
        /// <summary>
        /// 蚂蚁信息安全平台为接入方分配的业务key，必须正确
        /// </summary>
        [JsonProperty("app_key")]
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        /// <summary>
        /// 业务风险判断的具体参数，该参数是JsonString，Json中具体的key在接入信息安全平台时约定。
        /// </summary>
        [JsonProperty("event_data")]
        [XmlElement("event_data")]
        public string EventData { get; set; }

        /// <summary>
        /// 蚂蚁信息安全平台为接入方分配的业务类型，业务类型标识了具体识别的风险内容，例如：识别入职员工的风险，识别供应商的舞弊风险等。
        /// </summary>
        [JsonProperty("event_type")]
        [XmlElement("event_type")]
        public string EventType { get; set; }

        /// <summary>
        /// 蚂蚁信息安全平台为业务方分配的业务密钥，必须正确。
        /// </summary>
        [JsonProperty("secret_key")]
        [XmlElement("secret_key")]
        public string SecretKey { get; set; }
    }
}
