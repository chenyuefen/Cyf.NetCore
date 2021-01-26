using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayInsMarketingDiscountPreuseModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayInsMarketingDiscountPreuseModel : AlipayObject
    {
        /// <summary>
        /// 保险营销账号Id
        /// </summary>
        [JsonProperty("account_id")]
        [XmlElement("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// 保险营销账号类型
        /// </summary>
        [JsonProperty("account_type")]
        [XmlElement("account_type")]
        public long AccountType { get; set; }

        /// <summary>
        /// 保险营销业务类型
        /// </summary>
        [JsonProperty("business_type")]
        [XmlElement("business_type")]
        public long BusinessType { get; set; }

        /// <summary>
        /// 优惠决策因子
        /// </summary>
        [JsonProperty("factors")]
        [XmlArray("factors")]
        [XmlArrayItem("ins_mkt_factor_d_t_o")]
        public List<InsMktFactorDTO> Factors { get; set; }

        /// <summary>
        /// 营销市场列表
        /// </summary>
        [JsonProperty("market_types")]
        [XmlArray("market_types")]
        [XmlArrayItem("number")]
        public List<long> MarketTypes { get; set; }

        /// <summary>
        /// 权益活动列表
        /// </summary>
        [JsonProperty("mkt_coupon_campaigns")]
        [XmlArray("mkt_coupon_campaigns")]
        [XmlArrayItem("ins_mkt_coupon_cmpgn_base_d_t_o")]
        public List<InsMktCouponCmpgnBaseDTO> MktCouponCampaigns { get; set; }

        /// <summary>
        /// 用户选择的权益列表
        /// </summary>
        [JsonProperty("mkt_coupons")]
        [XmlArray("mkt_coupons")]
        [XmlArrayItem("ins_mkt_coupon_base_d_t_o")]
        public List<InsMktCouponBaseDTO> MktCoupons { get; set; }

        /// <summary>
        /// 营销标的列表
        /// </summary>
        [JsonProperty("mkt_objects")]
        [XmlArray("mkt_objects")]
        [XmlArrayItem("ins_mkt_object_d_t_o")]
        public List<InsMktObjectDTO> MktObjects { get; set; }

        /// <summary>
        /// 请求流水id
        /// </summary>
        [JsonProperty("request_id")]
        [XmlElement("request_id")]
        public string RequestId { get; set; }
    }
}
