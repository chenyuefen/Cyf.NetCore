using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMobilePublicMenuUserQueryResponse.
    /// </summary>
    public class AlipayMobilePublicMenuUserQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 结果码
        /// </summary>
        [JsonProperty("code")]
        [XmlElement("code")]
        public new string Code { get; set; }

        /// <summary>
        /// 菜单唯一标识
        /// </summary>
        [JsonProperty("menu_key")]
        [XmlElement("menu_key")]
        public string MenuKey { get; set; }

        /// <summary>
        /// 结果码描述
        /// </summary>
        [JsonProperty("msg")]
        [XmlElement("msg")]
        public new string Msg { get; set; }
    }
}
