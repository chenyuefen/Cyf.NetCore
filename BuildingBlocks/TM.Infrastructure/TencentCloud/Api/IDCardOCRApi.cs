/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：IDCardOCRApi
// 文件功能描述： 身份证识别
//
// 创建者：庄欣锴
// 创建时间：2020-05-09 00:43:00
// 
//----------------------------------------------------------------*/

using Newtonsoft.Json;
using RestSharp;
using System.Text;
using System.Threading.Tasks;
using TM.Infrastructure.Helpers;
using TM.Infrastructure.TencentCloud.Common;
using TM.Infrastructure.TencentCloud.Config;
using TM.Infrastructure.TencentCloud.Dto;

namespace TM.Infrastructure.TencentCloud.Api
{
    public static class IDCardOCRApi
    {
        /// <summary>
        /// 校验身份证
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static async Task<IDCardIdentificationResponse> CheckIDCard(IDCardIdentificationBaseRequest dto)
        {
            /**
             * //https://cloud.tencent.com/document/product/866/33524 
             * 身份证识别 接口文档
             **/
            var baseUrl = "https://ocr.tencentcloudapi.com/";
            var action = "IDCardOCR";//公共参数，本接口取值：IDCardOCR。 必选:是
            var version = "2018-11-19";//公共参数，本接口取值：2018-11-19。 必选:是
            var region = CosConfig.REGION;//公共参数，详见产品支持的 地域列表 必选:是
            var _imageUrl = Format.UrlEncode(dto.ImageUrl);//图片的 Url 地址。要求图片经Base64编码后不超过 7M，建议图片存储于腾讯云，可保障更高的下载速度和稳定性
            var _cardSide = dto.CardSide;//FRONT：身份证有照片的一面（人像面），BACK：身份证有国徽的一面（国徽面），该参数如果不填，将为您自动判断身份证正反面。
            var coinfigDto = new IDCardIdentificationConfig()
            {
                CropIdCard = true,
                CropPortrait = true,
                CopyWarn = true,
                BorderCheckWarn = true,
                ReshootWarn = true,
                DetectPsWarn = true,
                TempIdWarn = true,
                InvalidDateWarn = true,
                Quality = true
            };
            var config = JsonConvert.SerializeObject(coinfigDto);

            //请求接口
            var api = new StringBuilder()
                .Append($"{baseUrl}?")
                .Append($"ImageUrl={_imageUrl}")
                .Append($"{(Regexs.IsNull(_cardSide) ? "" : $"&CardSide={_cardSide}")}")
                //.Append($"{(Regexs.IsNull(config)?"":$"&Config={config}")}")
                .ToString();

            //请求公共参数
            var timespan = Time.GetUnixTimestamp();
            //生成签名串
            var authorization = SignV3.CreateAuthorizationSignV3("ocr", "ocr.tencentcloudapi.com", "GET", api.Split("?")[1], JsonConvert.SerializeObject(dto), timespan);

            //初始化restClient
            var client = new RestClient(api);
            var request = new RestRequest(method: Method.GET);

            //设置请求头公共参数
            request.AddHeader("Host", "ocr.tencentcloudapi.com");
            request.AddHeader("X-TC-Action", action);
            request.AddHeader("X-TC-RequestClient", "APIExplorer");
            request.AddHeader("X-TC-Timestamp", timespan.ToString());
            request.AddHeader("X-TC-Version", version);
            request.AddHeader("X-TC-Region", region);
            request.AddHeader("X-TC-Language", "zh-CN");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", authorization);

            //发起请求
            IRestResponse response = await client.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<IDCardIdentificationResponse>(response.Content);
            return responseData;
        }

        /// <summary>
        /// 返回新的错误描述
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static string GetErrorMsgByErrCode(string errorCode)
        {
            var msg = "";
            switch (errorCode)
            {
                case "AuthFailure.SignatureFailure": msg = "签名计算错误。"; break;
                case "FailedOperation.DownLoadError": msg = "文件下载失败。"; break;
                case "FailedOperation.EmptyImageError": msg = "身份证图片内容为空。"; break;
                case "FailedOperation.ImageBlur": msg = "身份证图片模糊。"; break;
                case "FailedOperation.ImageDecodeFailed": msg = "身份证图片解码失败。"; break;
                case "FailedOperation.ImageNoIdCard": msg = "图片中未检测到身份证。"; break;
                case "FailedOperation.ImageSizeTooLarge": msg = "图片尺寸过大。"; break;
                case "FailedOperation.OcrFailed": msg = "身份证识别失败。"; break;
                case "FailedOperation.UnKnowError": msg = "未知错误。"; break;
                case "FailedOperation.UnOpenError": msg = "服务未开通。"; break;
                case "FailedOperation.ConfigFormatError": msg = "Config不是有效的JSON格式。"; break;
                case "FailedOperation.InvalidParameterValueLimit": msg = "参数值错误。"; break;
                case "FailedOperation.TooLargeFileError": msg = "文件内容太大。"; break;
                case "FailedOperation.ChargeStatusException": msg = "计费状态异常。"; break;
                default:
                    break;
            }

            return msg;
        }
    }
}
