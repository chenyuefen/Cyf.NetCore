using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayDataDataserviceHolographicFactorQueryModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayDataDataserviceHolographicFactorQueryModel : AlipayObject
    {
        /// <summary>
        /// 参数中文名称:业务id  是否唯一：唯一  参数作用/应用场景：做幂等性控制  枚举值：无  如何获取：调用方生成传递过来  特殊说明：无
        /// </summary>
        [JsonProperty("biz_id")]
        [XmlElement("biz_id")]
        public string BizId { get; set; }

        /// <summary>
        /// 参数中文名称:身份证号  是否唯一：否  参数作用/应用场景：查询人脉因子和多头因子必备的用户三要素之一  枚举值：无  如何获取：商户传递给上数，上数传递到openapi  特殊说明：无
        /// </summary>
        [JsonProperty("cert_no")]
        [XmlElement("cert_no")]
        public string CertNo { get; set; }

        /// <summary>
        /// 参数中文名称:联系人列表  是否唯一：否  参数作用/应用场景：运行模型生成人脉因子必备的联系人列表参数  枚举值：无  如何获取：上数通过用户授权进行采集通讯录以及运营商报告，上数传递到openapi  特殊说明：无
        /// </summary>
        [JsonProperty("contact_info_list")]
        [XmlArray("contact_info_list")]
        [XmlArrayItem("holo_graphic_contact_info")]
        public List<HoloGraphicContactInfo> ContactInfoList { get; set; }

        /// <summary>
        /// 参数中文名称:运营商特征  是否唯一：否  参数作用/应用场景：运行模型生成人脉因子必备的运营商特征参数  枚举值：无  如何获取：上数通过用户授权采集运营商报告之后实时加工生成的运营商特征，上数传递到openapi  特殊说明：无
        /// </summary>
        [JsonProperty("isv_feature")]
        [XmlElement("isv_feature")]
        public string IsvFeature { get; set; }

        /// <summary>
        /// 参数中文名称:用户手机号  是否唯一：否  参数作用/应用场景：查询人脉因子和多头因子必备的用户三要素之一  枚举值：无  如何获取：商户传递给上数，上数传递到openapi  特殊说明：无
        /// </summary>
        [JsonProperty("mobile")]
        [XmlElement("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 参数中文名称:用户姓名  是否唯一：否  参数作用/应用场景：查询人脉因子和多头因子必备的用户三要素之一  枚举值：无  如何获取：商户传递给上数，上数传递到openapi  特殊说明：无
        /// </summary>
        [JsonProperty("user_name")]
        [XmlElement("user_name")]
        public string UserName { get; set; }
    }
}
