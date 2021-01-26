using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignIntelligentPromoBatchqueryResponse.
    /// </summary>
    public class KoubeiMarketingCampaignIntelligentPromoBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询返回的营销活动列表信息
        /// </summary>
        [JsonProperty("intelligent_promos")]
        [XmlArray("intelligent_promos")]
        [XmlArrayItem("intelligent_promo")]
        public List<IntelligentPromo> IntelligentPromos { get; set; }

        /// <summary>
        /// 查询后返回的分页信息
        /// </summary>
        [JsonProperty("page_result")]
        [XmlElement("page_result")]
        public PromoPageResult PageResult { get; set; }
    }
}
