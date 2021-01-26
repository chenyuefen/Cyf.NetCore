using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiSalesKbassetStuffOrdersresultSyncModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiSalesKbassetStuffOrdersresultSyncModel : AlipayObject
    {
        /// <summary>
        /// 物料单据反馈列表，最大200条
        /// </summary>
        [JsonProperty("orders_feedback")]
        [XmlArray("orders_feedback")]
        [XmlArrayItem("access_orders_feed_back")]
        public List<AccessOrdersFeedBack> OrdersFeedback { get; set; }
    }
}
