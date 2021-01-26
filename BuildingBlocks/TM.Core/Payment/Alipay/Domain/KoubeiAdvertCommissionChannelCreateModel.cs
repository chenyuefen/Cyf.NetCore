using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiAdvertCommissionChannelCreateModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiAdvertCommissionChannelCreateModel : AlipayObject
    {
        /// <summary>
        /// 新增渠道列表
        /// </summary>
        [JsonProperty("channels")]
        [XmlArray("channels")]
        [XmlArrayItem("kb_advert_add_channel_request")]
        public List<KbAdvertAddChannelRequest> Channels { get; set; }
    }
}
