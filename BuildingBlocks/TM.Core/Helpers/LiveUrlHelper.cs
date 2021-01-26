using System;
using System.Security.Cryptography;
using System.Text;
using TM.Core.Models.Entity;
using TM.Infrastructure.Configs;
using TM.Infrastructure.Helpers;

namespace TM.Core.Helpers
{
    /// <summary>
    /// 直播推流/播放 地址帮助类
    /// </summary>
    public static class LiveUrlHelper
    {
        public static CloudLiveConfigEntity _cloundLiveConfig = ConfigHelper.GetAppSettings<CloudLiveConfigEntity>("appsettings.json", "", "CloudLive");


        #region 推流地址
        /// <summary>
        /// 推流地址
        /// </summary>
        /// <param name="streamName">流Id,直播标识</param>
        /// <param name="endTime">推流地址过期时间</param>
        /// <returns></returns>
        public static string CreateLivePushUrl(string streamName, DateTime endTime)
        {
            var domain = _cloundLiveConfig.LivePushDomain;
            var authKey = _cloundLiveConfig.LivePushAuthKey;
            var url = CreateLiveUrlBase(domain, authKey, streamName, endTime);
            return url;
        }
        #endregion

        #region 播放地址
        /// <summary>
        /// 播放地址
        /// </summary>
        /// <param name="streamName">流Id,直播标识</param>
        /// <param name="endTime">播放地址过期时间</param>
        /// <returns></returns>
        public static string CreateLivePlayUrl(string streamName, DateTime endTime)
        {
            var domain = _cloundLiveConfig.LivePlayDomain;
            var authKey = _cloundLiveConfig.LivePlayAuthKey;
            var url = CreateLiveUrlBase(domain, authKey, streamName, endTime);
            return url;
        }
        #endregion

        #region 地址加密拼接
        public static string CreateLiveUrlBase(string domain, string authKey, string streamName, DateTime endTime)
        {
            using (var md5 = MD5.Create())
            {
                //时间戳
                var timeSpan = (endTime.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                //转为16进制
                var hexTimeString = timeSpan.ToString("x8").ToUpper();
                //配置鉴权字符串 txSecret=KEY+StreamName+hex(time)
                var secretString = new StringBuilder()
                    .Append(authKey)
                    .Append(streamName)
                    .Append(hexTimeString)
                    .ToString();

                //MD5 加密鉴权字符串
                var md5SecretString = Encrypt.Md5By32(secretString).ToLower();

                //拼接url
                var url = new StringBuilder()
                    .Append("rtmp://")
                    .Append($"{domain}/")//推流/播放域名
                    .Append("live/")//应用名称,默认live
                    .Append($"{streamName}")//流名称 用户自定义 用于表示直播流
                    .Append($"?txSecret={md5SecretString}")//直播鉴权串
                    .Append($"&txTime={hexTimeString}")//推流地址时间戳
                    .ToString();

                return url;
            }
        }
        #endregion
    }
}
