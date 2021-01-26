using System;
using System.Globalization;

namespace TM.Infrastructure.Timing
{
    /// <summary>
    /// 时间操作辅助类
    /// </summary>
    public class DateTimeHelper
    {
        #region GetDays(获取总天数)

        /// <summary>
        /// 获取指定年的总天数
        /// </summary>
        /// <param name="year">指定年</param>
        /// <returns></returns>
        public static int GetDays(int year)
        {
            return GetDays(year, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 获取指定年的总天数，使用指定区域性
        /// </summary>
        /// <param name="year">指定年</param>
        /// <param name="culture">指定区域性</param>
        /// <returns></returns>
        public static int GetDays(int year, CultureInfo culture)
        {
            var first = new DateTime(year, 1, 1, culture.Calendar);
            var last = new DateTime(year + 1, 1, 1, culture.Calendar);
            return GetDays(first, last);
        }

        /// <summary>
        /// 获取指定时间的年的总天数
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns></returns>
        public static int GetDays(DateTime date)
        {
            return GetDays(date.Year, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 获取两个时间之间的天数
        /// </summary>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <returns></returns>
        public static int GetDays(DateTime fromDate, DateTime toDate)
        {
            return Convert.ToInt32(toDate.Subtract(fromDate).TotalDays);
        }

        #endregion

        #region CalculateAge(计算年龄)

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <returns></returns>
        public static int CalculateAge(DateTime dateOfBirth)
        {
            return CalculateAge(dateOfBirth, DateTime.Now.Date);
        }

        /// <summary>
        /// 计算年龄，指定参考日期
        /// </summary>
        /// <param name="dateOfBirth">出生日期</param>
        /// <param name="referenceDate">参考日期</param>
        /// <returns></returns>
        public static int CalculateAge(DateTime dateOfBirth, DateTime referenceDate)
        {
            var years = referenceDate.Year - dateOfBirth.Year;
            if (referenceDate.Month < dateOfBirth.Month ||
                (referenceDate.Month == dateOfBirth.Month && referenceDate.Day < dateOfBirth.Day))
            {
                --years;
            }
            return years;
        }
        #endregion

        #region BusinessDateFormat(业务时间格式化)
        /// <summary>
        /// 业务时间格式化，返回 xxx前
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public static string BusinessDateFormat(DateTime dateTime)
        {
            TimeSpan span = (DateTime.Now - dateTime).Duration();
            if (span.TotalDays > 60)
            {
                return dateTime.ToString("yyyy-MM-dd");
            }
            if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            if (span.TotalDays > 14)
            {
                return "2周前";
            }
            if (span.TotalDays > 7)
            {
                return "1周前";
            }
            if (span.TotalDays > 1)
            {
                return $"{(int)Math.Floor(span.TotalDays)}天前";
            }
            if (span.TotalHours > 1)
            {
                return $"{(int)Math.Floor(span.TotalHours)}小时前";
            }
            if (span.TotalMinutes > 1)
            {
                return $"{(int)Math.Floor(span.TotalMinutes)}秒前";
            }
            return "1秒前";
        }

        #endregion

        #region ----UNIX转日期----
        /// <summary>
        /// 日期转换为时间戳（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalMilliseconds;
        }

        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime DateTimeFormat(long unixTime)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(unixTime + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        #endregion
    }
}
