/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：VodApiV2
// 文件功能描述： 服务端上传云点播api
//
// 创建者：庄欣锴
// 创建时间：2020-05-06 17:11:00
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
using TM.Infrastructure.TencentCloud.Util;

namespace TM.Infrastructure.TencentCloud.Api
{
    /// <summary>
    /// 云点播操作接口
    /// </summary>
    public static class VodApiV2
    {
        private const string baseApi = "https://vod.tencentcloudapi.com/";
        private const string hostName = "vod.tencentcloudapi.com";
        private const string serviceName = "vod";

        #region 拉取上传云点播

        public static async Task<BaseTencentRespnse> VodPullUploadAsync(VodPullUploadRequest dto)
        {
            var _baseApi = "https://vod.tencentcloudapi.com/";
            var action = "PullUpload";//公共参数，本接口取值：PullUpload。
            var version = "2018-07-17";//公共参数，本接口取值：2018-07-17。

            //组装url
            var api = new StringBuilder()
                .Append($"{_baseApi}?")
                .Append($"MediaUrl={ Format.UrlEncode(dto.MediaUrl)}")
                .Append($"&MediaName={dto.MediaName}")
                .Append($"&Procedure={VodConfig.ShortVideoProcedure}")
                .Append($"&ClassId={VodConfig.ShortVideoClassId}")
                .ToString();

            //请求公共参数
            var timespan = Time.GetUnixTimestamp();

            //生成签名串
            var authorization = SignV3.CreateAuthorizationSignV3("vod", "vod.tencentcloudapi.com",
                "GET", api.Split("?")[1], JsonConvert.SerializeObject(dto), timespan);

            //初始化restClient
            var client = new RestClient(api);
            var request = new RestRequest(method: Method.GET);

            //设置请求头公共参数
            request.AddHeader("Host", "vod.tencentcloudapi.com");
            request.AddHeader("X-TC-Action", action);
            request.AddHeader("X-TC-RequestClient", "APIExplorer");
            request.AddHeader("X-TC-Timestamp", timespan.ToString());
            request.AddHeader("X-TC-Version", version);
            request.AddHeader("X-TC-Region", VodConfig.REGION);
            request.AddHeader("X-TC-Language", "zh-CN");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", authorization);

            //发起请求
            var response = await client.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<BaseTencentRespnse>(response.Content);
            return responseData;

        }

        #endregion

        #region 拉取事件通知

        public static async Task<PullEventsResponseDto> PullEventsAsync()
        {
            const string action = "PullEvents";
            const string version = "2018-07-17";

            //组装Url
            var api = new StringBuilder()
                .Append($"{baseApi}?")
                .ToString();

            return await TCRestClientUtil.TCGetRequst<PullEventsResponseDto>(serviceName,
                hostName, api.Split("?")[1], api, action, version);
        }

        #endregion

        #region 确认事件通知



        #endregion
    }
}
