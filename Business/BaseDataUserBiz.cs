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
using System.Collections;
using Qisda.PMS.Common;

namespace Qisda.PMS.Business
{
    public class BaseDataUserBiz : BaseBusiness  
    {
        public IList<BaseDataUser> SelectDoMainNameByLoginName(string item)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<BaseDataUser>("SelectDoMainNameByLoginName", item);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/SelectDoMainNameByLoginName:" + ex.ToString());
                return null;
            }
        }

        public string SelectUserDepartmentByLoginName(string loginName)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject("SelectUserDepartmentByLoginName", loginName).ToString();
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/SelectUserDepartmentByLoginName:" + ex.ToString());
                return null;
            }
        }

        //Add by Abel Li 20140102 
        public string SelectLeaderByLoginName(string loginName)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject("SelectLeaderByLoginName", loginName).ToString();
               
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/SelectLeaderByLoginName:" + ex.ToString());
                return null;
            }
        }

        public IList<BaseDataUser> SelectBaseDataUser(string loginName,string role)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("LoginName", loginName);
                ht.Add("Role", role);

                return m_PMSSqlConnection.QueryForList<BaseDataUser>("SelectBaseDataUser", ht);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/SelectBaseDataUser:" + ex.ToString());
                return null;
            }
        }

        public IList<BaseDataUser> SelectBaseDataUserByMainTainUser(string departMentId, string englishName,
                                 string ntDomain, string domain, string empNo, string extention, string role)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Department", departMentId);
                ht.Add("EnglishName", englishName);
                ht.Add("NTDomain", ntDomain);
                ht.Add("Domain", domain);
                ht.Add("EmpNo", empNo);
                ht.Add("Extention", extention);
                ht.Add("Role", role);

                return m_PMSSqlConnection.QueryForList<BaseDataUser>("SelectBaseDataUserByMainTainUser", ht);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/SelectBaseDataUserByMainTainUser:" + ex.ToString());
                return null;
            }
        }

        public BaseDataUser SetUserOrgRole(BaseDataUser user)
        {
            try
            {
                switch (user.Role.Replace(" ",""))
                {
                    case "PM":
                        user.IsOrgPM = true;
                        break;
                    case "RD":
                        user.IsOrgRD= true;
                        break;
                    case "RDLEADER":
                        user.IsOrgPMO = true;
                        break;
                    case "RDMANAGER":
                        user.IsOrgRDManager = true;
                        break;
                    case "PMMANAGER":
                        user.IsOrgPMManager = true;
                        break;
                    default:
                        break;
                }

                return user;
            }

            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/SetUserOrgRole:" + ex.ToString());
                throw ex;
            }
        }

        public void DeleteBaseDataUserById(string id)
        {
            try
            {
                m_PMSSqlConnection.BeginTransaction();

                m_PMSSqlConnection.Update("UpdateBaseDataDepartmentUser", id);
                m_PMSSqlConnection.Update("UpdateBaseDataUserById", id);

                m_PMSSqlConnection.CommitTransaction();
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/UpdateBaseDataUserById:" + ex.ToString());
                m_PMSSqlConnection.RollBackTransaction();
                throw ex;
            }
        }

        public BaseDataUser SetUserProjectRole(BaseDataUser user, string pmsId)
        {
            try
            {
                PmsHeadBiz pmsBO = new PmsHeadBiz();
                IList<PmsHead> pmsDTOList = pmsBO.SelectPmsHeadByPmsId(pmsId);
                PmsHead pmsDTO;
                if (pmsDTOList != null && pmsDTOList.Count > 0)
                {
                    pmsDTO = pmsDTOList[0];
                }
                else
                {
                    return user;
                }

                if (pmsDTO.Pm.ToUpper().Contains(user.LoginName.ToUpper()))
                {
                    user.IsProjectPM = true;
                }

                if (pmsDTO.Qa.ToUpper().Contains(user.LoginName.ToUpper()))
                {
                    user.IsProjectQA = true;
                }

                if (pmsDTO.Sd.ToUpper().Contains(user.LoginName.ToUpper()))
                {
                    user.IsProjectSD = true;
                }
                if (pmsDTO.Se.ToUpper().Contains(user.LoginName.ToUpper()))
                {
                    user.IsProjectSE = true;
                }

                return user;
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/SetUserProjectRole:" + ex.ToString());
                throw ex;
            }
        }

        public bool CheckBaseDataUser(string empNo)
        {
            try
            {
                int count=m_PMSSqlConnection.QueryForObject<int>("CheckUserIsExit", empNo);
                return count > 0;
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/CheckUserIsExit:" + ex.ToString());
                throw ex;
            }

        }

        public void UpdateUserInformation(string id,  string ntDomainText, string maintainUser,
                                          string domainValue, string departmentValue, string userName,
                                          string mailAddress, string role, string extention,string maintainTime)
        {

            try
            {
                Hashtable ht = new Hashtable();

                ht.Add("id", id);
                ht.Add("Ntdomain", ntDomainText);
                ht.Add("DomainId", domainValue);
                ht.Add("DepartmentId", departmentValue);
                ht.Add("UserName", userName);
                ht.Add("MailAddress", mailAddress);
                ht.Add("Role", role);
                ht.Add("Extention", extention);
                ht.Add("MaintainUser", maintainUser);
                ht.Add("MaintainDate", maintainTime);
                
                m_PMSSqlConnection.BeginTransaction();

                m_PMSSqlConnection.Update("UpdateBaseDataUserByMainTainId", ht);
                m_PMSSqlConnection.Update("UpdateBaseDataDepartmentUser", ht);  

                m_PMSSqlConnection.CommitTransaction();
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/CheckUserIsExit:" + ex.ToString());
                m_PMSSqlConnection.RollBackTransaction();
                throw ex;
            }

        }

        public void InsertBaseDataUserAndDepartmentUser(BaseDataUser baseDataUser, BaseDataDepartmentUser baseDataDepartmentUser)
        {
            try
            {
                m_PMSSqlConnection.Insert("InsertBaseDataUser", baseDataUser);

                baseDataDepartmentUser.UserId = m_PMSSqlConnection.QueryForObject<int>("SelectUserId", baseDataUser.UserEmployeeNo);
                m_PMSSqlConnection.Insert("InsertBaseDataDepartmentUser", baseDataDepartmentUser);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/InsertBaseDataUser:" + ex.ToString());
                m_Logger.Error("BaseDataUserBiz/InsertBaseDataDepartmentUser:" + ex.ToString());
                throw ex;
            }
        
        }

        public int GetTfsTeamForTeamid(string teamName)
        {
            try
            {
                string teamId = m_TFSSqlConnection.QueryForObject<string>("SelectTfsTeamForTeamid", teamName);
                teamId = teamId == null ? "0" : teamId;
                return Convert.ToInt32(teamId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/GetTfsTeamForTeamid:" + ex.ToString());
                throw ex;
            }

        }

        public void InsertTfsUserList(TfsUserList tfsUserList)
        {
            try
            {
                m_TFSSqlConnection.Insert("InsertTfsUserList", tfsUserList);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/InsertTfsUserList:" + ex.ToString());
              
                throw ex;
            }
        }

        public bool GetWscUserRoleCount(string name)
        {
            try
            {
                int rowCout = m_WSCSqlConnection.QueryForObject<int>("SelectWscUserRoleCount", name);
                if (rowCout > 0)
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
                m_Logger.Error("BaseDataUserBiz/SelectWscUserRoleCount:" + ex.ToString());
                throw ex;
            }
        }

        public bool GetTfsUserListUserNameCount(string userName)
        {
            try
            {
                int rowCout = m_TFSSqlConnection.QueryForObject<int>("SelectTfsUserListUserNameCount", userName);
                if (rowCout > 0)
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
                m_Logger.Error("BaseDataUserBiz/SelectTfsUserListUserNameCount:" + ex.ToString());
                throw ex;
            }
        }

        

        public void InsertWscUserRole(WscUserRole wscUserRole)
        {
            try
            {
                m_WSCSqlConnection.Insert("InsertWscUserRole", wscUserRole);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataUserBiz/InsertWscUserRole:" + ex.ToString());
                throw ex;
            }
        }

    }
}
