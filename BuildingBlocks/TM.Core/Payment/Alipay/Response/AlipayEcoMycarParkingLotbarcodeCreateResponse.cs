using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoMycarParkingLotbarcodeCreateResponse.
    /// </summary>
    public class AlipayEcoMycarParkingLotbarcodeCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 返回二维码链接地址
        /// </summary>
        [JsonProperty("qrcode_url")]
        [XmlElement("qrcode_url")]
        public string QrcodeUrl { get; set; }

        /// <summary>
        /// 返回状态：1为成功，0为失败
        /// </summary>
        [JsonProperty("status")]
        [XmlElement("status")]
        public string Status { get; set; }
    }
}
