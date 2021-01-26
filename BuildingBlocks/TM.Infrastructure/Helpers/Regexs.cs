using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TM.Infrastructure.Helpers
{
    /// <summary>
    /// 正则表达式 操作
    /// </summary>
    public static class Regexs
    {
        #region GetValues(获取匹配值集合)

        /// <summary>
        /// 获取匹配值集合
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="resultPatterns">结果模式字符串数组，范例：new[]{"$1","$2"}</param>
        /// <param name="options">选项</param>
        public static Dictionary<string, string> GetValues(string input, string pattern, string[] resultPatterns,
            RegexOptions options = RegexOptions.IgnoreCase)
        {
            var result = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(input))
                return result;
            var match = System.Text.RegularExpressions.Regex.Match(input, pattern, options);
            if (match.Success == false)
                return result;
            AddResults(result, match, resultPatterns);
            return result;
        }

        /// <summary>
        /// 添加匹配结果
        /// </summary>
        /// <param name="result">匹配值字典</param>
        /// <param name="match">匹配结果</param>
        /// <param name="resultPatterns">结果模式字符串数组，范例：new[]{"$1","$2"}</param>
        private static void AddResults(Dictionary<string, string> result, Match match, string[] resultPatterns)
        {
            if (resultPatterns == null)
            {
                result.Add(string.Empty, match.Value);
                return;
            }
            foreach (var resultPattern in resultPatterns)
                result.Add(resultPattern, match.Result(resultPattern));
        }

        #endregion

        #region GetValue(获取匹配值)

        /// <summary>
        /// 获取匹配值
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="resultPattern">结果模式字符串，范例："$1"用来获取第一个()内的值</param>
        /// <param name="options">选项</param>
        public static string GetValue(string input, string pattern, string resultPattern = "",
            RegexOptions options = RegexOptions.IgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            var match = System.Text.RegularExpressions.Regex.Match(input, pattern, options);
            if (match.Success == false)
                return string.Empty;
            return string.IsNullOrWhiteSpace(resultPattern) ? match.Value : match.Result(resultPattern);
        }

        #endregion

        #region Split(分割成字符串数组)

        /// <summary>
        /// 分割成字符串数组
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">选项</param>
        public static string[] Split(string input, string pattern, RegexOptions options = RegexOptions.IgnoreCase) =>
            string.IsNullOrWhiteSpace(input)
                ? new string[] { }
                : System.Text.RegularExpressions.Regex.Split(input, pattern, options);

        #endregion

        #region Replace(替换)

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="options">选项</param>
        public static string Replace(string input, string pattern, string replacement,
            RegexOptions options = RegexOptions.IgnoreCase) => string.IsNullOrWhiteSpace(input)
            ? string.Empty
            : System.Text.RegularExpressions.Regex.Replace(input, pattern, replacement, options);

        #endregion

        #region IsMatch(验证输入与模式是否匹配)

        /// <summary>
        /// 验证输入与模式是否匹配
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        public static bool IsMatch(string input, string pattern) => IsMatch(input, pattern, RegexOptions.IgnoreCase);

        /// <summary>
        /// 验证输入与模式是否匹配
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">选项</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options) => Regex.IsMatch(input, pattern, options);

        #endregion

        #region IsInteger(验证整数)
        /// <summary>
        /// 验证整数
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool IsInteger(string input)
        {
            //string pattern = @"^-?\d+$";
            //return IsMatch(input, pattern);
            int i = 0;
            if (int.TryParse(input, out i))
                return true;
            else
                return false;
        }
        #endregion

        #region IsDecimal(验证小数)
        /// <summary>
        /// 验证小数
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool IsDecimal(string input)
        {
            string pattern = @"^([-+]?[1-9]\d*\.\d+|-?0\.\d*[1-9]\d*)$";
            return IsMatch(input, pattern);
        }
        #endregion

        #region IsNumber(验证是否是数字)
        /// <summary>
        /// 验证数字(double类型)
        /// [可以包含负号和小数点]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool IsNumber(string input)
        {
            //string pattern = @"^-?\d+$|^(-?\d+)(\.\d+)?$";
            //return IsMatch(input, pattern);
            double d = 0;
            if (double.TryParse(input, out d))
                return true;
            else
                return false;
        }
        #endregion

        #region IsMobilePhoneNumber(验证手机号码)
        /// <summary>
        /// 验证手机号码
        /// [可匹配"(+86)013325656352"，括号可以省略，+号可以省略，(+86)可以省略，11位手机号前的0可以省略；11位手机号第二位数可以是3、4、5、6、8、9中的任意一个]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool IsMobilePhoneNumber(string input)
        {
            string pattern = @"^(\((\+)?86\)|((\+)?86)?)0?1[3456789]\d{9}$";
            return IsMatch(input, pattern);
        }
        #endregion

        #region IsEmail(验证电子邮箱)
        /// <summary>
        /// 验证电子邮箱
        /// [@字符前可以包含字母、数字、下划线和点号；@字符后可以包含字母、数字、下划线和点号；@字符后至少包含一个点号且点号不能是最后一个字符；最后一个点号后只能是字母或数字]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool IsEmail(string input)
        {
            ////邮箱名以数字或字母开头；邮箱名可由字母、数字、点号、减号、下划线组成；邮箱名（@前的字符）长度为3～18个字符；邮箱名不能以点号、减号或下划线结尾；不能出现连续两个或两个以上的点号、减号。
            //string pattern = @"^[a-zA-Z0-9]((?<!(\.\.|--))[a-zA-Z0-9\._-]){1,16}[a-zA-Z0-9]@([0-9a-zA-Z][0-9a-zA-Z-]{0,62}\.)+([0-9a-zA-Z][0-9a-zA-Z-]{0,62})\.?|((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$";
            string pattern = @"^([\w-\.]+)@([\w-\.]+)(\.[a-zA-Z0-9]+)$";
            return IsMatch(input, pattern);
        }
        #endregion

        #region IsIDCard

        /// <summary>
        /// 验证身份证号（不区分一二代身份证号）
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool IsIDCard(string input)
        {
            if (input.Length == 18)
                return IsIDCard18(input);
            else if (input.Length == 15)
                return IsIDCard15(input);
            else
                return false;
        }

        /// <summary>
        /// 验证一代身份证号（15位数）
        /// [长度为15位的数字；匹配对应省份地址；生日能正确匹配]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool IsIDCard15(string input)
        {
            //验证是否可以转换为15位整数
            long l = 0;
            if (!long.TryParse(input, out l) || l.ToString().Length != 15)
            {
                return false;
            }
            //验证省份是否匹配
            //1~6位为地区代码，其中1、2位数为各省级政府的代码，3、4位数为地、市级政府的代码，5、6位数为县、区级政府代码。
            string address = "11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91,";
            if (!address.Contains(input.Remove(2) + ","))
            {
                return false;
            }
            //验证生日是否匹配
            string birthdate = input.Substring(6, 6).Insert(4, "/").Insert(2, "/");
            DateTime dt;
            if (!DateTime.TryParse(birthdate, out dt))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证二代身份证号（18位数，GB11643-1999标准）
        /// [长度为18位；前17位为数字，最后一位(校验码)可以为大小写x；匹配对应省份地址；生日能正确匹配；校验码能正确匹配]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool IsIDCard18(string input)
        {
            //验证是否可以转换为正确的整数
            long l = 0;
            if (!long.TryParse(input.Remove(17), out l) || l.ToString().Length != 17 || !long.TryParse(input.Replace('x', '0').Replace('X', '0'), out l))
            {
                return false;
            }
            //验证省份是否匹配
            //1~6位为地区代码，其中1、2位数为各省级政府的代码，3、4位数为地、市级政府的代码，5、6位数为县、区级政府代码。
            string address = "11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91,";
            if (!address.Contains(input.Remove(2) + ","))
            {
                return false;
            }
            //验证生日是否匹配
            string birthdate = input.Substring(6, 8).Insert(6, "/").Insert(4, "/");
            DateTime dt;
            if (!DateTime.TryParse(birthdate, out dt))
            {
                return false;
            }
            //校验码验证
            //校验码：
            //（1）十七位数字本体码加权求和公式 
            //S = Sum(Ai * Wi), i = 0, ... , 16 ，先对前17位数字的权求和 
            //Ai:表示第i位置上的身份证号码数字值 
            //Wi:表示第i位置上的加权因子 
            //Wi: 7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2 
            //（2）计算模 
            //Y = mod(S, 11) 
            //（3）通过模得到对应的校验码 
            //Y: 0 1 2 3 4 5 6 7 8 9 10 
            //校验码: 1 0 X 9 8 7 6 5 4 3 2 
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = input.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != input.Substring(17, 1).ToLower())
            {
                return false;
            }
            return true;
        }

        #endregion

        #region MidPasswordStrength(验证中等密码强度)
        /// <summary>
        /// 验证中等密码强度
        /// [字母+数字，字母+特殊字符，数字+特殊字符]
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool MidPasswordStrength(string input)
        {
            string pattern = @"^(?![a-zA-z]+$)(?!\d+$)(?![!@#$%^&*]+$)[a-zA-Z\d!@#$%^&*]+$";
            return IsMatch(input, pattern);
        }
        #endregion

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNull(string str)
        {
            if (str == null || str == "" || str.Trim() == "")
                return true;
            else
                return false;
        }

    }
}
