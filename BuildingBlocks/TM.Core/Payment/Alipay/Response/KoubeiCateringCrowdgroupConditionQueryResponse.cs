using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringCrowdgroupConditionQueryResponse.
    /// </summary>
    public class KoubeiCateringCrowdgroupConditionQueryResponse : AlipayResponse
    {
        /// <summary>
        /// isv创建的用户规则分组描述
        /// </summary>
        [JsonProperty("condition_list")]
        [XmlArray("condition_list")]
        [XmlArrayItem("user_crowd_conditions")]
        public List<UserCrowdConditions> ConditionList { get; set; }
    }
}
