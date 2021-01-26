/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：HttpRequest
// 文件功能描述： 网络请求  这里不需要释放链接
//
// 创建者：冼晓松
// 创建时间：2020-03-17 09:30
// 
//----------------------------------------------------------------*/

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TM.Infrastructure.Json;

namespace TM.Core.HttpClientFactory
{
    /// <summary>
    /// 网络请求
    /// </summary>
    public class HttpRequest : IHttpRequest
    {
        private readonly IHttpClientFactory _clientFactory;
        public HttpRequest(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// 发送get请求
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<TResponse> SendGetAsync<TResponse>(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var client = _clientFactory.CreateClient("live");
                var response = await client.SendAsync(request);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    return default(TResponse);
                }
                if (typeof(TResponse) == typeof(string))
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    return (TResponse)Convert.ChangeType(resultJson, typeof(TResponse));
                }
                else
                {
                    string resultJson = response.Content.ReadAsStringAsync().Result;
                    return resultJson.ToObject<TResponse>();
                }
            }
            catch (Exception ex)
            {
                return default(TResponse);
            }
        }

        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <param name="contentType">application/xml、application/json、application/text、application/x-www-form-urlencoded</param>
        /// <returns></returns>
        public async Task<TResponse> SendPostAsync<TRequest, TResponse>(string url, TRequest data, string token = null, string contentType = "application/json")
        {
            try
            {
                var _client = _clientFactory.CreateClient();
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;
                if (typeof(TRequest) == typeof(string))
                {
                    using (HttpContent httpContent = new StringContent(data == null ? string.Empty : data.ToString(), Encoding.UTF8))
                    {
                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        }
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                        response = await _client.PostAsync(url, httpContent);
                    }
                }
                else
                {
                    using (HttpContent httpContent = new StringContent(data == null ? string.Empty : data.ToJson(), Encoding.UTF8))
                    {
                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        }
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                        response = await _client.PostAsync(url, httpContent);
                    }
                }
                if (response == null || !response.IsSuccessStatusCode)
                {
                    return default(TResponse);
                }
                if (typeof(TResponse) == typeof(string))
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    return (TResponse)Convert.ChangeType(resultJson, typeof(TResponse));
                }
                else
                {
                    string resultJson = response.Content.ReadAsStringAsync().Result;
                    return resultJson.ToObject<TResponse>();
                }
            }
            catch (Exception ex)
            {
                return default(TResponse);
            }
        }

        /// <summary>
        /// 检测链接头状态
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetStatusCodeAsync(string url)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await client.SendAsync(request);
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
