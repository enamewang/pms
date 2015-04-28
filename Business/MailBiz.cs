using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using Qisda.PMS.Entity;
using System.Collections;
using System.Data;
using Qisda.PMS.Common;

namespace Qisda.PMS.Business
{
    public class MailBiz : BaseBusiness
    {
        PmsCommonBiz m_PmsCommonBiz = new PmsCommonBiz();

        Hashtable m_hasMailList = new Hashtable();

        #region get mail template and subject

        private Hashtable GetSubjectAndMailTemplate(PmsHead pmsHead, PmsCommonEnum.MailType mailType)
        {
            string mailBodyTemplate = string.Empty;
            string mailSubject = string.Empty;
            string mailBodyTemplateFullPath = string.Empty;
            string linkAddress = string.Empty;
            Hashtable subjectAndMailTemplate = new Hashtable();
            switch (mailType)
            {
                case PmsCommonEnum.MailType.CreateMail:
                    if (pmsHead.Type == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                    {
                        mailBodyTemplate = ConfigurationSettings.AppSettings["CreateServiceMailBodyTemplate"].ToString();
                        mailSubject = ConfigurationSettings.AppSettings["CreateMailSubject"].ToString(); break;
                    }
                    else
                    {
                        mailBodyTemplate = ConfigurationSettings.AppSettings["CreateMailBodyTemplate"].ToString();
                        mailSubject = ConfigurationSettings.AppSettings["CreateMailSubject"].ToString(); break;
                    }


                case PmsCommonEnum.MailType.AssignMemberMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["AssignMemberMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["AssignMemberMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.PISAndSTPMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["PISAndSTPMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["PISAndSTPMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.DevelopAndTestMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["DevelopAndTestMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["DevelopAndTestMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.ReleaseMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["ReleaseMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["ReleaseMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.ClosedMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["ClosedMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["ClosedMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.HardClosedMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["HardClosedMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["HardClosedMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.PendingMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["PendingMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["PendingMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.CancelledMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["CancelledMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["CancelledMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.ReactiveMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["ReactiveMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["ReactiveMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.ConfirmMail:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["ConfirmSdpTaskMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["ConfirmMailSubject"].ToString(); break;
                case PmsCommonEnum.MailType.WaitingClosed:
                    mailBodyTemplate = ConfigurationSettings.AppSettings["WaitingClosedMailBodyTemplate"].ToString();
                    mailSubject = ConfigurationSettings.AppSettings["WaitingClosedSubject"].ToString(); break;
                default: break;

            }
            try
            {
                mailBodyTemplateFullPath = m_PmsCommonBiz.GetHtmlContent(HttpRuntime.AppDomainAppPath + mailBodyTemplate);
            }
            catch (Exception ex)
            {
                m_Logger.Error("MailBiz/GetSubjectAndMailTemplate", ex);
                mailBodyTemplate = m_PmsCommonBiz.GetHtmlContent(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + mailBodyTemplate);
            }
            linkAddress = ConfigurationSettings.AppSettings["PMSUrl"].ToString();
            subjectAndMailTemplate.Add(mailSubject, mailBodyTemplateFullPath);
            return subjectAndMailTemplate;
        }
        #endregion

        #region add users MailAddress and partialMailBody to m_hasMailList

        /// <summary>
        /// Users means RDs or Managers 
        /// </summary>
        /// <param name="mailToUser"></param>
        /// <param name="partialMailBody"></param>
        private void AddInfoToMailList(IList<BaseDataUser> mailToUser, string partialMailBody)
        {
            if (mailToUser.Count > 0)
            {
                foreach (BaseDataUser User in mailToUser)
                {
                    if (!m_hasMailList.ContainsKey(User.MailAddress))
                    {
                        m_hasMailList.Add(User.MailAddress, partialMailBody);
                    }

                }
            }
        }

        #endregion

        #region get Mail to managers and RDs
        private IList<BaseDataUser> GetCreateMailManagers(PmsHead pmsHead)
        {
            BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
            //m_User = new BaseDataUser();
            //m_User.Role = "RD Leader";
            IList<BaseDataUser> rdLeaderList = baseDataUserBiz.SelectBaseDataUser(null, PmsCommonEnum.OrgRole.RD_LEADER.GetDescription());
            //m_User.Role = "RD Manager";
            IList<BaseDataUser> rdManagerList = baseDataUserBiz.SelectBaseDataUser(null, PmsCommonEnum.OrgRole.RD_MANAGER.GetDescription());
            IList<BaseDataUser> rdLeaderAndManagerList = rdLeaderList.Union<BaseDataUser>(rdManagerList).ToList();
            //m_User.Role = "PM";
            IList<BaseDataUser> pmList = null;
            string strPM = pmsHead.Pm.ToString().Trim();
            string[] arrayPM = strPM.Split(';');
            for (int i = 0; i < arrayPM.Length; i++)
            {
                pmList = baseDataUserBiz.SelectBaseDataUser(arrayPM[i], null);
            }
            IList<BaseDataUser> allPersonsList = rdLeaderAndManagerList.Union<BaseDataUser>(rdLeaderAndManagerList).ToList();
            if (pmList != null)
            {
                allPersonsList = allPersonsList.Union<BaseDataUser>(pmList).ToList();
            }

            return allPersonsList;
        }
        private IList<BaseDataUser> GetPromoteMailManagers(PmsHead pmsHead, int currentstage)
        {
            BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
            // m_User = new BaseDataUser();
            if (currentstage == (int)PmsCommonEnum.ProjectStage.Closed)
            {
                //m_User.Role = "RD Leader";
                IList<BaseDataUser> rdLeaderList = baseDataUserBiz.SelectBaseDataUser(null, PmsCommonEnum.OrgRole.RD_LEADER.GetDescription());
                //m_User.Role = "RD Manager";
                IList<BaseDataUser> rdManagerList = baseDataUserBiz.SelectBaseDataUser(null, PmsCommonEnum.OrgRole.RD_MANAGER.GetDescription());
                //m_User.Role = "PM Leader";
                IList<BaseDataUser> pmLeaderList = baseDataUserBiz.SelectBaseDataUser(null, PmsCommonEnum.OrgRole.PM_MANAGER.GetDescription());

                IList<BaseDataUser> allPersonsList = rdLeaderList.Union<BaseDataUser>(rdManagerList).ToList();
                allPersonsList = allPersonsList.Concat<BaseDataUser>(pmLeaderList).ToList();
                return allPersonsList;
            }
            else
            {
                //m_User.Role = "RD Leader";
                IList<BaseDataUser> rdLeaderList = baseDataUserBiz.SelectBaseDataUser(null, PmsCommonEnum.OrgRole.RD_LEADER.GetDescription());
                return rdLeaderList;

            }

        }
        private IList<BaseDataUser> GetRDs(PmsHead pmsHead)
        {

            IList<BaseDataUser> baseDataUserList = new List<BaseDataUser>();
            BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();

            // TODO: Check Pm SD QA exsit in baseDataUser or not

            if (!string.IsNullOrEmpty(pmsHead.Pm))
            {
                string[] pms = pmsHead.Pm.Trim().Split(';');
                foreach (string pm in pms)
                {
                    if (!string.IsNullOrEmpty(pm))
                    {
                        // m_User.LoginName = pm;
                        IList<BaseDataUser> pmList = baseDataUserBiz.SelectBaseDataUser(pm, null);
                        baseDataUserList.Add(pmList[0]);
                    }
                }
            }

            if (!string.IsNullOrEmpty(pmsHead.Sd))
            {
                string[] sds = pmsHead.Sd.Trim().Split(';');
                foreach (string sd in sds)
                {
                    if (!string.IsNullOrEmpty(sd))
                    {
                        // m_User.LoginName = sd;
                        IList<BaseDataUser> sdList = baseDataUserBiz.SelectBaseDataUser(sd, null);
                        baseDataUserList.Add(sdList[0]);
                    }
                }
            }

            if (!string.IsNullOrEmpty(pmsHead.Se))
            {
                string[] ses = pmsHead.Se.Trim().Split(';');
                foreach (string se in ses)
                {
                    if (!string.IsNullOrEmpty(se))
                    {
                        //m_User.LoginName = se;
                        IList<BaseDataUser> seList = baseDataUserBiz.SelectBaseDataUser(se, null);
                        baseDataUserList.Add(seList[0]);
                    }
                }
            }

            if (!string.IsNullOrEmpty(pmsHead.Qa))
            {
                string[] qas = pmsHead.Qa.Trim().Split(';');
                foreach (string qa in qas)
                {
                    if (!string.IsNullOrEmpty(qa))
                    {
                        //m_User.LoginName = qa;
                        IList<BaseDataUser> qaList = baseDataUserBiz.SelectBaseDataUser(qa, null);
                        baseDataUserList.Add(qaList[0]);
                    }
                }
            }

            return baseDataUserList;
        }
        #endregion

        #region get StringMailBody
        private string GetCreateStringMailBody(PmsHead pmsHead)
        {
            string pmsID = pmsHead.PmsId.ToString();
            string CrNo = string.Empty;
            PmsItarmMappingBiz pmsItarmMappingSelect = new PmsItarmMappingBiz();

            IList<PmsItarmMapping> pmsItarmMappingresult = pmsItarmMappingSelect.SelectPmsItarmMappingOther(pmsID);
            if (pmsItarmMappingresult != null && pmsItarmMappingresult.Count > 0)
            {
                CrNo = pmsItarmMappingresult[0].CrId;
            }

            string dueDate = string.Empty;
            dueDate = getStringDueDate(pmsHead);
            string planStartDate = string.Empty;
            planStartDate = getStringPlanStartDate(pmsHead);
            PmsCommonEnum.ProjectStage projectStage = (PmsCommonEnum.ProjectStage)System.Enum.Parse(typeof(PmsCommonEnum.ProjectStage), pmsHead.Stage.ToString());
            string stageName = projectStage.GetDescription();

            string strMailBody = string.Empty;

            if (pmsHead.Type == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
            {
                strMailBody = "<TR>" + "<TD>" + CrNo + "</TD>" + "<TD>" + pmsHead.PmsName + "</TD>"
                        + "<TD>" + pmsHead.Pm + "</TD>" + "<TD>" + pmsHead.Priority + "</TD>"
                        + "<TD>" + pmsHead.Domain + "</TD>" + "<TD>" + stageName + "</TD>" + "<TD>" + pmsHead.System + "</TD>"
                        + "</TR>";
            }
            else
            {
                strMailBody = "<TR>" + "<TD>" + CrNo + "</TD>" + "<TD>" + pmsHead.PmsName + "</TD>"
                        + "<TD>" + planStartDate + "</TD>" + "<TD>" + dueDate + "</TD>"
                        + "<TD>" + pmsHead.Pm + "</TD>" + "<TD>" + pmsHead.Priority + "</TD>"
                        + "<TD>" + pmsHead.Domain + "</TD>" + "<TD>" + stageName + "</TD>" + "<TD>" + pmsHead.System + "</TD>"
                        + "</TR>";
            }

            return strMailBody;

        }


        private string GetPromoteStringMailBody(PmsHead pmsHead, int currentStage)
        {
            string pmsID = pmsHead.PmsId.ToString();
            string CrNo = string.Empty;
            PmsItarmMappingBiz pmsItarmMappingSelect = new PmsItarmMappingBiz();
            IList<PmsItarmMapping> pmsItarmMappingresult = pmsItarmMappingSelect.SelectPmsItarmMappingOther(pmsID);
            if (pmsItarmMappingresult != null && pmsItarmMappingresult.Count > 0)
            {
                CrNo = pmsItarmMappingresult[0].CrId;
            }

            string strMailBody = string.Empty;
            PmsCommonEnum.ProjectStage stage = (PmsCommonEnum.ProjectStage)currentStage;
            string stageName = stage.GetDescription();
            string dueDate = string.Empty;
            dueDate = getStringDueDate(pmsHead);
            string planStartDate = string.Empty;
            planStartDate = getStringPlanStartDate(pmsHead);
            string progress = "0%";
            string totalManPower = "0";

            PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
            progress = string.IsNullOrEmpty(pmsHeadBiz.SelectProcessByPmsID(pmsHead.PmsId)) ? "0%" : pmsHeadBiz.SelectProcessByPmsID(pmsHead.PmsId) + "%";
            totalManPower = string.IsNullOrEmpty(pmsHeadBiz.SelectManPowerByPmsID(pmsHead.PmsId)) ? "0" : pmsHeadBiz.SelectManPowerByPmsID(pmsHead.PmsId);
            switch (currentStage)
            {
                case (int)PmsCommonEnum.ProjectStage.AssignMember:
                    strMailBody = "<TR>" + "<TD>" + CrNo + "</TD>" + "<TD>" + pmsHead.PmsName + "</TD>"
                                + "<TD>" + dueDate + "</TD>" + "<TD>" + planStartDate + "</TD>"
                                + "<TD>" + pmsHead.Pm + "</TD>" + "<TD>" + pmsHead.Priority + "</TD>" + "<TD>" + pmsHead.Description + "</TD>"
                                + "<TD>" + pmsHead.ImpactSite + "</TD>" + "<TD>" + pmsHead.Domain + "</TD>" + "<TD>" + pmsHead.System + "</TD>"
                                + "</TR>";
                    return strMailBody;

                case (int)PmsCommonEnum.ProjectStage.PIS_STP:
                    strMailBody = "<TR>" + "<TD>" + CrNo + "</TD>" + "<TD>" + pmsHead.PmsName + "</TD>"
                                + "<TD>" + dueDate + "</TD>" + "<TD>" + planStartDate + "</TD>"
                                + "<TD>" + pmsHead.Pm + "</TD>" + "<TD>" + pmsHead.Priority + "</TD>"
                                + "<TD>" + pmsHead.Sd + "</TD>" + "<TD>" + pmsHead.Se + "</TD>" + "<TD>" + pmsHead.Qa + "</TD>"
                                + "<TD>" + pmsHead.ImpactSite + "</TD>" + "<TD>" + pmsHead.Domain + "</TD>" + "<TD>" + pmsHead.System + "</TD>"
                                + "</TR>";
                    return strMailBody;
                case (int)PmsCommonEnum.ProjectStage.Closed:
                    string duration = string.Empty;
                    //duration = getStringDuration(pmsHead);
                    SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                    duration = sdpDetailBiz.GetDuration(pmsHead.PmsId);
                    strMailBody = "<TR>" + "<TD>" + CrNo + "</TD>" + "<TD>" + pmsHead.PmsName + "</TD>"
                                + "<TD>" + duration + "</TD>" + "<TD>" + pmsHead.Pm + "</TD>"
                                + "<TD>" + pmsHead.Priority + "</TD>" + "<TD>" + pmsHead.Sd + "</TD>" + "<TD>" + pmsHead.Se + "</TD>"
                                + "<TD>" + pmsHead.Qa + "</TD>" + "<TD>" + stageName + "</TD>" + "<TD>" + totalManPower + "</TD>"
                                + "<TD>" + pmsHead.ImpactSite + "</TD>" + "<TD>" + pmsHead.Domain + "</TD>" + "<TD>" + pmsHead.System + "</TD>"
                                + "</TR>";
                    return strMailBody;
                case (int)PmsCommonEnum.ProjectStage.Reactive:
                    //根据pmsID到数据库取数据
                    PmsHead head = new PmsHeadBiz().SelectPmsHeadByPmsId(pmsID)[0];
                    PmsCommonEnum.ProjectStage reactiveToStage = (PmsCommonEnum.ProjectStage)head.Stage;
                    string reactiveToStageName = reactiveToStage.GetDescription();
                    string beforeReactiveProgress = progress;
                    strMailBody = "<TR>" + "<TD>" + CrNo + "</TD>" + "<TD>" + head.PmsName + "</TD>"
                               + "<TD>" + getStringDueDate(head) + "</TD>" + "<TD>" + head.Pm + "</TD>"
                               + "<TD>" + head.Priority + "</TD>" + "<TD>" + head.Sd + "</TD>" + "<TD>" + head.Se + "</TD>"
                               + "<TD>" + head.Qa + "</TD>" + "<TD>" + reactiveToStageName + "</TD>" + "<TD>" + beforeReactiveProgress + "</TD>"
                               + "<TD>" + head.ImpactSite + "</TD>" + "<TD>" + head.Domain + "</TD>" + "<TD>" + head.System + "</TD>"
                               + "</TR>";
                    return strMailBody;
                //ConfirmMail is special,stageName can not be got from currentStage
                //
                case (int)PmsCommonEnum.MailType.ConfirmMail:

                    string stageString = string.Empty;

                    if (pmsHead.Stage != 0)
                    {
                        PmsCommonEnum.ProjectStage stageNameEnum = (PmsCommonEnum.ProjectStage)pmsHead.Stage;
                        stageString = stageNameEnum.GetDescription();
                    }

                    strMailBody = "<TR>" + "<TD style=' border: 1px solid black; border-right-style:none'>" + CrNo + "</TD>"
                                + "<TD style=' border: 1px solid black; border-right-style:none'>" + pmsHead.PmsName + "</TD>"
                                + "<TD style=' border: 1px solid black; border-right-style:none'>" + getStringDueDate(pmsHead) + "</TD>"
                                + "<TD style=' border: 1px solid black; border-right-style:none'>" + pmsHead.Pm + "</TD>"
                                + "<TD style=' border: 1px solid black; border-right-style:none'>" + pmsHead.Sd + "</TD>"
                                + "<TD style=' border: 1px solid black; border-right-style:none'>" + pmsHead.Priority + "</TD>"
                                + "<TD style=' border: 1px solid black; border-right-style:none'>" + pmsHead.Domain + "</TD>"
                                + "<TD style=' border: 1px solid black; border-right-style:none'>" + stageString + "</TD>"
                                + "<TD style=' border: 1px solid black; '>" + pmsHead.System + "</TD>"
                                + "</TR>";

                    return strMailBody;

                case (int)PmsCommonEnum.ProjectStage.WaitingClosed:
                    strMailBody = "<TR>" + "<TD>" + CrNo + "</TD>" + "<TD>" + pmsHead.PmsName + "</TD>"
                                + "<TD>" + pmsHead.Pm + "</TD>" + "<TD>" + pmsHead.Priority + "</TD>"
                                + "<TD>" + pmsHead.Sd + "</TD>" + "<TD>" + pmsHead.Se + "</TD>"
                                + "<TD>" + pmsHead.ImpactSite + "</TD>" + "<TD>" + pmsHead.Domain + "</TD>" + "<TD>" + pmsHead.System + "</TD>"
                                + "</TR>";
                    return strMailBody;


                default:
                    strMailBody = "<TR>" + "<TD>" + CrNo + "</TD>" + "<TD>" + pmsHead.PmsName + "</TD>"
                                + "<TD>" + dueDate + "</TD>" + "<TD>" + pmsHead.Pm + "</TD>"
                                + "<TD>" + pmsHead.Priority + "</TD>" + "<TD>" + pmsHead.Sd + "</TD>" + "<TD>" + pmsHead.Se + "</TD>"
                                + "<TD>" + pmsHead.Qa + "</TD>" + "<TD>" + stageName + "</TD>" + "<TD>" + progress + "</TD>"
                                + "<TD>" + pmsHead.ImpactSite + "</TD>" + "<TD>" + pmsHead.Domain + "</TD>" + "<TD>" + pmsHead.System + "</TD>"
                                + "</TR>";
                    return strMailBody;


            }
        }
        #endregion

        #region get duraion
        private string getStringDuration(PmsHead pmsHead)
        {
            string duraion = string.Empty;
            if (pmsHead.ActualStartDate == null)
            {
                return null;
            }
            else
            {
                string actualStartDate = pmsHead.ActualStartDate.ToString("yyyy");
                if (actualStartDate == "0000" || actualStartDate == "0001" || actualStartDate == "1900")
                {
                    return null;
                }
                else
                {
                    TimeSpan timeSpan = pmsHead.DueDate - pmsHead.ActualStartDate;
                    duraion = timeSpan.Days.ToString();
                    return duraion;
                }
            }

        }
        private string getStringDueDate(PmsHead pmsHead)
        {
            string dueDate = string.Empty;
            if (pmsHead.DueDate == null)
            {
                return null;
            }
            else
            {
                string dueYear = pmsHead.DueDate.ToString("yyyy");
                if (dueYear == "0000" || dueYear == "0001" || dueYear == "1900")
                {
                    return null;
                }
                else
                {
                    dueDate = m_PmsCommonBiz.FormatDateTime(pmsHead.DueDate.ToShortDateString());
                    return dueDate;
                }
            }

        }

        private string getStringPlanStartDate(PmsHead pmsHead)
        {
            string planStartDate = string.Empty;
            if (pmsHead.PlanStartDate == null)
            {
                return null;
            }
            else
            {
                string planStartYear = pmsHead.PlanStartDate.ToString("yyyy");
                if (planStartYear == "0000" || planStartYear == "0001" || planStartYear == "1900")
                {
                    return null;
                }
                else
                {
                    planStartDate = m_PmsCommonBiz.FormatDateTime(pmsHead.PlanStartDate.ToShortDateString());
                    return planStartDate;
                }
            }

        }


        #endregion

        #region Create Mail

        public void SendCreateMail(PmsHead pmsHead)
        {
            pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(pmsHead.PmsId)[0];
            m_hasMailList.Clear();
            string mailBody = string.Empty;
            mailBody = GetCreateStringMailBody(pmsHead);
            IList<BaseDataUser> mailToList = GetCreateMailManagers(pmsHead);

            AddInfoToMailList(mailToList, mailBody);

            string strMailto = string.Empty;
            foreach (DictionaryEntry item in m_hasMailList)
            {
                string mailTo = item.Key.ToString() + ";";
                strMailto += mailTo;
            }
            string strMailcc = string.Empty;
            // strMailcc = pmsHead.Pm.ToString();

            strMailcc = string.Empty;

            SendCurrentMail(pmsHead, PmsCommonEnum.MailType.CreateMail, strMailto, strMailcc, mailBody);

        }
        #endregion

        #region Create MeetingMinute Mail

        public bool SendCreateMeetingMinuteMail(string pmsId, string minId, string loginName, string action)
        {
            try
            {
                PmsMinHead pmsMinHead = new PmsMinHeadBiz().SelectPmsMinHeadByPmsIdMinId(pmsId, minId).FirstOrDefault();
                PmsHead pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(pmsId).FirstOrDefault();
                if (pmsMinHead == null || pmsHead == null)
                {
                    return false;
                }

                //Mail To
                string mailto = string.Empty;
                string mailcc = string.Empty;
                IList<BaseDataUser> mailToRDList = GetRDs(pmsHead);
                foreach (BaseDataUser user in mailToRDList)
                {
                    if (!string.IsNullOrEmpty(user.MailAddress) && !mailto.Contains(user.MailAddress))
                    {
                        mailto += user.MailAddress + ";";
                    }
                }

                string mailBodyTemplate = string.Empty;

                if (action.Trim().ToUpper() == "CREATED")
                {
                    mailBodyTemplate = ConfigurationSettings.AppSettings["CreateMeetingMinuteMailBodyTemplate"];
                }
                else
                {
                    mailBodyTemplate = ConfigurationSettings.AppSettings["EditMeetingMinuteMailBodyTemplate"];
                }
                string mailTemplate = m_PmsCommonBiz.GetHtmlContent(HttpRuntime.AppDomainAppPath + mailBodyTemplate);
                string strMailBody = GetMeetingMinuteMailBody(pmsMinHead);
                mailTemplate = mailTemplate.Replace("$Body$", strMailBody);

                string crId = new PmsItarmMappingBiz().SelectPmsItarmMappingOther(pmsMinHead.PmsId)[0].CrId;
                string meetingMinuteTypeDesc = GetMeetingMinuteTypeDesc(pmsMinHead.MeetingType) + " " + "Minutes";
                mailTemplate = mailTemplate.Replace("$Type$", meetingMinuteTypeDesc);

                string actionDesc = string.Empty;
                if (action.Trim().ToUpper() == "CREATED")
                {
                    actionDesc = "已被创建";
                }
                else
                {
                    actionDesc = "已被编辑";
                }

                string meeting_url = ConfigurationSettings.AppSettings["PMSExternalSystemViewUrl"].ToString();
                meeting_url = ConfigurationSettings.AppSettings["PMSExternalSystemViewUrl"].ToString() + "?Action=MEETING&PmsID=" + pmsHead.PmsId;
                string linkAddress = "<a href = " + meeting_url + ">" + crId + "</a>";
                // string strPmsInfor = pmsHead.PmsName + " " + "(" + linkAddress + ")'s" + " " + meetingMinuteTypeDesc + " " + actionDesc;
                mailTemplate = mailTemplate.Replace("$LinkAddress$", linkAddress);

                string subject = crId + "'s" + " " + meetingMinuteTypeDesc + " " + "has been " + action + " by" + " " + loginName.Replace(".", " "); ;

                Hashtable hashMail = new Hashtable();
                hashMail.Add("Form", ConfigurationSettings.AppSettings["PMSMailForm"].TrimEnd(';').Trim());
                hashMail.Add("To", mailto);
                hashMail.Add("Cc", mailcc);
                hashMail.Add("Subject", subject);
                hashMail.Add("Body", mailTemplate);
                hashMail.Add("BodyFormat", "HTML");
                SendMail(hashMail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GetMeetingMinuteMailBody(PmsMinHead pmsMinHead)
        {
            string mailBody = string.Empty;
            string meetingMinuteTypeDesc = GetMeetingMinuteTypeDesc(pmsMinHead.MeetingType);
            IList<PmsMinconclution> pmsMinconclutionList = new PmsMinconclutionBiz().SelectPmsMinconclutionByMinId(pmsMinHead.Mnid);
            string pmsMinconclutionHtml = GetMinconclutionHtml(pmsMinconclutionList);
            string pmsIssuesHtml = GetIssuesHtml(pmsMinHead);
            string pmsHeadHtml = GetHeadHtml(pmsMinHead);

            mailBody = pmsHeadHtml
                       + " <table style= 'border: 1px solid black; border-collapse: collapse;width:90%; cellpadding:5; cellspacing:0;' >"
                       + "<tr style=' border: 1px black;'>"
                       + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;background-color: #EBEBEB;'><b>Meeting Type</b></td>"
                       + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%; '>" + meetingMinuteTypeDesc + "</td>"
                       + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%; background-color: #EBEBEB;'><b>Host</b></td>"
                       + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:100px;'>" + pmsMinHead.Host + "</td>"
                       + "</tr>"
                        + "<tr style=' border: 1px black;'>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;background-color: #EBEBEB;'><b>Venue</b></td>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;'>" + pmsMinHead.Venue + "</td>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;background-color: #EBEBEB;'><b>Recorder</b></td>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;'>" + pmsMinHead.Recorder + "</td>"
                        + "</tr>"
                        + "<tr style=' border: 1px black;'>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;background-color: #EBEBEB;'><b>Meeting Start Time</b></td>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;'>" + pmsMinHead.StartTime + "</td>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;background-color: #EBEBEB;'><b>Meeting End Time</b></td>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;'>" + pmsMinHead.EndTime + "</td>"
                        + "</tr>"
                        + "<tr style=' border: 1px black;'>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;background-color: #EBEBEB;'><b>Subject</b></td>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none;' colspan = '3'>" + pmsMinHead.Subject + "</td>"
                        + "</tr>"
                        + "<tr style=' border: 1px black;'>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:25%;background-color: #EBEBEB;'><b>Attendee</b></td>"
                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none;' colspan = '3'>" + pmsMinHead.Attendee + "</td>"
                        + "</tr>"
                        + "</table>"
                        + pmsMinconclutionHtml
                        + pmsIssuesHtml;

            return mailBody;

        }
        private string GetHeadHtml(PmsMinHead pmsMinHead)
        {
            string minconclutionHtml = string.Empty;
            string minconclutionHtmlHead = string.Empty;
            string minconclutionHtmlBody = string.Empty;
            string pmsID = pmsMinHead.PmsId.ToString();
            string CrNo = string.Empty;

            PmsItarmMappingBiz pmsItarmMappingSelect = new PmsItarmMappingBiz();
            PmsHead pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(pmsID).FirstOrDefault();
            IList<PmsItarmMapping> pmsItarmMappingresult = pmsItarmMappingSelect.SelectPmsItarmMappingOther(pmsID);
            if (pmsItarmMappingresult != null && pmsItarmMappingresult.Count > 0)
            {
                CrNo = pmsItarmMappingresult[0].CrId;
            }
            PmsCommonEnum.ProjectStage stage = (PmsCommonEnum.ProjectStage)pmsHead.Stage;
            string stageName = stage.GetDescription();
            minconclutionHtmlHead = "<span style='color:blue'>CR Info:</span>"
                                  + "<table style= 'border: 1px solid black; border-collapse: collapse;width:90%; cellpadding:5; cellspacing:0;' >"
                                  + "<tr style=' border: 1px black;'>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>CR No</b></td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b> CR Name</b></td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>PM</b></td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>SD</b></td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>SE</b></td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>QA</b></td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>Stage</b></td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>Domain</b></td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>System</b></td>"
                                  + "</tr>";


            minconclutionHtmlBody = minconclutionHtmlBody + "<tr style=' border: 1px black;'>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + CrNo + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + pmsHead.PmsName + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + pmsHead.Pm + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + pmsHead.Sd + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + pmsHead.Se + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + pmsHead.Qa + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + stageName + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + pmsHead.Domain + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + pmsHead.System + "</td>"
                                  + "</tr></table>"
                                  + "<br/>";

            minconclutionHtml = minconclutionHtmlHead + minconclutionHtmlBody;

            return minconclutionHtml;
        }


        private string GetMinconclutionHtml(IList<PmsMinconclution> pmsMinconclutionList)
        {
            string minconclutionHtml = string.Empty;
            string minconclutionHtmlHead = string.Empty;
            string minconclutionHtmlBody = string.Empty;
            if (pmsMinconclutionList.Count > 0)
            {
                minconclutionHtmlHead = "<br/>"
                                        + "<span style='color:blue'>Conclusions:</span>"
                                        + "<table style= 'border: 1px solid black; border-collapse: collapse;width:90%; cellpadding:5; cellspacing:0;' >"
                                        + "<tr style=' border: 1px black;'>"
                                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:50px; background-color: #EBEBEB;'><b>No.</b></td>"
                                        + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>Title</b></td>"
                                        + "</tr>";
            }
            int serial = 1;
            foreach (PmsMinconclution pmsMinconclution in pmsMinconclutionList)
            {
                minconclutionHtmlBody = minconclutionHtmlBody + "<tr style=' border: 1px black;'>"
                                   + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:50px;'>" + serial + "</td>"
                                   + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + pmsMinconclution.Description + "</td>"
                                   + "</tr>";
                serial++;
            }
            if (pmsMinconclutionList.Count > 0)
            {
                minconclutionHtml = minconclutionHtmlHead + minconclutionHtmlBody + "</table>";
            }
            return minconclutionHtml;
        }

        private string GetIssuesHtml(PmsMinHead pmsMinHead)
        {
            string minconclutionHtml = string.Empty;
            string minconclutionHtmlHead = string.Empty;
            string minconclutionHtmlBody = string.Empty;
            PmsItarmMapping pmsItarmMapping = new PmsItarmMappingBiz().SelectPmsItarmMappingOther(pmsMinHead.PmsId.ToString()).FirstOrDefault();
            IList<BfIssueinfo> bfIssueinfoList = new IssueFreeBiz().GetIssueinfo(pmsItarmMapping.CrId, pmsMinHead.Mnid);
            if (bfIssueinfoList.Count > 0)
            {
                minconclutionHtmlHead = "<br/>"
                                     + "<span style='color:blue'>Issues:</span>"
                                     + "<table style= 'border: 1px solid black; border-collapse: collapse;width:90%; cellpadding:5; cellspacing:0;' >"
                                     + "<tr style=' border: 1px black;'>"
                                     + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:100px; background-color: #EBEBEB;'><b>IssueID</b></td>"
                                     + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; background-color: #EBEBEB;'><b>Description</b></td>"
                                     + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:100px; background-color: #EBEBEB;'><b>Creator</b></td>"
                                     + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:100px;background-color: #EBEBEB;'><b>Owner</b></td>"
                                     + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:100px;background-color: #EBEBEB;'><b>Status</b></td>"
                                     + "</tr>";

            }

            foreach (BfIssueinfo bfIssueinfo in bfIssueinfoList)
            {
                string issueViewUrl = ConfigurationManager.AppSettings["IssueViewUrl"];
                string issueID = bfIssueinfo.Issueid.ToString();
                string url = issueViewUrl + issueID;
                string urlHtml = "<a href='" + url + "'target='_blank' style='color:blue'>" + issueID + "</a>";
                minconclutionHtmlBody = minconclutionHtmlBody + "<tr style=' border: 1px black;'>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:100px;'>" + urlHtml + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; '>" + bfIssueinfo.IssueTitle + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:100px;'>" + bfIssueinfo.OpenedBy + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:100px;'>" + bfIssueinfo.AssignedTo + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:100px;'>" + bfIssueinfo.IssueStatus + "</td>";
            }

            if (bfIssueinfoList.Count > 0)
            {
                minconclutionHtml = minconclutionHtmlHead + minconclutionHtmlBody + "</tr></table>";

            }
            return minconclutionHtml;
        }

        private string GetMeetingMinuteTypeDesc(int type)
        {
            switch (type)
            {
                case (int)PmsCommonEnum.MeetingType.PESMeeting:
                    return "PES Meeting";
                case (int)PmsCommonEnum.MeetingType.PISMeeting:
                    return "PIS Meeting";
                case (int)PmsCommonEnum.MeetingType.STPMeeting:
                    return "STP Meeting";
                case (int)PmsCommonEnum.MeetingType.STCMeeting:
                    return "STC Meeting";
                case (int)PmsCommonEnum.MeetingType.RLNMeeting:
                    return "RLN Meeting";
                default:
                    return "Other";
            }

        }

        #endregion

        #region Promote Mail

        public void SendPromoteMail(PmsHead pmsHead, int currentStage)
        {

            m_hasMailList.Clear();
            string strMailBody = string.Empty;
            strMailBody = GetPromoteStringMailBody(pmsHead, currentStage);

            //Mail To
            IList<BaseDataUser> mailToRDList = GetRDs(pmsHead);
            string mailto = string.Empty;
            string mailcc = string.Empty;

            foreach (BaseDataUser user in mailToRDList)
            {
                if (!string.IsNullOrEmpty(user.MailAddress) && !mailto.Contains(user.MailAddress))
                {
                    mailto += user.MailAddress + ";";
                }
            }

            //Mail CC

            IList<BaseDataUser> mailToManagerList = GetPromoteMailManagers(pmsHead, currentStage);
            foreach (BaseDataUser user in mailToManagerList)
            {
                if (!string.IsNullOrEmpty(user.MailAddress) && !mailcc.Contains(user.MailAddress))
                {
                    mailcc += user.MailAddress + ";";
                }
            }
            switch (currentStage)
            {
                case (int)PmsCommonEnum.ProjectStage.AssignMember:
                    string tempMailto = mailto;
                    mailto = mailcc;
                    mailcc = tempMailto;
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.AssignMemberMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.ProjectStage.PIS_STP:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.PISAndSTPMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.ProjectStage.Develop_Test:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.DevelopAndTestMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.ProjectStage.Release:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.ReleaseMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.ProjectStage.Closed:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.ClosedMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.ProjectStage.Pending:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.PendingMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.ProjectStage.HardClosed:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.HardClosedMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.ProjectStage.Cancelled:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.CancelledMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.ProjectStage.Reactive:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.ReactiveMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.MailType.ConfirmMail:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.ConfirmMail, mailto, mailcc, strMailBody); break;
                case (int)PmsCommonEnum.ProjectStage.WaitingClosed:
                    SendCurrentMail(pmsHead, PmsCommonEnum.MailType.WaitingClosed, mailto, mailcc, strMailBody); break;
            }
        }


        #endregion

        #region Sendmail

        private void SendCurrentMail(PmsHead pmsHead, PmsCommonEnum.MailType mailType, string mailto, string mailCC, string strMailBody)
        {
            string subject;
            string template;
            string sdp_url;

            switch (mailType)
            {
                case PmsCommonEnum.MailType.ConfirmMail:
                    sdp_url = ConfigurationSettings.AppSettings["PMSExternalSystemViewUrl"].ToString() + "?Action=SDPEDIT&PmsID=" + pmsHead.PmsId;
                    break;

                default:
                    sdp_url = ConfigurationSettings.AppSettings["PMSExternalSystemViewUrl"].ToString() + "?Action=VIEW&PmsID=" + pmsHead.PmsId;
                    break;
            }

            string linkAddress = "<a href = " + sdp_url + ">PMS</a>";

            Hashtable subjectAndTemplate = GetSubjectAndMailTemplate(pmsHead, mailType);
            foreach (DictionaryEntry ST in subjectAndTemplate)
            {
                subject = ST.Key.ToString();
                if (subject.Contains("$Type$") && pmsHead.Type == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                {
                    subject = subject.Replace("$Type$", "Service");
                }
                else
                {
                    if (subject.Contains("$Type$"))
                    {
                        subject = subject.Replace("$Type$", "CR");
                    }

                }

                if (subject.Contains("$Creator$"))
                {
                    if (pmsHead.Creator.Contains("."))
                    {
                        pmsHead.Creator = pmsHead.Creator.Replace(".", " ");
                    }
                    subject = subject.Replace("$Creator$", pmsHead.Creator);
                }
                if (subject.Contains("$Promoter$"))
                {
                    if (pmsHead.UserName.Contains("."))
                    {
                        pmsHead.UserName = pmsHead.UserName.Replace(".", " ");
                    }
                    subject = subject.Replace("$Promoter$", pmsHead.UserName);
                }

                template = ST.Value.ToString();

                switch (mailType)
                {
                    case PmsCommonEnum.MailType.ConfirmMail:

                        template = template.Replace("$Body$", strMailBody);
                        template = ConfirmMail(pmsHead, template);
                        break;
                    default:
                        if (template.Contains("$Type$") && pmsHead.Type == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                        {
                            template = template.Replace("$Type$", "Service");
                        }
                        else
                        {
                            if (template.Contains("$Type$"))
                            {
                                template = template.Replace("$Type$", "CR");
                            }
                        }
                        template = template.Replace("$Body$", strMailBody);
                        if (template.Contains("$ResolveDescription$"))
                        {
                            string tableResolveDesc = GetResolveDescription(pmsHead);
                            template = template.Replace("$ResolveDescription$", tableResolveDesc);

                        }
                        break;
                }

                string templateLinkAddressReplaced = template.Replace("$LinkAddress$", linkAddress);
                string finalMailBody = templateLinkAddressReplaced;
                Hashtable hashMail = new Hashtable();
                hashMail.Add("Form", ConfigurationSettings.AppSettings["PMSMailForm"].TrimEnd(';').Trim());
                hashMail.Add("To", mailto);
                hashMail.Add("Cc", mailCC);
                hashMail.Add("Subject", subject);
                hashMail.Add("Body", finalMailBody);
                hashMail.Add("BodyFormat", "HTML");
                SendMail(hashMail);


            }
        }
        private string GetResolveDescription(PmsHead pmsHead)
        {
            string resolveDesc = pmsHead.ResolveDescription;
            resolveDesc = resolveDesc.Replace("\r\n", "<br/>").Replace(" ", "&nbsp&nbsp");
            string tableResolveDesc = "<span style='color:blue'>Resolve Description</span>"
                                    + "<table width='500px' cellpadding='5' cellspacing='0' border='0'>"
                                    + "<tr><td>" + resolveDesc + "</td></tr>"
                                    + "</table>"
                                    + "</br>";
            return tableResolveDesc;
        }
        private void SendMail(Hashtable hashMail)
        {

            try
            {

                m_WSCSqlConnection.BeginTransaction();

                m_WSCSqlConnection.QueryForObject("SendMail", hashMail);

                m_WSCSqlConnection.CommitTransaction();

            }
            catch (Exception ex)
            {
                m_WSCSqlConnection.RollBackTransaction();
                m_Logger.Error("MailBiz/SendMail:", ex);
            }
        }

        #endregion

        #region SDP Confirm Mail
        // ConfirmMail is special, it contains SDP task Information
        private string ConfirmMail(PmsHead pmsHead, string template)
        {
            // 
            template = GetTableByStage(pmsHead, template, PmsCommonEnum.EnumSdpPhase.Design);
            template = GetTableByStage(pmsHead, template, PmsCommonEnum.EnumSdpPhase.Development);
            template = GetTableByStage(pmsHead, template, PmsCommonEnum.EnumSdpPhase.Test);
            template = GetTableByStage(pmsHead, template, PmsCommonEnum.EnumSdpPhase.Release);

            return template;

        }
        private string GetTableByStage(PmsHead pmsHead, string template, PmsCommonEnum.EnumSdpPhase spdPhase)
        {
            SdpDetail sdpDetail = new SdpDetail();
            sdpDetail.Pmsid = pmsHead.PmsId;
            sdpDetail.Phase = ((int)spdPhase).ToString();
            string tablePlaceHold = string.Empty;
            string tableStage = string.Empty;
            switch (spdPhase)
            {
                case PmsCommonEnum.EnumSdpPhase.Design:
                    tablePlaceHold = "$DesignTable$";
                    tableStage = "设计阶段";
                    break;
                case PmsCommonEnum.EnumSdpPhase.Development:
                    tablePlaceHold = "$DevelopmentTable$";
                    tableStage = "开发阶段";
                    break;
                case PmsCommonEnum.EnumSdpPhase.Test:
                    tablePlaceHold = "$TestTable$";
                    tableStage = "测试阶段";
                    break;
                case PmsCommonEnum.EnumSdpPhase.Release:
                    tablePlaceHold = "$ReleaseTable$";
                    tableStage = "Release阶段";
                    break;
            }


            SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
            IList<SdpDetail> sdpDetailList = sdpDetailBiz.SelectSdpDetail(sdpDetail);

            if (sdpDetailList != null && sdpDetailList.Count > 0)
            {
                string specificTable = GetSpecificTable(sdpDetailList, tableStage);
                template = template.Replace(tablePlaceHold, specificTable);
            }
            else
            {
                template = template.Replace(tablePlaceHold, string.Empty);
            }
            return template;
        }
        private string GetSpecificTable(IList<SdpDetail> sdpDetailList, string tableStage)
        {
            string designTable = string.Empty;
            string designTableRow;
            designTableRow = "<span style='color:blue'>" + tableStage + "</span>"
                             + "<table style= 'border: 1px solid black; border-collapse: collapse;width:80%; cellpadding:5; cellspacing:0;'>"
                             + "<tr style=' border: 1px black;background-color: #EBEBEB;'>"
                             + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:80px;'><b>识别码</b></td>"
                             + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:300px;'><b>任务名称</b></td>"
                             + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:80px;'><b>计划工时</b></td>"
                             + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:150px;'><b>计划开始日期</b></td>"
                             + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:120px;'><b>计划结束日期</b></td>"
                             + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; width:100px;'><b>资源</b></td></tr>";


            for (int i = 0; i < sdpDetailList.Count; i++)
            {
                designTableRow += GetDesignTableRow(sdpDetailList[i], sdpDetailList.Count(), i);
            }


            designTable += designTableRow;
            return designTable + "</table><br/>"; ;
        }
        private string GetDesignTableRow(SdpDetail sdpDetail, int Count, int i)
        {
            string taskNo = sdpDetail.Taskno.ToString();
            string taslName = sdpDetail.TaskName;
            string planCost = sdpDetail.Plancost.ToString();
            PmsCommonBiz pmsCommonBiz = new PmsCommonBiz();

            string planStartDay = pmsCommonBiz.FormatDateTime(sdpDetail.Planstartday.ToString("yyyy-MM-dd"));
            string planEndDay = pmsCommonBiz.FormatDateTime(sdpDetail.Planendday.ToString("yyyy-MM-dd"));
            string resource = sdpDetail.Resource;
            string designTableRow;
            if (i == Count - 1)
            {
                designTableRow = "<tr style='border: 1px solid black; '>"
                                  + "<td style='border: 1px solid black; border-right-style:none; width:80px;'>" + taskNo + "</td>"
                                  + "<td style='border: 1px solid black; border-right-style:none; width:300px;'>" + taslName + "</td>"
                                  + "<td style='border: 1px solid black; border-right-style:none; width:80px;'>" + planCost + "</td>"
                                  + "<td style='border: 1px solid black; border-right-style:none; width:150px;'>" + planStartDay + "</td>"
                                  + "<td style='border: 1px solid black; border-right-style:none; width:150px;'>" + planEndDay + "</td>"
                                  + "<td style='border: 1px solid black;  width:100px;'>" + resource + "</td></tr>";
            }
            else
            {
                designTableRow = "<tr style='border: 1px solid black; '>"
                                   + "<td style='border: 1px solid black; border-right-style:none; BORDER-BOTTOM-STYLE: none; width:80px;'>" + taskNo + "</td>"
                                   + "<td style='border: 1px solid black; border-right-style:none; BORDER-BOTTOM-STYLE: none; width:300px;'>" + taslName + "</td>"
                                   + "<td style='border: 1px solid black; border-right-style:none; BORDER-BOTTOM-STYLE: none; width:80px;'>" + planCost + "</td>"
                                   + "<td style='border: 1px solid black; border-right-style:none; BORDER-BOTTOM-STYLE: none; width:150px;'>" + planStartDay + "</td>"
                                   + "<td style='border: 1px solid black; border-right-style:none; BORDER-BOTTOM-STYLE: none; width:150px;'>" + planEndDay + "</td>"
                                   + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; width:100px;'>" + resource + "</td></tr>";
            }

            return designTableRow;
        }
        #endregion

        #region Send Information Changed Mail
        public bool SendInformationChangedMail(PmsHead pmsHeadInit, PmsHead pmsHeadNew, string loginName)
        {
            try
            {
                IList<BaseDataUser> mailToRDList = GetRDs(pmsHeadInit);
                IList<BaseDataUser> mailToNewRDList = GetRDs(pmsHeadNew);
                IList<BaseDataUser> allRDsList = mailToRDList.Union(mailToNewRDList).ToList();

                string mailto = string.Empty;
                foreach (BaseDataUser user in allRDsList)
                {
                    if (!string.IsNullOrEmpty(user.MailAddress) && !mailto.Contains(user.MailAddress))
                    {
                        mailto += user.MailAddress + ";";
                    }
                }

                string mailcc = string.Empty;
                //m_User.Role = "RD Leader";
                BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
                IList<BaseDataUser> rdLeaderList
                    = baseDataUserBiz.SelectBaseDataUser(null, PmsCommonEnum.OrgRole.RD_LEADER.GetDescription());
                foreach (BaseDataUser user in rdLeaderList)
                {
                    if (!string.IsNullOrEmpty(user.MailAddress) && !mailcc.Contains(user.MailAddress))
                    {
                        mailcc += user.MailAddress + ";";
                    }
                }

                string subject = pmsHeadInit.Type + " information has been changed by " + loginName.Replace(".", " ");

                string mailBodyTemplate = ConfigurationSettings.AppSettings["InformationChangedMailBodyTemplate"];
                string mailTemplate = m_PmsCommonBiz.GetHtmlContent(HttpRuntime.AppDomainAppPath + mailBodyTemplate);
                string strMailBody = GetMailBody(pmsHeadInit, pmsHeadNew);
                mailTemplate = mailTemplate.Replace("$Body$", strMailBody);
                mailTemplate = mailTemplate.Replace("$PmsName$", pmsHeadInit.PmsName);
                string sdp_url = ConfigurationSettings.AppSettings["PMSExternalSystemViewUrl"].ToString() + "?Action=VIEW&PmsID=" + pmsHeadNew.PmsId;
                PmsItarmMappingBiz pmsItarmMappingBiz = new PmsItarmMappingBiz();
                IList<PmsItarmMapping> pmsItarmMappingList = pmsItarmMappingBiz.SelectPmsItarmMappingOther(pmsHeadNew.PmsId);
                string crId = string.Empty;
                if (pmsItarmMappingList != null && pmsItarmMappingList.Count() > 0)
                {
                    crId = pmsItarmMappingList[0].CrId;
                }
                string linkAddress = "<a href = " + sdp_url + ">" + crId + "</a>";
                mailTemplate = mailTemplate.Replace("$LinkAddress$", linkAddress);

                Hashtable hashMail = new Hashtable();
                hashMail.Add("Form", ConfigurationSettings.AppSettings["PMSMailForm"].TrimEnd(';').Trim());
                hashMail.Add("To", mailto);
                hashMail.Add("Cc", mailcc);
                hashMail.Add("Subject", subject);
                hashMail.Add("Body", mailTemplate);
                hashMail.Add("BodyFormat", "HTML");
                SendMail(hashMail);
                return true;
            }
            catch
            {
                return false;
            }

        }

        private string GetMailBody(PmsHead pmsHeadInit, PmsHead pmsHeadNew)
        {
            PmsCommonBiz pmsCommonBiz = new PmsCommonBiz();
            string dueDateBefore = pmsCommonBiz.FormatDateTime(pmsHeadInit.DueDate.ToString("yyyy-MM-dd"));
            string dueDateAfter = pmsCommonBiz.FormatDateTime(pmsHeadNew.DueDate.ToString("yyyy-MM-dd"));
            string mailBody = "<table style= 'border: 1px solid black; border-collapse: collapse;width:100%; cellpadding:5; cellspacing:0;' >"
                              + "<tr><td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:150px;'><b>Changed Item</b></td>"
                              + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:150px;'><b>Before</b></td>"
                              + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; width:300px;'><b>After</b></td></tr>";
            int startRow = 1;
            int countRow = GetCountRow(pmsHeadInit, pmsHeadNew);
            if (dueDateBefore != dueDateAfter)
            {
                bool isLastRow = false;
                if (startRow == countRow)
                {
                    isLastRow = true;
                }
                string dueDateRow = GetChangedItemRow(dueDateBefore, dueDateAfter, isLastRow, "DueDate");
                mailBody += dueDateRow;
                startRow++;
            }

            if (pmsHeadInit.Pm != pmsHeadNew.Pm)
            {
                bool isLastRow = false;
                if (startRow == countRow)
                {
                    isLastRow = true;
                }
                string pmRow = GetChangedItemRow(pmsHeadInit.Pm, pmsHeadNew.Pm, isLastRow, "PM");
                mailBody += pmRow;
                startRow++;
            }
            if (pmsHeadInit.Sd != pmsHeadNew.Sd)
            {
                bool isLastRow = false;
                if (startRow == countRow)
                {
                    isLastRow = true;
                }
                string sdRow = GetChangedItemRow(pmsHeadInit.Sd, pmsHeadNew.Sd, isLastRow, "SD");
                mailBody += sdRow;
                startRow++;
            }
            if (pmsHeadInit.Qa != pmsHeadNew.Qa)
            {
                bool isLastRow = false;
                if (startRow == countRow)
                {
                    isLastRow = true;
                }
                string qaRow = GetChangedItemRow(pmsHeadInit.Qa, pmsHeadNew.Qa, isLastRow, "QA");
                mailBody += qaRow;
                startRow++;
            }
            if (pmsHeadInit.Se != pmsHeadNew.Se)
            {
                bool isLastRow = false;
                if (startRow == countRow)
                {
                    isLastRow = true;
                }
                string seRow = GetChangedItemRow(pmsHeadInit.Se, pmsHeadNew.Se, isLastRow, "SE");
                mailBody += seRow;
                startRow++;
            }


            return mailBody + "</table>"; ;
            ;
        }

        private int GetCountRow(PmsHead pmsHeadInit, PmsHead pmsHeadNew)
        {
            int count = 0;
            PmsCommonBiz pmsCommonBiz = new PmsCommonBiz();
            string dueDateBefore = pmsCommonBiz.FormatDateTime(pmsHeadInit.DueDate.ToString("yyyy-MM-dd"));
            string dueDateAfter = pmsCommonBiz.FormatDateTime(pmsHeadNew.DueDate.ToString("yyyy-MM-dd"));
            if (dueDateBefore != dueDateAfter)
            {
                count++;
            }
            if (pmsHeadInit.Pm != pmsHeadNew.Pm)
            {
                count++;
            }
            if (pmsHeadInit.Sd != pmsHeadNew.Sd)
            {
                count++;
            }
            if (pmsHeadInit.Qa != pmsHeadNew.Qa)
            {
                count++;
            }
            if (pmsHeadInit.Se != pmsHeadNew.Se)
            {
                count++;
            }

            return count;
        }

        private string GetChangedItemRow(string oldItem, string newItem, bool isLastRow, string changedItem)
        {
            string changedRow;
            if (isLastRow)
            {
                changedRow = "<tr><td style='border: 1px solid black; border-right-style:none; width:150px;'>" + changedItem + "</td>"
                                  + "<td style='border: 1px solid black; border-right-style:none; width:300px;'>" + oldItem + "</td>"
                                  + "<td style='border: 1px solid black; width:300px;'>" + newItem + "</td></tr>";
            }

            else
            {
                changedRow = "<tr><td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:150px;'>" + changedItem + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; border-right-style:none; width:300px;'>" + oldItem + "</td>"
                                  + "<td style='border: 1px solid black; BORDER-BOTTOM-STYLE: none; width:300px;'>" + newItem + "</td></tr>";
            }

            return changedRow;
        }

        #endregion

        #region TaskMail
        public bool TaskMail(string serial, int taskMailType)
        {
            try
            {
             
                string crId = string.Empty;
                string PmsId = string.Empty;

                //serial捞取crId，PmsId
                //submitor = Createuser,auditor
                SdpDetail sdpDetail = new SdpDetailBiz().SelectSdpDetailAndCrIdBySerial(serial)[0];
                crId = sdpDetail.CrId;
                PmsId = sdpDetail.Pmsid;

                //提交人
                string submitor = string.Empty;
                submitor = sdpDetail.Createuser.Replace(".", " ");
                IList<BaseDataUser> submitorList = new BaseDataUserBiz().SelectBaseDataUser(sdpDetail.Createuser, null);
                string submitorMailAddress = (submitorList == null || submitorList.Count == 0) ? "" : submitorList[0].MailAddress;

                //审核人
                string auditor = string.Empty;
                auditor = sdpDetail.Auditor.Replace(".", " ");

                IList<BaseDataUser> auditorList = new BaseDataUserBiz().SelectBaseDataUser(sdpDetail.Auditor, null);
                //real
                string auditorMailAddress = (auditorList == null || auditorList.Count == 0) ? "" : auditorList[0].MailAddress;

                //Mail To
                string mailto = string.Empty;
                //Mail cc
                string mailcc = string.Empty;

                switch (taskMailType)
                {
                    case 1:
                        mailto = auditorMailAddress;
                        mailcc = submitorMailAddress;
                        break;
                    case 2:
                        mailto = auditorMailAddress;
                        mailcc = submitorMailAddress;
                        break;
                    case 3:
                        mailto = submitorMailAddress;
                        mailcc = auditorMailAddress;
                        break;
                    case 4:
                        mailto = submitorMailAddress;
                        mailcc = auditorMailAddress;
                        break;
                    default:
                        break;
                }


                //Mail Body
                string mailBodyTemplate = string.Empty;

                switch (taskMailType)
                {
                    case 1:
                        mailBodyTemplate = ConfigurationSettings.AppSettings["RecallTaskMailBodyTemplate"];
                        break;
                    case 2:
                        mailBodyTemplate = ConfigurationSettings.AppSettings["SubmitTaskMailBodyTemplate"];
                        break;
                    case 3:
                        mailBodyTemplate = ConfigurationSettings.AppSettings["ApproveTaskMailBodyTemplate"];
                        break;
                    case 4:
                        mailBodyTemplate = ConfigurationSettings.AppSettings["RejectTaskMailBodyTemplate"];
                        break;
                    default:
                        break;
                }
                string mailTemplate = m_PmsCommonBiz.GetHtmlContent(HttpRuntime.AppDomainAppPath + mailBodyTemplate);

                string task_url = ConfigurationSettings.AppSettings["PMSExternalSystemViewUrl"].ToString();
                task_url = ConfigurationSettings.AppSettings["PMSExternalSystemViewUrl"].ToString() + "?Action=SCHEDULE&PmsID=" + PmsId;
                string linkAddress = "<a href = " + task_url + ">" + crId + "</a>";
                mailTemplate = mailTemplate.Replace("$LinkAddress$", linkAddress);

                //Mail Subject
                string subject = string.Empty;
                switch (taskMailType)
                {
                    case 1:
                        subject = ConfigurationSettings.AppSettings["RecallTaskMailSubject"];
                        subject = subject.Replace("$Promoter$", submitor);
                        break;
                    case 2:
                        subject = ConfigurationSettings.AppSettings["SubmitTaskMailSubject"];
                        subject = subject.Replace("$Promoter$", submitor);
                        break;
                    case 3:
                        subject = ConfigurationSettings.AppSettings["ApproveTaskMailSubject"];
                        subject = subject.Replace("$Promoter$", auditor);
                        break;
                    case 4:
                        subject = ConfigurationSettings.AppSettings["RejectTaskMailSubject"];
                        subject = subject.Replace("$Promoter$", auditor);
                        break;
                    default:
                        break;
                }
                Hashtable hashMail = new Hashtable();
                hashMail.Add("Form", ConfigurationSettings.AppSettings["PMSMailForm"].TrimEnd(';').Trim());
                hashMail.Add("To", mailto);
                hashMail.Add("Cc", mailcc);
                hashMail.Add("Subject", subject);
                hashMail.Add("Body", mailTemplate);
                hashMail.Add("BodyFormat", "HTML");
                SendMail(hashMail);
                return true;
            }
            catch(Exception ex)
            {
                m_Logger.Error("MailBiz/SendMail:", ex);
                return false;
            }
        }
        #endregion
    }
}
