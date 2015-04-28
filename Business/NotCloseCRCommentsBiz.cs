using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;
using System.Web.UI;
using System.Security.Cryptography;
using System.Web;


namespace Qisda.PMS.Business
{
    public class NotCloseCRCommentsBiz : BaseBusiness
    {
        public IList<VPmsNotClosedcr> GetCRIDList(string loginName,string ParaDate)
        {
            try
            {
                Hashtable parahashtable = new Hashtable();
                parahashtable.Add("USERNAME",loginName);
                parahashtable.Add("ParamDate", ParaDate);
                return m_PMSSqlConnection.QueryForList<VPmsNotClosedcr>("GetVPmsNotClosedCRList", parahashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/GetVPmsNotClosedCRIDList:" + ex.ToString());
                return null;
            }
        }
        public VPmsNotClosedcr GetCRInfo(string pmsID)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject<VPmsNotClosedcr>("GetVPmsNotClosedCR", pmsID);
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/GetVPmsNotClosedCR:" + ex.ToString());
                return null;
            }
        }
        public CRComments GetCRComments(string strPMSID)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject<CRComments>("getCRComments", strPMSID);
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/getCRComments:" + ex.ToString());
                return null;
            }
        }

        public bool UpdatePMComments(string strPMSID, string strPMComments)
        {
            try
            {
                Hashtable hashPara = new Hashtable();
                hashPara.Add("PMComments", strPMComments);
                hashPara.Add("PMSID", strPMSID);
                if (m_PMSSqlConnection.Update("UpdatePMComments", hashPara) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/UpdatePMComments:" + ex.ToString());
                return false;
            }
        }
        public bool UpdateSDComments(string strPMSID, string strSDComments)
        {
            try
            {
                Hashtable hashPara = new Hashtable();
                hashPara.Add("SDComments", strSDComments);
                hashPara.Add("PMSID", strPMSID);
                if (m_PMSSqlConnection.Update("UpdateSDComments", hashPara) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/UpdateSDComments:" + ex.ToString());
                return false;
            }
        }
        public bool UpdateComments(CRComments comments)
        {
            try
            {
                Hashtable hashPara = new Hashtable();
                hashPara.Add("PMComments", comments.PMComments);
                hashPara.Add("SDComments", comments.SDComments);
                hashPara.Add("PMSID", comments.PMSID);
                if (m_PMSSqlConnection.Update("UpdateComments", hashPara) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/UpdatePMComments:" + ex.ToString());
                return false;
            }
        }
        public bool InsertCRComments(CRComments crcomments)
        {
            try
            {
                m_PMSSqlConnection.Insert("InsertSDComments", crcomments);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/InsertCRComments:" + ex.ToString());
                return false;
            }
        }
        /// <summary>

        /// NT account without domain, it's not same as GlobalDefinition.Cookie_LoginUser

        /// </summary>

        public string GetNTUser()
        {
            string strLogin_User = "";
            strLogin_User = HttpContext.Current.Request.ServerVariables["LOGON_USER"].Trim();
            if (strLogin_User == "")
            {
                strLogin_User = HttpContext.Current.User.Identity.Name.Trim();
            }
            if (strLogin_User == "")
            {
                HttpContext.Current.Response.Redirect("~/SysFrame/Login.aspx");
            }
            strLogin_User = strLogin_User.Substring(strLogin_User.IndexOf("\\") + 1, strLogin_User.Length - strLogin_User.IndexOf("\\") - 1);
            return strLogin_User;
        }

        public bool SaveComments(string userName,CRComments crComments)
        {
            try
            {
                if (CommentsExits(crComments.PMSID) == false)
                {
                    if (InsertCRComments(crComments) == true)
                    {
                        return true;
                    }
                }
                else
                {
                    if (IsLeader(userName) == true)
                    {
                        //save All Comments
                        if (UpdateComments(crComments) == true)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (IsPMRole(crComments.PMSID, userName) == true)
                        {
                            //save PM Comments
                            if (UpdatePMComments(crComments.PMSID, crComments.PMComments) == true)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            //save SD Comments
                            if (UpdateSDComments(crComments.PMSID, crComments.SDComments) == true)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/SaveComments:" + ex.ToString());
                return false;
            }
 
        }

        public bool CommentsExits(string pmsID)
        {
            long rowCount = m_PMSSqlConnection.QueryForObject<long>("getCRCommentsCount", pmsID);
            if (rowCount > 0)
            {
                return true;
            }
            return false;
        }

        public bool IsPMRole(string pmsID,string loginName)
        {
            Hashtable parahash = new Hashtable();
            parahash.Add("PMSID",pmsID);
            parahash.Add("LOGINNAME", loginName);
            long rowCount = m_PMSSqlConnection.QueryForObject<long>("checkIsPMRole", parahash);
            if (rowCount > 0)
            {
                return true;
            }
            return false;
        }
        public bool IsSDRole(string pmsID, string loginName)
        {
            Hashtable parahash = new Hashtable();
            parahash.Add("PMSID", pmsID);
            parahash.Add("LOGINNAME", loginName);
            long rowCount = m_PMSSqlConnection.QueryForObject<long>("checkIsSDRole", parahash);
            if (rowCount > 0)
            {
                return true;
            }
            return false;
        }
        public string GetMySQLDate()
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject<string>("getMySQLDate", null);
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/getMySQLDate:" + ex.ToString());
                return null;
            }
        }
        public bool IsLeader(string userName)
        {
            try
            {
                //modified by tim for chang Leader's scope
                //long count = m_PMSSqlConnection.QueryForObject<long>("CheckIsLeader", userName);
                //if (count == 0)
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
                string LeaderList = "Robert.Yang;Harris.Chang;Ike.Huang;Stanley.Wu;Stanley.Lee;Tyler.Liu;Benjemin.Deng;Sammi.Yao;Eric.Q.Liu;Arty.Yu";
                if (LeaderList.ToUpper().Contains(userName.ToUpper()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //End Modifired
            }
            catch (Exception ex)
            {
                m_Logger.Error("NotCloseCRCommentsBiz/CheckIsLeader:" + ex.ToString());
                return false;
            }
        }
    }
}
