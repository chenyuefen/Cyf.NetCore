using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipaySecurityProdFacepayVerifyModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipaySecurityProdFacepayVerifyModel : AlipayObject
    {
        /// <summary>
        /// 用户输入的扫脸付邀请码
        /// </summary>
        [JsonProperty("check_code")]
        [XmlElement("check_code")]
        public string CheckCode { get; set; }

        /// <summary>
        /// Base64编码的人脸图片。与ftoken参数二选一，当商户先前未调用人脸识别接口时使用此参数。
        /// </summary>
        [JsonProperty("face_image")]
        [XmlElement("face_image")]
        public string FaceImage { get; set; }

        /// <summary>
        /// 商户调用人脸上传接口时获得的ftoken。与face_image参数二选一，当商户先前调用了人脸识别接口并获得了ftoken时使用此参数
        /// </summary>
        [JsonProperty("ftoken")]
        [XmlElement("ftoken")]
        public string Ftoken { get; set; }

        /// <summary>
        /// 商户门店编号
        /// </summary>
        [JsonProperty("store_id")]
        [XmlElement("store_id")]
        public string StoreId { get; set; }

        /// <summary>
        /// 用户认证标识。传入完整的用户标识（例如用户输入的完整的11位用户手机号码,13800138000）或部分信息脱敏的用户标识（例如138****8000）。当热点人脸库命中成功时，可以使用部分信息脱敏的用户标识
        /// </summary>
        [JsonProperty("user_auth_id")]
        [XmlElement("user_auth_id")]
        public string UserAuthId { get; set; }

        /// <summary>
        /// 用户标识类型。目前支持手机号码，即mobile
        /// </summary>
        [JsonProperty("user_auth_type")]
        [XmlElement("user_auth_type")]
        public string UserAuthType { get; set; }
    }
}
