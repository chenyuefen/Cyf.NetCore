using System.Collections.Generic;
using TM.Core.Models.Entity;
using TM.Infrastructure.Configs;

namespace TM.Core.Configs
{
    /// <summary>
    /// redis配置
    /// </summary>
    public static class RedisConfig
    {
        public static RedisConfigEntity _redisConfig = ConfigHelper.GetAppSettings<RedisConfigEntity>("appsettings.json", "", "Redis");

        public static readonly string ConnectionString = _redisConfig.ConnectionString;
        public static readonly string DefaultCache = _redisConfig.DefaultCache;

        public static readonly string ORDER_CACHE_KEY = "order:";           //订单缓存
        public static readonly string STOCK_CACHE_KEY = "stock:";           //规格库存
        public static readonly string CATEGORY_CACHE_KEY = "category:";     //类目
        public static readonly string HOTWORD_CACHE_KEY = "hotword:";       //热搜词
        public static readonly string GALLERY_CACHE_KEY = "gallery";        //资源库分组
        public static readonly string LOGISTIC_CACHE_KEY = "logistic";      //物流
        public static readonly string KDNIAO_CACHE_KEY = "kdniao";			//快递鸟
        public static readonly string KDNIAO_CACHE_KEY_REFUND = "kdniaorefund";                     //退款单快递鸟
        public static readonly string PRODUCT_OPERATE_LOG_CACHE_KEY = "ProductOperateLog:";         //商品操作日志
        public static readonly string PRODUCT_OPERATE_LOG_BATCHCODE_CACHE_KEY = "BatchCode";        //商品操作批次
        public static readonly string Add_SALE_AMOUNT_CACHE_KEY = "addSaleAmount";                  //规格增加销量
        public static readonly string Add_SALE_AMOUNT_CACHE_KEY_GROUP = "addGroupSaleAmount";       //团购规格增加销量
        public static readonly string Deduct_SALE_AMOUNT_CACHE_KEY = "deductSaleAmount";            //规格返回销量
        public static readonly string Deduct_SALE_AMOUNT_CACHE_KEY_GROUP = "deductGroupSaleAmount"; //团购规格返回销量
        public static readonly string PRODUCT_COTENT_CACHE_KEY = "productContent";                  //商品操作内容
        //public static readonly string GIFIPACK_COUPONS_CACHE_KEY = "gifipack:coupons";              //礼包优惠券
        public static readonly string SHORT_VIDEO_KEY = "shortVideo:list";//短视频列表
        public static readonly string LIVE_ROOM_KEY = "liveRoom:list";//直播中房间列表
        public static readonly string ANCHOR_APPLY_IDCARD = "anchorIDCard";//主播申请暂存申请人身份证信息
        public static readonly string LIVE_RECORD_KEY = "liveRecord";//直播间上下滑浏览记录
        public static readonly string BANKCODE_CASH_KEY = "bankcode";//微信提现银行卡卡号
        public static readonly string CAPITAL_UNSETTLED_AMOUNT = "capitalunsettledamount:";  //账户资金余额 交易中金额
        public static readonly string SPULLIER_CAPITAL_UNSETTLED_AMOUNT = "suppliercapitalunsettledamount:";  //账户资金余额 交易中金额
        public static readonly string SHORT_VIDEO_RECORD_KEY = "shortVideoRecord:";//短视频浏览记录

        public static readonly string SERVICE_DESCRIPTION_KEY = "servicedescription:";     //服务说明
        public static readonly string BRAND_CACHE_KEY = "brand:";                  //品牌

        public static readonly string PAY_RECOMMOND_KEY = "spu:payrecommend:";        //支付成功推荐商品
        public static readonly string SELECTION_RECOMMOND_KEY = "spu:selection";        //支付成功推荐商品
        public static readonly string MEMBER_NEW_KEY = "spu:memberrecommend:new";     //会员中心:最新
        public static readonly string MEMBER_SALE_KEY = "spu:memberrecommend:sale";   //会员中心:销量
        public static readonly string MEMBER_SHARE_KEY = "spu:memberrecommend:share"; //会员中心:分享

        public static readonly string COUPON_FIXED = "Coupon:couponfixed";//固定优惠券

        //public const string SpuFliterList = "SpuFliter:List";//组合商品
        public const string SpuCouponList = "Coupon:SpuCouponLists";//商品详情页优惠券

        public const string SpuReview = "SpuReview:List";//商品详情页评论


        public const string GiftPackList = "Spu:GiftPack";
        public const string CombinedList = "Spu:Combined";
        public const string PickUpList = "Spu:PickUp";
        public const string FreeActivityList = "Spu:FreeActivity";
        public const string GroupActivityList = "Spu:GroupActivity";


        public static string GoodsActivityList(string ActivityType) => $"Spu:{ActivityType}";
        public static string PickUpSectionTime(long ActivityId) => $"PickUpSectionTime:{ActivityId}";
        public const string ActivitySkuInventory = "ActivitySkuInventory";    //商品活动规格库存
        public const string ActivitySkuOrder = "ActivitySkuOrder";

        public static string PartnerStoreAddress(string city) => $"PartnerStoreAddredd:{city}";

        public static string PartnerStoreWholeAddress(string wholeAdress) =>
            $"PartnerStoreAddredd:WholeAddress:{wholeAdress}";


        public static string FrontendCategory = "FrontendCategory"; //前端分类组件缓存key

        public static string FrontendCategorySpu(long fontendCategoryId) => $"FrontendCategorySpu:{fontendCategoryId}"; //前端分类组件商品缓存key
    }
}
