using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// ZolozAuthenticationCustomerFacemanageDeleteModel Data Structure.
    /// </summary>
    [Serializable]
    public class ZolozAuthenticationCustomerFacemanageDeleteModel : AlipayObject
    {
        /// <summary>
        /// 地域编码
        /// </summary>
        [JsonProperty("areacode")]
        [XmlElement("areacode")]
        public string Areacode { get; set; }

        /// <summary>
        /// 业务量
        /// </summary>
        [JsonProperty("bizscale")]
        [XmlElement("bizscale")]
        public string Bizscale { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [JsonProperty("brandcode")]
        [XmlElement("brandcode")]
        public string Brandcode { get; set; }

        /// <summary>
        /// 商户机具唯一编码，关键参数
        /// </summary>
        [JsonProperty("devicenum")]
        [XmlElement("devicenum")]
        public string Devicenum { get; set; }

        /// <summary>
        /// 拓展参数
        /// </summary>
        [JsonProperty("extinfo")]
        [XmlElement("extinfo")]
        public string Extinfo { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        [JsonProperty("facetype")]
        [XmlElement("facetype")]
        public string Facetype { get; set; }

        /// <summary>
        /// 入库用户信息
        /// </summary>
        [JsonProperty("faceval")]
        [XmlElement("faceval")]
        public string Faceval { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        [JsonProperty("group")]
        [XmlElement("group")]
        public string Group { get; set; }

        /// <summary>
        /// 门店编码
        /// </summary>
        [JsonProperty("storecode")]
        [XmlElement("storecode")]
        public string Storecode { get; set; }

        /// <summary>
        /// 有效期天数，如7天、30天、365天
        /// </summary>
        [JsonProperty("validtimes")]
        [XmlElement("validtimes")]
        public string Validtimes { get; set; }
    }
}
