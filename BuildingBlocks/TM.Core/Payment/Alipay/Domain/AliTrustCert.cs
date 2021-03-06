using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AliTrustCert Data Structure.
    /// </summary>
    [Serializable]
    public class AliTrustCert : AlipayObject
    {
        /// <summary>
        /// 点击信用认证图标之后的跳转链接
        /// </summary>
        [JsonProperty("forward_url")]
        [XmlElement("forward_url")]
        public string ForwardUrl { get; set; }

        /// <summary>
        /// 通过信用认证的图标链接
        /// </summary>
        [JsonProperty("icon_url")]
        [XmlElement("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 当通过信用认证时,取值为"T";否则为"F".
        /// </summary>
        [JsonProperty("is_certified")]
        [XmlElement("is_certified")]
        public string IsCertified { get; set; }

        /// <summary>
        /// 芝麻认证等级，取值为1,2,3
        /// </summary>
        [JsonProperty("level")]
        [XmlElement("level")]
        public string Level { get; set; }

        /// <summary>
        /// 当用户未通过芝麻认证时给出的原因提示
        /// </summary>
        [JsonProperty("message")]
        [XmlElement("message")]
        public string Message { get; set; }
    }
}
