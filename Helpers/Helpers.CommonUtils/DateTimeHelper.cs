using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers
{
    public static class DateTimeHelper
    {
        //public static DateTime ParserAlibabaTime(this string time)
        //{
        //    return DateTime.ParseExact(time, "yyyyMMddHHmmssfff+0800", null);
        //}

        /// <summary>
        /// 设置周一为第一天,1到7
        /// </summary>
        /// <returns></returns>
        public static int GetDayOfWeek(DateTime now)
        {
            return now.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
        }

        public static string TranslateWeekToFriendlyDisplay(IEnumerable<int> week)
        {
            week = week.Take(7);
            var weekStr = string.Join("", week);
            if (weekStr == "1111100") return "工作日";
            if (weekStr == "0000011") return "周末";
            if (weekStr == "1111111") return "每天";
            var sb = new StringBuilder();
            for (int i = 0; i < week.Count(); i++)
            {
                if (week.ElementAt(i) == 1)
                {
                    switch (i)
                    {
                        case 0:
                            sb.Append("周一,");
                            break;
                        case 1:
                            sb.Append("周二,");
                            break;
                        case 2:
                            sb.Append("周三,");
                            break;
                        case 3:
                            sb.Append("周四,");
                            break;
                        case 4:
                            sb.Append("周五,");
                            break;
                        case 5:
                            sb.Append("周六,");
                            break;
                        case 6:
                            sb.Append("周日,");
                            break;
                        default:
                            break;
                    }
                }
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}
