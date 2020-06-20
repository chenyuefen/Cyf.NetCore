using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.RabbitMqHelper
{
    public class MqConnection
    {
        //private readonly string _vhost = string.Empty;
        private readonly MqConfig _config = null;
        private IConnection _connection = null;
        public string Url => $"amqp://{_config.UserName}:{_config.Password}@{_config.HostName}:{_config.Port}/{_config.vHost}";

        public MqConnection(MqConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            //_vhost = config.vHost;
        }

        public IConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    ConnectionFactory factory = new ConnectionFactory
                    {
                        AutomaticRecoveryEnabled = true,//自动重连
                        UserName = _config.UserName,
                        Password = _config.Password,
                        HostName = _config.HostName,
                        Port = _config.Port,
                        VirtualHost = _config.vHost,
                    };
                    _connection = factory.CreateConnection();
                }
                return _connection;
            }
        }

        public IModel CreateModel() => Connection.CreateModel();
    }
}
