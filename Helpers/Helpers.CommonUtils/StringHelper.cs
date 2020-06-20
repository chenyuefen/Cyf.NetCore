using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Helpers
{
    public static class StringHelper
    {
        public static Encoding GetFileEncodeType(byte[] buffer)
        {
            //System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            //Byte[] buffer = br.ReadBytes(2);
            if (buffer == null || buffer.Length == 0)
            {
                return Encoding.ASCII;
            }
            else
            {
                if (buffer[0] >= 0xEF)
                {
                    if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                    {
                        return System.Text.Encoding.UTF8;
                    }
                    else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                    {
                        return System.Text.Encoding.BigEndianUnicode;
                    }
                    else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                    {
                        return System.Text.Encoding.Unicode;
                    }
                    else
                    {
                        return System.Text.Encoding.GetEncoding("gbk");
                    }
                }
                else
                {
                    return System.Text.Encoding.GetEncoding("gbk");
                }
            }
        }
        public static string GetFileText(byte[] buffer)
        {
            var encoding = GetFileEncodeType(buffer);
            return encoding.GetString(buffer);
        }
    }
    public static class StringHelperExt
    {
        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }

        public static bool IsNotNullAndEmpty(this string @this)
        {
            return !string.IsNullOrEmpty(@this);
        }

        public static bool IsNullOrWhiteSpace(this string @this)
        {
            return string.IsNullOrWhiteSpace(@this);
        }

        public static bool IsNotNullAndWhiteSpace(this string @this)
        {
            return !string.IsNullOrWhiteSpace(@this);
        }

        public static string UrlEncode(this string @this)
        {
            if (string.IsNullOrEmpty(@this)) return @this;
            StringBuilder sb = new StringBuilder();
            foreach (var item in @this)
            {
                var temp = HttpUtility.UrlEncode(item.ToString());
                sb.Append(temp.Length > 1 ? temp.ToUpper() : temp);
            }
            return sb.ToString();
        }
    }
}
