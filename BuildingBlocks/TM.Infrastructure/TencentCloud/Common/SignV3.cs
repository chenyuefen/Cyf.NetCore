/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：SignV3
// 文件功能描述： 腾讯云API签名串生成==>签名版本:API 3.0 签名V3
//
// 创建者：庄欣锴
// 创建时间：2020-05-08 17:11:00
// 
//----------------------------------------------------------------*/
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TM.Infrastructure.Helpers;
using TM.Infrastructure.TencentCloud.Config;
using TM.Infrastructure.Timing;

namespace TM.Infrastructure.TencentCloud.Common
{
    /// <summary>
    /// 接口鉴权 v3
    /// TC3-HMAC-SHA256 签名方法
    /// TC3-HMAC-SHA256 签名方法相比以前的 HmacSHA1 和 HmacSHA256 签名方法，
    /// 功能上覆盖了以前的签名方法，而且更安全，支持更大的请求，支持 json 格式，性能有一定提升，建议使用该签名方法计算签名。
    /// </summary>
    public static class SignV3
    {
        #region BASE INFO
        /// <summary>
        /// 腾讯云账户的账户标识 APPID
        /// </summary>
        public static string AppId = CosConfig.APP_ID;

        /// <summary>
        ///  云API 密钥 SecretId
        /// </summary>
        public static string SecretId = CosConfig.SECRET_ID;

        /// <summary>
        /// //云 API 密钥 SecretKey
        /// </summary>
        public static string SecretKey = CosConfig.SECRET_KEY;

        /// <summary>
        /// 云点播 默认存储桶地域
        /// </summary>
        public const string REGION = CosConfig.REGION;
        #endregion

        #region 签名串生成
        #region 生成云点播签名方法 v3 使用 TC3-HMAC-SHA256 签名方法时，公共参数需要统一放到 HTTP Header 请求头部中
        //生成Authorization
        /*HTTP 标准身份认证头部字段，例如：
         TC3-HMAC-SHA256 Credential=AKIDEXAMPLE/Date/service/tc3_request,
         SignedHeaders=content-type;host,
         Signature=fe5f80f77d5fa3beca038a248ff027d0445342fe2855ddc963176630326f1024

         其中 
         - TC3-HMAC-SHA256：签名方法，目前固定取该值；
         - Credential：签名凭证，AKIDEXAMPLE 是 SecretId；
         - Date 是 UTC 标准时间的日期，取值需要和公共参数 X-TC-Timestamp (当前 UNIX 时间戳)换算的 UTC 标准时间日期一致；
         - service 为产品名，通常为域名前缀，例如域名 cvm.tencentcloudapi.com 意味着产品名是 cvm。本产品取值为 vod；
         - SignedHeaders：参与签名计算的头部信息，content-type 和 host 为必选头部；
         - Signature：签名摘要。
         */
        /// <summary>
        /// 创建TC3-HMAC-SHA256 签名V3
        /// </summary>
        /// <param name="serviceName">service 为产品名，必须与调用的产品域名一致。</param>
        /// <param name="hostName">host</param>
        /// <param name="requestMethod"></param>
        /// <param name="requestPayload"></param>
        /// <param name="queryString"></param>
        /// <param name="timestamp">当前 UNIX 时间戳</param>
        /// <returns></returns>
        public static string CreateAuthorizationSignV3(string serviceName, string hostName, string requestMethod, string queryString, string requestPayload, long timestamp)
        {
            var tc3_request = "tc3_request";//终止字符串（tc3_request）
            var service = serviceName;//service 为产品名，必须与调用的产品域名一致。
            var host = hostName;
            var algorithm = "TC3-HMAC-SHA256";
            var date = DateTimeHelper.DateTimeFormat(timestamp).ConvertToTimeZone(TimeZoneInfo.Utc).ToString("yyyy-MM-dd");

            // ************* 步骤 1：拼接规范请求串 *************
            var hTTPRequestMethod = requestMethod;
            var canonicalURI = "/";
            var canonicalQueryString = requestMethod == "POST" ? "" : queryString;// 对于 POST 请求，固定为空字符串"",GET 请求，则为 URL 中问号（?）后面的字符串内容 
            var canonicalHeaders1 = requestMethod == "POST" ? "content-type:application/json" : "content-type:application/x-www-form-urlencoded";
            var canonicalHeaders2 = $"host:{host}";
            var signedHeaders = "content-type;host";


            /*即对 HTTP 请求正文做 SHA256 哈希,然后十六进制编码，最后编码串转换成小写字母*/
            var hashedRequestPayload = requestMethod == "POST" ? Encrypt.Sha256(requestPayload) : "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";

            var canonicalRequest = new StringBuilder()
                .Append($"{hTTPRequestMethod}\n")
                .Append($"{canonicalURI}\n")
                .Append($"{canonicalQueryString}\n")
                .Append($"{canonicalHeaders1}\n")
                .Append($"{canonicalHeaders2}\n")
                .Append($"\n")
                .Append($"{signedHeaders}\n")
                .Append($"{hashedRequestPayload}")
                .ToString();

            // ************* 步骤 2：拼接待签名字符串 *************
            var credentialScope = $"{date}/{service}/{tc3_request}";
            var stringToSign = new StringBuilder()
                .Append($"{algorithm}\n")//签名算法，目前固定为 TC3-HMAC-SHA256。
                .Append($"{timestamp}\n")//请求时间戳，即请求头部的公共参数 X-TC-Timestamp 取值
                .Append($"{credentialScope}\n")
                .Append($"{Encrypt.Sha256(canonicalRequest)}")
                .ToString();

            // ************* 步骤 3：计算签名 *************
            var secretDate = HMAC_SHA256(Encoding.UTF8.GetBytes($"TC3{SecretKey.ToString()}"), date);
            var secretServic = HMAC_SHA256(secretDate, service);
            var secretSigning = HMAC_SHA256(secretServic, tc3_request);
            var signature = string.Join("", HMAC_SHA256(secretSigning, stringToSign).ToList().Select(x => x.ToString("x2")).ToArray());//十六进制(小写)

            // ************* 步骤 4：拼接 Authorization *************
            var authorization = new StringBuilder()
                .Append($"{algorithm} ")
                .Append($"Credential={SecretId.ToString()}/{credentialScope}, ")
                .Append($"SignedHeaders={signedHeaders}, ")
                .Append($"Signature={signature}")
                .ToString();

            return authorization;
        }
        #endregion

        #region HMAC_SHA256
        public static byte[] HMAC_SHA256(byte[] key, string msg)
        {
            using (var hmacsha256 = new HMACSHA256(key))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(msg));
                return hashmessage;
            }

        }
        #endregion
        #endregion
    }
}
