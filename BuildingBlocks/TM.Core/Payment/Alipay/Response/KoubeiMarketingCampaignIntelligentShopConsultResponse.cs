using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignIntelligentShopConsultResponse.
    /// </summary>
    public class KoubeiMarketingCampaignIntelligentShopConsultResponse : AlipayResponse
    {
        /// <summary>
        /// 总共项数
        /// </summary>
        [JsonProperty("items")]
        [XmlElement("items")]
        public string Items { get; set; }

        /// <summary>
        /// 智能营销方案符合标准的门店列表
        /// </summary>
        [JsonProperty("shops")]
        [XmlArray("shops")]
        [XmlArrayItem("intelligent_promo_shop_summary_info")]
        public List<IntelligentPromoShopSummaryInfo> Shops { get; set; }
    }
}
