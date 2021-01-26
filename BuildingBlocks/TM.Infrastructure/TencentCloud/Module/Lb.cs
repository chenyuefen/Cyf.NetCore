namespace TM.Infrastructure.TencentCloud.Module
{
    /// <summary>
    /// 腾讯云负载均衡服务（Cloud Load Balancer）   https://cloud.tencent.com/document/api/214/888
    /// </summary>
    public class Lb : Base
    {
        public Lb()
        {
            serverHost = "lb.api.qcloud.com";
        }
    }
}
