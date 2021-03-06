using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoCplifeNoticePublishResponse.
    /// </summary>
    public class AlipayEcoCplifeNoticePublishResponse : AlipayResponse
    {
        /// <summary>
        /// 支付宝平台统一生产的通知公告唯一ID标示.
        /// </summary>
        [JsonProperty("notice_id")]
        [XmlElement("notice_id")]
        public string NoticeId { get; set; }
    }
}
