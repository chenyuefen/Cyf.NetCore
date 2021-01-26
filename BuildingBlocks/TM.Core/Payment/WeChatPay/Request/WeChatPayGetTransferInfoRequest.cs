﻿using System.Collections.Generic;
using TM.Core.Payment.WeChatPay.Response;

namespace TM.Core.Payment.WeChatPay.Request
{
    /// <summary>
    /// 查询企业付款
    /// </summary>
    public class WeChatPayGetTransferInfoRequest : IWeChatPayCertificateRequest<WeChatPayGetTransferInfoResponse>
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string PartnerTradeNo { get; set; }

        #region IWeChatPayCertificateRequest Members

        public string GetRequestUrl()
        {
            return "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new WeChatPayDictionary()
            {
                { "appid", AppId },
                { "partner_trade_no", PartnerTradeNo },
            };
            return parameters;
        }

        #endregion
    }
}
