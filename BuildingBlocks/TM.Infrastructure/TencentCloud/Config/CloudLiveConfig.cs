
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：CloudLiveConfig
// 文件功能描述：
// 
// 创建者：庄欣锴
// 创建时间：2020年6月6日
// 
//----------------------------------------------------------------*/

using TM.Infrastructure.Configs;

namespace TM.Infrastructure.TencentCloud.Config
{
    public class CloudLiveConfig
    {
        /// <summary>
        /// 直播推流域名
        /// </summary>
        public static string LivePushDomain = ConfigHelper.Configuration["CloudLive:LivePushDomain"];
    }
}