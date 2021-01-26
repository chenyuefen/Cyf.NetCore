namespace TM.Core.Models.Dtos
{
    /// <summary>
    /// 商城结果类
    /// 用于请求商城端接口，解析数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }
    }
}
