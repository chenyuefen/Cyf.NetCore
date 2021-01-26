namespace TM.Infrastructure.Configs
{
    public class TMKeys
    {
        // redis key prefix must start with [system]
        public static readonly string System = "tm";

        public static string RegisterPhonePrefix = System + ":register:phone:";

        public static string UserJwtTokenPrefix = System + ":user:jwt:token:";

        public static string Provinces = System + ":country:";

        /// <summary>
        /// 接口地址
        /// </summary>
        public const string ApiHost = "ApiHost";

        /// <summary>
        /// 站点地址
        /// </summary>
        public const string WebHost = "WebHost";

        /// <summary>
        /// 访客用户默认密码
        /// </summary>
        public const string GuestDefaultPassword = "123456";

        /// <summary>
        /// 用户Cookie名称
        /// </summary>
        public const string UserGuidCookiesName = "ShopUserGuid";

        /// <summary>
        /// Jwt中userLoginKey的组成字符串格式
        /// 0当前时间戳 | 1用户ID
        /// </summary>
        public const string UserLoginKeyFormat = "{0}UserID{1}";

        /// <summary>
        /// Jwt中userLoginKey加密密钥
        /// </summary>
        public const string UserLoginKeyCipher = "tianmei963Userloginkey258Teamax741";

        /// <summary>
        /// 用户密码加密保存到数据库的加密密钥
        /// </summary>
        public const string UserPasswordMd5Cipher = "tianmei963PasswordMd5Key258Teamax741";
    }
}

