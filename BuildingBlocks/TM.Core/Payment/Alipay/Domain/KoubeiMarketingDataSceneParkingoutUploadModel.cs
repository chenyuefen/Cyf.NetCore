using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiMarketingDataSceneParkingoutUploadModel Data Structure.
    /// </summary>
    [Serializable]
    public class KoubeiMarketingDataSceneParkingoutUploadModel : AlipayObject
    {
        /// <summary>
        /// 停车出库场景业务参数
        /// </summary>
        [JsonProperty("biz_info")]
        [XmlElement("biz_info")]
        public ParkingOutScene BizInfo { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonProperty("ext_info")]
        [XmlElement("ext_info")]
        public string ExtInfo { get; set; }

        /// <summary>
        /// 外部请求号，确保唯一，用于幂等控制
        /// </summary>
        [JsonProperty("out_request_no")]
        [XmlElement("out_request_no")]
        public string OutRequestNo { get; set; }

        /// <summary>
        /// 场景类型
        /// </summary>
        [JsonProperty("scene_type")]
        [XmlElement("scene_type")]
        public string SceneType { get; set; }

        /// <summary>
        /// 支付宝用户的userId
        /// </summary>
        [JsonProperty("user_id")]
        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}
