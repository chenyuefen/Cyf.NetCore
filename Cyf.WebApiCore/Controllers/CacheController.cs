using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyCaching.Core;

namespace Cyf.WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IEasyCachingProviderFactory _easyCachingProviderFactory;

        public CacheController(IEasyCachingProviderFactory easyCachingProviderFactory)
        {
            _easyCachingProviderFactory = easyCachingProviderFactory;
        }

        [HttpGet]
        [Route("EasySet")]
        public IActionResult Set()
        {
            var cache1 = _easyCachingProviderFactory.GetCachingProvider("csredis");
            var cache2 = _easyCachingProviderFactory.GetCachingProvider("default");
            cache1.Set("testcs", 1, TimeSpan.FromSeconds(500));
            cache2.Set("testde", "testde", TimeSpan.FromSeconds(500));
            return new JsonResult(new
            {
                Data = "设置成功"
            });
        }

        [HttpGet]
        [Route("EsayGet")]
        public IActionResult Get()
        {
            var cache1 = _easyCachingProviderFactory.GetCachingProvider("csredis");
            var cache2 = _easyCachingProviderFactory.GetCachingProvider("default");
            var c1 = cache1.Get<string>("testcs");
            var c2 = cache2.Get<string>("testde");
            return new JsonResult(new
            {
                RedisCache = c1,
                DefaultCache = c2
            }) ;
        } 
        [HttpGet]
        [Route("CsRedisSet")]
        public IActionResult CsRedisSet()
        {
            var cache1 = RedisHelper.HSetNx("hashkey3", "value3", 13);
            var cache2 = RedisHelper.Set("hashkey2", "value2");
            return new JsonResult(new
            {
                RedisCache = cache1,
                RedisCache2 = cache2
            });
        }
        [HttpGet]
        [Route("CsRedisGet")]
        public IActionResult CsRedisGet()
        {
            //var cache1 = RedisHelper.HGet("hashkey3", "value3");
            var cache2 = RedisHelper.Get("hashkey2");

            return new JsonResult(new
            {
                RedisCache2 = cache2
            });
        }
    }
}