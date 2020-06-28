using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cyf.JWTAuthentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cyf.WebApiCore.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[Authorize] 
    public class AuthorizeController : ControllerBase
    {
        private ILogger<AuthorizeController> _Logger = null;
        private IJWTService _iJWTService = null;
        public AuthorizeController(ILogger<AuthorizeController> logger,
            IJWTService service)
        {
            this._iJWTService = service;
            this._Logger = logger;
            _Logger.LogInformation($"{nameof(AuthorizeController)} 被构造。。。。 ");
        }

        /// <summary>
        /// 获取token的
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpGet]
        public string Login(string name, string password)
        {
            ///这里肯定是需要去连接数据库做数据校验
            if ("Cyf".Equals(name) && "123456".Equals(password))//应该数据库
            {
                string token = this._iJWTService.GetToken(name);
                return JsonConvert.SerializeObject(new
                {
                    result = true,
                    token
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    result = false,
                    token = ""
                });
            }
        }

        [HttpGet]
        [Route("Get")]
        [AllowAnonymous] 
        public IActionResult Get()
        {
            return new JsonResult(new
            {
                Data = "这个是OK的，这个Api并没有要求授权！"
            });
        }


        [HttpGet]
        [Route("GetAuthorizeData")]
        [Authorize] //Microsoft.AspNetCore.Authorization
        public IActionResult GetAuthorizeData()
        {
            var Name = base.HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => !string.IsNullOrEmpty(a.Value))?.Value;

            Console.WriteLine($"this is Name {Name}");
            return new JsonResult(new
            {
                Data = "已授权",
                Type = "GetAuthorizeData"
                //Claims=Newtonsoft.Json.JsonConvert.SerializeObject(Claims)
            });
        }
    }
}