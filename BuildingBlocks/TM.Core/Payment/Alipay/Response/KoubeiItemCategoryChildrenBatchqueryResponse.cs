using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiItemCategoryChildrenBatchqueryResponse.
    /// </summary>
    public class KoubeiItemCategoryChildrenBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 口碑标准后台类目信息列表
        /// </summary>
        [JsonProperty("category_list")]
        [XmlArray("category_list")]
        [XmlArrayItem("standard_category_info")]
        public List<StandardCategoryInfo> CategoryList { get; set; }
    }
}
