using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using Helpers;

namespace Helpers.RabbitMqHelper
{
    public class MqProducerChannel
    {
        public IConnection Connection { get; set; }
        public IModel Channel { get; set; }
        public static UTF8Encoding UTF8 { get; } = new UTF8Encoding(false);
        public string ExchangeTypeName { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutekeyName { get; set; }

        public MqProducerChannel(string exchangeType, string exchange, string queue, string routekey)
        {
            ExchangeTypeName = exchangeType;
            ExchangeName = exchange;
            QueueName = queue;
            RoutekeyName = routekey;
        }

        /// <summary>
        ///  向当前队列发送消息
        /// </summary>
        /// <param name="content"></param>
        public void Publish<T>(T content, byte priority = 200)
        {
            byte[] body = ByteSerialize(content);
            IBasicProperties prop = Channel.CreateBasicProperties();
            prop.Priority = priority;
            prop.Persistent = MqGlobleConfig.Durable;
            //Channel.BasicPublish(ExchangeName, RoutekeyName, false, prop, body);
            lock (Channel)
            {
                Channel.BasicPublish(ExchangeName, RoutekeyName, false, prop, body);
            }
        }

        public void BatchPublish<T>(IEnumerable<(T, byte?)> contents)
        {
            if (contents is null)
            {
                throw new ArgumentNullException(nameof(contents));
            }
            var batch = Channel.CreateBasicPublishBatch();
            foreach (var item in contents)
            {
                var content = item.Item1;
                var priority = item.Item2 ?? 200;
                byte[] body = ByteSerialize(content);
                IBasicProperties prop = Channel.CreateBasicProperties();
                prop.Priority = priority;
                prop.Persistent = MqGlobleConfig.Durable;
                batch.Add(ExchangeName, RoutekeyName, false, prop, body);
            }
            lock (Channel)
            {
                batch.Publish();
            }
        }

        private byte[] ByteSerialize<T>(T obj)
        {
            return Encoding.UTF8.GetBytes(obj is string ? obj as string : JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        ///  关闭消息队列的连接
        /// </summary>
        public void Stop()
        {
            Channel.Dispose();
            Connection.Dispose();
        }
    }
}
