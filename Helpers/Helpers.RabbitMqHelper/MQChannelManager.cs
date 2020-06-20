using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.RabbitMqHelper
{
    public class MqChannelManager
    {
        private readonly MqConnection _mqConnection;

        public MqChannelManager(MqConfig config) : this(new MqConnection(config))
        {
        }

        public MqChannelManager(MqConnection conn)
        {
            _mqConnection = conn;
        }

        /// <summary>
        ///  创建消费的消息通道
        /// </summary>
        public MqConsumerChannel CreateReceiveChannel(string exchangeType, string exchange, string queue, string routekey, Func<MessageBody, Task> onReceivedCallback)
        {
            var channel = CreateConsumerChannel(exchangeType, exchange, queue, routekey);
            MqConsumerChannel myChannel = new MqConsumerChannel(exchangeType, exchange, queue, routekey)
            {
                Connection = _mqConnection.Connection,
                OnReceivedCallback = onReceivedCallback,
            };
            CreateConsumer(channel, queue, ref myChannel);
            return myChannel;
        }

        public MqProducerChannel CreatePublishChannel(string exchangeType, string queue) => CreatePublishChannel(exchangeType, MqGlobleConfig.Exchange, queue, queue);

        /// <summary>
        /// 创建生产的消息通道
        /// </summary>
        public MqProducerChannel CreatePublishChannel(string exchangeType, string exchange, string queue, string routekey)
        {
            var channel = CreateProducerChannel(exchangeType, exchange, queue, routekey);
            MqProducerChannel producerChannel = new MqProducerChannel(exchangeType, exchange, queue, routekey)
            {
                Connection = _mqConnection.Connection,
                Channel = channel
            };
            return producerChannel;
        }

        /// <summary>
        ///  创建一个通道，包含交换机/路由/队列，并建立绑定关系
        /// </summary>
        /// <param name="type">交换机类型</param>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="routeKey">路由名称</param>
        /// <returns></returns>
        private IModel CreateConsumerChannel(string exchangeType, string exchangeName, string queueName, string routeKey, IDictionary<string, object> exchangeDeclarearguments = null)
        {
            Log.Information($"mq配置--并发:{MqGlobleConfig.PrefetchCount},持久:{MqGlobleConfig.Durable}");
            var channel = _mqConnection.Connection.CreateModel();
            channel.BasicQos(MqGlobleConfig.PrefetchSize, MqGlobleConfig.PrefetchCount, MqGlobleConfig.Global);
            channel.QueueDeclare(queueName, MqGlobleConfig.Durable, false, false, new Dictionary<string, object> { { "x-max-priority", byte.MaxValue } });
            channel.ExchangeDeclare(exchangeName, exchangeType, MqGlobleConfig.Durable, false, exchangeDeclarearguments);
            if (exchangeName != "default")
            {
                channel.QueueBind(queueName, exchangeName, routeKey);
            }
            return channel;
        }

        /// <summary>
        ///  创建一个通道
        /// </summary>
        private IModel CreateProducerChannel(string exchangeType, string exchangeName, string queueName, string routeKey, IDictionary<string, object> exchangeDeclarearguments = null)
        {
            IModel channel = _mqConnection.CreateModel();
            channel.QueueDeclare(queueName, MqGlobleConfig.Durable, false, false, new Dictionary<string, object> { { "x-max-priority", byte.MaxValue } });
            channel.ExchangeDeclare(exchangeName, exchangeType, MqGlobleConfig.Durable, false, exchangeDeclarearguments);
            if (exchangeName != "default")
            {
                channel.QueueBind(queueName, exchangeName, routeKey);
            }
            return channel;
        }

        /// <summary>
        ///  接收消息到队列中
        /// </summary>
        /// <param name="model">消息通道</param>
        /// <param name="queue">队列名称</param>
        /// <param name="callback">订阅消息的回调事件</param>
        /// <returns></returns>
        private EventingBasicConsumer CreateConsumer(IModel model, string queue, ref MqConsumerChannel mqConsumerChannel)
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(model);
            consumer.Received += mqConsumerChannel.Receive;
            mqConsumerChannel.Consumer = consumer;
            model.BasicConsume(queue: queue, autoAck: MqGlobleConfig.AutoAck, consumer: consumer);
            return consumer;
        }
    }
}
