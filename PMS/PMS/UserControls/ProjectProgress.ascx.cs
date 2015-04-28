using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.PMS.UserControls;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;
using System.Collections;
using System.Web.UI.HtmlControls;


namespace PMS.PMS.UserControls
{
    public partial class ProjectProgress : ProjectsInformationUserControlBase
    {
        protected readonly ProjectProgressBiz m_ProjectProgressBiz = new ProjectProgressBiz();
        //protected global::System.Web.UI.WebControls.ImageButton img;

        //存储image按钮的当前状态
        private bool imageButtonStageIsEnabled;
        public bool ImageButtonStageIsEnabled
        {
            set
            {
                imageButtonStageIsEnabled = value;
            }
            get
            {
                return imageButtonStageIsEnabled;
            }


        }

        //修改ImageButtonStageStatue的状态，用于Edit按钮和OK按钮事件复原
        public void ChangeImageButtonStageStatus(bool statue)
        {
            if (statue)
            {
                ImageButtonStage.Enabled = true;
                ImageButtonStage.ImageUrl = "~/SysFrame/images/right.JPG";
            }
            else
            {
                ImageButtonStage.ImageUrl = "~/SysFrame/images/right2.JPG";
                ImageButtonStage.Enabled = false;
            }

        }


        //  ImageButton img = new ImageButton();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            //放在!IsPostBack外面是防止这些发生变化。
            LabelProjectTitle.Text = ProjectType;
            LabelPMSIdTitle.Text = CrId;

            InitPmsFlow(Stage, ProjectType);
        }

        #region 绘制ProjectProgress控件
        //根据当前的stage和Type绘制该控件。
        public void InitPmsFlow(int stage, string type)
        {
            IList<string> stageNameList;
            bool isCurrentStageContains;

            //获取当前type对应的stageName集合stageNameList
            GetStageNameList(stage, type, out stageNameList, out isCurrentStageContains);

            //根据stageNameList绘制控件
            DrawControl(stageNameList, stage, isCurrentStageContains);

        }

        private void GetStageNameList(int stage, string type, out IList<string> stageNameList, out  bool isCurrentStageContains)
        {
            //获取当前的type类型对应的pmsFlow
            int intPending = (int)PmsCommonEnum.ProjectStage.Pending;
            int intHardClosed = (int)PmsCommonEnum.ProjectStage.HardClosed;
            int intCancelled = (int)PmsCommonEnum.ProjectStage.Cancelled;
            int intReactive = (int)PmsCommonEnum.ProjectStage.Reactive;

            //注意要以order作为排序依据
            IList<int> stages = ProjectTypeStageList.Select(t => t)
                .Where(p => (p.Typeid == type
                    && p.Stageid != intPending
                    && p.Stageid != intHardClosed
                    && p.Stageid != intCancelled
                    && p.Stageid != intReactive))
                    .OrderBy(m => m.Order)
                    .Select(t => t.Stageid).Distinct().ToList();

            //判断是否包含当前的stage
            isCurrentStageContains = stages.Contains(stage);
            if (!isCurrentStageContains)
            {
                stages.Add(stage);
            }

            stageNameList = new List<string>();
            foreach (int i in stages)
            {
                string stageTemp = Enum.Parse(typeof(PmsCommonEnum.ProjectStage), i.ToString()).GetDescription();
                stageNameList.Add(stageTemp);
            }
        }

        private void DrawControl(IList<string> stageNameList, int stage, bool isCurrentStageNormal)
        {
            TRFlow.Controls.Clear();
            TRImage.Controls.Clear();

            PmsCommonEnum.ProjectStage currentStage = (PmsCommonEnum.ProjectStage)System.Enum.Parse(typeof(PmsCommonEnum.ProjectStage), stage.ToString());
            string currentStageName = currentStage.GetDescription();

            //根据当前的stageNameList绘制控件
            int count = 1;
            foreach (string stageName in stageNameList)
            {
                #region 绘制lable
                HtmlTableCell tc = new HtmlTableCell();
                tc.Align = "center";
                tc.NoWrap = true;
                Label lbl = new Label();
                lbl.ID = "lbl" + stageName;

                if (count < stageNameList.Count)
                {
                    lbl.Text = stageName + "-->";
                }
                else
                {
                    lbl.Text = stageName;
                }
                count++;
                tc.Controls.Add(lbl);
                TRFlow.Controls.Add(tc);
                #endregion

                #region 绘制TRImage
                if (currentStageName == stageName)
                {
                    #region 绘制ImgButton
                    HtmlTableCell tcimg = new HtmlTableCell();
                    tcimg.Align = "center";
                    // ImageButton img = new ImageButton();
                    //img.ID = "imgBtnPromote";

                    //如果isStageContains为true，则证明当前的stage是正常的stage,
                    //否则则为其他异常的stage。
                    //SetFlowEnable(stage)检测当前用户是否具备点击按钮的资格。
                    if (isCurrentStageNormal && SetFlowEnable(stage))
                    {
                        ImageButtonStage.ImageUrl = "~/SysFrame/images/right.JPG";
                        ImageButtonStage.Enabled = true;
                        ImageButtonStageIsEnabled = true;
                        // img.CommandArgument = stage.ToString();
                        //img.Attributes.Add("onclick", "CheckPromote();return false;");
                        // img.Click += new ImageClickEventHandler(Button_Click);
                        //img.Attributes.Add("onclick", "Button_Click();return false;");
                        // img.Attributes.Add("OnClientClick", "ProjectProgressImg_OnClientClick();return false;");
                        //  img.OnClientClick = "javascript:ProjectProgressImg_OnClientClick();return false;";

                    }
                    else
                    {
                        ImageButtonStage.ImageUrl = "~/SysFrame/images/right2.JPG";
                        ImageButtonStage.Enabled = false;
                        ImageButtonStageIsEnabled = false;
                    }
                    tcimg.Controls.Add(ImageButtonStage);
                    TRImage.Controls.Add(tcimg);
                    #endregion
                }
                else
                {
                    #region 绘制一个普通的占位符
                    HtmlTableCell tcimg = new HtmlTableCell();
                    tcimg.Align = "center";
                    TRImage.Controls.Add(tcimg);
                    #endregion
                }
                #endregion
            }
        }

        private bool SetFlowEnable(int stageId)
        {
            bool flag = false;

            // string currentRole = string.Empty;
            // BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();

            // baseDataUserBiz.SelectBaseDataUser(LoginName, null)[0].Role;

            // string role = CurrentUser.Role;

            PmsCommonEnum.ProjectStage projectStage
                        = (PmsCommonEnum.ProjectStage)System.Enum.Parse(typeof(PmsCommonEnum.ProjectStage), stageId.ToString());

            switch (projectStage)
            {
                case PmsCommonEnum.ProjectStage.PES:
                    if (CurrentUser.IsProjectPM || CurrentUser.IsOrgPMO)
                    {
                        flag = true;
                    }
                    break;

                case PmsCommonEnum.ProjectStage.AssignMember:
                    if (CurrentUser.IsOrgPMO)
                    {
                        flag = true;
                    }
                    break;

                case PmsCommonEnum.ProjectStage.PIS_STP:
                    if (CurrentUser.IsProjectSD)
                    {
                        flag = true;
                    }
                    break;

                case PmsCommonEnum.ProjectStage.Develop_Test:
                    if (CurrentUser.IsProjectQA)
                    {
                        flag = true;
                    }
                    break;

                case PmsCommonEnum.ProjectStage.WaitingClosed:
                    if (CurrentUser.IsProjectPM || CurrentUser.IsOrgPMO)
                    {
                        flag = true;
                    }
                    break;
                default:
                    break;
            }

            return flag;
        }
        #endregion

        #region Button_Click


        #region 检查信息是否符合推进进度（按钮）
        public bool CheckPromote(PmsHead pmsHead, out string message)
        {
            try
            {
                if (!m_ProjectProgressBiz.CheckDocuments(pmsHead, pmsHead.Stage, out message))
                {
                    return false;
                }

                bool flag = true;
                switch (pmsHead.Stage)//stage为当前状态
                {
                    case (int)PmsCommonEnum.ProjectStage.PES:
                        //Mark by Ename Wang on 20111128 08:56 for Service type has no dute date.
                        if (pmsHead.Type == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                        {
                            break;
                        }
                        if (pmsHead.DueDate < Qisda.DateTime.QDateTime.ChangeNumericToDate("19000101"))
                        {
                            message = "Please input due date!";
                            flag = false;
                        }
                        break;

                    case (int)PmsCommonEnum.ProjectStage.AssignMember:
                        if (string.IsNullOrEmpty(pmsHead.Sd))
                        {
                            message = "Please assign SD!";
                            flag = false;
                        }
                        break;

                    //Abel 注释掉 on 2014-01-22
                    //case (int)PmsCommonEnum.ProjectStage.PIS_STP:

                    //    string designComp = PageProjectsInformation.DesignCompletedPercent;
                    //    if (!string.IsNullOrEmpty(designComp))
                    //    {
                    //        designComp = designComp.Replace("%", "");
                    //        if (float.Parse(designComp) <= 0)
                    //        {
                    //            message = "Please maintain design phase of the information!";
                    //            flag = false;
                    //        }
                    //    }

                    //    break;

                    //case (int)PmsCommonEnum.ProjectStage.Develop_Test:
                    //    flag = DevelopTestCheck(pmsHead.Type, out message);
                    //    break;

                    case (int)PmsCommonEnum.ProjectStage.WaitingClosed:
                        flag = true;
                        break;

                    default:
                        message = "Has no right to promote!";
                        flag = false;
                        break;
                }
                return flag;
            }
            catch (Exception)
            {
                message = "Please Check Promote Condition!";
                return false;
            }

        }


        //Abel 注释掉 on 2014-01-22
        //private bool DevelopTestCheck(string type, out string message)
        //{
        //    message = "";
        //    try
        //    {
        //        IList<string> phaseList = m_ProjectProgressBiz.SelectSdpDetailTemplatePhase(type);

        //        foreach (string phase in phaseList)
        //        {
        //            int intPhase = int.Parse(phase.Trim());
        //            if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Design)
        //            {
        //                string designCompletedPercent = PageProjectsInformation.DesignCompletedPercent;
        //                if (designCompletedPercent != "100.0%")
        //                {
        //                    message = "Please finish the design phase of SDP!";
        //                    return false;
        //                }
        //            }

        //            if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Development)
        //            {
        //                string developmentCompletedPercent = PageProjectsInformation.DevelopmentCompletedPercent;
        //                if (developmentCompletedPercent != "100.0%")
        //                {
        //                    message = "Please finish the development phase of SDP!";
        //                    return false;
        //                }
        //            }

        //            if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Test && type != PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
        //            {

        //                string testCompletedPercent = PageProjectsInformation.TestCompletedPercent;
        //                if (testCompletedPercent != "100.0%")
        //                {
        //                    message = "Please finish the test phase of SDP!";
        //                    return false;
        //                }
        //            }

        //        }

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        #endregion

        protected void ImageButtonStage_Click(object sender, ImageClickEventArgs e)
        {
            #region 获取信息pmsHead
            //从数据库中获取最新数据
            PmsHead pmsHead = null;
            PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
            IList<PmsHead> pmsHeadList = pmsHeadBiz.SelectPmsHead(PmsID, null);
            if (pmsHeadList != null && pmsHeadList.Count > 0)
                pmsHead = pmsHeadList[0];
            else
            {
                Msgbox("无相关资料！");
                return;
            }
            #endregion

            #region 检查是否符合推进条件
            string message;
            if (!CheckPromote(pmsHead, out message))
            {
                Msgbox(message);
                return;
            }
            #endregion

            #region 更新数据库（推进成功）

            //获取当前的type类型对应的pmsFlow
            int intPending = (int)PmsCommonEnum.ProjectStage.Pending;
            int intHardClosed = (int)PmsCommonEnum.ProjectStage.HardClosed;
            int intCancelled = (int)PmsCommonEnum.ProjectStage.Cancelled;
            int intReactive = (int)PmsCommonEnum.ProjectStage.Reactive;
            IList<int> stages = ProjectTypeStageList.Select(t => t)
                .Where(p => (p.Typeid == ProjectType
                    && p.Stageid != intPending
                    && p.Stageid != intHardClosed
                    && p.Stageid != intCancelled
                    && p.Stageid != intReactive))
                    .OrderBy(m => m.Order)
                    .Select(t => t.Stageid)
                    .ToList();

            int oldStage = Stage;
            int newStage = 0;

            for (int i = 0; i < stages.Count; i++)
            {
                if (oldStage == stages[i])
                {
                    newStage = stages[i + 1];
                }
            }

            string strAction = Enum.Parse(typeof(PmsCommonEnum.ProjectStage), oldStage.ToString()).GetDescription();

            #region  SDP confirm

            //Pm or Sd must confirm the SDP  Change only Sd confirm
            //if (IsPm(pmsHead.Pm, CurrentUser.LoginName) || IsSd(pmsHead.Sd, CurrentUser.LoginName))
            if (IsSd(pmsHead.Sd, CurrentUser.LoginName))
            {
                switch (pmsHead.Type)
                {
                    case "CR":
                    case "Study":
                    case "Project":
                        if (strAction == PmsCommonEnum.ProjectStage.PIS_STP.GetDescription())
                        {
                            if (pmsHead.SDPConfirmDate.ToString("yyyy") == "0000" || pmsHead.SDPConfirmDate.ToString("yyyy") == "0001"
                           || pmsHead.SDPConfirmDate.ToString("yyyy") == "1900")
                            {
                                Msgbox("Please Click SDP Confirm First");
                                return;
                            }

                        }
                        break;
                    case "Small CR":

                        if (strAction == PmsCommonEnum.ProjectStage.Develop_Test.GetDescription())
                        {
                            if (pmsHead.SDPConfirmDate.ToString("yyyy") == "0000" || pmsHead.SDPConfirmDate.ToString("yyyy") == "0001"
                           || pmsHead.SDPConfirmDate.ToString("yyyy") == "1900")
                            {
                                Msgbox("Please Click SDP Confirm First");
                                return;
                            }

                        }
                        break;
                    default:
                        break;
                }


            }
            #endregion

            bool blResult = new BasicInformationDetailBiz().UpdateStages(PmsID, LoginName, oldStage, newStage, strAction);

            //从waitingclose到Close,需要更新Head页签closeday事件
            if (oldStage == (int)PmsCommonEnum.ProjectStage.WaitingClosed)
            {
                DateTime closeDate = PmsSysBiz.GetDBDateTime();

                pmsHeadBiz.UpdatePmsHeadCloseDate(PmsID, LoginName, closeDate);

                //修改BasicInformationDetail页面和Service页面上的close时间;
                PageProjectsInformation.CloseDate = m_PmsCommonBiz.FormatDateTime(closeDate.ToString("yyyy-MM-dd").Trim());
                PageProjectsInformation.CloseDateForService = m_PmsCommonBiz.FormatDateTime(closeDate.ToString("yyyy-MM-dd").Trim());
            }


            if (!blResult)
            {
                Msgbox("更新stage数据失败！");
            }

            #endregion

            #region 重绘控件
            //如果推进到release 则点亮BasicInformationDetail.ascx
            if (newStage == (int)PmsCommonEnum.ProjectStage.Release)
            {
                PageProjectsInformation.SetReleaseButtonEnable();
            }

            Stage = newStage;
            InitPmsFlow(Stage, ProjectType);
            #endregion

            #region 发送相关的mail
            LoginName.Replace(".", " ");
            pmsHead.UserName = LoginName;
            new MailBiz().SendPromoteMail(pmsHead, newStage);
            #endregion
        }
        #endregion

        private bool IsPm(string pm, string loginName)
        {
            return IsTheRole(pm, loginName);
        }

        private bool IsSd(string sd, string loginName)
        {
            return IsTheRole(sd, loginName);
        }

        private bool IsTheRole(string role, string loginName)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(role))
            {
                string[] roles = role.Split(';');
                foreach (string roleName in roles)
                {
                    if (roleName == loginName)
                    {
                        result = true;
                    }
                }

            }
            return result;
        }
    }
}