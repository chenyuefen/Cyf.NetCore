using System.Collections.Generic;
using TM.Core.Payment.WeChatPay.Response;

namespace TM.Core.Payment.WeChatPay.Request
{
    /// <summary>
    /// 查询红包记录
    /// </summary>
    public class WeChatPayGetHBInfoRequest : IWeChatPayCertificateRequest<WeChatPayGetHBInfoResponse>
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string MchBillNo { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string BillType { get; set; }

        #region IWeChatPayCertificateRequest Members

        public string GetRequestUrl()
        {
            return "https://api.mch.weixin.qq.com/mmpaymkttransfers/gethbinfo";
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new WeChatPayDictionary()
            {
                { "appid", AppId },
                { "mch_billno", MchBillNo },
                { "bill_type", BillType },
            };
            return parameters;
        }

        #endregion
    }
}
