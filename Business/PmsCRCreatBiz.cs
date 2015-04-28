using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;
using System.Web.UI;

namespace Qisda.PMS.Business
{
    public class PmsCRCreatBiz : BaseBusiness
    {
        private bool CheckVersionFormat(string[] version, string flag, out string message)
        {
            message = string.Empty;
            if (version.Length == 3)
            {
                if (version[1].Length != 4 || version[2].Length != 2)
                {
                    message = flag == "old" ? "System old version is wrong,please contact PMS director to edit the value! \r\n Old version Format: AA.BBCC.DD ." : "Please Check new version, New version Format: AA.BBCC.DD";
                    return false;
                }
            }
            else
            {
                //checkResult = false;
                message = flag == "old" ? "System old version is wrong,please contact PMS director to edit the value! \r\n Old version Format: AA.BBCC.DD!" : "Please Check new version,New version Format: AA.BBCC.DD";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查版本号是否符合要求
        /// </summary>
        /// <param name="oldVersion">旧版本号</param>
        /// <param name="newVersion">新版本号</param>
        /// <param name="message">错误的提示信息</param>
        /// <param name="falg">返回new或者old，用以判断是哪个version出错了。</param>
        /// <returns></returns>
        public bool CheckVersion(string oldVersion, string newVersion, out string message, out string falg)
        {

            if (string.IsNullOrEmpty(oldVersion))
            {
                message = "The old version can not be empty, please contact PMS director to set up the value!";
                falg = "old";
                return false;
            }

            if (string.IsNullOrEmpty(newVersion))
            {
                message = "Please input NewVersion!";
                falg = "new";
                return false;
            }

            if (oldVersion == newVersion)
            {
                message = "NewVersion and OldVersion is same!";
                falg = "new";
                return false;
            }

            string[] arrTmpOldVersion = oldVersion.Split('.');
            string[] arrTmpNewVersion = newVersion.Split('.');

            if (!CheckVersionFormat(arrTmpOldVersion, "old", out message))
            {
                falg = "old";
                return false;
            }
            if (!CheckVersionFormat(arrTmpNewVersion, "new", out message))
            {
                falg = "new";
                return false;
            }

            int oldV;
            int newV;
            if (!int.TryParse(arrTmpOldVersion[0] + arrTmpOldVersion[1], out oldV))
            {
                message = "Old version Format: AA.BBCC.DD.Type of  AA and BBCC shoud be number, please contact PMS director to edit the value!";
                falg = "old";
                return false;
            }
            if (!int.TryParse(arrTmpNewVersion[0] + arrTmpNewVersion[1], out newV))
            {
                message = "New version Format: AA.BBCC.DD. Type of  AA and BBCC shoud be number!";
                falg = "new";
                return false;
            }

            if (oldV > newV)
            {
                message = "NewVersion shoud be larger than OldVersion!";
                falg = "new";
                return false;
            }
            else
            {
                if (oldV == newV)
                {
                    byte[] arrayOld1 = new byte[1];
                    arrayOld1 = System.Text.Encoding.ASCII.GetBytes(arrTmpOldVersion[2].Substring(0, 1).ToUpper());
                    byte[] arrayNew1 = new byte[1];
                    arrayNew1 = System.Text.Encoding.ASCII.GetBytes(arrTmpNewVersion[2].Substring(0, 1).ToUpper());
                    int asciicodeOld1 = (int)(arrayOld1[0]);
                    int asciicodeNew1 = (int)(arrayNew1[0]);

                    if (asciicodeOld1 > asciicodeNew1)
                    {
                        message = "NewVersion shoud be larger than OldVersion!";
                        falg = "new";
                        return false;
                    }
                    else
                    {
                        if (asciicodeOld1 == asciicodeNew1)
                        {
                            byte[] arrayOld2 = new byte[1];
                            arrayOld2 = System.Text.Encoding.ASCII.GetBytes(arrTmpOldVersion[2].Substring(1, 1).ToUpper());
                            byte[] arrayNew2 = new byte[1];
                            arrayNew2 = System.Text.Encoding.ASCII.GetBytes(arrTmpNewVersion[2].Substring(1, 1).ToUpper());
                            int asciicodeOld2 = (int)(arrayOld2[0]);
                            int asciicodeNew2 = (int)(arrayNew2[0]);

                            if (asciicodeOld2 >= asciicodeNew2)
                            {
                                message = "NewVersion shoud be larger than OldVersion!";
                                falg = "new";
                                return false;
                            }
                        }

                    }
                }
            }


            falg = "new";
            return true;
        }


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

        #region Get New PmsID
        public bool GetNewPmsIdTempCrId(out DateTime creatDateTime, out string newPmsId, out string newTempCrId)
        {
            creatDateTime = PmsSysBiz.GetDBDateTime();
            string creatDate = FormatDate(creatDateTime, "yyyyMMdd");

            //获取PmsId
            string pmsIdPart = "PMS" + creatDate;
            PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
            IList<PmsHead> pmsHeadListPmsId = pmsHeadBiz.SelectPmsHeadByPmsId(pmsIdPart);

            // pmsIdListCount = pmsHeadListPmsId.Count;
            if (pmsHeadListPmsId != null && pmsHeadListPmsId.Count > 0)
            {
                //目前不考虑“9999”的情况
                //取最大的记录加1
                int maxPmsIdNum = int.Parse(pmsHeadListPmsId[pmsHeadListPmsId.Count - 1].PmsId.Substring(11, 4));
                int tempPmsIdNum;
                for (int i = 0; i < pmsHeadListPmsId.Count; i++)
                {
                    tempPmsIdNum = int.Parse(pmsHeadListPmsId[i].PmsId.Substring(11, 4));
                    if (maxPmsIdNum < tempPmsIdNum)
                    {
                        maxPmsIdNum = tempPmsIdNum;
                    }
                }
                maxPmsIdNum = maxPmsIdNum + 1;

                string pmsIdNum = maxPmsIdNum.ToString();

                //不足四位补“0”
                while (pmsIdNum.Length < 4)
                {
                    pmsIdNum = "0" + pmsIdNum;
                }
                ////不足四位补“0”
                //do
                //{
                //    pmsIdNum = "0" + pmsIdNum;

                //} while (pmsIdNum.Length < 4);

                newPmsId = pmsIdPart + pmsIdNum;
            }
            else
            {
                newPmsId = pmsIdPart + "0001";
            }

            //获取CrId
            string tempCrIdPart = "T" + creatDate;
            //IList<PmsHead> pmsHeadListCrId = pmsHeadBiz.SelectPmsHeadByTempCrIdPart(tempCrIdPart);
            IList<PmsItarmMapping> pmsItarmMappingList = new PmsItarmMappingBiz().SelectPmsItarmMappingByTempCrIdPart(tempCrIdPart);

            if (pmsItarmMappingList != null && pmsItarmMappingList.Count > 0)
            {
                //目前不考虑“9999”的情况
                //取最大的记录加1
                int maxCrIdNum = int.Parse(pmsItarmMappingList[pmsItarmMappingList.Count - 1].CrId.Substring(9, 4));
                int tempCrIdNum;
                for (int j = 0; j < pmsItarmMappingList.Count; j++)
                {
                    tempCrIdNum = int.Parse(pmsItarmMappingList[j].CrId.Substring(9, 4));
                    if (maxCrIdNum < tempCrIdNum)
                    {
                        maxCrIdNum = tempCrIdNum;
                    }
                }
                maxCrIdNum = maxCrIdNum + 1;

                string crIdNum = maxCrIdNum.ToString();

                //不足四位补“0”
                while (crIdNum.Length < 4)
                {
                    crIdNum = "0" + crIdNum;
                }
                //do
                //{
                //    crIdNum = "0" + crIdNum;

                //} while (crIdNum.Length < 4);
                newTempCrId = tempCrIdPart + crIdNum;
            }
            else
            {
                newTempCrId = tempCrIdPart + "0001";
            }

            return true;
        }
        #endregion

        //时间格式化函数
        public string FormatDate(DateTime date, string pattern)
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

        public string GetOwnerDept(string loginName)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject<string>("SelectBaseDataDepartmentName", loginName);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsCRCreatBiz/SelectBaseDataDepartmentName:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsSystemVersion> SelectPmsSystemVersionByDomainSystem(string systemDomain, string sysName, string site)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "SystemDomain", systemDomain }, { "System", sysName }, { "Site", site } };
                return m_PMSSqlConnection.QueryForList<PmsSystemVersion>("SelectPmsSystemVersionByDomainSystem", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsCRCreatBiz/SelectPmsSystemVersionByDomainSystem:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsSystemVersion> SelectPmsSystemVersionByTeamDomainSite(string teamDomain, string site)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "TeamDomain", teamDomain }, { "Site", site } };
                return m_PMSSqlConnection.QueryForList<PmsSystemVersion>("SelectPmsSystemVersionByTeamDomainSite", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsCRCreatBiz/SelectPmsSystemVersionByTeamDomainSite:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsSystemVersion> SelectPmsSystemVersionByTeamDomain(string teamDomain)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "TeamDomain", teamDomain } };
                return m_PMSSqlConnection.QueryForList<PmsSystemVersion>("SelectPmsSystemVersionByTeamDomain", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsCRCreatBiz/SelectPmsSystemVersionByTeamDomainSite:" + ex.ToString());
                return null;
            }
        }
        public PmsSystemVersion SelectPmsSystemVersionByTeamSystemSite(string teamDomain, string system, string site)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "SystemDomain", teamDomain }, { "System", system }, { "Site", site } };
                return m_PMSSqlConnection.QueryForList<PmsSystemVersion>("SelectPmsSystemVersionByTeamSystemSite", hashtable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsCRCreatBiz/SelectPmsSystemVersionByDomainSystem:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 更新系统表的version参数
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="sysName"></param>
        /// <param name="oldVersion">当前系统的version</param>
        /// <param name="newVersion">下一个版本的version(改版本并未使用，预留做下一次使用)</param>
        /// <returns></returns>
        private bool UpdatePmsSystemVersionOldVersionNewVersion(string systemDomain, string sysName, string site, string oldVersion, string newVersion)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "SystemDomain", systemDomain }, { "System", sysName }, { "Site", site }, { "OldVersion", oldVersion }, { "NewVersion", newVersion } };
                m_PMSSqlConnection.Update("UpdatePmsSystemVersionOldVersionNewVersion", hashtable);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsCRCreatBiz/UpdatePmsSystemVersionOldVersionNewVersion:" + ex.ToString());
                return false;
            }
        }

        //当前页面上的newversion
        public bool UpdateSysVersion(string domain, string sysName, string site, string currentVersion)
        {
            try
            {
                var versionList = currentVersion.Split('.');
                int newVerPart;
                if (versionList[1] != "9999")
                {
                    newVerPart = int.Parse(versionList[1]) + 1;
                    versionList[1] = newVerPart.ToString();
                    //不足四位补齐
                    while (versionList[1].Length < 4)
                    {
                        versionList[1] = "0" + versionList[1];
                    }
                }
                else
                {
                    newVerPart = int.Parse(versionList[0]) + 1;
                    versionList[0] = newVerPart.ToString();
                    versionList[1] = "0000";
                }
                string newVersion = versionList[0] + "." + versionList[1] + "." + versionList[2];

                //更新系统表PmsSystemVersion
                bool result = UpdatePmsSystemVersionOldVersionNewVersion(domain, sysName, site, currentVersion, newVersion);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsCRCreatBiz/UpdateSysVersion:" + ex.ToString());
                return false;
            }


        }

        public bool InsertItarmCrList(ItarmCrList itarmCrList)
        {
            try
            {
                m_PMSSqlConnection.Insert("InsertItarmCrList", itarmCrList);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsCRCreatBiz/InsertItarmCrList:" + ex.ToString());
                return false;
            }
        }














    }
}
