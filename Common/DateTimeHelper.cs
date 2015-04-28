using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Configuration;

namespace Qisda.PMS.Common
{
    public sealed class DateTimeHelper
    {
        private static string ms_DateFormat = ConfigurationManager.AppSettings["DateFormat"].ToString();
        private static char ms_dateSeparator = FindSeparator(ms_DateFormat);
        private DateTimeHelper()
        {
        }

        /// <summary>
        /// 日期分隔符

        /// </summary>
        public static char DateSeparator
        {
            get
            {
                return ms_dateSeparator;
            }
        }

        public static DateTime InvalidDate
        {
            get
            {
                return new DateTime(0001, 1, 1);
            }
        }
        /// <summary>
        /// 将日期类型按照系统指定的格式转换成字符串型

        /// </summary>
        /// <param name="dateTime">需转换的日期</param>
        /// <returns>转换后的字符串</returns>
        public static string ConvertDateToString(DateTime? dateTime)
        {
            return ConvertDateToString(dateTime, ms_DateFormat);
        }

        /// <summary>
        /// 将日期转换为年月的格式如2007/05
        /// </summary>
        /// <param name="dateTime">需转换的日期</param>
        /// <returns>转换后的字符串</returns>
        public static string ConvertDateToYearMonth(DateTime? dateTime)
        {
            return ConvertDateToString(dateTime, GetYearMonthFormat());
        }
        /// <summary>
        /// 将int类型转换成日期型，如果Int值不在99991231和00010101之间，则返回DateTime.MaxValue
        /// </summary>
        /// <param name="date">需转换的int型日期</param>
        /// <returns>转换后的日期</returns>
        public static DateTime ConvertIntToDate(int date)
        {
            DateTime returnDate = InvalidDate;
            if (date < 99991232 || date > 00010100)
            {
                int year = date / 10000;
                if (year <= 9999 && year >= 0001)
                {
                    int month = (date % 10000) / 100;
                    if (month < 13 && month > 0)
                    {
                        int lastDay = new DateTime(year, month, 1).AddMonths(1).AddDays(-1).Day;
                        int day = date % 100;
                        if (day <= lastDay && day > 0)
                        {
                            returnDate = new DateTime(year, month, day);
                        }
                    }
                }
            }
            return returnDate;
        }
        /// <summary>
        /// 将int类型按照系统指定的格式转换成字符串型
        /// </summary>
        /// <param name="date">需转换的int型日期</param>
        /// <returns>转换后的日期字符串</returns>
        public static string ConvertIntToDateString(int date)
        {
            return ConvertDateToString(ConvertIntToDate(date));
        }

        /// <summary>
        /// 将日期类型转换为符合系统格式的字符串
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="dateFormat">格式</param>
        /// <returns>格式化后的字符串</returns>
        private static string ConvertDateToString(DateTime? dateTime, string dateFormat)
        {
            if (!dateTime.HasValue) return string.Empty;
            string year = string.Empty;
            string month = string.Empty;
            string day = string.Empty;
            year = dateTime.Value.Year.ToString();
            month = dateTime.Value.Month.ToString();
            day = dateTime.Value.Day.ToString();

            dateFormat = dateFormat.Trim().ToLower();
            string dateReturn = dateFormat.Replace("yyyy", year.PadLeft(4, '0'))
                .Replace("yy", year.PadLeft(4, '0').Substring(2, 2))
                .Replace("mm", month.PadLeft(2, '0'))
                .Replace("m", month)
                .Replace("dd", day.PadLeft(2, '0'))
                .Replace("d", day);
            return dateReturn;
        }


        private static string GetYearMonthFormat()
        {
            string dateFormat = ms_DateFormat.ToLower();
            string yearMonthFormat = string.Empty;
            int dayIndex = dateFormat.IndexOf("dd");
            string daySign = string.Empty;
            string separator = DateSeparator.ToString();
            if (dayIndex > -1)
            {
                daySign = "dd";
            }
            else
            {
                daySign = "d";
            }
            yearMonthFormat = dateFormat.Replace(separator + daySign + separator, separator);
            yearMonthFormat = yearMonthFormat.Replace(separator + daySign, "");
            yearMonthFormat = yearMonthFormat.Replace(daySign + separator, "");
            return yearMonthFormat;
        }
        private static char FindSeparator(string format)
        {
            char[] formatChars = format.ToCharArray();
            foreach (char c in formatChars)
            {
                if (char.IsLetterOrDigit(c) == false)
                {
                    return c;
                }
            }
            return '\0';
        }
    }

}