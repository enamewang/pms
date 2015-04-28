using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Qisda.PMS.Common
{
    public class PageParameterManager
    {
        #region static Default value
        public static PageParameterManager Default
        {
            get { return m_Default ?? (m_Default = new PageParameterManager()); }
        }
        private static PageParameterManager m_Default;
        #endregion

        #region constructor
        private readonly HttpContext m_HttpContext;

        private PageParameterManager()
        {

        }

        public PageParameterManager(HttpContext context)
        {
            m_HttpContext = context;
        }
        #endregion

        #region GetString

        /// <summary>
        /// 获取必需的字符串参数值。
        /// </summary>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public string GetRequiredString(string paraName)
        {
            return GetRequiredParameterValue(paraName);
        }

        /// <summary>
        /// 获取可选的字符串参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public string GetString(string paraName, string defaultValue)
        {
            var paraValue = GetParameterValue(paraName);
            return string.IsNullOrEmpty(paraValue) ? defaultValue : paraValue;
        }

        /// <summary>
        /// 获取可选的字符串参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值。如果参数不存在则返回值为 String.Empty 。</returns>
        public string GetString(string paraName)
        {
            return GetString(paraName, string.Empty);
        }

        #endregion

        #region GetBoolean

        /// <summary>
        /// 获取必需的布尔型参数值。
        /// </summary>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public bool GetRequiredBoolean(string paraName)
        {
            var paraValue = GetRequiredParameterValue(paraName);
            bool value;
            if (bool.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的布尔型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public bool GetBoolean(string paraName, bool defaultValue)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return defaultValue;
            }
            bool value;
            if (bool.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的布尔型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值。如果参数不存在则返回值为null。</returns>
        public bool? GetBoolean(string paraName)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return null;
            }
            bool value;
            if (bool.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        #endregion

        #region GetDateTime

        /// <summary>
        /// 获取必需的日期时间参数值。
        /// </summary>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public DateTime GetRequiredDateTime(string paraName)
        {
            var paraValue = GetRequiredParameterValue(paraName);
            DateTime value;
            if (DateTime.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的日期时间参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public DateTime GetDateTime(string paraName, DateTime defaultValue)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return defaultValue;
            }
            DateTime value;
            if (DateTime.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的日期时间参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值。如果参数不存在则返回值为 null 。</returns>
        public DateTime? GetDateTime(string paraName)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return null;
            }
            DateTime value;
            if (DateTime.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        #endregion

        #region GetInt32

        /// <summary>
        /// 获取必需的整型参数值。
        /// </summary>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public int GetRequiredInt32(string paraName)
        {
            var paraValue = GetRequiredParameterValue(paraName);
            int value;
            if (int.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的整型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public int GetInt32(string paraName, int defaultValue)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return defaultValue;
            }
            int value;
            if (int.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的整型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值。如果参数不存在则返回值为 null。</returns>
        public int? GetInt32(string paraName)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return null;
            }
            int value;
            if (int.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        #endregion

        #region GetInt64

        /// <summary>
        /// 获取必需的长整型参数值。
        /// </summary>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public long GetRequiredInt64(string paraName)
        {
            var paraValue = GetRequiredParameterValue(paraName);
            long value;
            if (long.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的长整型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public long GetInt64(string paraName, long defaultValue)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return defaultValue;
            }
            long value;
            if (long.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的长整型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值。如果参数不存在则返回值为null。</returns>
        public long? GetInt64(string paraName)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return null;
            }
            long value;
            if (long.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        #endregion

        #region GetInt16

        /// <summary>
        /// 获取必需的短整型参数值。
        /// </summary>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public short GetRequiredInt16(string paraName)
        {
            var paraValue = GetRequiredParameterValue(paraName);
            short value;
            if (short.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的短整型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public short GetInt16(string paraName, short defaultValue)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return defaultValue;
            }
            short value;
            if (short.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的短整型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值。如果参数不存在则返回值为null。</returns>
        public short? GetInt16(string paraName)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return null;
            }
            short value;
            if (short.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        #endregion

        #region GetDouble

        /// <summary>
        /// 获取必需的浮点型参数值。
        /// </summary>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public double GetRequiredDouble(string paraName)
        {
            var paraValue = GetRequiredParameterValue(paraName);
            double value;
            if (double.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的浮点型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public double GetDouble(string paraName, double defaultValue)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return defaultValue;
            }
            double value;
            if (double.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的浮点型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值。如果参数不存在则返回值为null。</returns>
        public double? GetDouble(string paraName)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return null;
            }
            double value;
            if (double.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        #endregion

        #region GetDecimal

        /// <summary>
        /// 获取必需的浮点型参数值。
        /// </summary>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public decimal GetRequiredDecimal(string paraName)
        {
            var paraValue = GetRequiredParameterValue(paraName);
            decimal value;
            if (decimal.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的浮点型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public decimal GetDecimal(string paraName, decimal defaultValue)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return defaultValue;
            }
            decimal value;
            if (decimal.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        /// <summary>
        /// 获取可选的浮点型参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值。如果参数不存在则返回值为null。</returns>
        public decimal? GetDecimal(string paraName)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return null;
            }
            decimal value;
            if (decimal.TryParse(paraValue, out value))
            {
                return value;
            }
            throw new Exception("Parameter Error: " + paraName);
        }

        #endregion

        #region GetGuid

        /// <summary>
        /// 获取必需的Guid参数值。
        /// </summary>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public Guid GetRequiredGuid(string paraName)
        {
            var paraValue = GetRequiredParameterValue(paraName);
            try
            {
                return new Guid(paraValue);
            }
            catch (Exception)
            {
                throw new Exception("Parameter Error: " + paraName);
            }
        }

        /// <summary>
        /// 获取可选的Guid参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public Guid GetGuid(string paraName, Guid defaultValue)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return defaultValue;
            }
            try
            {
                return new Guid(paraValue);
            }
            catch (Exception)
            {
                throw new Exception("Parameter Error: " + paraName);
            }
        }

        /// <summary>
        /// 获取可选的Guid参数值。
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值。如果参数不存在则返回值为 null 。</returns>
        public Guid? GetGuid(string paraName)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                return null;
            }
            try
            {
                return new Guid(paraValue);
            }
            catch (Exception)
            {
                throw new Exception("Parameter Error: " + paraName);
            }
        }

        #endregion

        #region GetEnum

        /// <summary>
        /// 获取可选的枚举参数值，可以解析整数形式或者枚举名称形式所描述的枚举值。
        /// </summary>
        /// <typeparam name="T">枚举的类型。</typeparam>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。</returns>
        public T GetRequiredEnum<T>(string paraName)
             where T : struct
        {
            return ParseEnum<T>(GetRequiredParameterValue(paraName));
        }

        /// <summary>
        /// 获取可选的枚举参数值，可以解析整数形式或者枚举名称形式所描述的枚举值。
        /// </summary>
        /// <typeparam name="T">枚举的类型。</typeparam>
        /// <param name="paraName">参数名称。</param>
        /// <param name="defaultValue">当参数不存在时的默认值。</param>
        /// <returns>参数值。如果参数不存在则返回值为 <paramref name="defaultValue"/> 。</returns>
        public T GetEnum<T>(string paraName, T defaultValue)
            where T : struct
        {
            var paraValue = GetParameterValue(paraName);
            return string.IsNullOrEmpty(paraValue) ? defaultValue : ParseEnum<T>(paraValue);
        }

        /// <summary>
        /// 获取可选的枚举参数值，可以解析整数形式或者枚举名称形式所描述的枚举值。
        /// </summary>
        /// <typeparam name="T">枚举的类型。</typeparam>
        /// <param name="paraName">参数名称。</param>
        /// <returns>参数值。如果参数不存在则返回值为 null</returns>
        public T? GetEnum<T>(string paraName)
            where T : struct
        {
            var paraValue = GetParameterValue(paraName);
            return string.IsNullOrEmpty(paraValue) ? (T?)null : ParseEnum<T>(paraValue);
        }

        private T ParseEnum<T>(string paraValue)
             where T : struct
        {
            int intValue;
            if (int.TryParse(paraValue, out intValue))
            {
                return (T)Enum.ToObject(typeof(T), intValue);
            }

            return (T)Enum.Parse(typeof(T), paraValue, true);
        }
        #endregion

        #region GetParameterValue

        private string GetParameterValue(string paraName)
        {
            if (string.IsNullOrEmpty(paraName))
            {
                throw new ArgumentNullException("paraName");
            }

            string paraValue = m_HttpContext == null
                                ? (HttpContext.Current.Request.QueryString[paraName] ?? string.Empty).Trim()
                                : (m_HttpContext.Request.QueryString[paraName] ?? string.Empty).Trim();

            var decodeParaValue = (HttpUtility.UrlDecode(paraValue) ?? string.Empty).Trim();

            return decodeParaValue;
        }

        private string GetRequiredParameterValue(string paraName)
        {
            var paraValue = GetParameterValue(paraName);
            if (string.IsNullOrEmpty(paraValue))
            {
                throw new Exception("Parameter Error: " + paraName);
            }
            return paraValue;
        }

        #endregion
    }
}
