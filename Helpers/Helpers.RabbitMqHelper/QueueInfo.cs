using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.RabbitMqHelper
{
    public class QueueInfo
    {

        public QueueInfo(string queue, Func<MessageBody,Task> onReceived) : this(queue, queue, onReceived)
        {
        }
        public QueueInfo(string queueName, string routerKey, Func<MessageBody, Task> onReceived)
        {
            QueueName = queueName;
            RouterKey = routerKey;
            OnReceived = onReceived;
            ExchangeType = RabbitMQ.Client.ExchangeType.Direct;
        }

        /// <summary>
        ///  队列名称
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        ///  路由名称
        /// </summary>
        public string RouterKey { get; set; }
        /// <summary>
        ///  交换机类型
        /// </summary>
        public string ExchangeType { get; set; }
        /// <summary>
        ///  接受消息委托
        /// </summary>
        public Func<MessageBody, Task> OnReceived { get; set; }
    }
}
