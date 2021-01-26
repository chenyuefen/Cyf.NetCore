using EasyCaching.Core;
using System;
using TM.Infrastructure.Cache.Abstractions;

namespace TM.Infrastructure.Cache.EasyCaching
{
    /// <summary>
    /// EasyCaching缓存服务
    /// </summary>
    public partial class EasyCacheManager : ICache
    {
        /// <summary>
        /// EasyCaching缓存提供器
        /// </summary>
        private readonly IEasyCachingProvider _provider;

        /// <summary>
        /// RedisCaching缓存提供器
        /// </summary>
        private readonly IRedisCachingProvider _redisCachingProvider;

        /// <summary>
        /// 初始化一个<see cref="CacheManager"/>类型的实例
        /// </summary>
        /// <param name="provider">EasyCaching缓存提供器</param>
        public EasyCacheManager(IEasyCachingProvider provider, IRedisCachingProvider redisCachingProvider)
        {
            _provider = provider;
            _redisCachingProvider = redisCachingProvider;
        }

        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public bool Exists(string key)
        {
            return _provider.Exists(key);
        }

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        /// <param name="expiration">过期时间间隔</param>
        public T Get<T>(string key, Func<T> func, TimeSpan? expiration = null)
        {
            var result = _provider.Get(key, func, GetExpiration(expiration));
            return result.Value;
        }

        /// <summary>
        /// 获取过期时间间隔
        /// </summary>
        private TimeSpan GetExpiration(TimeSpan? expiration)
        {
            expiration = expiration ?? TimeSpan.FromHours(12);
            return expiration ?? default(TimeSpan);
        }
        /// <summary>
        /// 获取过期时间间隔
        /// </summary>
        /// <param name="cacheTime">分钟</param>
        /// <returns></returns>
        private TimeSpan GetExpiration(int? cacheTime)
        {
            return cacheTime.HasValue ? TimeSpan.FromMinutes(cacheTime.Value) : TimeSpan.FromHours(12);
        }

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        public bool Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            return _provider.TrySet(key, value, GetExpiration(expiration));
        }

        /// <summary>
        /// 添加缓存。如果已存在缓存，将覆盖
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        public void Add<T>(string key, T value, TimeSpan? expiration = null) => _provider.Set(key, value, GetExpiration(expiration));

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public void Remove(string key)
        {
            _provider.Remove(key);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            _provider.Flush();
        }

        public T Get<T>(string key, Func<T> func, int? cacheTime = null)
        {
            var result = _provider.Get(key, func, GetExpiration(cacheTime));
            return result.Value;
        }

        public T Get<T>(string key)
        {
            var result = _provider.Get<T>(key);
            return result.Value;
        }

    }
}
