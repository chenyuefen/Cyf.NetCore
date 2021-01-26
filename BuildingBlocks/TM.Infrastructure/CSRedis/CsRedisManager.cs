using CSRedis;
using System;
using System.Threading.Tasks;
using TM.Infrastructure.Configs;

namespace TM.Infrastructure.CSRedis
{
    /// <summary>
    /// CsRedis缓存服务
    /// 继承CsRedis官方提供的服务，这里只是对一些官方接口做了一下拓展，使用静态方式并初始化Redis服务器
    /// </summary>
    public abstract partial class CsRedisManager : RedisHelper
    {
        private static CSRedisClient _redisManager;
        static CsRedisManager()
        {
            _redisManager = new CSRedisClient(ConfigHelper.GetJsonConfig("appsettings.json").GetSection("RedisConnection").Value);      //Redis的连接字符串
            Initialization(_redisManager);
        }

        //https://www.cnblogs.com/additwujiahua/p/11479303.html

        /// <summary>
        /// 将一个或多个值插入到列表头部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool LPush(string key, string value)
        {
            try
            {
                //从头部插入 
                _redisManager.LPush(key, value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        
        public static new async Task<T> CacheShellAsync<T>(string key,int timeout, Func<Task<T>> func)
        {
            try
            {
                return await _redisManager.CacheShellAsync(key, timeout, func);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static new async Task<T> CacheShellAsync<T>(string key, TimeSpan timeout, Func<Task<T>> func)
        {
            try
            {
                return await _redisManager.CacheShellAsync(key, (int)timeout.TotalSeconds, func);
            }
            catch (Exception e)
            {
                return default;
            }
        }


    }
}
