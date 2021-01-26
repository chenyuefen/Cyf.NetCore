using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOfflinePayMasterKey Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOfflinePayMasterKey : AlipayObject
    {
        /// <summary>
        /// 秘钥id
        /// </summary>
        [JsonProperty("key_id")]
        [XmlElement("key_id")]
        public long KeyId { get; set; }

        /// <summary>
        /// 支付宝脱机服务公钥
        /// </summary>
        [JsonProperty("public_key")]
        [XmlElement("public_key")]
        public string PublicKey { get; set; }
    }
}
