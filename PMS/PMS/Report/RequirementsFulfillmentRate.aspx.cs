using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using WSC.Common;
using Microsoft.Reporting.WebForms;
using Qisda.Common.ReportingServices;
using System.Configuration;

namespace PMS.PMS.Maintain
{
    public partial class RequirementsFulfillmentRate : System.Web.UI.Page
    {
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
            BindDropDownList();
        }
        #endregion

        #region BindDropDownList
        private void BindDropDownList()
        {
            try
            {

                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "UserDept");

                DropDownListUserDept.DataSource = pmsSysList;
                DropDownListUserDept.DataTextField = "data1";
                DropDownListUserDept.DataValueField = "data1";
                DropDownListUserDept.DataBind();

                DropDownListUserDept.Items.Insert(0, new ListItem());
                DropDownListUserDept.Items[0].Text = "";
                DropDownListUserDept.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind DropDownList failure');", true);
            }
        }
        #endregion

        #region ButtonInquiry_Click
        protected void ButtonInquiry_Click(object sender, EventArgs e)
        {
            BindReportViewer();
        }

        private void BindReportViewer()
        {
            try
            {
                string userDept = DropDownListUserDept.SelectedValue.Trim();
                string monthFrom = TextBoxYearMonthFrom.Text.Trim();
                string monthTo = TextBoxYearMonthTo.Text.Trim();
                string reportPath = GetReportPath();
                SetReportViewParameter(ReportViewer1, reportPath);
                ReportParameter[] reportParameter = new ReportParameter[3];
                reportParameter[0] = new ReportParameter("UserDept", userDept);
                reportParameter[1] = new ReportParameter("MonthFrom", monthFrom);
                reportParameter[2] = new ReportParameter("MonthTo", monthTo);

                ReportViewer1.ServerReport.SetParameters(reportParameter);
                ReportViewer1.ServerReport.Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        private void SetReportViewParameter(ReportViewer reportViewer, string reportPath)
        {
            reportViewer.ServerReport.ReportServerCredentials = new ReportServerCredentials();
            reportViewer.ShowCredentialPrompts = false;
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.ServerReport.ReportServerUrl =
                new System.Uri(ConfigurationManager.AppSettings["SSRS_ReportServer"].ToString());
            reportViewer.ServerReport.ReportPath = reportPath;
        }

        private string GetReportPath()
        {
            string reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "RequirementsFulfillmentRateAllDept";
            if (DropDownListUserDept.SelectedValue.Trim() != string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "RequirementsFulfillmentRateOneDept";
            }
            if (CheckBoxDivisionByDept.Checked == true)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "RequirementsFulfillmentRateDivisionByDept";
            }
            return reportPath;
        }
        #endregion

        #region DropDownListUserDept_SelectedIndexChanged
        protected void DropDownListUserDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListUserDept.SelectedValue.Trim() == string.Empty)
            {
                CheckBoxDivisionByDept.Enabled = true;
            }
            else
            {
                CheckBoxDivisionByDept.Checked = false;
                CheckBoxDivisionByDept.Enabled = false;
            }
        }
        #endregion
    }
}
