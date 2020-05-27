using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cyf.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        #region Identity
        private IConfiguration _IConfiguration = null;
        private ILogger<TestController> _logger = null;
        public TestController(IConfiguration configuration, ILogger<TestController> logger)
        {
            this._IConfiguration = configuration;
            this._logger = logger;
        }
        #endregion
        [HttpGet]
        public IActionResult Get()
        {
            this._logger.LogWarning("TestController-Get 执行");
            string msg = null;

            using (ConsulClient consulClient = new ConsulClient(c => c.Address = new Uri("http://127.0.0.1:8500")))//实例化当前的consul
            {
                //consulClient.Agent.Services()获取consul中注册的所有的服务
                Dictionary<string, AgentService> services = consulClient.Agent.Services().Result.Response;
                foreach (KeyValuePair<string, AgentService> kv in services)
                {
                    this._logger.LogWarning($"key={kv.Key},{kv.Value.Address},{kv.Value.ID},{kv.Value.Service},{kv.Value.Port} {string.Join(",", kv.Value.Tags)}");
                }

                //获取所有服务名字是"apiserviceTest"所有的服务
                var agentServices = services.Where(s => s.Value.Service.Equals("apiserviceTest", StringComparison.CurrentCultureIgnoreCase))
                   .Select(s => s.Value);

                //根据当前TickCount对服务器个数取模，“随机”取一个机器出来，避免“轮询”的负载均衡策略需要计数加锁问题
                //1 随机获取 -- 均衡调度
                var agentService = agentServices.ElementAt(Environment.TickCount % agentServices.Count());

                msg = $"{agentService.Address},{agentService.ID},{agentService.Service},{agentService.Port}";
                this._logger.LogWarning(msg);
            }
            return new JsonResult(new
            {
                Id = 123,
                Name = "Cyf",
                Remark = msg
            });
        }
    }
}