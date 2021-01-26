using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingDataMemberReportQueryResponse.
    /// </summary>
    public class KoubeiMarketingDataMemberReportQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询成功时返回json格式数据
        /// </summary>
        [JsonProperty("report_data")]
        [XmlElement("report_data")]
        public string ReportData { get; set; }
    }
}
