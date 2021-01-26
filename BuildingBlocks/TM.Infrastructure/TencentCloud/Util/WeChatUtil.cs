/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：WeChatUtil
// 文件功能描述: 微信支付工具类
//
// 创建者：庄欣锴
// 创建时间：2020年5月22日18:16:38
0// 
//----------------------------------------------------------------*/

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TM.Infrastructure.TencentCloud.Api;
using TM.Infrastructure.TencentCloud.Config;

public class WeChatUtil
{
    /// <summary>
    /// 生成微信 字典排序的 xml格式字符串
    /// </summary>
    /// <param name="sdo">C#内置对象，默认排序</param>
    /// <returns></returns>
    public static string WeChatSignXml(SortedDictionary<string, object> sdo)
    {
        StringBuilder sXML = new StringBuilder("<xml>");
        foreach (var dr in sdo)
        {
            sXML.AppendFormat("<{0}>{1}</{0}>", dr.Key, dr.Value);
        }

        sXML.Append("</xml>");
        return sXML.ToString();
    }

    /// <summary>
    /// 获取 签名 MD5加密字符串
    /// </summary>
    /// <param name="sd"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string CreateSign(SortedDictionary<string, object> sd, string key)
    {
        var dic = sd.OrderBy(d => d.Key).ToDictionary(d => d.Key, d => d.Value);
        var sign = dic.Aggregate("", (current, d) => current + (d.Key + "=" + d.Value + "&"));
        sign += "key=" + key;
        return GetMD5(sign, "UTF-8");
    }

    /// <summary>  
    /// 获取时间戳  
    /// </summary>  
    /// <returns></returns>  
    public static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    public static string getNoncestr()
    {
        Random random = new Random();
        return GetMD5(random.Next(1000).ToString(), "UTF-8");
    }

    /// <summary>
    /// MD5
    /// </summary>
    /// <param name="encypStr"></param>
    /// <param name="charset"></param>
    /// <returns></returns>
    public static string GetMD5(string encypStr, string charset)
    {
        string retStr;
        MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

        //创建md5对象
        byte[] inputBye;
        byte[] outputBye;

        //使用GB2312编码方式把字符串转化为字节数组．
        try
        {
            inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
        }
        catch (Exception ex)
        {
            inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
        }

        outputBye = m5.ComputeHash(inputBye);

        retStr = System.BitConverter.ToString(outputBye);
        retStr = retStr.Replace("-", "").ToUpper();
        return retStr;
    }

    #region 微信证书post请求

    /// <summary>
    /// 微信证书post请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="param"></param>
    /// <param name="path">证书路径</param>
    /// <param name="key">证书密码</param>
    /// <returns></returns>
    public static string WxCerHttpPost(string url, string param, string path, string key)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.Accept = "*/*";
        request.Timeout = 15000;
        request.AllowAutoRedirect = false;

        ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback(CheckValidationResult);
        X509Certificate cer = new X509Certificate2(path, key);
        request.ClientCertificates.Add(cer);

        StreamWriter requestStream = null;
        WebResponse response = null;
        string responseStr = null;

        try
        {
            requestStream = new StreamWriter(request.GetRequestStream());
            requestStream.Write(param);
            requestStream.Close();

            response = request.GetResponse();
            if (response != null)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                responseStr = reader.ReadToEnd();
                reader.Close();
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            request = null;
            requestStream = null;
            response = null;
        }

        return responseStr;
    }

    #endregion

    #region 采用标准RSA算法加密

    /// <summary>
    /// 采用标准RSA算法
    /// </summary>
    /// <param name="EncryptString"></param>
    /// <returns></returns>
    public async static Task<string> RSAEncrypt(string EncryptString)
    {
        // var pempath = $"{Environment.CurrentDirectory}/{ConfigHelper.Configuration["WeChatPay:RsaPublicKey"]}";
        if (!File.Exists(WxPayConfig.pempath))
        {
            var PublicKey = await WxPayApi.Getpublickey();
            if (!string.IsNullOrWhiteSpace(PublicKey))
            {
                File.WriteAllText(WxPayConfig.pempath, PublicKey);
            }
            else
                return "获取公钥失败！";
        }

        string R;
        // 加载公钥
        RsaKeyParameters pubkey;
        using (var sr = new StreamReader(WxPayConfig.pempath))
        {
            var pemReader = new PemReader(sr);
            pubkey = (RsaKeyParameters)pemReader.ReadObject();
        }

        // 初始化cipher
        var cipher =
            (BufferedAsymmetricBlockCipher)CipherUtilities.GetCipher("RSA/ECB/OAEPWITHSHA-1ANDMGF1PADDING");
        cipher.Init(true, pubkey);

        // 加密message
        var message = Encoding.UTF8.GetBytes(EncryptString);
        var output = EncryptUtil.Encrypt(message, cipher);
        R = Convert.ToBase64String(output);
        return R;
    }

    #endregion

    private static bool CheckValidationResult(object sender,
        X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        //if (errors == SslPolicyErrors.None)
        return true;
        //return false;
    }
}