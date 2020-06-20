using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.RabbitMqHelper
{
    public class MessageBody
    {
        public EventingBasicConsumer Consumer { get; set; }
        public BasicDeliverEventArgs BasicDeliver { get; set; }
        public string Content { get; set; }
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsError => Exception != null || !string.IsNullOrEmpty(ErrorMessage);

        public bool BasicAck(bool multiple = false)
        {
            try
            {
                Consumer.Model.BasicAck(BasicDeliver.DeliveryTag, multiple);
                return true;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"delivery-{BasicDeliver.DeliveryTag}|{multiple}");
                return false;
            }
        }

        public void BasicReject(bool requeue = false)
        {
            try
            {
                Consumer.Model.BasicReject(BasicDeliver.DeliveryTag, requeue);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"delivery-{BasicDeliver.DeliveryTag}|{requeue}");
            }
        }
    }
}
