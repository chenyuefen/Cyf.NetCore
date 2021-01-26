using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TM.Infrastructure.Tencent.Module
{
    public enum MapEnum
    {
    }

    public enum TrafficToolsEnum
    {
        /// <summary>
        /// 驾车
        /// </summary>
        [DescriptionAttribute("驾车")]
        driving = 0,
        /// <summary>
        /// 步行
        /// </summary>
        [DescriptionAttribute("步行")]
        walking = 1,
        /// <summary>
        /// 自行车
        /// </summary>
        [DescriptionAttribute("自行车")]
        bicycling = 2
    }
}
