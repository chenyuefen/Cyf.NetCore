using Newtonsoft.Json;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataserviceUserlevelZrankGetResponse.
    /// </summary>
    public class AlipayDataDataserviceUserlevelZrankGetResponse : AlipayResponse
    {
        /// <summary>
        /// 活跃高价值用户返回
        /// </summary>
        [JsonProperty("result")]
        [XmlElement("result")]
        public AlipayHighValueCustomerResult Result { get; set; }
    }
}
