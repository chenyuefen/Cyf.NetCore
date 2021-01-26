/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：RedisLock
// 文件功能描述： redis分布式锁
//
// 创建者：冼晓松
// 创建时间：2020-03-17 09:30
// 
//----------------------------------------------------------------*/

using System;
using System.Threading;
using System.Threading.Tasks;
using TM.Infrastructure.CSRedis;

namespace TM.Infrastructure.Lock
{
    /// <summary>
    /// redis分布式锁
    /// </summary>
    public static class RedisLock
    {
        /// <summary>
        /// 锁名
        /// </summary>
        private static string LOCK_NAME = "LockForSetNx:";

        public static async Task<bool> TryLockAsync(string key, TimeSpan ts, double waitLockSeconds = 0)
        {
            var expireSeconds = (int)ts.TotalSeconds;
            return await TryLockAsync(key, expireSeconds, waitLockSeconds);
        }

        /// <summary>
        /// 读取分布式锁    执行代码前读取锁
        /// </summary>
        /// <param name="key">锁key</param>
        /// <param name="lockExpirySeconds">锁自动超时时间(秒)</param>
        /// <param name="waitLockSeconds">等待锁时间(秒)</param>
        /// <returns></returns>
        public static async Task<bool> TryLockAsync(string key, int lockExpirySeconds = 10, double waitLockSeconds = 0)
        {
            //间隔等待50毫秒
            int waitIntervalMs = 50;
            string lockKey = LOCK_NAME + key;

            DateTime begin = DateTime.Now;
            while (true)
            {
                //循环获取取锁
                if (await CsRedisManager.SetNxAsync(lockKey, 1))
                {
                    //设置锁的过期时间
                    await CsRedisManager.ExpireAsync(lockKey, lockExpirySeconds);
                    return true;
                }

                //不等待锁则返回
                if (waitLockSeconds == 0)
                    break;

                //超过等待时间，则不再等待
                if ((DateTime.Now - begin).TotalSeconds >= waitLockSeconds)
                    break;

                Thread.Sleep(waitIntervalMs);
            }
            return false;
        }

        /// <summary>
        /// 释放锁 执行代码后调用释放锁
        /// </summary>
        /// <param name="key"></param>
        public static async Task UnlockAsync(string key)
        {
            string lockKey = LOCK_NAME + key;
            await CsRedisManager.DelAsync(lockKey);
        }
    }
}
