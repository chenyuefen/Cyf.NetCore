﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.RabbitMqHelper
{
    public interface IService
    {
        ///// <summary>
        /////  创建通道
        ///// </summary>
        ///// <param name="queue">队列名称</param>
        ///// <param name="routeKey">路由名称</param>
        ///// <param name="exchangeType">交换机类型</param>
        ///// <returns></returns>
        //MqConsumerChannel CreateChannel(string queue, string routeKey, string exchangeType);

        /// <summary>
        ///  开启订阅
        /// </summary>
        void Start();

        /// <summary>
        ///  停止订阅
        /// </summary>
        void Stop();

        /// <summary>
        ///  通道列表
        /// </summary>
        List<MqConsumerChannel> Channels { get; }

        ///// <summary>
        /////  消息队列中定义的虚拟机
        ///// </summary>
        //string vHost { get; set; }

        /// <summary>
        ///  消息队列中定义的交换机
        /// </summary>
        string ExchangeName { get; set; }
    }
}
