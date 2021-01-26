using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using TM.Core.Modules;

namespace TM.Core
{
    public static partial class GlobalConfiguration
    {
        /// <summary>
        /// 数据初始化时间
        /// </summary>
        public static DateTime InitialOn = new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Local);

        public static IList<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();

        public static string DefaultCulture => "en-US";

        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }

        public static IConfiguration Configuration { get; set; }
    }
}
