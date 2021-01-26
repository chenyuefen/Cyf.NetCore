using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TM.Infrastructure.Tencent.Module;
using System.Threading.Tasks;

namespace TM.Infrastructure.Tencent
{
    public class MapApi
    {
        private const string Key = "XW5BZ-HNLW4-UHGU4-XJRG6-KDJCO-EIBSK";

        /// <summary>
        /// 地址转坐标url
        /// </summary>
        private const string geocoderUrl = "https://apis.map.qq.com/ws/geocoder/v1/";

        /// <summary>
        /// 批量距离计算 -距离矩阵
        /// </summary>
        private const string matrixUrl = "https://apis.map.qq.com/ws/distance/v1/matrix";

        /// <summary>
        /// 地址转坐标
        /// location 和 address  其中一个必填
        /// </summary>
        /// <param name="location">位置坐标</param>
        /// <param name="address">地址（注：地址中请包含城市名称，否则会影响解析效果）</param>
        /// <param name="address">指定地址所属城市</param>
        /// <returns></returns>
        public static async Task<MapResponse<GeocoderResult>> GetLngLat(Location location = null, string address = "", string region = "")
        {
            var data = new Dictionary<string, string>();
            if (location != null)
                data.Add("location", $"{location.lat},{location.lng}");
            if (!string.IsNullOrWhiteSpace(address))
                data.Add("address", address);
            if (!string.IsNullOrWhiteSpace(region))
                data.Add("region", region);
            return await SendMapRequest<GeocoderResult>(data, geocoderUrl);
        }

        /// <summary>
        /// 批量距离计算 -距离矩阵
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static async Task<MapResponse<DistanceResult>> GetDistance(List<Location> from, List<Location> to, TrafficToolsEnum mode = TrafficToolsEnum.walking)
        {
            var data = new Dictionary<string, string>();
            data.Add("mode", mode.ToString());
            data.Add("from", string.Join(";", from.Select(p=>$"{p.lat},{p.lng}")));
            data.Add("to", string.Join(";", to.Select(p => $"{p.lat},{p.lng}")));

            return await SendMapRequest<DistanceResult>(data, matrixUrl);
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<MapResponse<T>> SendMapRequest<T>(Dictionary<string, string> data, string url)
        {
            if (data == null)
                data = new Dictionary<string, string>();
            data.Add("key", Key);

            url += "?";
            foreach (var item in data)
                url += item.Key + "=" + item.Value + "&";
            url = url.TrimEnd('&');

            var client = new RestClient(url);
            var request = new RestRequest(method: Method.GET);

            var response = await client.ExecuteAsync(request);
            var content = JsonConvert.DeserializeObject<MapResponse<T>>(response.Content);

            return content;
        }
    }
}
