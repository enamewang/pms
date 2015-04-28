#region -- Page Information --
//////////////////////////////////////////////////////////////////////////////////
// File name:    
// Copyright (C) 2013 Qisda Corporation.     
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
    public partial class AddNewTask : PageBase
    {
        #region  Define Variable
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
        public string PmsId
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
        public string CrID
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
        public string Phase
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
        public int TaskSerial
        {
            get
            {
                object obj = ViewState["TaskSerial"];
                return (obj == null) ? -1 : int.Parse(ViewState["TaskSerial"].ToString());
            }
            set
            {
                ViewState["TaskSerial"] = value;
            }
        }
        public PmsHead objPmsHead;
        public PmsHead ObjPmsHead
        {
            get
            {
                return (objPmsHead == null) ? new PmsHead() : objPmsHead;
            }
            set
            {
                objPmsHead = value;
            }
        }

        public string LoginName
        {
            get
            {
                return WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            }
        }
        public string Service
        {
            get
            {
                return PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription();
            }
        }
        public string Support
        {
            get
            {
                return ((int)PmsCommonEnum.EnumSdpPhase.Support).ToString();
            }
        }
        public bool PhaseNull;
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
            if (string.IsNullOrEmpty(Request.Params["Phase"]))
                PhaseNull = true;
            Phase = Request.Params["Phase"];
            string Action = Request.Params["Action"];           

            IList<PmsHead> pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(PmsId);
            if (pmsHead != null && pmsHead.Count > 0)
                ObjPmsHead = pmsHead[0];
            else {
                Msgbox("Data Bind Error!");
                return;
            }
            TextBoxCrNo.Text = CrID;
            TextBoxCrName.Text = ObjPmsHead.PmsName;

            Hashtable rTable = new Hashtable();
            rTable.Add("PM", ObjPmsHead.Pm);
            rTable.Add("SD", ObjPmsHead.Sd);
            rTable.Add("SE", ObjPmsHead.Se);
            rTable.Add("QA", ObjPmsHead.Qa);
            ViewState["Roles"] = rTable;
            foreach (string key in rTable.Keys)
            {
                this.DropDownListRoleInfo.Items.Add(new ListItem(key, rTable[key].ToString()));
            }
            if (!PhaseNull)
            {
                PmsCommonEnum.PlanPhase planPhase = (PmsCommonEnum.PlanPhase)System.Enum.Parse(typeof(PmsCommonEnum.PlanPhase), Phase);
                TextBoxPhase.Text = planPhase.GetDescription();
                TextBoxPhase.Visible = true;
                LabelPhaseMark.Visible = false;
                DropDownListPhases.Visible = false;
            }
            BindDropDownList();
            if (Action != "Add")
                EditOutData();
        }
        #endregion

        #region  BindDropDownList
        private void BindDropDownList()
        {
            if (string.IsNullOrEmpty(Request.Params["Phase"]))
            {
                BindDropDownListPhases();
                TextBoxPhase.Visible = false;
                LabelPhaseMark.Visible = true;
                DropDownListPhases.Visible = true;
            }
            BindDropDownListOperationType();
            BindDropDownListTaskType();
            BindDropDownListFunctionType();
            BindDropDownListProgramLanguage();
            BindDropDownListTaskComplexity();
            BindDropDownListResource();
            switch (Phase) { 
                case "4":
                    EditOutDataByDesign();
                    break;
                case "5":
                    EditOutDataByDevelopment();
                    break;
                case "6":
                    EditOutDataByTest();
                    break;
                case "7":
                    EditOutDataByRelease();
                    break;
                case "8":
                    EditOutDataSupport();
                    break;
            }          
        }
        #endregion

        #region  BindDropDownListPhases
        private void BindDropDownListPhases()
        {
            try
            {
                Dictionary<string, string> phases = new PmsCommonEnum().GetEnumValueAndDesc(typeof(PmsCommonEnum.PlanPhase));
                if (phases.Keys.Contains("PES阶段"))
                    phases.Remove("PES阶段");
                if (phases.Keys.Contains("上线实施阶段"))
                    phases.Remove("上线实施阶段");
                this.DropDownListPhases.DataSource = phases;                
                this.DropDownListPhases.DataTextField = "Key";
                this.DropDownListPhases.DataValueField = "Value";
                this.DropDownListPhases.DataBind();

                this.DropDownListPhases.Items.Insert(0, new ListItem());
                this.DropDownListPhases.Items[0].Text = "";
                this.DropDownListPhases.Items[0].Value = "";
                this.DropDownListPhases.SelectedIndex = 0;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind TaskPhases failure');", true);
                this.DropDownListPhases.Focus();
            }
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
            sdpDetailParms.Serial = int.Parse(Request.Params["Serial"]);
            sdpDetailParms.Pmsid = PmsId;
            SdpDetail SdpDetailResult = new SdpDetailBiz().SelectSdpDetail(sdpDetailParms)[0];

            this.TextBoxTaskName.Text = SdpDetailResult.TaskName;
            this.DropDownListTaskType.SelectedValue = SdpDetailResult.TaskType.ToString();
            this.DropDownListFunctionType.SelectedValue = SdpDetailResult.FunctionType.ToString();
            this.DropDownListProgramLanguage.SelectedValue = SdpDetailResult.ProgramLanguage.ToString();
            this.DropDownListTaskComplexity.SelectedValue = SdpDetailResult.TaskComplexity.ToString();
            this.TextBoxRefCost.Text = SdpDetailResult.Refcost.ToString();
            this.TextBoxPlanCost.Text = SdpDetailResult.Plancost.ToString();
            this.DateTextBoxPlanStartDate.Text = new PmsCommonBiz().FormatDateTime(SdpDetailResult.Planstartday.ToString("yyyy-MM-dd").Trim());
            this.DateTextBoxPlanEndDate.Text = new PmsCommonBiz().FormatDateTime(SdpDetailResult.Planendday.ToString("yyyy-MM-dd").Trim());
            this.DropDownListResource.SelectedValue = SdpDetailResult.Resource;
            this.DropDownListRole.SelectedValue = SdpDetailResult.Role;
            this.DropDownListRole.Items.Add(new ListItem(SdpDetailResult.Role, SdpDetailResult.Role));
            this.TextBoxPlanRemark.Text = SdpDetailResult.ScheduleRemark;
        }
        #endregion

        #region EditOutDataByDesign
        private void EditOutDataByDesign()
        {
            this.DropDownListOperationType.SelectedValue = "1";
            this.DropDownListTaskType.SelectedValue = "4";
            this.DropDownListFunctionType.SelectedValue = "1";
            this.DropDownListProgramLanguage.SelectedValue = "3";
            this.DropDownListTaskComplexity.SelectedValue = "2";
            this.TextBoxRefCost.Text = GetRefCost("1", "4", "2", "3", "2");
            this.DropDownListResource.SelectedValue = LoginName;
        }
        #endregion

        #region EditOutDataByDevelopment
        private void EditOutDataByDevelopment()
        {
            this.DropDownListOperationType.SelectedValue = "1";
            this.DropDownListTaskType.SelectedValue = "5";
            this.DropDownListFunctionType.SelectedValue = "1";
            this.DropDownListProgramLanguage.SelectedValue = "2";
            this.DropDownListTaskComplexity.SelectedValue = "3";
            this.TextBoxRefCost.Text = GetRefCost("1", "5", "1", "2", "3");
        }
        #endregion

        #region EditOutDataByTest
        private void EditOutDataByTest()
        {
            this.DropDownListOperationType.SelectedValue = "1";
            this.DropDownListTaskType.SelectedValue = "10";
            this.DropDownListFunctionType.SelectedValue = "1";
            this.DropDownListProgramLanguage.SelectedValue = "3";
            this.DropDownListTaskComplexity.SelectedValue = "3";
            this.TextBoxRefCost.Text = GetRefCost("1", "10", "1", "3", "3");
            this.DropDownListResource.SelectedValue = LoginName;
        }
        #endregion

        #region EditOutDataByRelease
        private void EditOutDataByRelease()
        {
            this.DropDownListOperationType.SelectedValue = "1";
            this.DropDownListTaskType.SelectedValue = "9";            
            this.DropDownListFunctionType.SelectedValue = "6";
            this.DropDownListProgramLanguage.SelectedValue = "3";
            this.DropDownListTaskComplexity.SelectedValue = "3";
            this.TextBoxRefCost.Text =GetRefCost("1","9","6","3","3");
            this.DropDownListResource.SelectedValue = LoginName;
        }
        #endregion

        #region EditOutDataSupport
        private void EditOutDataSupport()
        {
            this.DropDownListOperationType.SelectedValue = "1";
            this.DropDownListTaskType.SelectedValue = "12";
            this.DropDownListFunctionType.SelectedValue = "6";
            this.DropDownListProgramLanguage.SelectedValue = "3";
            this.DropDownListTaskComplexity.SelectedValue = "3";
            this.TextBoxRefCost.Text = GetRefCost("1", "9", "6", "3", "3");
            this.DropDownListResource.SelectedValue = LoginName;
        }
        #endregion

        #region Save
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
            SdpDetail sdpDetail = new SdpDetail();
            DateTime dateTime = PmsSysBiz.GetDBDateTime();

            sdpDetail.Pmsid = PmsId;
            sdpDetail.Phase = Phase;
            if (PhaseNull)
                sdpDetail.Phase = DropDownListPhases.Text.Trim();
            sdpDetail.TaskName = TextBoxTaskName.Text.Trim();
            sdpDetail.OperationType = int.Parse(DropDownListOperationType.Text.Trim());
            sdpDetail.TaskType = int.Parse(DropDownListTaskType.Text.Trim());
            sdpDetail.FunctionType = int.Parse(DropDownListFunctionType.Text.Trim());
            sdpDetail.ProgramLanguage = int.Parse(DropDownListProgramLanguage.Text.Trim());
            sdpDetail.TaskComplexity = int.Parse(DropDownListTaskComplexity.Text.Trim());
            //sdpDetail.Actualcost = double.Parse(TextBoxRefCost.Text.Trim());
            sdpDetail.Refcost = double.Parse(HiddenFieldRefCost.Value.Trim());
            sdpDetail.Plancost = double.Parse(TextBoxPlanCost.Text.Trim());
            sdpDetail.Auditor = new BaseDataUserBiz().SelectLeaderByLoginName(LoginName);
            HiddenFieldAuditor.Value = sdpDetail.Auditor;
            IList<PmsSys> pmsSysList = new PmsSysBiz().SelectData2ByTypeData1("PM", "AuditAgent", sdpDetail.Auditor);
            sdpDetail.AuditorAgent = (pmsSysList == null || pmsSysList.Count == 0) ? "" : pmsSysList.FirstOrDefault().Data2;
            HiddenFieldAuditorAgent.Value = sdpDetail.AuditorAgent;
            sdpDetail.Planstartday = DateTime.Parse(DateTextBoxPlanStartDate.Text.Trim());
            sdpDetail.Planendday = DateTime.Parse(DateTextBoxPlanEndDate.Text.Trim());
            sdpDetail.Resource = DropDownListResource.Text.Trim();
            //sdpDetail.Role = DropDownListRole.Text.Trim();
            sdpDetail.Role = HiddenFieldRole.Value.Trim();
            sdpDetail.ScheduleRemark = TextBoxPlanRemark.Text.Trim();
            sdpDetail.AuditStatus = 1;
            sdpDetail.TaskStatus = 1;
            sdpDetail.Taskno = 1;
            sdpDetail.Iseditable = "Y";
            sdpDetail.Deleteflag = "N";
            sdpDetail.Createdate = dateTime;
            sdpDetail.Createuser = LoginName;
            sdpDetail.Maintaindate = dateTime;
            sdpDetail.Maintainuser = LoginName;

            TaskSerial = sdpDetailBiz.InsertSdpDetail(sdpDetail);
            HiddenFieldSerial.Value = TaskSerial.ToString();
            if (TaskSerial > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "SaveSuccess();", true);
            }
            else
            {
                Msgbox("Save the new item failed!");
                return;
            }
            //更新head表的PlanStartDate   
            new PmsHeadBiz().UpdatePmsHeadPlanStartDate(PmsId);
        }
        #endregion

        #region DropDownListPhases_SelectedIndexChanged
        protected void DropDownListPhases_SelectedIndexChanged(object sender, EventArgs e)
        {
            Phase = this.DropDownListPhases.SelectedValue;
            BindDropDownListTaskType();
            switch (Phase)
            {
                case "4":
                    EditOutDataByDesign();
                    break;
                case "5":
                    EditOutDataByDevelopment();
                    break;
                case "6":
                    EditOutDataByTest();
                    break;
                case "7":
                    EditOutDataByRelease();
                    break;
                case "8":
                    EditOutDataSupport();
                    break;
            }  
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "Refresh();", true);
        }
        #endregion

        #region GetRefCost
        public string GetRefCost(string operationType, string taskType,string functionType, string programLanguage, string complexity)
        {           
            PmsSdpRefcost sdpRefcost = new PmsSdpRefcost();
            sdpRefcost.OperationType = int.Parse(operationType);
            sdpRefcost.TaskComplexity = int.Parse(complexity);
            sdpRefcost.Tasktype = int.Parse(taskType);
            sdpRefcost.Functiontype = int.Parse(functionType);
            sdpRefcost.Programlanguage = int.Parse(programLanguage);
            IList<PmsSdpRefcost> sdpRefcostlList = new PmsSdpRefcostBiz().SelectPmsSdpRefcost(sdpRefcost);
            if (sdpRefcostlList == null || sdpRefcostlList.Count == 0)
                return "";
            else
                return sdpRefcostlList[0].Refcost.ToString();          
        }

        #endregion
    }
}
