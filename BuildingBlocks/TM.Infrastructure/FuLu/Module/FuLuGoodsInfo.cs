using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Infrastructure.FuLu.Module
{
    public class FuLuGoodsInfo
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public long product_id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string four_category_icon { get; set; }
        /// <summary>
        /// 面值
        /// </summary>
        public double face_value { get; set; }
        /// <summary>
        /// 库存类型：卡密、直充
        /// </summary>
        public string product_type { get; set; }
        /// <summary>
        /// 单价（单位：元）
        /// </summary>
        public double purchase_price { get; set; }
        /// <summary>
        /// 商品模板Id，可能为空
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 库存状态：断货、警报、充足
        /// </summary>
        public string stock_status { get; set; }
        /// <summary>
        /// 销售状态：下架、上架、维护中、库存维护（本接口只取上架状态的商品）
        /// </summary>
        public string sales_status { get; set; }
        /// <summary>
        /// 商品详情
        /// </summary>
        public string details { get; set; }

    }
}
