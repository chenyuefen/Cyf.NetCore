using TM.Infrastructure.Configs;

namespace HangfireHttpJobClient.Config
{
    public static class JobConfig
    {
        /// <summary>
        /// 接口域名
        /// </summary>
        public static string DomainName = ConfigHelper.Configuration["DomainUrL"];

        /// <summary>
        /// 合伙人定时Cron
        /// </summary>
        public static string PartnerCron = ConfigHelper.Configuration["HttpJobServer:PartnerCron"];

        /// <summary>
        /// 作业服务域名
        /// </summary>
        public static string ServerUrl = ConfigHelper.Configuration["HttpJobServer:Url"];

        /// <summary>
        /// 作业服务登陆账号
        /// </summary>
        public static string BasicUserName = ConfigHelper.Configuration["HttpJobServer:BasicUserName"];

        /// <summary>
        /// 作业服务登陆密码
        /// </summary>
        public static string BasicUserPwd = ConfigHelper.Configuration["HttpJobServer:BasicUserPwd"];
    }
}