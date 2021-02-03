using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiRetailWmsProducerBatchqueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiRetailWmsProducerBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 最多查询100个
        /// </summary>
        [JsonProperty("producer_ids")]
        [XmlArray("producer_ids")]
        [XmlArrayItem("string")]
        public List<string> ProducerIds { get; set; }
    }
}