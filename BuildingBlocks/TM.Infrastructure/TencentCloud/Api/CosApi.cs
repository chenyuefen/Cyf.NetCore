/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：CosApi
// 文件功能描述： 腾讯云Api
//
// 创建者：冼晓松
// 创建时间：2020-03-17 09:30
// 
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using TM.Infrastructure.TencentCloud.Cos;

namespace TM.Infrastructure.TencentCloud.Api
{
    /// <summary>
    /// 腾讯云Api
    /// </summary>
    public class CosApi
    {
        const string COSAPI_CGI_URL = "http://gz.file.myqcloud.com/files/v2/";
        //文件大于8M时采用分片上传,小于等于8M时采用单文件上传
        const int SLICE_UPLOAD_FILE_SIZE = 8 * 1024 * 1024;
        //用户计算用户签名超时时间
        const int SIGN_EXPIRED_TIME = 180;
        //HTTP请求超时时间
        const int HTTP_TIMEOUT_TIME = 60;
        private string appId;
        private string secretId;
        private string secretKey;
        private int timeOut;
        private Request httpRequest;

        /// <summary>
        /// CosCloudApi 构造方法
        /// </summary>
        /// <param name="appId">授权appid</param>
        /// <param name="secretId">授权secret id</param>
        /// <param name="secretKey">授权secret key</param>
        /// <param name="timeOut">网络超时,默认60秒</param>
        public CosApi(string appId, string secretId, string secretKey, int timeOut = HTTP_TIMEOUT_TIME)
        {
            this.appId = appId;
            this.secretId = secretId;
            this.secretKey = secretKey;
            this.timeOut = timeOut * 1000;
            this.httpRequest = new Request();
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件路径</param>
        /// <returns></returns>
        public async Task<COSResult> GetFileStatAsync(string bucketName, string remotePath)
        {
            var url = generateURL(bucketName, remotePath);
            var data = new Dictionary<string, object>();
            data.Add("op", "stat");
            var sign = Sign.Signature(appId, secretId, secretKey, bucketName);
            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            return await httpRequest.SendRequestStreamAsync(url, data, HttpMethod.Get, header, timeOut);
        }

        /// <summary>
        /// 内部方法：构造URL
        /// </summary>
        /// <returns></returns>
        public string generateURL(string bucketName, string remotePath)
        {
            return COSAPI_CGI_URL + this.appId + "/" + bucketName + HttpUtils.EncodeRemotePath(remotePath);
        }

    }
}
