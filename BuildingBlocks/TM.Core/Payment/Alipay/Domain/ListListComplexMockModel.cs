using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// ListListComplexMockModel Data Structure.
    /// </summary>
    [Serializable]
    public class ListListComplexMockModel : AlipayObject
    {
        /// <summary>
        /// 复杂对象list
        /// </summary>
        [JsonProperty("cm_list")]
        [XmlArray("cm_list")]
        [XmlArrayItem("complext_mock_model")]
        public List<ComplextMockModel> CmList { get; set; }
    }
}
