using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicMenuQueryResponse.
    /// </summary>
    public class AlipayOpenPublicMenuQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 一级菜单数组，个数应为1~4个
        /// </summary>
        [JsonProperty("menu_content")]
        [XmlElement("menu_content")]
        public string MenuContent { get; set; }
    }
}
