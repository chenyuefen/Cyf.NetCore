using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public static class ActionResultFactory
    {
        public static JsonResult Return200(object data = default)
        {
            return new JsonResult(new MyResult(200, "ok", data));
        }

        public static JsonResult Return400(string msg = "fail", object data = null)
        {
            return new JsonResult(new MyResult(400, msg, data));
        }

        public static JsonResult ReturnOther(int code, string msg = null, object data = null)
        {
            return new JsonResult(new MyResult(code, msg, data));
        }
    }
}
