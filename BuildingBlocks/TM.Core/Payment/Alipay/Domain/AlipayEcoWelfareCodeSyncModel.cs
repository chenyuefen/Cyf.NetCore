using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEcoWelfareCodeSyncModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayEcoWelfareCodeSyncModel : AlipayObject
    {
        /// <summary>
        /// 支付宝账户USERID，和福利账户ID而选一，可以通过支付宝钱包用户信息共享接口获取支付宝账户ID
        /// </summary>
        [JsonProperty("alipay_user_id")]
        [XmlElement("alipay_user_id")]
        public string AlipayUserId { get; set; }

        /// <summary>
        /// 条码码值
        /// </summary>
        [JsonProperty("code")]
        [XmlElement("code")]
        public string Code { get; set; }

        /// <summary>
        /// 条码可使用超时时间  格式为yyyy-MM-dd HH:mm:ss   备注：超时时间不允许比启动时间小
        /// </summary>
        [JsonProperty("code_expire_date")]
        [XmlElement("code_expire_date")]
        public string CodeExpireDate { get; set; }

        /// <summary>
        /// 条码数量
        /// </summary>
        [JsonProperty("code_num")]
        [XmlElement("code_num")]
        public long CodeNum { get; set; }

        /// <summary>
        /// 条码图片url
        /// </summary>
        [JsonProperty("code_pic_url")]
        [XmlElement("code_pic_url")]
        public string CodePicUrl { get; set; }

        /// <summary>
        /// 条码可使用开发时间  格式为yyyy-MM-dd HH:mm:ss。
        /// </summary>
        [JsonProperty("code_start_date")]
        [XmlElement("code_start_date")]
        public string CodeStartDate { get; set; }

        /// <summary>
        /// 条码状态  CREATE 创建  NOT_USED 没有使用  USED 已经被使用  INVALID 码无效  EXPIRED 码过期  DISABLED 码冻结  NOT_EXIST 码不存在
        /// </summary>
        [JsonProperty("code_status")]
        [XmlElement("code_status")]
        public string CodeStatus { get; set; }

        /// <summary>
        /// 条码状态变更时间  格式为yyyy-MM-dd HH:mm:ss。
        /// </summary>
        [JsonProperty("code_status_date")]
        [XmlElement("code_status_date")]
        public string CodeStatusDate { get; set; }

        /// <summary>
        /// 条码业务类型  商品品类码：goods  用户商品条码：barcode
        /// </summary>
        [JsonProperty("code_type")]
        [XmlElement("code_type")]
        public string CodeType { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        [JsonProperty("extend_params")]
        [XmlElement("extend_params")]
        public string ExtendParams { get; set; }

        /// <summary>
        /// ISV代码，唯一确定ISV身份由福利平台分配
        /// </summary>
        [JsonProperty("isv_code")]
        [XmlElement("isv_code")]
        public string IsvCode { get; set; }

        /// <summary>
        /// 核销门店详细信息
        /// </summary>
        [JsonProperty("store_info")]
        [XmlElement("store_info")]
        public WelfareEcoStoreInfo StoreInfo { get; set; }

        /// <summary>
        /// 同步数据时间 格式为yyyy-MM-dd HH:mm:ss。
        /// </summary>
        [JsonProperty("sync_date")]
        [XmlElement("sync_date")]
        public string SyncDate { get; set; }

        /// <summary>
        /// 福利平台订单对应的用户ID，和支付宝用户ID而选一
        /// </summary>
        [JsonProperty("welfare_user_id")]
        [XmlElement("welfare_user_id")]
        public string WelfareUserId { get; set; }
    }
}
