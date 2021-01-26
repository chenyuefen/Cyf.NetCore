using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// SceneProdDataQueryResultDetail Data Structure.
    /// </summary>
    [Serializable]
    public class SceneProdDataQueryResultDetail : AlipayObject
    {
        /// <summary>
        /// 网商银行申请单号
        /// </summary>
        [JsonProperty("app_seqno")]
        [XmlElement("app_seqno")]
        public string AppSeqno { get; set; }

        /// <summary>
        /// 机构需要查询的订单数据，
        /// </summary>
        [JsonProperty("result")]
        [XmlElement("result")]
        public string Result { get; set; }
    }
}
