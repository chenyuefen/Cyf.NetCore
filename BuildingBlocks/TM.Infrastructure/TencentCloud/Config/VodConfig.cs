
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：VodConfig
// 文件功能描述：
//  云点播配置
// 创建者：庄欣锴
// 创建时间：2020年5月27日
// 
//----------------------------------------------------------------*/

using System;
using TM.Infrastructure.Configs;

namespace TM.Infrastructure.TencentCloud.Config
{
    public class VodConfig : BaseTencentConfig
    {
        /// <summary>
        /// 云点播短视频视频分类ID
        /// </summary>
        public static int ShortVideoClassId = Convert.ToInt32(ConfigHelper.Configuration["CloudVod:SVClassId"]);

        /// <summary>
        /// 云点播短视频任务流名称
        /// </summary>
        public static string ShortVideoProcedure = ConfigHelper.Configuration["CloudVod:SVProcedure"];
    }
}