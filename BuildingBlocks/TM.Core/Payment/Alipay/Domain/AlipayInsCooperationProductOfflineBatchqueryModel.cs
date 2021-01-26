using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayInsCooperationProductOfflineBatchqueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayInsCooperationProductOfflineBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 机构在蚂蚁平台上的惟一标识
        /// </summary>
        [JsonProperty("inst_id")]
        [XmlElement("inst_id")]
        public string InstId { get; set; }
    }
}
