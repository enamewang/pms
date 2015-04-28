using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;
using MySql.Data;
using System.Web.UI;
using System.Collections;
using System.Globalization;

namespace Qisda.PMS.Business
{
    public class PmsCommonBiz : BaseBusiness
    {

        public bool CheckUser(string ename)
        {
            BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
            //BaseDataUser baseDataUser = new BaseDataUser();
            //baseDataUser.LoginName = ename;

            IList<BaseDataUser> baseDataUserList = baseDataUserBiz.SelectBaseDataUser(ename, null);
            if (baseDataUserList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int ConvertStringToInt(string value)
        {
            try
            {
                int result = 0;
                if (!value.Trim().Equals(string.Empty))
                {
                    result = Convert.ToInt32(value);
                }
                return result;
            }
            catch (Exception ex)
            {

                m_Logger.Error("PmsCommonBiz/ConvertStringToInt:" + ex.Message.ToString());
                return 0;
            }
        }

        public float ConvertStringToFloat(string value)
        {
            try
            {
                float result = 0;
                if (!value.Trim().Equals(string.Empty))
                {
                    result = float.Parse(value);
                }
                return result;
            }
            catch (Exception ex)
            {

                m_Logger.Error("PmsCommonBiz/ConvertStringToFloat:" + ex.Message.ToString());
                return 0;
            }

        }
        //add by  Ename Wang on 2011-08-25
        /// <summary>
        /// convert datetime to string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ConvertDateToString(DateTime dateTime)
        {
            try
            {
                string result = string.Empty;
                if (dateTime != null)
                {
                    result = (dateTime.ToString("yyyy-MM-dd").Equals("1900-01-01")) ? string.Empty : dateTime.ToString("yyyy-MM-dd");
                }
                return result;
            }
            catch (Exception ex)
            {

                m_Logger.Error("pmsCommonBiz/ConvertDateToString:" + ex.Message.ToString());
                return string.Empty;
            }
        }
        public decimal ConvertDecimal(string value)
        {
            try
            {
                decimal result = 0;
                if (!value.Trim().Equals(string.Empty))
                {
                    result = Convert.ToDecimal(value);
                }
                return result;
            }
            catch (Exception ex)
            {

                m_Logger.Error("CommonBusiness/ConvertDecimal:" + ex.Message.ToString());
                return 0;
            }
        }

        /// <summary>
        /// check control value is empty or not,if is empty return true,else return false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckControlEmpty(string value)
        {
            try
            {
                bool result = false;
                if (value.Trim().Equals(string.Empty))
                    result = true;
                return result;
            }
            catch (Exception ex)
            {

                m_Logger.Error("CommonBusiness/CheckControlEmpty:" + ex.Message.ToString());
                return false;
            }
        }

        // end add on 2011-08-25

        public string FormatDateTime(string value)
        {
            try
            {
                string result = string.Empty;
                if (!value.Trim().Equals("1900-01-01") && !value.Trim().Equals("0001-01-01")
                    && !value.Trim().Equals("01-01") && !value.Trim().Equals("0001-1-1") && !value.Trim().Equals("0000-00-00") && !value.Trim().Equals("1899-12-29"))
                {
                    result = value.Trim();
                }
                else
                {
                    result = "";
                }

                return result;
            }
            catch (Exception ex)
            {

                m_Logger.Error("PmsCommonBiz/FormatDateTime:" + ex.Message.ToString());
                return string.Empty;
            }
        }

        public DateTime ConvertDateTime(string value)
        {
            try
            {
                DateTime result = new DateTime(1900, 1, 1);
                if (!value.Trim().Equals(string.Empty))
                {
                    result = DateTime.Parse(value);
                }
                return result;
            }
            catch (Exception ex)
            {

                m_Logger.Error("PmsCommonBiz/ConvertDateTime:" + ex.Message.ToString());
                return new DateTime(1900, 01, 01);
            }
        }

        public string GetHtmlContent(string filepath)
        {

            FileInfo fileInfo = new FileInfo(filepath);

            using (StreamReader readerText = fileInfo.OpenText())
            {
                return readerText.ReadToEnd();
            }
        }

        //时间格式化函数
        public static string FormatDate(DateTime date, string pattern)
        {
            DateTime meanlessDate = new DateTime(0x76c, 1, 1);
            if (date == meanlessDate)
            {
                return string.Empty;
            }
            string format = pattern;
            if (string.IsNullOrEmpty(format.Trim()))
            {
                format = "yyyy/MM/dd";
            }
            DateTimeFormatInfo provider = new DateTimeFormatInfo();
            format = format.Replace('Y', 'y').Replace('m', 'M').Replace('D', 'd');
            provider.LongDatePattern = format;
            provider.ShortDatePattern = format;
            return date.ToString(format, provider);
        }


        /// <summary> 
        /// 将字符串运用 base64算法加密 
        /// </summary> 
        /// <param name="code_type">编码类型（编码名称） 
        /// * 代码页 名称 
        /// * 1200 "UTF-16LE"、"utf-16"、"ucs-2"、"unicode"或"ISO-10646-UCS-2" 
        /// * 1201 "UTF-16BE"或"unicodeFFFE" 
        /// * 1252 "windows-1252" 
        /// * 65000 "utf-7"、"csUnicode11UTF7"、"unicode-1-1-utf-7"、"unicode-2-0-utf-7"、"x-unicode-1-1-utf-7"或"x-unicode-2-0-utf-7" 
        /// * 65001 "utf-8"、"unicode-1-1-utf-8"、"unicode-2-0-utf-8"、"x-unicode-1-1-utf-8"或"x-unicode-2-0-utf-8" 
        /// * 20127 "us-ascii"、"us"、"ascii"、"ANSI_X3.4-1968"、"ANSI_X3.4-1986"、"cp367"、"csASCII"、"IBM367"、"iso-ir-6"、"ISO646-US"或"ISO_646.irv:1991" 
        /// * 54936 "GB18030"    
        /// </param> 
        /// <param name="code">待加密的字符串</param> 
        /// <returns>加密后的字符串</returns> 
        public static string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);  //将一组字符编码为一个字节序列. 
            try
            {
                encode = Convert.ToBase64String(bytes);  //将8位无符号整数数组的子集转换为其等效的,以64为基的数字编码的字符串形式. 
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        /// <summary> 
        /// 将字符串运用 base64算法解密 
        /// </summary> 
        /// <param name="code_type">编码类型</param> 
        /// <param name="code">已用base64算法加密的字符串</param> 
        /// <returns>解密后的字符串</returns> 
        public static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);  //将2进制编码转换为8位无符号整数数组. 
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);  //将指定字节数组中的一个字节序列解码为一个字符串。 
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        #region Registe


        /// <summary>
        /// Common registe function for start up script
        /// key is generated by Guid, then ensure key is not repeat
        /// </summary>
        /// <param name="page"></param>
        /// <param name="script"></param>
        public void CommonRegisteStart(Page page, string script)
        {
            string key = Guid.NewGuid().ToString();
            if (page.IsStartupScriptRegistered(key) == false)
            {
                page.RegisterStartupScript(key, script);
            }
        }

        /// <summary>
        /// Common registe function for block script 
        /// key is generated by Guid, then ensure key is not repeat
        /// </summary>
        /// <param name="page"></param>
        /// <param name="script"></param>
        public void CommonRegisteBlock(Page page, string script)
        {
            string key = Guid.NewGuid().ToString();
            if (page.IsClientScriptBlockRegistered(key) == false)
            {
                page.RegisterClientScriptBlock(key, script);
            }
        }

        #region Refactory function
        /// <summary>
        /// Construct array start up script, and use common registe function to registe script to page
        /// </summary>
        /// <param name="page">target page</param>
        /// <param name="controlID1">control's client id</param>
        /// <param name="controlID2">control's client id</param>
        /// <param name="arrayName">array's name what you want to use in the page</param>
        public void RegisteClientScript(Page page, string controlID1, string controlID2, string arrayName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"<script language=""javascript"">");
            builder.AppendFormat("var {0} = new Array(", arrayName);
            builder.AppendFormat("\"{0}\", ", controlID1);
            builder.AppendFormat("\"{0}\"", controlID2);
            builder.Append(");</script>");

            CommonRegisteStart(page, builder.ToString());
        }

        /// <summary>
        /// Construct array start up script, and use common registe function to registe script to page
        /// </summary>
        /// <param name="page">target page</param>
        /// <param name="controlsID">collection for control's client id</param>
        /// <param name="arrayName">array's name what you want to use in the page</param>
        public void RegisteClientScript(Page page, string[] controlsID, string arrayName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"<script language=""javascript"">");
            builder.AppendFormat("var {0} = new Array(", arrayName);

            foreach (string controlID in controlsID)
            {
                builder.AppendFormat("\"{0}\",", controlID);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(");</script>");

            CommonRegisteStart(page, builder.ToString());
        }

        /// <summary>
        /// Construct array start up script, and use common registe function to registe script to page
        /// </summary>
        /// <param name="page">target page</param>
        /// <param name="arrayList">collection for control's client id</param>
        /// <param name="arrayName">array's name what you want to use in the page</param>
        public void RegisteClientScript(Page page, ArrayList arrayList, string arrayName)
        {
            //if (arrayList == null)
            //    return;

            string[] controlsID = new string[arrayList.Count];

            int i = 0;
            foreach (object array in arrayList)
            {
                controlsID[i] = array.ToString();
                i++;
            }

            RegisteClientScript(page, controlsID, arrayName);
        }

        /// <summary>
        /// Registe start up function to run certain script
        /// </summary>
        /// <param name="page"></param>
        /// <param name="functionName">functionName without parameters</param>
        public void RegisteClientScript(Page page, string functionName)
        {
            string scriptHelper = @"<script language=""javascript"">{0}();</script>";
            CommonRegisteStart(page, string.Format(scriptHelper, functionName));
        }

        /// <summary>
        /// Construct array start up script, and use common registe function to registe script to page-- By Jerry Jiang for system maintaince
        /// </summary>
        /// <param name="page">target page</param>
        /// <param name="controlsID">collection for control's client id</param>
        /// <param name="arrayName">array's name what you want to use in the page</param>
        public void RegisteClientScriptForSystemMaintance(Page page, string[] controlsID, string arrayName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"<script language=""javascript"" >");
            builder.AppendFormat("var {0} = new Array(", arrayName);

            foreach (string controlID in controlsID)
            {
                builder.AppendFormat("\"{0}\",", controlID);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(")</script>");

            string script = builder.ToString();
            page.RegisterStartupScript(arrayName, script);
        }

        #endregion

        /// <summary>
        /// 计算两个日期之间的周数
        /// </summary>
        /// <param name="dateFrom">起始日期</param>
        /// <param name="dateTo">结束日期</param>
        /// <returns>两个日期之间的周数</returns>
       
         //--modified by ITO.Abel.Li on 20131228  星期第一天改为星期一  取前一天比较
        public static int NumberOfWeeks(DateTime dateFrom, DateTime dateTo)
        {
            TimeSpan Span = dateTo.Subtract(dateFrom);

            if (Span.Days <= 7)
            {
                if (dateFrom.AddDays(-1).DayOfWeek > dateTo.AddDays(-1).DayOfWeek)
                {
                    return 2;
                }

                return 1;
            }

            int Days = Span.Days - 7 + (int)dateFrom.DayOfWeek;
            int WeekCount = 1;
            int DayCount = 0;

            for (WeekCount = 1; DayCount < Days; WeekCount++)
            {
                DayCount += 7;
            }

            return WeekCount;
        }

        #endregion

    }
}
