using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceIotMdeviceprodAssetQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayCommerceIotMdeviceprodAssetQueryModel : AlipayObject
    {
        /// <summary>
        /// 模板ID（物料系统的item_id）
        /// </summary>
        [JsonProperty("item_id")]
        [XmlElement("item_id")]
        public string ItemId { get; set; }
    }
}
