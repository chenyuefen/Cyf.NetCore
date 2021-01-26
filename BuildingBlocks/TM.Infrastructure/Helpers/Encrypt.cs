using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TM.Infrastructure.Extensions.Common;
using TM.Infrastructure.Helpers.Internal;

namespace TM.Infrastructure.Helpers
{
    /// <summary>
    /// 加密操作
    /// 说明：
    /// 1、AES加密整理自支付宝SDK
    /// 2、RSA加密采用 https://github.com/stulzq/DotnetCore.RSA/blob/master/DotnetCore.RSA/RSAHelper.cs
    /// </summary>
    public static class Encrypt
    {
        #region Md5加密

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Md5By16(string value)
        {
            return Md5By16(value, Encoding.UTF8);
        }

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Md5By16(string value, Encoding encoding)
        {
            return Md5(value, encoding, 4, 8);
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Md5By32(string value)
        {
            return Md5By32(value, Encoding.UTF8);
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Md5By32(string value, Encoding encoding)
        {
            return Md5(value, encoding, null, null);
        }

        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        private static string Md5(string value, Encoding encoding, int? startIndex, int? length)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            var md5 = new MD5CryptoServiceProvider();
            string result;
            try
            {
                var hash = md5.ComputeHash(encoding.GetBytes(value));
                result = startIndex == null
                    ? BitConverter.ToString(hash)
                    : BitConverter.ToString(hash, startIndex.SafeValue(), length.SafeValue());
            }
            finally
            {
                md5?.Clear();
                md5?.Dispose();
            }
            return result.Replace("-", "");
        }

        #endregion

        #region DES加密

        /// <summary>
        /// DES密钥，24位字符串
        /// </summary>
        public static string DesKey = "vs9#s^un21fcv%|f0XpR,+vh";

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <returns></returns>
        public static string DesEncrypt(object value)
        {
            return DesEncrypt(value, DesKey);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥，24位</param>
        /// <returns></returns>
        public static string DesEncrypt(object value, string key)
        {
            return DesEncrypt(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥，24位</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string DesEncrypt(object value, string key, Encoding encoding)
        {
            var text = value.SafeString();
            if (ValidateDes(text, key) == false)
            {
                return string.Empty;
            }
            using (var transform = CreateDesProvider(key).CreateEncryptor())
            {
                return GetEncryptResult(text, encoding, transform);
            }
        }

        /// <summary>
        /// 验证Des加密参数
        /// </summary>
        /// <param name="text">待加密的文本</param>
        /// <param name="key">密钥，24位</param>
        /// <returns></returns>
        private static bool ValidateDes(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            return key.Length == 24;
        }

        /// <summary>
        /// 创建Des加密服务提供程序
        /// </summary>
        /// <param name="key">密钥，24位</param>
        /// <returns></returns>
        private static TripleDESCryptoServiceProvider CreateDesProvider(string key)
        {
            return new TripleDESCryptoServiceProvider()
            {
                Key = Encoding.ASCII.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
        }

        /// <summary>
        /// 获取加密结果
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="transform">加密器</param>
        /// <returns></returns>
        private static string GetEncryptResult(string value, Encoding encoding, ICryptoTransform transform)
        {
            var bytes = encoding.GetBytes(value);
            var result = transform.TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <returns></returns>
        public static string DesDecrypt(object value)
        {
            return DesDecrypt(value, DesKey);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="key">密钥，24位</param>
        /// <returns></returns>
        public static string DesDecrypt(object value, string key)
        {
            return DesDecrypt(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="key">密钥，24位</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string DesDecrypt(object value, string key, Encoding encoding)
        {
            var text = value.SafeString();
            if (!ValidateDes(text, key))
            {
                return string.Empty;
            }

            using (var transform = CreateDesProvider(key).CreateDecryptor())
            {
                return GetDecryptResult(text, encoding, transform);
            }
        }

        /// <summary>
        /// 获取解密结果
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="transform">加密器</param>
        /// <returns></returns>
        private static string GetDecryptResult(string value, Encoding encoding, ICryptoTransform transform)
        {
            var bytes = Convert.FromBase64String(value);
            var result = transform.TransformFinalBlock(bytes, 0, bytes.Length);
            return encoding.GetString(result);
        }

        #endregion

        #region AES加密

        /// <summary>
        /// 128位0向量
        /// </summary>
        private static byte[] _iv;

        /// <summary>
        /// 128位0向量
        /// </summary>
        private static byte[] Iv
        {
            get
            {
                if (_iv == null)
                {
                    var size = 16;
                    _iv = new byte[size];
                    for (int i = 0; i < size; i++)
                    {
                        _iv[i] = 0;
                    }
                }
                return _iv;
            }
        }

        /// <summary>
        /// AES密钥
        /// </summary>
        public static string AesKey = "QszaP1AF8utIarcBqdhYTZpVGbiNQ9M6IL";

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <returns></returns>
        public static string AesEncrypt(string value)
        {
            return AesEncrypt(value, AesKey);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string AesEncrypt(string value, string key)
        {
            return AesEncrypt(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string AesEncrypt(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            var rijndaelManaged = CreateRijndaelManaged(key);
            using (var transform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV))
            {
                return GetEncryptResult(value, encoding, transform);
            }
        }

        /// <summary>
        /// 创建RijndaelManaged
        /// </summary>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        private static RijndaelManaged CreateRijndaelManaged(string key)
        {
            return new RijndaelManaged()
            {
                Key = Convert.FromBase64String(key),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                IV = Iv
            };
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <returns></returns>
        public static string AesDecrypt(string value)
        {
            return AesDecrypt(value, AesKey);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string AesDecrypt(string value, string key)
        {
            return AesDecrypt(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value">待解密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string AesDecrypt(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            var rijndaelManaged = CreateRijndaelManaged(key);
            using (var transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV))
            {
                return GetDecryptResult(value, encoding, transform);
            }
        }

        #endregion

        #region RSA签名

        /// <summary>
        /// RSA签名，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        /// <returns></returns>
        public static string RsaSign(string value, string key)
        {
            return RsaSign(value, key, Encoding.UTF8);
        }
        /// <summary>
        /// RSA签名，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string RsaSign(string value, string key, Encoding encoding)
        {
            return RsaSign(value, key, encoding, RSAType.RSA);
        }
        /// <summary>
        /// RSA签名，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        /// <returns></returns>
        public static string Rsa2Sign(string value, string key)
        {
            return Rsa2Sign(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// RSA签名，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Rsa2Sign(string value, string key, Encoding encoding)
        {
            return RsaSign(value, key, encoding, RSAType.RSA2);
        }

        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="type">RSA算法类型</param>
        /// <returns></returns>
        private static string RsaSign(string value, string key, Encoding encoding, RSAType type)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            var rsa = new RsaHelper(type, encoding, key);
            return rsa.Sign(value);
        }

        /// <summary>
        /// RSA验签，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static bool RsaVerify(string value, string publicKey, string sign)
        {
            return RsaVerify(value, publicKey, sign, Encoding.UTF8);
        }

        /// <summary>
        /// RSA验签，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static bool RsaVerify(string value, string publicKey, string sign, Encoding encoding)
        {
            return RsaVerify(value, publicKey, sign, encoding, RSAType.RSA);
        }

        /// <summary>
        /// RSA验签，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static bool Rsa2Verify(string value, string publicKey, string sign)
        {
            return Rsa2Verify(value, publicKey, sign, Encoding.UTF8);
        }

        /// <summary>
        /// RSA验签，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static bool Rsa2Verify(string value, string publicKey, string sign, Encoding encoding)
        {
            return RsaVerify(value, publicKey, sign, encoding, RSAType.RSA2);
        }

        /// <summary>
        /// RSA验签
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="type">RSA算法类型</param>
        /// <returns></returns>
        private static bool RsaVerify(string value, string publicKey, string sign, Encoding encoding, RSAType type)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            var rsa = new RsaHelper(type, encoding, publicKey: publicKey);
            return rsa.Verify(value, sign);
        }

        #endregion

        #region HmacMd5加密

        /// <summary>
        /// HMACMD5加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string HmacMd5(string value, string key)
        {
            return HmacMd5(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// HMACMD5加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string HmacMd5(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            var md5 = new HMACMD5(encoding.GetBytes(key));
            var hash = md5.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region HmacSha1加密

        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string HmacSha1(string value, string key)
        {
            return HmacSha1(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string HmacSha1(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            var sha1 = new HMACSHA1(encoding.GetBytes(key));
            var hash = sha1.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region HmacSha256加密

        /// <summary>
        /// HMACSHA256加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string HmacSha256(string value, string key)
        {
            return HmacSha256(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// HMACSHA256加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string HmacSha256(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            var sha256 = new HMACSHA256(encoding.GetBytes(key));
            var hash = sha256.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region HmacSha384加密

        /// <summary>
        /// HMACSHA384加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string HmacSha384(string value, string key)
        {
            return HmacSha384(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// HMACSHA384加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string HmacSha384(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            var sha384 = new HMACSHA384(encoding.GetBytes(key));
            var hash = sha384.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region HmacSha512加密

        /// <summary>
        /// HMACSHA512加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string HmacSha512(string value, string key)
        {
            return HmacSha512(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// HMACSHA512加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string HmacSha512(string value, string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }
            var sha512 = new HMACSHA512(encoding.GetBytes(key));
            var hash = sha512.ComputeHash(encoding.GetBytes(value));
            return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
        }

        #endregion

        #region SHA1加密

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Sha1(string value)
        {
            return Sha1(value, Encoding.UTF8);
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Sha1(string value, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(encoding.GetBytes(value));
                return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
            }
        }

        #endregion

        #region SHA256加密

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Sha256(string value)
        {
            return Sha256(value, Encoding.UTF8);
        }

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Sha256(string value, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(encoding.GetBytes(value));
                return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
            }
        }

        public static string Sha256X16(string value)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(value);
            SHA256 sha256 = new SHA256Managed();
            sha256.ComputeHash(clearBytes);
            byte[] hashedBytes = sha256.Hash;
            sha256.Clear();
            string output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return output;
        }

        #endregion

        #region SHA384加密

        /// <summary>
        /// SHA384加密
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Sha384(string value)
        {
            return Sha384(value, Encoding.UTF8);
        }

        /// <summary>
        /// SHA384加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Sha384(string value, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            using (var sha = SHA384.Create())
            {
                var hash = sha.ComputeHash(encoding.GetBytes(value));
                return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
            }
        }

        #endregion

        #region SHA512加密

        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Sha512(string value)
        {
            return Sha512(value, Encoding.UTF8);
        }

        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Sha512(string value, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            using (var sha = SHA512.Create())
            {
                var hash = sha.ComputeHash(encoding.GetBytes(value));
                return string.Join("", hash.ToList().Select(x => x.ToString("x2")).ToArray());
            }
        }

        #endregion

        #region Base64加密

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Base64Encrypt(string value)
        {
            return Base64Encrypt(value, Encoding.UTF8);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Base64Encrypt(string value, Encoding encoding)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : Convert.ToBase64String(encoding.GetBytes(value));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Base64Decrypt(string value)
        {
            return Base64Decrypt(value, Encoding.UTF8);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Base64Decrypt(string value, Encoding encoding)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : encoding.GetString(Convert.FromBase64String(value));
        }

        #endregion

        #region Base62数值编解码

        //编码,可加一些字符也可以实现72,96等任意进制转换，但是有符号数据不直观，会影响阅读。
        private static String basekeys = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static int exponent = basekeys.Length;//幂数

        /// <summary>
        /// EncodeBase62(Base62数值编码)
        /// </summary>
        /// <param name="value">The max value can not more decimal.MaxValue<</param>
        /// <returns>Return a specified 62 encode string</returns>
        public static string EncodeBase62(decimal value)
        {
            string result = string.Empty;
            do
            {
                decimal index = value % exponent;
                result = basekeys[(int)index] + result;
                value = (value - index) / exponent;
            }
            while (value > 0);

            return result;
        }


        /// <summary>
        /// DecodeBase62(Base62解码数值)
        /// </summary>
        /// <param name="value">62 encode string</param>
        /// <returns>Return a specified decimal number that decode by 62 string</returns>
        public static decimal DecodeBase62(string value)
        {
            decimal result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                int x = value.Length - i - 1;
                result += basekeys.IndexOf(value[i]) * Pow(exponent, x);// Math.Pow(exponent, x);
            }
            return result;
        }

        /// <summary>
        /// 一个数据的N次方
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static decimal Pow(decimal baseNo, decimal x)
        {
            decimal value = 1;////1 will be the result for any number's power 0.任何数的0次方，结果都等于1
            while (x > 0)
            {
                value = value * baseNo;
                x--;
            }
            return value;
        }

        #endregion

    }
}
