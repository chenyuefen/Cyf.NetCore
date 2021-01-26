using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// InsMktCouponDTO Data Structure.
    /// </summary>
    [Serializable]
    public class InsMktCouponDTO : AlipayObject
    {
        /// <summary>
        /// 权益资产Id，如券Id
        /// </summary>
        [JsonProperty("asset_id")]
        [XmlElement("asset_id")]
        public string AssetId { get; set; }

        /// <summary>
        /// 权益Id
        /// </summary>
        [JsonProperty("coupon_id")]
        [XmlElement("coupon_id")]
        public string CouponId { get; set; }

        /// <summary>
        /// 权益类型
        /// </summary>
        [JsonProperty("coupon_type")]
        [XmlElement("coupon_type")]
        public string CouponType { get; set; }

        /// <summary>
        /// 500元单品券
        /// </summary>
        [JsonProperty("coupon_value")]
        [XmlElement("coupon_value")]
        public string CouponValue { get; set; }

        /// <summary>
        /// 是否推荐使用该优惠
        /// </summary>
        [JsonProperty("recommend")]
        [XmlElement("recommend")]
        public bool Recommend { get; set; }

        /// <summary>
        /// 核销结束时间
        /// </summary>
        [JsonProperty("use_end_time")]
        [XmlElement("use_end_time")]
        public string UseEndTime { get; set; }

        /// <summary>
        /// 核销规则
        /// </summary>
        [JsonProperty("use_rule")]
        [XmlElement("use_rule")]
        public string UseRule { get; set; }

        /// <summary>
        /// 核销开始时间
        /// </summary>
        [JsonProperty("use_start_time")]
        [XmlElement("use_start_time")]
        public string UseStartTime { get; set; }
    }
}
