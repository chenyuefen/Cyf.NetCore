using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using TM.Infrastructure.Excel;

namespace TM.Core.Models.Dtos
{
    public static class ActionResultFactory
    {
        public static JsonResult NoContent(string data = "查无数据")
        {
            return new JsonResult(new ApiResult()
            {
                Code = Code.NoContent,
                Message = data
            });
        }

        public static JsonResult Ok(object data = default)
        {
            return new JsonResult(new ApiResult()
            {
                Code = Code.Ok,
                Message = data?.ToString()
            });
        }

        public static JsonResult Error(object data = null)
        {
            return new JsonResult(new ApiResult()
            {
                Code = Code.Error,
                Message = data?.ToString()
            });
        }

        public static JsonResult Other(Code code,  object data = null)
        {
            return new JsonResult(new ApiResult()
            {
                Code = code,
                Message = data?.ToString()
            });
        }

        public static FileContentResult File(byte[] buffer, string fileName)
        {
            return new FileContentResult(buffer, MediaTypeNames.Application.Octet) 
            {
                FileDownloadName = fileName
            };
        }
        
        public static FileContentResult Excel<T>(List<T> data, string fileName, bool IsList = false)
        {
			if (!fileName.EndsWith(".xls"))
			{
                fileName += ".xls";
            }
            var buffer = NPOIHelper.Export(data, string.Empty, IsList);
            return new FileContentResult(buffer, MediaTypeNames.Application.Octet) 
            {
                FileDownloadName = fileName
            };
        }

        public static FileContentResult ListToExcel<T>(List<T> data, string fileName, bool IsList = false)
        {
            if (!fileName.EndsWith(".xls"))
            {
                fileName += ".xls";
            }
            var ms = NPOIHelper.ListToExcel(data, fileName);
            return new FileContentResult(ms.GetBuffer(), MediaTypeNames.Application.Octet)
            {
                FileDownloadName = fileName
            };
        }
    }
}
