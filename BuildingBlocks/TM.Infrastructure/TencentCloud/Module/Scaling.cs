namespace TM.Infrastructure.TencentCloud.Module
{
    /// <summary>
    /// 弹性伸缩（Auto Scaling，AS）https://cloud.tencent.com/document/product/377
    /// </summary>
    public class Scaling : Base
    {
        public Scaling()
        {
            serverHost = "scaling.api.qcloud.com";
        }
    }
}
