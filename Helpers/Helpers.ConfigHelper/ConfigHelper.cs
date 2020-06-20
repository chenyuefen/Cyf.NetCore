using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public static class ConfigHelper
    {
        /// <summary>
        /// apollo配置
        /// 需要配置节apollo
        /// </summary>
        public static IConfigurationBuilder AddApolloExt<T>(this IConfigurationBuilder configurationBuilder)
        {
            var logger = SerilogFactory.CreateLog<T>(apiKey: "");
            var config = configurationBuilder.Build().GetSection("apollo");
            var appid = config.GetValue<string>("appid");
            logger.Information($"apollo appid:{appid}");
            if (!string.IsNullOrEmpty(appid))
            {
                var apolloConfig = configurationBuilder.AddApollo(config);

                apolloConfig
                    //.AddDefault(ConfigFileFormat.Xml)
                    //.AddDefault(ConfigFileFormat.Json)
                    //.AddDefault(ConfigFileFormat.Yml)
                    //.AddDefault(ConfigFileFormat.Yaml)
                    .AddDefault();

                var ns = config.GetSection("Namespace").Get<List<string>>();
                for (int i = 0; i < ns?.Count; i++)
                {
                    apolloConfig.AddNamespace(ns[i]);
                }
            }
            logger.Dispose();
            return configurationBuilder;
        }

        /// <summary>
        /// apollo配置
        /// 需要配置节apollo
        /// </summary>
        public static IHostBuilder AddApolloExt<T>(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((context, configBuild) =>
            {
                configBuild.AddApolloExt<T>();
            });
            return hostBuilder;
        }
    }
}
