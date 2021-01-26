
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：WxPayDto
// 文件功能描述： WxPayDto
//
// 创建者：庄欣锴
// 创建时间：2020年5月22日18:16:38
0// 
//----------------------------------------------------------------*/

using System.ComponentModel;
using System.Xml.Serialization;
namespace TM.Infrastructure.TencentCloud.Dto
{
    public class WxPayDto
    {
    }

    public class WxPayToBankRequest
    {
        /// <summary>
        ///微信支付分配的商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户企业付款单号
        /// </summary>
        public string PartnerTradeNo { get; set; }

        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 收款方银行卡号
        /// </summary>
        public string EncBankNo { get; set; }

        /// <summary>
        /// 收款方用户名
        /// </summary>
        public string EncTrueName { get; set; }

        /// <summary>
        /// 银行卡所在开户行编号
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// 付款金额：RMB分 （支付总额，不含手续费）
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 企业付款到银行卡付款说明,即订单备注
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
    }

    #region 企业付款到银行卡返回结果

    [XmlType(TypeName = "xml")]
    public class PayToUserResult
    {
        /// <summary>
        /// SUCCESS/FAIL
        ///字段是通信标识，非付款标识，付款是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 业务结果
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误代码	
        /// </summary>
        public ErrCode err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 商户企业付款单号
        /// </summary>
        public string partner_trade_no { get; set; }

        /// <summary>
        /// 代付金额RMB:分
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 随机字符串，长度小于32位
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 返回包携带签名给商
        /// </summary>
        public string sign { get; set; }

        /*以下字段在return_code 和result_code都为SUCCESS的时候有返回*/
        /// <summary>
        /// 代付成功后，返回的内部业务单号
        /// </summary>
        public string payment_no { get; set; }

        /// <summary>
        /// 手续费金额 RMB：分
        /// </summary>
        public int cmms_amt { get; set; }

    }

    public enum ErrCode
    {
        [Description("无效的请求，商户系统异常导致，商户权限异常、证书错误、频率限制等。使用原单号以及原请求参数重试。")]
        INVALID_REQUEST,
        [Description("业务错误导致交易失败。请先调用查询接口，查询此次付款结果，如结果为不明确状态（如订单号不存在），请务必使用原商户订单号及原请求参数重试")]
        SYSTEMERROR,
        [Description("参数错误，商户系统异常导致。商户检查请求参数是否合法，证书，签名")]
        PARAM_ERROR,
        [Description("签名错误")]
        SIGNERROR,
        [Description("超额；已达到今日付款金额上限或已达到今日银行卡收款金额上限")]
        AMOUNT_LIMIT,
        [Description("受理失败，订单已存在")]
        ORDERPAID,
        [Description("已存在该单，并且订单信息不一致；或订单太老")]
        FATAL_ERROR,
        [Description("账号余额不足")]
        NOTENOUGH,
        [Description("超过每分钟600次的频率限制")]
        FREQUENCY_LIMITED,
        [Description("Wx侧受理成功")]
        SUCCESS,
        [Description("收款账户不在收款账户列表")]
        RECV_ACCOUNT_NOT_ALLOWED,
        [Description("本商户号未配置API发起能力")]
        PAY_CHANNEL_NOT_ALLOWED,
        [Description("已达到付款给此用户次数上限")]
        SENDNUM_LIMIT,
        [Description("此IP地址不允许调用接口")]
        NO_AUTH

    }

    #endregion

    #region 获取Rsa公钥

    [XmlType(TypeName = "xml")]
    public class PublicKeyModel
    {
        public string return_code { get; set; }

        public string return_msg { get; set; }


        public string result_code { get; set; }

        public PublicKeyCode err_code { get; set; }

        public string err_code_des { get; set; }

        public string mch_id { get; set; }

        public string pub_key { get; set; }
    }

    public enum PublicKeyCode
    {
        [Description("签名错误")]
        SIGNERROR,
        [Description("系统繁忙，请稍后重试")]
        SYSTEMERROR
    }


    #endregion

    #region 查询企业付款到银行卡
    public class QueryBankProgressResponse
    {
        /// <summary>
        /// SUCCESS/FAIL
        ///此字段是通信标识，非付款标识，付款是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }

        /**以下字段在return_code为SUCCESS的时候有返回**/
        /// <summary>
        /// 业务结果
        /// SUCCESS/FAIL，非付款标识
        /// 付款是否成功需要查看status字段来判断
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误码信息
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 结果信息描述
        /// </summary>
        public string err_code_des { get; set; }

        /**以下字段在return_code 和result_code都为SUCCESS的时候有返回**/
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 商户企业付款单号
        /// </summary>
        public string partner_trade_no { get; set; }

        /// <summary>
        /// 微信企业付款单号,即为微信内部业务单号
        /// </summary>
        public string payment_no { get; set; }

        /// <summary>
        /// 银行卡号(MD5加密)
        /// </summary>
        public string bank_no_md5 { get; set; }

        /// <summary>
        /// 收款人真实姓名(MD5加密)
        /// </summary>
        public string true_name_md5 { get; set; }

        /// <summary>
        /// 代付订单金额RMB：分
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// 代付订单状态：
        /// PROCESSING（处理中，如有明确失败，则返回额外失败原因；否则没有错误原因）
        /// SUCCESS（付款成功）
        /// FAILED（付款失败,需要替换付款单号重新发起付款）
        /// BANK_FAIL（银行退票，订单状态由付款成功流转至退票,退票时付款金额和手续费会自动退还）
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 手续费订单金额 RMB：分
        /// </summary>
        public string cmms_amt { get; set; }

        /// <summary>
        /// 微信侧订单创建时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 微信侧付款成功时间（依赖银行的处理进度，可能出现延迟返回，甚至被银行退票的情况）
        /// </summary>
        public string pay_succ_time { get; set; }

        /// <summary>
        /// 订单失败原因（如：余额不足）
        /// </summary>
        public string reason { get; set; }
    }

    #endregion
}