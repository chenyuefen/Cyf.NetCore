namespace TM.Infrastructure.TencentCloud.Entities.Base.Interface
{
    /// <summary>
    /// 所有 JSON 格式返回值的API返回结果接口
    /// </summary>
    public interface IJsonResult
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 状态  成功：Success
        /// </summary>
        public string codeDesc { get; set; }

        /// <summary>
        /// errcode的
        /// </summary>
        public abstract int ErrorCodeValue { get; }
    }
}
