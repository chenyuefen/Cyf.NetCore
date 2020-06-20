using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public class MyResult : MyResult<object>
    {
        public MyResult(int code, string msg, object data) : base(code, msg, data)
        {
        }
    }

    public class MyResult<T>
    {
        public MyResult(int code, string msg, T data)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }

        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }
}
