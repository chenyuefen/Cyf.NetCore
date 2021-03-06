﻿using Zhaoxi.AspnetCore.Cache.Redis.Interface;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.AspnetCore.Cache.Redis.Interface
{
    /// <summary>
    ///  Redis list的实现为一个双向链表，即可以支持反向查找和遍历，更方便操作，不过带来了部分额外的内存开销，
    ///  Redis内部的很多实现，包括发送缓冲队列等也都是用的这个数据结构。  
    /// </summary>
    interface IRedisListService  
    {
        #region 赋值
        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        void LPush(string key, string value);
        /// <summary>
        /// 从左侧向list中添加值，并设置过期时间
        /// </summary>
        void LPush(string key, string value, DateTime dt);
        /// <summary>
        /// 从左侧向list中添加值，设置过期时间
        /// </summary>
        void LPush(string key, string value, TimeSpan sp);
        /// <summary>
        /// 从右侧向list中添加值
        /// </summary>
        void RPush(string key, string value);
        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>    
        void RPush(string key, string value, DateTime dt);
        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>        
        void RPush(string key, string value, TimeSpan sp);
        /// <summary>
        /// 添加key/value
        /// </summary>     
        void Add(string key, string value);
        /// <summary>
        /// 添加key/value ,并设置过期时间
        /// </summary>  
        void Add(string key, string value, DateTime dt);
        /// <summary>
        /// 添加key/value。并添加过期时间
        /// </summary>  
        void Add(string key, string value, TimeSpan sp);
        /// <summary>
        /// 为key添加多个值
        /// </summary>  
        void Add(string key, List<string> values);
        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        void Add(string key, List<string> values, DateTime dt);
        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        void Add(string key, List<string> values, TimeSpan sp);
        #endregion

        #region 获取值
        /// <summary>
        /// 获取list中key包含的数据数量
        /// </summary>  
        long Count(string key);
        /// <summary>
        /// 获取key包含的所有数据集合
        /// </summary>  
        List<string> Get(string key);
        /// <summary>
        /// 获取key中下标为star到end的值集合 
        /// </summary>  
        List<string> Get(string key, int star, int end);
        #endregion

        #region 阻塞命令
        /// <summary>
        ///  阻塞命令：从list为key的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        string BlockingPopItemFromList(string key, TimeSpan? sp);
        /// <summary>
        ///  阻塞命令：从多个list中尾部移除一个值,并返回移除的值&key，阻塞时间为sp
        /// </summary>  
        ItemRef BlockingPopItemFromLists(string[] keys, TimeSpan? sp);


        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        string BlockingDequeueItemFromList(string key, TimeSpan? sp);

        /// <summary>
        /// 阻塞命令：从多个list中尾部移除一个值，并返回移除的值&key，阻塞时间为sp
        /// </summary>  
        ItemRef BlockingDequeueItemFromLists(string[] keys, TimeSpan? sp);

        /// <summary>
        /// 阻塞命令：从list中一个fromkey的尾部移除一个值，添加到另外一个tokey的头部，并返回移除的值，阻塞时间为sp
        /// </summary>  
        string BlockingPopAndPushItemBetweenLists(string fromkey, string tokey, TimeSpan? sp);
        #endregion

        #region 删除
        /// <summary>
        /// 从尾部移除数据，返回移除的数据
        /// </summary>  
        string PopItemFromList(string key);
        /// <summary>
        /// 从尾部移除数据，返回移除的数据
        /// </summary>  
        string DequeueItemFromList(string key);

        /// <summary>
        /// 移除list中，key/value,与参数相同的值，并返回移除的数量
        /// </summary>  
        long RemoveItemFromList(string key, string value);
        /// <summary>
        /// 从list的尾部移除一个数据，返回移除的数据
        /// </summary>  
        string RemoveEndFromList(string key);
        /// <summary>
        /// 从list的头部移除一个数据，返回移除的值
        /// </summary>  
        string RemoveStartFromList(string key);
        #endregion

        #region 其它
        /// <summary>
        /// 从一个list的尾部移除一个数据，添加到另外一个list的头部，并返回移动的值
        /// </summary>  
        string PopAndPushItemBetweenLists(string fromKey, string toKey);
        /// <summary>
        /// 清理数据，保持list长度
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">起点</param>
        /// <param name="end">终结点</param>
        void TrimList(string key, int start, int end);

        #endregion

        #region 发布订阅
        void Publish(string channel, string message);

        void Subscribe(string channel, Action<string, string, IRedisSubscription> actionOnMessage);
        #endregion
    }
}
