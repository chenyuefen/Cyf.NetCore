using COSXML;
using COSXML.Auth;
using COSXML.Model.Bucket;
using COSXML.Model.Object;
using COSXML.Model.Service;
using COSXML.Model.Tag;
using COSXML.Utils;
using System;
using System.Collections.Generic;
using TM.Infrastructure.Configs;
using TM.Infrastructure.CosCloud.Dto;
using TM.Infrastructure.Extensions.Common;
using TM.Infrastructure.TencentCloud.Config;

namespace TM.Infrastructure.TencentCloud.Api
{
    /// <summary>
    /// 对象云存储操作方法
    /// </summary>
    public static class CosApiV2
    {
        /// <summary>
        /// 腾讯云账户的账户标识 APPID
        /// </summary>
        public static string AppId = ConfigHelper.Configuration["TencentQCloud:AppId"];

        /// <summary>
        ///  云API 密钥 SecretId
        /// </summary>
        public static string SecretId = ConfigHelper.Configuration["TencentQCloud:SecretId"];

        /// <summary>
        /// 云 API 密钥 SecretKey
        /// </summary>
        public static string SecretKey = ConfigHelper.Configuration["TencentQCloud:SecretKey"];

        #region 初始化云存储基本配置
        public static CosXml InitializeCosConfig()
        {
            CosXmlConfig config = new CosXmlConfig.Builder()
              .SetConnectionTimeoutMs(60000)  //设置连接超时时间，单位毫秒，默认45000ms
              .SetReadWriteTimeoutMs(40000)  //设置读写超时时间，单位毫秒，默认45000ms
              .IsHttps(true)  //设置默认 HTTPS 请求
              .SetAppid(AppId) //设置腾讯云账户的账户标识 APPID
              .SetRegion(CosConfig.REGION) //设置一个默认的存储桶地域
              .Build();

            long durationSecond = 600;          //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider qCloudCredentialProvider = new DefaultQCloudCredentialProvider(SecretId,
              SecretKey, durationSecond);

            CosXml cosXml = new CosXmlServer(config, qCloudCredentialProvider);

            return cosXml;
        }
        #endregion

        #region 文件Url拆分,返回Object Key
        public static string ReturnObjectKey(string url)
        {
            var keyPath1 = $"https://{ CosConfig.DEFAULT_BUCKET}-{AppId}.cos.{CosConfig.REGION}.myqcloud.com";
            var keyPath2 = url.Split(keyPath1)[1];
            return keyPath2;
        }
        #endregion

        #region 创建存储桶
        /// <summary>
        /// 存储桶格式 [自定义]-[APPID]
        /// </summary>
        public static CosResultInfoResponseDto CreateBucket(string bucketName)
        {
            var cosXml = InitializeCosConfig();
            var response = new CosResultInfoResponseDto();
            try
            {
                string bucket = $"{bucketName}-{AppId}"; //格式：BucketName-APPID
                PutBucketRequest request = new PutBucketRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                PutBucketResult result = cosXml.PutBucket(request);
                //请求成功
                //请求成功
                response.statusCode = result.httpCode;
                response.statusMessage = result.httpMessage;
                return response;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                response.statusCode = clientEx.errorCode;
                response.statusMessage = clientEx.Message;
                response.info = clientEx.ToJson();
                return response;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                response.statusCode = serverEx.statusCode;
                response.statusMessage = serverEx.statusMessage;
                response.info = serverEx.GetInfo();
                return response;
            }
        }
        #endregion

        #region 检索存储桶是否存在且是否有权限访问
        /// <summary>
        /// 检索存储桶是否存在且是否有权限访问
        /// </summary>
        /// <param name="bucketName">存储桶，格式：BucketName-APPID</param>
        public static CosResultInfoResponseDto CheckBucket(string bucketName = CosConfig.DEFAULT_BUCKET)
        {
            var cosXml = InitializeCosConfig();
            var response = new CosResultInfoResponseDto();
            try
            {
                string bucket = $"{bucketName}-{AppId}"; //格式：BucketName-APPID
                HeadBucketRequest request = new HeadBucketRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                HeadBucketResult result = cosXml.HeadBucket(request);
                //请求成功
                response.statusCode = result.httpCode;
                response.statusMessage = result.httpMessage;
                return response;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                response.statusCode = clientEx.errorCode;
                response.statusMessage = clientEx.Message;
                response.info = clientEx.ToJson();
                return response;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                response.statusCode = serverEx.statusCode;
                response.statusMessage = serverEx.statusMessage;
                response.info = serverEx.GetInfo();
                return response;
            }
        }
        #endregion

        #region 查询存储桶列表
        public static CosListAllMyBucketsResponseDto QueryBucketList()
        {
            var cosXml = InitializeCosConfig();
            var response = new CosListAllMyBucketsResponseDto()
            {
                BucketsList = new List<ListAllMyBuckets.Bucket>()
            };
            try
            {
                GetServiceRequest request = new GetServiceRequest();
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetServiceResult result = cosXml.GetService(request);
                //得到所有的 buckets
                response.statusCode = result.httpCode;
                response.statusMessage = result.httpMessage;
                response.BucketsList = result.listAllMyBuckets.buckets; ;
                return response;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                response.statusCode = clientEx.errorCode;
                response.statusMessage = clientEx.Message;
                response.info = clientEx.ToJson();
                return response;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                response.statusCode = serverEx.statusCode;
                response.statusMessage = serverEx.statusMessage;
                response.info = serverEx.GetInfo();
                return response;
            }
        }
        #endregion

        #region 简单上传对象
        /// <summary>
        /// 简单上传对象
        /// 上传一个对象至存储桶。最大支持上传不超过5GB的对象
        /// </summary>
        /// <param name="key">对象在存储桶中的位置，即称对象键(也可以理解为在存储桶路径)</param>
        /// <param name="data">图片/视频 byte[]数据</param>
        /// <param name="bucketName">存储桶，格式：BucketName-APPID</param>
        public static CosResultInfoResponseDto UploadObject(string key, byte[] data, string bucketName = CosConfig.DEFAULT_BUCKET)
        {
            var cosXml = InitializeCosConfig();
            var response = new CosResultInfoResponseDto();
            try
            {
                string bucket = $"{bucketName}-{AppId}"; //存储桶，格式：BucketName-APPID";
                PutObjectRequest request = new PutObjectRequest(bucket, key, data);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                PutObjectResult result = cosXml.PutObject(request);
                //对象的 eTag
                string eTag = result.eTag;

                //拼接Url
                var cosDataUrl = $"https://{bucket}.cos.{CosConfig.REGION}.myqcloud.com{key}";

                response.statusCode = result.httpCode;
                response.statusMessage = result.httpMessage;
                response.cosDataUrl = cosDataUrl;
                return response;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                response.statusCode = clientEx.errorCode;
                response.statusMessage = clientEx.Message;
                response.info = clientEx.ToJson();
                return response;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                response.statusCode = serverEx.statusCode;
                response.statusMessage = serverEx.statusMessage;
                response.info = serverEx.GetInfo();
                return response;
            }
        }
        #endregion

        #region 查询对象元数据
        /// <summary>
        /// 查询对象元数据
        /// </summary>
        /// <param name="key">对象在存储桶中的位置，即称对象键</param>
        /// <param name="bucketName">存储桶，格式：BucketName-APPID</param>
        public static void QueryObject(string key, string bucketName = CosConfig.DEFAULT_BUCKET)
        {
            var cosXml = InitializeCosConfig();
            try
            {
                string bucket = $"{bucketName}-{AppId}"; //存储桶，格式：BucketName-APPID
                HeadObjectRequest request = new HeadObjectRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                HeadObjectResult result = cosXml.HeadObject(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }
        #endregion

        #region 删除单个对象
        /// <summary>
        /// 删除单个对象
        /// </summary>
        /// <param name="key">对象在存储桶中的位置，即称对象键(也可以理解为在存储桶路径)</param>
        /// <param name="bucketName">存储桶，格式：BucketName-APPID</param>
        public static CosResultInfoResponseDto DeleteObject(string key, string bucketName = CosConfig.DEFAULT_BUCKET)
        {
            var cosXml = InitializeCosConfig();
            var response = new CosResultInfoResponseDto();
            try
            {
                string bucket = $"{bucketName}-{AppId}"; //存储桶，格式：BucketName-APPID";
                DeleteObjectRequest request = new DeleteObjectRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteObjectResult result = cosXml.DeleteObject(request);
                //请求成功
                response.statusCode = result.httpCode;
                response.statusMessage = result.httpMessage;
                return response;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                response.statusCode = clientEx.errorCode;
                response.statusMessage = clientEx.Message;
                response.info = clientEx.ToJson();
                return response;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                response.statusCode = serverEx.statusCode;
                response.statusMessage = serverEx.statusMessage;
                response.info = serverEx.GetInfo();
                return response;
            }
        }
        #endregion
    }
}
