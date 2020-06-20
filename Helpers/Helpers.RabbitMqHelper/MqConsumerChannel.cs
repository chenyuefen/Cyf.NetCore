using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Impl;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.RabbitMqHelper
{
    /// <summary>
    /// 生产者隧道
    /// </summary>
    public class MqConsumerChannel
    {
        public IConnection Connection { get; set; }
        public EventingBasicConsumer Consumer { get; set; }
        public static UTF8Encoding UTF8 { get; } = new UTF8Encoding(false);
        public string ExchangeType { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string Routekey { get; set; }
        private readonly Lazy<ILogger> _loggerLazy = new Lazy<ILogger>(() => Log.ForContext<MqConsumerChannel>());
        private ILogger _logger => _loggerLazy.Value;

        /// <summary>
        ///  外部订阅消费者通知委托
        /// </summary>
        public Func<MessageBody, Task> OnReceivedCallback { get; set; }

        public MqConsumerChannel(string exchangeType, string exchange, string queue, string routekey)
        {
            ExchangeType = exchangeType;
            ExchangeName = exchange;
            QueueName = queue;
            Routekey = routekey;
        }

        /// <summary>
        /// 消息接收
        /// </summary>
        internal async void Receive(object sender, BasicDeliverEventArgs e)
        {
            MessageBody body = null;
            try
            {
                body = new MessageBody
                {
                    Consumer = (EventingBasicConsumer)sender,
                    BasicDeliver = e
                };
                try
                {
                    body.Content = ByteDerialize(e.Body);
                    _logger.Debug(body.Content);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "订阅出错");
                    body.Content = $"订阅出错|{ex.Message}";
                    body.ErrorMessage = $"订阅出错|{ex.Message}";
                    body.Exception = ex;
                }
                await OnReceivedCallback?.Invoke(body);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "订阅出错");
                try
                {
                    body?.BasicAck();
                }
                catch (Exception ex2)
                {
                    _logger.Error(ex2, "订阅出错");
                }
            }
        }

        private string ByteDerialize(ReadOnlyMemory<byte> bytes)
        {
            return Encoding.UTF8.GetString(bytes.Span);
        }

        /// <summary>
        ///  关闭消息队列的连接
        /// </summary>
        public void Stop()
        {
            if (Connection != null && Connection.IsOpen)
            {
                this.Connection.Close();
                this.Connection.Dispose();
            }
        }
    }
}
