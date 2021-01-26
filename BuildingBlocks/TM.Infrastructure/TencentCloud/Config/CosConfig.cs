using TM.Infrastructure.Configs;

namespace TM.Infrastructure.TencentCloud.Config
{
    /// <summary>
    /// 云存储基本配置
    /// 静态用大写
    /// </summary>
    public class CosConfig : BaseTencentConfig
    {
        /// <summary>
        /// yalgetstatic
        /// </summary>
        public const string DEFAULT_BUCKET = "yalgetstatic";

        /// <summary>
        /// 直播对象存储文件基础名称
        /// </summary>
        public const string BASE_FILE_NAME = "/mall";

        /// <summary>
        /// 直播申请二维码文件夹
        /// </summary>
        public const string QRCODE_PIC = "/qrcoder/";

        /// <summary>
        /// 直播预告文件上传
        /// </summary>
        public const string STUDIO_NOTICE_FILE = "/notice/";

        /// <summary>
        /// 身份信息文件上传
        /// </summary>
        public const string IDCARD_PIC = "/idcard/";

        /// <summary>
        /// 会员头像
        /// </summary>
        public const string HEAD_THUMB_PIC = "/headthumb/";

        /// <summary>
        /// 短视频文件上传
        /// </summary>
        public const string SHORT_VIDEO = "/shortvideo/";

        /// <summary>
        /// 商品图片上传
        /// </summary>
        public const string SPU_IMAGE = "/spuimages/";

        /// <summary>
        /// 订单退款凭证上传
        /// </summary>
        public const string TRADE_REFUND = "/traderefund/";

        /// <summary>
        /// 评价图片上传
        /// </summary>
        public const string EVALUATE_IMAGE = "/evaluateimage/";

        /// <summary>
        /// 评价视频上传
        /// </summary>
        public const string EVALUATE_VIDEO = "/evaluatevideo/";

        /// <summary>
        /// 商品视频上传
        /// </summary>
        public const string SPU_VIDEO = "/spuvideos/";

        /// <summary>
        /// 小程序码上传
        /// </summary>
        public const string MINI_APP_CODE = "/wxacodes/";

        /// <summary>
        /// 个人背景图片
        /// </summary>
        public const string Back_GROUND_PIC = "/backgroundpics/";

        /// <summary>
        /// 合伙人门店图片
        /// </summary>
        public const string PARTNER_STOREURL = "/partnerstoreurl/";

        /// <summary>
        /// 合伙人营业执照
        /// </summary>
        public const string PARTNER_LICENSE = "/partnerlicense/";

        /// <summary>
        /// 供应商营业执照
        /// </summary>
        public const string SUPPLIER_LICENSE = "/supplierlicense/";

        /// <summary>
        /// 加速文件域名  对象存储
        /// </summary>
        public const string DOMAIN_URL = "https://yalgetstatic-1302639461.file.myqcloud.com";

        /// <summary>
        /// 加速图片域名   数据万象
        /// 转换成缩略图https://yalgetstatic-1302639461.image.myqcloud.com?imageView2/3/w/250/h/250/q/85
        /// </summary>
        public const string DOMAIN_PICURL = "https://yalgetstatic-1302639461.image.myqcloud.com";


        /// <summary>
        /// 素材图片
        /// </summary>
        public const string SOURCE_MATERIAL_PIC = "/sourcematerialpics";

        /// <summary>
        /// 微信直播视频
        /// </summary>
        public const string WX_BROADCAST_VIDEO = "/wxbroadcastvideo/";
    }
}
