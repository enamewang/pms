using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.PMS.AppCode;
using Qisda.PMS.Business;
using Qisda.PMS.Common;
using Qisda.PMS.Entity;
using PMS.PMS.UserControls;

namespace PMS.PMS.Maintain
{
    public partial class ProjectsInformation : PageBase
    {
        protected ProjectsInformationBiz m_ProjectsInformationBiz = new ProjectsInformationBiz();
        // BaseDataUser CurrentUser = new BaseDataUser();

        #region Define Variable

        private string m_PmsID;
        public string PmsID
        {
            get
            {
                if (string.IsNullOrEmpty(m_PmsID))
                {
                    m_PmsID = (Request.QueryString["PmsID"] ?? string.Empty).Trim();
                }
                return m_PmsID;
                //string pmsID = (Request.QueryString["PmsID"] ?? string.Empty).Trim();
                //return pmsID;
            }
            set
            {
                m_PmsID = value;
            }

        }

        private string m_LoginName;
        public string LoginName
        {
            get
            {
                if (string.IsNullOrEmpty(m_LoginName))
                {
                    m_LoginName = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                }
                return m_LoginName;
            }
            set { m_LoginName = value; }
        }

        private string m_CrId;
        public string CrId
        {
            get
            {
                if (string.IsNullOrEmpty(m_CrId))
                {
                    if (!m_ProjectsInformationBiz.GetCrID(PmsID, out m_CrId))
                    {
                        Msgbox("Get CrId failure!");
                    }
                }
                return m_CrId;
            }
            set { m_CrId = value; }
        }

        BasicInformationDetailService BasicInformationDetailService1 { get; set; }
        BasicInformationDetail BasicInformationDetail1 { get; set; }

        //仅供PageLoad时使用
        private PmsHead m_PmsHead;
        public PmsHead InitPmsHead
        {
            get
            {
                if (m_PmsHead == null)
                {
                    PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                    IList<PmsHead> pmsHeadList = pmsHeadBiz.SelectPmsHeadByPmsId(PmsID);
                    if (pmsHeadList != null && pmsHeadList.Count > 0)
                        m_PmsHead = pmsHeadList[0];
                }
                return m_PmsHead;

            }
            set { m_PmsHead = value; }
        }

        private int m_Stage = 0;
        public int Stage
        {
            get
            {
                if (m_Stage == 0)
                {
                    m_Stage = InitPmsHead.Stage;
                }
                return m_Stage;
            }
            set
            {
                m_Stage = value;

                if (BasicInformationDetail1 != null)
                {
                    //给页面级变量Stage赋值时，一并修改BasicInformationDetail1页面上的TextBoxStage的text
                    BasicInformationDetail1.BasicPageTextBoxStageName
                        = Enum.Parse(typeof(PmsCommonEnum.ProjectStage), value.ToString()).GetDescription();
                }

                if (BasicInformationDetailService1 != null)
                {
                    //Mark by Ename Wang on 20111128 11:01
                    //for setting BasicInformationDetailService1.BasicPageTextBoxStageName 
                    BasicInformationDetailService1.BasicPaGetextBoxStageName
                        = Enum.Parse(typeof(PmsCommonEnum.ProjectStage), value.ToString()).GetDescription();
                }
            }
        }

        private string m_ProjectType;
        public string ProjectType
        {
            get
            {
                if (string.IsNullOrEmpty(m_ProjectType))
                {
                    m_ProjectType = InitPmsHead.Type;
                }
                return m_ProjectType;
            }
            set { m_ProjectType = value; }
        }

        // 这段代码会引起cs1061错误
        // 可能原因是System和关键字冲突。

        private string m_System;
        public string SystemName
        {
            get
            {
                if (string.IsNullOrEmpty(m_System))
                {
                    m_System = InitPmsHead.System;
                }
                return m_System;
            }
            set { m_System = value; }
        }

        private string m_NewVersion;
        public string NewVersion
        {
            get
            {
                if (string.IsNullOrEmpty(m_NewVersion))
                {
                    m_NewVersion = InitPmsHead.NewVersion;
                }
                return m_NewVersion;
            }
            set { m_NewVersion = value; }
        }

        private BaseDataUser m_CurrentUser;
        public BaseDataUser CurrentUser
        {
            get
            {
                if (m_CurrentUser == null)
                {
                    m_CurrentUser = new BaseDataUser();

                    //对当前的CurrentUser设置属性
                    // ProjectsInformationBiz projectsInformationBiz=new ProjectsInformationBiz();
                    if (!m_ProjectsInformationBiz.GetCurrentUser(ref m_CurrentUser, PmsID, LoginName))
                    {
                        Msgbox("InitUserRole failure!");
                    }

                }
                return m_CurrentUser;
            }
            set
            {
                m_CurrentUser = value;
                //对当前的CurrentUser的属性设置
                if (!m_ProjectsInformationBiz.GetCurrentUser(ref m_CurrentUser, PmsID, LoginName))
                {
                    Msgbox("InitUserRole failure!");
                }

            }
        }

        //将所有的Type和对应的stage都取出来
        private IList<PmsFlowTemplate> projectTypeStageList;
        public IList<PmsFlowTemplate> ProjectTypeStageList
        {
            get
            {
                if (projectTypeStageList == null)
                {
                    projectTypeStageList = new PmsFlowTemplateBiz().SelectPmsFlowTemplateType();
                    if (projectTypeStageList == null)
                    {
                        Msgbox("Get projectType failure!");
                    }
                }
                return projectTypeStageList;
            }
            set
            {
                projectTypeStageList = value;
            }
        }

        public string DocUrl
        {
            get
            {
                object obj = ViewState["DocUrl"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["DocUrl"] = value; }
        }

        // add by Ename Wang on 20120321
        public string ChangeHistoryUrl
        {
            get
            {
                object obj = ViewState["ChangeHistoryUrl"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["ChangeHistoryUrl"] = value; }

        }
        // end add

        // add by Ename Wang on 20120321
        public string MeetingMinuteUrl
        {
            get
            {
                object obj = ViewState["MeetingMinuteUrl"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["MeetingMinuteUrl"] = value; }

        }
        // end add
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // 根据ProjectType加载BasicInformationDetail
            if (ProjectType == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
            {
                BasicInformationDetailService1 = (BasicInformationDetailService)Page.LoadControl("../UserControls/BasicInformationDetailService.ascx");
                TabPanelBasicInformation.Controls.Add(BasicInformationDetailService1);
            }
            else
            {
                BasicInformationDetail1 = (BasicInformationDetail)Page.LoadControl("../UserControls/BasicInformationDetail.ascx");
                TabPanelBasicInformation.Controls.Add(BasicInformationDetail1);
            }

            string action = Request.QueryString["Action"];
            if (action != null)
            {
                switch (action)
                {
                    case "SCHEDULE":
                        Tabs1.ActiveTabIndex = 1;
                        break;
                    case "SDPEDIT":
                        Tabs1.ActiveTabIndex = 2;
                        break;
                    case "HISTORY":
                        Tabs1.ActiveTabIndex = 4;
                        break;
                    case "MEETING":
                        Tabs1.ActiveTabIndex = 5;
                        break;
                    default:
                        Tabs1.ActiveTabIndex = 0;
                        break;
                }
            }

            // 上传文档界面的Url
            DocUrl = "DocumentsMaintain.aspx" + "?&PmsID=" + PmsID + "&CrID=" + CrId + "&ProjectType=" + ProjectType + "&System=" + SystemName + "&NewVersion=" + NewVersion;

            // 变更历史界面的Url
            ChangeHistoryUrl = "ChangeHistory.aspx" + "?&PmsID=" + PmsID + "&CrID=" + CrId;

            // 添加会议记录界面的Url
            MeetingMinuteUrl = "MeetingMinuteMaintain.aspx" + "?&PmsID=" + PmsID + "&CrID=" + CrId;

        }

        #region 控件函数的相互调用(提供调用的平台)
        public void ProjectProgressInitPmsFlow(int stage, string type)
        {
            ProjectProgress1.InitPmsFlow(stage, type);

            ProjectType = type;
        }

        public void ChangeImageButtonStatus(bool statue)
        {
            //设置ImageButton不可点，或者可点。
            //记得上次的状态，当点击edit按钮时，变化，Ok时复原、
            ProjectProgress1.ChangeImageButtonStageStatus(statue);
        }

        public bool ImageButtonStageIsEnabled
        {
            get
            {
                return ProjectProgress1.ImageButtonStageIsEnabled;
            }
        }

        public void SetReleaseButtonEnable()
        {
            if (BasicInformationDetail1 != null)
            {
                BasicInformationDetail1.SetReleaseButtonEnable();
            }
            if (BasicInformationDetailService1 != null)
            {
                BasicInformationDetailService1.SetReleaseButtonEnable();//Add by Ename Wang on 20111205 22:35
            }
        }
        #endregion

        #region 设置BasicInformationDetailServiceControl页面上的部分控件值
        public string SetTxtTotalManpowerBaseInforDetailService
        {
            set
            {
                if (BasicInformationDetailService1 != null)
                {
                    BasicInformationDetailService1.TotalManpower = value;
                }

            }
        }
        public string DurationForService
        {
            set
            {
                if (BasicInformationDetailService1 != null)
                {
                    BasicInformationDetailService1.Duration = value;
                }
            }

        }

        public string CloseDateForService
        {
            set
            {
                if (BasicInformationDetailService1 != null)
                {
                    BasicInformationDetailService1.CloseDate = value;
                }

            }
        }
        #endregion

        #region 设置BasicInformationDetail页面上的部分控件值
        //调用页面是SdpDetailInformation.ascx

        //Abel 注释掉 on 2014-01-22
        ////设置BasicInformationDetail页签上的txtProgress的值
        //public string SetTxtProgressBaseInforDetail
        //{
        //    set
        //    {
        //        if (BasicInformationDetailService1 != null)
        //        {
        //            BasicInformationDetail1.TotalProgress = value;
        //        }

        //    }
        //}

        ////设置BasicInformationDetail页签上的txtTotalManpower的值
        //public string SetTxtTotalManpowerBaseInforDetail
        //{
        //    set
        //    {
        //        if (BasicInformationDetail1 != null)
        //        {
        //            BasicInformationDetail1.TotalManpower = value;
        //        }

        //    }
        //}

        public string CloseDate
        {
            set
            {
                if (BasicInformationDetail1 != null)
                {
                    BasicInformationDetail1.CloseDate = value;
                }

            }
        }

        public string ActualStartDate
        {
            set
            {
                if (BasicInformationDetail1 != null)
                {
                    BasicInformationDetail1.ActualStartDate = value;
                }

            }
        }




        public Control BasicInformationDetailControl
        {
            get
            {
                return BasicInformationDetail1;
            }
        }

        public Control BasicInformationDetailServiceControl
        {
            get
            {
                return BasicInformationDetailService1;
            }
        }

        #endregion
        //Abel 注释掉 on 2014-01-22
        //#region 获取页面SdpDetailInformation.ascx上的项目完成情况信息
        //public string TotalTime
        //{
        //    get
        //    {
        //        return SdpDetailInformation1.TotalTime;
        //    }
        //}

        //public string DurationService
        //{
        //    get
        //    {
        //        return SdpDetailInformation1.DurationForService;
        //    }
        //}

        //public string TotalPercent
        //{
        //    get
        //    {
        //        return SdpDetailInformation1.TotalPercent;
        //    }

        //}

        //public string DesignCompletedPercent
        //{
        //    get
        //    {
        //        return SdpDetailInformation1.DesignCompletedPercent;
        //    }

        //}

        //public string DevelopmentCompletedPercent
        //{
        //    get
        //    {
        //        return SdpDetailInformation1.DevelopmentCompletedPercent;
        //    }

        //}

        //public string TestCompletedPercent
        //{
        //    get
        //    {
        //        return SdpDetailInformation1.TestCompletedPercent;
        //    }

        //}

        //public string ReleaseCompletedPercent
        //{
        //    get
        //    {
        //        return SdpDetailInformation1.ReleaseCompletedPercent;
        //    }

        //}

        //public string SupportCompletedPercent
        //{
        //    get
        //    {
        //        return SdpDetailInformation1.SupportCompletedPercent;
        //    }
        //}
        //#endregion

        [System.Web.Services.WebMethod]
        public static PmsSystemVersion GetVersionNewAndOld(string systemDomain, string sysName, string site)
        {
            PmsCRCreatBiz pmsCRCreatBiz = new PmsCRCreatBiz();
            IList<PmsSystemVersion> pmsSystemVersionList =
                pmsCRCreatBiz.SelectPmsSystemVersionByDomainSystem(systemDomain, sysName, site);

            if (pmsSystemVersionList != null && pmsSystemVersionList.Count > 0)
            {
                return pmsSystemVersionList[0];
            }
            else
            {
                PmsSystemVersion pmsSystemVersion = new PmsSystemVersion();
                pmsSystemVersion.NewVersion = "";
                pmsSystemVersion.OldVersion = "";
                return pmsSystemVersion;
            }
        }
    }
}
