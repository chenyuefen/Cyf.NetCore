using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers.Extensions
{
    public static class SecurityHelperExt
    {
        public static string ToMd5(this string @this)
        {
            return SecurityHelper.ToMD5(@this);
        }
    }
}
