using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Helpers.RabbitMqHelper
{
    public class MqServcieManager
    {
        private CancellationTokenSource _cancellationTokenSource = null;
        public List<IService> Services { get; } = new List<IService>();
        public ILogger _logger = Log.ForContext<MqServcieManager>();
        public MqServcieManager()
        {
        }

        public void Start()
        {
            foreach (var item in this.Services)
            {
                try
                {
                    item.Start();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, ex.Message);
                }
            }
            //_cancellationTokenSource = new CancellationTokenSource();
            //var forget = SelfCheck(_cancellationTokenSource.Token);
        }

        public void Stop()
        {
            try
            {
                foreach (var item in this.Services)
                {
                    try
                    {
                        item.Stop();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, ex.Message);
                    }
                }
                Services.Clear();
                _cancellationTokenSource?.Cancel();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }

        public void AddService(IService service)
        {
            Services.Add(service);
        }

        /// <summary>
        ///  自检，配合 RabbitMQ 内部自动重连机制
        /// </summary>
        private async Task SelfCheck(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Run();
                await Task.Delay(TimeSpan.FromSeconds(60));
            }
            void Run()
            {
                try
                {
                    int error = 0, reconnect = 0;
                    _logger.Debug($"正在执行自检");
                    foreach (var item in Services)
                    {
                        for (int i = 0; i < item.Channels.Count; i++)
                        {
                            var c = item.Channels[i];
                            if (c.Connection == null || !c.Connection.IsOpen)
                            {
                                error++;
                                _logger.Information($"{c.ExchangeName} {c.QueueName} {c.Routekey} 重新创建消费者");
                                try
                                {
                                    c.Stop();
                                    var manager = new MqChannelManager(MqGlobleConfig.DefaultMQConfig);
                                    var channel = manager.CreateReceiveChannel(c.ExchangeType, c.ExchangeName, c.QueueName, c.Routekey, c.OnReceivedCallback);
                                    item.Channels.Remove(c);
                                    item.Channels.Add(channel);
                                    _logger.Information($"{c.ExchangeName} {c.QueueName} {c.Routekey} 重新创建完成");
                                    reconnect++;
                                }
                                catch (Exception ex)
                                {
                                    _logger.Error(ex, ex.Message);
                                }
                            }
                        }
                    }
                    _logger.Debug($"自检完成，错误数：{error}，重连成功数：{reconnect}");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "mq自检错误");
                }
            }

        }
    }
}
