using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicSettingCategoryQueryResponse.
    /// </summary>
    public class AlipayOpenPublicSettingCategoryQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回已设置的一级行业分类名称
        /// </summary>
        [JsonProperty("primary_category")]
        [XmlElement("primary_category")]
        public string PrimaryCategory { get; set; }

        /// <summary>
        /// 返回已设置的二级行业分类名称
        /// </summary>
        [JsonProperty("secondary_category")]
        [XmlElement("secondary_category")]
        public string SecondaryCategory { get; set; }
    }
}
