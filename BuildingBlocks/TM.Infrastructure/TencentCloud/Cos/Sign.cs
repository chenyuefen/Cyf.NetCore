using System;
using System.Security.Cryptography;
using System.Text;

namespace TM.Infrastructure.TencentCloud.Cos
{
    /// <summary>
    /// 对象存储签名
    /// </summary>
    public static class Sign
    {
        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secretId"></param>
        /// <param name="secretKey"></param>
        /// <param name="expired"></param>
        /// <param name="fileId"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        private static string Signature(string appId, string secretId, string secretKey, long expired, string fileId, string bucketName)
        {
            if (secretId == "" || secretKey == "")
            {
                return "-1";
            }
            var now = DateTime.Now.ToUnixTime() / 1000;
            var rand = new Random();
            var rdm = rand.Next(Int32.MaxValue);
            var plainText = "a=" + appId + "&k=" + secretId + "&e=" + expired + "&t=" + now + "&r=" + rdm + "&f=" + fileId + "&b=" + bucketName;

            using (HMACSHA1 mac = new HMACSHA1(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = mac.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                var pText = Encoding.UTF8.GetBytes(plainText);
                var all = new byte[hash.Length + pText.Length];
                Array.Copy(hash, 0, all, 0, hash.Length);
                Array.Copy(pText, 0, all, hash.Length, pText.Length);
                return Convert.ToBase64String(all);
            }
        }

        public static string Signature(string appId, string secretId, string secretKey, string bucketName)
        {
            return Signature(appId, secretId, secretKey, getExpiredTime(), "", bucketName);
        }

        /// <summary>
        /// 内部方法：超时时间(当前系统时间+300秒)
        /// </summary>
        /// <returns></returns>	
        public static long getExpiredTime()
        {
            return DateTime.Now.ToUnixTime() / 1000 + 180;
        }

        public static long ToUnixTime(this DateTime nowTime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }

    }
}
