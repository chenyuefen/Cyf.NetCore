
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：LiveApi
// 文件功能描述： 云直播服务端操作接口
//
// 创建者：庄欣锴
// 创建时间：2020-05-06 17:11:00
// 
//----------------------------------------------------------------*/

using System;
using System.Text;
using System.Threading.Tasks;
using TM.Infrastructure.TencentCloud.Config;
using TM.Infrastructure.TencentCloud.Dto;
using TM.Infrastructure.TencentCloud.Util;

namespace TM.Infrastructure.TencentCloud.Api
{
    public class CloudLiveApi
    {
        private const string baseApi = "https://mall.tencentcloudapi.com/";
        private const string hostName = "mall.tencentcloudapi.com";

        #region 断开直播流

        public static async Task<BaseTencentRespnseV2> DropLiveStreamAsync(DropLiveStreamRequestDto dto)
        {
            const string action = "DropLiveStream";
            const string version = "2018-08-01";

            //组装Url
            var api = new StringBuilder()
                .Append($"{baseApi}?")
                .Append($"DomainName={CloudLiveConfig.LivePushDomain}")
                .Append($"&AppName=live")
                .Append($"&StreamName={dto.StreamName}")
                .ToString();

            return await TCRestClientUtil.TCGetRequst<BaseTencentRespnseV2>("live",
                hostName, api.Split("?")[1], api, action, version);

        }

        #endregion


        #region 禁推直播流

        public static async Task<BaseTencentRespnseV2> ForbidLiveStreamAsync(ForbidLiveStreamRequestDto dto)
        {
            const string action = "ForbidLiveStream";
            const string version = "2018-08-01";
            var resumeTime = dto.ResumeTime?.ToString("YYYY-MM-DDThh:mm:ssZ")
                             ?? DateTime.Now.AddDays(7).ToString("YYYY-MM-DDThh:mm:ssZ");
            //组装Url
            var api = new StringBuilder()
                .Append($"{baseApi}?")
                .Append($"DomainName={CloudLiveConfig.LivePushDomain}")
                .Append($"&AppName=live")
                .Append($"&StreamName={dto.StreamName}")
                .Append($"&ResumeTime={resumeTime}")
                .Append($"$Reason={dto.Reason}")
                .ToString();

            return await TCRestClientUtil.TCGetRequst<BaseTencentRespnseV2>("live",
                hostName, api.Split("?")[1], api, action, version);
        }

        #endregion

        #region 恢复直播推流

        public static async Task<BaseTencentRespnseV2> ResumeLiveStreamAsync(ResumeLiveStreamRequestDto dto)
        {
            const string action = "ResumeLiveStream";
            const string version = "2018-08-01";

            //组装Url
            var api = new StringBuilder()
                .Append($"{baseApi}?")
                .Append($"DomainName={CloudLiveConfig.LivePushDomain}")
                .Append($"&AppName=live")
                .Append($"&StreamName={dto.StreamName}")
                .ToString();

            return await TCRestClientUtil.TCGetRequst<BaseTencentRespnseV2>("live",
                hostName, api.Split("?")[1], api, action, version);
        }

        #endregion
    }
}
