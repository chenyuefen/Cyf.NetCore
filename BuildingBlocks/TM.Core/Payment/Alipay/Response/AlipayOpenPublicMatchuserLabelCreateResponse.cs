using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicMatchuserLabelCreateResponse.
    /// </summary>
    public class AlipayOpenPublicMatchuserLabelCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 用户打标失败数量
        /// </summary>
        [JsonProperty("error_count")]
        [XmlElement("error_count")]
        public long ErrorCount { get; set; }

        /// <summary>
        /// 出错的匹配器列表
        /// </summary>
        [JsonProperty("error_matchers")]
        [XmlArray("error_matchers")]
        [XmlArrayItem("error_matcher")]
        public List<ErrorMatcher> ErrorMatchers { get; set; }
    }
}
