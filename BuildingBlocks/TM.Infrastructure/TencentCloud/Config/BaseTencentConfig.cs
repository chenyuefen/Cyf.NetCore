
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：BaseTencentConfig
// 文件功能描述：
// 
// 创建者：庄欣锴
// 创建时间：2020年5月27日
// 
//----------------------------------------------------------------*/

using TM.Infrastructure.Configs;

namespace TM.Infrastructure.TencentCloud.Config
{
    public class BaseTencentConfig
    {
        /// <summary>
        /// 腾讯云账户的账户标识 APPID
        /// </summary>
        public static string APP_ID = ConfigHelper.Configuration["TencentQCloud:AppId"];

        /// <summary>
        ///  云API 密钥 SecretId
        /// </summary>
        public static string SECRET_ID = ConfigHelper.Configuration["TencentQCloud:SecretId"];

        /// <summary>
        /// //云 API 密钥 SecretKey
        /// </summary>
        public static string SECRET_KEY = ConfigHelper.Configuration["TencentQCloud:SecretKey"];

        /// <summary>
        /// 默认的存储桶地域
        /// </summary>
        public const string REGION = "ap-guangzhou";
    }
}