using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEcoMycarDataserviceMaintainvehicleShareModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEcoMycarDataserviceMaintainvehicleShareModel : AlipayObject
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [JsonProperty("vid")]
        [XmlElement("vid")]
        public string Vid { get; set; }
    }
}
