using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cyf.NetCore.Middlewares
{
    public class ErrorLoggerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public ErrorLoggerMiddleware(RequestDelegate next, ILogger<ErrorLoggerMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogError(context, ex);
            }
        }


        private async Task LogError(HttpContext context, Exception ex)
        {
            _logger.LogCritical(ex, "未处理异常");
            var outputMsg = ex.Message;
            //针对mongo大小限制
            if (Regex.IsMatch(outputMsg, @"Size (\d+) is larger than MaxDocumentSize 16777216"))
            {
                var size = int.Parse(Regex.Match(outputMsg, @"Size (\d+) is larger than MaxDocumentSize 16777216").Groups[1].Value) * 1.0 / 1024 / 1024;
                outputMsg = $"总大小不能超过16M,你的参数大小为{size:N2}";
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(outputMsg, _settings));
        }
    }

}
