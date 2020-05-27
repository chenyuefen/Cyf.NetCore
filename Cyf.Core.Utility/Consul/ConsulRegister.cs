﻿using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cyf.Core.Utility.Consul
{
    public static class ConsulRegister
    {
        public static void RegistConsul(this IConfiguration configuration)
        {
            #region 注册consul
            string ip = configuration["ip"] ?? "Localhost";
            //部署到不同服务器的时候不能写成127.0.0.1或者0.0.0.0,因为这是让服务消费者调用的地址
            //int port = int.Parse(configuration["Consul:ServicePort"]);//服务端口
            int port = string.IsNullOrWhiteSpace(configuration["port"]) ? 44344 : int.Parse(configuration["port"]);
            ConsulClient client = new ConsulClient(obj =>
            {
                obj.Address = new Uri("http://127.0.0.1:8500");
                obj.Datacenter = "dc1";
            });
            //向consul注册服务
            Task<WriteResult> result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "apiserviceTest_" + Guid.NewGuid(),//服务编号，不能重复
                Name = "apiserviceTest",//服务的名字--将来调用时用的就是这个
                Address = ip,
                Port = port,
                Tags = new string[] { },//可以用来设置权重
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务停止多久后反注册
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                    HTTP = $"http://{ip}:{port}/api/health",//健康检查地址,能返回StateCode = 200即可
                    Timeout = TimeSpan.FromSeconds(5)
                }
            });
            #endregion
        }

    }
}
