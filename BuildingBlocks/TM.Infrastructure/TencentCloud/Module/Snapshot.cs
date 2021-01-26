namespace TM.Infrastructure.TencentCloud.Module
{
    /// <summary>
    /// 腾讯云硬盘（CBS）  https://cloud.tencent.com/document/api/362/2445
    /// </summary>
    public class Snapshot : Base
    {
        public Snapshot()
        {
            serverHost = "snapshot.api.qcloud.com";
        }
    }
}
