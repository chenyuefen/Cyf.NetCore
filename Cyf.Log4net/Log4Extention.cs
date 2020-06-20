using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyf.Log4net
{
    public static class Log4Extention
    {
        public static void UseLog(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddFilter("System", LogLevel.Warning);
            loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);//过滤掉系统默认的一些日志
            loggingBuilder.AddLog4Net();
        }
    }
}
