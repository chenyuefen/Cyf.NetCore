using System;
using System.Security.Cryptography;
using System.Text;

namespace TM.Infrastructure.TencentCloud.Common
{
    /// <summary>
    /// 点播签名类
    /// </summary>
    public class Signature
    {
        public string secretId;
        public string secretKey;
        public int random;
        public long currentTimeStamp;
        public int expireTime;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="secretId"></param>
        /// <param name="secretKey"></param>
        public Signature(string secretId, string secretKey)
        {
            this.secretId = secretId;
            this.secretKey = secretKey;
            this.random = new Random().Next(0, 1000000);
            this.currentTimeStamp = Signature.GetTimeStamp();
            this.expireTime = 3600 * 24 * 2;
        }

        /// <summary>
        /// 获取云点播上传签名
        /// </summary>
        /// <returns></returns>
        public string GetUploadSignature()
        {
            string signString = "";
            signString += ("secretId=" + Uri.EscapeDataString((secretId)));
            signString += ("&currentTimeStamp=" + currentTimeStamp);
            signString += ("&expireTime=" + (currentTimeStamp + expireTime));
            signString += ("&random=" + random);

            byte[] bytesSign = CreateHmacSha(signString, secretKey);
            byte[] byteContent = Encoding.Default.GetBytes(signString);
            byte[] nCon = new byte[bytesSign.Length + byteContent.Length];
            bytesSign.CopyTo(nCon, 0);
            byteContent.CopyTo(nCon, bytesSign.Length);
            return Convert.ToBase64String(nCon);
        }

        /// <summary>
        /// 读取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return Convert.ToInt64(timeSpan.TotalSeconds);
        }

        /// <summary>
        /// 生成基于哈希的消息验证代码
        /// </summary>
        /// <param name="signatureString"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        private byte[] CreateHmacSha(string signString, string secretKey)
        {
            var encoding = Encoding.UTF8;
            HMACSHA1 hmacsha = new HMACSHA1(encoding.GetBytes(secretKey));
            hmacsha.Initialize();
            byte[] buffer = encoding.GetBytes(signString);
            return hmacsha.ComputeHash(buffer);
        }
    }
}
