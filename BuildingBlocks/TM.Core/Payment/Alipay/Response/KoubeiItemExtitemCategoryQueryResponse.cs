using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiItemExtitemCategoryQueryResponse.
    /// </summary>
    public class KoubeiItemExtitemCategoryQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 品类信息列表
        /// </summary>
        [JsonProperty("category_list")]
        [XmlArray("category_list")]
        [XmlArrayItem("ext_category")]
        public List<ExtCategory> CategoryList { get; set; }
    }
}
