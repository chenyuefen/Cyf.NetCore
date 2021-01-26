namespace TM.Infrastructure.CosCloud.Dto
{
    public class CosResultInfoResponseDto
    {
        /// <summary>
        /// 状态码
        /// http status code
        /// </summary>
        public int statusCode { get; set; }

        /// <summary>
        /// http消息
        /// http status message
        /// </summary>
        public string statusMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string info { get; set; }

        /// <summary>
        /// 上传资源返回
        /// </summary>
        public string cosDataUrl { get; set; }
    }
}
