using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;
using Qisda.PMS.Business;

namespace Qisda.PMS.Business
{
    public class BasicInformationDetailBiz : BaseBusiness
    {
        /// <summary>
        /// 返回两个时间之差
        /// </summary>
        /// <param name="dueDate">结束时间</param>
        /// <param name="actualStartDate">实际开始时间</param>
        /// <param name="planStartDate">计划开始时间</param>
        /// <returns>返回实际结束时间与实际开始时间的差值（如果实际时间不存在，则返回实际结束时间与计划结束时间差（单位为天数））</returns>
        public int GetDuration(DateTime dueDate, DateTime actualStartDate, DateTime planStartDate)
        {
            int duration;
            TimeSpan result;
            //DateTime result=new DateTime();
            string due = dueDate.ToString("yyyy-MM-dd").Trim();
            string actualStart = actualStartDate.ToString("yyyy-MM-dd").Trim();

            if (!due.Equals("1900-01-01") && !due.Equals("0001-01-01")
                    && !due.Equals("01-01") && !due.Equals("0001-1-1")
                    && !due.Equals("0000-00-00"))
            {
                if (!actualStart.Equals("1900-01-01") && !actualStart.Equals("0001-01-01")
                    && !actualStart.Equals("01-01") && !actualStart.Equals("0001-1-1")
                    && !actualStart.Equals("0000-00-00"))
                {
                    result = dueDate - actualStartDate;
                }
                else
                {
                    result = dueDate - planStartDate;
                }

                duration = result.Days + 1;//加上一天，因为取间隔的时候会排除第一天，所以要在加上

            }
            else
            {
                // result = DateTime.Now - DateTime.Now;
                duration = 0;//这个地方不加是因为没有分配时间，所以是0天。
            }

            return duration;
        }

        public bool UpdateStages(string pmsId, string loginName, int oldStage, int newStage, string strAction)
        {
            DateTime dtCurDate = PmsSysBiz.GetDBDateTime();
            bool blResult = false;

            try
            {
                #region Update Stage
                PmsHead pmsHead = new PmsHead();
                pmsHead.PmsId = pmsId;
                pmsHead.Stage = newStage;
                pmsHead.MaintainDate = dtCurDate;
                pmsHead.MaintainUser = loginName;
                pmsHead.AbnormalStage = oldStage;
                #endregion

                #region Insert PMSChangeHistory
                PmsChangeHistory pmsChangeHistory = new PmsChangeHistory();
                pmsChangeHistory.PmsId = pmsId;
                pmsChangeHistory.ChangeContent = "Stage,AbnormalStage";
                pmsChangeHistory.Action = strAction;
                pmsChangeHistory.CreateDate = dtCurDate;
                pmsChangeHistory.Creator = loginName;
                #endregion

                PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                blResult = pmsHeadBiz.UpdateStage(pmsHead, pmsChangeHistory);
            }
            catch
            {
                blResult = false;
            }

            return blResult;
        }

        #region Get Stage
        public bool GetStageBeforeReactiveAndUpdateStages(string pmsId, string loginName, string strAction, out int newStage)
        {
            newStage = 0;
            try
            {
                int oldStage = 0;
                PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                IList<PmsHead> pmsHeadList = pmsHeadBiz.SelectPmsHead(pmsId, null);

                if (pmsHeadList != null && pmsHeadList.Count > 0)
                {
                    oldStage = int.Parse(pmsHeadList[0].Stage.ToString().Trim());
                    newStage = int.Parse(pmsHeadList[0].AbnormalStage.ToString().Trim());
                    bool blResult = UpdateStages(pmsId, loginName, oldStage, newStage, strAction);
                    return blResult;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion

        public bool UpdatePmsHeadForOK(PmsHead pmsHead)
        {
            try
            {
                m_PMSSqlConnection.Update("UpdatePmsHeadForOK", pmsHead);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("BasicInformationDetailBiz/UpdatePmsHeadForOK:" + ex.ToString());
                return false;
            }
        }

        public bool UpdatePmsFlow(PmsFlow pmsFlow)
        {
            try
            {
                m_PMSSqlConnection.Update("UpdatePmsFlow", pmsFlow);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("BasicInformationDetailBiz/pmsFlow:" + ex.ToString());
                return false;
            }
        }

        public bool Save(PmsHead pmsHead, string oldCrId, string newCrId, string loginName, string oldType, string newType)
        {
            try
            {
                m_PMSSqlConnection.BeginTransaction();

                // add by Ename Wang on 20120321
                #region 更新pms_flow表
                PmsFlow pmsFlow = new PmsFlow();
                PmsFlowTemplateBiz pmsFlowTemplateBiz = new PmsFlowTemplateBiz();
                IList<PmsFlowTemplate> pmsFlowTemplateList = pmsFlowTemplateBiz.SelectPmsFlowTemplateByTypeId(newType);
                pmsFlow.FlowId = pmsFlowTemplateList[0].FlowId;
                pmsFlow.PmsId = pmsHead.PmsId;
                pmsFlow.Creator = loginName;
                pmsFlow.CreateDate = System.DateTime.Now;
                if (!UpdatePmsFlow(pmsFlow))
                {
                    m_PMSSqlConnection.RollBackTransaction();
                    return false;
                }
                #endregion
                // end add

                // add by Ename Wang on 20120531 fix change CR to Small CR stage Bug
                IList<int> StageIds = new List<int>();
                bool IsExist = true;
                foreach (PmsFlowTemplate pmsFlowTemplate in pmsFlowTemplateList)
                {
                    StageIds.Add(pmsFlowTemplate.Stageid);
                }
                if (StageIds != null)
                {
                    IsExist = StageIds.Contains(pmsHead.Stage);
                }
                if (IsExist == false)
                {
                    switch (pmsHead.Stage)
                    {
                        case (int)PmsCommonEnum.ProjectStage.PIS_STP:
                            pmsHead.Stage = (int)PmsCommonEnum.ProjectStage.Develop_Test;
                            break;
                        default:
                            break;
                    }

                }
                // end add

                if (!UpdatePmsHeadForOK(pmsHead))
                {
                    m_PMSSqlConnection.RollBackTransaction();
                    return false;
                }

                if (newType != oldType)
                {
                    if (!new SdpDetailBiz().InsertSdpDetailByTemplateOnTypeChange(pmsHead.PmsId, oldType, newType))
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return false;
                    }
                }

                if (oldCrId != newCrId)
                {
                    //更新itarm_cr_list(删除旧的CRID，更新新的CRID)
                    if (!DeleteItarmCrListAndCo(oldCrId))
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return false;
                    }

                    PmsItarmMappingBiz pmsItarmMappingBiz = new PmsItarmMappingBiz();
                    IList<PmsItarmMapping> pmsItarmMappingChange = pmsItarmMappingBiz.SelectPmsItarmMapping(newCrId, null);
                    string changeContent = pmsItarmMappingChange.Aggregate("The following data is deleted:", (current, m) => current + m.CrId + ":" + m.PmsId + ".");

                    // 已经存在SD的CR不能删除
                    if (pmsItarmMappingChange != null)
                    {
                        if (pmsItarmMappingChange.FirstOrDefault() != null)
                        {
                            string pmsId = pmsItarmMappingChange.FirstOrDefault().PmsId;
                            PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                            IList<PmsHead> ListPmsHead = pmsHeadBiz.SelectPmsHeadByPmsId(pmsId);
                            if (ListPmsHead != null)
                            {
                                if (ListPmsHead.FirstOrDefault() != null)
                                {
                                    if (ListPmsHead.FirstOrDefault().Sd == string.Empty)
                                    {
                                        if (!pmsItarmMappingBiz.DeletePmsItarmMappingCrId(newCrId))
                                        {
                                            m_PMSSqlConnection.RollBackTransaction();
                                            return false;
                                        }

                                    }
                                }
                            }

                        }
                    }

                    if (!pmsItarmMappingBiz.UpdatePmsItarmMappingCrId(oldCrId, newCrId))
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return false;
                    }

                    //New CR 是ITARM创建的,它的SDP应及时删掉,防止和老CR的SDP重复
                    if (!new SdpDetailBiz().DeleteSDPForCRNoChange(newCrId))
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return false;
                    }

                    //更新bugfree数据库中的表。账号权限不足，执行报错
                    //if (!UpdateBugfree(oldCrId, newCrId))
                    //{
                    //    m_PMSSqlConnection.RollBackTransaction();
                    //    return false;
                    //}

                    PmsChangeHistory pmsChangeHistory = new PmsChangeHistory();
                    pmsChangeHistory.PmsId = pmsHead.PmsId;
                    pmsChangeHistory.Action = "Delete and Update CrId";
                    pmsChangeHistory.ChangeContent = changeContent + " CrId is changed from '" + oldCrId + "' to '" + newCrId + "'";
                    pmsChangeHistory.Creator = loginName;
                    pmsChangeHistory.CreateDate = PmsSysBiz.GetDBDateTime();
                    new PmsChangeHistoryBiz().InsertPmsChangeHistory(pmsChangeHistory);

                }

                //更新pms_system_version 
                #region 更新系统版本表
                //TODO: oldType,newType为Service的情况。

                if (oldType != PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                {

                    bool upVerResult =
                    new PmsCRCreatBiz().UpdateSysVersion(pmsHead.Domain, pmsHead.System, pmsHead.Site, pmsHead.NewVersion);

                    if (!upVerResult)
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return false;
                    }
                }
                #endregion



                m_PMSSqlConnection.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                m_PMSSqlConnection.RollBackTransaction();
                m_Logger.Error("BasicInformationDetailBiz/Save" + ex.Message.ToString());
                return false;
            }
        }

        public void GetReleaseXml(string pmsId, string sdpUrl, out string releaseXml)
        {

            //<CR_ID/>                --HeadSerial                             H
            //<APPLY_SITE/>           --多个site，用单引号分?                  H 
            //<PROJECT_NAME/>         --System Name                            H
            //<CREATOR/>              --�有QA传QA,没QA传SD,没SD传SE            D
            //<PROJECT_TYPE/>         --CR/Project/Bug/Services/Study        H
            //<DUE_DATE/>             --YYYYMMDD                               H
            //<PROJECT_STATUS/>       --正常/提前/Delay
            //<PM/>                   --多个人员以单引号分隔,例如: Jack.Huang,Coase.Tseng,Derek.Chang     H
            //<SD/>                   --同上                                   D
            //<SE/>                   --同上                                   D
            //<QA/>                   --同上                                   D
            //<SDP_FILE_NAME/>        --?
            //<SDP_URL/>
            //<CONTACT/>              --�传人名，例如：sammi.yao   （CREATOR  + 分机？）  H


            string roleQA = "";
            string roleSD = "";
            string roleSE = "";
            string contactQA = "";
            string contactSD = "";
            string contactSE = "";
            // string sdp_url = string.Empty;
            StringBuilder xml = new StringBuilder();
            PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();


            PmsHead pmsSdpHead = new PmsHead();
            pmsSdpHead.PmsId = pmsId;
            pmsSdpHead.Vid = "PM";
            IList<PmsHead> pmsSdpHeadList = pmsHeadBiz.SelectPmsHeadOther(pmsSdpHead);

            xml.Append("<RLNS>");
            if (pmsSdpHeadList != null && pmsSdpHeadList.Count > 0)
            {
                string project_status = "";
                string strNowDate = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string due_date = pmsSdpHeadList[0].DueDate.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                if (pmsSdpHeadList[0].DueDate == null)
                {
                    project_status = string.Empty;
                }
                else
                {
                    if (Convert.ToInt32(due_date) > Convert.ToInt32(strNowDate))
                    {
                        project_status = "提前";
                    }
                    else if (Convert.ToInt32(due_date) == Convert.ToInt32(strNowDate))
                    {
                        project_status = "正常";
                    }
                    else
                    {
                        project_status = "Delay";
                    }
                }
                //end added

                xml.Append("<CR_ID>").Append(pmsSdpHeadList[0].CrId.Trim()).Append("</CR_ID>");
                xml.Append("<APPLY_SITE>").Append(pmsSdpHeadList[0].Site.Trim()).Append("</APPLY_SITE>");
                xml.Append("<CR_NAME>").Append(pmsSdpHeadList[0].PmsName.Trim()).Append("</CR_NAME>");
                xml.Append("<TYPE>").Append(pmsSdpHeadList[0].Type.Trim()).Append("</TYPE>");
                xml.Append("<DUE_DATE>").Append((pmsSdpHeadList[0].DueDate == null) ? string.Empty : pmsSdpHeadList[0].DueDate.ToString("yyyyMMdd").Trim()).Append("</DUE_DATE>");

                xml.Append("<STATUS>").Append(project_status).Append("</STATUS>");

                xml.Append("<IMPACT>").Append((pmsSdpHeadList[0].Site == null) ? string.Empty : pmsSdpHeadList[0].Site.Trim()).Append("</IMPACT>");
                xml.Append("<PM>").Append(pmsSdpHeadList[0].Pm.Trim()).Append("</PM>");
                xml.Append("<SDP_FILE_NAME>SDP</SDP_FILE_NAME>");
                string systemName = pmsSdpHeadList[0].System.Trim();
                systemName = systemName.Contains("(") ? systemName.Substring(0, systemName.IndexOf("(")) : systemName;
                xml.Append("<SYSTEM>").Append(systemName).Append("</SYSTEM>");
                xml.Append("<OLD_VERSION>").Append((pmsSdpHeadList[0].OldVersion == null) ? string.Empty : pmsSdpHeadList[0].OldVersion.Trim()).Append("</OLD_VERSION>");
                xml.Append("<NEW_VERSION>").Append((pmsSdpHeadList[0].NewVersion == null) ? string.Empty : pmsSdpHeadList[0].NewVersion.Trim()).Append("</NEW_VERSION>");
                xml.Append("<SDP_URL>").Append(sdpUrl).Append("</SDP_URL>");


                #region 获取文档的相关信息
                PmsDocuments pmsDocuments = new PmsDocuments();
                pmsDocuments.PmsId = pmsId;
                IList<PmsDocuments> pmsDocumentsList = new PmsDocumentsBiz().SelectPmsDocuments(pmsDocuments);
                string pesFileName = string.Empty;
                string pisFileName = string.Empty;
                string stpFileName = string.Empty;
                string stcFileName = string.Empty;
                string rlnFileName = string.Empty;
                string studyReportFileName = string.Empty;
                string otherFileName = string.Empty;
                string pesMinFileName = string.Empty;
                string pisMinFileName = string.Empty;
                string stpMinFileName = string.Empty;

                string pesUrl = string.Empty;
                string pisUrl = string.Empty;
                string stpUrl = string.Empty;
                string stcUrl = string.Empty;
                string rlnUrl = string.Empty;
                string studyReportUrl = string.Empty;
                string otherUrl = string.Empty;
                string pesMinUrl = string.Empty;
                string pisMinUrl = string.Empty;
                string stpMinUrl = string.Empty;

                if (pmsDocumentsList != null && pmsDocumentsList.Count > 0)
                {
                    foreach (PmsDocuments d in pmsDocumentsList)
                    {
                        switch (d.DocTypeId)
                        {
                            case (int)PmsCommonEnum.DocumentType.PES:
                                pesFileName = d.FileName;
                                pesUrl = d.Path;
                                break;
                            case (int)PmsCommonEnum.DocumentType.PIS:
                                pisFileName = d.FileName;
                                pisUrl = d.Path;
                                break;
                            case (int)PmsCommonEnum.DocumentType.STP:
                                stpFileName = d.FileName;
                                stpUrl = d.Path;
                                break;

                            case (int)PmsCommonEnum.DocumentType.STC:
                                stcFileName = d.FileName;
                                stcUrl = d.Path;
                                break;
                            case (int)PmsCommonEnum.DocumentType.RLN:
                                rlnFileName = d.FileName;
                                rlnUrl = d.Path;
                                break;
                            case (int)PmsCommonEnum.DocumentType.Study_Report:
                                studyReportFileName = d.FileName;
                                studyReportUrl = d.Path;
                                break;
                            case (int)PmsCommonEnum.DocumentType.Other:
                                otherFileName = d.FileName;
                                otherUrl = d.Path;
                                break;
                            case (int)PmsCommonEnum.DocumentType.PES_MIN:
                                pesMinFileName = d.FileName;
                                pesMinUrl = d.Path;
                                break;
                            case (int)PmsCommonEnum.DocumentType.PIS_MIN:
                                pisMinFileName = d.FileName;
                                pisMinUrl = d.Path;
                                break;
                            case (int)PmsCommonEnum.DocumentType.STP_MIN:
                                stpMinFileName = d.FileName;
                                stpMinUrl = d.Path;
                                break;
                            default:
                                break;
                        }
                    }
                }

                xml.Append("<PES_FILE_NAME>").Append(pesFileName).Append("</PES_FILE_NAME>");
                xml.Append("<PES_URL>").Append(pesUrl).Append("</PES_URL>");

                xml.Append("<PIS_FILE_NAME>").Append(pisFileName).Append("</PIS_FILE_NAME>");
                xml.Append("<PIS_URL>").Append(pisUrl).Append("</PIS_URL>");

                xml.Append("<STP_FILE_NAME>").Append(stpFileName).Append("</STP_FILE_NAME>");
                xml.Append("<STP_URL>").Append(stpUrl).Append("</STP_URL>");

                xml.Append("<STC_FILE_NAME>").Append(stcFileName).Append("</STC_FILE_NAME>");
                xml.Append("<STC_URL>").Append(stcUrl).Append("</STC_URL>");

                xml.Append("<RLN_FILE_NAME>").Append(rlnFileName).Append("</RLN_FILE_NAME>");
                xml.Append("<RLN_URL>").Append(rlnUrl).Append("</RLN_URL>");

                xml.Append("<StudyReport_FILE_NAME>").Append(studyReportFileName).Append("</StudyReport_FILE_NAME>");
                xml.Append("<StudyReport_URL>").Append(studyReportUrl).Append("</StudyReport_URL>");

                xml.Append("<Other_FILE_NAME>").Append(otherFileName).Append("</Other_FILE_NAME>");
                xml.Append("<Other_URL>").Append(otherUrl).Append("</Other_URL>");

                xml.Append("<PES_MIN_FILE_NAME>").Append(pesMinFileName).Append("</PES_MIN_FILE_NAME>");
                xml.Append("<PES_MIN_URL>").Append(pesMinUrl).Append("</PES_MIN_URL>");

                xml.Append("<PIS_MIN_FILE_NAME>").Append(pisMinFileName).Append("</PIS_MIN_FILE_NAME>");
                xml.Append("<PIS_MIN_URL>").Append(pisMinUrl).Append("</PIS_MIN_URL>");

                xml.Append("<STP_MIN_FILE_NAME>").Append(stpMinFileName).Append("</STP_MIN_FILE_NAME>");
                xml.Append("<STP_MIN_URL>").Append(stpMinUrl).Append("</STP_MIN_URL>");

                #endregion


                BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
                //  BaseDataUser baseDataUser = new BaseDataUser();
                if (!string.IsNullOrEmpty(pmsSdpHeadList[0].Qa))
                {
                    roleQA = string.Concat(roleQA, ",", pmsSdpHeadList[0].Qa);
                    string[] qas = roleQA.Split(';');
                    foreach (string str in qas)
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            string ext = baseDataUserBiz.SelectBaseDataUser(str.Trim(), null)[0].Extention;
                            contactQA = string.Concat(contactQA, ",", str, " ", ext);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(pmsSdpHeadList[0].Sd))
                {
                    roleSD = string.Concat(roleSD, ",", pmsSdpHeadList[0].Sd);
                    string[] sds = roleSD.Split(';');
                    foreach (string str in sds)
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            string ext = baseDataUserBiz.SelectBaseDataUser(str.Trim(), null)[0].Extention;
                            contactSD = string.Concat(contactSD, ",", str, " ", ext);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(pmsSdpHeadList[0].Se))
                {
                    roleSE = string.Concat(roleSE, ",", pmsSdpHeadList[0].Se);
                    string[] ses = roleSE.Split(';');
                    foreach (string str in ses)
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            string ext = baseDataUserBiz.SelectBaseDataUser(str.Trim(), null)[0].Extention;
                            contactSE = string.Concat(contactSE, ",", str, " ", ext);
                        }
                    }
                }

                xml.Append("<SD>").Append((roleSD != "") ? roleSD.Substring(1) : string.Empty).Append("</SD>");
                xml.Append("<SE>").Append((roleSE != "") ? roleSE.Substring(1) : string.Empty).Append("</SE>");
                xml.Append("<QA>").Append((roleQA != "") ? roleQA.Substring(1) : string.Empty).Append("</QA>");
                if (roleSD != "" || roleSE != "" || roleQA != "")
                {
                    xml.Append("<CREATOR>").Append((roleQA.Length > 1) ? roleQA.Substring(1) : (roleSD.Length > 1) ? roleSD.Substring(1) : roleSE.Substring(1)).Append("</CREATOR>");
                }
                else
                    xml.Append("<CREATOR>").Append(string.Empty).Append("</CREATOR>");
            }
            xml.Append("</RLNS>");
            m_Logger.Error(xml);
            releaseXml = xml.ToString();
        }

        private bool DeleteItarmCrListAndCo(string oldCrId)
        {
            try
            {
                m_PMSSqlConnection.Delete("DeleteItarmCrList", oldCrId);
                m_PMSSqlConnection.Delete("DeleteItarmCrListCo", oldCrId);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("BasicInformationDetailBiz/UpdateItarmCrListPart、DeleteItarmCrList" + ex.Message.ToString());
                return false;
            }
        }

        public bool CheckCrIdIsExist(string oldCrId, string newCrId)
        {
            try
            {
                IList<string> result = m_PMSSqlConnection.QueryForList<string>("SelectCrIdItarmcrlistItarmcrlistco", newCrId);
                if (result.Count == 0)
                {
                    ItarmCrListBiz itarmCrListBiz = new ItarmCrListBiz();
                    if (itarmCrListBiz.InsertFromItarmRequirement(oldCrId, newCrId) > 0)
                    {
                        return true;
                    }
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("BasicInformationDetailBiz/SelectCrIdItarmcrlistItarmcrlistco" + ex.Message.ToString());
                return false;
            }
        }



        //更新bugfree数据库中的表
        private bool UpdateBugfree(string oldCrId, string newCrId)
        {
            try
            {
                m_BugFreeSqlConnection.BeginTransaction();
                Hashtable hashtable = new Hashtable();
                hashtable.Add("NewCrId", newCrId);
                hashtable.Add("OldCrId", oldCrId);

                m_BugFreeSqlConnection.Update("UpdateBfResultinfoBugMachine", hashtable);

                m_BugFreeSqlConnection.Update("UpdateBfIssueinfoIssueMachine", hashtable);

                m_BugFreeSqlConnection.Update("UpdateBfResultinfoResultMachine", hashtable);

                m_BugFreeSqlConnection.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                m_BugFreeSqlConnection.RollBackTransaction();
                m_Logger.Error("BasicInformationDetailBiz/UpdateBugfree:" + ex.ToString());
                return false;
            }


        }

    }
}
