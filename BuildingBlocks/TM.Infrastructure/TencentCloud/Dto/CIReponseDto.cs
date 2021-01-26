namespace TM.Infrastructure.TencentCloud.Dto
{
    public class CIReponseDto
    {
        #region 图片信息

        public class ImageInfoResponse
        {
            public string Format { get; set; }

            public string Width { get; set; }

            public string Height { get; set; }

            public string Size { get; set; }

            public string Md5 { get; set; }

            public string Photo_rgb { get; set; }
        }
        #endregion
    }
}