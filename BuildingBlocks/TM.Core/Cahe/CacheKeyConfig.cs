namespace TM.Core.Cache
{
    /// <summary>
    /// 缓存标记
    /// </summary>
    public static class CacheKeyConfig
    {
        /// <summary>
        /// 获取直播聊天室缓存键
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static string GetIMGroupKey(string groupId) => $"Live:IM:{groupId}";

        /// <summary>
        /// 获取直播聊天室点赞缓存键
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static string GetThumbsUpKey(string groupId) => $"Live:ThumbsUp:{groupId}";

        /// <summary>
        /// 秒杀库存缓存键
        /// </summary>
        /// <param name="secikllId"></param>
        /// <returns></returns>
        public static string GetSeckillStockKey(string secikllId) => $"Live:Seckill:{secikllId}";

        /// <summary>
        /// 秒杀订单预扣缓存键
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static string GetSeckillWithHoldKey(string orderId) => $"Live:WithHold:{orderId}";

        /// <summary>
        /// 商品规格锁定库存
        /// </summary>
        /// <param name="skuId"></param>
        /// <returns></returns>
        public static string GetSkuLockQuantityKey(string skuId) => $"Live:LockQuantity:{skuId}";

        /// <summary>
        /// 创建直播预告商品缓存键
        /// </summary>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        public static string GetNoticeSpuKey(string noticeId) => $"Live:Notice:{noticeId}";

        /// <summary>
        /// 小店缴费流水缓存键
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public static string GetStorePaymentFlowKey(string flowId) => $"Live:StorePayment:{flowId}";

        /// <summary>
        /// 总后台登陆
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetBsGenTokenKey(string id) => $"Live:BsGenToken:{id}";

        /// <summary>
        /// 小店后台登陆
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetBsTokenKey(string id) => $"Live:BsToken:{id}";

        /// <summary>
        /// 短视频点赞
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetShortVideoThunbsUpKey(string id) => $"Live:ShortVideo:ThumbsUp:{id}";

        /// <summary>
        /// 短视频播放量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetShortVideoPlayCountKey(string id) => $"Live:ShortVideo:PlayCount:{id}";

        /// <summary>
        /// 短视频评论点赞
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetShortVideoCommentThumbsUp(string id) => $"Live:ShortVideo:CommentThumbsUp:{id}";

        /// <summary>
        /// 短视频评论数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetShortVideoCommentCount(string id) => $"Live:ShortVideo:CommentCount:{id}";

        public const string ADDRESS_AREA = "Area:AreaList";             //所有地区
        public const string ADDRESS_DOMAIN = "Area:DomainList";         //区域
        public const string ADDRESS_PROVINCE = "Area:ProvinceList";     //省
        public const string ADDRESS_CITY = "Area:CityList";             //市
        public const string ADDRESS_DISTRICT = "Area:DistrictList";     //区
        public const string ADDRESS_STREET = "Area:StreetList";         //街道

    }
}
