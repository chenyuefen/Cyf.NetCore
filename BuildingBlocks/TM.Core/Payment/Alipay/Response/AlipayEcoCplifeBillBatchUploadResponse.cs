using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoCplifeBillBatchUploadResponse.
    /// </summary>
    public class AlipayEcoCplifeBillBatchUploadResponse : AlipayResponse
    {
        /// <summary>
        /// 成功导入所有条目集合中的条目，则回传请求中的批次号
        /// </summary>
        [JsonProperty("batch_id")]
        [XmlElement("batch_id")]
        public string BatchId { get; set; }
    }
}
