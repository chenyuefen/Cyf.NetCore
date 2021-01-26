using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// PromoteDateModel Data Structure.
    /// </summary>
    [Serializable]
    public class PromoteDateModel : AlipayObject
    {
        /// <summary>
        /// 日期
        /// </summary>
        [JsonProperty("date")]
        [XmlElement("date")]
        public string Date { get; set; }

        /// <summary>
        /// 推广数据
        /// </summary>
        [JsonProperty("promote_data")]
        [XmlElement("promote_data")]
        public PromoteDataModel PromoteData { get; set; }
    }
}
