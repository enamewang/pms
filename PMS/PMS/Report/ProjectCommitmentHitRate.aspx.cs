using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.Common.ReportingServices;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using PMS.PMS.AppCode;
using System.Collections;

namespace PMS.PMS.Report
{
    public partial class ProjectCommitmentHitRate : PageBase
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

                pmsSysList = pmsSysBiz.SelectData2ByTypeData1("PM", "Report", "StatisticsInterval");

                DropDownListStatisticsInterval.DataSource = pmsSysList;
                DropDownListStatisticsInterval.DataTextField = "data2";
                DropDownListStatisticsInterval.DataValueField = "data2";
                DropDownListStatisticsInterval.DataBind();

                DropDownListStatisticsInterval.Items.Insert(0, new ListItem());
                DropDownListStatisticsInterval.Items[0].Text = "";
                DropDownListStatisticsInterval.Items[0].Value = "";

                pmsSysList = pmsSysBiz.SelectData1ByType("PM", "UserDept");

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
            if (TextBoxYearMonthFrom.Text.Trim() != "" && TextBoxYearMonthTo.Text != "")
            {
                DateTime dateTimeYearMonthFrom;
                DateTime dateTimeYearMonthTo;
                DateTime.TryParse(TextBoxYearMonthFrom.Text, out dateTimeYearMonthFrom);
                DateTime.TryParse(TextBoxYearMonthTo.Text, out dateTimeYearMonthTo);

                if (dateTimeYearMonthFrom > dateTimeYearMonthTo)
                {
                    Msgbox("Year/Month From is bigger than Year/Month To!");
                    return;
                }
            }

            InsertProjectCommitmentHitRate();
            BindReportViewer();
        }
        #endregion

        #region InsertProjectCommitmentHitRate
        private void InsertProjectCommitmentHitRate()
        {
            IList<string> weeks = new List<string>();
            IList<string> months = GetMonths();
            foreach (string month in months)
            {

                PmsProjectCommitmentHitRateBiz pmsProjectCommitmentHitRateBiz = new PmsProjectCommitmentHitRateBiz();
                PmsProjectCommitmentHitRate pmsProjectCommitmentHitRate = new PmsProjectCommitmentHitRate();
                IList<PmsProjectCommitmentHitRate> listPmsProjectCommitmentHitRate;
                if (DropDownListStatisticsInterval.SelectedValue.Trim() == "Bi-weekly")
                {
                    weeks = GetWeeks(month);

                    foreach (string week in weeks)
                    {
                        pmsProjectCommitmentHitRate.Week = week;
                        pmsProjectCommitmentHitRate.UserDept = DropDownListUserDept.SelectedValue.Trim();

                        listPmsProjectCommitmentHitRate =
                            pmsProjectCommitmentHitRateBiz.GetProjectCommitmentHitRatePmsHead(pmsProjectCommitmentHitRate);

                        if (listPmsProjectCommitmentHitRate == null || listPmsProjectCommitmentHitRate.Count == 0)
                        {
                            return;
                        }

                        listPmsProjectCommitmentHitRate = SetListPmsProjectCommitmentHitRate(listPmsProjectCommitmentHitRate, month, week);

                        pmsProjectCommitmentHitRateBiz.InsertPmsProjectCommitmentHitRate(listPmsProjectCommitmentHitRate);
                    }
                }
                else
                {
                    pmsProjectCommitmentHitRate.Month = month;
                    pmsProjectCommitmentHitRate.UserDept = DropDownListUserDept.SelectedValue.Trim();
                    listPmsProjectCommitmentHitRate =
                           pmsProjectCommitmentHitRateBiz.GetProjectCommitmentHitRatePmsHead(pmsProjectCommitmentHitRate);

                    if (listPmsProjectCommitmentHitRate == null || listPmsProjectCommitmentHitRate.Count == 0)
                    {
                        return;
                    }

                    listPmsProjectCommitmentHitRate = SetListPmsProjectCommitmentHitRate(listPmsProjectCommitmentHitRate, month, pmsProjectCommitmentHitRate.Week);

                    pmsProjectCommitmentHitRateBiz.InsertPmsProjectCommitmentHitRate(listPmsProjectCommitmentHitRate);

                }
            }
        }
        #endregion

        #region GetMonths
        private IList<string> GetMonths()
        {
            IList<string> months = new List<string>();
            int yearFrom = int.Parse(TextBoxYearMonthFrom.Text.Substring(0, 4));
            int yearTo = int.Parse(TextBoxYearMonthTo.Text.Substring(0, 4));
            int yearInterval = yearTo - yearFrom;
            if (yearInterval == 0)
            {
                int monthFrom = int.Parse(TextBoxYearMonthFrom.Text.Substring(5, 2));
                int monthTo = int.Parse(TextBoxYearMonthTo.Text.Substring(5, 2));
                int monthInterval = monthTo - monthFrom;
                months = AddMonth(months, yearFrom, monthFrom, monthInterval);
            }
            else
            {
                for (int i = 0; i <= yearInterval; i++)
                {
                    if (i == 0 || i == yearInterval) // 两头的年份单独计算
                    {
                        if (i == 0)
                        {
                            int year = yearFrom;
                            int monthFrom = int.Parse(TextBoxYearMonthFrom.Text.Substring(5, 2));
                            int monthInterval = 12 - monthFrom;
                            AddMonth(months, year, monthFrom, monthInterval);
                        }

                        if (i == yearInterval)
                        {
                            int year = yearFrom + yearInterval;
                            int monthFrom = 1;
                            int monthTo = int.Parse(TextBoxYearMonthTo.Text.Substring(5, 2));
                            int monthInterval = monthTo - monthFrom;
                            AddMonth(months, year, monthFrom, monthInterval);
                        }

                    }
                    else //中间的年份固定有十二个月
                    {
                        int year = yearFrom + i;
                        int monthFrom = 1;
                        int monthInterval = 11;
                        AddMonth(months, year, monthFrom, monthInterval);
                    }
                }
            }
            return months;
        }
        #endregion

        #region AddMonth
        private IList<string> AddMonth(IList<string> months, int year, int monthFrom, int monthInterval)
        {
            for (int i = 0; i <= monthInterval; i++)
            {
                string formatMonth = (monthFrom + i).ToString();
                if ((monthFrom + i).ToString().Length == 1)
                {
                    formatMonth = "0" + formatMonth;
                }
                string month = year.ToString() + "/" + formatMonth;
                months.Add(month);
            }
            return months;
        }
        #endregion

        #region GetWeeks
        private IList<string> GetWeeks(string month)
        {
            IList<string> weeks = new List<string>();
            PmsSysBiz pmsSysBiz = new PmsSysBiz();
            IList<PmsSys> pmsSysList = pmsSysBiz.SelectData2ByTypeData1("PM", "RequirementPeriod", month);

            if (pmsSysList.Count > 0)
            {
                foreach (var pmsSys in pmsSysList)
                {
                    weeks.Add(pmsSys.Data2);
                }
            }
            return weeks;
        }
        #endregion

        #region SetListPmsProjectCommitmentHitRate
        private IList<PmsProjectCommitmentHitRate> SetListPmsProjectCommitmentHitRate(IList<PmsProjectCommitmentHitRate> listPmsProjectCommitmentHitRate, string month, string week)
        {
            foreach (var PmsProjectCommitmentHitRate in listPmsProjectCommitmentHitRate)
            {
                PmsProjectCommitmentHitRate.Month = month;
                PmsProjectCommitmentHitRate.Week = week;
                PmsProjectCommitmentHitRate.Createdate = System.DateTime.Now;
                PmsProjectCommitmentHitRate.Createuser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                PmsProjectCommitmentHitRate.Maintaindate = System.DateTime.Now;
                PmsProjectCommitmentHitRate.Maintainuser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                if (PmsProjectCommitmentHitRate.ReleaseDate.Date > PmsProjectCommitmentHitRate.DueDate.Date)
                {
                    PmsProjectCommitmentHitRate.Status = "Delay";
                }
                else
                {
                    if (DueDateChangedForRdReason(PmsProjectCommitmentHitRate.PmsId))
                    {
                        PmsProjectCommitmentHitRate.Status = "Delay";
                    }
                    else
                    {
                        PmsProjectCommitmentHitRate.Status = "OnSchedule";
                    }
                }
            }
            return listPmsProjectCommitmentHitRate;
        }
        #endregion

        #region DueDateChangedForRdReason
        private bool DueDateChangedForRdReason(string pmsId)
        {
            bool result = false;
            IList<PmsHeadH> listtPmsHeadH = new PmsHeadHBiz().SelectPmsHeadHByPmsId(pmsId);
            foreach (var pmsHeadH in listtPmsHeadH)
            {
                string dueDate = pmsHeadH.DueDate.ToString("yyyy-MM-dd");
                if (!dueDate.Equals("1900-01-01") && !dueDate.Equals("0001-01-01") && !dueDate.Equals("0000-00-00") && !dueDate.Equals("1899-12-30") && !dueDate.Equals("01-01-01"))
                {
                    if (pmsHeadH.ReasonType != string.Empty)
                    {
                        PmsSysBiz pmsSysBiz = new PmsSysBiz();
                        IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByTypeData5("PM", "ReasonType", "RD");
                        foreach (var pmsSys in pmsSysList)
                        {
                            if (pmsHeadH.ReasonType == pmsSys.Data1)
                            {
                                return true;
                            }
                        }

                    }
                }
            }
            return result;
        }
        #endregion

        #region BindReportViewer
        private void BindReportViewer()
        {
            try
            {
                string statisticsInterval = DropDownListStatisticsInterval.SelectedValue.Trim();
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
        #endregion

        #region SetReportViewParameter
        private void SetReportViewParameter(ReportViewer reportViewer, string reportPath)
        {
            reportViewer.ServerReport.ReportServerCredentials = new ReportServerCredentials();
            reportViewer.ShowCredentialPrompts = false;
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.ServerReport.ReportServerUrl =
                new System.Uri(ConfigurationManager.AppSettings["SSRS_ReportServer"].ToString());
            reportViewer.ServerReport.ReportPath = reportPath;
        }
        #endregion

        #region GetReportPath
        private string GetReportPath()
        {
            string reportPath = string.Empty;
            if (DropDownListStatisticsInterval.SelectedValue.Trim().ToUpper() == "MONTHLY")
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectCommitmentHitRateAllDeptMonthly";
                if (DropDownListUserDept.SelectedValue.Trim() != string.Empty)
                {
                    reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectCommitmentHitRateOneDeptMonthly";
                }
                if (CheckBoxDivisionByDept.Checked == true)
                {
                    reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectCommitmentHitRateDivisionByDeptMonthly";
                }
            }
            else
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectCommitmentHitRateAllDept";
                if (DropDownListUserDept.SelectedValue.Trim() != string.Empty)
                {
                    reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectCommitmentHitRateOneDept";
                }
                if (CheckBoxDivisionByDept.Checked == true)
                {
                    reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "ProjectCommitmentHitRateDivisionByDept";
                }
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
