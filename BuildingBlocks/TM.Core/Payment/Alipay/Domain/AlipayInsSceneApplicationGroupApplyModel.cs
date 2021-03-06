using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayInsSceneApplicationGroupApplyModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayInsSceneApplicationGroupApplyModel : AlipayObject
    {
        /// <summary>
        /// 收件人
        /// </summary>
        [JsonProperty("addressee")]
        [XmlElement("addressee")]
        public InsAddressee Addressee { get; set; }

        /// <summary>
        /// 投保人
        /// </summary>
        [JsonProperty("applicant")]
        [XmlElement("applicant")]
        public InsPerson Applicant { get; set; }

        /// <summary>
        /// 投保申请信息列表
        /// </summary>
        [JsonProperty("applications")]
        [XmlArray("applications")]
        [XmlArrayItem("ins_application")]
        public List<InsApplication> Applications { get; set; }

        /// <summary>
        /// 保费支付账单流水的标题
        /// </summary>
        [JsonProperty("bill_title")]
        [XmlElement("bill_title")]
        public string BillTitle { get; set; }

        /// <summary>
        /// 商户生成的外部投保业务号,必须保证唯一
        /// </summary>
        [JsonProperty("out_biz_no")]
        [XmlElement("out_biz_no")]
        public string OutBizNo { get; set; }

        /// <summary>
        /// 产品编码;由蚂蚁保险平台分配,商户通过该产品编码投保特定的保险产品
        /// </summary>
        [JsonProperty("prod_code")]
        [XmlElement("prod_code")]
        public string ProdCode { get; set; }

        /// <summary>
        /// 渠道来源
        /// </summary>
        [JsonProperty("source")]
        [XmlElement("source")]
        public string Source { get; set; }
    }
}
