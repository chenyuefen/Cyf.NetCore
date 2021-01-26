using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceCityfacilitatorCityQueryResponse.
    /// </summary>
    public class AlipayCommerceCityfacilitatorCityQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 城市列表
        /// </summary>
        [JsonProperty("citys")]
        [XmlArray("citys")]
        [XmlArrayItem("city_function")]
        public List<CityFunction> Citys { get; set; }
    }
}
