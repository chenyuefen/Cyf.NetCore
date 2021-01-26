using System.Collections.Generic;
using TM.Core.Payment.WeChatPay.Response;

namespace TM.Core.Payment.WeChatPay.Request
{
    /// <summary>
    /// 查询企业付款银行卡
    /// </summary>
    public class WeChatPayQueryBankRequest : IWeChatPayCertificateRequest<WeChatPayQueryBankResponse>
    {
        /// <summary>
        /// 商户企业付款单号
        /// </summary>
        public string PartnerTradeNo { get; set; }

        #region IWeChatPayCertificateRequest Members

        public string GetRequestUrl()
        {
            return "https://api.mch.weixin.qq.com/mmpaysptrans/query_bank";
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new WeChatPayDictionary()
            {
                { "partner_trade_no", PartnerTradeNo }
            };
            return parameters;
        }

        #endregion
    }
}
