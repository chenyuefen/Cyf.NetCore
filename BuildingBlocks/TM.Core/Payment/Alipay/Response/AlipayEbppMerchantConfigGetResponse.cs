using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppMerchantConfigGetResponse.
    /// </summary>
    public class AlipayEbppMerchantConfigGetResponse : AlipayResponse
    {
        /// <summary>
        /// 商户机构配置信息
        /// </summary>
        [JsonProperty("inst_configs")]
        [XmlArray("inst_configs")]
        [XmlArrayItem("merchant_inst_config")]
        public List<MerchantInstConfig> InstConfigs { get; set; }

        /// <summary>
        /// 商户的用户ID
        /// </summary>
        [JsonProperty("merchant_user_id")]
        [XmlElement("merchant_user_id")]
        public string MerchantUserId { get; set; }
    }
}
