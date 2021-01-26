namespace TM.Core.Models.Dtos
{
    /// <summary>
    /// API 返回JSON字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult
    {
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public Code Code { get; set; } = Code.Ok;
    }

    /// <summary>
    /// API 返回JSON字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// 信息
        /// </summary>
        //public string Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        //public Code Code { get; set; } = Code.Ok;
        /// <summary>
        /// 数据集
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// API 返回JSON字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T, K> : ApiResult
    {
        /// <summary>
        /// 信息
        /// </summary>
        //public string Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        //public Code Code { get; set; } = Code.Ok;
        /// <summary>
        /// 数据集
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 拓展数据
        /// </summary>
        public K Ext { get; set; }
    }
}
