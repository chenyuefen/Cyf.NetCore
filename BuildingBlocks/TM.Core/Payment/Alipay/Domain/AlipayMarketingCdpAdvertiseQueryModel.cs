using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingCdpAdvertiseQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayMarketingCdpAdvertiseQueryModel : AlipayObject
    {
        /// <summary>
        /// 广告Id，唯一标识一条广告
        /// </summary>
        [JsonProperty("ad_id")]
        [XmlElement("ad_id")]
        public string AdId { get; set; }
    }
}
