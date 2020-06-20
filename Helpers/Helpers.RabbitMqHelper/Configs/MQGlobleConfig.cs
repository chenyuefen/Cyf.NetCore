using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.RabbitMqHelper
{
    /// <summary>
    /// mq默认配置
    /// </summary>
    public static class MqGlobleConfig
    {
        /// <summary>
        /// 服务质量保证指定最大的unacked messages数目
        /// </summary>
        public static uint PrefetchSize { get; } = 0;
        /// <summary>
        /// 在no_ask=false的情况下生效，即在自动应答的情况下这两个值是不生效的
        /// </summary>
        public static ushort PrefetchCount { get; set; } = 1;
        /// <summary>
        /// true\false 是否将上面设置应用于channel，简单点说，就是上面限制是channel级别的还是consumer级别
        /// </summary>
        public static bool Global { get; } = false;
        /// <summary>
        /// 自动应答
        /// </summary>
        public static bool AutoAck { get; set; } = false;
        /// <summary>
        /// 是否持久化
        /// </summary>
        public static bool Durable { get; set; } = true;
        public static MqConfig DefaultMQConfig { get; private set; }
        /// <summary>
        /// 默认虚拟机
        /// </summary>
        public static string VHost { get; private set; }
        /// <summary>
        /// 默认交换机
        /// </summary>
        public static string Exchange { get; private set; }
        public static string UserName => DefaultMQConfig.UserName;
        public static string Password => DefaultMQConfig.Password;
        /// <summary>
        /// 初始化mq
        /// </summary>
        /// <param name="configuration"></param>
        public static void InitDefaultConnection(IConfiguration configuration) => InitDefaultConnection(configuration.GetSection("mq").Get<MqConfig>());
        public static void InitDefaultConnection(MqConfig mQConfig)
        {
            DefaultMQConfig = mQConfig;
            VHost = mQConfig.vHost;
            Exchange = mQConfig.ExchangeName;
            PrefetchCount = mQConfig.PrefetchCount;
        }
        public static void InitDefaultConnection(MqConfig mQConfig, string vHost, string exchange)
        {
            DefaultMQConfig = mQConfig;
            VHost = vHost;
            Exchange = exchange;
            PrefetchCount = mQConfig.PrefetchCount;
        }
    }
}
