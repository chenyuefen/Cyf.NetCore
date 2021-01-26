using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCommerceIotMdeviceprodAssetapplyQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayCommerceIotMdeviceprodAssetapplyQueryModel : AlipayObject
    {
        /// <summary>
        /// 物料平台的申请单ID
        /// </summary>
        [JsonProperty("apply_order_id")]
        [XmlElement("apply_order_id")]
        public string ApplyOrderId { get; set; }
    }
}
