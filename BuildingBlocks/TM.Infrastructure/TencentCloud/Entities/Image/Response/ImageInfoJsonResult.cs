using TM.Infrastructure.TencentCloud.Entities.Base;

namespace TM.Infrastructure.TencentCloud.Entities.Image.Response
{
    /// <summary>
    /// 数据万象
    /// </summary>
    public class ImageInfoJsonResult : WxJsonResult
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string error { get; set; } = "请求成功";

        /// <summary>
        /// 图片格式
        /// </summary>
        public string format { get; set; }

        /// <summary>
        /// 图片宽度
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// 图片高度
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 图片尺寸
        /// </summary>
        public long size { get; set; }

        /// <summary>
        /// 图片MD5加密
        /// </summary>
        public string md5 { get; set; }

        /// <summary>
        /// 图片RGB
        /// </summary>
        public string photo_rgb { get; set; }
    }
}
