namespace TM.Infrastructure.TencentCloud.Module
{
    /// <summary>
    /// 云服务器 API（Cloud Virtual Machine）    https://cloud.tencent.com/document/api/213/15688
    /// </summary>
    public class Cvm : Base
    {
        public Cvm()
        {
            serverHost = "cvm.api.qcloud.com";
        }
    }
}
