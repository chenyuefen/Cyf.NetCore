using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceTransportOfflinepayTradeSettleModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayCommerceTransportOfflinepayTradeSettleModel : AlipayObject
    {
        /// <summary>
        /// 脱机交易列表
        /// </summary>
        [JsonProperty("trade_list")]
        [XmlArray("trade_list")]
        [XmlArrayItem("alipay_offline_trade")]
        public List<AlipayOfflineTrade> TradeList { get; set; }
    }
}
