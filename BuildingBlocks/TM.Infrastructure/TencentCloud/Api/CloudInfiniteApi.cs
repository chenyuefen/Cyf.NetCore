/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：CloudInfiniteApi
// 文件功能描述： 数据万象
// 腾讯云为客户提供的专业一体化的图片解决方案，涵盖图片上传、下载、存储、处理、识别等功能
// 前有图片处理、原图保护、跨域访问设置、样式预设等功能。
// 创建者：庄欣锴
// 创建时间：2020年5月19日14:30:55
// 
//----------------------------------------------------------------*/

using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TM.Infrastructure.Helpers;
using TM.Infrastructure.TencentCloud.Dto;

namespace TM.Infrastructure.TencentCloud.Api
{
    public static class CloudInfiniteApi
    {
        #region 获取图片基本信息
        public static async Task<byte[]> GetImage(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method: Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            return response.RawBytes;
        }

        public static async Task<CIReponseDto.ImageInfoResponse> GetImageInfo(string url, bool isJoin = false, int againCount = 4)
        {
            var c = isJoin ? "|" : "?";
            if (Regexs.IsNull(url)) return null;
            var api = $"{url}{c}imageInfo";
            var client = new RestClient(api);
            var request = new RestRequest(method: Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            try
            {
                var responseData = JsonConvert.DeserializeObject<CIReponseDto.ImageInfoResponse>(response.Content);

                return responseData;
            }
            catch (System.Exception ex)
            {
                if (againCount > 0)
                {
                    System.Threading.Thread.Sleep(2000);
                    againCount--;
                    return await GetImageInfo(url, isJoin, againCount);
                }
                throw ex;
            }
        }

        /// <summary>
        /// 外部Url获取图片基本信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<CIReponseDto.ImageInfoResponse> GetUrlImageInfo(string url)
        {
            if (Regexs.IsNull(url)) return null;
            var client = new RestClient(url);
            var request = new RestRequest(method: Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            using (var ms = new System.IO.MemoryStream(response.RawBytes))
            {
                using (var _image = System.Drawing.Bitmap.FromStream(ms, true))
                {
                    return new CIReponseDto.ImageInfoResponse()
                    {
                        Format = _image.RawFormat.ToString(),
                        Width = _image.Width.ToString(),
                        Height = _image.Height.ToString(),
                        Size = ms.Length.ToString()
                    };
                }
            }
        }

        /// <summary>
        /// 获取文件基本信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static CIReponseDto.ImageInfoResponse GetUrlVideoInfo(string url)
        {
            if (Regexs.IsNull(url)) return null;
            var request = WebRequest.Create(url) as HttpWebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                return new CIReponseDto.ImageInfoResponse()
                {
                    Format = Path.GetExtension(url).TrimStart('.'),
                    Size = response.ContentLength.ToString()
                };
            }
        }
        #endregion

        /// <summary>
        /// 根据原图获取缩略图（加拼接尺寸）
        /// </summary>
        /// <param name="masterUrl"></param>
        /// <returns></returns>
        public static async Task<string> GetThumbUrlAsync(string masterUrl)
        {
            if (string.IsNullOrEmpty(masterUrl))
            {
                return masterUrl;
            }
            var tempThumbUrl = masterUrl;
            if (masterUrl.Contains("#"))
            {
                tempThumbUrl = masterUrl.Split("#")[0];
            }
            tempThumbUrl += "?imageMogr2/thumbnail/!50p";
            var imageInfo = await GetImageInfo(tempThumbUrl, true);
            if (imageInfo != null)
            {
                tempThumbUrl += $"#width={imageInfo.Width}&height={imageInfo.Height}";
            }
            return tempThumbUrl;
        }

        /// <summary>
        /// 主图拼接尺寸
        /// </summary>
        /// <param name="masterUrl"></param>
        /// <returns></returns>
        public static async Task<string> GetMasterUrlAsync(string masterUrl)
        {
            if (string.IsNullOrEmpty(masterUrl))
            {
                return masterUrl;
            }
            var tempUrl = masterUrl;
            if (masterUrl.Contains("#"))
            {
                return tempUrl;
            }
            var imageInfo = await GetImageInfo(masterUrl);
            if (imageInfo != null)
            {
                tempUrl += $"#width={imageInfo.Width}&height={imageInfo.Height}";
            }
            return tempUrl;
        }
    }
}