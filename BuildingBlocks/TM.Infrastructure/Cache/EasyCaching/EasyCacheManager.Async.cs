using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Infrastructure.Cache.Abstractions;
using TM.Infrastructure.Json;

namespace TM.Infrastructure.Cache.EasyCaching
{
    /// <summary>
    /// EasyCaching缓存管理 - 异步操作
    /// </summary>
    public partial class EasyCacheManager : ICache
    {
        #region----Key/String----

        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task<bool> ExistsAsync(string key) => await _provider.ExistsAsync(key);

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        /// <param name="expiration">过期时间间隔</param>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> func, TimeSpan? expiration = null)
        {
            var result = await _provider.GetAsync(key, func, GetExpiration(expiration));
            return result.Value;
        }

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="type">缓存数据类型</param>
        public async Task<object> GetAsync(string key, Type type)
        {
            return await _provider.GetAsync(key, type);
        }

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        public async Task<T> GetAsync<T>(string key)
        {
            var result = await _provider.GetAsync<T>(key);
            return result.Value;
        }

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        public async Task<bool> TryAddAsync<T>(string key, T value, TimeSpan? expiration = null) => await _provider.TrySetAsync(key, value, GetExpiration(expiration));

        /// <summary>
        /// 添加缓存。如果已存在缓存，将覆盖
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        public async Task AddAsync<T>(string key, T value, TimeSpan? expiration = null) => await _provider.SetAsync(key, value, GetExpiration(expiration));

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task RemoveAsync(string key) => await _provider.RemoveAsync(key);

        /// <summary>
        /// 通过缓存键前缀移除缓存
        /// </summary>
        /// <param name="prefix">缓存键前缀</param>
        public async Task RemoveByPrefixAsync(string prefix) => await _provider.RemoveByPrefixAsync(prefix);

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        public async Task ClearAsync() => await _provider.FlushAsync();

        /// <summary>
        /// 设置键过期时间
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="second">秒数</param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string cacheKey, int second) => await _redisCachingProvider.KeyExpireAsync(cacheKey, second);

        #endregion

        #region----Hash----

        /// <summary>
        /// Hash 获取指定字段的值
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public async Task<string> HGetAsync(string cacheKey, string field)
        {
            return await _redisCachingProvider.HGetAsync(cacheKey, field);
        }

        /// <summary>
        /// Hash 获取指定字段的值
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public async Task<T> HGetAsync<T>(string cacheKey, string field)
        {
            var result = await _redisCachingProvider.HGetAsync(cacheKey, field);
            return string.IsNullOrWhiteSpace(result) ? default(T) : result.ToObject<T>();
        }

        /// <summary>
        /// Hash 覆盖设置指定键字段值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="field"></param>
        /// <param name="cacheValue"></param>
        /// <returns></returns>
        public async Task<bool> HSetAsync(string cacheKey, string field, string cacheValue) => await _redisCachingProvider.HSetAsync(cacheKey, field, cacheValue);

        /// <summary>
        /// Hash 自增/自减指定字段的数值
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="field">字段</param>
        /// <param name="val">+增加/-减少数量</param>
        /// <returns></returns>
        public async Task<long> HIncrByAsync(string cacheKey, string field, long val = 1) => await _redisCachingProvider.HIncrByAsync(cacheKey, field, val);

        /// <summary>
        /// Hash 删除一个或多个指定域
        /// O(N)， N为要删除的域的数量
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="fields">删除字段列表</param>
        public async Task<long> HDelAsync(string cacheKey, IList<string> fields = null) => await _redisCachingProvider.HDelAsync(cacheKey, fields);

        /// <summary>
        /// Hash 获取field的数量
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public async Task<long> HLenAsync(string cacheKey) => await _redisCachingProvider.HLenAsync(cacheKey);

        #endregion

    }
}
