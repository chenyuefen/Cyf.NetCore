namespace TM.Infrastructure.TencentCloud.Module
{
    /// <summary>
    /// 黑石服务器 （Cloud Physical Machine） 私有网络 https://cloud.tencent.com/document/product/386/17140
    /// </summary>
    public class Bmvpc : Base
    {
        public Bmvpc()
        {
            serverHost = "bmvpc.api.qcloud.com";
        }
    }
}
