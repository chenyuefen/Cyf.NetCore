namespace TM.Core.Models.Entity
{
    public class Configs
    {
    }

    public class RedisConfigEntity
    {
        public string ConnectionString { get; set; }

        public string DefaultCache { get; set; }
    }

    public class GloryBAConfigEntity
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }
        public string SuiteId { get; set; }

        public string SuiteSecret { get; set; }

        public string Token { get; set; }

        public string EncodingAESKey { get; set; }

        public string CorpId { get; set; }

        public string ProviderSecret { get; set; }

        public string ProviderToken { get; set; }

        public string ProviderEncodingAESKey { get; set; }

        public string TemplateToken { get; set; }

        public string TemplateEncodingAESKey { get; set; }

        public string ContactToken { get; set; }

        public string ContactEncodingAESKey { get; set; }

        public string ExternalContactToken { get; set; }

        public string ExternalContactEncodingAESKey { get; set; }
    }

    public class ShopMallConfigEntity
    {
        public string ShopMallDomain { get; set; }
    }

    public class TencentQCloudConfigEntity
    {
        /// <summary>
        /// 腾讯云APPID
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 腾讯云api密钥id
        /// </summary>
        public string SecretId { get; set; }

        /// <summary>
        /// 腾讯云api密钥key
        /// </summary>
        public string SecretKey { get; set; }
    }

    /// <summary>
    /// 云直播配置实体
    /// </summary>
    public class CloudLiveConfigEntity
    {
        /// <summary>
        /// 云直播推流域名
        /// </summary>
        public string LivePushDomain { get; set; }

        /// <summary>
        /// 云直播播放域名
        /// </summary>
        public string LivePlayDomain { get; set; }

        /// <summary>
        /// 云直播推流鉴权Key
        /// </summary>
        public string LivePushAuthKey { get; set; }

        /// <summary>
        /// 云直播播放鉴权Key
        /// </summary>
        public string LivePlayAuthKey { get; set; }
    }
}
