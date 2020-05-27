using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cyf.NetCore.Controllers
{
    /// <summary>
    ///  ********************* 微服务架构 ****************************
    /// 1 微服务架构解析，优缺点、挑战与转变
    /// 2 服务实例准备，Consul安装
    /// 3 Consul注册，心跳检测，服务发现
    /// 
    /// 微服务架构专题，基于Core
    /// 
    /// 命令行参数--AddCommandLine---启动时可以传递参数---然后可以Configuration["name"] 获取一下
    /// 
    /// 
    /// 服务注册与发现
    /// a  添加Webapi服务---添加log4net---注入到控制器--记录日志
    /// b  命令启动webapi服务---2个实例--不同端口
    /// c  准备consul--启动--nuget-consul
    /// d  网站启动后需要注册到consul
    /// e  添加health-check，健康检查
    /// f  在startup去注册下---然后启动多个实例
    /// g  去http://localhost:8500查看多个服务被发现和心跳检测 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/Values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Values/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
