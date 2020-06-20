using Exceptionless;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Helpers
{
    public class SerilogFactory
    {
        /// <summary>
        /// 初始化日志
        /// </summary>
        public static void Init<T>(IConfiguration configuration = null, string appName = null, string apiKey = null, string serverUrl = null,
            string outputTemplate = null)
        {
            Log.Logger = CreateLog<T>(configuration, appName, apiKey, serverUrl, outputTemplate: outputTemplate);
            Log.Debug("日志组件初始化完成");
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        public static Logger CreateLog<T>(IConfiguration configuration = null, string appName = null, string apiKey = null,
            string serverUrl = null,
            string outputTemplate = null)
        {
            appName ??= configuration?["appName"] ?? "none";
            apiKey ??= configuration?["exceptionless:key"];
            serverUrl ??= configuration?["exceptionless:url"];
            outputTemplate ??= configuration?["outputTemplate"] ?? "{Timestamp:HH:mm:ss} {Level:u3} {userName} 【{Message:lj}】 {SourceContext:l}{NewLine}{Exception}";

            var directory = typeof(T).Assembly.GetName().Name;
            var path = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = $"c:\\logs\\{directory}\\";
            }
            else
            {
                path = "logs";
            }

            var conf = new LoggerConfiguration()
#if DEBUG
               .MinimumLevel.Verbose()
#else
               .MinimumLevel.Information()
#endif
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .MinimumLevel.Override("System", LogEventLevel.Warning)
               .Enrich.WithProperty("appName", appName)
               .WriteTo.Console()
               .WriteTo.Debug()
               .WriteTo.Logger(fileLogger =>
               {
                   fileLogger
                   .Filter.ByIncludingOnly(xx =>
                   {
                       return xx.Level > LogEventLevel.Warning;
                   })
                   .WriteTo.Async(x =>
                   {
                       x.File(Path.Combine(path, $"log-{Process.GetCurrentProcess().Id}-.err"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, outputTemplate: outputTemplate);
                   });
               })
               .WriteTo.Logger(fileLogger =>
               {
                   fileLogger
                   .Filter.ByIncludingOnly(xx =>
                   {
                       return xx.Level == LogEventLevel.Warning;
                   })
                   .WriteTo.Async(x =>
                   {
                       x.File(Path.Combine(path, $"log-{Process.GetCurrentProcess().Id}-.wrn"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, outputTemplate: outputTemplate);
                   });
               })
               .WriteTo.Logger(fileLogger =>
               {
                   fileLogger
                   .MinimumLevel.Verbose()
                   .Filter.ByIncludingOnly(xx => xx.Level < LogEventLevel.Warning)
                   .WriteTo.Async(x =>
                   {
                       x.File(Path.Combine(path, $"log-{Process.GetCurrentProcess().Id}-.inf"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, outputTemplate: outputTemplate);
                   });
                   if (configuration != null) fileLogger.ReadFrom.Configuration(configuration);
               });
            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(serverUrl))
            {
                Exceptionless.ExceptionlessClient.Default.Startup(apiKey);
                Exceptionless.ExceptionlessClient.Default.Configuration.ServerUrl = serverUrl;
                conf.WriteTo.Exceptionless();
            }
            if (configuration != null) conf.ReadFrom.Configuration(configuration);
            return conf.CreateLogger();
        }
    }
}
