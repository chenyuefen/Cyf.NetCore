using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayMarketingDataDashboardCancelModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayMarketingDataDashboardCancelModel : AlipayObject
    {
        /// <summary>
        /// 批量取消仪表盘授权
        /// </summary>
        [JsonProperty("dashboard_ids")]
        [XmlArray("dashboard_ids")]
        [XmlArrayItem("string")]
        public List<string> DashboardIds { get; set; }
    }
}
