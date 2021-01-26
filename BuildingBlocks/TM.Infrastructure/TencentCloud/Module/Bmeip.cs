namespace TM.Infrastructure.TencentCloud.Module
{
    /// <summary>
    /// 黑石服务器 （Cloud Physical Machine）弹性公网IP https://cloud.tencent.com/document/product/386/6669
    /// </summary>
    public class Bmeip : Base
    {
        public Bmeip()
        {
            serverHost = "bmeip.api.qcloud.com";
        }
    }
}
