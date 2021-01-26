using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayCreditAutofinanceLoanCloseModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayCreditAutofinanceLoanCloseModel : AlipayObject
    {
        /// <summary>
        /// 汽车金融内部订单号
        /// </summary>
        [JsonProperty("applyno")]
        [XmlElement("applyno")]
        public string Applyno { get; set; }

        /// <summary>
        /// 机构编号
        /// </summary>
        [JsonProperty("orgcode")]
        [XmlElement("orgcode")]
        public string Orgcode { get; set; }

        /// <summary>
        /// 外部平台订单号,64个字符以内、只能包含字母、数字、下划线；需保证在外部平台端不重复
        /// </summary>
        [JsonProperty("outorderno")]
        [XmlElement("outorderno")]
        public string Outorderno { get; set; }

        /// <summary>
        /// 关闭原因
        /// </summary>
        [JsonProperty("reson")]
        [XmlElement("reson")]
        public string Reson { get; set; }

        /// <summary>
        /// 关闭类型  1. CLOSE_USER_CANCEL(用户主动放弃贷款)  2. CLOSE_TD_REJECT(同盾校验失败)  3. CLOSE_OTHER(其他情况)
        /// </summary>
        [JsonProperty("type")]
        [XmlElement("type")]
        public string Type { get; set; }
    }
}
