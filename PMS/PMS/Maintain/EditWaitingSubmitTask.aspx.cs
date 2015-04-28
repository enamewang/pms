#region -- Page Information --
//////////////////////////////////////////////////////////////////////////////////
// File name:    
// Copyright (C) 2011 Qisda Corporation.     
// Author:		  ITO.Abel.Li   
// ALTER  Date:   2012/03/21
// Current Version:  1.0
// Description:   behind code of
// History: 
// 
//      Date     |    Time    |    Author   |  Modification 
// 1  2013/12/27 |   11:23:39 |  ITO.Abel.Li|  Create
//////////////////////////////////////////////////////////////////////////////////
#endregion

#region -- Using NameSpace --
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PMS.PMS.AppCode;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.Collections.Generic;
using Qisda.PMS.Common;
using Qisda.Web;
using Titan.WebForm;
#endregion

namespace PMS.PMS.Maintain
{
    public partial class EditWaitingSubmitTask : PageBase
    {
        #region Define Variable
        public string LoginName
        {
            get
            {
                return WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            }
        }
        public SdpDetail SdpDetailPage
        {
            get
            {
                object obj = ViewState["SdpDetailPage"];
                return (obj == null) ? new SdpDetail() : (SdpDetail)ViewState["SdpDetailPage"];
            }
            set
            {
                ViewState["SdpDetailPage"] = value;
            }
        }
        protected Hashtable Roles
        {
            get
            {
                object obj = ViewState["Roles"];
                return (obj == null) ? (new Hashtable()) : (Hashtable)ViewState["Roles"];
            }
            set
            {
                ViewState["Roles"] = value;
            }
        }

        private string PmsId
        {
            get
            {
                object obj = ViewState["PmsId"];
                return (obj == null) ? "" : ViewState["PmsId"].ToString();
            }
            set
            {
                ViewState["PmsId"] = value;
            }
        }

        private string CrID
        {
            get
            {
                object obj = ViewState["CrID"];
                return (obj == null) ? "" : ViewState["CrID"].ToString();
            }
            set
            {
                ViewState["CrID"] = value;
            }
        }

        private string Phase
        {
            get
            {
                object obj = ViewState["Phase"];
                return (obj == null) ? "" : ViewState["Phase"].ToString();
            }
            set
            {
                ViewState["Phase"] = value;
            }
        }
        private int Serial
        {
            get
            {
                object obj = ViewState["Serial"];
                return (obj == null) ? -1 : int.Parse(ViewState["Serial"].ToString());
            }
            set
            {
                ViewState["Serial"] = value;
            }
        }

        private SdpDetail SdpDetailResult
        {
            get
            {
                object obj = ViewState["SdpDetailResult"];
                return (obj == null) ? new SdpDetail() : (SdpDetail)ViewState["SdpDetailResult"];
            }
            set
            {
                ViewState["SdpDetailResult"] = value;
            }
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        #endregion

        #region InitPage
        private void InitPage()
        {
            PmsId = Request.Params["PmsID"];
            CrID = Request.Params["CrId"];
            Phase = Request.Params["Phase"];            
            int serial = 0;
            int.TryParse(Request.Params["Serial"], out serial);
            if (serial == 0)
                return;
            else
                Serial = serial;
            
            IList<PmsHead> pmsHeadList = new PmsHeadBiz().SelectPmsHead(PmsId, "");
            PmsHead ph;
            if (pmsHeadList != null && pmsHeadList.Count > 0)
                ph = pmsHeadList[0];
            else {
                Msgbox("Data is null");
                return;}
                
            //IList<PmsHead> pmsHead = new PmsHeadBiz().SelectPmsHead(PmsId, ""); ;
            //PmsHead ph = pmsHead[0];
            TextBoxCrNo.Text = CrID;
            TextBoxCrName.Text = ph.PmsName;

            Hashtable rTable = new Hashtable();
            rTable.Add("PM", ph.Pm);
            rTable.Add("SD", ph.Sd);
            rTable.Add("SE", ph.Se);
            rTable.Add("QA", ph.Qa);
            ViewState["Roles"] = rTable;            
            foreach (string key in rTable.Keys)
            {
                this.DropDownListRoleInfo.Items.Add(new ListItem(key, rTable[key].ToString()));
            }
            
            PmsCommonEnum.PlanPhase planPhase = (PmsCommonEnum.PlanPhase)System.Enum.Parse(typeof(PmsCommonEnum.PlanPhase), Phase);
            TextBoxPhase.Text = planPhase.GetDescription();
            BindDropDownList();
            EditOutData();
        }
        #endregion

        #region  BindDropDownList
        private void BindDropDownList()
        {
            BindDropDownListOperationType();
            BindDropDownListTaskType();
            BindDropDownListFunctionType();
            BindDropDownListProgramLanguage();
            BindDropDownListTaskComplexity();
            BindDropDownListResource();
        }
        #endregion

        #region  BindDropDownListOperationType
        private void BindDropDownListOperationType()
        {
            PmsSysBiz pmsSysBiz = new PmsSysBiz();
            try
            {
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1Data2ByType("PM", "OperationType");
                this.DropDownListOperationType.DataSource = pmsSysList;
                this.DropDownListOperationType.DataTextField = "Data2";
                this.DropDownListOperationType.DataValueField = "Data1";
                this.DropDownListOperationType.DataBind();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind TaskType failure');", true);
                this.DropDownListTaskType.Focus();
            }
        }
        #endregion

        #region  BindDropDownListTaskType
        private void BindDropDownListTaskType()
        {
            PmsSysBiz pmsSysBiz = new PmsSysBiz();
            try
            {
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData2Data3ByType("PM", "TaskType", Phase);
                this.DropDownListTaskType.DataSource = pmsSysList;
                this.DropDownListTaskType.DataTextField = "Data3";
                this.DropDownListTaskType.DataValueField = "Data2";
                this.DropDownListTaskType.DataBind();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind TaskType failure');", true);
                this.DropDownListTaskType.Focus();
            }
        }
        #endregion

        #region  BindDropDownListFunctionType
        private void BindDropDownListFunctionType()
        {
            PmsSysBiz pmsSysBiz = new PmsSysBiz();
            try
            {
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1Data2ByType("PM", "FunctionType");
                this.DropDownListFunctionType.DataSource = pmsSysList;
                this.DropDownListFunctionType.DataTextField = "Data2";
                this.DropDownListFunctionType.DataValueField = "Data1";
                this.DropDownListFunctionType.DataBind();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind FunctionType failure');", true);
                this.DropDownListFunctionType.Focus();
            }
        }
        #endregion

        #region BindDropDownListProgramLanguage
        private void BindDropDownListProgramLanguage()
        {
            PmsSysBiz pmsSysBiz = new PmsSysBiz();

            try
            {
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1Data2ByType("PM", "ProgramLanguage");
                this.DropDownListProgramLanguage.DataSource = pmsSysList;
                this.DropDownListProgramLanguage.DataTextField = "Data2";
                this.DropDownListProgramLanguage.DataValueField = "Data1";
                this.DropDownListProgramLanguage.DataBind();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind ProgramLanguage failure');", true);
                this.DropDownListProgramLanguage.Focus();
            }
        }
        #endregion

        #region  BindDropDownListTaskComplexity
        private void BindDropDownListTaskComplexity()
        {
            PmsSysBiz pmsSysBiz = new PmsSysBiz();
            try
            {
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1Data2ByType("PM", "TaskComplexity");
                this.DropDownListTaskComplexity.DataSource = pmsSysList;
                this.DropDownListTaskComplexity.DataTextField = "Data2";
                this.DropDownListTaskComplexity.DataValueField = "Data1";
                this.DropDownListTaskComplexity.DataBind();

                this.DropDownListTaskComplexity.Items.Insert(0, new ListItem());
                this.DropDownListTaskComplexity.Items[0].Text = "";
                this.DropDownListTaskComplexity.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind TaskComplexity failure');", true);
                this.DropDownListTaskComplexity.Focus();
            }
        }
        #endregion

        #region BindDropDownListResource
        private void BindDropDownListResource()
        {
            Hashtable ha = Roles;
            List<string> resourceList = new List<string>();
            try
            {
                foreach (string str in ha.Values)
                {
                    string[] sArray = str.Split(';');
                    foreach (string name in sArray)
                    {
                        if (!resourceList.Contains(name))
                            resourceList.Add(name);
                    }
                }
                this.DropDownListResource.DataSource = resourceList;
                this.DropDownListResource.DataBind();

                this.DropDownListResource.Items.Insert(0, new ListItem());
                this.DropDownListResource.Items[0].Text = "";
                this.DropDownListResource.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind Resource failure');", true);
                this.DropDownListResource.Focus();
            }
        }
        #endregion

        #region EditOutData
        private void EditOutData()
        {
            SdpDetail sdpDetailParms = new SdpDetail();
            sdpDetailParms.Serial = Serial;
            sdpDetailParms.Pmsid = PmsId;
            SdpDetailResult = new SdpDetailBiz().SelectSdpDetail(sdpDetailParms)[0];

            this.TextBoxAuditStatus.Text = SdpDetailResult.AuditStatusDesc;
            this.TextBoxTaskStatus.Text = SdpDetailResult.TaskStatusDesc;
            this.TextBoxTaskName.Text = SdpDetailResult.TaskName;
            this.DropDownListOperationType.SelectedValue = SdpDetailResult.OperationType.ToString();
            this.DropDownListTaskType.SelectedValue = SdpDetailResult.TaskType.ToString();
            this.DropDownListFunctionType.SelectedValue = SdpDetailResult.FunctionType.ToString();
            this.DropDownListProgramLanguage.SelectedValue = SdpDetailResult.ProgramLanguage.ToString();
            this.DropDownListTaskComplexity.SelectedValue = SdpDetailResult.TaskComplexity.ToString();
            this.TextBoxRefCost.Text = (SdpDetailResult.Refcost.ToString() == "0") ? "" : SdpDetailResult.Refcost.ToString();
            this.TextBoxPlanCost.Text = (SdpDetailResult.Plancost.ToString() == "0") ? "" : SdpDetailResult.Plancost.ToString();
            this.DateTextBoxPlanStartDate.Text = new PmsCommonBiz().FormatDateTime(SdpDetailResult.Planstartday.ToString("yyyy-MM-dd").Trim());
            this.DateTextBoxPlanEndDate.Text = new PmsCommonBiz().FormatDateTime(SdpDetailResult.Planendday.ToString("yyyy-MM-dd").Trim());
            this.DropDownListResource.SelectedValue = SdpDetailResult.Resource;
            this.DropDownListRole.Items.Add(new ListItem(SdpDetailResult.Role, SdpDetailResult.Role));
            this.DropDownListRole.SelectedValue = SdpDetailResult.Role;            
            this.TextBoxPlanRemark.Text = SdpDetailResult.ScheduleRemark;
        }
        #endregion

        #region Save
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
            SdpDetail sdpDetailPage = SdpDetailResult;
            DateTime dateTime = PmsSysBiz.GetDBDateTime();

            //Check 日期是否跨周、大于三天、超过dudate
            PmsHead objHead = new PmsHeadBiz().SelectPmsHeadByPmsId(PmsId)[0];
            if (!ValidatePlanDate((TextBox)DateTextBoxPlanStartDate, (TextBox)DateTextBoxPlanEndDate, int.Parse(Phase), objHead.DueDate, objHead.Type))
            {
                return;
            }

            //如果任务类型为 Review Meeting 不检查任务名和资源等重复
            if (DropDownListTaskType.SelectedItem.Text.Trim() != "Review Meeting")
            {
                // 当TaskName或Resource发生改变时Check TaskName和Resource不能重复
                if (SdpDetailResult.TaskName != TextBoxTaskName.Text.Trim() || SdpDetailResult.Resource != DropDownListResource.Text.Trim())
                {
                    SdpDetail sdpDetail = new SdpDetail();
                    sdpDetail.Pmsid = PmsId;
                    sdpDetail.TaskName = TextBoxTaskName.Text.Trim();
                    sdpDetail.Phase = Phase.ToString();
                    sdpDetail.Role = DropDownListRole.Text.Trim();
                    sdpDetail.Resource = DropDownListResource.Text.Trim();
                    IList<SdpDetail> sdpDetailList = sdpDetailBiz.SelectSdpDetail(sdpDetail);
                    if (sdpDetailList != null && sdpDetailList.Count > 0)
                    {
                        if (sdpDetailList[0].TaskName == sdpDetail.TaskName && sdpDetailList[0].Resource == sdpDetail.Resource)
                        {
                            Msgbox("The same taskname and  resource has been exist!");
                            return;
                        }
                    }
                }
            }
            sdpDetailPage.TaskName = TextBoxTaskName.Text.Trim();
            sdpDetailPage.TaskType = int.Parse(DropDownListTaskType.Text.Trim());
            sdpDetailPage.FunctionType = int.Parse(DropDownListFunctionType.Text.Trim());
            sdpDetailPage.ProgramLanguage = int.Parse(DropDownListProgramLanguage.Text.Trim());
            sdpDetailPage.TaskComplexity = int.Parse(DropDownListTaskComplexity.Text.Trim());
            //sdpDetailPage.Actualcost = double.Parse(TextBoxRefCost.Text.Trim());
            sdpDetailPage.Refcost = double.Parse(HiddenFieldRefCost.Value.Trim());
            sdpDetailPage.Plancost = double.Parse(TextBoxPlanCost.Text.Trim());
            sdpDetailPage.Planstartday = DateTime.Parse(DateTextBoxPlanStartDate.Text.Trim());
            sdpDetailPage.Planendday = DateTime.Parse(DateTextBoxPlanEndDate.Text.Trim());
            sdpDetailPage.Resource = DropDownListResource.Text.Trim();
            sdpDetailPage.Auditor = new BaseDataUserBiz().SelectLeaderByLoginName(LoginName);
            HiddenFieldAuditor.Value = sdpDetailPage.Auditor;
            //sdpDetailPage.Role = DropDownListRole.Text.Trim();
            sdpDetailPage.Role = HiddenFieldRole.Value.Trim();
            sdpDetailPage.ScheduleRemark = TextBoxPlanRemark.Text.Trim();
            sdpDetailPage.Maintaindate = dateTime;
            sdpDetailPage.Maintainuser = LoginName;

            //获得对应AuditStatus
            //SdpDetail sParms = new SdpDetail();
            //sParms.Serial = Serial;
            //SdpDetail sResult = new SdpDetailBiz().SelectSdpDetail(sParms)[0];
            if (SdpDetailResult.AuditStatus == 0) {
                sdpDetailPage.AuditStatus = 1;
                sdpDetailPage.TaskStatus = 1;                
                sdpDetailPage.Iseditable = "Y";
                sdpDetailPage.Deleteflag = "N";               
                sdpDetailPage.Createdate = dateTime;
                sdpDetailPage.Createuser = LoginName;               
            }
            else
            {
                int auditStatus = GetAuditStatus(SdpDetailResult, sdpDetailPage);
                if (auditStatus == -1)
                    return;
                else
                    sdpDetailPage.AuditStatus = auditStatus;
            }

            if (sdpDetailBiz.UpdateSdpDetail(sdpDetailPage))
            {
                SdpDetailPage = sdpDetailPage;
                HiddenFieldAuditStatus.Value = sdpDetailPage.AuditStatus.ToString();
                HiddenFieldAuditStatusDesc.Value = System.Enum.Parse(typeof(PmsCommonEnum.AuditStatus), sdpDetailPage.AuditStatus.ToString()).GetDescription();
                HiddenFieldTaskStatus.Value = sdpDetailPage.TaskStatus.ToString();
                HiddenFieldTaskStatusDesc.Value = System.Enum.Parse(typeof(PmsCommonEnum.TaskStatus), sdpDetailPage.TaskStatus.ToString()).GetDescription();


                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "SaveSuccess();", true);
            }
            else
            {
                Msgbox("Update this item failed!");
                return;
            }
            // 更新head表的PlanStartDate   
            new PmsHeadBiz().UpdatePmsHeadPlanStartDate(PmsId);
        }
        #endregion

        #region GetAuditStatus
        private int GetAuditStatus(SdpDetail sdpDetailResult, SdpDetail sdpDetailPage)
        {
            int AuditStatus = -1;
            switch (sdpDetailResult.AuditStatus)
            {
                case 1:
                    AuditStatus = 1;
                    break;
                case 2:
                    SdpDetail sParms = new SdpDetail();
                    sParms.Serial = Serial;
                    IList<SdpDetail> sdpDetailList = new SdpDetailBiz().SelectSdpDetail(sParms);
                    if (sdpDetailList[0].AuditStatus != 2)
                        Msgbox("Audit status has changed, please refresh. ");
                    else
                        AuditStatus = 2;
                    break;
                case 3:
                    if (sdpDetailResult.TaskStatus == 1)
                        AuditStatus = 1;
                    if (sdpDetailResult.TaskStatus == 3
                        || sdpDetailResult.TaskStatus == 4
                        || sdpDetailResult.TaskStatus == 5
                        || sdpDetailResult.TaskStatus == 6)
                    {
                        Msgbox("This audit status can not save! ");
                        break;
                    }
                    if (sdpDetailResult.TaskStatus == 2)
                    {
                        if (hidvalueIsSave.Value != "Y")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "Test();", true);
                            break;
                        }
                        else
                        {
                            SdpDetail oldParms = new SdpDetail();
                            oldParms.Serial = Serial;
                            SdpDetail oldSdpDetail = new SdpDetailBiz().SelectSdpDetail(oldParms)[0];
                            if (oldSdpDetail.Plancost != sdpDetailPage.Plancost
                           || oldSdpDetail.Planstartday != sdpDetailPage.Planstartday
                           || oldSdpDetail.Planendday != sdpDetailPage.Planendday)
                            {
                                AuditStatus = 1;
                                break;
                            }
                            else AuditStatus = oldSdpDetail.AuditStatus;
                        }
                    }
                    break;
                case 4:
                    AuditStatus = 1;
                    break;
            }
            return AuditStatus;
        }
        #endregion

        #region ValidateSave
        private bool ValidatePlanDate(TextBox textBoxPlanStartDay, TextBox textBoxPlanEndDay, int phase, DateTime duedate, string projectType)
        {
            if (textBoxPlanStartDay.Text.Trim() == "" || textBoxPlanEndDay.Text.Trim() == "" || !CheckDueDateValid(duedate))
            {
                return true;
            }
            DateTime planDateFrom;
            DateTime planDateTo;

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
    }
}
