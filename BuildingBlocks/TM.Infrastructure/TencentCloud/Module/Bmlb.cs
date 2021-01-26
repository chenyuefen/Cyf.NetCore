namespace TM.Infrastructure.TencentCloud.Module
{
    /// <summary>
    /// 黑石服务器 （Cloud Physical Machine） 负载均衡 https://cloud.tencent.com/document/product/386/9308
    /// </summary>
    public class Bmlb : Base
    {
        public Bmlb()
        {
            serverHost = "bmlb.api.qcloud.com";
        }
    }
}
