using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.RabbitMqHelper
{
    public class DemoService : MqServiceBase
    {
        public DemoService()
        {
        }

        public override string QueueName => "Demo";

        public override Task OnReceived(MessageBody message)
        {
            try
            {
                Log.Information(message.Content);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
            message.BasicAck();
            return Task.CompletedTask;
        }
    }
}
