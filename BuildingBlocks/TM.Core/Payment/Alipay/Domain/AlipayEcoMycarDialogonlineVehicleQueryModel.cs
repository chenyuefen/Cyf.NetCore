using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEcoMycarDialogonlineVehicleQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEcoMycarDialogonlineVehicleQueryModel : AlipayObject
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [JsonProperty("vi_id")]
        [XmlElement("vi_id")]
        public string ViId { get; set; }
    }
}
