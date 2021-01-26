using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCraftsmanDataProviderCreateResponse.
    /// </summary>
    public class KoubeiCraftsmanDataProviderCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 手艺人id
        /// </summary>
        [JsonProperty("craftsman_id")]
        [XmlElement("craftsman_id")]
        public string CraftsmanId { get; set; }
    }
}
