using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TM.Core.Models.Dtos;

namespace TM.Core.AspNetCore
{
    public class ApiControllerBase : ControllerBase
    {
        #region ----获取请求表单 参数检验错误信息----
        /// <summary>
        /// 获取View表单的错误信息
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public string GetModelStateMsgStr()
        {
            StringBuilder sbMsg = new StringBuilder();
            //获取所有错误的Key
            List<string> Keys = ModelState.Keys.ToList();
            //获取每一个key对应的ModelStateDictionary
            foreach (var key in Keys)
            {
                var listErrors = ModelState[key].Errors.ToList();
                //将错误描述添加到sb中
                foreach (var error in listErrors)
                {
                    sbMsg.Append(error.ErrorMessage);
                }
            }
            return sbMsg.ToString();
        }
        #endregion

        #region ----接口返回结果----
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult SuccessResult(string msg)
        {
            ApiResult result = new ApiResult();
            result.Code = Models.Code.Ok;
            result.Message = msg;
            return result;
        }

        /// <summary>
        /// 成功带结果Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ApiResult<T> SuccessResult<T>(string msg, T obj)
        {
            ApiResult<T> result = new ApiResult<T>();
            result.Code = Models.Code.Ok;
            result.Message = msg;
            result.Data = obj;
            return result;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult ErrorResult(string msg, Models.Code status = Models.Code.Error)
        {
            ApiResult result = new ApiResult();
            result.Code = status;
            result.Message = msg;
            return result;
        }

        /// <summary>
        /// 失败，带泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static ApiResult<T> ErrorResult<T>(string msg, Models.Code status = Models.Code.Error)
        {
            ApiResult<T> result = new ApiResult<T>();
            result.Code = status;
            result.Message = msg;
            return result;
        }
        /// <summary>
		/// 失败，带泛型
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="msg"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public static ApiResult<T, K> ErrorResult<T, K>(string msg, Models.Code status = Models.Code.Error)
        {
            ApiResult<T, K> result = new ApiResult<T, K>();
            result.Code = status;
            result.Message = msg;
            return result;
        }

        #endregion

    }

}
