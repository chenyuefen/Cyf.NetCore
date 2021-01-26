namespace TM.Infrastructure.TencentCloud.Module
{
    public class Morphling : Base
    {
        public Morphling(string module)
        {
            serverHost = module + ".api.qcloud.com";
        }

        public void morph(string module)
        {
            serverHost = module + ".api.qcloud.com";
        }
    }
}
