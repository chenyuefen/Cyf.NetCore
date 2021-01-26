using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiRetailShopitemBatchqueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiRetailShopitemBatchqueryModel : AlipayObject
    {
        /// <summary>
        /// 查询店铺商品查询入参
        /// </summary>
        [JsonProperty("shop_items")]
        [XmlArray("shop_items")]
        [XmlArrayItem("request_ext_shop_item_query")]
        public List<RequestExtShopItemQuery> ShopItems { get; set; }
    }
}
