using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandEnterpriseApplyModel Data Structure.
    /// </summary>
    [Serializable]
    public class AntMerchantExpandEnterpriseApplyModel : AlipayObject
    {
        /// <summary>
        /// 企业基础信息
        /// </summary>
        [JsonProperty("base_info")]
        [XmlElement("base_info")]
        public BaseInfo BaseInfo { get; set; }

        /// <summary>
        /// 企业对公账户信息
        /// </summary>
        [JsonProperty("business_bank_account_info")]
        [XmlElement("business_bank_account_info")]
        public BusinessBankAccountInfo BusinessBankAccountInfo { get; set; }

        /// <summary>
        /// 企业营业执照信息
        /// </summary>
        [JsonProperty("business_license_info")]
        [XmlElement("business_license_info")]
        public BusinessLicenceInfo BusinessLicenseInfo { get; set; }

        /// <summary>
        /// 企业级商户法人信息
        /// </summary>
        [JsonProperty("legal_representative_info")]
        [XmlElement("legal_representative_info")]
        public LegalRepresentativeInfo LegalRepresentativeInfo { get; set; }

        /// <summary>
        /// 支付宝登录别名,必须是邮箱地址。入驻申请结果会通知到该邮箱地址或手机号码。如填入的是已有的企业版支付宝账号则后续认证与签约基于该账号进行，如填入的邮箱地址没有对应的支付宝账号则用该邮箱地址创建一个企业版支付宝账户，如填入的是已有的非企业版支付宝账号则预校验失败。
        /// </summary>
        [JsonProperty("login_id")]
        [XmlElement("login_id")]
        public string LoginId { get; set; }

        /// <summary>
        /// 外部入驻申请单据号，需保证在开发者端不重复
        /// </summary>
        [JsonProperty("out_biz_no")]
        [XmlElement("out_biz_no")]
        public string OutBizNo { get; set; }

        /// <summary>
        /// 企业的门店信息，签约当面付时必选
        /// </summary>
        [JsonProperty("shop_info")]
        [XmlElement("shop_info")]
        public ShopInfo ShopInfo { get; set; }
    }
}
