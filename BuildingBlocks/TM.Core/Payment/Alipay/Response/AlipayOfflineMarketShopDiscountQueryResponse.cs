using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOfflineMarketShopDiscountQueryResponse.
    /// </summary>
    public class AlipayOfflineMarketShopDiscountQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回商户正在进行中的活动摘要列表数据，注意：如果入参query_type不传递或者传递错误值，该参数将不会返回。
        /// </summary>
        [JsonProperty("camp_list")]
        [XmlArray("camp_list")]
        [XmlArrayItem("camp_detail_info")]
        public List<CampDetailInfo> CampList { get; set; }

        /// <summary>
        /// 返回商户正在进行中的活动记录数，注意：如果入参query_type不传递或者传递错误值，该参数将不会返回。
        /// </summary>
        [JsonProperty("camp_num")]
        [XmlElement("camp_num")]
        public long CampNum { get; set; }

        /// <summary>
        /// 优惠信息列表，最大20条，按生效时间排序，注意：如果入参query_type传递了指定的值，该参数将不会返回。只有当query_type不传递时方可返回数据
        /// </summary>
        [JsonProperty("discount_list")]
        [XmlArray("discount_list")]
        [XmlArrayItem("shop_discount_info")]
        public List<ShopDiscountInfo> DiscountList { get; set; }

        /// <summary>
        /// 商品列表，最大20条，按申领数量排序，注意：如果入参query_type传递了指定的值，该参数将不会返回。只有当query_type不传递时方可返回数据
        /// </summary>
        [JsonProperty("item_list")]
        [XmlArray("item_list")]
        [XmlArrayItem("shop_discount_info")]
        public List<ShopDiscountInfo> ItemList { get; set; }
    }
}
