using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiAdvertCommissionAdvchannelUnbindModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiAdvertCommissionAdvchannelUnbindModel : AlipayObject
    {
        /// <summary>
        /// 广告ID
        /// </summary>
        [JsonProperty("adv_id")]
        [XmlElement("adv_id")]
        public string AdvId { get; set; }

        /// <summary>
        /// 渠道ID列表
        /// </summary>
        [JsonProperty("channel_id_list")]
        [XmlArray("channel_id_list")]
        [XmlArrayItem("string")]
        public List<string> ChannelIdList { get; set; }
    }
}
