using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// BusinessLicenseCertFileds Data Structure.
    /// </summary>
    [Serializable]
    public class BusinessLicenseCertFileds : AlipayObject
    {
        /// <summary>
        /// 社会信用代码
        /// </summary>
        [JsonProperty("creditcode")]
        [XmlElement("creditcode")]
        public string Creditcode { get; set; }

        /// <summary>
        /// 公司名字
        /// </summary>
        [JsonProperty("entname")]
        [XmlElement("entname")]
        public string Entname { get; set; }
    }
}
