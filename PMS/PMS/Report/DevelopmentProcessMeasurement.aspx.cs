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
    public partial class DevelopmentProcessMeasurement : System.Web.UI.Page
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

                pmsSysList = pmsSysBiz.SelectData1ByType("PM", "RDDept");

                DropDownListDepartment.DataSource = pmsSysList;
                DropDownListDepartment.DataTextField = "data1";
                DropDownListDepartment.DataValueField = "data1";
                DropDownListDepartment.DataBind();

                DropDownListDepartment.Items.Insert(0, new ListItem());
                DropDownListDepartment.Items[0].Text = "";
                DropDownListDepartment.Items[0].Value = "";
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
                string userDept = DropDownListDepartment.SelectedValue.Trim();
                string userName = TextBoxUserName.Text.Trim();
                DateTime currentDate = DateTime.Now;
                DateTime dateFrom = DateTime.Parse(TextBoxDateFrom.Text.Trim()).Date;
                DateTime dateTo = DateTime.Parse(TextBoxDateTo.Text.Trim()).Date;

                new PmsDevelopmentProcessMeasurementBiz().DeletePmsDevelopmentProcessMeasurementAll();

                //1.根据userDept和userName取得项目
                PmsEvmRawDataByUser pmsEvmRawDataByUser = new PmsEvmRawDataByUser
                {
                    UserDept = userDept,
                    UserName = userName
                };

                IList<PmsEvmRawDataByUser> listPmsEvmRawDataByUser = new PmsEvmRawDataByUserBiz().SelectPmsEvmRawDataByUser(pmsEvmRawDataByUser);

                //2.根据dateFrom和dateTo过滤ReleaseDate在此之间的项目
                listPmsEvmRawDataByUser = listPmsEvmRawDataByUser.Where(t => (t.ReleaseDate.Date >= dateFrom && t.ReleaseDate.Date <= dateTo)).ToList();

                //3.根据User的统计项目
                if (listPmsEvmRawDataByUser.Count > 0)
                {
                    IList<string> listUser = listPmsEvmRawDataByUser.Select(t => t.UserName).ToList();
                    foreach (var user in listUser)
                    {
                        listPmsEvmRawDataByUser = listPmsEvmRawDataByUser.Where(t => t.UserName == user).ToList();
                        PmsDevelopmentProcessMeasurement pmsDevelopmentProcessMeasurement = new PmsDevelopmentProcessMeasurement();
                        pmsDevelopmentProcessMeasurement.Vid = "PM";
                        pmsDevelopmentProcessMeasurement.UserDept = listPmsEvmRawDataByUser.FirstOrDefault().UserDept;
                        pmsDevelopmentProcessMeasurement.UserName = user;
                        pmsDevelopmentProcessMeasurement.CrCount = listPmsEvmRawDataByUser.Count;
                        pmsDevelopmentProcessMeasurement.LargelyAdvance = listPmsEvmRawDataByUser.Where(t => t.Sv >= 8).Count();
                        pmsDevelopmentProcessMeasurement.Advance = listPmsEvmRawDataByUser.Where(t => t.Sv < 8 && t.Sv > 0).Count();
                        pmsDevelopmentProcessMeasurement.Normal = listPmsEvmRawDataByUser.Where(t => t.Sv == 0).Count();
                        pmsDevelopmentProcessMeasurement.Delay = listPmsEvmRawDataByUser.Where(t => t.Sv < 0 && t.Sv > -8).Count();
                        pmsDevelopmentProcessMeasurement.LargelyDelay = listPmsEvmRawDataByUser.Where(t => t.Sv <= -8).Count();
                        pmsDevelopmentProcessMeasurement.LargelySurplus = listPmsEvmRawDataByUser.Where(t => t.Cv >= 8).Count();
                        pmsDevelopmentProcessMeasurement.Surplus = listPmsEvmRawDataByUser.Where(t => t.Cv < 8 && t.Cv > 0).Count();
                        pmsDevelopmentProcessMeasurement.Balance = listPmsEvmRawDataByUser.Where(t => t.Cv == 0).Count();
                        pmsDevelopmentProcessMeasurement.Deficit = listPmsEvmRawDataByUser.Where(t => t.Cv < 0 && t.Cv > -8).Count();
                        pmsDevelopmentProcessMeasurement.LargelyDeficit = listPmsEvmRawDataByUser.Where(t => t.Cv <= -8).Count();

                        //进度偏差得分
                        int scheduleVarianceScore = pmsDevelopmentProcessMeasurement.LargelyAdvance * 5 + pmsDevelopmentProcessMeasurement.Advance * 3
                                                  + pmsDevelopmentProcessMeasurement.Normal * 1 + pmsDevelopmentProcessMeasurement.Delay * (-3) + pmsDevelopmentProcessMeasurement.LargelyDelay * (-5);

                        //成本偏差得分
                        int costVarianceScore = pmsDevelopmentProcessMeasurement.LargelySurplus * 5 + pmsDevelopmentProcessMeasurement.Surplus * 3
                                                  + pmsDevelopmentProcessMeasurement.Balance * 1 + pmsDevelopmentProcessMeasurement.Deficit * (-3) + pmsDevelopmentProcessMeasurement.LargelyDeficit * (-5);

                        //基本得分
                        int baseScore = pmsDevelopmentProcessMeasurement.CrCount * 1;

                        //进度偏差评价
                        pmsDevelopmentProcessMeasurement.ScheduleEvaluation = GetEvaluationByScore(scheduleVarianceScore, baseScore);

                        //成本偏差评价
                        pmsDevelopmentProcessMeasurement.ScheduleEvaluation = GetEvaluationByScore(costVarianceScore, baseScore);

                        pmsDevelopmentProcessMeasurement.CreateDate = currentDate;
                        pmsDevelopmentProcessMeasurement.Creator = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");

                        new PmsDevelopmentProcessMeasurementBiz().InsertPmsDevelopmentProcessMeasurement(pmsDevelopmentProcessMeasurement);
                    }
                }

                string reportPath = GetReportPath();

                SetReportViewParameter(ReportViewer1, reportPath);

                ReportParameter[] reportParameter = new ReportParameter[4];
                reportParameter[0] = new ReportParameter("UserDept", userDept);
                reportParameter[1] = new ReportParameter("UserName", userName);
                reportParameter[2] = new ReportParameter("DateFrom", dateFrom.ToString("yyyy-MM-dd"));
                reportParameter[3] = new ReportParameter("DateTo", dateTo.ToString("yyyy-MM-dd"));

                ReportViewer1.ServerReport.SetParameters(reportParameter);
                ReportViewer1.ServerReport.Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        private string GetEvaluationByScore(int varianceScore, int baseScore)
        {
            string evaluation = "";
            float ratio = varianceScore / baseScore;

            if (ratio >= baseScore * 1.5)
            {
                evaluation = "优秀";
            }
            else if (ratio < baseScore * 1.5 && ratio >= baseScore * 1.1)
            {
                evaluation = "良好";
            }
            else if (ratio < baseScore * 1.1 && ratio >= baseScore * 0.9)
            {
                evaluation = "一般";
            }
            else if (ratio < baseScore * 0.9 && ratio >= baseScore * 0.5)
            {
                evaluation = "差";
            }
            else if (ratio < baseScore * 0.5)
            {
                evaluation = "很差";
            }
            return evaluation;
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
            if (DropDownListDepartment.SelectedValue.Trim() != string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DevelopmentProcessMeasurementByUserDept";
            }
            else if (TextBoxUserName.Text.Trim() != string.Empty)
            {
                reportPath = ConfigurationManager.AppSettings["SSRS_ReportPath_Fold"] + "DevelopmentProcessMeasurementByUserName";
            }
            return reportPath;
        }
        #endregion
    }
}
