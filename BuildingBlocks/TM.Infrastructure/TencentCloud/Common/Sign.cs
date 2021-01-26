using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TM.Infrastructure.TencentCloud.Common
{
    /// <summary>
    /// 签名类
    /// </summary>
    public class Sign
    {
        ///<summary>生成签名</summary>
        ///<param name="signStr">被加密串</param>
        ///<param name="secret">加密密钥</param>
        ///<returns>签名</returns>
        public static string Signature(string signStr, string secret, string SignatureMethod)
        {
            if (SignatureMethod == "HmacSHA256")
                using (HMACSHA256 mac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
                {
                    byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(signStr));
                    return Convert.ToBase64String(hash);
                }
            else
                using (HMACSHA1 mac = new HMACSHA1(Encoding.UTF8.GetBytes(secret)))
                {
                    byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(signStr));
                    return Convert.ToBase64String(hash);
                }
        }

        /// <summary>
        /// 建立参数字符串
        /// </summary>
        /// <param name="requestParams"></param>
        /// <param name="requestMethod"></param>
        /// <returns></returns>
        protected static string BuildParamStr(SortedDictionary<string, object> requestParams, string requestMethod = "GET")
        {
            string retStr = "";
            foreach (string key in requestParams.Keys)
            {
                if (key == "Signature")
                {
                    continue;
                }
                if (requestMethod == "POST" && requestParams[key].ToString().Substring(0, 1).Equals("@"))
                {
                    continue;
                }
                retStr += string.Format("{0}={1}&", key.Replace("_", "."), requestParams[key]);
            }
            return "?" + retStr.TrimEnd('&');
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="requestParams"></param>
        /// <param name="requestMethod"></param>
        /// <param name="requestHost"></param>
        /// <param name="requestPath"></param>
        /// <returns></returns>
        public static string MakeSignPlainText(SortedDictionary<string, object> requestParams, string requestMethod = "GET",
            string requestHost = "cvm.api.qcloud.com", string requestPath = "/v2/index.php")
        {
            string retStr = "";
            retStr += requestMethod;
            retStr += requestHost;
            retStr += requestPath;
            retStr += BuildParamStr(requestParams);
            return retStr;
        }

        /// <summary>
        /// 获取签名链
        /// </summary>
        /// <param name="secretId"></param>
        /// <param name="expireTime"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static string BuildSignPlain(string secretId)
        {
            secretId = Uri.EscapeDataString(secretId);
            var currentTimeStamp = GetTimeStamp();
            var random = new Random().Next(0, 1000000);
            var expireTime = currentTimeStamp + 3600 * 24 * 2;
            return string.Format("secretId={0}&currentTimeStamp={1}&expireTime={2}&random={3}", secretId, currentTimeStamp, expireTime, random);
        }

        /// <summary>
        /// 读取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
}
