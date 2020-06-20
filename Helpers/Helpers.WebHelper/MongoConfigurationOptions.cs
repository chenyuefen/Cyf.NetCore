using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.WebHelper
{
    /// <summary>
    /// mongodb配置类
    /// </summary>
    public class MongoConfigurationOptions
    {
        public string MongoConnection { get; set; }
        public string MongoDatabaseName { get; set; }
    }
}
