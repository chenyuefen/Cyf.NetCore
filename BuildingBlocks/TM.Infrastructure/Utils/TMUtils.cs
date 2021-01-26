namespace TM.Infrastructure.Utils
{
    public class TMUtils
    {
        #region ----生成连接字符串----

        /// <summary>
        /// 生成连接字符串 
        /// </summary>
        /// <param name="sysUrl">数据库地址</param>
        /// <param name="comPort">数据库端口</param>
        /// <param name="comPort">数据库名</param>
        /// <param name="dbUid">数据库账号</param>
        /// <param name="dbPwd">数据库密码</param>       
        /// <returns>连接字符串</returns>
        public static string CreateConnStr(string sysUrl, int comPort, string dbName, string dbUid, string dbPwd)
        {
            var connSrt = string.Format("Server={0},{1};Database={2};Integrated Security=false;pooling=false;User ID={3};Password={4};", sysUrl, comPort, dbName, dbUid, dbPwd);
            return connSrt;
        }
        /// <summary>
        /// 生成连接字符串 
        /// </summary>
        /// <param name="sysUrl">数据库地址</param>
        /// <param name="comPort">数据库端口</param>
        /// <param name="comPort">数据库名</param>
        /// <param name="dbUid">数据库账号</param>
        /// <param name="dbPwd">数据库密码</param>      
        /// <param name="dbPwd">超时时间 秒</param>   
        /// <returns>连接字符串</returns>
        public static string CreateConnStr(string sysUrl, int comPort, string dbName, string dbUid, string dbPwd, int timeOut)
        {
            var connSrt = string.Format("Server={0},{1};Database={2};Integrated Security=false;pooling=false;User ID={3};Password={4};Connect Timeout={5}", sysUrl, comPort, dbName, dbUid, dbPwd, timeOut);
            return connSrt;
        }
        #endregion
    }
}
