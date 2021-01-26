using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOfflineMarketingVoucherCodeUploadResponse.
    /// </summary>
    public class AlipayOfflineMarketingVoucherCodeUploadResponse : AlipayResponse
    {
        /// <summary>
        /// 码库id
        /// </summary>
        [JsonProperty("code_inventory_id")]
        [XmlElement("code_inventory_id")]
        public string CodeInventoryId { get; set; }
    }
}
