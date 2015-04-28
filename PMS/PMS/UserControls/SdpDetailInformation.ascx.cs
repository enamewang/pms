using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;
using Qisda.Web;
using Titan.WebForm;

namespace PMS.PMS.UserControls
{
    public partial class SdpDetailInformation : ProjectsInformationUserControlBase
    {
        protected readonly SdpDetailBiz m_SdpDetailBiz = new SdpDetailBiz();
        #region  Define Variable


        //<asp:HiddenField ID="HiddenFieldActualStartDateValue" runat="server" />
        //    <asp:HiddenField ID="HiddenFieldCloseDateValue" runat="server" />


        public string TotalTime
        {
            get
            {
                return labelAllPhaseActualTime.Text.Trim();
            }
            set
            {
                labelAllPhaseActualTime.Text = value;

                HiddenFieldTotalManpowerValue.Value = value;
                //修改BasicInformationDetail.ascx页面上的控件TxtTotalManpower值

                //Abel 注释掉 on 2014-01-22
                //PageProjectsInformation.SetTxtTotalManpowerBaseInforDetail = value;
                PageProjectsInformation.SetTxtTotalManpowerBaseInforDetailService = value;// Add by Ename Wang on 20111126 19:14
            }
        }

        public string DurationForService
        {
            get
            {
                return HiddenFieldDurationValue.Value;
            }
            set
            {
                HiddenFieldDurationValue.Value = value;
                PageProjectsInformation.DurationForService = value;// Add by Ename Wang on 20111126 19:14
            }
        }

        public string TotalPercent
        {
            get
            {
                return labelAllPhasePercentValue.Text.Trim();
            }
            set
            {
                labelAllPhasePercentValue.Text = value;

                HiddenFieldProgressValue.Value = value;
                //修改BasicInformationDetail.ascx页面上的控件TxtProgress值
                if(PageProjectsInformation.ProjectType!="Service")
                {
                   //Abel 注释掉 on2014-01-22
                   // PageProjectsInformation.SetTxtProgressBaseInforDetail = value;
                }
            }
        }

        public string DesignCompletedPercent
        {
            get
            {
                return labelDesignCompletedPercentValue.Text.Trim();
            }
            set
            {
                labelDesignCompletedPercentValue.Text = value;
            }
        }

        public string DevelopmentCompletedPercent
        {
            get
            {
                return labelDevelopmentCompletedPercentValue.Text.Trim();
            }
            set
            {
                labelDevelopmentCompletedPercentValue.Text = value;
            }
        }

        public string TestCompletedPercent
        {
            get
            {
                return labelTestCompletedPercentValue.Text.Trim();
            }
            set
            {
                labelTestCompletedPercentValue.Text = value;
            }
        }

        public string ReleaseCompletedPercent
        {
            get
            {
                return labelReleaseCompletedPercentValue.Text.Trim();
            }
            set
            {
                labelReleaseCompletedPercentValue.Text = value;
            }
        }

        public string SupportCompletedPercent
        {
            get
            {
                return labelSupportCompletedPercentValue.Text.Trim();
            }
            set
            {
                labelSupportCompletedPercentValue.Text = value;
            }
        }
        #endregion

        #region Define Variable

        private string UserStatus
        {
            get
            {
                object obj = ViewState["UserStatus"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["UserStatus"] = value; }
        }

        private bool IsCrRD
        {
            get
            {
                object obj = ViewState["IsCrRD"];
                return (obj == null) ? false : (bool)obj;
            }
            set { ViewState["IsCrRD"] = value; }
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Buffer = true;
            this.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            this.Response.Expires = 0;
            this.Response.CacheControl = "no-cache";

            if (!IsPostBack)
            {
                InitPage();
                BindGrid();
            }
            ConfirmButtonEnable();
        }
        #endregion

        private void InitPage()
        {
            #region 设置鼠标经过图片的样式
            BindMouseEvent(imageButtonExpand);
            BindMouseEvent(ImageDivDesignDetailOpen);
            BindMouseEvent(ImageDivDevelopmentDetailOpen);
            BindMouseEvent(ImageDivReleaseSDetailOpen);
            BindMouseEvent(ImageDivSupportDetailOpen);
            BindMouseEvent(ImageDivTestDetailOpen);
            #endregion

            //GetUserStatus
            UserStatus = CurrentUser.Role.Trim().ToUpper();

            CheckCRRD();

            HiddenFieldActualStartDateValue.Value = m_PmsCommonBiz.FormatDateTime(m_SdpDetailBiz.SelectMinActualStartDate(PmsID, null)[0].Actualstartday.ToString("yyyy-MM-dd").Trim());

            SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();

            DurationForService = sdpDetailBiz.GetDuration(PmsID);
        }

        //检查当前用户是否属于该项目组成员
        private void CheckCRRD()
        {
            try
            {
                #region Get StageID

                PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                IList<PmsHead> pmsHeadList = pmsHeadBiz.SelectPmsHead(PmsID, LoginName);

                if (pmsHeadList != null && pmsHeadList.Count > 0)
                {
                    IsCrRD = true;
                }
                else
                {
                    IsCrRD = false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindMouseEvent(Image imagebutton)
        {
            imagebutton.Attributes.Add("onmouseover", "TaskNameMouseOver(this)");
            imagebutton.Attributes.Add("onmouseout", "TaskNameMouseOut(this)");
        }

        private void BindGrid()
        {
            try
            {
                GetDateByCRPhase(gridViewDesign, (int)PmsCommonEnum.EnumSdpPhase.Design);
                GetDateByCRPhase(gridViewDevelopment, (int)PmsCommonEnum.EnumSdpPhase.Development);
                GetDateByCRPhase(gridViewTest, (int)PmsCommonEnum.EnumSdpPhase.Test);
                GetDateByCRPhase(gridViewRelease, (int)PmsCommonEnum.EnumSdpPhase.Release);
                GetDateByCRPhase(gridViewSupport, (int)PmsCommonEnum.EnumSdpPhase.Support);

                SetControlStatus();

                AllPhasePercent();
            }
            catch(Exception e)
            {
                Msgbox("BindGrid failure!");
            }
        }

        private void ConfirmButtonEnable()
        {
            PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
            IList<PmsHead> pmsHeadList = pmsHeadBiz.SelectPmsHeadByPmsId(PmsID);
            if (pmsHeadList == null || pmsHeadList.Count <= 0)
            {
                return;
            }

            if (pmsHeadList[0].Type == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
            {
                ButtonConfirm.Visible = false;
            }

            if (pmsHeadList[0].SDPConfirmDate.ToString("yyyy") == "0000" || pmsHeadList[0].SDPConfirmDate.ToString("yyyy") == "0001"
                || pmsHeadList[0].SDPConfirmDate.ToString("yyyy") == "1900" || pmsHeadList[0].SDPConfirmDate.ToString("yyyy") == "1899")
            {
                if (CurrentUser.IsProjectSD)
                {
                    switch (pmsHeadList[0].Type)
                    {
                        case "CR":
                        case "Study":
                        case "Project":
                            if (pmsHeadList[0].Stage == (int)PmsCommonEnum.ProjectStage.PIS_STP)
                            {
                                ButtonConfirm.Enabled = true;
                            }

                            break;
                        case "Small CR":

                            if (pmsHeadList[0].Stage == (int)PmsCommonEnum.ProjectStage.Develop_Test)
                            {
                                ButtonConfirm.Enabled = true;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
        }

        

        #region 单个GridView的绑定和计算
        private void GetDateByCRPhase(GridView gridView, int phase)
        {
            try
            {
                SdpDetail sdpDetail = new SdpDetail();
                sdpDetail.Pmsid = PmsID;
                sdpDetail.Phase = phase.ToString();

                IList<SdpDetail> sdpDetailList = m_SdpDetailBiz.SelectSdpDetail(sdpDetail);

                if (sdpDetailList != null && sdpDetailList.Count > 0)
                {
                    gridView.DataSource = sdpDetailList;
                    gridView.DataBind();

                    DropDownList dropDownListR = (DropDownList)gridView.FooterRow.FindControl("dropDownListRole");
                    DropDownList dropDownListU = (DropDownList)gridView.FooterRow.FindControl("dropDownListUser");
                    BindDropDownListRole(dropDownListR);
                    BindDropDownListUser(dropDownListU);

                    GetPlanDate(phase);

                    CalculatePercent(gridView, phase);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void BindDropDownListRole(DropDownList dropDownList)
        {
            try
            {
                if (dropDownList != null)
                {
                    dropDownList.Items.Clear();
                    dropDownList.DataSource = Enum.GetNames(typeof(PmsCommonEnum.Role));
                    dropDownList.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BindDropDownListUser(DropDownList dropDownList)
        {
            try
            {
                //原逻辑：
                //BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
                //IList<BaseDataUser> baseDataUserList = baseDataUserBiz.SelectBaseDataUser(null, "RD");

                //改为：（如果需要分角色的话再改回去）
                //BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
                //IList<BaseDataUser> baseDataUserList = baseDataUserBiz.SelectBaseDataUser(null, null);

                //if (baseDataUserList != null && baseDataUserList.Count > 0)
                //{
                //    dropDownList.DataSource = baseDataUserList;
                //    dropDownList.DataTextField = "UserName";
                //    dropDownList.DataValueField = "UserName";
                //    dropDownList.DataBind();
                //}

                IList<PmsHead> pmsHeadPmSdSeQAList = new PmsHeadBiz().SelectPmSdSeQA(PmsID);
                IList<string> projectMemberList = new List<string>();

                if (pmsHeadPmSdSeQAList != null && pmsHeadPmSdSeQAList.Count > 0)
                {
                    foreach (PmsHead head in pmsHeadPmSdSeQAList)
                    {
                        string[] membersPm = head.Pm.Split(';');
                        foreach (string memberPm in membersPm)
                        {
                            if (!string.IsNullOrEmpty(memberPm))
                            {
                                projectMemberList.Add(memberPm);
                            }
                        }

                        string[] membersSd = head.Sd.Split(';');
                        foreach (string memberSd in membersSd)
                        {
                            if (!string.IsNullOrEmpty(memberSd))
                            {
                                projectMemberList.Add(memberSd);
                            }
                        }

                        string[] membersSe = head.Se.Split(';');
                        foreach (string memberSe in membersSe)
                        {
                            if (!string.IsNullOrEmpty(memberSe))
                            {
                                projectMemberList.Add(memberSe);
                            }
                        }

                        string[] membersQa = head.Qa.Split(';');
                        foreach (string memberQa in membersQa)
                        {
                            if (!string.IsNullOrEmpty(memberQa))
                            {
                                projectMemberList.Add(memberQa);
                            }
                        }
                    }

                    if (projectMemberList.Count > 0)
                    {
                        projectMemberList = projectMemberList.Distinct().ToList();

                        dropDownList.DataSource = projectMemberList;
                        //dropDownList.DataTextField = "value";
                        //dropDownList.DataValueField = "value";
                        dropDownList.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetPlanDate(int phase)
        {
            try
            {
                IList<SdpDetail> sdpDetailMin = m_SdpDetailBiz.SelectMinPlanSDate(PmsID, phase.ToString());
                IList<SdpDetail> sdpDetailMax = m_SdpDetailBiz.SelectMaxPlanEDate(PmsID, phase.ToString());

                string planStartDay = sdpDetailMin[0].Planstartday.ToString("yyyy-MM-dd");
                string planEndDay = sdpDetailMax[0].Planendday.ToString("yyyy-MM-dd");

                #region Bind PlanDate
                switch (phase)
                {
                    case (int)PmsCommonEnum.EnumSdpPhase.Design:
                        labelDesignBeginDateValue.Text = planStartDay;
                        labelDesignEndDateValue.Text = planEndDay == "0001-01-01" ? "" : planEndDay;
                        break;

                    case (int)PmsCommonEnum.EnumSdpPhase.Development:
                        labelDevelopmentBeginDateValue.Text = planStartDay;
                        labelDevelopmentEndDateValue.Text = planEndDay == "0001-01-01" ? "" : planEndDay;
                        break;

                    case (int)PmsCommonEnum.EnumSdpPhase.Test:
                        labelTestBeginDateValue.Text = planStartDay;
                        labelTestEndDateValue.Text = planEndDay == "0001-01-01" ? "" : planEndDay;
                        break;

                    case (int)PmsCommonEnum.EnumSdpPhase.Release:
                        labelReleaseBeginDateValue.Text = planStartDay;
                        labelReleaseEndDateValue.Text = planEndDay == "0001-01-01" ? "" : planEndDay;
                        break;

                    case (int)PmsCommonEnum.EnumSdpPhase.Support:
                        labelSupportBeginDateValue.Text = planStartDay;
                        labelSupportEndDateValue.Text = planEndDay == "0001-01-01" ? "" : planEndDay;
                        break;
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void CalculatePercent(GridView gridview, int Phase)
        {
            #region Define Variable
            decimal compleatePercent = 0;
            decimal totalPlanCost = 0;
            decimal totalPercent = 0;
            decimal totalActualCost = 0;
            #endregion

            try
            {
                for (int i = 0; i < gridview.Rows.Count; i++)
                {
                    Label labelPlanCost = gridview.Rows[i].FindControl("labelPlanCost") as Label;
                    Label labelCompletedPercent = gridview.Rows[i].FindControl("labelCompletedPercent") as Label;
                    Label labelActualCost = gridview.Rows[i].FindControl("labelActualCost") as Label;
                    totalPlanCost += Convert.ToDecimal(labelPlanCost.Text.Trim());
                    totalActualCost += Convert.ToDecimal(labelActualCost.Text.Trim());
                    totalPercent += Convert.ToDecimal(labelPlanCost.Text.Trim()) * Convert.ToDecimal(labelCompletedPercent.Text.Trim());
                }

                if (totalPlanCost > 0)
                    compleatePercent = Convert.ToDecimal(totalPercent / totalPlanCost);

                switch (Phase)
                {
                    case (int)PmsCommonEnum.EnumSdpPhase.Design:
                        labelDesignCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                        labelDesignPlanTime.Text = totalPlanCost.ToString();
                        labelDesignActualTime.Text = totalActualCost.ToString();
                        updatePanelDesignCompletedPercent.Update();
                        break;
                    case (int)PmsCommonEnum.EnumSdpPhase.Development:
                        labelDevelopmentCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                        labelDevelopmentPlanTime.Text = totalPlanCost.ToString();
                        labelDevelopmentActualTime.Text = totalActualCost.ToString();
                        updatePanelDevelopmentCompletedPercent.Update();
                        break;
                    case (int)PmsCommonEnum.EnumSdpPhase.Test:
                        labelTestCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                        labelTestPlanTime.Text = totalPlanCost.ToString();
                        labelTestActualTime.Text = totalActualCost.ToString();
                        updatePanelTestCompletedPercent.Update();
                        break;
                    case (int)PmsCommonEnum.EnumSdpPhase.Release:
                        labelReleaseCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                        labelReleasePlanTime.Text = totalPlanCost.ToString();
                        labelReleaseActualTime.Text = totalActualCost.ToString();
                        updatePanelReleaseCompletedPercent.Update();
                        break;
                    case (int)PmsCommonEnum.EnumSdpPhase.Support:
                        labelSupportCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                        labelSupportPlanTime.Text = totalPlanCost.ToString();
                        labelSupportActualTime.Text = totalActualCost.ToString();
                        updatePanelSupportCompletedPercent.Update();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GridViewFooter的权限设置
        private void SetControlStatus()
        {
            
            if(Stage ==(int)PmsCommonEnum.ProjectStage.Closed ||
                Stage ==(int)PmsCommonEnum.ProjectStage.HardClosed || 
                Stage ==(int)PmsCommonEnum.ProjectStage.Pending ||
                Stage ==(int)PmsCommonEnum.ProjectStage.Cancelled 
               )
            {
                SetGridViewFooter(gridViewDesign);
                SetGridViewFooter(gridViewDevelopment);
                SetGridViewFooter(gridViewTest);
                SetGridViewFooter(gridViewRelease);
                SetGridViewFooter(gridViewSupport);
            }

            if (Stage == (int)PmsCommonEnum.ProjectStage.WaitingClosed)
            {
                SetGridViewFooter(gridViewDesign);
                SetGridViewFooter(gridViewDevelopment);
                SetGridViewFooter(gridViewTest);
                SetGridViewFooter(gridViewRelease);
            }


            if (IsCrRD == false
                && !CurrentUser.IsOrgPMO
                && !CurrentUser.IsOrgRDManager)
            {
                SetGridViewFooter(gridViewDesign);
                SetGridViewFooter(gridViewDevelopment);
                SetGridViewFooter(gridViewTest);
                SetGridViewFooter(gridViewRelease);
                SetGridViewFooter(gridViewSupport);
            }
        }

        private void SetGridViewFooter(GridView gridView)
        {
            if (gridView.FooterRow != null)
            {
                gridView.FooterRow.Visible = false;
            }
            //gridView.Columns[13].Visible = false;
            //gridView.Columns[14].Visible = false;
            //gridView.Columns[15].Visible = false;
        }
        #endregion

        protected void InitBindData(GridViewRowEventArgs e)
        {
            PmsCommonBiz pmsCommonBiz = new PmsCommonBiz();

            Label labelActualStartDay = e.Row.FindControl("labelActualStartDay") as Label;
            Label labelActualEndDay = e.Row.FindControl("labelActualEndDay") as Label;
            Label labelPlanStartDay = e.Row.FindControl("labelPlanStartDay") as Label;
            Label labelPlanEndDay = e.Row.FindControl("labelPlanEndDay") as Label;

            TextBox calendarActualEndDay = e.Row.FindControl("calendarActualEndDay") as TextBox;
            TextBox calendarActualStartDay = e.Row.FindControl("calendarActualStartDay") as TextBox;
            TextBox calendarPlanStartDay = e.Row.FindControl("calendarPlanStartDay") as TextBox;
            TextBox calendarPlanEndDay = e.Row.FindControl("calendarPlanEndDay") as TextBox;

            if (labelActualStartDay != null)
            {
                labelActualEndDay.Text = pmsCommonBiz.FormatDateTime(labelActualEndDay.Text);
                labelActualStartDay.Text = pmsCommonBiz.FormatDateTime(labelActualStartDay.Text);
                labelPlanStartDay.Text = pmsCommonBiz.FormatDateTime(labelPlanStartDay.Text);
                labelPlanEndDay.Text = pmsCommonBiz.FormatDateTime(labelPlanEndDay.Text);
            }
            if (calendarActualEndDay != null)
            {
                calendarActualEndDay.Text = pmsCommonBiz.FormatDateTime(calendarActualEndDay.Text);
                calendarActualStartDay.Text = pmsCommonBiz.FormatDateTime(calendarActualStartDay.Text);
                calendarPlanStartDay.Text = pmsCommonBiz.FormatDateTime(calendarPlanStartDay.Text);
                calendarPlanEndDay.Text = pmsCommonBiz.FormatDateTime(calendarPlanEndDay.Text);
            }
        }

        private void BindDropDownList(GridView gridView, int rowIndex)
        {
            // string strRole = "";
            //string strUser = "";

            try
            {
                #region Bind Role
                DropDownList dropDownListRole = (DropDownList)gridView.Rows[rowIndex].FindControl("dropDownListRole");

                if (dropDownListRole != null)
                {
                    BindDropDownListRole(dropDownListRole);

                    int serial = int.Parse(gridView.DataKeys[rowIndex]["Serial"].ToString());

                    if (serial > 0)
                    {
                        SdpDetail sdpDetail = new SdpDetail();
                        sdpDetail.Serial = serial;

                        //SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                        IList<SdpDetail> sdpDetailList = m_SdpDetailBiz.SelectSdpDetail(sdpDetail);

                        if (sdpDetailList != null && sdpDetailList.Count > 0)
                        {
                            string strRole = sdpDetailList[0].Role.Trim();
                            if (!string.IsNullOrEmpty(strRole))
                            {
                                QWeb.SelectItem(dropDownListRole, strRole);
                            }
                        }
                    }

                }
                #endregion

                #region Bind Resource
                DropDownList dropDownListUser = (DropDownList)gridView.Rows[rowIndex].FindControl("dropDownListUser");

                if (dropDownListUser != null)
                {
                    BindDropDownListUser(dropDownListUser);

                    int serial = int.Parse(gridView.DataKeys[rowIndex]["Serial"].ToString());

                    if (serial > 0)
                    {
                        SdpDetail sdpDetail = new SdpDetail();
                        sdpDetail.Serial = serial;

                        SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                        IList<SdpDetail> sdpDetailList = sdpDetailBiz.SelectSdpDetail(sdpDetail);

                        if (sdpDetailList != null && sdpDetailList.Count > 0)
                        {
                            string strUser = sdpDetailList[0].Resource.Trim();
                            if (!string.IsNullOrEmpty(strUser))
                            {
                                SetDropDownListItem(dropDownListUser, strUser);
                                // QWeb.SelectItem(dropDownListUser, strUser);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BindChangeEvent(GridViewRowEventArgs e)
        {
            TextBox textBoxActualCost = e.Row.FindControl("textBoxActualCost") as TextBox;
            TextBox textBoxPlanCost = e.Row.FindControl("textBoxPlanCost") as TextBox;
            TextBox textBoxCompletedPercent = e.Row.FindControl("textBoxCompletedPercent") as TextBox;
            TextBox textBoxTaskNo = e.Row.FindControl("textBoxTaskNo") as TextBox;
            TextBox textBoxPreTaskNo = e.Row.FindControl("textBoxPreTaskNo") as TextBox;
            ImageButton QimagebuttonSave = e.Row.FindControl("QimagebuttonSave") as ImageButton;
            ImageButton QimagebuttonUpdate = e.Row.FindControl("QimagebuttonUpdate") as ImageButton;
            TextBox calendarActualEndDay = e.Row.FindControl("calendarActualEndDay") as TextBox;
            TextBox calendarActualStartDay = e.Row.FindControl("calendarActualStartDay") as TextBox;
            TextBox calendarPlanStartDay = e.Row.FindControl("calendarPlanStartDay") as TextBox;
            TextBox calendarPlanEndDay = e.Row.FindControl("calendarPlanEndDay") as TextBox;
            TextBox textBoxRemark = e.Row.FindControl("textBoxRemark") as TextBox;
            TextBox textBoxTaskName = e.Row.FindControl("textBoxTaskName") as TextBox;
            DropDownList dropDownListUser = e.Row.FindControl("dropDownListUser") as DropDownList;

            // ImageButton QimagebuttonDelete = e.Row.FindControl("QimagebuttonDelete") as ImageButton;
            //CheckPercent
            if (textBoxActualCost != null)
            {
                textBoxActualCost.Attributes.Add("onkeypress", "return Numbers(this,event,true);");
                textBoxPlanCost.Attributes.Add("onkeypress", "return Numbers(this,event,true);");
                textBoxCompletedPercent.Attributes.Add("onkeypress", "return Numbers(this,event,true);");
                textBoxTaskNo.Attributes.Add("onkeypress", "return Numbers(this,event,true);");
                textBoxPreTaskNo.Attributes.Add("onkeypress", "return Numbers(this,event,true);");

                textBoxActualCost.Attributes.Add("onchange", "CompletePercent('" + textBoxActualCost.ClientID + "','" + textBoxPlanCost.ClientID + "','" + textBoxCompletedPercent.ClientID + "')");

                if (QimagebuttonUpdate != null)
                {
                    QimagebuttonUpdate.Attributes.Add("onclick", "return CheckPercent('" + textBoxActualCost.ClientID + "','" + textBoxPlanCost.ClientID + "','" + textBoxCompletedPercent.ClientID + "','"
                           + calendarPlanStartDay.ClientID + "','" + calendarPlanEndDay.ClientID + "','" + calendarActualStartDay.ClientID + "','" + calendarActualEndDay.ClientID +"','" +textBoxTaskName.ClientID + "','" + dropDownListUser.ClientID + "','" + textBoxRemark.ClientID + "')");
                }

                if (QimagebuttonSave != null)
                {
                    QimagebuttonSave.Attributes.Add("onclick", "javascript:return CheckPercent('" + textBoxActualCost.ClientID + "','" + textBoxPlanCost.ClientID + "','" + textBoxCompletedPercent.ClientID + "','"
                          + calendarPlanStartDay.ClientID + "','" + calendarPlanEndDay.ClientID + "','" + calendarActualStartDay.ClientID + "','" + calendarActualEndDay.ClientID + "','" + textBoxTaskName.ClientID +"','" + dropDownListUser.ClientID +"','" + textBoxRemark.ClientID + "');");

                }
            }
        }

        public void GridCommand(object sender, GridViewCommandEventArgs e, GridView gridView, int CRPhase)
        {
            int rowIndex = 0;
            int currentRowIndex = 0;
            DateTime dateTime = PmsSysBiz.GetDBDateTime();

            PmsCommonBiz pmsCommonBiz = new PmsCommonBiz();
            SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();

            rowIndex = int.Parse(e.CommandArgument.ToString());
            currentRowIndex = rowIndex - (gridView.PageIndex * gridView.PageSize);
            PmsHead objHead = new PmsHeadBiz().SelectPmsHeadByPmsId(PmsID)[0];
            switch (e.CommandName)
            {
                case "Save":
                    {
                        #region Save

                        TextBox textBoxTaskNo = (TextBox)gridView.FooterRow.FindControl("textBoxTaskNo");
                        TextBox textBoxTaskName = (TextBox)gridView.FooterRow.FindControl("textBoxTaskName");
                        TextBox textBoxPlanCost = (TextBox)gridView.FooterRow.FindControl("textBoxPlanCost");
                        TextBox textBoxActualCost = (TextBox)gridView.FooterRow.FindControl("textBoxActualCost");
                        TextBox textBoxCompletedPercent = (TextBox)gridView.FooterRow.FindControl("textBoxCompletedPercent");

                        TextBox textBoxPlanStartDay = (TextBox)gridView.FooterRow.FindControl("calendarPlanStartDay");
                        TextBox textBoxPlanEndDay = (TextBox)gridView.FooterRow.FindControl("calendarPlanEndDay");
                        TextBox textBoxActualStartDay = (TextBox)gridView.FooterRow.FindControl("calendarActualStartDay");
                        TextBox textBoxActualEndDay = (TextBox)gridView.FooterRow.FindControl("calendarActualEndDay");

                        TextBox textBoxPreTaskNo = (TextBox)gridView.FooterRow.FindControl("textBoxPreTaskNo");
                        DropDownList dropDownListRole = (DropDownList)gridView.FooterRow.FindControl("dropDownListRole");
                        DropDownList dropDownListUser = (DropDownList)gridView.FooterRow.FindControl("dropDownListUser");
                        TextBox textBoxRemark = (TextBox)gridView.FooterRow.FindControl("textBoxRemark");
                        
                        //if (textBoxPlanStartDay.Text == "")
                        //{
                        //    Msgbox("The Plan Start Day Can Not Be Null!");
                        //    return;
                        //}

                        //if (textBoxPlanEndDay.Text == "")
                        //{
                        //    Msgbox("The Plan End Day Can Not Be Null!");
                        //    return;
                        //}
                         

                        if (!ValidatePlanDate(textBoxPlanStartDay, textBoxPlanEndDay, CRPhase, objHead.DueDate, ProjectType))
                        {
                            return;
                        }

                        SdpDetail sdpDetail = new SdpDetail();
                        sdpDetail.Pmsid = PmsID;
                        sdpDetail.TaskName = textBoxTaskName.Text.Trim();
                        sdpDetail.Phase = CRPhase.ToString();
                        sdpDetail.Role = dropDownListRole.SelectedItem.Text.Trim();
                        sdpDetail.Resource = dropDownListUser.SelectedItem.Text.Trim();

                        IList<SdpDetail> sdpDetailList = sdpDetailBiz.SelectSdpDetail(sdpDetail);
                        if (sdpDetailList != null && sdpDetailList.Count > 0)
                        {

                            if (sdpDetailList[0].TaskName == sdpDetail.TaskName && sdpDetailList[0].Resource == sdpDetail.Resource)
                            {
                                Msgbox("The Same TaskName and  Resource have been exist!");
                                //Msgbox("Task name&Phase&Role&Resource have been exist,Please check!");
                                return;
                            }
                        }

                        sdpDetail.Taskno = pmsCommonBiz.ConvertStringToInt(textBoxTaskNo.Text.ToString().Trim());
                        sdpDetail.Plancost = pmsCommonBiz.ConvertStringToFloat(textBoxPlanCost.Text.ToString().Trim());
                        sdpDetail.Actualcost = pmsCommonBiz.ConvertStringToFloat(textBoxActualCost.Text.ToString().Trim());
                        sdpDetail.Planstartday = pmsCommonBiz.ConvertDateTime(textBoxPlanStartDay.Text.ToString().Trim());
                        sdpDetail.Planendday = pmsCommonBiz.ConvertDateTime(textBoxPlanEndDay.Text.ToString().Trim());
                        sdpDetail.Actualstartday = pmsCommonBiz.ConvertDateTime(textBoxActualStartDay.Text.ToString().Trim());
                        sdpDetail.Actualendday = pmsCommonBiz.ConvertDateTime(textBoxActualEndDay.Text.ToString().Trim());
                        sdpDetail.PretaskNo = pmsCommonBiz.ConvertStringToInt(textBoxPreTaskNo.Text.ToString().Trim());
                        sdpDetail.Remark = textBoxRemark.Text.ToString().Trim();
                        sdpDetail.Iseditable = "Y";
                        sdpDetail.Deleteflag = "N";
                        sdpDetail.Createdate = dateTime;
                        sdpDetail.Createuser = LoginName;
                        sdpDetail.Maintaindate = dateTime;
                        sdpDetail.Maintainuser = LoginName;
                        sdpDetail.Completedpercent = pmsCommonBiz.ConvertStringToFloat(textBoxCompletedPercent.Text.ToString().Trim());

                        int insertResult = sdpDetailBiz.InsertSdpDetail(sdpDetail);

                        if (insertResult <= 0)
                        {
                            Msgbox("Save the new item failed!");
                            return;
                        }

                        //更新head表的PlanStartDate、ActualStartDate
                        new PmsHeadBiz().UpdatePmsHeadActualStartDate(PmsID);
                        // PageProjectsInformation.PlanStartDate = m_SdpDetailBiz.SelectMinPlanSDate(PmsID, null)[0].Planstartday;

                        HiddenFieldActualStartDateValue.Value = m_PmsCommonBiz.FormatDateTime(m_SdpDetailBiz.SelectMinActualStartDate(PmsID, null)[0].Actualstartday.ToString("yyyy-MM-dd").Trim());
                        HiddenFieldDurationValue.Value = m_SdpDetailBiz.GetDuration(PmsID);
                        DurationForService = sdpDetailBiz.GetDuration(PmsID);
                        PageProjectsInformation.ActualStartDate = HiddenFieldActualStartDateValue.Value;

                        gridView.EditIndex = -1;
                        BindGrid(sender, e, gridView, CRPhase);
                        CalculatePercent(gridView, CRPhase);
                        AllPhasePercent();
                        break;

                        #endregion
                    }

                case "Update":
                    {
                        #region Update
                        TextBox textBoxTaskNo = (TextBox)gridView.Rows[currentRowIndex].FindControl("textBoxTaskNo");
                        TextBox textBoxTaskName = (TextBox)gridView.Rows[currentRowIndex].FindControl("textBoxTaskName");
                        TextBox textBoxPlanCost = (TextBox)gridView.Rows[currentRowIndex].FindControl("textBoxPlanCost");
                        TextBox textBoxActualCost = (TextBox)gridView.Rows[currentRowIndex].FindControl("textBoxActualCost");
                        TextBox textBoxCompletedPercent = (TextBox)gridView.Rows[currentRowIndex].FindControl("textBoxCompletedPercent");

                        DateTextBox textBoxPlanStartDay = (DateTextBox)gridView.Rows[currentRowIndex].FindControl("calendarPlanStartDay");
                        DateTextBox textBoxPlanEndDay = (DateTextBox)gridView.Rows[currentRowIndex].FindControl("calendarPlanEndDay");
                        DateTextBox textBoxActualStartDay = (DateTextBox)gridView.Rows[currentRowIndex].FindControl("calendarActualStartDay");
                        DateTextBox textBoxActualEndDay = (DateTextBox)gridView.Rows[currentRowIndex].FindControl("calendarActualEndDay");

                        TextBox textBoxPreTaskNo = (TextBox)gridView.Rows[currentRowIndex].FindControl("textBoxPreTaskNo");
                        DropDownList dropDownListRole = (DropDownList)gridView.Rows[currentRowIndex].FindControl("dropDownListRole");
                        DropDownList dropDownListUser = (DropDownList)gridView.Rows[currentRowIndex].FindControl("dropDownListUser");
                        TextBox textBoxRemark = (TextBox)gridView.Rows[currentRowIndex].FindControl("textBoxRemark");

                        SdpDetail pmsSdpDetail = new SdpDetail();
                        pmsSdpDetail.Pmsid = PmsID;
                        pmsSdpDetail.Serial = int.Parse(gridView.DataKeys[currentRowIndex]["Serial"].ToString());
                        pmsSdpDetail.TaskName = textBoxTaskName.Text.Trim();
                        pmsSdpDetail.Phase = CRPhase.ToString();
                        pmsSdpDetail.Role = dropDownListRole.SelectedItem.Text.Trim();
                        pmsSdpDetail.Resource = dropDownListUser.SelectedItem.Text.Trim();


                        if (!ValidatePlanDate(textBoxPlanStartDay, textBoxPlanEndDay, CRPhase, objHead.DueDate, ProjectType))
                        {
                            return;
                        }

                        IList<SdpDetail> sdpDetailList = sdpDetailBiz.SelectSdpDetailOther(pmsSdpDetail);

                        if (sdpDetailList != null && sdpDetailList.Count > 0)
                        {
                            if (sdpDetailList[0].TaskName == pmsSdpDetail.TaskName && sdpDetailList[0].Resource == pmsSdpDetail.Resource)
                            {
                                Msgbox("The Same TaskName and  Resource have been exist!");
                                //Msgbox("Task name and Resource have been exist,Please check!");
                                return;
                            }
                        }

                        pmsSdpDetail.Taskno = pmsCommonBiz.ConvertStringToInt(textBoxTaskNo.Text.ToString().Trim());
                        pmsSdpDetail.Plancost = pmsCommonBiz.ConvertStringToFloat(textBoxPlanCost.Text.ToString().Trim());
                        pmsSdpDetail.Actualcost = pmsCommonBiz.ConvertStringToFloat(textBoxActualCost.Text.ToString().Trim());
                        pmsSdpDetail.Completedpercent = pmsCommonBiz.ConvertStringToFloat(textBoxCompletedPercent.Text.ToString().Trim());
                        pmsSdpDetail.Planstartday = pmsCommonBiz.ConvertDateTime(textBoxPlanStartDay.Text.ToString().Trim());
                        pmsSdpDetail.Planendday = pmsCommonBiz.ConvertDateTime(textBoxPlanEndDay.Text.ToString().Trim());
                        pmsSdpDetail.Actualstartday = pmsCommonBiz.ConvertDateTime(textBoxActualStartDay.Text.ToString().Trim());
                        pmsSdpDetail.Actualendday = pmsCommonBiz.ConvertDateTime(textBoxActualEndDay.Text.ToString().Trim());
                        pmsSdpDetail.PretaskNo = pmsCommonBiz.ConvertStringToInt(textBoxPreTaskNo.Text.ToString().Trim());
                        pmsSdpDetail.Remark = textBoxRemark.Text.ToString().Trim();
                        pmsSdpDetail.Iseditable = "Y";
                        pmsSdpDetail.Deleteflag = "N";
                        pmsSdpDetail.Maintaindate = dateTime;
                        pmsSdpDetail.Maintainuser = LoginName;

                        bool updateResult = sdpDetailBiz.UpdateSdpDetail(pmsSdpDetail);

                        if (updateResult == false)
                        {
                            Msgbox("Update this item failed!");
                            return;
                        }
                        
                        //更新head表的PlanStartDate、ActualStartDate
                        new PmsHeadBiz().UpdatePmsHeadActualStartDate(PmsID);
                        // PageProjectsInformation.PlanStartDate = m_SdpDetailBiz.SelectMinPlanSDate(PmsID, null)[0].Planstartday;

                        HiddenFieldActualStartDateValue.Value = m_PmsCommonBiz.FormatDateTime(m_SdpDetailBiz.SelectMinActualStartDate(PmsID, null)[0].Actualstartday.ToString("yyyy-MM-dd").Trim());
                        DurationForService = sdpDetailBiz.GetDuration(PmsID);
                        HiddenFieldDurationValue.Value = m_SdpDetailBiz.GetDuration(PmsID);
                        PageProjectsInformation.ActualStartDate = HiddenFieldActualStartDateValue.Value;

                        gridView.EditIndex = -1;

                        BindGrid(sender, e, gridView, CRPhase);

                        break;

                        #endregion
                    }

                case "Delete":
                    {
                        #region Delete
                        int delResult = sdpDetailBiz.DeleteSdpDetail(gridView.DataKeys[currentRowIndex]["Serial"].ToString());

                        if (delResult <= 0)
                        {
                            Msgbox("Delete this item failed!");
                            return;
                        }

                        //更新head表的PlanStartDate、ActualStartDate
                        new PmsHeadBiz().UpdatePmsHeadActualStartDate(PmsID);
                        // PageProjectsInformation.PlanStartDate = m_SdpDetailBiz.SelectMinPlanSDate(PmsID, null)[0].Planstartday;

                        HiddenFieldActualStartDateValue.Value = m_PmsCommonBiz.FormatDateTime(m_SdpDetailBiz.SelectMinActualStartDate(PmsID, null)[0].Actualstartday.ToString("yyyy-MM-dd").Trim());
                        PageProjectsInformation.ActualStartDate = HiddenFieldActualStartDateValue.Value;

                        gridView.EditIndex = -1;
                        BindGrid(sender, e, gridView, CRPhase);
                        break;

                        #endregion
                    }

                case "Edit":
                    {
                        #region Edit

                        rowIndex = int.Parse(e.CommandArgument.ToString());
                        gridView.EditIndex = rowIndex - (gridView.PageIndex * gridView.PageSize);

                        BindGrid(sender, e, gridView, CRPhase);

                        break;

                        #endregion
                    }

                case "Cancel":
                    {
                        #region Cancel

                        gridView.EditIndex = -1;
                        BindGrid(sender, e, gridView, CRPhase);
                        break;

                        #endregion
                    }

                case "Copy":
                    {
                        #region Copy
                        rowIndex = int.Parse(e.CommandArgument.ToString());
                        currentRowIndex = rowIndex - (gridView.PageIndex * gridView.PageSize);

                        Label labelTaskNo = (Label)gridView.Rows[currentRowIndex].FindControl("labelTaskNo");
                        if (labelTaskNo == null)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "saveScript", "alert('Please save the item you edited first!');", true);
                            return;
                        }
                        else
                        {
                            Label labelTaskName = (Label)gridView.Rows[currentRowIndex].FindControl("labelTaskName");
                            Label labelPlanCost = (Label)gridView.Rows[currentRowIndex].FindControl("labelPlanCost");
                            Label labelActualCost = (Label)gridView.Rows[currentRowIndex].FindControl("labelActualCost");
                            Label labelCompletedPercent = (Label)gridView.Rows[currentRowIndex].FindControl("labelCompletedPercent");
                            Label labelPlanStartDay = (Label)gridView.Rows[currentRowIndex].FindControl("labelPlanStartDay");
                            Label labelPlanEndDay = (Label)gridView.Rows[currentRowIndex].FindControl("labelPlanEndDay");
                            Label labelActualStartDay = (Label)gridView.Rows[currentRowIndex].FindControl("labelActualStartDay");
                            Label labelActualEndDay = (Label)gridView.Rows[currentRowIndex].FindControl("labelActualEndDay");
                            Label labelPreTaskNo = (Label)gridView.Rows[currentRowIndex].FindControl("labelPreTaskNo");
                            Label labelRole = (Label)gridView.Rows[currentRowIndex].FindControl("labelRole");
                            Label labelResource = (Label)gridView.Rows[currentRowIndex].FindControl("labelResource");
                            Label labelRemark = (Label)gridView.Rows[currentRowIndex].FindControl("labelRemark");

                            ((TextBox)gridView.FooterRow.FindControl("textBoxTaskNo")).Text = Server.HtmlDecode(labelTaskNo.Text);
                            ((TextBox)gridView.FooterRow.FindControl("textBoxTaskName")).Text = Server.HtmlDecode(labelTaskName.Text);
                            ((TextBox)gridView.FooterRow.FindControl("textBoxPlanCost")).Text = Server.HtmlDecode(labelPlanCost.Text);
                            ((TextBox)gridView.FooterRow.FindControl("textBoxActualCost")).Text = Server.HtmlDecode(labelActualCost.Text);
                            ((TextBox)gridView.FooterRow.FindControl("textBoxCompletedPercent")).Text = Server.HtmlDecode(labelCompletedPercent.Text);
                            ((TextBox)gridView.FooterRow.FindControl("calendarPlanStartDay")).Text = Server.HtmlDecode(labelPlanStartDay.Text);
                            ((TextBox)gridView.FooterRow.FindControl("calendarPlanEndDay")).Text = Server.HtmlDecode(labelPlanEndDay.Text);
                            ((TextBox)gridView.FooterRow.FindControl("calendarActualStartDay")).Text = Server.HtmlDecode(labelActualStartDay.Text);
                            ((TextBox)gridView.FooterRow.FindControl("calendarActualEndDay")).Text = Server.HtmlDecode(labelActualEndDay.Text);
                            ((TextBox)gridView.FooterRow.FindControl("textBoxPreTaskNo")).Text = Server.HtmlDecode(labelPreTaskNo.Text);

                            DropDownList dropDown = ((DropDownList)gridView.FooterRow.FindControl("dropDownListRole"));

                            dropDown.SelectedIndex = dropDown.Items.IndexOf(dropDown.Items.FindByText(labelRole.Text.Trim()));

                            if (labelResource.Text.Trim() != "")
                            {
                                DropDownList dropDownUs = (DropDownList)gridView.FooterRow.FindControl("dropDownListUser");
                                SetDropDownListItem(dropDownUs, labelResource.Text);
                            }
                            ((TextBox)gridView.FooterRow.FindControl("textBoxRemark")).Text = Server.HtmlDecode(labelRemark.Text);
                        }
                        break;
                        #endregion
                    }

                case "CopyTest":
                    {
                        CopyFromDevelopment();
                        break;
                    }
            }

        }

        private void BindGrid(object sender, EventArgs e, GridView gridView, int phase)
        {
            if (sender != null)
            {
                SdpDetail sdpDetail = new SdpDetail();
                sdpDetail.Pmsid = PmsID;
                sdpDetail.Phase = phase.ToString();

                SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                IList<SdpDetail> sdpDetailList = sdpDetailBiz.SelectSdpDetail(sdpDetail);

                gridView.DataSource = sdpDetailList;
                gridView.DataBind();

                //Bind footer value
                DropDownList dropDownListRole = (DropDownList)gridView.FooterRow.FindControl("dropDownListRole");
                BindDropDownListRole(dropDownListRole);

                DropDownList dropDownListUser = (DropDownList)gridView.FooterRow.FindControl("dropDownListUser");
                BindDropDownListUser(dropDownListUser);

                GetPlanDate(phase);
            }
        }

        protected void CalculatePercent(GridView gridview, string phase)
        {
            decimal compleatePercent = 0;
            decimal totalPlanCost = 0;
            decimal totalPercent = 0;
            decimal totalActualCost = 0;

            for (int i = 0; i < gridview.Rows.Count; i++)
            {
                Label labelPlanCost = gridview.Rows[i].FindControl("labelPlanCost") as Label;
                Label labelCompletedPercent = gridview.Rows[i].FindControl("labelCompletedPercent") as Label;
                Label labelActualCost = gridview.Rows[i].FindControl("labelActualCost") as Label;
                totalPlanCost += Convert.ToDecimal(labelPlanCost.Text.Trim());
                totalActualCost += Convert.ToDecimal(labelActualCost.Text.Trim());
                totalPercent += Convert.ToDecimal(labelPlanCost.Text.Trim()) * Convert.ToDecimal(labelCompletedPercent.Text.Trim());
            }

            if (totalPlanCost > 0)
                compleatePercent = Convert.ToDecimal(totalPercent / totalPlanCost);

            switch (int.Parse(phase))
            {
                case (int)PmsCommonEnum.EnumSdpPhase.Design:
                    labelDesignCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                    labelDesignPlanTime.Text = totalPlanCost.ToString();
                    labelDesignActualTime.Text = totalActualCost.ToString();
                    updatePanelDesignCompletedPercent.Update();
                    break;
                case (int)PmsCommonEnum.EnumSdpPhase.Development:
                    labelDevelopmentCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                    labelDevelopmentPlanTime.Text = totalPlanCost.ToString();
                    labelDevelopmentActualTime.Text = totalActualCost.ToString();
                    updatePanelDevelopmentCompletedPercent.Update();
                    break;
                case (int)PmsCommonEnum.EnumSdpPhase.Test:
                    labelTestCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                    labelTestPlanTime.Text = totalPlanCost.ToString();
                    labelTestActualTime.Text = totalActualCost.ToString();
                    updatePanelTestCompletedPercent.Update();
                    break;
                case (int)PmsCommonEnum.EnumSdpPhase.Release:
                    labelReleaseCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                    labelReleasePlanTime.Text = totalPlanCost.ToString();
                    labelReleaseActualTime.Text = totalActualCost.ToString();
                    updatePanelReleaseCompletedPercent.Update();
                    break;
                case (int)PmsCommonEnum.EnumSdpPhase.Support:
                    labelSupportCompletedPercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
                    labelSupportPlanTime.Text = totalPlanCost.ToString();
                    labelSupportActualTime.Text = totalActualCost.ToString();
                    updatePanelSupportCompletedPercent.Update();
                    break;
            }
        }

        protected void AllPhasePercent()
        {
            decimal compleatePercent = 0;
            decimal totalPlanCost = 0;
            decimal totalPercent = 0;
            decimal totalActualCost = 0;

            SdpDetail sdpDetail = new SdpDetail();
            sdpDetail.Pmsid = PmsID;

            SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
            IList<SdpDetail> pmsSdpDetailIlist = sdpDetailBiz.SelectPhaseByPmsID(sdpDetail);

            if (pmsSdpDetailIlist != null && pmsSdpDetailIlist.Count > 0)
            {
                for (int rowIndex = 0; rowIndex < pmsSdpDetailIlist.Count; rowIndex++)
                {
                    if (pmsSdpDetailIlist[rowIndex].Phase == ((int)PmsCommonEnum.EnumSdpPhase.Design).ToString())
                    {
                        for (int i = 0; i < gridViewDesign.Rows.Count; i++)
                        {
                            Label labelPlanCost = gridViewDesign.Rows[i].FindControl("labelPlanCost") as Label;
                            Label labelCompletedPercent = gridViewDesign.Rows[i].FindControl("labelCompletedPercent") as Label;
                            Label labelActualCost = gridViewDesign.Rows[i].FindControl("labelActualCost") as Label;
                            totalPlanCost += Convert.ToDecimal(labelPlanCost.Text.Trim());
                            totalActualCost += Convert.ToDecimal(labelActualCost.Text.Trim());
                            totalPercent += Convert.ToDecimal(labelPlanCost.Text.Trim()) * Convert.ToDecimal(labelCompletedPercent.Text.Trim());
                        }
                    }

                    if (pmsSdpDetailIlist[rowIndex].Phase == ((int)PmsCommonEnum.EnumSdpPhase.Development).ToString())
                    {
                        for (int i = 0; i < gridViewDevelopment.Rows.Count; i++)
                        {
                            Label labelPlanCost = gridViewDevelopment.Rows[i].FindControl("labelPlanCost") as Label;
                            Label labelCompletedPercent = gridViewDevelopment.Rows[i].FindControl("labelCompletedPercent") as Label;
                            Label labelActualCost = gridViewDevelopment.Rows[i].FindControl("labelActualCost") as Label;
                            totalPlanCost += Convert.ToDecimal(labelPlanCost.Text.Trim());
                            totalActualCost += Convert.ToDecimal(labelActualCost.Text.Trim());
                            totalPercent += Convert.ToDecimal(labelPlanCost.Text.Trim()) * Convert.ToDecimal(labelCompletedPercent.Text.Trim());
                        }
                    }

                    if (pmsSdpDetailIlist[rowIndex].Phase == ((int)PmsCommonEnum.EnumSdpPhase.Test).ToString())
                    {
                        for (int i = 0; i < gridViewTest.Rows.Count; i++)
                        {
                            Label labelPlanCost = gridViewTest.Rows[i].FindControl("labelPlanCost") as Label;
                            Label labelCompletedPercent = gridViewTest.Rows[i].FindControl("labelCompletedPercent") as Label;
                            Label labelActualCost = gridViewTest.Rows[i].FindControl("labelActualCost") as Label;
                            totalPlanCost += Convert.ToDecimal(labelPlanCost.Text.Trim());
                            totalActualCost += Convert.ToDecimal(labelActualCost.Text.Trim());
                            totalPercent += Convert.ToDecimal(labelPlanCost.Text.Trim()) * Convert.ToDecimal(labelCompletedPercent.Text.Trim());
                        }
                    }

                    if (pmsSdpDetailIlist[rowIndex].Phase == ((int)PmsCommonEnum.EnumSdpPhase.Release).ToString())
                    {
                        for (int i = 0; i < gridViewRelease.Rows.Count; i++)
                        {
                            Label labelPlanCost = gridViewRelease.Rows[i].FindControl("labelPlanCost") as Label;
                            Label labelCompletedPercent = gridViewRelease.Rows[i].FindControl("labelCompletedPercent") as Label;
                            Label labelActualCost = gridViewRelease.Rows[i].FindControl("labelActualCost") as Label;
                            totalPlanCost += Convert.ToDecimal(labelPlanCost.Text.Trim());
                            totalActualCost += Convert.ToDecimal(labelActualCost.Text.Trim());
                            totalPercent += Convert.ToDecimal(labelPlanCost.Text.Trim()) * Convert.ToDecimal(labelCompletedPercent.Text.Trim());
                        }
                    }
                    if (pmsSdpDetailIlist[rowIndex].Phase == ((int)PmsCommonEnum.EnumSdpPhase.Support).ToString())
                    {
                        for (int i = 0; i < gridViewSupport.Rows.Count; i++)
                        {
                            Label labelPlanCost = gridViewSupport.Rows[i].FindControl("labelPlanCost") as Label;
                            Label labelCompletedPercent = gridViewSupport.Rows[i].FindControl("labelCompletedPercent") as Label;
                            Label labelActualCost = gridViewSupport.Rows[i].FindControl("labelActualCost") as Label;
                            totalPlanCost += Convert.ToDecimal(labelPlanCost.Text.Trim());
                            totalActualCost += Convert.ToDecimal(labelActualCost.Text.Trim());
                            totalPercent += Convert.ToDecimal(labelPlanCost.Text.Trim()) * Convert.ToDecimal(labelCompletedPercent.Text.Trim());
                        }
                    }
                }
            }

            if (totalPlanCost > 0)
                compleatePercent = Convert.ToDecimal(totalPercent / totalPlanCost);


            // labelAllPhasePercentValue.Text = string.Format("{0:0.0}", compleatePercent) + "%";
            TotalPercent = string.Format("{0:0.0}", compleatePercent) + "%";
            labelAllPhasePlanTime.Text = totalPlanCost.ToString();
            //labelAllPhaseActualTime.Text = totalActualCost.ToString();
            TotalTime = totalActualCost.ToString();
            updatePanelAllPhasePercent.Update();

        }

        protected void CopyFromDevelopment()
        {
            int maxTestTaskNo = 0;
            int minDevelopmentTaskNo = 0;
            int differenceValue = 0;

            SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();

            SdpDetail sdpDetail = new SdpDetail();
            sdpDetail.Pmsid = PmsID;
            sdpDetail.Phase = ((int)PmsCommonEnum.EnumSdpPhase.Development).ToString();

            IList<SdpDetail> sdpDetailDevList = sdpDetailBiz.SelectMinTaskNo(sdpDetail);
            if (sdpDetailDevList != null && sdpDetailDevList.Count > 0)
            {
                minDevelopmentTaskNo = int.Parse(sdpDetailDevList[0].Taskno.ToString().Trim());
            }

            SdpDetail sdpDetailTest = new SdpDetail();
            sdpDetailTest.Pmsid = PmsID;
            sdpDetailTest.Phase = ((int)PmsCommonEnum.EnumSdpPhase.Test).ToString();

            IList<SdpDetail> sdpDetailTestList = sdpDetailBiz.SelectMaxTaskNo(sdpDetailTest);
            if (sdpDetailTestList != null && sdpDetailTestList.Count > 0)
            {
                maxTestTaskNo = int.Parse(sdpDetailTestList[0].Taskno.ToString().Trim());
            }

            differenceValue = (minDevelopmentTaskNo >= maxTestTaskNo ? (minDevelopmentTaskNo - maxTestTaskNo) : (maxTestTaskNo - minDevelopmentTaskNo));

            SdpDetail sdpDetailInfo = new SdpDetail();
            sdpDetailInfo.Pmsid = PmsID;
            sdpDetailInfo.Phase = ((int)PmsCommonEnum.EnumSdpPhase.Development).ToString();

            IList<SdpDetail> pmsSdpDetailIlist = sdpDetailBiz.SelectSdpDetail(sdpDetailInfo);

            if (pmsSdpDetailIlist != null && pmsSdpDetailIlist.Count > 0)
            {
                for (int i = 0; i < pmsSdpDetailIlist.Count; i++)
                {
                    pmsSdpDetailIlist[i].Taskno = pmsSdpDetailIlist[i].Taskno + 1 - (minDevelopmentTaskNo - maxTestTaskNo);
                    pmsSdpDetailIlist[i].Planstartday = pmsSdpDetailIlist[i].Planendday.AddDays(1);
                    pmsSdpDetailIlist[i].Planendday = pmsSdpDetailIlist[i].Planendday.AddDays(1);
                }
            }

            if (sdpDetailBiz.InsertCopyFromDevelopment(pmsSdpDetailIlist, PmsID, LoginName))
            {
                gridViewTest.EditIndex = -1;
                GetDateByCRPhase(gridViewTest, (int)PmsCommonEnum.EnumSdpPhase.Test);
                CalculatePercent(gridViewTest, PmsCommonEnum.EnumSdpPhase.Test.ToString());
                AllPhasePercent();
            }
            else
            {
                Msgbox(" Save the new item failed!");
                return;
            }

        }

        #region Deal gridViewDesign
        protected void gridViewDesign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow || (e.Row.RowType == DataControlRowType.Header) || (e.Row.RowType == DataControlRowType.Footer))
                {
                    for (int rowIndex = 0; rowIndex < gridViewDesign.Rows.Count; rowIndex++)
                    {
                        BindDropDownList(gridViewDesign, rowIndex);
                    }

                    #region Hide Columns
                    // if ((Stage >= (int)PmsCommonEnum.EnumSdpPhase.Release) || (IsCrRD == false && UserStatus != PmsCommonEnum.OrgRole.RD_LEADER.GetDescription() && UserStatus != PmsCommonEnum.OrgRole.RD_MANAGER.GetDescription()))
                    if ((Stage >= (int)PmsCommonEnum.EnumSdpPhase.Release) || (IsCrRD == false))
                    {
                        e.Row.Cells[13].Visible = false;
                        e.Row.Cells[14].Visible = false;
                        e.Row.Cells[15].Visible = false;
                    }
                    #endregion

                    BindChangeEvent(e);

                    InitBindData(e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridViewDesign_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewDesign.EditIndex = e.NewEditIndex;
        }

        protected void gridViewDesign_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridCommand(sender, e, gridViewDesign, (int)PmsCommonEnum.EnumSdpPhase.Design);
        }

        protected void gridViewDesign_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //
        }

        protected void gridViewDesign_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //
        }

        protected void gridViewDesign_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CalculatePercent(gridViewDesign, (int)PmsCommonEnum.EnumSdpPhase.Design);
            AllPhasePercent();
        }

        protected void gridViewDesign_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.gridViewDesign.EditIndex = -1;
            BindGrid(sender, e, gridViewDesign, (int)PmsCommonEnum.EnumSdpPhase.Design);
            CalculatePercent(gridViewDesign, (int)PmsCommonEnum.EnumSdpPhase.Design);
            AllPhasePercent();
        }

        #endregion

        #region Deal GridViewDevelopment

        protected void gridViewDevelopment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //
        }

        protected void gridViewDevelopment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridCommand(sender, e, gridViewDevelopment, (int)PmsCommonEnum.EnumSdpPhase.Development);
        }

        protected void gridViewDevelopment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow || (e.Row.RowType == DataControlRowType.Header) || (e.Row.RowType == DataControlRowType.Footer))
                {
                    for (int rowIndex = 0; rowIndex < gridViewDevelopment.Rows.Count; rowIndex++)
                    {
                        BindDropDownList(gridViewDevelopment, rowIndex);
                    }

                    #region Hide Columns
                    //if ((Stage >= (int)PmsCommonEnum.EnumSdpPhase.Release) || (IsCrRD == false && UserStatus != "RD LEADER" && UserStatus != "RD MANAGER"))
                    if ((Stage >= (int)PmsCommonEnum.EnumSdpPhase.Release) || (IsCrRD == false))
                    {
                        e.Row.Cells[13].Visible = false;
                        e.Row.Cells[14].Visible = false;
                        e.Row.Cells[15].Visible = false;
                    }
                    #endregion

                    BindChangeEvent(e);

                    InitBindData(e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridViewDevelopment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CalculatePercent(gridViewDevelopment, (int)PmsCommonEnum.EnumSdpPhase.Development);
            AllPhasePercent();
        }

        protected void gridViewDevelopment_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewDevelopment.EditIndex = e.NewEditIndex;
        }

        protected void gridViewDevelopment_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.gridViewDevelopment.EditIndex = -1;
            BindGrid(sender, e, gridViewDevelopment, (int)PmsCommonEnum.EnumSdpPhase.Development);
            CalculatePercent(gridViewDevelopment, (int)PmsCommonEnum.EnumSdpPhase.Development);
            AllPhasePercent();
        }

        #endregion

        #region Deal gridViewTest
        protected void gridViewTest_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //
        }

        protected void gridViewTest_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewTest.EditIndex = e.NewEditIndex;
        }

        protected void gridViewTest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridCommand(sender, e, gridViewTest, (int)PmsCommonEnum.EnumSdpPhase.Test);
        }

        protected void gridViewTest_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CalculatePercent(gridViewTest, (int)PmsCommonEnum.EnumSdpPhase.Test);
            AllPhasePercent();
        }

        protected void gridViewTest_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.gridViewTest.EditIndex = -1;
            BindGrid(sender, e, gridViewTest, (int)PmsCommonEnum.EnumSdpPhase.Test);
            CalculatePercent(gridViewTest, (int)PmsCommonEnum.EnumSdpPhase.Test);
            AllPhasePercent();
        }

        protected void gridViewTest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow || (e.Row.RowType == DataControlRowType.Header) || (e.Row.RowType == DataControlRowType.Footer))
                {
                    for (int rowIndex = 0; rowIndex < gridViewTest.Rows.Count; rowIndex++)
                    {
                        BindDropDownList(gridViewTest, rowIndex);
                    }

                    #region Hide Columns
                    //if ((Stage >= (int)PmsCommonEnum.EnumSdpPhase.Release) || (IsCrRD == false && UserStatus != "RD LEADER" && UserStatus != "RD MANAGER"))
                    if ((Stage >= (int)PmsCommonEnum.EnumSdpPhase.Release) || (IsCrRD == false))
                    {
                        e.Row.Cells[13].Visible = false;
                        e.Row.Cells[14].Visible = false;
                        e.Row.Cells[15].Visible = false;
                    }
                    #endregion

                    BindChangeEvent(e);

                    InitBindData(e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Deal gridViewRelease

        protected void gridViewRelease_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //
        }

        protected void gridViewRelease_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewRelease.EditIndex = e.NewEditIndex;
        }

        protected void gridViewRelease_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridCommand(sender, e, gridViewRelease, (int)PmsCommonEnum.EnumSdpPhase.Release);
        }

        protected void gridViewRelease_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CalculatePercent(gridViewRelease, (int)PmsCommonEnum.EnumSdpPhase.Release);
            AllPhasePercent();
        }

        protected void gridViewRelease_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.gridViewRelease.EditIndex = -1;
            BindGrid(sender, e, gridViewRelease, (int)PmsCommonEnum.EnumSdpPhase.Release);
            CalculatePercent(gridViewRelease, (int)PmsCommonEnum.EnumSdpPhase.Release);
            AllPhasePercent();
        }

        protected void gridViewRelease_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow || (e.Row.RowType == DataControlRowType.Header) || (e.Row.RowType == DataControlRowType.Footer))
                {
                    for (int rowIndex = 0; rowIndex < gridViewRelease.Rows.Count; rowIndex++)
                    {
                        BindDropDownList(gridViewRelease, rowIndex);
                    }

                    #region Hide Columns
                    //if ((Stage >= (int)PmsCommonEnum.EnumSdpPhase.Release) || (IsCrRD == false && UserStatus != "RD LEADER" && UserStatus != "RD MANAGER"))
                    if ((Stage >= (int)PmsCommonEnum.EnumSdpPhase.Release) || (IsCrRD == false))
                    {
                        e.Row.Cells[13].Visible = false;
                        e.Row.Cells[14].Visible = false;
                        e.Row.Cells[15].Visible = false;
                    }
                    #endregion

                    BindChangeEvent(e);

                    InitBindData(e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Deal gridViewSupport

        protected void gridViewSupport_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //
        }

        protected void gridViewSupport_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewSupport.EditIndex = e.NewEditIndex;
        }

        protected void gridViewSupport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridCommand(sender, e, gridViewSupport, (int)PmsCommonEnum.EnumSdpPhase.Support);
        }

        protected void gridViewSupport_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CalculatePercent(gridViewSupport, (int)PmsCommonEnum.EnumSdpPhase.Support);
            AllPhasePercent();
        }

        protected void gridViewSupport_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.gridViewSupport.EditIndex = -1;
            BindGrid(sender, e, gridViewSupport, (int)PmsCommonEnum.EnumSdpPhase.Support);
            CalculatePercent(gridViewSupport, (int)PmsCommonEnum.EnumSdpPhase.Support);
            AllPhasePercent();
        }

        protected void gridViewSupport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow || (e.Row.RowType == DataControlRowType.Header) || (e.Row.RowType == DataControlRowType.Footer))
                {
                    for (int rowIndex = 0; rowIndex < gridViewSupport.Rows.Count; rowIndex++)
                    {
                        BindDropDownList(gridViewSupport, rowIndex);
                    }

                    #region Hide Columns
                    //if (Stage >= (int)PmsCommonEnum.EnumSdpPhase.Release)
                    //if (IsCrRD == false && UserStatus != "RD LEADER" && UserStatus != "RD MANAGER")                 
                    
                    if (IsCrRD == false)
                    {
                        e.Row.Cells[13].Visible = false;
                        e.Row.Cells[14].Visible = false;
                        e.Row.Cells[15].Visible = false;
                    }

                    if (Stage == (int)PmsCommonEnum.ProjectStage.Closed ||
                        Stage == (int)PmsCommonEnum.ProjectStage.HardClosed ||
                        Stage == (int)PmsCommonEnum.ProjectStage.Pending ||
                        Stage == (int)PmsCommonEnum.ProjectStage.Cancelled)
                      
                    {
                        e.Row.Cells[13].Visible = false;
                        e.Row.Cells[14].Visible = false;
                        e.Row.Cells[15].Visible = false;
                    }
                    #endregion

                    BindChangeEvent(e);

                    InitBindData(e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidatePlanDate(TextBox textBoxPlanStartDay, TextBox textBoxPlanEndDay, int phase, DateTime duedate, string projectType)
        {

            if (textBoxPlanStartDay.Text.Trim() == "" || textBoxPlanEndDay.Text.Trim() == "" || !CheckDueDateValid(duedate))
            {

                return true;
            }
            DateTime planDateFrom;
            DateTime planDateTo;
            //DateTime dueDate;
            //if (!(this.calendarDueDate.Text.Trim() != "" && DateTime.TryParse(this.calendarDueDate.Text, out dueDate)))
            //{
            //    Msgbox("The CR due date is invalid, please contact the PM to modify it!')";
            //    return false;
            //}
            if (!(textBoxPlanStartDay.Text != "" && DateTime.TryParse(textBoxPlanStartDay.Text, out planDateFrom)))
            {
                Msgbox("The planned start date is invalid!");
                return false;
            }
            if (!(textBoxPlanEndDay.Text != "" && DateTime.TryParse(textBoxPlanEndDay.Text, out planDateTo)))
            {
                Msgbox("The planned end date is invalid!");
                return false;
            }

            // TODO:维护SDP,需要判断planned start date 和due date

            // 由于Create Service没有due date,所以通不过验证。

            // 两个解决方案，1.增加due date,2.Service类型不验证。
            // Mark by Ename Wang on 20111126 19:02
            if (projectType != PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
            {
                if (planDateFrom > duedate && phase != (int)PmsCommonEnum.EnumSdpPhase.Support)
                {
                    Msgbox("The planned start date should be less than the CR due date!");
                    return false;
                }
                if (planDateTo > duedate && phase != (int)PmsCommonEnum.EnumSdpPhase.Support)
                {
                    Msgbox("The planned end date should be less than the CR due date!");
                    return false;
                }

            }
            // end Mark 
            if (planDateFrom > planDateTo && phase != (int)PmsCommonEnum.EnumSdpPhase.Support)
            {
                Msgbox("The planned end date should be more than the plan start date!");
                return false;
            }
            if (PmsCommonBiz.NumberOfWeeks(planDateFrom, planDateTo) > 1)
            {
                Msgbox("Task period should not be cross week!");
                return false;
            }
            if (planDateTo.Subtract(planDateFrom).Days + 1 > 3)
            {
                Msgbox("Task duration should be less than 3 days!");
                return false;
            }
            return true;
        }

        private bool CheckDueDateValid(DateTime dueDate)
        {
            DateTime result = new DateTime(1900, 1, 1);
            if (dueDate > result)
            {
                return true;
            }
            return false;
        }
        #endregion

        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                SdpDetail sdpDetail = new SdpDetail();
                sdpDetail.Pmsid = PmsID;
              

                IList<SdpDetail> sdpDetailList = m_SdpDetailBiz.SelectSdpDetail(sdpDetail);
                if (sdpDetailList == null || sdpDetailList.Count <= 0 ||sdpDetailList.Where(t=>t.Planstartday>new DateTime(1901,1,1)).Count()==0)
                {
                    Msgbox("Please maintain SDP before confirm SDP!");
                    return;
                }

                PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                InitPmsHead.SDPConfirmDate = DateTime.Now;
                string releasePhase = ((int)PmsCommonEnum.EnumSdpPhase.Release).ToString();
                sdpDetail.Phase = releasePhase;
                IList<SdpDetail> sdpDetailList1 = sdpDetailBiz.SelectMaxPlanEDate(PmsID, releasePhase);
                if (sdpDetailList1 != null && sdpDetailList1.Count > 0 && sdpDetailList1.Where(t => t.Planendday > new DateTime(1900, 1, 1)).Count() > 0)
                {
                    //marked by Tim on 20121226 for [SDP Confirm can't update DueDate]
                    //InitPmsHead.DueDate = sdpDetailList1[0].Planendday;
                    //End marked 
                }
                else
                {
                    Msgbox("Please maintain SDP of release task before confirm SDP!");
                    return;
                }
                
                bool result = pmsHeadBiz.UpdatePmsHead(InitPmsHead);
                if (!result)
                {
                    Msgbox("Update failed");
                    return;
                }
                MailBiz mailBiz = new MailBiz();
                LoginName.Replace(".", " ");
                InitPmsHead.UserName = LoginName;
                mailBiz.SendPromoteMail(InitPmsHead, (int)PmsCommonEnum.MailType.ConfirmMail);
                ButtonConfirm.Enabled = false;
            }
            catch (Exception)
            {
                Msgbox("Confirm failed");

            }

        }
    }
}