using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KbAdvertDealBillResponse Data Structure.
    /// </summary>
    [Serializable]
    public class KbAdvertDealBillResponse : AlipayObject
    {
        /// <summary>
        /// 账单下载地址(为空表示查无账单)
        /// </summary>
        [JsonProperty("download_url")]
        [XmlElement("download_url")]
        public string DownloadUrl { get; set; }
    }
}
