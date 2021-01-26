using System;
using System.Text;

namespace TM.Infrastructure.Helpers
{
    /// <summary>
    /// 格式化操作
    /// </summary>
    public static class Format
    {
        /// <summary>
        /// 加密手机号码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        public static string EncryptPhoneOfChina(string phone)
        {
            return string.IsNullOrWhiteSpace(phone)
                ? string.Empty
                : $"{phone.Substring(0, 3)}******{phone.Substring(phone.Length - 2, 2)}";
        }

        /// <summary>
        /// 加密车牌号
        /// </summary>
        /// <param name="plateNumber">车牌号</param>
        /// <returns></returns>
        public static string EncryptPlateNumberOfChina(string plateNumber)
        {
            return string.IsNullOrWhiteSpace(plateNumber)
                ? string.Empty
                : $"{plateNumber.Substring(0, 2)}***{plateNumber.Substring(plateNumber.Length - 2, 2)}";
        }

        /// <summary>
        /// 加密汽车VIN
        /// </summary>
        /// <param name="vinCode">汽车VIN</param>
        /// <returns></returns>
        public static string EncryptVinCode(string vinCode)
        {
            return string.IsNullOrWhiteSpace(vinCode)
                ? string.Empty
                : $"{vinCode.Substring(0, 3)}***********{vinCode.Substring(vinCode.Length - 3, 3)}";
        }

        /// <summary>
        /// 格式化金额
        /// </summary>
        /// <param name="money">金额</param>
        /// <param name="isEncrypt">是否加密。默认：false</param>
        /// <returns></returns>
        public static string FormatMoney(decimal money, bool isEncrypt = false)
        {
            return isEncrypt ? "***" : $"{money:N2}";
        }

        #region 以万为单位格式为数字
        /// <summary>
        /// 以万为单位格式为数字
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FormatNumberToW(int num)
        {
            var stringNumber = num.ToString();
            if (num < 10000) return stringNumber;

            var newNumber = Math.Round(Convert.ToDecimal(num) / 10000, 1, MidpointRounding.AwayFromZero);
            return $"{newNumber}W";
        }
        #endregion

        #region RFC3986  URLEncode 字符集 UTF8，所有特殊字符均需编码，大写形式。
        public static string UrlEncode(string input)
        {
            StringBuilder newBytes = new StringBuilder();
            var urf8Bytes = Encoding.UTF8.GetBytes(input);
            foreach (var item in urf8Bytes)
            {
                if (IsReverseChar((char)item))
                {
                    newBytes.Append('%');
                    newBytes.Append(ByteToHex(item));

                }
                else
                    newBytes.Append((char)item);
            }

            return newBytes.ToString();
        }

        private static bool IsReverseChar(char c)
        {
            return !((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')
                    || c == '-' || c == '_' || c == '.' || c == '~' || c == '&' || c == '=');
        }

        private static string ByteToHex(byte b)
        {
            return b.ToString("X2");
        }
        #endregion
    }
}
