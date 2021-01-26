using Newtonsoft.Json;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringTablelistQueryResponse.
    /// </summary>
    public class KoubeiCateringTablelistQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回tablelistresult列表
        /// </summary>
        [JsonProperty("tableinfo_result")]
        [XmlElement("tableinfo_result")]
        public TableInfoResult TableinfoResult { get; set; }
    }
}
