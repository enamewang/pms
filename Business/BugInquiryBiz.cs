using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class BugInquiryBiz : BaseBusiness
    {
        //DropDownListTeam
        public IList<BaseDataDepartment> SelectBaseDataDepartmentNameId()
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<BaseDataDepartment>("SelectBaseDataDepartmentNameId", null);

            }
            catch (Exception ex)
            {
                m_Logger.Error("BugInquiryBiz/SelectBaseDataDepartmentNameId:" + ex.ToString());
                return null;
            }
        }

        //DropDownList
        public IList<BaseDataUser> SelectUserName()
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<BaseDataUser>("SelectUserName", null);

            }
            catch (Exception ex)
            {
                m_Logger.Error("BugInquiryBiz/SelectUserName:" + ex.ToString());
                return null;
            }
        }

        //根据页面上输入的crId,qa,sd查询CRId CrName Sd
        private IList<ItarmCrList> SelectCRIdNamePMSIdSdByQaSd(string qa, string sd, DateTime crCloseDateFrom, DateTime crCloseDateTo)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Qa", qa }, { "Sd", sd }, { "CloseDateFrom", crCloseDateFrom }, { "CloseDateTo", crCloseDateTo } };
                return m_PMSSqlConnection.QueryForList<ItarmCrList>("SelectCRIdNamePMSIdSdByQaSd", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BugInquiryBiz/SelectCRIdNamePMSIdSdByQaSd:" + ex.ToString());
                return null;
            }

        }

        private IList<ItarmCrList> SelectCRIdNamePMSIdSdByCrId(string crId)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "CrId", crId } };

                return m_PMSSqlConnection.QueryForList<ItarmCrList>("SelectCRIdNamePMSIdSdByCrId", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BugInquiryBiz/SelectCRIdNamePMSIdSdByCrId:" + ex.ToString());
                return null;
            }
        }

        private IList<ItarmCrList> SelectCRIdNamePMSIdSdByCrIdQaSd(string crId, string qa, string sd, DateTime crCloseDataFrom, DateTime crCloseDataTo)
        {
            IList<ItarmCrList> result;
            IList<ItarmCrList> resultQaSd = SelectCRIdNamePMSIdSdByQaSd(qa, sd, crCloseDataFrom, crCloseDataTo);

            if (!string.IsNullOrEmpty(crId))
            {
                result = new List<ItarmCrList>();
                IList<ItarmCrList> resultCrId = SelectCRIdNamePMSIdSdByCrId(crId);
                foreach (ItarmCrList c in resultCrId)
                {
                    if (resultQaSd.Select(t => t).Where(m => m.CrId == c.CrId && m.CrName == c.CrName && m.Sd == c.Sd).Count() > 0)
                    {
                        result.Add(c);
                    }
                }
            }
            else
            {
                result = resultQaSd;
            }


            //foreach (ItarmCrList r in result)
            //{
            //    foreach (ItarmCrList s in result)
            //    {
            //        if (r.CrId == s.CrId && r.CrName == s.CrName)
            //        {
            //            var rsd = r.Sd.Split(',').ToList();
            //            var ssd = s.Sd.Split(',').ToList();
            //            foreach (string s1 in ssd.Where(s1 => !rsd.Contains(s1)))
            //            {
            //                rsd.Add(s1);
            //            }

            //            string sds = rsd.Aggregate(string.Empty, (current, m) => current + ";" + m);
            //            if (!string.IsNullOrEmpty(sds))
            //            {
            //                r.Sd = sds.Substring(1, sds.Length - 1);
            //            }

            //        }
            //    }
            //}

            if (result != null)
            {
                result = result.Distinct().ToList();
            }
            return result;
        }

        //根据Team查询该Team中包含的人员。
        private IList<BaseDataUser> SelectBaseDataUserUserNameByTeam(string departmentId)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "DepartmentId", departmentId } };
                return m_PMSSqlConnection.QueryForList<BaseDataUser>("SelectBaseDataUserUserNameByTeam", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BugInquiryBiz/SelectBaseDataUserUserNameByTeam:" + ex.ToString());
                return null;
            }
        }

        //获取Bug责任人名单
        private string GetDutyUserNames(string teamIds, string bugOwner)
        {
            string userNames = string.Empty;
            //根据team查询该team包含你的人员姓名
            IList<BaseDataUser> teamUserName = SelectBaseDataUserUserNameByTeam(teamIds);

            //如果bugOwner不在查询结果集teamUserName中，则team和bugOwner同时作为查询条件时，两者没有交集，将查询不到结果。
            if (!string.IsNullOrEmpty(bugOwner))
            {
                var listUserName = teamUserName.Where(n => n.UserName.StartsWith(bugOwner) || n.UserName.EndsWith(bugOwner) || n.UserName.IndexOf(bugOwner) != -1).Select(t => t.UserName).ToList();
                if (listUserName == null || listUserName.Count == 0)
                {
                    return null;
                }
                else
                {
                    userNames = listUserName.Aggregate(string.Empty, (current, s) => "'" + s + "'," + current);

                    if (!string.IsNullOrEmpty(userNames))
                    {
                        userNames = userNames.Substring(0, userNames.Length - 1);
                    }
                    return userNames;
                }
            }
            else
            {
                userNames = teamUserName.Select(t => t.UserName).Aggregate(string.Empty, (current, s) => "'" + s + "'," + current);
                if (!string.IsNullOrEmpty(userNames))
                {
                    userNames = userNames.Substring(0, userNames.Length - 1);
                }
                return userNames;
            }


        }

        //根据变量参数查询Bug的相关信息
        //其中crIds为通过页面上的Team,SD,QA查询出来的crid集合
        private IList<BfBuginfo> SelectBfBugInfoPart(string crIds, string bugOwners,
            string bugCreator, DateTime bugCreateDateFrom, DateTime bugCreateDateTo,
            DateTime bugResolvedDateFrom, DateTime bugResolvedDateTo,
            DateTime bugClosedDateFrom, DateTime bugClosedDateTo)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("CrIds", crIds);
                hashtable.Add("BugOwner", bugOwners);
                hashtable.Add("BugCreator", bugCreator);
                hashtable.Add("CreateDateFrom", bugCreateDateFrom);
                hashtable.Add("CreateDateTo", bugCreateDateTo);
                hashtable.Add("ResolvedDateFrom", bugResolvedDateFrom);
                hashtable.Add("ResolvedDateTo", bugResolvedDateTo);
                hashtable.Add("ClosedDateFrom", bugClosedDateFrom);
                hashtable.Add("ClosedDateTo", bugClosedDateTo);

                IList<BfBuginfo> result = m_BugFreeSqlConnection.QueryForList<BfBuginfo>("SelectBfBugInfoPart", hashtable);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("BugInquiryBiz/SelectBfBugInfoPart:" + ex.ToString());
                return null;
            }
        }


        private IList<BfBuginfo> GetBfBuginfoUnionCrInfor(IList<ItarmCrList> itarmCrList, IList<BfBuginfo> bfBuginfo)
        {
            if (itarmCrList == null || itarmCrList.Count == 0 || bfBuginfo == null || bfBuginfo.Count == 0)
            {
                return null;
            }

            //将两个集合合并为一个
            IList<BfBuginfo> result = new List<BfBuginfo>();
            foreach (BfBuginfo b in bfBuginfo)
            {
                foreach (ItarmCrList c in itarmCrList.Where(c => b.BugMachine == c.CrId))
                {
                    BfBuginfo bf = new BfBuginfo();
                    bf.CrId = c.CrId;
                    bf.CrName = c.CrName;
                    bf.Sd = c.Sd;
                    bf.PmsId = c.PmsId;
                    bf.DutyBy = b.DutyBy;
                    bf.BugId = b.BugId;
                    bf.BugTitle = b.BugTitle;
                    bf.ClosedDate = b.ClosedDate;

                    result.Add(bf);
                }
            }

            return result;
        }

        public IList<BfBuginfo> GetBugInfoCrInfor
            (
            string crId, string crSd,
            string crQa, string bugOwner,
            string team, string bugCreator,
            DateTime crCloseDataFrom, DateTime crCloseDataTo,
            DateTime bugCreateDateFrom, DateTime bugCreateDateTo,
            DateTime bugResolvedDateFrom, DateTime bugResolvedDateTo,
            DateTime bugClosedDateFrom, DateTime bugClosedDateTo
            )
        {

            IList<string> teamIdList = GetAllTeamIdByParentId(team);
            string teamIds = teamIdList.Aggregate(string.Empty, (current, id) => current + "," + id);
            if (!string.IsNullOrEmpty(teamIds))
            {
                teamIds = teamIds.Substring(1, teamIds.Length - 1);
            }

            #region 获取Bug的责任人userNameBugDuty(分两部分，第一部分是PMS数据库中的人员，第二部分是Other，Closed，Active)
            //根据teamIds, bugOwner查询Bug的责任人，两者之间取交集，如果不存在，则查询结果也为空，返回null;
            string userNameBugDuty = GetDutyUserNames(teamIds, bugOwner);
            if (string.IsNullOrEmpty(userNameBugDuty))
            {
                return null;
            }
            //BugFree中的人名都是不带点的，因此需要把点去掉，换成空格
            userNameBugDuty = userNameBugDuty.Replace(".", " ");
            #endregion

            #region 获取CrId：crIds(以及CR的相关信息)
            //根据crId, crQa, crSd查询CR的相关信息
            //CrId,CrName,Cr SD
            IList<ItarmCrList> itarmCrList = SelectCRIdNamePMSIdSdByCrIdQaSd(crId, crQa, crSd, crCloseDataFrom, crCloseDataTo);
            if (itarmCrList == null)
            {
                return null;
            }

            string crIds = itarmCrList.Select(t => t.CrId).Aggregate(string.Empty, (current, s) => "'" + s + "'," + current);
            if (!string.IsNullOrEmpty(crIds))
            {
                crIds = crIds.Substring(0, crIds.Length - 1);
            }
            else
            {
                return null;
            }

            #endregion

            #region 根据Bug责任人，Crid,CrCreator等查询BugInfor的信息
            if (!string.IsNullOrEmpty(bugCreator))
            {
                //BugFree中的人名都是不带点的，因此需要把点去掉，换成空格
                bugCreator = bugCreator.Replace(".", " ");
            }

            #region 当bugOwner为空时，需要增加几个人员Other，Closed，Active,才能查出所有的BUG
            //if (string.IsNullOrEmpty(team) && string.IsNullOrEmpty(bugOwner))
            if (string.IsNullOrEmpty(bugOwner))
            {
                userNameBugDuty = userNameBugDuty + ",'Other','Closed','Active'";
            }
            #endregion

            IList<BfBuginfo> bfBuginfo =
                SelectBfBugInfoPart(crIds, userNameBugDuty,
                bugCreator, bugCreateDateFrom, bugCreateDateTo,
                bugResolvedDateFrom, bugResolvedDateTo,
                bugClosedDateFrom, bugClosedDateTo);
            #endregion

            #region 将Bug信息和CR信息合并

            IList<BfBuginfo> result = GetBfBuginfoUnionCrInfor(itarmCrList, bfBuginfo);
            return result;
            #endregion
        }

        /// <summary>
        /// 通过当前的Id(部门)查找他所有的子id（子部门）
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        /// 
        public IList<string> teamIdlist = new List<string>();
        public IList<string> GetAllTeamIdByParentId(string parentId)
        {
            //IList<string> teamIds = new List<string>();
             teamIdlist.Add(parentId);

            IList<BaseDataDepartment> baseDataDepartmentTeamId = SelectBaseDataDepartmentIdByParentId(parentId);
            if (baseDataDepartmentTeamId != null && baseDataDepartmentTeamId.Count > 0)
            {
                foreach (BaseDataDepartment dataDepartment in baseDataDepartmentTeamId)
                {
                    teamIdlist.Add(dataDepartment.Id.ToString());
                    //teamIds.Concat(GetAllTeamIdByParentId(dataDepartment.Id.ToString()));
                    GetAllTeamIdByParentId(dataDepartment.Id.ToString());
                }

            }
            //return teamIdlist;

            return teamIdlist.Distinct().ToList(); ;
           
           // return teamIds.Distinct().ToList();
        }

       
        //public IList<string> GetTeamIdChild(string parentId)
        //{
        //    IList<BaseDataDepartment> baseDataDepartmentTeamId = SelectBaseDataDepartmentIdByParentId(parentId);
        //    if (baseDataDepartmentTeamId != null && baseDataDepartmentTeamId.Count > 0)
        //    {
        //        foreach (BaseDataDepartment dataDepartment in baseDataDepartmentTeamId)
        //        {
        //            teamIdlist.Add(dataDepartment.Id.ToString());
        //            //teamIds.Concat(GetAllTeamIdByParentId(dataDepartment.Id.ToString()));
        //            return GetTeamIdChild(dataDepartment.Id.ToString());
        //        }

        //    }
        //    return teamIdlist;
        //}

        public IList<BaseDataDepartment> SelectBaseDataDepartmentIdByParentId(string parentId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<BaseDataDepartment>("SelectBaseDataDepartmentIdByParentId", parentId);

            }
            catch (Exception ex)
            {
                m_Logger.Error("BugInquiryBiz/SelectBaseDataDepartmentIdByParentId:" + ex.ToString());
                return null;
            }
        }

    }
}
