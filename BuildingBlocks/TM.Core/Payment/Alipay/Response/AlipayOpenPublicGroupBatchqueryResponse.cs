using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicGroupBatchqueryResponse.
    /// </summary>
    public class AlipayOpenPublicGroupBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 用户分组列表，包含每个分组的id、name、以及规则模型
        /// </summary>
        [JsonProperty("groups")]
        [XmlArray("groups")]
        [XmlArrayItem("query_group")]
        public List<QueryGroup> Groups { get; set; }
    }
}
