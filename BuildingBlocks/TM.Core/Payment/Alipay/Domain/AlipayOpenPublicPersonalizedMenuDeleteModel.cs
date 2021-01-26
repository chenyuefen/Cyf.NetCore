using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenPublicPersonalizedMenuDeleteModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenPublicPersonalizedMenuDeleteModel : AlipayObject
    {
        /// <summary>
        /// 要删除的个性化菜单key
        /// </summary>
        [JsonProperty("menu_key")]
        [XmlElement("menu_key")]
        public string MenuKey { get; set; }
    }
}
