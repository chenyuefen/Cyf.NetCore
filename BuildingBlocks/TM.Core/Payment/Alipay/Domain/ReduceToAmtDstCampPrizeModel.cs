using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// ReduceToAmtDstCampPrizeModel Data Structure.
    /// </summary>
    [Serializable]
    public class ReduceToAmtDstCampPrizeModel : AlipayObject
    {
        /// <summary>
        /// 优惠后价格
        /// </summary>
        [JsonProperty("reduct_to_amt")]
        [XmlElement("reduct_to_amt")]
        public string ReductToAmt { get; set; }
    }
}
