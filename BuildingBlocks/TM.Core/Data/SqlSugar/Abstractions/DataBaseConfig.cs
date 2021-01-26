using System.Collections.Generic;

namespace TM.Core.Data.SqlSugar.Abstractions
{
    public class DataBaseConfig
    {
        public string DbType { get; set; }
        public DBConnectionStrings ConnectionStrings { get; set; }
    }

    public class DBConnectionStrings
    {
        public string Master { get; set; }
        public List<SlaveConnectionConfigs> Slave { get; set; }
    }

    public class SlaveConnectionConfigs
    {
        public int HitRate { get; set; }

        public string ConnectionString { get; set; }
    }

    public class SqlServerConfig
    {
        public string DbType { get; set; }

        public string ConnectionString { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenDurationInMinutes { get; set; }
    }


}
