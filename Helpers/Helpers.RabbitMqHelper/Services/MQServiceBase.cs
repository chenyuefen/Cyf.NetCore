using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.RabbitMqHelper
{
    public abstract class MqServiceBase : IService
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="message"></param>
        public abstract Task OnReceived(MessageBody message);
        /// <summary>
        /// 队列名称
        /// </summary>
        public abstract string QueueName { get; }

        public List<MqConsumerChannel> Channels { get; } = new List<MqConsumerChannel>();

        /// <summary>
        ///  消息队列配置
        /// </summary>
        public MqConfig Config { get; set; }

        ///// <summary>
        /////  消息队列中定义的虚拟机
        ///// </summary>
        //public string vHost { get; set; } = MqGlobleConfig.VHost;

        /// <summary>
        ///  消息队列中定义的交换机
        /// </summary>
        public string ExchangeName { get; set; } = MqGlobleConfig.Exchange;

        /// <summary>
        ///  定义的队列列表
        /// </summary>
        protected List<QueueInfo> Queues { get; } = new List<QueueInfo>();

        /// <summary>
        /// 是否开始标记
        /// </summary>
        private bool _started = false;

        public MqServiceBase() : this(MqGlobleConfig.DefaultMQConfig)
        {

        }

        public MqServiceBase(MqConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        //public MqConsumerChannel CreateChannel(string queue, string routeKey, string exchangeType)
        //{
        //    MqConnection conn = new MqConnection(Config, vHost);
        //    MqChannelManager cm = new MqChannelManager(conn);
        //    MqConsumerChannel channel = cm.CreateReceiveChannel(exchangeType, ExchangeName, queue, routeKey);
        //    return channel;
        //}

        /// <summary>
        ///  启动订阅
        /// </summary>
        public void Start()
        {
            if (!_started)
            {
                if (!Queues.Exists(x => x.QueueName == QueueName)) Queues.Add(new QueueInfo(QueueName, OnReceived));

                var manager = new MqChannelManager(Config);
                foreach (var item in Queues)
                {
                    MqConsumerChannel channel = manager.CreateReceiveChannel(item.ExchangeType, this.ExchangeName, item.QueueName, item.RouterKey, item.OnReceived);
                    this.Channels.Add(channel);
                }
                _started = true;
                Log.Information("mq服务开始");
            }
        }

        /// <summary>
        ///  停止订阅
        /// </summary>
        public void Stop()
        {
            if (_started)
            {
                foreach (var c in Channels)
                {
                    c.Stop();
                }
                Channels.Clear();
                _started = false;
                Log.Information("mq服务结束");
            }
        }

    }
}
