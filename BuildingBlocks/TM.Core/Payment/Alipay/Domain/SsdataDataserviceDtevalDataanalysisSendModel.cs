using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// SsdataDataserviceDtevalDataanalysisSendModel Data Structure.
    /// </summary>
    [Serializable]
    public class SsdataDataserviceDtevalDataanalysisSendModel : AlipayObject
    {
        /// <summary>
        /// 业务编号, 唯一流水标识
        /// </summary>
        [JsonProperty("biz_number")]
        [XmlElement("biz_number")]
        public string BizNumber { get; set; }

        /// <summary>
        /// 业务来源，暂只支持上数来源数据流入
        /// </summary>
        [JsonProperty("biz_source")]
        [XmlElement("biz_source")]
        public string BizSource { get; set; }

        /// <summary>
        /// 授权采集数据，爬取的完整数据加部分业务标识信息
        /// </summary>
        [JsonProperty("data_content")]
        [XmlElement("data_content")]
        public string DataContent { get; set; }

        /// <summary>
        /// 处理业务类型，包括运营商、公积金等
        /// </summary>
        [JsonProperty("process_biz_type")]
        [XmlElement("process_biz_type")]
        public string ProcessBizType { get; set; }
    }
}
