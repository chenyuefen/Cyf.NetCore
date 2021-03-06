using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenAppYufanlingsanyaowuYufalingsanyaowuQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenAppYufanlingsanyaowuYufalingsanyaowuQueryModel : AlipayObject
    {
        /// <summary>
        /// 省份编码，国标码
        /// </summary>
        [JsonProperty("province_code")]
        [XmlArray("province_code")]
        [XmlArrayItem("string")]
        public List<string> ProvinceCode { get; set; }
    }
}
