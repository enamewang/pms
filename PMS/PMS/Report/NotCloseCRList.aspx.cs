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
    public partial class NotCloseCRList : System.Web.UI.Page
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

                SetReportViewParameter(ReportViewer1, ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "PMS_NotCloseCRList_Web");
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

namespace Qisda.Common.ReportingServices
{
    public class ReportServerCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        #region IReportServerCredentials Members

        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        {
            userName = password = authority = "";
            authCookie = null;
            return false;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get
            {
                string logonMethod = ConfigurationManager.AppSettings["SSRS_Logon_Method"];
                if (!string.IsNullOrEmpty(logonMethod) && logonMethod == "1")
                    return (WindowsIdentity)HttpContext.Current.User.Identity;
                else
                    return null;
            }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                string logonMethod = ConfigurationManager.AppSettings["SSRS_Logon_Method"];
                if (!string.IsNullOrEmpty(logonMethod) && logonMethod == "2")
                {
                    string userName = ConfigurationManager.AppSettings["SSRS_Logon_UserName"];
                    string password = ConfigurationManager.AppSettings["SSRS_Logon_Password"];
                    string domain = ConfigurationManager.AppSettings["SSRS_Logon_Domain"];
                    return new System.Net.NetworkCredential(userName, password, domain);
                }
                else
                    return (System.Net.ICredentials)HttpContext.Current.User.Identity;
            }
        }

        #endregion
    }
}