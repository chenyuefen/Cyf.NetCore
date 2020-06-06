using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;

namespace Cyf.MicroService.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        // 1、加载ocelot配置文件
                        // config.AddJsonFile("ocelot.json");

                        config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                              .AddJsonFile("appsettings.json", true, true)
                              .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                              .AddOcelot(hostingContext.HostingEnvironment)
                              //.AddJsonFile("ocelot_ids4.json", true, true) // 动态路由配置
                              .AddEnvironmentVariables();
                    });
                });
                
    }
}
