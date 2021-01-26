using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Infrastructure.FuLu.Module
{
    public class GoodsCallBackInfo
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public string product_id { get; set; }

        /// <summary>
        /// 商品变更类型：1、商品状态变更，2、商品价格变更
        /// </summary>
        public string changed_type { get; set; }

        /// <summary>
        /// 商品变更后的状态：上架，下架
        /// </summary>
        public string product_sale_status { get; set; }

        /// <summary>
        /// 商品变更后的价格
        /// </summary>
        public string product_price { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }

    public class OrderCallBackInfo
    {
        /// <summary>
        /// 福禄开放平台订单号
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 交易完成时间，格式为：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string charge_finish_time { get; set; }
        /// <summary>
        /// 合作商家订单号
        /// </summary>
        public string customer_order_no { get; set; }
        /// <summary>
        /// 订单状态 success或failed
        /// </summary>
        public string order_status { get; set; }
        /// <summary>
        /// 充值描述
        /// </summary>
        public string recharge_description { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 交易单价
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public string buy_num { get; set; }
        /// <summary>
        /// 运营商流水号
        /// </summary>
        public string operator_serial_number { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }

    public class RefundCallBackInfo
    {
        /// <summary>
        /// 福禄开放平台订单号
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 合作商家订单号
        /// </summary>
        public string customer_order_no { get; set; }
        /// <summary>
        /// 退款时间，格式为：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string refund_time { get; set; }
        /// <summary>
        /// 退款状态
        /// </summary>
        public string refund_status { get; set; }
        /// <summary>
        /// 退款说明
        /// </summary>
        public string refund_description { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
}
