using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlmReportData Data Structure.
    /// </summary>
    [Serializable]
    public class AlmReportData : AlipayObject
    {
        /// <summary>
        /// 数据大类
        /// </summary>
        [JsonProperty("biz_type")]
        [XmlElement("biz_type")]
        public string BizType { get; set; }

        /// <summary>
        /// 期限类别
        /// </summary>
        [JsonProperty("date_type")]
        [XmlElement("date_type")]
        public string DateType { get; set; }

        /// <summary>
        /// 数据日期
        /// </summary>
        [JsonProperty("report_date")]
        [XmlElement("report_date")]
        public string ReportDate { get; set; }

        /// <summary>
        /// 报表名称
        /// </summary>
        [JsonProperty("report_name")]
        [XmlElement("report_name")]
        public string ReportName { get; set; }

        /// <summary>
        /// 报表数据，只支持整数（可为负），详细见下面描述。  金额单位：分，1万即传 1000000  百分比：乘以1万后的值。例如：50%，则提供 0.5*10000即：5000  偏离度bp：bp*1万提供。例如：30.5bp，提供值：305000
        /// </summary>
        [JsonProperty("report_value")]
        [XmlElement("report_value")]
        public long ReportValue { get; set; }

        /// <summary>
        /// 报表小类
        /// </summary>
        [JsonProperty("sub_biz_type")]
        [XmlElement("sub_biz_type")]
        public string SubBizType { get; set; }
    }
}
