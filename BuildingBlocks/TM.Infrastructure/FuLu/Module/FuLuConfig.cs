using System;
using System.Collections.Generic;
using System.Text;
using TM.Infrastructure.Configs;

namespace TM.Infrastructure.FuLu.Module
{
    public class FuLuConfig
    {
        public readonly static string AppKey = ConfigHelper.Configuration["LuFuSetting:AppKey"];

        public readonly static string AppSecret = ConfigHelper.Configuration["LuFuSetting:AppSecret"];
        /// <summary>
        /// 版本号
        /// </summary>

        public readonly static string Version = ConfigHelper.Configuration["LuFuSetting:Version"];
        /// <summary>
        /// 请求地址
        /// </summary>

        public readonly static string Url = ConfigHelper.Configuration["LuFuSetting:Url"];


        #region Method
        /// <summary>
        /// 获取商品列表
        /// </summary>
        public const string GoodsListGet = "fulu.goods.list.get";
        /// <summary>
        /// 获取商品信息接口
        /// </summary>
        public const string GoodsInfoGet = "fulu.goods.info.get";
        /// <summary>
        /// 直充下单接口
        /// </summary>
        public const string OrderDirectAdd = "fulu.order.direct.add";
        /// <summary>
        /// 卡密下单接口
        /// </summary>
        public const string OrderCardAdd = "fulu.order.card.add";
        /// <summary>
        /// 话费下单接口
        /// </summary>
        public const string OrderMobileAdd = "fulu.order.mobile.add";
        /// <summary>
        /// 订单查询接口
        /// </summary>
        public const string OrderInfoGet = "fulu.order.info.get";
        /// <summary>
        /// 对账单申请接口
        /// </summary>
        public const string OrderRecordGet = "fulu.order.record.get";
        /// <summary>
        /// 手机号归属地接口
        /// </summary>
        public const string MobileInfoGet= "fulu.mobile.info.get";
        #endregion
    }
}
