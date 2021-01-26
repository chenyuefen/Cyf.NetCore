using SqlSugar;

namespace TM.Core.Data.SqlSugar.Abstractions
{
    public class ShopMallConfig
    {
        public static ConnectionConfig DbCofig = SqlSugarConfig.GetConnectionString("appsettings.json", "DataBase_TeaMaxWxDB");
    }
}
