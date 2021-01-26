using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingCardBenefitQueryResponse.
    /// </summary>
    public class AlipayMarketingCardBenefitQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 会员卡模板外部权益列表
        /// </summary>
        [JsonProperty("mcard_template_benefit_query")]
        [XmlArray("mcard_template_benefit_query")]
        [XmlArrayItem("mcard_template_benefit_query")]
        public List<McardTemplateBenefitQuery> McardTemplateBenefitQuery { get; set; }
    }
}
