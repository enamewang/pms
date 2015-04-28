using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using Qisda.PMS.Entity;


namespace Qisda.PMS.Business
{
    public class SystemListPopUpBiz : BaseBusiness
    {
        #region DropDownList Domain
        public IList<string> GetItarmSystemDomain()
        {
            IList<string> resultList = m_ITARMSqlConnection.QueryForList<string>("SelectItarmSystemDomain", null);
            return resultList;
        }
        #endregion

        #region DropDownList Site
        public IList<ItarmSystem> GetItarmSystemSite()
        {
            IList<ItarmSystem> resultList = m_ITARMSqlConnection.QueryForList<ItarmSystem>("SelectItarmSystemSite", null);
            return resultList;
        }
        #endregion

        #region DropDownList PM
        public IList<ItarmUser> GetItarmPmNoName()
        {
            IList<ItarmUser> resultList = m_ITARMSqlConnection.QueryForList<ItarmUser>("SelectItarmPmNoName", null);
            return resultList;
        }
        #endregion


        #region 调用存储过程
        /// <summary>
        /// 获取SystemListPopUp页面GridView数据源
        /// </summary>
        /// <returns></returns>
        public IList<ItarmSystem> GetPersonsByName(string bname, string ename, string cname, string domain, string pm, string site)
        {
            Hashtable hashtable = new Hashtable { { "bname", bname }, { "ename", ename }, { "cname", cname }, { "domain", domain }, { "pm", pm }, { "site", site } };
            IList<ItarmSystem> resultList = m_ITARMSqlConnection.QueryForList<ItarmSystem>("SelectItarmSystemSystem", hashtable);
            return resultList;
        }
        #endregion
       

        //用于非法字符的转换。

        #region SqlEncode
        /// <summary>
        /// SqlEncode
        /// </summary>
        /// <param name="strCharacter"></param>
        /// <returns></returns>
        public string SqlEncode(string strCharacter)
        {
            strCharacter = strCharacter.Replace("'", "''");
            strCharacter = strCharacter.Replace("[", "[[]");
            strCharacter = strCharacter.Replace("_", "[_]");
            strCharacter = strCharacter.Replace("%", "[%]");
            return strCharacter;
        }
        #endregion












    }
}
