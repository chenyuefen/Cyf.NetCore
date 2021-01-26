using Newtonsoft.Json;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingToolFengdieSitesSyncResponse.
    /// </summary>
    public class AlipayMarketingToolFengdieSitesSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 返回站点升级是否成功
        /// </summary>
        [JsonProperty("data")]
        [XmlElement("data")]
        public FengdieSuccessRespModel Data { get; set; }
    }
}
