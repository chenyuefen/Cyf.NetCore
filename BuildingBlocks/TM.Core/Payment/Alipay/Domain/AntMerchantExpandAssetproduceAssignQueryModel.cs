using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandAssetproduceAssignQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AntMerchantExpandAssetproduceAssignQueryModel : AlipayObject
    {
        /// <summary>
        /// 每次拉取最大记录数量，可选值为[1,200] ;
        /// </summary>
        [JsonProperty("page_size")]
        [XmlElement("page_size")]
        public long PageSize { get; set; }
    }
}
