using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// InsMktCouponCampaignDTO Data Structure.
    /// </summary>
    [Serializable]
    public class InsMktCouponCampaignDTO : AlipayObject
    {
        /// <summary>
        /// 活动核销截止时间
        /// </summary>
        [JsonProperty("campaign_end_time")]
        [XmlElement("campaign_end_time")]
        public string CampaignEndTime { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        [JsonProperty("campaign_id")]
        [XmlElement("campaign_id")]
        public string CampaignId { get; set; }

        /// <summary>
        /// 活动备注
        /// </summary>
        [JsonProperty("campaign_memo")]
        [XmlElement("campaign_memo")]
        public string CampaignMemo { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        [JsonProperty("campaign_name")]
        [XmlElement("campaign_name")]
        public string CampaignName { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        [JsonProperty("campaign_start_time")]
        [XmlElement("campaign_start_time")]
        public string CampaignStartTime { get; set; }

        /// <summary>
        /// 活动的权益类型描述
        /// </summary>
        [JsonProperty("coupon_type")]
        [XmlElement("coupon_type")]
        public string CouponType { get; set; }

        /// <summary>
        /// 权益盖帽值，如1000元优惠券
        /// </summary>
        [JsonProperty("coupon_upper_value")]
        [XmlElement("coupon_upper_value")]
        public string CouponUpperValue { get; set; }

        /// <summary>
        /// 权益值，如500元优惠券
        /// </summary>
        [JsonProperty("coupon_value")]
        [XmlElement("coupon_value")]
        public string CouponValue { get; set; }
    }
}
