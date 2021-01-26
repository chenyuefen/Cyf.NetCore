﻿using System.Collections.Generic;
using TM.Core.Payment.WeChatPay.Response;

namespace TM.Core.Payment.WeChatPay.Request
{
    /// <summary>
    /// 发放裂变红包
    /// </summary>
    public class WeChatPaySendGroupRedPackRequest : IWeChatPayCertificateRequest<WeChatPaySendGroupRedPackResponse>
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string MchBillNo { get; set; }

        /// <summary>
        /// 子商户号
        /// </summary>
        public string SubMchId { get; set; }

        /// <summary>
        /// 公众账号appid
        /// </summary>
        public string WXAppId { get; set; }

        /// <summary>
        /// 触达用户appid
        /// </summary>
        public string MsgAppId { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string SendName { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        public string ReOpenId { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// 红包发放总人数
        /// </summary>
        public int TotalNum { get; set; }

        /// <summary>
        /// 红包金额设置方式
        /// </summary>
        public string AmtType { get; set; }

        /// <summary>
        /// 红包祝福语
        /// </summary>
        public string Wishing { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 场景id
        /// </summary>
        public string SceneId { get; set; }

        /// <summary>
        /// 活动信息
        /// </summary>
        public string RiskInfo { get; set; }

        /// <summary>
        /// 资金授权商户号
        /// </summary>
        public string ConsumeMchId { get; set; }

        #region IWeChatPayCertificateRequest Members

        public string GetRequestUrl()
        {
            return "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendgroupredpack";
        }

        public IDictionary<string, string> GetParameters()
        {
            var parameters = new WeChatPayDictionary()
            {
                { "mch_billno", MchBillNo },
                { "sub_mch_id", SubMchId },
                { "wxappid", WXAppId },
                { "msgappid", MsgAppId },
                { "send_name", SendName },
                { "re_openid", ReOpenId },
                { "total_amount", TotalAmount },
                { "total_num", TotalNum },
                { "amt_type", AmtType },
                { "wishing", Wishing },
                { "act_name", ActName },
                { "remark", Remark },
                { "scene_id", SceneId },
                { "risk_info", RiskInfo },
                { "consume_mch_id", ConsumeMchId },
            };
            return parameters;
        }

        #endregion
    }
}
