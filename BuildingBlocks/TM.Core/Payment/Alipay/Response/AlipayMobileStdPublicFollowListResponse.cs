using Newtonsoft.Json;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMobileStdPublicFollowListResponse.
    /// </summary>
    public class AlipayMobileStdPublicFollowListResponse : AlipayResponse
    {
        /// <summary>
        /// 当前组的值
        /// </summary>
        [JsonProperty("count")]
        [XmlElement("count")]
        public string Count { get; set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        [JsonProperty("data")]
        [XmlElement("data")]
        public AlipayData Data { get; set; }

        /// <summary>
        /// 与nextUserId对应，标准Alipay UserId
        /// </summary>
        [JsonProperty("next_alipay_user_id")]
        [XmlElement("next_alipay_user_id")]
        public string NextAlipayUserId { get; set; }

        /// <summary>
        /// 查询分组的userid
        /// </summary>
        [JsonProperty("next_user_id")]
        [XmlElement("next_user_id")]
        public string NextUserId { get; set; }
    }
}
