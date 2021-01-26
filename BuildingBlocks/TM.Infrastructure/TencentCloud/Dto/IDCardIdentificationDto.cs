namespace TM.Infrastructure.TencentCloud.Dto
{
    class IDCardIdentificationDto
    {
    }

    #region 身份证校验请求参数
    public class IDCardIdentificationBaseRequest
    {
        /*图片的 Url 地址。要求图片经Base64编码后不超过 7M，分辨率建议500*800以上，
         * 支持PNG、JPG、JPEG、BMP格式。建议卡片部分占据图片2/3以上。
         * 建议图片存储于腾讯云，可保障更高的下载速度和稳定性。*/
        public string ImageUrl { get; set; }

        /**
         * 	FRONT：身份证有照片的一面（人像面），
         * 	BACK：身份证有国徽的一面（国徽面），
         * 	该参数如果不填，将为您自动判断身份证正反面。
         */
        public string CardSide { get; set; }
    }
    #endregion

    #region 身份证校验接口配置
    /// <summary>
    /// 身份证校验接口配置
    /// </summary>
    public class IDCardIdentificationConfig
    {
        /// <summary>
        /// 身份证照片裁剪（去掉证件外多余的边缘、自动矫正拍摄角度）
        /// </summary>
        public bool CropIdCard { get; set; }

        /// <summary>
        /// 人像照片裁剪（自动抠取身份证头像区域）
        /// </summary>
        public bool CropPortrait { get; set; }

        /// <summary>
        /// 复印件告警
        /// </summary>
        public bool CopyWarn { get; set; }

        /// <summary>
        /// 边框和框内遮挡告警
        /// </summary>
        public bool BorderCheckWarn { get; set; }

        /// <summary>
        /// 翻拍告警
        /// </summary>
        public bool ReshootWarn { get; set; }

        /// <summary>
        /// PS检测告警
        /// </summary>
        public bool DetectPsWarn { get; set; }

        /// <summary>
        /// 临时身份证告警
        /// </summary>
        public bool TempIdWarn { get; set; }

        /// <summary>
        /// 身份证有效日期不合法告警
        /// </summary>
        public bool InvalidDateWarn { get; set; }

        /// <summary>
        /// 图片质量分数（评价图片的模糊程度）
        /// </summary>
        public bool Quality { get; set; }
    }
    #endregion

    #region 身份证校验返回值
    public class IDCardIdentificationResponse
    {
        public IDCardIdentificationResponseBase Response { get; set; }
    }

    public class IDCardIdentificationResponseBase
    {
        /// <summary>
        /// 姓名（人像面） 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别（人像面）
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 民族（人像面）
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 出生日期（人像面）
        /// </summary>
        public string Birth { get; set; }

        /// <summary>
        /// 地址（人像面）
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 身份证号（人像面）
        /// </summary>
        public string IdNum { get; set; }

        /// <summary>
        /// 发证机关（国徽面）
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// 证件有效期（国徽面）
        /// </summary>
        public string ValidDate { get; set; }
        public string AdvancedInfo { get; set; }
        public string RequestId { get; set; }
        public TencentError Error { get; set; }
    }

    public class TencentError
    {
        public string Code { get; set; }

        public string Message { get; set; }
    }

    public class IDCardAdvancedInfo
    {
        /// <summary>
        /// 身份证照片裁剪
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 人像照片裁剪
        /// </summary>
        public string Portrait { get; set; }
    }
    #endregion
}
