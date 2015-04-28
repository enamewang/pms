using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;
using Qisda.Web;
using WSC.Common;
using Qisda.DateTime;

namespace PMS.PMS.UserControls
{
    public partial class BasicInformationDetailService : ProjectsInformationUserControlBase
    {
        protected readonly PmsCRCreatBiz m_PmsCRCreatBiz = new PmsCRCreatBiz();

        #region Define Variable


        public string TotalManpower
        {
            get
            {
                return txtTotalManpower.Text.Trim();
            }
            set
            {
                txtTotalManpower.Text = value;

            }
        }

        public string Duration
        {
            get { return txtDuration.Text.Trim(); }
            set { txtDuration.Text = value; }
        }

        public string CloseDate
        {
            set
            {
                TextBoxCloseDate.Text = value;
            }
        }


        public string BasicPaGetextBoxStageName
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

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ProjectType != PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
            {
                return;
            }

            if (!IsPostBack)
            {
                //给ButtonCancel添加客户端事件
                ButtonCancelTop.OnClientClick = "javascript:window.location='ProjectsInformation.aspx?PmsID=" + PmsID + "'; return false;";
                ButtonCancelUnder.OnClientClick = "javascript:window.location='ProjectsInformation.aspx?PmsID=" + PmsID + "'; return false;";

                //绑定信息到页面并设置页面控件状态
                InitPage();
            }
        }

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

                ShowChangeHistory();
            }
            ImageButtonSearchCrId.Enabled = false;
            //SetViewStatusCSS();

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
                IList<PmsSys> pmsSysListPriority = pmsSysBiz.SelectData1ByType("PM", "Priority");
                dropdownlistPriority.DataSource = pmsSysListPriority;
                dropdownlistPriority.DataTextField = "Data1";
                dropdownlistPriority.DataValueField = "Data1";
                dropdownlistPriority.DataBind();
                QWeb.SelectItem(dropdownlistPriority, "Normal");
            }
            catch
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
                txtRelatedCrNo.Text = (pmsHead.RelatedCrNo ?? string.Empty).Trim();
                txtType.Text = (pmsHead.Type ?? string.Empty).Trim();
                ProjectType = (pmsHead.Type ?? string.Empty).Trim();

                string Description = pmsHead.Description;
                //Description = Description.Replace("<P>", "");
                //Description = Description.Replace("&nbsp;", " ");
                //Description = Description.Replace("</P>", "\n");
                //Description = Description.Replace("&lt;", "<");
                //Description = Description.Replace("&gt;", ">");


                txtDescription.Text = Description;
                txtResolveDescription.Text = pmsHead.ResolveDescription;

                //获取枚举类型的Description
                PmsCommonEnum.ProjectStage projectStage = (PmsCommonEnum.ProjectStage)Enum.Parse(typeof(PmsCommonEnum.ProjectStage), pmsHead.Stage.ToString());
                txtStage.Text = projectStage.GetDescription();
                txtSystem.Text = (pmsHead.System ?? string.Empty).Trim();
                txtCreateDate.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.CreateDate.ToString("yyyy-MM-dd").Trim());
                txtReleaseDate.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.ReleaseDate.ToString("yyyy-MM-dd").Trim());
                TextBoxDueDate.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.DueDate.ToString("yyyy-MM-dd").Trim());
                TextBoxCloseDate.Text = m_PmsCommonBiz.FormatDateTime(pmsHead.CloseDate.ToString("yyyy-MM-dd").Trim());

                SetDropDownListItem(dropdownlistDomain, (pmsHead.Domain ?? string.Empty).Trim());
                SetDropDownListItem(dropdownlistPriority, (pmsHead.Priority ?? string.Empty).Trim());
                SetDropDownListItem(dropdownlistSite, (pmsHead.Site ?? string.Empty).Trim());
                SetDropDownListItem(dropdownlistImpactSite, (pmsHead.ImpactSite ?? string.Empty).Trim());

                //add Progress TotalManpower by  Ename Wang on 20140123
                //TotalManpower和Duration赋值由SdpDetail完成
                txtTotalManpower.Text = (pmsHead.TotalManpower ?? string.Empty).Trim();
                txtDuration.Text
                        = m_BasicInformationDetailBiz.GetDuration(pmsHead.DueDate, pmsHead.ActualStartDate, pmsHead.PlanStartDate) + " Days";
                //end add

                txtPM.Text = (pmsHead.Pm ?? string.Empty).Trim();
                txtSD.Text = (pmsHead.Sd ?? string.Empty).Trim();
                txtQA.Text = (pmsHead.Qa ?? string.Empty).Trim();
                txtSE.Text = (pmsHead.Se ?? string.Empty).Trim();
            }
            catch
            {
                Msgbox("Set Control Default Value Failure!");
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

            if (CurrentUser.IsOrgPMO || CurrentUser.IsProjectPM || CurrentUser.IsProjectQA || CurrentUser.IsProjectSD || CurrentUser.IsProjectSE)
            {
                ButtonEdit.Enabled = true;
            }


            if (CurrentUser.IsProjectPM || CurrentUser.IsOrgPMO)
            {


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
            if (CurrentUser.IsProjectQA)
            {
                if (projectStage == PmsCommonEnum.ProjectStage.PIS_STP
                    || projectStage == PmsCommonEnum.ProjectStage.Develop_Test
                    || projectStage == PmsCommonEnum.ProjectStage.Release)
                {
                    ButtonPRelease.Visible = false;
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
            ButtonPRelease.Visible = false; // PRelease is always false
            ButtonRelease.Enabled = blStatus;

            ButtonOKTop.Enabled = blStatus;
            ButtonOKUnder.Enabled = blStatus;
            ButtonCancelTop.Enabled = blStatus;
            ButtonCancelUnder.Enabled = blStatus;
        }

        #endregion

        #region SetEditStatusCSS
        private void SetEditStatusCSS()
        {
            txtCRID.CssClass = "UnderLineOnlyTextBox";

            txtType.CssClass = "UnderLineOnlyTextBoxHalf";
            if (CurrentUser.IsProjectPM || CurrentUser.IsOrgPMO)
            {
                txtPMSName.CssClass = "NoLineDisplay";
                txtRelatedCrNo.CssClass = "NoLineDisplayHalf";
                txtDescription.CssClass = "MutilineTextBoxNotReadOnly";
            }
            else
            {
                txtPMSName.CssClass = "UnderLineOnlyTextBox";
                txtRelatedCrNo.CssClass = "UnderLineOnlyTextBoxHalf";
                txtDescription.CssClass = "MutilineTextBoxReadOnly";
            }
            txtStage.CssClass = "UnderLineOnlyTextBoxHalf";
            txtCreateDate.CssClass = "UnderLineOnlyTextBoxHalf";
            dropdownlistDomain.CssClass = "DropDownListEnableHalf";
            dropdownlistPriority.CssClass = "DropDownListEnableHalf";
            dropdownlistSite.CssClass = "DropDownListEnableHalf";
            dropdownlistImpactSite.CssClass = "DropDownListEnableHalf";
            if (CurrentUser.IsProjectPM || CurrentUser.IsOrgPMO)
            {
                txtSystem.CssClass = "NoLineDisplayHalf";
                TextBoxDueDate.CssClass = "NoLineDisplayHalf";
            }
            else
            {
                txtSystem.CssClass = "UnderLineOnlyTextBoxHalf";
                TextBoxDueDate.CssClass = "UnderLineOnlyTextBoxHalf";
            }
            txtReleaseDate.CssClass = "UnderLineOnlyTextBoxHalf";




            txtTotalManpower.CssClass = "UnderLineOnlyTextBoxHalf";
            txtDuration.CssClass = "UnderLineOnlyTextBoxHalf";
            if (CurrentUser.IsProjectPM)
            {
                txtPM.CssClass = "NoLineDisplayHalf";
            }
            if (CurrentUser.IsOrgPMO)
            {
                txtPM.CssClass = "NoLineDisplayHalf";
                txtSD.CssClass = "NoLineDisplayHalf";
                txtQA.CssClass = "NoLineDisplayHalf";
                txtSE.CssClass = "NoLineDisplayHalf";
            }
            // Service description and resolve description is special

            txtResolveDescription.CssClass = "MutilineTextBoxNotReadOnly";
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

        #endregion

        #region ButtonEdit事件
        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            SetControlStatusForEdit();
        }

        private void SetControlStatusForEdit()
        {
            #region Set Control Status
            ButtonEdit.Enabled = false;
            ButtonPending.Enabled = false;
            ButtonHardColse.Enabled = false;
            ButtonCancelled.Enabled = false;
            ButtonReactive.Enabled = false;
            ButtonPRelease.Visible = false;
            ButtonRelease.Enabled = false;

            ButtonOKTop.Visible = true;
            ButtonOKUnder.Visible = true;
            ButtonCancelTop.Visible = true;
            ButtonCancelUnder.Visible = true;

            ButtonOKTop.Enabled = true;
            ButtonOKUnder.Enabled = true;
            ButtonCancelTop.Enabled = true;
            ButtonCancelUnder.Enabled = true;

            if (CurrentUser.IsProjectPM || CurrentUser.IsOrgPMO)
            {
                txtPMSName.ReadOnly = false;
                txtRelatedCrNo.ReadOnly = false;
                txtSystem.ReadOnly = false;

                ImageButtonSearchCrId.Visible = true;
                ImageButtonSearchCrId.Enabled = true;
                //给textboxSystem绑定点击事件

                txtSystem.ReadOnly = false;
                txtSystem.Attributes.Add("onClick", "ShowSystemListPopUpService();");

                TextBoxDueDate.ReadOnly = false;
                TextBoxDueDate.Attributes.Add("onkeypress", "return false;");
                RenderScript.RenderCalendarScript(TextBoxDueDate, string.Empty);

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

                txtDescription.ReadOnly = false;

                dropdownlistDomain.Enabled = true;
                dropdownlistPriority.Enabled = true;
                dropdownlistSite.Enabled = true;
                dropdownlistImpactSite.Enabled = true;

                txtPM.ReadOnly = false;

                if (CurrentUser.IsOrgPMO)
                {
                    txtSD.ReadOnly = false;
                    txtSE.ReadOnly = false;
                    txtQA.ReadOnly = false;
                }
            }

            txtResolveDescription.ReadOnly = false;

            SetEditStatusCSS();
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
        #endregion

        #region EventButtonOK
        private void EventButtonOK()
        {
            try
            {
                #region 检查输入的信息是否符合要求
                if (!CheckControlValueForButtonOK())
                {
                    return;
                }
                #endregion

                #region 记录CR信息更改历史
                if (!RecordChangeHistory())
                {
                    return;
                }
                #endregion

                #region check Due Date PM QA SD SE Changed

                bool InformationChanged = false;

                string dueDate = InitPmsHead.DueDate.ToString("yyyy-MM-dd");
                string pm = InitPmsHead.Pm;
                string qa = InitPmsHead.Qa;
                string sd = InitPmsHead.Sd;
                string se = InitPmsHead.Se;
                InformationChanged = CheckIsChanged(dueDate, pm, qa, sd, se);

                #endregion

                #region 更新数据库相关信息

                //获取用以更新数据库的信息
                PmsHead pmsHead;
                if (!GetControlValue(out pmsHead))
                {
                    return;
                }

                //更新pms_itarm_mapping表
                //更新pmshead表
                //更新itarm_cr_list
                string type = PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription();
                if (!m_BasicInformationDetailBiz.Save(pmsHead, CrId, txtCRID.Text.Trim(), LoginName, ProjectType, type))
                {
                    Msgbox("Save Failure!");
                    return;
                }
                CrId = txtCRID.Text.Trim();

                #endregion

                //发Information change Mail
                if (InformationChanged)
                {
                    // Information changed,then send mail
                    MailBiz mailBiz = new MailBiz();
                    bool result = mailBiz.SendInformationChangedMail(InitPmsHead, pmsHead, LoginName);
                }

                // CurrentUser.LoginName = WSC.GlobalDefinition.Cookie_LoginUser.ToString(); 

                IList<BaseDataUser> baseDataUserList = new BaseDataUserBiz().SelectBaseDataUser(WSC.GlobalDefinition.Cookie_LoginUser, null);
                if (baseDataUserList != null && baseDataUserList.Count > 0)
                {
                    CurrentUser = baseDataUserList[0];
                }

                #region 设置页面的状态
                InitButtonControlStatus(Stage);
                SetControlStatusAfterOKClick();
                #endregion

                ButtonOKTop.Visible = false;
                ButtonOKUnder.Visible = false;
                ButtonCancelTop.Visible = false;
                ButtonCancelUnder.Visible = false;
                ImageButtonSearchCrId.Enabled = false;

                //复原状态进度条上的按钮状态
                PageProjectsInformation.ChangeImageButtonStatus(PageProjectsInformation.ImageButtonStageIsEnabled);
                PageRegisterStartupScript("window.location=window.location");
            }
            catch (Exception ex)
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
                if (dueDateInit.Trim() != Server.HtmlDecode(TextBoxDueDate.Text).Trim())
                {
                    pmsHeadH.DueDate = InitPmsHead.DueDate;
                    pmsHeadH.DueDateNew = DateTime.Parse(TextBoxDueDate.Text.Trim());
                }
            }

            if (!string.IsNullOrEmpty(type))
            {
                if (type.Trim() != Server.HtmlDecode(txtType.Text).Trim())
                {
                    pmsHeadH.Type = type;
                    pmsHeadH.TypeNew = Server.HtmlDecode(txtType.Text).Trim();
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
                if (dueDateInit.Trim() != Server.HtmlDecode(TextBoxDueDate.Text).Trim())
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
                if (type.Trim() != Server.HtmlDecode(txtType.Text).Trim())
                {
                    result = true;
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
                pmsHeadH.ReasonType = string.Empty;
                pmsHeadH.Reason = string.Empty;
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

        #region 检查需要填写的信息是否符合要求
        private bool CheckControlValueForButtonOK()
        {
            try
            {
                //PM和PMO需要填写的共同信息。

                if (!CheckControlDropDownListIsNull(dropdownlistDomain, "Please select Domain!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistPriority, "Please select Priority!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistSite, "Please select Site!"))
                    return false;

                if (!CheckControlTextBoxIsNull(txtSystem, "Please input System!"))
                    return false;

                if (!CheckControlTextBoxIsNull(txtPM, "Please input PM!"))
                    return false;
                //
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



                //PMO需要填写的信息
                if (CurrentUser.IsOrgPMO)
                //if (CurrentUser.IsOrgPMO && InitPmsHead.Stage == (int)PmsCommonEnum.ProjectStage.AssignMember)
                {
                    if (!CheckControlTextBoxIsNull(txtSD, "Please input SD!"))
                        return false;

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


                    if (!CheckControlTextBoxIsNull(txtSE, "Please input SE!"))
                        return false;
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


                    if (!CheckControlTextBoxIsNull(txtQA, "Please input QA!"))
                        return false;
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

                //检查该CRID是否存在
                if (!m_BasicInformationDetailBiz.CheckCrIdIsExist(InitPmsHead.CrId, txtCRID.Text.Trim()))
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

        private bool GetControlValue(out PmsHead pmsHead)
        {
            pmsHead = new PmsHead();
            try
            {
                // pmsHead.CrId = txtCRID.Text.Trim();
                pmsHead.PmsId = PmsID;
                pmsHead.PmsName = txtPMSName.Text.Trim();
                pmsHead.Type = txtType.Text;
                pmsHead.RelatedCrNo = txtRelatedCrNo.Text.Trim();
                pmsHead.Description = txtDescription.Text.Trim();
                pmsHead.ResolveDescription = txtResolveDescription.Text.Trim();
                pmsHead.Stage = Stage;
                pmsHead.System = txtSystem.Text.Trim();
                if (!string.IsNullOrEmpty(txtCreateDate.Text.Trim()))
                {
                    pmsHead.CreateDate = DateTime.Parse(txtCreateDate.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtReleaseDate.Text.Trim()))
                {
                    pmsHead.ReleaseDate = DateTime.Parse(txtReleaseDate.Text.Trim());
                }
                if (!string.IsNullOrEmpty(TextBoxDueDate.Text.Trim()))
                {
                    pmsHead.DueDate = DateTime.Parse(TextBoxDueDate.Text.Trim());
                }

                pmsHead.NeedSTP = "N";
                pmsHead.NeedSTC = "N";
                pmsHead.NewVersion = "";
                pmsHead.OldVersion = "";

                pmsHead.Domain = dropdownlistDomain.SelectedValue;
                pmsHead.Priority = dropdownlistPriority.SelectedValue;
                pmsHead.Site = dropdownlistSite.SelectedValue;
                pmsHead.ImpactSite = dropdownlistImpactSite.SelectedValue;
                //pmsHead.Progress = txtProgress.Text.Trim();
                pmsHead.TotalManpower = txtTotalManpower.Text.Trim();

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

        private void SetControlStatusAfterOKClick()
        {
            dropdownlistDomain.Enabled = false;
            dropdownlistPriority.Enabled = false;
            dropdownlistSite.Enabled = false;
            dropdownlistImpactSite.Enabled = false;

            TextBoxDueDate.Enabled = false;
            TextBoxDueDate.ReadOnly = true;

            TextBoxDueDate.CssClass = "UnderLineOnlyTextBoxHalf";

            //SetTextBoxReadOnlyCSS();
        }



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
                    string type = "Service";
                    PageProjectsInformation.ProjectProgressInitPmsFlow(newStage, type);

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
                    string type = "Service";
                    PageProjectsInformation.ProjectProgressInitPmsFlow(newStage, type);

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
                        catch (Exception ex)
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

        #region ButtonRelease事件
        protected void ButtonRelease_Click(object sender, EventArgs e)
        {
            //Check Due_date
            //Abel 注释掉 on 2014-01-22
            //string message = string.Empty;
            //if (!CheckForRelease(ProjectType, out message))
            //{
            //    Msgbox(message);
            //    return;
            //}
            // Check Resolve Description ,can be put 
            if (txtResolveDescription.Text.Trim() == string.Empty)
            {
                Msgbox("Please input Resolve Description!");
                return;
            }
            //Mark by Ename Wang on 20111128 10:00 
            // for Service type  Release check failed
            // Transfer data to RLNS failed!
            // so set not check for service at this moment

            ButtonPRelease.Visible = false;
            ButtonRelease.Enabled = false;

            //更新release时间,同时如果DueDate没有填写，则DueDate=ReleaseDate

            PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
            InitPmsHead.ReleaseDate = DateTime.Now;

            string dueDate = m_PmsCommonBiz.FormatDateTime(InitPmsHead.DueDate.ToString("yyyy-MM-dd"));
            if (dueDate != string.Empty)
            {
                InitPmsHead.DueDate = DateTime.Parse(dueDate);
            }
            else
            {
                InitPmsHead.DueDate = DateTime.Now;
            }

            //更新状态
            Stage = (int)PmsCommonEnum.ProjectStage.WaitingClosed;
            InitPmsHead.Stage = Stage;
            bool result = pmsHeadBiz.UpdatePmsHead(InitPmsHead);
            if (result)
            {
                txtReleaseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                string dueDateForTextBox = m_PmsCommonBiz.FormatDateTime(InitPmsHead.DueDate.ToString("yyyy-MM-dd"));
                if (dueDate != string.Empty)
                {
                    TextBoxDueDate.Text = dueDateForTextBox;
                }
                else
                {
                    TextBoxDueDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }

            //重绘ProjectProgress控件
            string type = "Service";
            PageProjectsInformation.ProjectProgressInitPmsFlow(Stage, type);
            txtStage.Text = PmsCommonEnum.ProjectStage.WaitingClosed.GetDescription();

            #region 发送release mail
            //push release to WaitingClosed
            LoginName.Replace(".", " ");
            InitPmsHead.UserName = LoginName;
            new MailBiz().SendPromoteMail(InitPmsHead, Stage);
            #endregion
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
        //                message = "Please finish the design phase of SDP!";
        //                return false;
        //            }
        //        }

        //        if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Development)
        //        {
        //            string developmentCompletedPercent = PageProjectsInformation.DevelopmentCompletedPercent;
        //            if (developmentCompletedPercent != "100.0%")
        //            {
        //                message = "Please finish the development phase of SDP!";
        //                return false;
        //            }
        //        }
        //        //Test阶段先不检查
        //        //if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Test)
        //        //{
        //        //    string testCompletedPercent = PageProjectsInformation.TestCompletedPercent;
        //        //    if (testCompletedPercent != "100.0%")
        //        //    {
        //        //        message = "Please finish the test phase of SDP!";
        //        //        return false;
        //        //    }
        //        //}
        //        if (intPhase == (int)PmsCommonEnum.EnumSdpPhase.Release)
        //        {
        //            string releaseCompletedPercent = PageProjectsInformation.ReleaseCompletedPercent;
        //            if (releaseCompletedPercent != "100.0%")
        //            {
        //                message = "Please finish the Release phase of SDP!";
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
                ButtonPRelease.Visible = false;
                ButtonRelease.Enabled = true;
            }
        }
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
                    if (dueDate.Trim() != Server.HtmlDecode(TextBoxDueDate.Text).Trim())
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
    }
}