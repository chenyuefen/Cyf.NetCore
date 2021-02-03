﻿using Newtonsoft.Json;

namespace TM.Core.Payment.Alipay.Notify
{
    /// <summary>
    /// App支付异步通知
    /// </summary>
    public class AlipayTradeAppPayNotifyResponse : AlipayNotifyResponse
    {
        /// <summary>
        /// 通知时间
        /// </summary>
        [JsonProperty("notify_time")]
        public string NotifyTime { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        [JsonProperty("notify_type")]
        public string NotifyType { get; set; }

        /// <summary>
        /// 通知校验ID
        /// </summary>
        [JsonProperty("notify_id")]
        public string NotifyId { get; set; }

        /// <summary>
        /// 支付宝分配给开发者的应用Id
        /// </summary>
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// 编码格式
        /// </summary>
        [JsonProperty("charset")]
        public string Charset { get; set; }

        /// <summary>
        /// 接口版本
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        [JsonProperty("sign_type")]
        public string SignType { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [JsonProperty("sign")]
        public string Sign { get; set; }

        // 业务参数

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        [JsonProperty("trade_no")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 商户业务号
        /// </summary>
        [JsonProperty("out_biz_no")]
        public string OutBizNo { get; set; }

        /// <summary>
        /// 买家支付宝用户号
        /// </summary>
        [JsonProperty("buyer_id")]
        public string BuyerId { get; set; }

        /// <summary>
        /// 买家支付宝账号
        /// </summary>
        [JsonProperty("buyer_logon_id")]
        public string BuyerLogonId { get; set; }

        /// <summary>
        /// 卖家支付宝用户号
        /// </summary>
        [JsonProperty("seller_id")]
        public string SellerId { get; set; }

        /// <summary>
        /// 卖家支付宝账号
        /// </summary>
        [JsonProperty("seller_email")]
        public string SellerEmail { get; set; }

        /// <summary>
        /// 交易状态
        /// WAIT_BUYER_PAY（交易创建，等待买家付款）、
        /// TRADE_CLOSED（未付款交易超时关闭，或支付完成后全额退款）、
        /// TRADE_SUCCESS（交易支付成功）、
        /// TRADE_FINISHED（交易结束，不可退款）
        /// </summary>
        [JsonProperty("trade_status")]
        public string TradeStatus { get; set; }

        /// <summary>
        /// 交易的订单金额，单位为元，两位小数。该参数的值为支付时传入的total_amount
        /// </summary>
        [JsonProperty("total_amount")]
        public string TotalAmount { get; set; }

        /// <summary>
        /// 实收金额，单位为元，两位小数。该金额为本笔交易，商户账户能够实际收到的金额
        /// </summary>
        [JsonProperty("receipt_amount")]
        public string ReceiptAmount { get; set; }

        /// <summary>
        /// 开票金额  交易中用户支付的可开具发票的金额，单位为元，两位小数。该金额代表该笔交易中可以给用户开具发票的金额
        /// </summary>
        [JsonProperty("invoice_amount")]
        public string InvoiceAmount { get; set; }

        /// <summary>
        /// 买家实付金额，单位为元，两位小数。该金额代表该笔交易买家实际支付的金额，不包含商户折扣等金额
        /// </summary>
        [JsonProperty("buyer_pay_amount")]
        public string BuyerPayAmount { get; set; }

        /// <summary>
        /// 积分支付的金额，单位为元，两位小数。该金额代表该笔交易中用户使用积分支付的金额，比如集分宝或者支付宝实时优惠等
        /// </summary>
        [JsonProperty("point_amount")]
        public string PointAmount { get; set; }

        /// <summary>
        /// 总退款金额
        /// </summary>
        [JsonProperty("refund_fee")]
        public string RefundFee { get; set; }

        /// <summary>
        /// 订单标题
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// 交易创建时间
        /// </summary>
        [JsonProperty("gmt_create")]
        public string GmtCreate { get; set; }

        /// <summary>
        /// 交易付款时间
        /// </summary>
        [JsonProperty("gmt_payment")]
        public string GmtPayment { get; set; }

        /// <summary>
        /// 交易退款时间
        /// </summary>
        [JsonProperty("gmt_refund")]
        public string GmtRefund { get; set; }

        /// <summary>
        /// 交易结束时间
        /// </summary>
        [JsonProperty("gmt_close")]
        public string GmtClose { get; set; }

        /// <summary>
        /// 支付金额信息
        /// </summary>
        [JsonProperty("fund_bill_list")]
        public string FundBillList { get; set; }

        /// <summary>
        /// 回传参数
        /// </summary>
        [JsonProperty("passback_params")]
        public string PassbackParams { get; set; }

        /// <summary>
        /// 优惠券信息
        /// </summary>
        [JsonProperty("voucher_detail_list")]
        public string VoucherDetailList { get; set; }
    }
}