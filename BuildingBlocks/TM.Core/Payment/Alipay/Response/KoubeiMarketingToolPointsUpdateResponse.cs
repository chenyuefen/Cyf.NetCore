using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingToolPointsUpdateResponse.
    /// </summary>
    public class KoubeiMarketingToolPointsUpdateResponse : AlipayResponse
    {
        /// <summary>
        /// 集点变更流水号
        /// </summary>
        [JsonProperty("point_log_no")]
        [XmlElement("point_log_no")]
        public string PointLogNo { get; set; }
    }
}
