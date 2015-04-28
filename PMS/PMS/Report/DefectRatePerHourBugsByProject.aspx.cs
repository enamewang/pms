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

namespace PMS.PMS.Report
{
    public partial class DefectRatePerHourBugsByProject : System.Web.UI.Page
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
                IList<PmsSys> pmsSysList = new List<PmsSys>();

                pmsSysList = pmsSysBiz.SelectData2ByTypeData1("PM", "Report", "YEAR");

                DropDownListYear.DataSource = pmsSysList;
                DropDownListYear.DataTextField = "data2";
                DropDownListYear.DataValueField = "data2";
                DropDownListYear.DataBind();

                DropDownListYear.Items.Insert(0, new ListItem());
                DropDownListYear.Items[0].Text = "";
                DropDownListYear.Items[0].Value = "";


                pmsSysList = pmsSysBiz.SelectData2ByTypeData1("PM", "Report", "MONTH");

                DropDownListMonth.DataSource = pmsSysList;
                DropDownListMonth.DataTextField = "data2";
                DropDownListMonth.DataValueField = "data2";
                DropDownListMonth.DataBind();

                DropDownListMonth.Items.Insert(0, new ListItem());
                DropDownListMonth.Items[0].Text = "";
                DropDownListMonth.Items[0].Value = "";


                pmsSysList = pmsSysBiz.SelectData1ByType("PM", "UserDept");

                DropDownListUserDept.DataSource = pmsSysList;
                DropDownListUserDept.DataTextField = "data1";
                DropDownListUserDept.DataValueField = "data1";
                DropDownListUserDept.DataBind();

                DropDownListUserDept.Items.Insert(0, new ListItem());
                DropDownListUserDept.Items[0].Text = "";
                DropDownListUserDept.Items[0].Value = "";


                pmsSysList = pmsSysBiz.SelectData1ByType("PM", "RDDept");

                DropDownListRDDept.DataSource = pmsSysList;
                DropDownListRDDept.DataTextField = "data1";
                DropDownListRDDept.DataValueField = "data1";
                DropDownListRDDept.DataBind();

                DropDownListRDDept.Items.Insert(0, new ListItem());
                DropDownListRDDept.Items[0].Text = "";
                DropDownListRDDept.Items[0].Value = "";
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
                string year = DropDownListYear.SelectedValue.Trim();
                string month = DropDownListMonth.SelectedValue.Trim();
                string userDept = DropDownListUserDept.SelectedValue.Trim();
                string rDDept = DropDownListRDDept.SelectedValue.Trim();

                string reportPath = GetReportPath();

                SetReportViewParameter(ReportViewer1, reportPath);

                ReportParameter[] reportParameter = new ReportParameter[4];
                reportParameter[0] = new ReportParameter("Year", year);
                reportParameter[1] = new ReportParameter("Month", month);
                reportParameter[2] = new ReportParameter("UserDept", userDept);
                reportParameter[3] = new ReportParameter("RDDept", rDDept);

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
            string reportPath = string.Empty;
            if (DropDownListUserDept.SelectedValue.Trim() != string.Empty && DropDownListRDDept.SelectedValue.Trim() != string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsByProjectAll";
            }
            else if (DropDownListUserDept.SelectedValue.Trim() != string.Empty && DropDownListRDDept.SelectedValue.Trim() == string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsByProjectUserDept";
            }
            else if (DropDownListUserDept.SelectedValue.Trim() == string.Empty && DropDownListRDDept.SelectedValue.Trim() != string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsByProjectRDDeppt";
            }
            else if (DropDownListUserDept.SelectedValue.Trim() == string.Empty && DropDownListRDDept.SelectedValue.Trim() == string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsByProject";
            }

            return reportPath;
        }
        #endregion
    }
}
