using TM.Core.Models;
using TM.Core.Models.Dtos;

namespace TM.Core.Extensions
{
    public static class ApiResultExtensions
    {
        /// <summary>
        /// 自定义ApiResults返回
        /// </summary>
        /// <param name="resultCount"></param>
        /// <param name="successContent"></param>
        /// <param name="failContent"></param>
        /// <returns></returns>
        public static ApiResult CustomReturn(int resultCount, string successContent, string failContent)
        {
            var apiResult = new ApiResult();
            apiResult.Code = resultCount > 0 ? Code.Ok : Code.Error;
            apiResult.Message = resultCount > 0 ? successContent : failContent;
            return apiResult;
        }
    }
}
