 
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.AspnetCore.Cache.Redis.Init;

namespace Zhaoxi.AspnetCore.Cache.Redis.Interface
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisBase : IDisposable
    {
        /// <summary>
        /// Redis连接；
        /// </summary>
        public IRedisClient iClient { get; private set; }
        /// <summary>
        /// 构造时完成链接的打开
        /// </summary>
        public RedisBase()
        {
            iClient = RedisManager.GetClient();
        }

        //public static IRedisClient iClient { get; private set; }
        //static RedisBase()
        //{
        //    iClient = RedisManager.GetClient();
        //}


        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    iClient.Dispose();
                    iClient = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Transcation()
        {
            using (IRedisTransaction irt = this.iClient.CreateTransaction())
            {
                try
                {
                    irt.QueueCommand(r => r.Set("key", 20));
                    irt.QueueCommand(r => r.Increment("key", 1));
                    irt.Commit(); // 提交事务
                }
                catch (Exception ex)
                {
                    irt.Rollback();
                    throw ex;
                }
            }
        }


        /// <summary>
        /// 清除全部数据 请小心
        /// </summary>
        public virtual void FlushAll()
        {
            iClient.FlushAll();
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            iClient.Save();//阻塞式save
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            iClient.SaveAsync();//异步save
        }

        public bool Lock(string key, int expirySeconds = 5, double waitSeconds = 0)
        {
            int waitIntervalMs = 50;//间隔等待时长 毫秒 合理配置一下 应该也会影响性能 间隔时间太长肯定是不行的
            string lockKey = "lock_key:" + key;

            DateTime begin = DateTime.Now;
            while (true)
            {
                if (((RedisClient)iClient).SetNX(lockKey, new byte[] { 1 }) == 1)
                {
                    ((RedisClient)iClient).Expire(lockKey, expirySeconds);
                    return true;
                }

                //不等待锁则返回
                if (waitSeconds <= 0)
                    break;

                if ((DateTime.Now - begin).TotalSeconds >= waitSeconds)//等待超时
                    break;

                System.Threading.Thread.Sleep(waitIntervalMs);
            }
            return false;
        }

        public long UnLock(string key)
        {

            string lockKey = "lock_key:" + key;
            return ((RedisClient)iClient).Del(lockKey);
        }
    }
}
