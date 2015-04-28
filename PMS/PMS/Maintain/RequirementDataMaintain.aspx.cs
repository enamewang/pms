#region -- Page Information --
//////////////////////////////////////////////////////////////////////////////////
// File name: CreateService.aspx.cs    
// Copyright (C) 2011 Qisda Corporation. All rights reserved.    
// Author:		  Ename Wang   
// ALTER  Date:   2012/03/21
// Current Version:  1.0
// Description:   behind code of AutoExecutionSetup.aspx
// History: 
// 
//      Date     |    Time    |    Author   |  Modification 
// 1  2012/03/21 |   18:01:39 |  Ename Wang |  Create
//////////////////////////////////////////////////////////////////////////////////
#endregion

#region -- Using NameSpace --
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.PMS.AppCode;
using System.IO;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
#endregion

namespace PMS.PMS.Maintain
{
    public partial class RequirementDataMaintain : PageBase
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
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1Data2ByType("PM", "RequirementPeriod");

                IList<string> listYearMonth = pmsSysList.Select(t => t.Data1).Distinct().ToList();
                DropDownListYearMonth.DataSource = listYearMonth;
                DropDownListYearMonth.DataBind();

                DropDownListYearMonth.Items.Insert(0, new ListItem());
                DropDownListYearMonth.Items[0].Text = "";
                DropDownListYearMonth.Items[0].Value = "";

                DropDownListWeekPeriod.DataSource = pmsSysList;
                DropDownListWeekPeriod.DataTextField = "data2";
                DropDownListWeekPeriod.DataValueField = "data2";
                DropDownListWeekPeriod.DataBind();

                DropDownListWeekPeriod.Items.Insert(0, new ListItem());
                DropDownListWeekPeriod.Items[0].Text = "";
                DropDownListWeekPeriod.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind Domain failure');", true);
                this.DropDownListYearMonth.Focus();
            }
        }
        #endregion

        #region DropDownListYearMonth_SelectedIndexChanged
        protected void DropDownListYearMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownListWeekPeriod.Items.Clear();

                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                string yearMonth = DropDownListYearMonth.SelectedValue.Trim();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData2ByTypeData1("PM", "RequirementPeriod", yearMonth);

                DropDownListWeekPeriod.DataSource = pmsSysList;
                DropDownListWeekPeriod.DataTextField = "data2";
                DropDownListWeekPeriod.DataValueField = "data2";
                DropDownListWeekPeriod.DataBind();

                DropDownListWeekPeriod.Items.Insert(0, new ListItem());
                DropDownListWeekPeriod.Items[0].Text = "";
                DropDownListWeekPeriod.Items[0].Value = "";

                ClearGrid();
            }
            catch
            {

            }
        }
        #endregion

        #region DropDownListWeekPeriod_SelectedIndexChanged
        protected void DropDownListWeekPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearGrid();
        }
        #endregion

        #region ClearGrid()
        private void ClearGrid()
        {
            GridViewRequirement.EmptyDataText = "";
            IList<PmsRequirement> pmsRequirementList = new List<PmsRequirement>();
            GridViewRequirement.DataSource = pmsRequirementList;
            GridViewRequirement.DataBind();
        }
        #endregion

        #region GridView_RowDeleting
        protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                int serial = int.Parse(GridViewRequirement.DataKeys[rowIndex]["Serial"].ToString());

                PmsRequirementBiz pmsRequirementBiz = new PmsRequirementBiz();
                PmsRequirement pmsRequirement = new PmsRequirement();
                pmsRequirement.Vid = "PZ";
                pmsRequirement.Serial = serial;
                pmsRequirement.Maintaindate = System.DateTime.Now;
                pmsRequirement.Maintainuser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                pmsRequirementBiz.DeletePmsRequirement(pmsRequirement);

                BindGrid();
            }
            catch
            {
                Msgbox("Delete failure!");
            }
        }
        #endregion

        #region ButtonUpload_Click
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            // 上传，显示，保存此次上传的文件名和路径。
            if (string.IsNullOrEmpty(DropDownListYearMonth.SelectedValue.Trim()))
            {
                Msgbox("Year and Month can not be empty!");
                return;
            }

            if (string.IsNullOrEmpty(DropDownListWeekPeriod.SelectedValue.Trim()))
            {
                Msgbox("Week Period can not be empty!");
                return;
            }

            if (!FileUpload.HasFile)
            {
                Msgbox("please choose file to upload!");
                return;
            }
            try
            {
                //上传文件
                string fileFullPath = UpLoadFile();
                ImportFile(fileFullPath);
                BindGrid();
            }

            catch (Exception ex)
            {

                Msgbox("Import file failed");
            }
        }
        #endregion

        #region UpLoadFile
        private string UpLoadFile()
        {
            string fileName = FileUpload.FileName;
            string requirementDataFile = ConfigurationManager.AppSettings["RequirementDataFile"];
            string fileFullPath = Server.MapPath(requirementDataFile) + "\\" + fileName;
            string path = Server.MapPath(requirementDataFile) + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(fileFullPath))
            {
                File.Delete(fileFullPath);
            }
            FileUpload.SaveAs(fileFullPath);
            return fileFullPath;
        }
        #endregion

        #region ImportFile
        private void ImportFile(string fileFullPath)
        {
            DataSet dsExcel = ExcelToDataSet(fileFullPath);
            SaveExcelDataSet(dsExcel);
        }
        #endregion

        #region ExcelToDataSet
        private DataSet ExcelToDataSet(string excelFileName)
        {
            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source={0};" + "Extended Properties='Excel 5.0; HDR=Yes; IMEX=1;'", excelFileName);
            DataSet ds = new DataSet();
            using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbDataAdapter command = null;
                    string sqlExcel = string.Empty;
                    string[] sheetNames = new string[] { "BACH$", "CIM$", "IP$" };
                    for (int i = 0; i < sheetNames.Length; i++)
                    {
                        sqlExcel = string.Format("select * from [{0}]", sheetNames[i]);
                        command = new OleDbDataAdapter(sqlExcel, connectionString);
                        command.Fill(ds, "CRDATA");
                    }
                    sqlExcel = string.Format("select * from [{0}]", "Summary$");
                    command = new OleDbDataAdapter(sqlExcel, connectionString);
                    command.Fill(ds, "SUMMARY");
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }
            return ds;
        }
        #endregion

        #region SaveExcelDataSet
        private void SaveExcelDataSet(DataSet dsExcel)
        {
            IList<PmsRequirement> listPmsRequirement = GetPmsRequirementList(dsExcel);

            IList<PmsHeadCount> listPmsHeadCount = GetPmsHeadCountList(dsExcel);//取得HeadCount数据,Department

            IList<PmsHeadCountByContent> listPmsHeadCountByContent = GetPmsHeadCountByContentList(dsExcel);//取得HeadCount数据,Content

            if (listPmsRequirement != null && listPmsHeadCount != null && listPmsHeadCountByContent != null)
            {
                PmsRequirementBiz pmsRequirementBiz = new PmsRequirementBiz();
                pmsRequirementBiz.InsertPmsRequirement(listPmsRequirement, listPmsHeadCount, listPmsHeadCountByContent);
            }
        }
        #endregion

        #region GetPmsRequirementList
        private IList<PmsRequirement> GetPmsRequirementList(DataSet dsExcel)
        {
            IList<PmsRequirement> listPmsRequirement = new List<PmsRequirement>();
            DataTable dtExcel = new DataTable();
            dtExcel = dsExcel.Tables["CRDATA"];
            PmsRequirement pmsRequirement = new PmsRequirement();

            for (int k = 1; k < dtExcel.Rows.Count; k++)
            {
                // A列无值表示已无数据，读取结束
                string userDept = dtExcel.Rows[k]["Team"].ToString().Trim();
                if (userDept == string.Empty)
                {
                    continue;
                }

                string crIdThis = dtExcel.Rows[k]["CR No"].ToString().Trim();
                string crNameThis = dtExcel.Rows[k]["Item"].ToString().Trim();
                string role = dtExcel.Rows[k]["Role"].ToString().Trim();
                string manpower = dtExcel.Rows[k]["Manpower"].ToString().Trim();

                if (k == 1)
                {
                    pmsRequirement = GetPmsRequirement(dtExcel.Rows[k], role, manpower);
                }

                // 从第二行开始，判断上一行是否为同一个CR，若相同，则这一行不处理
                if (k >= 2)
                {
                    string crIdPre = dtExcel.Rows[k - 1]["CR No"].ToString().Trim();
                    string crNamePre = dtExcel.Rows[k - 1]["Item"].ToString().Trim();

                    // CR ID 相同且CR Name相同或者CR ID为空且 CR Name也为空或者CR ID为空且 CR Name相同
                    if ((crIdThis == crIdPre && crNameThis == crNamePre)
                        || (crIdThis == string.Empty && crNameThis == string.Empty
                        || (crIdThis == string.Empty && crNameThis == crNamePre))
                      )
                    {
                        pmsRequirement = UpdatePmsRequirement(pmsRequirement, role, manpower);
                    }
                    else
                    {
                        pmsRequirement.Sd = pmsRequirement.Sd.TrimEnd(';');
                        pmsRequirement.Se = pmsRequirement.Se.TrimEnd(';');
                        pmsRequirement.Qa = pmsRequirement.Qa.TrimEnd(';');
                        listPmsRequirement.Add(pmsRequirement);
                        pmsRequirement = GetPmsRequirement(dtExcel.Rows[k], role, manpower);
                    }

                    // 最后一笔资料，在本次循环加上
                    if (k == dtExcel.Rows.Count - 1)
                    {
                        pmsRequirement.Sd = pmsRequirement.Sd.TrimEnd(';');
                        pmsRequirement.Se = pmsRequirement.Se.TrimEnd(';');
                        pmsRequirement.Qa = pmsRequirement.Qa.TrimEnd(';');
                        listPmsRequirement.Add(pmsRequirement);
                    }
                }
            }
            return listPmsRequirement;
        }
        #endregion

        #region GetPmsRequirement
        private PmsRequirement GetPmsRequirement(DataRow dtExcelRows, string role, string manpower)
        {
            PmsRequirement pmsRequirement = new PmsRequirement();
            pmsRequirement.Vid = "PM";
            pmsRequirement.YearAndMonth = DropDownListYearMonth.SelectedValue.Trim();
            pmsRequirement.RequirementPeriod = DropDownListWeekPeriod.SelectedValue.Trim();
            pmsRequirement.UserDept = dtExcelRows["Team"].ToString().Trim();
            pmsRequirement.CrId = dtExcelRows["CR No"].ToString().Trim();
            pmsRequirement.CrName = dtExcelRows["Item"].ToString().Trim();
            pmsRequirement.Type = dtExcelRows["System Type"].ToString().Trim();
            pmsRequirement.System = dtExcelRows["System"].ToString().Trim();
            pmsRequirement.Pm = dtExcelRows["PM/CE"].ToString().Trim();
            pmsRequirement.Status = dtExcelRows["Status"].ToString().Trim();
            pmsRequirement.Createdate = System.DateTime.Now;
            pmsRequirement.Createuser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            pmsRequirement.Maintaindate = System.DateTime.Now;
            pmsRequirement.Maintainuser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");

            UpdatePmsRequirement(pmsRequirement, role, manpower);

            return pmsRequirement;
        }
        #endregion

        #region UpdatePmsRequirement
        private PmsRequirement UpdatePmsRequirement(PmsRequirement pmsRequirement, string role, string manpower)
        {
            if (manpower.Trim() != string.Empty)
            {
                switch (role)
                {
                    case "SD":
                        pmsRequirement.Sd = pmsRequirement.Sd + manpower + ";";
                        break;
                    case "SE":
                        pmsRequirement.Se = pmsRequirement.Se + manpower + ";";
                        break;
                    case "QA":
                        pmsRequirement.Qa = pmsRequirement.Qa + manpower + ";";
                        break;
                    default:
                        break;
                }
            }
            return pmsRequirement;
        }
        #endregion

        #region GetPmsHeadCountList
        private IList<PmsHeadCount> GetPmsHeadCountList(DataSet dsExcel)
        {
            IList<PmsHeadCount> listPmsHeadCount = new List<PmsHeadCount>();
            DataTable dtExcel = new DataTable();
            dtExcel = dsExcel.Tables["SUMMARY"];
            PmsHeadCount pmsHeadCount = new PmsHeadCount();

            for (int k = 1; k < dtExcel.Rows.Count; k++)
            {
                string role = dtExcel.Rows[k]["F1"].ToString().ToUpper().Trim();
                switch (role)
                {
                    case "SD":
                    case "SE":
                    case "QA":
                        listPmsHeadCount = AddPmsHeadCount(listPmsHeadCount, dtExcel.Rows[k], role);
                        break;
                    default:
                        break;
                }
            }
            return listPmsHeadCount;
        }

        private IList<PmsHeadCount> AddPmsHeadCount(IList<PmsHeadCount> listPmsHeadCount, DataRow dtExcelRows, string role)
        {
            PmsHeadCount pmsHeadCount = new PmsHeadCount();

            string ai10HeadCount = dtExcelRows["F6"].ToString();
            pmsHeadCount = GetPmsHeadCount(role, "AI10", ai10HeadCount);
            listPmsHeadCount.Add(pmsHeadCount);
            string ai20HeadCount = dtExcelRows["F7"].ToString();
            pmsHeadCount = GetPmsHeadCount(role, "AI20", ai20HeadCount);
            listPmsHeadCount.Add(pmsHeadCount);
            string ai30HeadCount = dtExcelRows["Resource"].ToString();
            pmsHeadCount = GetPmsHeadCount(role, "AI30", ai30HeadCount);
            listPmsHeadCount.Add(pmsHeadCount);
            string ai40HeadCount = dtExcelRows["F9"].ToString();
            pmsHeadCount = GetPmsHeadCount(role, "AI40", ai40HeadCount);
            listPmsHeadCount.Add(pmsHeadCount);
            string aic0HeadCount = dtExcelRows["F10"].ToString();
            pmsHeadCount = GetPmsHeadCount(role, "AIC0", aic0HeadCount);
            listPmsHeadCount.Add(pmsHeadCount);

            return listPmsHeadCount;
        }

        private PmsHeadCount GetPmsHeadCount(string role, string userDept, string headCount)
        {
            PmsHeadCount pmsHeadCount = new PmsHeadCount();
            pmsHeadCount.Vid = "PM";
            pmsHeadCount.YearAndMonth = DropDownListYearMonth.SelectedValue.Trim();
            pmsHeadCount.RequirementPeriod = DropDownListWeekPeriod.SelectedValue.Trim();
            pmsHeadCount.UserDept = userDept;
            pmsHeadCount.Role = role;
            pmsHeadCount.HeadCount = float.Parse(headCount);
            pmsHeadCount.Createdate = System.DateTime.Now;
            pmsHeadCount.Createuser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");

            return pmsHeadCount;
        }

        #endregion

        #region GetPmsHeadCountByContentList
        private IList<PmsHeadCountByContent> GetPmsHeadCountByContentList(DataSet dsExcel)
        {
            IList<PmsHeadCountByContent> listPmsHeadCountByContent = new List<PmsHeadCountByContent>();
            DataTable dtExcel = new DataTable();
            dtExcel = dsExcel.Tables["SUMMARY"];
            PmsHeadCountByContent pmsHeadCountByContent = new PmsHeadCountByContent();

            for (int k = 1; k < dtExcel.Rows.Count; k++)
            {
                string role = dtExcel.Rows[k]["F1"].ToString().ToUpper().Trim();
                switch (role)
                {
                    case "SD":
                    case "SE":
                    case "QA":
                        listPmsHeadCountByContent = AddPmsHeadCountByContent(listPmsHeadCountByContent, dtExcel.Rows[k], role);
                        break;
                    default:
                        break;
                }
            }
            return listPmsHeadCountByContent;
        }

        private IList<PmsHeadCountByContent> AddPmsHeadCountByContent(IList<PmsHeadCountByContent> listPmsHeadCountByContent, DataRow dtExcelRows, string role)
        {
            PmsHeadCountByContent pmsHeadCountByContent = new PmsHeadCountByContent();

            string vB2NetHeadCount = dtExcelRows["F12"].ToString();
            pmsHeadCountByContent = GetPmsHeadCountByContent(role, "VB2NET", vB2NetHeadCount);
            listPmsHeadCountByContent.Add(pmsHeadCountByContent);

            return listPmsHeadCountByContent;
        }

        private PmsHeadCountByContent GetPmsHeadCountByContent(string role, string contentType, string headCount)
        {
            PmsHeadCountByContent pmsHeadCountByContent = new PmsHeadCountByContent();
            pmsHeadCountByContent.Vid = "PM";
            pmsHeadCountByContent.YearAndMonth = DropDownListYearMonth.SelectedValue.Trim();
            pmsHeadCountByContent.RequirementPeriod = DropDownListWeekPeriod.SelectedValue.Trim();
            pmsHeadCountByContent.ContentType = contentType;
            pmsHeadCountByContent.Role = role;
            pmsHeadCountByContent.HeadCount = float.Parse(headCount);
            pmsHeadCountByContent.Createdate = System.DateTime.Now;
            pmsHeadCountByContent.Createuser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");

            return pmsHeadCountByContent;
        }

        #endregion

        #region ButtonInquiry_Click
        protected void ButtonInquiry_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            PmsRequirementBiz pmsRequirementBiz = new PmsRequirementBiz();
            PmsRequirement pmsRequirement = new PmsRequirement();
            pmsRequirement.RequirementPeriod = DropDownListWeekPeriod.SelectedValue;
            GridViewRequirement.EmptyDataText = "No Data.";
            IList<PmsRequirement> pmsRequirementList = pmsRequirementBiz.GetPmsRequirementByWeekPeriod(pmsRequirement);
            GridViewRequirement.DataSource = pmsRequirementList;
            GridViewRequirement.DataBind();
        }

        #endregion

        #region GridViewRequirement_RowDataBound
        protected void GridViewRequirement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView == null)
            {
                return;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BindDropDownList(e.Row);
            }

            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }

            PmsRequirement dataItem = (PmsRequirement)e.Row.DataItem;

            if (dataItem == null)
            {
                return;
            }

            // 如果SD栏位有值则取SD栏位,否则取SE栏位当做SD，以此类推取QA栏位
            string sd = dataItem.Sd.Trim();
            if (string.IsNullOrEmpty(sd))
            {
                if (string.IsNullOrEmpty(dataItem.Se))
                {
                    sd = dataItem.Qa;
                }
                else
                {
                    sd = dataItem.Se;
                }
            }

            if (sd.ToUpper().Trim() != "TBD")
            {
                Label labelSd = (Label)e.Row.FindControl("labelSd");
                labelSd.Text = sd;
            }
            else
            {
                Label labelSd = (Label)e.Row.FindControl("labelSd");
                labelSd.Text = "";
            }

            if (e.Row.RowIndex == gridView.EditIndex)
            {
                DropDownList dropDownListStatus = (DropDownList)e.Row.FindControl("DropDownListStatus");
                dropDownListStatus.SelectedValue = dataItem.Status;
            }

        }

        private void BindDropDownList(GridViewRow row)
        {
            DropDownList dropDownListStatus = (DropDownList)row.FindControl("DropDownListStatus");

            if (dropDownListStatus != null)
            {
                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1Data2ByType("PM", "RequirementStatus");

                dropDownListStatus.DataSource = pmsSysList;
                dropDownListStatus.DataTextField = "data1";
                dropDownListStatus.DataValueField = "data1";
                dropDownListStatus.DataBind();
            }
        }
        #endregion

        #region GridViewRequirement_RowCommand
        protected void GridViewRequirement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int currentRowIndex = int.Parse(e.CommandArgument.ToString());
            string commandName = e.CommandName;
            switch (commandName)
            {
                case "Edit":
                    GridViewRequirement.EditIndex = currentRowIndex;
                    BindGrid();
                    break;
                case "Update":
                    TextBox textBoxCRId = (TextBox)GridViewRequirement.Rows[currentRowIndex].FindControl("textBoxCRId");
                    string crId = Server.HtmlDecode(textBoxCRId.Text).Trim();
                    if (!CheckUpdate(crId))
                    {
                        return;
                    }
                    DropDownList dropDownListStatus = (DropDownList)GridViewRequirement.Rows[currentRowIndex].FindControl("DropDownListStatus");
                    string status = Server.HtmlDecode(dropDownListStatus.SelectedValue).Trim();
                    int serial = int.Parse(GridViewRequirement.DataKeys[currentRowIndex]["Serial"].ToString());
                    PmsRequirementBiz pmsRequirementBiz = new PmsRequirementBiz();
                    PmsRequirement pmsRequirement = new PmsRequirement();
                    pmsRequirement.Serial = serial;
                    pmsRequirement.CrId = crId;
                    pmsRequirement.Status = status;
                    pmsRequirement.Maintaindate = System.DateTime.Now;
                    pmsRequirement.Maintainuser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                    if (!pmsRequirementBiz.UpdatePmsRequirement(pmsRequirement))
                    {
                        Msgbox("Update failed!");
                        return;
                    }
                    GridViewRequirement.EditIndex = -1;
                    BindGrid();
                    break;
                case "Cancel":
                    GridViewRequirement.EditIndex = -1;
                    BindGrid();
                    break;
                default:
                    break;
            }
        }

        private bool CheckUpdate(string crId)
        {
            if (crId == string.Empty)
            {
                Msgbox("Please input CR No !");
                return false;
            }
            PmsItarmMappingBiz pmsItarmMappingBiz = new PmsItarmMappingBiz();
            IList<PmsItarmMapping> listPmsItarmMapping = pmsItarmMappingBiz.SelectPmsItarmMapping(crId, string.Empty);
            if (listPmsItarmMapping == null || listPmsItarmMapping.Count == 0)
            {
                Msgbox("Please input correct CR No !");
                return false;
            }
            return true;
        }
        #endregion

        #region GridViewEvent
        protected void GridViewRequirement_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridViewRequirement_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridViewRequirement_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        #endregion
    }
}
