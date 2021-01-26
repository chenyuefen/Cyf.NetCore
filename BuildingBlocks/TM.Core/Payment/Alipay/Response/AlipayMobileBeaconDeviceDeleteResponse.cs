using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMobileBeaconDeviceDeleteResponse.
    /// </summary>
    public class AlipayMobileBeaconDeviceDeleteResponse : AlipayResponse
    {
        /// <summary>
        /// 操作返回码
        /// </summary>
        [JsonProperty("code")]
        [XmlElement("code")]
        public new string Code { get; set; }

        /// <summary>
        /// 请求处理结果
        /// </summary>
        [JsonProperty("msg")]
        [XmlElement("msg")]
        public new string Msg { get; set; }
    }
}
