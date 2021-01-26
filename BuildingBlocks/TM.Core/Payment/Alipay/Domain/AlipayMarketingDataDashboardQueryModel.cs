using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingDataDashboardQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayMarketingDataDashboardQueryModel : AlipayObject
    {
        /// <summary>
        /// 仪表盘ID
        /// </summary>
        [JsonProperty("dashboard_id")]
        [XmlElement("dashboard_id")]
        public string DashboardId { get; set; }

        /// <summary>
        /// 仪表盘过滤条件
        /// </summary>
        [JsonProperty("param")]
        [XmlArray("param")]
        [XmlArrayItem("dashboard_param")]
        public List<DashboardParam> Param { get; set; }
    }
}
