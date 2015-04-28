using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.PMS.Maintain;
using PMS.PMS.UserControls;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;
using Qisda.Web;
using Titan.WebForm;
using WSC.Common;
using Qisda.DateTime;


namespace PMS.PMS.UserControls
{
    public partial class BasicInformationDetail : ProjectsInformationUserControlBase
    {
        protected readonly PmsCRCreatBiz m_PmsCRCreatBiz = new PmsCRCreatBiz();

        #region Define Variable
        //Abel 注释掉 on 2014-01-22
        //public string TotalProgress
        //{
        //    get
        //    {
        //        return txtProgress.Text.Trim();
        //    }
        //    set
        //    {
        //        txtProgress.Text = value;
        //        //  PageRegisterStartupScript("SettxtProgressValue(" + value + ")");
        //    }
        //}

        //public string TotalManpower
        //{
        //    get
        //    {
        //        return txtTotalManpower.Text.Trim();
        //    }
        //    set
        //    {
        //        txtTotalManpower.Text = value;
        //        // PageRegisterStartupScript("SettxtTotalManpowerValue(" + value + ")");
        //    }
        //}

        public string BasicPageTextBoxStageName
        {
            get
            {
                return txtStage.Text.Trim();
            }
            set
            {
                txtStage.Text = value;
            }
        }

        public string CloseDate
        {
            set
            {
                txtCloseDate.Text = value;
                //  PageRegisterStartupScript("SettxtCloseDateValue(" + txtCloseDate.Text + ")");
            }
        }

        public string ActualStartDate
        {
            set
            {
                txtActualStartDate.Text = value;
                //  PageRegisterStartupScript("SettxtActualStartDateValue(" + txtActualStartDate.Text + ")");
            }
            get
            {
                return txtActualStartDate.Text;

            }
        }

        //public DateTime PlanStartDate
        //{
        //    set
        //    {
        //        dateTextBoxPlanStart.Text = m_PmsCommonBiz.FormatDateTime(value.ToString("yyyy-MM-dd").Trim());
        //    }
        //}

        //#region 供AttachFileMaintain.ascx使用
        //public string SystemName
        //{
        //    get
        //    {
        //        return txtSystem.Text.Trim();
        //    }
        //}
        //public string NewVersion
        //{
        //    get
        //    {
        //        return txtNewVision.Text.Trim();
        //    }
        //}
        //#endregion

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ProjectType == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
            {
                return;
            }
            if (!IsPostBack)
            {

                //给textboxSystem绑定点击事件
                // txtSystem.Attributes.Add("onClick", "ShowSystemListPopUp()");

                //给ButtonCancel添加客户端事件
                ButtonCancelTop.OnClientClick = "javascript:window.location='ProjectsInformation.aspx?PmsID=" + PmsID + "'; return false;";
                ButtonCancelUnder.OnClientClick = "javascript:window.location='ProjectsInformation.aspx?PmsID=" + PmsID + "'; return false;";

                //绑定信息到页面并设置页面控件状态
                InitPage();
            }
        }
        #endregion

        #region 页面初始化工作
        private void InitPage()
        {
            //绑定DropDownList
            BindDropDownList();

            PmsHead pmsHead = InitPmsHead;
            if (pmsHead != null)
            {
                //给页面控件赋值
                SetControlValue(pmsHead, CrId);

                Stage = pmsHead.Stage;

                //设置按钮的初始状态
                InitButtonControlStatus(pmsHead.Stage);

                //add by Ename Wang on 20130206
                ShowChangeHistory();
                //end add
            }
            ImageButtonSearchCrId.Enabled = false;
            SetTextBoxReadOnlyCSS();
        }

        #region 绑定DropDownList Site ImpactSite Priority
        private void BindDropDownList()
        {
            try
            {
                SystemListPopUpBiz systemListPopUpBiz = new SystemListPopUpBiz();
                IList<string> resultDomain = systemListPopUpBiz.GetItarmSystemDomain();
                dropdownlistDomain.DataSource = resultDomain;
                dropdownlistDomain.DataBind();
                dropdownlistDomain.Items.Insert(0, "");

                IList<ItarmSystem> resultSite = systemListPopUpBiz.GetItarmSystemSite();
                dropdownlistSite.DataSource = resultSite;
                dropdownlistSite.DataTextField = "Site";
                dropdownlistSite.DataValueField = "Site";
                dropdownlistSite.DataBind();
                dropdownlistSite.Items.Insert(0, "");

                dropdownlistImpactSite.DataSource = resultSite;
                dropdownlistImpactSite.DataTextField = "Site";
                dropdownlistImpactSite.DataValueField = "Site";
                dropdownlistImpactSite.DataBind();
                QWeb.SelectItem(dropdownlistImpactSite, "QCS");

                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysListType = pmsSysBiz.SelectData1ByType("PM", "Type");
                dropdownlistType.DataSource = pmsSysListType;
                dropdownlistType.DataTextField = "Data1";
                dropdownlistType.DataValueField = "Data1";
                dropdownlistType.DataBind();

                IList<PmsSys> pmsSysListPriority = pmsSysBiz.SelectData1ByType("PM", "Priority");
                dropdownlistPriority.DataSource = pmsSysListPriority;
                dropdownlistPriority.DataTextField = "Data1";
                dropdownlistPriority.DataValueField = "Data1";
                dropdownlistPriority.DataBind();
                QWeb.SelectItem(dropdownlistPriority, "Normal");
            }
            catch (Exception ex)
            {
                Msgbox("Bind Type/Domain/Site/ImpactSite/Priority failure!");
            }


        }
        #endregion

        #region Set Control Value
        private void SetControlValue(PmsHead pmsHead, string crId)
        {
            try
            {
                txtCRID.Text = crId;
                txtPMSName.Text = (pmsHead.PmsName ?? string.Empty).Trim();
                SetDropDownListItem(dropdownlistType, (pmsHead.Type ?? string.Empty).Trim());
                if (pmsHead.Type == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                {
                    TrRelatedCrNo.Style["display"] = "block";
                }
                else
                {
                    TrRelatedCrNo.Style["display"] = "none";
                }
                txtRelatedCrNo.Text = (pmsHead.RelatedCrNo ?? string.Empty).Trim();

                ProjectType = (pmsHead.Type ?? string.Empty).Trim();

                txtDescription.Text = (pmsHead.Description ?? string.Empty).Trim();

                //获取枚举类型的Description
                PmsCommonEnum.ProjectStage projectStage = (PmsCommonEnum.ProjectStage)System.Enum.Parse(typeof(PmsCommonEnum.ProjectStage), pmsHead.Stage.ToString());
                txtStage.Text = projectStage.GetDescription();

                txtSystem.Text = (pmsHead.System ?? string.Empty).Trim();
                txtCreateDate.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.CreateDate.ToString("yyyy-MM-dd").Trim());
                txtActualStartDate.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.ActualStartDate.ToString("yyyy-MM-dd").Trim());
                dateTextBoxDueDate.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.DueDate.ToString("yyyy-MM-dd").Trim());
                txtReleaseDate.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.ReleaseDate.ToString("yyyy-MM-dd").Trim());
                txtCloseDate.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.CloseDate.ToString("yyyy-MM-dd").Trim());
                SetDropDownListItem(dropdownlistDomain, (pmsHead.Domain ?? string.Empty).Trim());
                SetDropDownListItem(dropdownlistPriority, (pmsHead.Priority ?? string.Empty).Trim());
                SetDropDownListItem(dropdownlistSite, (pmsHead.Site ?? string.Empty).Trim());
                SetDropDownListItem(dropdownlistImpactSite, (pmsHead.ImpactSite ?? string.Empty).Trim());
                dateTextBoxPlanStart.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.PlanStartDate.ToString("yyyy-MM-dd").Trim());

                //add Progress TotalManpower by  Ename Wang on 20140123
                //Progress和TotalManpower赋值有SdpDetail完成
                txtProgress.Text = (pmsHead.Progress ?? string.Empty).Trim();
                txtTotalManpower.Text = (pmsHead.TotalManpower ?? string.Empty).Trim();
                txtDuration.Text
                        = m_BasicInformationDetailBiz.GetDuration(pmsHead.DueDate, pmsHead.ActualStartDate, pmsHead.PlanStartDate) + " Days";
                //end add

                RadioButtonNeedSTPYes.Checked = pmsHead.NeedSTP == "Y" ? true : false;
                RadioButtonNeedSTPNo.Checked = pmsHead.NeedSTP == "N" ? true : false;
                RadioButtonNeedSTCYes.Checked = pmsHead.NeedSTC == "Y" ? true : false;
                RadioButtonNeedSTCNo.Checked = pmsHead.NeedSTC == "N" ? true : false;
                RadioButtonVB2NetYes.Checked = pmsHead.Category == "Y" ? true : false;
                RadioButtonVB2NetNo.Checked = pmsHead.Category == "N" ? true : false;
                RadioButtonCodeReviewYes.Checked = pmsHead.CodeReview == "Y" ? true : false;
                RadioButtonCodeReviewNo.Checked = pmsHead.CodeReview == "N" ? true : false;
                RadioButtonSelfTestingAuditYes.Checked = pmsHead.SelfTesting == "Y" ? true : false;
                RadioButtonSelfTestingAuditNo.Checked = pmsHead.SelfTesting == "N" ? true : false;
                txtNewVision.Text = (pmsHead.NewVersion ?? string.Empty).Trim();
                txtOldVision.Text = (pmsHead.OldVersion ?? string.Empty).Trim();
                HiddenFieldOldVision.Value = (pmsHead.OldVersion ?? string.Empty).Trim();


                txtPM.Text = (pmsHead.Pm ?? string.Empty).Trim();
                txtSD.Text = (pmsHead.Sd ?? string.Empty).Trim();
                txtQA.Text = (pmsHead.Qa ?? string.Empty).Trim();
                txtSE.Text = (pmsHead.Se ?? string.Empty).Trim();

                //获取Related Item
                GetRelatedItem(crId);
            }
            catch
            {
                Msgbox("Set Control Default Value Failure!");
            }
        }

        private bool GetRelatedItem(string crId)
        {
            try
            {
                if (crId != "")
                {
                    ItarmCrListCoBiz itarmCrListCoBiz = new ItarmCrListCoBiz();

                    //IList<PmsItarmMapping> SelectPmsItarmMappingCoCrNoPmsIdByCrNo(string crId)

                    IList<PmsItarmMapping> pmsItarmMappingList = itarmCrListCoBiz.SelectPmsItarmMappingCoCrNoPmsIdByCrNo(crId);

                    if (pmsItarmMappingList != null && pmsItarmMappingList.Count > 0)
                    {
                        int iCount = 0;
                        Panel panelItem = (Panel)PageProjectsInformation.BasicInformationDetailControl.FindControl("panelItem");

                        foreach (PmsItarmMapping pmsItarmMapping in pmsItarmMappingList)
                        {
                            iCount = iCount + 1;
                            Label labelItem = new Label();
                            labelItem.ID = "labelItem" + (iCount).ToString();
                            labelItem.Text = pmsItarmMapping.CrId.Trim();
                            labelItem.Font.Underline = true;
                            //labelItem.Style.Add("cursor", "hand");
                            labelItem.Attributes.Add("OnClick", "relatedItem('" + pmsItarmMapping.PmsId.Trim() + "')");

                            // project.Attributes.Add("onmouseout", "TaskNameMouseOut(this)");

                            labelItem.Attributes.Add("onmouseover", "TaskNameMouseOver(this)");
                            labelItem.Attributes.Add("onmouseout", "TaskNameMouseOut(this)");
                            panelItem.Controls.Add(labelItem);

                            Label labelBlank = new Label();
                            labelBlank.Text = "  ";
                            panelItem.Controls.Add(labelBlank);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        #endregion

        #region Set Button Status
        private void InitButtonControlStatus(int stage)
        {
            //将按钮灰掉
            SetButtonStatus(false);

            //根据项目进展状态和登录者角色决定点亮哪些按钮
            PmsCommonEnum.ProjectStage projectStage
                = (PmsCommonEnum.ProjectStage)System.Enum.Parse(typeof(PmsCommonEnum.ProjectStage), stage.ToString());

            if (CurrentUser.IsProjectPM || CurrentUser.IsOrgPMO)
            {
                ButtonEdit.Enabled = true;

                if (projectStage == PmsCommonEnum.ProjectStage.Pending
                   || projectStage == PmsCommonEnum.ProjectStage.HardClosed
                   || projectStage == PmsCommonEnum.ProjectStage.Cancelled)
                {
                    ButtonReactive.Enabled = true;
                }
                else if (projectStage != PmsCommonEnum.ProjectStage.WaitingClosed &&
                   projectStage != PmsCommonEnum.ProjectStage.Closed)
                {
                    ButtonPending.Enabled = true;
                    ButtonHardColse.Enabled = true;
                    ButtonCancelled.Enabled = true;
                }
            }
            //Add By Ename Wang on 20111220 18:07
            //让SD在PIS_STP阶段有修改CR Name的权限
            if (CurrentUser.IsProjectSD)
            {
                ButtonEdit.Enabled = true;
            }
            if (CurrentUser.IsProjectQA)
            {
                if (projectStage == PmsCommonEnum.ProjectStage.PIS_STP
                    || projectStage == PmsCommonEnum.ProjectStage.Develop_Test
                    || projectStage == PmsCommonEnum.ProjectStage.Release)
                {
                    ButtonPRelease.Enabled = true;
                }
                //判断是否是在开发阶段，如果是，QA才有权release或者Prelease
                if (projectStage == PmsCommonEnum.ProjectStage.Release)
                {

                    ButtonRelease.Enabled = true;
                }
            }

        }

        private void SetButtonStatus(bool blStatus)
        {
            ButtonEdit.Enabled = blStatus;
            ButtonPending.Enabled = blStatus;
            ButtonHardColse.Enabled = blStatus;
            ButtonCancelled.Enabled = blStatus;
            ButtonReactive.Enabled = blStatus;
            ButtonPRelease.Enabled = blStatus;
            ButtonRelease.Enabled = blStatus;

            ButtonOKTop.Enabled = blStatus;
            ButtonOKUnder.Enabled = blStatus;
            ButtonCancelTop.Enabled = blStatus;
            ButtonCancelUnder.Enabled = blStatus;
        }

        #endregion

        #region SetTextBoxReadOnlyCSS
        private void SetTextBoxReadOnlyCSS()
        {
            //获取BasicInformationDetail自定义控件的子控件。
            //通过页面取获取控件BasicInformationDetail的.controls
            //不能通过Page.Form.Controls方式获取。
            ControlCollection txtBoxs = PageProjectsInformation.BasicInformationDetailControl.Controls;
            foreach (Control txt in txtBoxs)
            {
                if (txt.GetType() == typeof(TextBox))
                {
                    TextBox txtBox = (TextBox)txt;
                    if (txtBox.ReadOnly)
                    {
                        txtBox.CssClass = "UnderLineOnlyTextBox";
                    }
                    else
                    {
                        txtBox.CssClass = "NoLineDisplay";
                    }
                }

                if (txt.GetType() == typeof(DropDownList))
                {
                    DropDownList txtBox = (DropDownList)txt;
                    if (txtBox.Enabled)
                    {
                        txtBox.CssClass = "NoLineDisplay";
                    }
                    else
                    {
                        txtBox.CssClass = "UnderLineOnlyTextBox";
                    }
                }
            }


            if (txtDescription.ReadOnly)
            {
                txtDescription.CssClass = "CrDescriptionReadOnly";
            }
            else
            {
                txtDescription.CssClass = "CrDescriptionNotReadOnly";
            }

        }
        #endregion

        #region ShowChangeHistory
        private void ShowChangeHistory()
        {
            IList<PmsHeadH> listPmsHeadH = new PmsHeadHBiz().SelectPmsHeadHByPmsId(InitPmsHead.PmsId);
            if (listPmsHeadH == null)
            {
                return;
            }

            listPmsHeadH = listPmsHeadH.Where(t => !string.IsNullOrEmpty(t.ReasonType))
                                           .OrderBy(t => t.Seq)
                                           .ToList();
            if (listPmsHeadH.Count > 0)
            {
                PmsHeadH pmsHeadH = listPmsHeadH[listPmsHeadH.Count - 1];
                if (pmsHeadH != null)
                {
                    ImageRedFlag.Visible = true;

                    string message = string.Empty;

                    message = "Due date has been changed: "
                            + "from " + QDateTime.FormatDate(pmsHeadH.DueDate, "yyyy-MM-dd") + " "
                            + "to " + QDateTime.FormatDate(pmsHeadH.DueDateNew, "yyyy-MM-dd") + " "
                            + "for " + pmsHeadH.ReasonType + " (" + pmsHeadH.Reason + ") "
                            + "on " + QDateTime.FormatDate(pmsHeadH.Maintaindate, "yyyy-MM-dd") + " "
                            + "by " + pmsHeadH.Maintainuser;

                    HiddenFieldChangeHistory.Value = message;
                }
            }
        }
        #endregion

        #endregion

        #region ButtonEdit事件
        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            SetControlStatusForEdit();
        }

        private void SetControlStatusForEdit()
        {
            // DueDate换成textbox,点击Edit，注册js,选择日期
            //System在后台实现onblur



            #region Set Control Status
            ButtonEdit.Enabled = false;
            ButtonPending.Enabled = false;
            ButtonHardColse.Enabled = false;
            ButtonCancelled.Enabled = false;
            ButtonReactive.Enabled = false;
            ButtonPRelease.Enabled = false;
            ButtonRelease.Enabled = false;

            ButtonOKTop.Visible = true;
            ButtonOKUnder.Visible = true;
            ButtonCancelTop.Visible = true;
            ButtonCancelUnder.Visible = true;

            ButtonOKTop.Enabled = true;
            ButtonOKUnder.Enabled = true;
            ButtonCancelTop.Enabled = true;
            ButtonCancelUnder.Enabled = true;

            //Add By Ename Wang on 20111220 18:23
            //让SD在PIS_STP阶段有修改CR Name的权限
            if (CurrentUser.IsProjectSD)
            {
                txtPMSName.ReadOnly = false;
                txtPMSName.CssClass = "NoLineDisplay";
            }

            if (CurrentUser.IsProjectPM || CurrentUser.IsOrgPMO)
            {
                if (txtCRID.Text.Trim().StartsWith("T"))
                {
                    txtCRID.ReadOnly = false;
                    ImageButtonSearchCrId.Enabled = true;
                }

                txtPMSName.ReadOnly = false;
                txtDescription.ReadOnly = false;

                txtRelatedCrNo.ReadOnly = false;
                txtRelatedCrNo.CssClass = "NoLineDisplay";

                txtSystem.ReadOnly = false;
                txtSystem.Attributes.Add("onblur", "txtSystem_onblur();");
                txtSystem.Attributes.Add("onClick", "ShowSystemListPopUp()");

                dateTextBoxDueDate.ReadOnly = false;
                dateTextBoxPlanStart.ReadOnly = false;
                dateTextBoxDueDate.Attributes.Add("onkeypress", "return false;");
                dateTextBoxPlanStart.Attributes.Add("onkeypress", "return false;");
                RenderScript.RenderCalendarScript(dateTextBoxDueDate, string.Empty);
                RenderScript.RenderCalendarScript(dateTextBoxPlanStart, string.Empty);

                // add by Ename Wang on 20130204
                PanelDueDateChangeReason.Visible = true;
                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysListType = pmsSysBiz.SelectData1Data2ByType("PM", "ReasonType");
                ddlDueDateChangeReasonType.DataSource = pmsSysListType;
                ddlDueDateChangeReasonType.DataTextField = "Data1Data2";
                ddlDueDateChangeReasonType.DataValueField = "Data1";
                ddlDueDateChangeReasonType.DataBind();
                ddlDueDateChangeReasonType.Items.Insert(0, "");
                // end add

                dropdownlistType.Enabled = true;
                dropdownlistDomain.Enabled = true;

                dropdownlistPriority.Enabled = true;
                dropdownlistSite.Enabled = true;
                dropdownlistImpactSite.Enabled = true;

                dateTextBoxPlanStart.Enabled = true;
                dateTextBoxPlanStart.ReadOnly = false;

                RadioButtonNeedSTPYes.Enabled = true;
                RadioButtonNeedSTPNo.Enabled = true;
                RadioButtonNeedSTCYes.Enabled = true;
                RadioButtonNeedSTCNo.Enabled = true;
                RadioButtonVB2NetYes.Enabled = true;
                RadioButtonVB2NetNo.Enabled = true;
                RadioButtonCodeReviewYes.Enabled = true;
                RadioButtonCodeReviewNo.Enabled = true;
                RadioButtonSelfTestingAuditYes.Enabled = true;
                RadioButtonSelfTestingAuditNo.Enabled = true;
                txtNewVision.ReadOnly = false;
                txtOldVision.ReadOnly = false;

                txtPM.ReadOnly = false;
                if (CurrentUser.IsOrgPMO)
                {
                    txtSD.ReadOnly = false;
                    txtSE.ReadOnly = false;
                    txtQA.ReadOnly = false;
                }
                SetTextBoxReadOnlyCSS();
            }

            #endregion

            #region  设置状态进度条上的按钮状态不可用
            PageProjectsInformation.ChangeImageButtonStatus(false);
            #endregion
        }
        #endregion

        #region ButtonOK事件
        protected void ButtonOKTop_Click(object sender, EventArgs e)
        {
            EventButtonOK();
        }

        protected void ButtonOKUnder_Click(object sender, EventArgs e)
        {
            EventButtonOK();
        }

        #region EventButtonOK
        private void EventButtonOK()
        {
            try
            {
                #region 检查输入的信息是否符合要求
                if (!CheckControlValueForButtonOK())
                {
                    PageRegisterStartupScript("txtSystem_onblur()");
                    return;
                }
                #endregion

                #region 检查type变更是否合法
                if (!CheckTypeChang())
                {
                    PageRegisterStartupScript("txtSystem_onblur()");
                    return;
                }
                #endregion

                #region 检查版本是否重复
                if (!CheckNewVersionDuplicate())
                {
                    Msgbox("the new version is duplicate,please contact pms system admin !");
                    return;
                }
                #endregion

                #region 记录CR信息更改历史
                if (!RecordChangeHistory())
                {
                    return;
                }
                #endregion

                #region 更新数据库相关信息
                //获取用以更新数据库的信息
                PmsHead pmsHead;
                if (!GetControlValue(out pmsHead))
                {
                    PageRegisterStartupScript("txtSystem_onblur()");
                    return;
                }

                //更新pms_itarm_mapping表
                //更新pmshead表
                //更新pmsflow表  // add by Ename Wang on 20120321 
                //更新itarm_cr_list
                //更新pms_system_version
                if (!m_BasicInformationDetailBiz.Save(pmsHead, CrId, txtCRID.Text.Trim(), LoginName, ProjectType, dropdownlistType.SelectedValue.Trim()))
                {
                    Msgbox("Save Failure!");
                    PageRegisterStartupScript("txtSystem_onblur()");
                    return;
                }

                //add by Ename Wang on 20120511

                if (!UpdateRlnsCrId(CrId, txtCRID.Text.Trim()))
                {
                    Msgbox("Update RLNS System Failure!");
                    PageRegisterStartupScript("txtSystem_onblur()");
                    return;
                }
                //end add 

                CrId = txtCRID.Text.Trim();

                txtDuration.Text
                     = m_BasicInformationDetailBiz.GetDuration(pmsHead.DueDate, pmsHead.ActualStartDate, pmsHead.PlanStartDate) + " Days";

                #endregion

                #region Send Mail
                //if Due Date PM QA SD SE Changed, then Send Mail 
                bool InformationChanged = false;
                if (Server.HtmlDecode(dropdownlistType.SelectedValue).Trim()
                    != PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                {
                    string dueDate = InitPmsHead.DueDate.ToString("yyyy-MM-dd");
                    string pm = InitPmsHead.Pm;
                    string qa = InitPmsHead.Qa;
                    string sd = InitPmsHead.Sd;
                    string se = InitPmsHead.Se;

                    InformationChanged = CheckIsChanged(dueDate, pm, qa, sd, se);
                }

                if (InformationChanged)
                {
                    // Information changed,then send mail
                    MailBiz mailBiz = new MailBiz();
                    bool result = mailBiz.SendInformationChangedMail(InitPmsHead, pmsHead, LoginName);
                }
                #endregion

                #region 设置页面的状态

                InitButtonControlStatus(Stage);
                SetControlStatusAfterOKClick();

                ButtonOKTop.Visible = false;
                ButtonOKUnder.Visible = false;
                ButtonCancelTop.Visible = false;
                ButtonCancelUnder.Visible = false;
                ImageButtonSearchCrId.Enabled = false;

                //复原状态进度条上的按钮状态
                PageProjectsInformation.ChangeImageButtonStatus(PageProjectsInformation.ImageButtonStageIsEnabled);
                PageRegisterStartupScript("window.location=window.location");
                #endregion
            }
            catch (Exception)
            {
                Msgbox("Save Failure!");
                PageRegisterStartupScript("txtSystem_onblur()");
            }
        }
        #endregion

        #region RecordChangeHistory()
        private bool RecordChangeHistory()
        {
            bool isDueDateChanged = IsDueDateChanged();
            bool isOtherInformationChanged = IsOtherInformationChanged();
            bool isDueDateChangeReasonUpdate = IsDueDateChangeReasonUpdate();

            if (isDueDateChanged)
            {
                if (string.IsNullOrEmpty(ddlDueDateChangeReasonType.SelectedValue.Trim()))
                {
                    Msgbox("Please select due date change reason type!");
                    return false;
                }
                if (string.IsNullOrEmpty(txtDueDateChangeReason.Text.Trim()))
                {
                    Msgbox("Please input due date change reason!");
                    return false;
                }
                InsertChangedHistory(isDueDateChanged);
            }
            else
            {
                if (isDueDateChangeReasonUpdate)
                {
                    UpdateChangedHistory(isDueDateChanged, isDueDateChangeReasonUpdate);
                }

                if (isOtherInformationChanged)
                {
                    InsertChangedHistory(isDueDateChanged);
                }
            }
            return true;
        }

        #region GetPmsHeadH
        private PmsHeadH GetPmsHeadH()
        {
            string crId = CrId;
            string system = InitPmsHead.System;
            string type = InitPmsHead.Type;
            string pm = InitPmsHead.Pm;
            string sd = InitPmsHead.Sd;
            string se = InitPmsHead.Se;
            string qa = InitPmsHead.Qa;

            PmsHeadH pmsHeadH = new PmsHeadH();
            pmsHeadH.PmsId = InitPmsHead.PmsId;

            if (!string.IsNullOrEmpty(crId))
            {
                if (crId.Trim() != Server.HtmlDecode(txtCRID.Text).Trim())
                {
                    pmsHeadH.CrId = crId;
                    pmsHeadH.CrIdNew = Server.HtmlDecode(txtCRID.Text).Trim();
                }
            }

            if (!string.IsNullOrEmpty(system))
            {
                if (system.Trim() != Server.HtmlDecode(txtSystem.Text).Trim())
                {
                    pmsHeadH.System = system;
                    pmsHeadH.SystemNew = Server.HtmlDecode(txtSystem.Text).Trim();
                }
            }

            string dueDateInit = QDateTime.FormatDate(InitPmsHead.DueDate, "yyyy-MM-dd");
            if (!dueDateInit.Equals("1900-01-01") && !dueDateInit.Equals("0001-01-01") && !dueDateInit.Equals("0000-00-00")
            && !dueDateInit.Equals("1899-12-30") && !dueDateInit.Equals("01-01-01"))
            {
                if (dueDateInit.Trim() != Server.HtmlDecode(dateTextBoxDueDate.Text).Trim())
                {
                    pmsHeadH.DueDate = InitPmsHead.DueDate;
                    pmsHeadH.DueDateNew = DateTime.Parse(dateTextBoxDueDate.Text.Trim());
                }
            }

            if (!string.IsNullOrEmpty(type))
            {
                if (type.Trim() != Server.HtmlDecode(dropdownlistType.SelectedValue).Trim())
                {
                    pmsHeadH.Type = type;
                    pmsHeadH.TypeNew = Server.HtmlDecode(dropdownlistType.SelectedValue).Trim();
                }
            }

            if (pm.Trim() != Server.HtmlDecode(txtPM.Text).Trim())
            {
                pmsHeadH.Pm = pm;
                pmsHeadH.PmNew = Server.HtmlDecode(txtPM.Text).Trim();
            }

            if (qa.Trim() != Server.HtmlDecode(txtQA.Text).Trim())
            {
                pmsHeadH.Qa = qa;
                pmsHeadH.QaNew = Server.HtmlDecode(txtQA.Text).Trim();
            }

            if (sd.Trim() != Server.HtmlDecode(txtSD.Text).Trim())
            {
                pmsHeadH.Sd = sd;
                pmsHeadH.SdNew = Server.HtmlDecode(txtSD.Text).Trim();
            }

            if (se.Trim() != Server.HtmlDecode(txtSE.Text).Trim())
            {
                pmsHeadH.Se = se;
                pmsHeadH.SeNew = Server.HtmlDecode(txtSE.Text).Trim();
            }


            if (!string.IsNullOrEmpty(ddlDueDateChangeReasonType.SelectedValue.Trim()))
            {
                pmsHeadH.ReasonType = ddlDueDateChangeReasonType.SelectedValue.Trim();
            }

            if (!string.IsNullOrEmpty(txtDueDateChangeReason.Text.Trim()))
            {
                pmsHeadH.Reason = Server.HtmlDecode(txtDueDateChangeReason.Text.Trim());
            }

            pmsHeadH.Maintainuser = LoginName;
            pmsHeadH.Maintaindate = PmsSysBiz.GetDBDateTime();

            return pmsHeadH;
        }
        #endregion

        #region IsDueDateChanged
        private bool IsDueDateChanged()
        {
            bool result = false;
            string dueDateInit = QDateTime.FormatDate(InitPmsHead.DueDate, "yyyy-MM-dd");
            if (!dueDateInit.Equals("1900-01-01") && !dueDateInit.Equals("0001-01-01") && !dueDateInit.Equals("0000-00-00")
            && !dueDateInit.Equals("1899-12-30") && !dueDateInit.Equals("01-01-01"))
            {
                if (dueDateInit.Trim() != Server.HtmlDecode(dateTextBoxDueDate.Text).Trim())
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region IsOtherInformationChanged
        private bool IsOtherInformationChanged()
        {
            bool result = false;

            string crId = CrId;
            string system = InitPmsHead.System;
            string type = InitPmsHead.Type;
            string pm = InitPmsHead.Pm;
            string sd = InitPmsHead.Sd;
            string se = InitPmsHead.Se;
            string qa = InitPmsHead.Qa;

            if (!string.IsNullOrEmpty(crId))
            {
                if (crId.Trim() != Server.HtmlDecode(txtCRID.Text).Trim())
                {
                    result = true;
                }
            }

            if (!string.IsNullOrEmpty(system))
            {
                if (system.Trim() != Server.HtmlDecode(txtSystem.Text).Trim())
                {
                    result = true;
                }
            }

            if (!string.IsNullOrEmpty(type))
            {
                if (type.Trim() != Server.HtmlDecode(dropdownlistType.SelectedValue).Trim())
                {
                    result = true;
                }
            }


            if (pm.Trim() != Server.HtmlDecode(txtPM.Text).Trim())
            {
                result = true;
            }



            if (qa.Trim() != Server.HtmlDecode(txtQA.Text).Trim())
            {
                result = true;
            }



            if (sd.Trim() != Server.HtmlDecode(txtSD.Text).Trim())
            {
                result = true;
            }



            if (se.Trim() != Server.HtmlDecode(txtSE.Text).Trim())
            {
                result = true;
            }


            return result;
        }
        #endregion

        #region isDueDateChangeReasonUpdate
        private bool IsDueDateChangeReasonUpdate()
        {
            bool result = false;

            if (!string.IsNullOrEmpty(ddlDueDateChangeReasonType.SelectedValue.Trim()))
            {
                result = true;
            }

            if (!string.IsNullOrEmpty(txtDueDateChangeReason.Text.Trim()))
            {
                result = true;
            }

            return result;
        }
        #endregion

        #region InsertChangedHistory
        private void InsertChangedHistory(bool isDueDateChanged)
        {
            IList<PmsHeadH> listPmsHeadH = new PmsHeadHBiz().SelectPmsHeadHByPmsId(InitPmsHead.PmsId);
            IList<int> listSeq = listPmsHeadH.Select(t => t.Seq).ToList();
            int insertSeq = 1;
            if (listSeq.Count > 0)
            {
                insertSeq = listSeq.Max() + 1;
            }

            PmsHeadH pmsHeadH = GetPmsHeadH();
            if (!isDueDateChanged)
            {
                pmsHeadH.ReasonType = "";
                pmsHeadH.Reason = "";
            }
            pmsHeadH.Seq = insertSeq;

            if (!new PmsHeadHBiz().InsertPmsHeadH(pmsHeadH))
            {
                Msgbox("Insert change history failed!");
                return;
            };
        }
        #endregion

        #region UpdateChangedHistory
        private void UpdateChangedHistory(bool isDueDateChanged, bool isDueDateChangeReasonUpdate)
        {
            IList<PmsHeadH> listPmsHeadH = new PmsHeadHBiz().SelectPmsHeadHByPmsId(InitPmsHead.PmsId);

            listPmsHeadH = listPmsHeadH.Where(t => !string.IsNullOrEmpty(t.ReasonType))
                                           .OrderBy(t => t.Seq)
                                           .ToList();

            IList<int> listSeq = listPmsHeadH.Select(t => t.Seq).ToList();

            if (listSeq.Count == 0)
            {
                return;
            }

            PmsHeadH pmsHeadH = GetPmsHeadH();
            pmsHeadH.Seq = listSeq.Max();

            if (!new PmsHeadHBiz().UpdatePmsHeadH(pmsHeadH))
            {
                Msgbox("Update change history failed!");
                return;
            };
        }
        #endregion

        #endregion

        #region UpdateRlnsCrId
        // add by Ename Wang on 20120511
        private bool UpdateRlnsCrId(string oldCrId, string newCrId)
        {
            if (oldCrId == newCrId)
            {
                return true;
            }
            else
            {
                try
                {
                    RLNWebService.RLNSWebService RLNService = new RLNWebService.RLNSWebService();
                    RLNService.Url = ConfigurationManager.AppSettings["RLSWebService"].ToString();
                    string resultXML = RLNService.UpdateTempCrIdToCrId(oldCrId, newCrId);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        // end add
        #endregion

        #region CheckIsChanged
        private bool CheckIsChanged(string dueDate, string pm, string qa, string sd, string se)
        {
            bool result = false;
            if (!dueDate.Equals("1900-01-01") && !dueDate.Equals("0001-01-01") && !dueDate.Equals("0000-00-00")
                && !dueDate.Equals("1899-12-30") && !dueDate.Equals("01-01-01"))
            {
                if (!string.IsNullOrEmpty(dueDate))
                {
                    if (dueDate.Trim() != Server.HtmlDecode(dateTextBoxDueDate.Text).Trim())
                    {
                        result = true;
                    }
                }
            }

            if (!string.IsNullOrEmpty(pm))
            {
                if (pm.Trim() != Server.HtmlDecode(txtPM.Text).Trim())
                {
                    result = true;
                }
            }
            if (!string.IsNullOrEmpty(qa))
            {
                if (qa.Trim() != Server.HtmlDecode(txtQA.Text).Trim())
                {
                    result = true;
                }
            }

            if (!string.IsNullOrEmpty(sd))
            {
                if (sd.Trim() != Server.HtmlDecode(txtSD.Text).Trim())
                {
                    result = true;
                }
            }

            if (!string.IsNullOrEmpty(se))
            {
                if (se.Trim() != Server.HtmlDecode(txtSE.Text).Trim())
                {
                    result = true;
                }
            }

            return result;
        }
        #endregion

        #region 检查需要填写的信息是否符合要求
        private bool CheckControlValueForButtonOK()
        {
            try
            {
                //PM和PMO需要填写的共同信息。
                if (!CheckControlTextBoxIsNull(txtCRID, "Please input CR No!"))
                    return false;
                if (!CheckControlTextBoxIsNull(txtPMSName, "Please input CR Name!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistType, "Please select Type!"))
                    return false;
                if (!CheckControlTextBoxIsNull(txtStage, "Please input Stage!"))
                    return false;
                if (!CheckControlTextBoxIsNull(dateTextBoxDueDate, "Please select Due Date!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistDomain, "Please select Domain!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistPriority, "Please select Priority!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistSite, "Please select Site!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistImpactSite, "Please select Impact Site!"))
                    return false;
                if (!CheckControlTextBoxIsNull(dateTextBoxPlanStart, "Please input Plan Start Date!"))
                    return false;
                if (!CheckControlTextBoxIsNull(txtSystem, "Please input System!"))
                    return false;

                string message;
                string falg;

                if (!m_PmsCRCreatBiz.CheckVersion(HiddenFieldOldVision.Value.Trim(), txtNewVision.Text.Trim(), out message, out falg))
                {
                    Msgbox(message);
                    if (falg == "old")
                        txtOldVision.Focus();
                    else
                        txtNewVision.Focus();
                    return false;
                }

                if (!CheckControlTextBoxIsNull(txtPM, "Please input PM!"))
                    return false;
                else
                {
                    string[] pms = txtPM.Text.Trim().Split(';');
                    foreach (string pm in pms)
                    {
                        if (pm != string.Empty && (!m_PmsCRCreatBiz.CheckUser(pm)))
                        {
                            Msgbox(pm + " does not exist!");
                            txtPM.Focus();
                            return false;
                        }
                    }

                }

                //if (DateTime.Parse(dateTextBoxPlanStart.Text.Trim()) < PmsSysBiz.GetDBDateTime())
                //{
                //    Msgbox("PlanStartDate must larger than CurrentDate!");
                //    dateTextBoxPlanStart.Focus();
                //    return false;
                //}
                //if (DateTime.Parse(dateTextBoxDueDate.Text.Trim()) < PmsSysBiz.GetDBDateTime())
                //{
                //    Msgbox("DueDate must larger than CurrentDate!");
                //    dateTextBoxDueDate.Focus();
                //    return false;
                //}
                if (DateTime.Parse(dateTextBoxDueDate.Text.Trim()) < DateTime.Parse(dateTextBoxPlanStart.Text.Trim()))
                {
                    Msgbox("DueDate must larger than PlanStartDate!");
                    dateTextBoxDueDate.Focus();
                    return false;
                }

                //PMO在AssignMember阶段需要填写的信息
                if (CurrentUser.IsOrgPMO && InitPmsHead.Stage == 2)
                {
                    if (!CheckControlTextBoxIsNull(txtSD, "Please input SD!"))
                        return false;
                    else
                    {
                        string[] sds = txtSD.Text.Trim().Split(';');
                        foreach (string sd in sds)
                        {
                            if (sd != string.Empty && (!m_PmsCRCreatBiz.CheckUser(sd)))
                            {
                                Msgbox(sd + " does not exist");
                                txtSD.Focus();
                                return false;
                            }
                        }

                    }
                    if (!CheckControlTextBoxIsNull(txtSE, "Please input SE!"))
                        return false;
                    else
                    {
                        string[] ses = txtSE.Text.Trim().Split(';');
                        foreach (string se in ses)
                        {
                            if (se != string.Empty && (!m_PmsCRCreatBiz.CheckUser(se)))
                            {
                                Msgbox(se + " does not exist");
                                txtSE.Focus();
                                return false;
                            }
                        }

                    }
                    if (!CheckControlTextBoxIsNull(txtQA, "Please input QA!"))
                        return false;
                    else
                    {
                        string[] qas = txtQA.Text.Trim().Split(';');
                        foreach (string qa in qas)
                        {
                            if (qa != string.Empty && (!m_PmsCRCreatBiz.CheckUser(qa)))
                            {
                                Msgbox(qa + " does not exist");
                                txtQA.Focus();
                                return false;
                            }
                        }

                    }
                }

                //检查该CRID是否存在
                if (!m_BasicInformationDetailBiz.CheckCrIdIsExist(CrId, txtCRID.Text.Trim()) && txtCRID.Text.Trim().Substring(0, 2) == "CR")
                {
                    Msgbox("The CR No does not exist!");
                    txtCRID.Focus();
                    return false;
                }

                return true;

            }
            catch (Exception)
            {
                Msgbox("Fill in the wrong data.Please check the data you have input!");
                return false;
            }
        }
        #endregion

        #region GetControlValue
        private bool GetControlValue(out PmsHead pmsHead)
        {
            pmsHead = new PmsHead();
            try
            {
                // pmsHead.CrId = txtCRID.Text.Trim();
                pmsHead.PmsId = PmsID;
                pmsHead.PmsName = txtPMSName.Text.Trim();
                pmsHead.Type = dropdownlistType.SelectedValue;

                pmsHead.RelatedCrNo = txtRelatedCrNo.Text.Trim();

                pmsHead.Description = txtDescription.Text.Trim();
                pmsHead.Stage = Stage;
                pmsHead.System = txtSystem.Text.Trim();
                //  pmsHead.CreateDate=
                if (!string.IsNullOrEmpty(txtCreateDate.Text.Trim()))
                {
                    pmsHead.CreateDate = DateTime.Parse(txtCreateDate.Text.Trim());
                }

                if (!string.IsNullOrEmpty(txtActualStartDate.Text.Trim()))
                {
                    pmsHead.ActualStartDate = DateTime.Parse(txtActualStartDate.Text.Trim());
                }

                if (!string.IsNullOrEmpty(dateTextBoxDueDate.Text.Trim()))
                {
                    pmsHead.DueDate = DateTime.Parse(dateTextBoxDueDate.Text.Trim());

                }
                if (!string.IsNullOrEmpty(txtReleaseDate.Text.Trim()))
                {
                    pmsHead.ReleaseDate = DateTime.Parse(txtReleaseDate.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtCloseDate.Text.Trim()))
                {
                    pmsHead.CloseDate = DateTime.Parse(txtCloseDate.Text.Trim());
                }
                if (!string.IsNullOrEmpty(dateTextBoxPlanStart.Text.Trim()))
                {
                    pmsHead.PlanStartDate = DateTime.Parse(dateTextBoxPlanStart.Text.Trim());
                }
                pmsHead.Domain = dropdownlistDomain.SelectedValue;
                pmsHead.Priority = dropdownlistPriority.SelectedValue;
                pmsHead.Site = dropdownlistSite.SelectedValue;
                pmsHead.ImpactSite = dropdownlistImpactSite.SelectedValue;
                pmsHead.Progress = txtProgress.Text.Trim();
                pmsHead.TotalManpower = txtTotalManpower.Text.Trim();
                // pmsHead  Duration 
                pmsHead.NeedSTP = RadioButtonNeedSTPYes.Checked ? "Y" : "N";
                pmsHead.NeedSTC = RadioButtonNeedSTCYes.Checked ? "Y" : "N";
                pmsHead.Category = RadioButtonVB2NetYes.Checked ? "Y" : "N";
                pmsHead.CodeReview = RadioButtonCodeReviewYes.Checked ? "Y" : "N";
                pmsHead.SelfTesting = RadioButtonSelfTestingAuditYes.Checked ? "Y" : "N";
                // pmsHead.OldVersion = txtOldVision.Text.Trim();
                pmsHead.OldVersion = HiddenFieldOldVision.Value.Trim().Replace("'", "");

                pmsHead.NewVersion = txtNewVision.Text.Trim();
                pmsHead.Pm = txtPM.Text.Trim();
                pmsHead.Sd = txtSD.Text.Trim();
                pmsHead.Qa = txtQA.Text.Trim();
                pmsHead.Se = txtSE.Text.Trim();
                pmsHead.MaintainDate = PmsSysBiz.GetDBDateTime();
                pmsHead.MaintainUser = LoginName;
                return true;

            }
            catch (Exception exception)
            {

                Msgbox("保存数据失败！\r\n" + exception.Message);
                return false;
            }
        }
        #endregion

        #region SetControlStatusAfterOKClick
        private void SetControlStatusAfterOKClick()
        {
            txtCRID.ReadOnly = true;
            txtPMSName.ReadOnly = true;
            txtDescription.ReadOnly = true;

            txtRelatedCrNo.ReadOnly = true;
            txtRelatedCrNo.CssClass = "UnderLineOnlyTextBox";

            txtSystem.ReadOnly = true;
            txtSystem.Enabled = false;

            dateTextBoxDueDate.Enabled = false;
            dateTextBoxDueDate.ReadOnly = true;


            dropdownlistType.Enabled = false;
            dropdownlistDomain.Enabled = false;

            dropdownlistPriority.Enabled = false;
            dropdownlistSite.Enabled = false;
            dropdownlistImpactSite.Enabled = false;

            dateTextBoxPlanStart.Enabled = false;
            dateTextBoxPlanStart.ReadOnly = true;

            RadioButtonNeedSTPYes.Enabled = false;
            RadioButtonNeedSTPNo.Enabled = false;
            RadioButtonNeedSTCYes.Enabled = false;
            RadioButtonNeedSTCNo.Enabled = false;
            RadioButtonVB2NetYes.Enabled = false;
            RadioButtonVB2NetNo.Enabled = false;

            txtNewVision.ReadOnly = true;
            txtOldVision.ReadOnly = true;

            txtPM.ReadOnly = true;
            if (CurrentUser.IsOrgPMO)
            {
                txtSD.ReadOnly = true;
                txtSE.ReadOnly = true;
                txtQA.ReadOnly = true;
            }

            SetTextBoxReadOnlyCSS();
        }
        #endregion

        #region Type改变是否合法
        private bool CheckTypeChang()
        {
            string newType = dropdownlistType.SelectedValue.Trim();

            //如果type没有变化，则认为是对的。
            if (newType == ProjectType)
            {
                return true;
            }


            //如果type发生了变化，则检查是否可以更改。
            try
            {
                //不能从大的TYPE转成小的。
                if (newType == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription()
                    || newType == PmsCommonEnum.ProjectTypeFlowId.Study.GetDescription())
                {
                    Msgbox("Can not change Type from " + ProjectType + " to " + newType + "!");
                    return false;
                }

                IList<PmsFlowTemplate> newTypeStagePmsFlowTemplate
                    = ProjectTypeStageList.Select(t => t).Where(p => p.Typeid == newType).OrderBy(m => m.Order).ToList();

                if (newTypeStagePmsFlowTemplate.Select(t => t.Stageid).Contains(Stage))
                {
                    PageProjectsInformation.ProjectProgressInitPmsFlow(Stage, newType);

                }
                else
                {
                    //如果是CR转Samll CR， 需要注意若stage 是PIS阶段，则则转到Small CR 后变为Develop
                    if (newType == PmsCommonEnum.ProjectTypeFlowId.SmallCR.GetDescription()
                        && Stage == (int)PmsCommonEnum.ProjectStage.PIS_STP)
                    {
                        PageProjectsInformation.ProjectProgressInitPmsFlow((int)PmsCommonEnum.ProjectStage.Develop_Test, newType);

                    }
                    else
                    {
                        Msgbox("Can not change Type from " + ProjectType + " to " + newType + "!");
                        return false;

                    }

                }
                //当type不为Service时，证明没有txtRelatedCrNo的text,该值应该为空。
                if (newType != PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                {
                    txtRelatedCrNo.Text = "";
                    TrRelatedCrNo.Style["display"] = "none";
                    txtRelatedCrNo.CssClass = "UnderLineOnlyTextBox";
                }
                else
                {
                    TrRelatedCrNo.Style["display"] = "block";
                    txtRelatedCrNo.CssClass = "NoLineDisplay";
                }
                return true;
            }
            catch (Exception)
            {
                Msgbox("Change Type from " + ProjectType + " to " + newType + " failure!");
                return false;
            }

        }
        #endregion

        #region 检查版本是否重复
        private bool CheckNewVersionDuplicate()
        {
            PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
            PmsHead pmsHead = new PmsHead();
            pmsHead.PmsId = PmsID;
            pmsHead.System = this.txtSystem.Text.Trim();
            pmsHead.Domain = this.dropdownlistDomain.SelectedValue.Trim();
            pmsHead.Site = this.dropdownlistSite.SelectedValue.Trim();
            pmsHead.NewVersion = this.txtNewVision.Text.Trim();

            IList<PmsHead> resultHeadList = pmsHeadBiz.SelectPmsHeadForCheckNewVersion(pmsHead);
            if (resultHeadList.Count > 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #endregion

        #region ButtonPending事件
        protected void ButtonPending_Click(object sender, EventArgs e)
        {
            int newStage = (int)PmsCommonEnum.ProjectStage.Pending;
            AbnormalStageButtonEvent(PmsID, LoginName, Stage, newStage);
        }
        #endregion

        #region ButtonHardColse事件
        protected void ButtonHardColse_Click(object sender, EventArgs e)
        {
            int newStage = (int)PmsCommonEnum.ProjectStage.HardClosed;
            AbnormalStageButtonEvent(PmsID, LoginName, Stage, newStage);
        }
        #endregion

        #region ButtonCancelled事件
        protected void ButtonCancelled_Click(object sender, EventArgs e)
        {
            int newStage = (int)PmsCommonEnum.ProjectStage.Cancelled;
            AbnormalStageButtonEvent(PmsID, LoginName, Stage, newStage);
        }
        #endregion

        #region 异常按钮点击后触发的事件ButtonPending，ButtonHardColse，ButtonCancelled
        private void AbnormalStageButtonEvent(string pmsId, string loginName, int oldStage, int newStage)
        {
            //获取枚举类型的Description
            PmsCommonEnum.ProjectStage projectStage = (PmsCommonEnum.ProjectStage)System.Enum.Parse(typeof(PmsCommonEnum.ProjectStage), newStage.ToString());
            string stageDescription = projectStage.GetDescription();
            try
            {
                bool blResult = m_BasicInformationDetailBiz.UpdateStages(pmsId, loginName, oldStage, newStage, stageDescription);

                if (blResult)
                {
                    txtStage.Text = stageDescription;
                    Stage = newStage;

                    InitButtonControlStatus(newStage);

                    //重绘ProjectProgress控件
                    PageProjectsInformation.ProjectProgressInitPmsFlow(newStage, dropdownlistType.SelectedValue.Trim());

                    IList<PmsHead> pmsHeadList = new PmsHeadBiz().SelectPmsHead(PmsID, null);
                    if (pmsHeadList != null && pmsHeadList.Count > 0)
                    {
                        PmsHead pmsHead = pmsHeadList[0];
                        try
                        {
                            LoginName.Replace(".", " ");
                            pmsHead.UserName = LoginName;
                            new MailBiz().SendPromoteMail(pmsHead, newStage);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    Msgbox(stageDescription + " Successfully!");
                }
                else
                {
                    Msgbox(stageDescription + " Failure!");
                }
            }
            catch
            {
                Msgbox(stageDescription + " Failure!");
            }
        }
        #endregion

        #region ButtonReactive事件
        protected void ButtonReactive_Click(object sender, EventArgs e)
        {
            try
            {
                string action = PmsCommonEnum.ProjectStage.Reactive.GetDescription();

                //获取在Reactive之前的Stage
                int newStage;
                bool blResult = m_BasicInformationDetailBiz.GetStageBeforeReactiveAndUpdateStages(PmsID, LoginName, action, out newStage);
                if (blResult)
                {
                    Stage = newStage;
                    // 获取枚举类型的Description
                    PmsCommonEnum.ProjectStage projectStage
                        = (PmsCommonEnum.ProjectStage)System.Enum.Parse(typeof(PmsCommonEnum.ProjectStage), newStage.ToString());
                    txtStage.Text = projectStage.GetDescription();

                    //重绘ProjectProgress控件
                    PageProjectsInformation.ProjectProgressInitPmsFlow(newStage, dropdownlistType.SelectedValue.Trim());

                    InitButtonControlStatus(newStage);

                    IList<PmsHead> pmsHeadList = new PmsHeadBiz().SelectPmsHead(PmsID, null);
                    if (pmsHeadList != null && pmsHeadList.Count > 0)
                    {
                        PmsHead pmsHead = pmsHeadList[0];
                        try
                        {
                            LoginName.Replace(".", " ");
                            pmsHead.UserName = LoginName;
                            new MailBiz().SendPromoteMail(pmsHead, (int)PmsCommonEnum.ProjectStage.Reactive);
                        }
                        catch (Exception)
                        {
                        }
                    }


                    Msgbox("Reactive Successfully!");
                }
                else
                {
                    Msgbox("Reactive Failure!");
                }
            }
            catch
            {
                Msgbox("Reactive Failure!");
            }
        }
        #endregion

        #region ButtonPRelease事件
        protected void ButtonPRelease_Click(object sender, EventArgs e)
        {
            if (!Release("P"))
            {
                return;
            }
        }
        #endregion

        #region ButtonRelease事件
        protected void ButtonRelease_Click(object sender, EventArgs e)
        {
            //Check Due_date
            string message = string.Empty;
            //Abel 注释掉 on 2014-01-22
            //if (!CheckForRelease(ProjectType, out message))
            //{
            //    Msgbox(message);
            //    return;
            //}

            // add by Ename Wang on 20120619
            // 检查目前的CR No是否为临时CR NO 
            if (CheckIsTempCR(this.txtCRID.Text, out message))
            {
                Msgbox(message);// 仅仅是提醒，不return,卡关！
            }
            //end add
            if (!Release("Y"))
            {
                return;
            }

            ButtonPRelease.Enabled = false;
            ButtonRelease.Enabled = false;

            //更新release时间。
            txtReleaseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //更新状态
            Stage = (int)PmsCommonEnum.ProjectStage.WaitingClosed;

            //重绘ProjectProgress控件
            PageProjectsInformation.ProjectProgressInitPmsFlow(Stage, dropdownlistType.SelectedValue.Trim());
            txtStage.Text = PmsCommonEnum.ProjectStage.WaitingClosed.GetDescription();
        }
        #endregion

        #region CheckIsTempCR
        private bool CheckIsTempCR(string crID, out string message)
        {
            message = string.Empty;
            if (crID.Contains("T") && ProjectType != PmsCommonEnum.ProjectTypeFlowId.Study.GetDescription())
            {
                message = "this CR No is temp CR No,please check is there a formal CR No exists!";
                return true;
            }
            return false;
        }
        #endregion

        //Abel 注释掉 on 2014-01-22
        #region Release
        //private bool CheckForRelease(string type, out string message)
        //{
        //    message = "";
        //    IList<string> phaseList = new ProjectProgressBiz().SelectSdpDetailTemplatePhase(type);

        //    foreach (string phase in phaseList)
        //    {
        //        int intPhase = int.Parse(phase.Trim());
        //        if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Design)
        //        {
        //            string designCompletedPercent = PageProjectsInformation.DesignCompletedPercent;
        //            if (designCompletedPercent != "100.0%")
        //            {
        //                message = "Please finish the design phase!";
        //                return false;
        //            }
        //        }

        //        if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Development)
        //        {
        //            string developmentCompletedPercent = PageProjectsInformation.DevelopmentCompletedPercent;
        //            if (developmentCompletedPercent != "100.0%")
        //            {
        //                message = "Please finish the development phase!";
        //                return false;
        //            }
        //        }

        //        if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Test)
        //        {
        //            string testCompletedPercent = PageProjectsInformation.TestCompletedPercent;
        //            if (testCompletedPercent != "100.0%")
        //            {
        //                message = "Please finish the test phase!";
        //                return false;
        //            }
        //        }
        //        if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Release)
        //        {
        //            string releaseCompletedPercent = PageProjectsInformation.ReleaseCompletedPercent;
        //            if (releaseCompletedPercent != "100.0%")
        //            {
        //                message = "Please finish the Release  phase!";
        //                return false;
        //            }

        //        }

        //    }
        //    return true;
        //}


        private bool Release(string releaseType)
        {
            try
            {
                //Check Due_date
                if (dateTextBoxDueDate.Text.Trim() == "" || dateTextBoxDueDate.Text.Trim() == "0001-01-01")
                {
                    Msgbox("Please input due date!");
                    return false;
                }

                #region 检查文档是否存在
                if (releaseType == "Y")
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
                        return false;
                    }
                    #endregion

                    string message;
                    ProjectProgressBiz projectProgressBiz = new ProjectProgressBiz();

                    if (!projectProgressBiz.CheckDocuments(pmsHead, pmsHead.Stage, out message))
                    {
                        Msgbox(message);
                        return false;
                    }
                }
                #endregion


                //Check if RLNS have Sent RLN Mail for this CR
                SdpDetailBiz sdpBusiness = new SdpDetailBiz();

                //Check if RLNS have Sent RLN Mail for this CR
                if (!sdpBusiness.CheckRLN(this.txtCRID.Text.Trim(), this.dropdownlistSite.SelectedValue))
                {
                    Msgbox("RLNS already exists!");
                    return false;
                }

                string errorInfo;
                //Get detail info for RLNS
                if (!GetDetailInfo4Rlns(PmsID, out errorInfo))
                {
                    // Log.Error("Detail/btnPRelease_Click");
                    Msgbox("Transfer data to RLNS failed! " + errorInfo);
                    return false;
                }
                else
                {
                    //记录当前的releaseType
                    new PmsHeadBiz().UpdatePmsHeadRerver1ByPmsId(PmsID, releaseType);

                    //add by Tim on 20121226 for //更新head表的PlanStartDate、ActualStartDate
                    new PmsHeadBiz().UpdatePmsHeadActualStartDate(PmsID);
                    //End add

                    //Get RLN_ID 
                    string RLN_ID = Convert.ToString(sdpBusiness.GetRLNID(this.txtCRID.Text.Trim()));
                    string RLNSPage = ConfigurationSettings.AppSettings["RLSCreate"].ToString();
                    RLNSPage = RLNSPage + "?Act=Maintain&RLNID=" + RLN_ID + "&ReleaseType=" + releaseType;
                    //Open RLNS page
                    ScriptManager.RegisterStartupScript(this, GetType(), "saveScript",
                                                           "window.open('" + RLNSPage + "');", true);

                    //for update pms_Head.RERVER1='P' when Patial Release
                    //for update pms_Head.RERVER1='Y' when Release

                    // bool update = new PmsHeadBiz().UpdatePmsHeadRerver1ByPmsId(PmsID, releaseType);
                }
                return true;

            }
            catch (Exception ex)
            {
                Msgbox("Transfer data to RLNS failed!\n");
                //  Log.Error("Detail/btnPRelease_Click", ex);
                return false;
            }
        }

        public bool GetDetailInfo4Rlns(string pmsId, out string errorInfo)
        {
            errorInfo = string.Empty;
            #region
            try
            {
                string xml;
                // string sdp_url = string.Concat(Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("?") + 1), "pmsid=", pmsId);
                string sdp_url = ConfigurationSettings.AppSettings["PMSExternalSystemViewUrl"].ToString() + "?Action=SDPEDIT&PmsID=" + pmsId;

                m_BasicInformationDetailBiz.GetReleaseXml(pmsId, sdp_url, out xml);
                RLNWebService.RLNSWebService RLNService = new RLNWebService.RLNSWebService();
                RLNService.Url = ConfigurationManager.AppSettings["RLSWebService"].ToString();
                string resultXML = RLNService.CreateRLN(xml);
                SdpDetailBiz sdpBusiness = new SdpDetailBiz();
                string RLSResult = sdpBusiness.GetResultFromRLS(resultXML, "RLSReturn/STATE");
                errorInfo = sdpBusiness.GetResultFromRLS(resultXML, "RLSReturn/ERR_DESC");
                return RLSResult.Equals("True");
            }
            catch (Exception ex)
            {
                errorInfo = "CreateRLN Error";
                return false;
            }
            #endregion
        }
        #endregion

        #region SetReleaseButtonEnable
        //该函数供ProjectProgress.ascx调用
        public void SetReleaseButtonEnable()
        {
            if (CurrentUser.IsProjectQA)
            {
                ButtonPRelease.Enabled = true;
                ButtonRelease.Enabled = true;
            }
        }
        #endregion

    }
}