using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingDataDishdiagnoseBatchqueryResponse.
    /// </summary>
    public class KoubeiMarketingDataDishdiagnoseBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询返回的详情数据
        /// </summary>
        [JsonProperty("item_diagnose_list")]
        [XmlArray("item_diagnose_list")]
        [XmlArrayItem("item_diagnose_detail")]
        public List<ItemDiagnoseDetail> ItemDiagnoseList { get; set; }

        /// <summary>
        /// 记录的总条数
        /// </summary>
        [JsonProperty("total")]
        [XmlElement("total")]
        public long Total { get; set; }
    }
}
