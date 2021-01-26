/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：CFCAUtils
// 文件功能描述：
// 中金支付工具类
// 创建者：庄欣锴
// 创建时间：2020年5月30日
// 
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TM.Infrastructure.Utils
{
    public class CPCNUtils
    {
        public static string Base64UrlEncoding(string str)
        {
            return str.Replace("+", "%2B").Replace("/", "%2F");
        }

        public static byte[] hex2bytes(string hexString)
        {
            // 转换成大写
            hexString = hexString.ToUpper();

            // 计算字节数组的长度
            var chars = hexString.ToCharArray();
            var bytes = new byte[chars.Length / 2];

            // 数组索引
            var index = 0;

            for (var i = 0; i < chars.Length; i += 2)
            {
                byte newByte = 0x00;

                // 高位
                newByte |= char2byte(chars[i]);
                newByte <<= 4;

                // 低位
                newByte |= char2byte(chars[i + 1]);

                // 赋值
                bytes[index] = newByte;

                index++;
            }

            return bytes;
        }

        public static byte char2byte(char ch)
        {
            return ch switch
            {
                '0' => 0x00,
                '1' => 0x01,
                '2' => 0x02,
                '3' => 0x03,
                '4' => 0x04,
                '5' => 0x05,
                '6' => 0x06,
                '7' => 0x07,
                '8' => 0x08,
                '9' => 0x09,
                'A' => 0x0A,
                'B' => 0x0B,
                'C' => 0x0C,
                'D' => 0x0D,
                'E' => 0x0E,
                'F' => 0x0F,
                _ => 0x00
            };
        }

        public static byte[] Hash(string text)
        {
            var sha1 = SHA1.Create();
            var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
            return bytes;
        }

        public static string ConvertBytesToHexString(IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes) sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }

        public static string CreateNumberByTime()
        {
            var dt = System.DateTime.Now;
            var str = $"{dt:yyyyMMddHHmmssfff}" + RandCode(10);

            return str;
        }

        public static string CreateNumberByTime20()
        {
            var dt = System.DateTime.Now;
            var str = $"{dt:yyyyMMddHHmmssfff}" + RandCode(3);

            return str;
        }

        private static string RandCode(int n)
        {
            var arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var num = new StringBuilder();
            var rnd = new System.Random(Guid.NewGuid().GetHashCode());
            for (var i = 0; i < n; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
            }

            return num.ToString();
        }
    }
}