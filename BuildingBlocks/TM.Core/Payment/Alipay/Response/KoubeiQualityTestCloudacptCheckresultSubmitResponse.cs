using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiQualityTestCloudacptCheckresultSubmitResponse.
    /// </summary>
    public class KoubeiQualityTestCloudacptCheckresultSubmitResponse : AlipayResponse
    {
        /// <summary>
        /// 活动id
        /// </summary>
        [JsonProperty("activity_id")]
        [XmlElement("activity_id")]
        public string ActivityId { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [JsonProperty("batch_id")]
        [XmlElement("batch_id")]
        public string BatchId { get; set; }
    }
}
