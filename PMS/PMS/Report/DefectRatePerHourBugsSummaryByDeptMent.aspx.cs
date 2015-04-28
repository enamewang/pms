using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Security.Principal;
using Qisda.Common.ReportingServices;

namespace PMS.PMS.Report
{
    public partial class DefectRatePerHourBugsSummaryByDeptMent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReport();
            }
        }
        public void BindReport()
        {
            try
            {

                SetReportViewParameter(ReportViewer1, ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DefectRatePerHourBugsSummryByDeptMent");
                //ReportViewer1.ServerReport.SetParameters(parm);
                ReportViewer1.ServerReport.Refresh();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "check", "alert('" + ex.Message + "!');",
                                                    true);
                return;

            }
        }
        private void SetReportViewParameter(ReportViewer RV, string strReportPath)
        {
            RV.ServerReport.ReportServerCredentials = new ReportServerCredentials();
            RV.ShowCredentialPrompts = false;
            RV.ProcessingMode = ProcessingMode.Remote;
            RV.ServerReport.ReportServerUrl =
                new System.Uri(ConfigurationManager.AppSettings["SSRS_ReportServer"].ToString());
            RV.ServerReport.ReportPath = strReportPath;
        }
    }
}

