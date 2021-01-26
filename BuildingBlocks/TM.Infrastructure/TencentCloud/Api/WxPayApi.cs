/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：WxPayToBankApi
// 文件功能描述： 微信提现到银行卡
// 文档:https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=24_2
//
// 创建者：庄欣锴
// 创建时间：2020年5月22日10:16:12
0// 
//----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Infrastructure.Configs;
using TM.Infrastructure.Helpers;
using TM.Infrastructure.TencentCloud.Config;
using TM.Infrastructure.TencentCloud.Dto;

namespace TM.Infrastructure.TencentCloud.Api
{
    public static class WxPayApi
    {
        #region 企业付款到银行卡

        /// <summary>
        /// 企业付款到银行卡
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static async Task<PayToUserResult> PayToBank(WxPayToBankRequest dto)
        {

            const string api = "https://api.mch.weixin.qq.com/mmpaysptrans/pay_bank";

            /*1.生成签名*/
            var dictionary = new SortedDictionary<string, object>
            {
                {"mch_id", WxPayConfig.mchId},
                {"partner_trade_no", dto.PartnerTradeNo},
                {"nonce_str", WeChatUtil.getNoncestr()},
                {"enc_bank_no", await WeChatUtil.RSAEncrypt(dto.EncBankNo)},
                {"enc_true_name", await WeChatUtil.RSAEncrypt(dto.EncTrueName)},
                {"bank_code", dto.BankCode},
                {"amount", dto.Amount}
            };
            dictionary.Add("sign", WeChatUtil.CreateSign(dictionary, WxPayConfig.key));
            /*2.生成xml数据*/
            var xml = WeChatUtil.WeChatSignXml(dictionary);
            /*开始请求接口*/
            try
            {
                var xmlStr = WeChatUtil.WxCerHttpPost(api, xml, WxPayConfig.cerPath, WxPayConfig.mchId);
                if (!Regexs.IsNull(xmlStr))
                {
                    var returnXml = WePayXmlUtil.XmlToObect<PayToUserResult>(xmlStr);
                    return returnXml;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        #endregion

        #region 用于对商户企业付款到银行卡操作进行结果查询，返回付款操作详细结果

        /// <summary>
        /// 用于对商户企业付款到银行卡操作进行结果查询
        /// </summary>
        /// <param name="partner_trade_no">商户订单号，需保持唯一（只允许数字[0~9]或字母[A~Z]和[a~z]最短8位，最长32位）</param>
        /// <returns></returns>
        public static async Task<QueryBankProgressResponse> QueryBankProgress(string partner_trade_no)
        {
            const string api = "https://api.mch.weixin.qq.com/mmpaysptrans/query_bank";

            /*1.生成签名*/
            var dictionary = new SortedDictionary<string, object>
            {
                {"mch_id",WxPayConfig.mchId},
                {"partner_trade_no",partner_trade_no},
                {"nonce_str",WeChatUtil.getNoncestr()},
            };
            dictionary.Add("sign", WeChatUtil.CreateSign(dictionary, WxPayConfig.key));
            /*2.生成xml数据*/
            var xml = WeChatUtil.WeChatSignXml(dictionary);

            /*开始请求接口*/
            try
            {
                var xmlStr = WeChatUtil.WxCerHttpPost(api, xml, WxPayConfig.cerPath, WxPayConfig.mchId);
                if (!string.IsNullOrWhiteSpace(xmlStr))
                {
                    var returnXml = WePayXmlUtil.XmlToObect<QueryBankProgressResponse>(xmlStr);
                    return returnXml;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        #endregion

        #region 获取RSA加密公钥

        public static async Task<string> Getpublickey()
        {

            const string api = "https://fraud.mch.weixin.qq.com/risk/getpublickey";
            var dictionary = new SortedDictionary<string, object>
            {
                {"mch_id", WxPayConfig.mchId}, {"nonce_str", WeChatUtil.getNoncestr()}, {"sign_type", "MD5"}
            };
            dictionary.Add("sign", WeChatUtil.CreateSign(dictionary, WxPayConfig.key));
            var xml = WeChatUtil.WeChatSignXml(dictionary);

            var cerPath = $"{Environment.CurrentDirectory}/{ConfigHelper.Configuration["WeChatPay:Certificate"]}";
            try
            {
                var response = WeChatUtil.WxCerHttpPost(api, xml, cerPath, WxPayConfig.mchId);

                if (string.IsNullOrWhiteSpace(response)) return "";
                var returnXml = WePayXmlUtil.XmlToObect<PublicKeyModel>(response);
                if (returnXml.return_code != "SUCCESS") return "";
                return returnXml.result_code == "SUCCESS" ? returnXml.pub_key : "";
            }
            catch (Exception e)
            {
                return "";
            }
        }

        #endregion
    }
}