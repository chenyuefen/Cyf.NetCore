using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using TM.Core.Payment.Alipay.Domain;

namespace TM.Core.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenPublicAccountQueryResponse.
    /// </summary>
    public class AlipayOpenPublicAccountQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 绑定账户列表
        /// </summary>
        [JsonProperty("public_bind_accounts")]
        [XmlArray("public_bind_accounts")]
        [XmlArrayItem("std_public_bind_account")]
        public List<StdPublicBindAccount> PublicBindAccounts { get; set; }
    }
}
