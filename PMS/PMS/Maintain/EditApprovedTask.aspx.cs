
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
using Qisda.PMS.Entity;
using Qisda.PMS.Business;
using System.Collections.Generic;
using Qisda.PMS.Common;

namespace PMS.PMS.Maintain
{

    public partial class EditApprovedTask : PageBase
    {
        #region Define Variable
        public string LoginName
        {
            get
            {
                return WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
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
        protected SdpDetail SdpDetailResult
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
            Serial = int.Parse(Request.Params["Serial"]);
            string PmsId = Request.Params["PmsID"];
            string crId = Request.Params["CrId"];
            //Abel test用
            //Serial = 697;
            //PmsId = "PMS201303010003";

            SdpDetail sdpDetailParms = new SdpDetail();
            sdpDetailParms.Serial = Serial;
            IList<PmsHead> pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(PmsId);
            ;
            PmsHead ph = pmsHead[0];
            TextBoxCrNo.Text = crId;
            TextBoxCrName.Text = ph.PmsName;
            IList<SdpDetail> sdpDetailList = new SdpDetailBiz().SelectSdpDetail(sdpDetailParms);
            if (sdpDetailList == null && sdpDetailList.Count == 0)
            {
                Msgbox("Data is null");
                return;
            }
            SdpDetailResult = (sdpDetailList == null || sdpDetailList.Count == 0) ? new SdpDetail() : sdpDetailList[0];
            ApprovedOutData();
            ControlEnabled(SdpDetailResult.TaskStatus);
        }
        #endregion
        #region ApprovedOutData
        private void ApprovedOutData()
        {
            PmsCommonEnum.PlanPhase planPhase = (PmsCommonEnum.PlanPhase)System.Enum.Parse(typeof(PmsCommonEnum.PlanPhase), SdpDetailResult.Phase);
            TextBoxPhase.Text = planPhase.GetDescription();

            this.TextBoxAuditStatus.Text = SdpDetailResult.AuditStatusDesc;
            this.TextBoxTaskStatus.Text = SdpDetailResult.TaskStatusDesc;
            this.TextBoxTaskName.Text = SdpDetailResult.TaskName;
            this.DropDownListOperationType.Items.Add(new ListItem(SdpDetailResult.OperationTypeDesc, SdpDetailResult.OperationType.ToString()));
            this.DropDownListTaskType.Items.Add(new ListItem(SdpDetailResult.TaskTypeDesc, SdpDetailResult.TaskType.ToString()));
            this.DropDownListFunctionType.Items.Add(new ListItem(SdpDetailResult.FunctionTypeDesc, SdpDetailResult.FunctionType.ToString()));
            this.DropDownListProgramLanguage.Items.Add(new ListItem(SdpDetailResult.ProgramLanguageDesc, SdpDetailResult.ProgramLanguage.ToString()));
            this.DropDownListTaskComplexity.Items.Add(new ListItem(SdpDetailResult.TaskComplexityDesc, SdpDetailResult.TaskComplexity.ToString()));
            this.TextBoxRefCost.Text = (SdpDetailResult.Refcost.ToString() == "0") ? "" : SdpDetailResult.Refcost.ToString();
            this.TextBoxPlanCost.Text = (SdpDetailResult.Plancost.ToString() == "0") ? "" : SdpDetailResult.Plancost.ToString();
            this.DateTextBoxPlanStartDate.Text = new PmsCommonBiz().FormatDateTime(SdpDetailResult.Planstartday.ToString("yyyy-MM-dd").Trim());
            this.DateTextBoxPlanEndDate.Text = new PmsCommonBiz().FormatDateTime(SdpDetailResult.Planendday.ToString("yyyy-MM-dd").Trim());
            this.DateTextBoxActualStartDate.Text = new PmsCommonBiz().FormatDateTime(SdpDetailResult.Actualstartday.ToString("yyyy-MM-dd").Trim());
            this.DateTextBoxActualEndDate.Text = new PmsCommonBiz().FormatDateTime(SdpDetailResult.Actualendday.ToString("yyyy-MM-dd").Trim());
            this.TextBoxActualCost.Text = (SdpDetailResult.Actualcost.ToString() == "0") ? "" : SdpDetailResult.Actualcost.ToString();
            this.TextBoxCompletionRate.Text = (SdpDetailResult.Completedpercent.ToString() == "0") ? "" : SdpDetailResult.Completedpercent.ToString();
            this.DropDownListResource.Items.Add(new ListItem(SdpDetailResult.Resource, SdpDetailResult.Resource));
            this.DropDownListRole.Items.Add(new ListItem(SdpDetailResult.Role, SdpDetailResult.Role));
            this.TextBoxExecuteRemark.Text = SdpDetailResult.ExecuteRemark;
            this.HiddenFieldResultTaskStatus.Value = SdpDetailResult.TaskStatus.ToString();
        }
        #endregion

        #region ControlEnabled
        private void ControlEnabled(int taskStatus)
        {
            this.btnSave.Enabled = false;
            this.btnPending.Enabled = false;
            this.bthCancelled.Enabled = false;
            this.btnHardClose.Enabled = false;
            this.bthReactive.Enabled = false;
            this.DateTextBoxActualStartDate.Enabled = false;
            this.DateTextBoxActualEndDate.Enabled = false;
            this.TextBoxActualCost.Enabled = false;
            this.TextBoxCompletionRate.Enabled = false;
            if (taskStatus == 1 || taskStatus == 2 || taskStatus == 3)
            {
                this.btnSave.Enabled = true;
                this.DateTextBoxActualStartDate.Enabled = true;
                this.DateTextBoxActualEndDate.Enabled = true;
                this.TextBoxActualCost.Enabled = true;
                this.TextBoxCompletionRate.Enabled = true;
            }
            if (taskStatus == 1 || taskStatus == 2)
            {
                this.btnPending.Enabled = true;
            }
            if (taskStatus == 1 || taskStatus == 2 || taskStatus == 6)
            {
                this.bthCancelled.Enabled = true;
            }
            if (taskStatus == 2 || taskStatus == 6)
            {
                this.btnHardClose.Enabled = true;
            }
            if (taskStatus == 4 || taskStatus == 5 || taskStatus == 6)
                this.bthReactive.Enabled = true;

            //if (this.btnSave.Enabled || this.bthReactive.Enabled) {
            //    this.DateTextBoxActualStartDate.Enabled = true;
            //    this.DateTextBoxActualEndDate.Enabled = true;
            //    this.TextBoxActualCost.Enabled = true;
            //    this.TextBoxCompletionRate.Enabled = true;            
            //}            
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool updateTrue;
            if (this.HiddenButtonName.Value == "Save" || this.HiddenButtonName.Value == "Reactive" || this.HiddenButtonName.Value == "Pending")
            {
                SdpDetailResult.Actualstartday = (this.DateTextBoxActualStartDate.Text.Trim() == "") ? new DateTime() : DateTime.Parse(this.DateTextBoxActualStartDate.Text.Trim());
                SdpDetailResult.Actualendday = (this.DateTextBoxActualEndDate.Text.Trim() == "") ? new DateTime() : DateTime.Parse(this.DateTextBoxActualEndDate.Text.Trim());
                SdpDetailResult.Actualcost = (this.TextBoxActualCost.Text.Trim() == "") ? 0 : double.Parse(this.TextBoxActualCost.Text.Trim());
                SdpDetailResult.Completedpercent = (this.HiddenPercent.Value == "") ? 0 : double.Parse(this.HiddenPercent.Value);
                SdpDetailResult.ExecuteRemark = this.TextBoxExecuteRemark.Text;
                SdpDetailResult.TaskStatus = int.Parse(this.HiddenTaskStatus.Value);

                updateTrue = new SdpDetailBiz().UpdateSdpDetail(SdpDetailResult);
            }
            else if (this.HiddenButtonName.Value == "HardClose")
            {
                SdpDetailResult.TaskStatus = int.Parse(this.HiddenTaskStatus.Value);
                SdpDetailResult.Actualendday = PmsSysBiz.GetDBDateTime();
                SdpDetailResult.Completedpercent = 100d;

                updateTrue = new SdpDetailBiz().UpdateSdpDetail(SdpDetailResult);
            }
            else
            {
                SdpDetailResult.TaskStatus = int.Parse(this.HiddenTaskStatus.Value);
                updateTrue = new SdpDetailBiz().UpdateSdpDetail(SdpDetailResult);
            }

            if (this.HiddenButtonName.Value == "Save" && updateTrue)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "SaveSuccess();", true);
                //更新head表的ActualStartDate
                new PmsHeadBiz().UpdatePmsHeadActualStartDate(SdpDetailResult.Pmsid);
            }
            if (!updateTrue) {
                Msgbox("Update fails!");
            }

            SdpDetail sdpDetailParms = new SdpDetail();
            sdpDetailParms.Serial = Serial;

            SdpDetailResult = new SdpDetailBiz().SelectSdpDetail(sdpDetailParms)[0];
            this.TextBoxTaskStatus.Text = SdpDetailResult.TaskStatusDesc;
            this.HiddenFieldResultTaskStatus.Value = SdpDetailResult.TaskStatus.ToString();
            ControlEnabled(SdpDetailResult.TaskStatus);
            InitPage();

        }
    }
}
