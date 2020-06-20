using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMqProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqp://test:test@47.92.238.208:30000/test") };
            using (var connection = factory.CreateConnection())
            {
                Console.WriteLine("start ...");
                while (Console.ReadLine() != null)
                {
                    using (var channel = connection.CreateModel())
                    {
                        //创建一个名叫"hello"的消息队列
                        channel.QueueDeclare(queue: "hello",
                            durable: false,//是否要持久化
                            exclusive: false,//如果设置true的化，队列将变成私有的，只有创建队列的应用程序才能够消费队列消息
                            autoDelete: false,//当最后一个消费者取消订阅的时候，队列会自动移除
                            arguments: null); 

                         var message = "Hello World!";
                        var body = Encoding.UTF8.GetBytes(message);

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;//持久化消息

                        //向该消息队列发送消息message
                        channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: properties,
                            body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                }
            }
        }
    }
}
