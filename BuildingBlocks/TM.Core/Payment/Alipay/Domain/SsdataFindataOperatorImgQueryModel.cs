using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// SsdataFindataOperatorImgQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class SsdataFindataOperatorImgQueryModel : AlipayObject
    {
        /// <summary>
        /// 系统业务流水号，在提交用户信息时获得
        /// </summary>
        [JsonProperty("biz_no")]
        [XmlElement("biz_no")]
        public string BizNo { get; set; }
    }
}
