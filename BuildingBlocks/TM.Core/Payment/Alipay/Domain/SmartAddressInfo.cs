using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// SmartAddressInfo Data Structure.
    /// </summary>
    [Serializable]
    public class SmartAddressInfo : AlipayObject
    {
        /// <summary>
        /// 行政区代码-区，使用国家行政区划代码，可参考http://www.stats.gov.cn/tjsj/tjbz/xzqhdm/
        /// </summary>
        [JsonProperty("area_code")]
        [XmlElement("area_code")]
        public long AreaCode { get; set; }

        /// <summary>
        /// 行政区代码-市，使用国家行政区划代码，可参考http://www.stats.gov.cn/tjsj/tjbz/xzqhdm/
        /// </summary>
        [JsonProperty("city_code")]
        [XmlElement("city_code")]
        public long CityCode { get; set; }

        /// <summary>
        /// 自助售货机地址
        /// </summary>
        [JsonProperty("machine_address")]
        [XmlElement("machine_address")]
        public string MachineAddress { get; set; }

        /// <summary>
        /// 行政区代码-省，使用国家行政区划代码，可参考http://www.stats.gov.cn/tjsj/tjbz/xzqhdm/
        /// </summary>
        [JsonProperty("province_code")]
        [XmlElement("province_code")]
        public long ProvinceCode { get; set; }
    }
}
