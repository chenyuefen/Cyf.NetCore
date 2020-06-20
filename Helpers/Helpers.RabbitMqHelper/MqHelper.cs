using Helpers.RabbitMqHelper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Helpers
{
    public class MqHelper
    {
        public MqHelper(string urlBase)
        {
            _urlBase = urlBase;
        }
        public static Lazy<HttpClient> _httpClientLazy = new Lazy<HttpClient>(() => new HttpClient(new HttpClientHandler
        {
            Credentials = new NetworkCredential(MqGlobleConfig.UserName, MqGlobleConfig.Password),
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        })
        {
            Timeout = TimeSpan.FromSeconds(5)
        });
        private readonly string _urlBase;

        public static HttpClient _httpClient => _httpClientLazy.Value;

        public async Task<bool> IsLoad(string queue, int max = 10000) => await GetQueueNum(queue) > max;

        /// <summary>
        /// 获取队列数量,null是报异常
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public async Task<long?> GetQueueNum(string queue)
        {
            try
            {
#if DEBUG
                var content = await _httpClient.GetStringAsync($"{_urlBase}/api/queues/{MqGlobleConfig.VHost}/{queue}");
#else
                var content = await _httpClient.GetStringAsync($"{_urlBase}/api/queues/{MqGlobleConfig.VHost}/{queue}");
#endif
                var ret = JsonConvert.DeserializeObject<QueueResponse.Rootobject>(content);
                return ret.messages;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"获取队列{queue}数量");
                return null;
            }
        }
    }
}
