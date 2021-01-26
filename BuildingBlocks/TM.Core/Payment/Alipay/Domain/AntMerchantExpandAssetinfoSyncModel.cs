using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandAssetinfoSyncModel Data Structure.
    /// </summary>
    [Serializable]
    public class AntMerchantExpandAssetinfoSyncModel : AlipayObject
    {
        /// <summary>
        /// 传入需要反馈的物料信息对象列表.
        /// </summary>
        [JsonProperty("asset_infos")]
        [XmlArray("asset_infos")]
        [XmlArrayItem("asset_info_item")]
        public List<AssetInfoItem> AssetInfos { get; set; }
    }
}
