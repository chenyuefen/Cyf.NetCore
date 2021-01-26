
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：TCRestClientUtil
// 文件功能描述：
// 腾讯云接口请求方法
// 创建者：庄欣锴
// 创建时间：2020年6月7日
// 
//----------------------------------------------------------------*/

using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using TM.Infrastructure.Helpers;
using TM.Infrastructure.TencentCloud.Common;
using TM.Infrastructure.TencentCloud.Config;

namespace TM.Infrastructure.TencentCloud.Util
{
    public class TCRestClientUtil
    {
        /// <summary>
        /// 腾讯云接口 Get 请求
        /// </summary>
        /// <param name="serviceName">接口服务名称</param>
        /// <param name="hostName">接口请求域名</param>
        /// <param name="queryString">url参数</param>
        /// <param name="api">请求url</param>
        /// <param name="action">接口action</param>
        /// <param name="version">接口version</param>
        /// <param name="requestPayload">请求参数</param>
        /// <typeparam name="T">响应参数</typeparam>
        /// <returns></returns>
        public static async Task<T> TCGetRequst<T>(string serviceName, string hostName,
            string queryString, string api, string action, string version,
            string requestPayload = "")
        {
            //请求公共参数
            var timespan = Time.GetUnixTimestamp();

            //生成签名串
            var authorization = SignV3.CreateAuthorizationSignV3(serviceName, hostName,
                "GET", queryString, "", timespan);

            //初始化restClient
            var client = new RestClient(api);
            var request = new RestRequest(method: Method.GET);

            //设置请求头公共参数
            request.AddHeader("Host", hostName);
            request.AddHeader("X-TC-Action", action);
            request.AddHeader("X-TC-RequestClient", "APIExplorer");
            request.AddHeader("X-TC-Timestamp", timespan.ToString());
            request.AddHeader("X-TC-Version", version);
            request.AddHeader("X-TC-Region", BaseTencentConfig.REGION);
            request.AddHeader("X-TC-Language", "zh-CN");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", authorization);
            //发起请求
            var response = await client.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<T>(response.Content);
            return responseData;
        }
    }
}