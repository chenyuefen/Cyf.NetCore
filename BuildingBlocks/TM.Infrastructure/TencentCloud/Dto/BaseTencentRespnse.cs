
/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：BaseTencentRespnse
// 文件功能描述：
// 
// 创建者：庄欣锴
// 创建时间：2020年5月27日
// 
//----------------------------------------------------------------*/

namespace TM.Infrastructure.TencentCloud.Dto
{
    public class BaseTencentRespnse
    {
        public string TaskId { get; set; }

        /// <summary>
        /// 唯一请求 ID，每次请求都会返回。定位问题时需要提供该次请求的 RequestId。
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 错误示例
        /// </summary>
        public BaseTencentErrResponse Error { get; set; }
    }

    public class BaseTencentRespnseV2
    {
        public BaseTencentRespnse Response { get; set; }
    }

    public class BaseTencentErrResponse
    {
        public string Code { get; set; }

        public string Message { get; set; }
    }
}