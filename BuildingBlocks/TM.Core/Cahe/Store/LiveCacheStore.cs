using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Core.Cache;
using TM.Core.Cahe.Dto;
using TM.Core.Cahe.Store.Abstractions;
using TM.Infrastructure.Cache.Abstractions;

namespace TM.Core.Cahe.Store
{
    public partial class LiveCacheStore : ILiveCacheStore
    {
        private readonly ICache _cache;

        public LiveCacheStore(
             ICache cache)
        {
            _cache = cache;
        }

        #region 初始化并返回最终数据对象
        /// <summary>
        /// 初始化并返回最终数据对象
        /// </summary>
        /// <typeparam name="T">数据缓存对象</typeparam>
        /// <param name="keyType">key类型 1:im 2:thumbs</param>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<T> InitCacheOutDto<T>(int keyType, string key)
        {
            var cacheKey = GetCacheKey(keyType, key);
            //获取数据缓存对象
            var cacheModel = await GetCacheOutDtoAsync<T>(keyType, key);
            //初始化数据缓存对象
            var newCacheModel = GetInitDto(keyType);
            await AddCacheAsync(keyType, key, newCacheModel);
            return cacheModel;
        }
        #endregion

        #region 获取缓存数据
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">数据缓存对象</typeparam>
        /// <param name="keyType">key类型 1:im 2:thumbs</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetCacheOutDtoAsync<T>(int keyType, string key)
        {
            var cacheKey = GetCacheKey(keyType, key);
            //获取数据缓存对象
            var json = await _cache.GetAsync<string>(cacheKey);
            if (json == null)
            {
                //初始化数据缓存对象
                var newCacheModel = GetInitDto(keyType);
                await AddCacheAsync(keyType, key, newCacheModel);
                json = await _cache.GetAsync<string>(cacheKey);
            }
            var cacheModel = JsonConvert.DeserializeObject<T>(json);
            return cacheModel;
        }
        #endregion

        #region 添加缓存。如果已存在缓存，将覆盖
        public async Task AddCacheAsync<T>(int keyType, string key, T value)
        {
            var cacheKey = GetCacheKey(keyType, key);
            await _cache.AddAsync(cacheKey, JsonConvert.SerializeObject(value), DateTime.Now.AddDays(7).Subtract(DateTime.Now));

        }
        #endregion

        #region 添加缓存。如果已存在缓存，将覆盖,自定义过期时间
        public async Task AddCacheAsync<T>(int keyType, string key, T value, TimeSpan timeSpan)
        {
            var cacheKey = GetCacheKey(keyType, key);
            await _cache.AddAsync(cacheKey, JsonConvert.SerializeObject(value), timeSpan);
        }
        #endregion

        #region 判断缓存是否存在
        public async Task<bool> IsExistAsync(int keyType, string key)
        {
            var cacheKey = GetCacheKey(keyType, key);
            //获取数据缓存对象
            var json = await _cache.GetAsync<string>(cacheKey);
            return json != null;
        }
        #endregion

        #region 删除
        public async Task DelCacheAsync(int keyType, string key)
        {
            var cacheKey = GetCacheKey(keyType, key);
            await _cache.RemoveAsync(cacheKey);
        }
        #endregion

        #region 返回缓存key
        public string GetCacheKey(int keyType, string key)
        {
            var cacheKey = "";
            switch (keyType)
            {
                case (int)CacheKeyType.IM:
                    cacheKey = CacheKeyConfig.GetIMGroupKey(key); break;
                case (int)CacheKeyType.ThumbsUp:
                    cacheKey = CacheKeyConfig.GetThumbsUpKey(key); break;
                case (int)CacheKeyType.Seckill:
                    cacheKey = CacheKeyConfig.GetSeckillStockKey(key); break;
                case (int)CacheKeyType.SeckillWithHold:
                    cacheKey = CacheKeyConfig.GetSeckillWithHoldKey(key); break;
                case (int)CacheKeyType.SkuLockQuantity:
                    cacheKey = CacheKeyConfig.GetSkuLockQuantityKey(key); break;
                case (int)CacheKeyType.NoticeSpu:
                    cacheKey = CacheKeyConfig.GetNoticeSpuKey(key); break;
                case (int)CacheKeyType.StorePaymentFlow:
                    cacheKey = CacheKeyConfig.GetStorePaymentFlowKey(key); break;
                case (int)CacheKeyType.BsGenTokenKey:
                    cacheKey = CacheKeyConfig.GetBsGenTokenKey(key); break;
                case (int)CacheKeyType.BsTokenKey:
                    cacheKey = CacheKeyConfig.GetBsTokenKey(key); break;
                case (int)CacheKeyType.SVThumbsUp:
                    cacheKey = CacheKeyConfig.GetShortVideoThunbsUpKey(key); break;
                case (int)CacheKeyType.SVPlayCount:
                    cacheKey = CacheKeyConfig.GetShortVideoPlayCountKey(key); break;
                case (int)CacheKeyType.SVCThumbsUp:
                    cacheKey = CacheKeyConfig.GetShortVideoCommentThumbsUp(key); break;
                case (int)CacheKeyType.SVCCount:
                    cacheKey = CacheKeyConfig.GetShortVideoCommentCount(key); break;
                default:
                    break;
            }

            return cacheKey;
        }
        #endregion

        #region 返回初始化缓存数据对象
        public object GetInitDto(int keyType)
        {
            object ob = new object();
            switch (keyType)
            {
                case (int)CacheKeyType.IM:
                    ob = new IMGroupCacheDto() { OnlineUserCount = 0 }; break;
                case (int)CacheKeyType.ThumbsUp:
                    ob = new ThumbsUpCacheDto() { ThumbsUpCount = 0 }; break;
                case (int)CacheKeyType.Seckill:
                    ob = new SeckillCacheDto() { }; break;
                case (int)CacheKeyType.SeckillWithHold:
                    ob = new SeckillOrderWithHoldStockDto() { }; break;
                case (int)CacheKeyType.SkuLockQuantity:
                    ob = new SkuLockQuantityCacheDto() { }; break;
                case (int)CacheKeyType.NoticeSpu:
                    ob = new List<NoticeSpuCacheDto>() { }; break;
                case (int)CacheKeyType.StorePaymentFlow:
                    ob = new List<StorePaymentCacheDto>() { }; break;
                case (int)CacheKeyType.BsGenTokenKey:
                    ob = new List<BsGenUserTokenCacheDto>() { }; break;
                case (int)CacheKeyType.BsTokenKey:
                    ob = new List<BsUserTokenCacheDto>() { }; break;
                case (int)CacheKeyType.SVThumbsUp:
                    ob = new List<ShortVideoThunbsUpCache>() { }; break;
                case (int)CacheKeyType.SVPlayCount:
                    ob = new List<ShortVideoPlayCountCache>() { }; break;
                default:
                    break;
            }
            return ob;
        }
        #endregion
    }

    public enum CacheKeyType
    {
        //im
        IM = 1,
        //点赞
        ThumbsUp = 2,
        //秒杀库存
        Seckill = 3,
        //秒杀预扣==>对应订单
        SeckillWithHold = 4,
        //商品规格锁定库存
        SkuLockQuantity = 5,
        //预告商品创建
        NoticeSpu = 6,
        //小店缴费流水
        StorePaymentFlow = 7,
        //总后台登陆
        BsGenTokenKey = 8,
        //小店后台登陆
        BsTokenKey = 9,
        //短视频点赞
        SVThumbsUp = 10,
        //短视频播放
        SVPlayCount = 11,
        //短视频评论点赞
        SVCThumbsUp = 12,
        //短视频评论数
        SVCCount = 13
    }
}
