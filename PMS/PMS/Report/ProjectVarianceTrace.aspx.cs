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
    public partial class ProjectVarianceTrace : System.Web.UI.Page
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
                string crNo = TextBoxCrNo.Text.Trim();
                DateTime dateFrom = DateTime.Parse(TextBoxDateFrom.Text.Trim() == string.Empty ? "1900-01-01" : TextBoxDateFrom.Text.Trim()).Date;
                DateTime dateTo = DateTime.Parse(TextBoxDateTo.Text.Trim() == string.Empty ? "1900-01-01" : TextBoxDateTo.Text.Trim()).Date;

                string reportPath = GetReportPath();
                SetReportViewParameter(ReportViewer1, reportPath);

                ReportParameter[] reportParameter = new ReportParameter[3];
                reportParameter[0] = new ReportParameter("CRNo", crNo);
                reportParameter[1] = new ReportParameter("DateFrom", dateFrom.ToString("yyyy-MM-dd"));
                reportParameter[2] = new ReportParameter("DateTo", dateTo.ToString("yyyy-MM-dd"));

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

            if (TextBoxCrNo.Text.Trim() != string.Empty && TextBoxDateFrom.Text.Trim() != string.Empty && TextBoxDateTo.Text.Trim() != string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectVarianceTraceAll";
            }
            else if (TextBoxCrNo.Text.Trim() != string.Empty && TextBoxDateFrom.Text.Trim() != string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectVarianceTraceDateFrom";
            }
            else if (TextBoxCrNo.Text.Trim() != string.Empty && TextBoxDateTo.Text.Trim() != string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectVarianceTraceDateTo";
            }
            else if (TextBoxCrNo.Text.Trim() != string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectVarianceTraceCrNo";
            }
            return reportPath;
        }
        #endregion
    }
}
