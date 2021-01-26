using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TM.Core.Payment.Alipay.Domain
{
    /// <summary>
    /// OldListListComplexMockModel Data Structure.
    /// </summary>
    [Serializable]
    public class OldListListComplexMockModel : AlipayObject
    {
        /// <summary>
        /// 复杂模型list
        /// </summary>
        [JsonProperty("cm_list")]
        [XmlArray("cm_list")]
        [XmlArrayItem("old_complext_mock_model")]
        public List<OldComplextMockModel> CmList { get; set; }
    }
}
