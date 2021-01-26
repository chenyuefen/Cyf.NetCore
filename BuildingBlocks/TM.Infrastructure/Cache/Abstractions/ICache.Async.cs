using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TM.Infrastructure.Cache.Abstractions
{
    /// <summary>
    /// 缓存 - 异步
    /// </summary>
    public partial interface ICache
    {
        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        /// <param name="expiration">过期时间间隔</param>
        Task<T> GetAsync<T>(string key, Func<Task<T>> func, TimeSpan? expiration = null);

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="type">缓存数据类型</param>
        Task<object> GetAsync(string key, Type type);

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        Task<bool> TryAddAsync<T>(string key, T value, TimeSpan? expiration = null);

        /// <summary>
        /// 添加缓存。如果已存在缓存，将覆盖
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        Task AddAsync<T>(string key, T value, TimeSpan? expiration = null);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        Task RemoveAsync(string key);

        /// <summary>
        /// 通过缓存键前缀移除缓存
        /// </summary>
        /// <param name="prefix">缓存键前缀</param>
        Task RemoveByPrefixAsync(string prefix);

        /// <summary>
        /// 清空缓存
        /// </summary>
        Task ClearAsync();

        /// <summary>
        /// 设置键过期时间
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="second">秒数</param>
        /// <returns></returns>
        Task<bool> KeyExpireAsync(string cacheKey, int second);



        /// <summary>
        /// Hash 获取指定字段的值
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        Task<string> HGetAsync(string cacheKey, string field);

        /// <summary>
        /// Hash 获取指定字段的值
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        Task<T> HGetAsync<T>(string cacheKey, string field);

        /// <summary>
        /// Hash 覆盖设置指定键字段值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="field"></param>
        /// <param name="cacheValue"></param>
        /// <returns></returns>
        Task<bool> HSetAsync(string cacheKey, string field, string cacheValue);

        /// <summary>
        /// Hash 增加指定字段的数值
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="field">值</param>
        /// <param name="val">增加数量</param>
        /// <returns>增值操作执行后的该字段的数值</returns>
        Task<long> HIncrByAsync(string cacheKey, string field, long val = 1);

        /// <summary>
        /// Hash 删除一个或多个指定域
        /// O(N)， N为要删除的域的数量
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="fields">删除字段列表</param>
        Task<long> HDelAsync(string cacheKey, IList<string> fields = null);

        /// <summary>
        /// Hash 获取field的数量
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        Task<long> HLenAsync(string cacheKey);
    }
}
