using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Infrastructure.FuLu.Module
{
    public class CardsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string card_number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string card_pwd { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public string card_deadline { get; set; }
    }

    public class FuLuOrderInfo
    {
        /// <summary>
        /// 充值区（中文）,仅网游直充订单返回
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int buy_num { get; set; }
        /// <summary>
        /// 卡密信息，仅卡密订单返回（注意：卡密是密文，需要进行解密使用），
        /// 当解密出来的卡号为“”或“无卡号”时，则以卡密为准，否则以卡号+密码为准；
        /// </summary>
        public List<CardsItem> cards { get; set; }
        /// <summary>
        /// 充值账号
        /// </summary>
        public string charge_account { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 外部订单号，每次请求必须唯一
        /// </summary>
        public string customer_order_no { get; set; }
        /// <summary>
        /// 订单完成时间
        /// </summary>
        public string finish_time { get; set; }
        /// <summary>
        /// 运营商流水号
        /// </summary>
        public string operator_serial_number { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 交易单价（单位：元）
        /// </summary>
        public double order_price { get; set; }
        /// <summary>
        /// 订单状态： （success：成功，processing：处理中，failed：失败，untreated：未处理）
        /// </summary>
        public string order_state { get; set; }
        /// <summary>
        /// 订单类型：1-话费 2-流量 3-卡密 4-直充
        /// </summary>
        public int order_type { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public int product_id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 充值服（中文）,仅网游直充订单返回
        /// </summary>
        public string server { get; set; }
        /// <summary>
        /// 计费方式（中文）,仅网游直充订单返回
        /// </summary>
        public string type { get; set; }
    }
}
