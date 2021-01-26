namespace TM.Infrastructure.TencentCloud.Cos
{
    public class COSResultBase
    {

        /// <summary>
        /// 为0时候成功
        /// </summary>
        public int code { get; set; }
        public string message { get; set; }
        public string request_id { get; set; }
        public string vid { get; set; }
        public string session { get; set; }
        public int slice_size { get; set; }
        //{"code":0,"message":"SUCCESS","request_id":"NTk1MGFjZDdfYzhhMzNiXzMyMTZfNDY5ZTJl",
        //"data":{"access_url":"http://yalgetbk-1251048739.file.myqcloud.com/click/test.png",
        //"resource_path":"/1251048739/yalgetbk/click/test.png",
        //"source_url":"http://yalgetbk-1251048739.cosgz.myqcloud.com/click/test.png",
        //"url":"http://gz.file.myqcloud.com/files/v2/1251048739/yalgetbk/click/test.png",
        //"vid":"e4971f2c25a62b2d846b578325677f191498459352"}}
    }
    /// <summary>
    /// cos上传文件返回类
    /// </summary>
    public class COSResult : COSResultBase
    {
        public COSResult() { }
        public COSResult(int code, string message)
        {
            this.code = code;
            this.message = message;
        }
        public COSResultData data { get; set; }
    }

    /// <summary>
    /// 文件返回类
    /// </summary>
    public class COSResultData
    {
        /// <summary>
        /// cdn加速的url
        /// </summary>
        public string access_url { get; set; }
        /// <summary>
        /// 资源目录
        /// </summary>
        public string resource_path { get; set; }
        /// <summary>
        /// 非cdn的url
        /// </summary>
        public string source_url { get; set; }
        /// <summary>
        /// cros的url要开启回溯
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 文件id
        /// </summary>
        public string vid { get; set; }

    }
}
