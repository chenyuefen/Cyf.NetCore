using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenPublicPartnerSubscribeSyncModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayOpenPublicPartnerSubscribeSyncModel : AlipayObject
    {
        /// <summary>
        /// 是否接受服务窗消息
        /// </summary>
        [JsonProperty("accept_msg")]
        [XmlElement("accept_msg")]
        public string AcceptMsg { get; set; }

        /// <summary>
        /// 关注的服务窗id
        /// </summary>
        [JsonProperty("follow_object_id")]
        [XmlElement("follow_object_id")]
        public string FollowObjectId { get; set; }

        /// <summary>
        /// 操作类型，添加关注或取消关注
        /// </summary>
        [JsonProperty("operate_type")]
        [XmlElement("operate_type")]
        public string OperateType { get; set; }

        /// <summary>
        /// 是否打开接收公众号PUSH提醒开关 ON:打开 OFF:关闭
        /// </summary>
        [JsonProperty("push_switch")]
        [XmlElement("push_switch")]
        public string PushSwitch { get; set; }

        /// <summary>
        /// 关注来源
        /// </summary>
        [JsonProperty("source_id")]
        [XmlElement("source_id")]
        public string SourceId { get; set; }

        /// <summary>
        /// 关注服务窗的用户id
        /// </summary>
        [JsonProperty("user_id")]
        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}
