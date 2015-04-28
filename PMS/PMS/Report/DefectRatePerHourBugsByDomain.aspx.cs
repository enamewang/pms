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
    public partial class DefectRatePerHourBugsByDomain : System.Web.UI.Page
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

                pmsSysList = pmsSysBiz.SelectData1ByType("PM", "ReportDomain");

                DropDownListDomain.DataSource = pmsSysList;
                DropDownListDomain.DataTextField = "data1";
                DropDownListDomain.DataValueField = "data1";
                DropDownListDomain.DataBind();

                DropDownListDomain.Items.Insert(0, new ListItem());
                DropDownListDomain.Items[0].Text = "";
                DropDownListDomain.Items[0].Value = "";
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
                string monthFrom = TextBoxYearMonthFrom.Text.Trim();
                string monthTo = TextBoxYearMonthTo.Text.Trim();

                string reportPath = GetReportPath();

                SetReportViewParameter(ReportViewer1, reportPath);

                ReportParameter[] reportParameter = new ReportParameter[2];
                reportParameter[0] = new ReportParameter("MonthFrom", monthFrom);
                reportParameter[1] = new ReportParameter("MonthTo", monthTo);

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
            string reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsByDomainAll";
            if (DropDownListDomain.SelectedValue.Trim() == "BACH")
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsByDomainBACH";
            }
            else if (DropDownListDomain.SelectedValue.Trim() == "CIM")
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsByDomainCIM";
            }
            else if (DropDownListDomain.SelectedValue.Trim() == "IP")
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsByDomainIP";
            }
            else if (CheckBoxByAIC0.Checked == true)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsByDomainACIO";
            }
            return reportPath;
        }
        #endregion
    }
}
