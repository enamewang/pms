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
using Qisda.Security.Cryptography;
using WSC;
using WSC.Common;
using WSC.Framework;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.Collections.Generic;
using Qisda.Web;
using Qisda.IO;
using log4net;
using System.Text;

namespace PMS.PMS.Maintain
{
    public partial class InquiryAndExport : WSC.FramePage
    {
        ILog Log = LogManager.GetLogger("InquiryAndExport");
        BaseDataUser CurrentUser = new BaseDataUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ViewState["SortExpression"] = "CreateDate";
                InitPage();
                BindGrid(sender, e);
                //InitGrid(sender, e);
                //   Session["InquiryPage_Refresh"] = "N"; 
                // buttonCreate.OnClientClick = "showModalDialog('Create.aspx?Action=ADD&RandomID=" + Guid.NewGuid().ToString() + "','','dialogWidth=400px;dialogHeight=500px;center=yes;help=no;status=no;scroll=no');";
                buttonCreate.OnClientClick = "showModalDialog('CreatCRExcessivePage.aspx?Action=ADD&RandomID=" + Guid.NewGuid().ToString() + "','','dialogWidth=400px;dialogHeight=500px;center=yes;help=no;status=no;scroll=no');";

                // buttonCreateService.OnClientClick = "showModalDialog('CreateServiceExcessivePage.aspx?Action=ADD&RandomID=" + Guid.NewGuid().ToString() + "','',' dialogWidth=400px;dialogHeight=500px;center=yes;help=no;status=no;scroll=no;resizable=yes');";

                buttonCreateService.OnClientClick = "showModalDialog('CreateServiceExcessivePage.aspx?Action=ADD&RandomID=" + Guid.NewGuid().ToString() + "','',' dialogWidth=550px;dialogHeight=auto;center=yes;help=no;status=no');";
                //buttonCreateService.OnClientClick = "window.open('CreateServiceExcessivePage.aspx?Action=ADD&RandomID=" + Guid.NewGuid().ToString() + "','','width=400px,height=500px ');";
            }
        }

        #region Define Variable
        //added by Albee 2010-08-03 Sort
        private SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Descending;
                }

                return (SortDirection)ViewState["sortDirection"];

            }
            set
            {
                ViewState["sortDirection"] = value;
            }
        }

        private string LoginName
        {
            get
            {
                object obj = ViewState["LoginName"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["LoginName"] = value; }
        }

        private string UserStatus
        {
            get
            {
                object obj = ViewState["UserStatus"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["UserStatus"] = value; }
        }

        private string m_BugCreateUrl;
        private string BugCreateUrl
        {
            get
            {
                if (string.IsNullOrEmpty(m_BugCreateUrl))
                {
                    m_BugCreateUrl = ConfigurationManager.AppSettings["BugCreateUrl"];
                }
                return m_BugCreateUrl;
            }
        }
        #endregion

        private void InitPage()
        {
            LoginName = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            if (LoginName.ToUpper() == "TYLER.LIU" || LoginName.ToUpper() == "STANLEY.LEE")
            {
                textboxUserName.Text = string.Empty;
            }
            else
            {
                textboxUserName.Text = LoginName;
            }

            textboxCrNo.Text = Request["CrNo"];
            InitUserRole("");
            BindDropDownListDoMain();
            BindDropDownListType();
            BindDropDownListSite();
            BindDropDownListStage();
            BindDropDownListPriority();
        }

        private void BindDropDownListDoMain()
        {
            try
            {
                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                //IList<BaseDataDomain> baseDataDomainList = baseDataDomainBiz.SelectBaseDataDomain(null);
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Domain");

                dropdownlistDomain.DataSource = pmsSysList;
                dropdownlistDomain.DataTextField = "data1";
                dropdownlistDomain.DataValueField = "data1";
                dropdownlistDomain.DataBind();

                dropdownlistDomain.Items.Insert(0, new ListItem());
                dropdownlistDomain.Items[0].Text = "";
                dropdownlistDomain.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind Domain failure');", true);
                this.dropdownlistDomain.Focus();
            }
        }

        private void BindDropDownListType()
        {
            try
            {
                //PmsSys pmsSys = new PmsSys();
                //pmsSys.Vid = "PM";
                //pmsSys.Type = "Type";

                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Type");
                dropdownlistType.DataSource = pmsSysList;
                dropdownlistType.DataTextField = "Data1";
                dropdownlistType.DataValueField = "Data1";
                dropdownlistType.DataBind();

                dropdownlistType.Items.Insert(0, new ListItem());
                dropdownlistType.Items[0].Text = "";
                dropdownlistType.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind Type failure!');", true);
                this.dropdownlistType.Focus();
            }
        }

        private void BindDropDownListSite()
        {
            try
            {
                //PmsSys pmsSys = new PmsSys();
                //pmsSys.Vid = "PM";
                //pmsSys.Type = "Site";

                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Site");
                dropdownlistSite.DataSource = pmsSysList;
                dropdownlistSite.DataTextField = "Data1";
                dropdownlistSite.DataValueField = "Data1";
                dropdownlistSite.DataBind();

                dropdownlistSite.Items.Insert(0, new ListItem());
                dropdownlistSite.Items[0].Text = "";
                dropdownlistSite.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind Site failure');", true);
                this.dropdownlistSite.Focus();
            }

        }

        private void BindDropDownListStage()
        {
            try
            {
                PmsStageBiz pmsStageBiz = new PmsStageBiz();
                IList<PmsStage> pmsStageList = pmsStageBiz.SelectStageNameByVID("PM");
                dropdownlistStage.DataSource = pmsStageList;
                dropdownlistStage.DataTextField = "StageName";
                dropdownlistStage.DataValueField = "StageId";
                dropdownlistStage.DataBind();

                dropdownlistStage.Items.Insert(0, new ListItem());
                dropdownlistStage.Items[0].Text = "";
                dropdownlistStage.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind Stage failure');", true);
                this.dropdownlistStage.Focus();
            }
        }
        private void BindDropDownListPriority()
        {
            try
            {
                //PmsSys pmsSys = new PmsSys();
                //pmsSys.Vid = "PM";
                //pmsSys.Type = "Priority";

                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Priority");
                dropdownlistPriority.DataSource = pmsSysList;
                dropdownlistPriority.DataTextField = "Data1";
                dropdownlistPriority.DataValueField = "Data1";
                dropdownlistPriority.DataBind();

                dropdownlistPriority.Items.Insert(0, new ListItem());
                dropdownlistPriority.Items[0].Text = "";
                dropdownlistPriority.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind Priority failure');", true);
                this.dropdownlistPriority.Focus();
            }
        }


        public void InitGrid(object sender, EventArgs e)
        {

            try
            {
                PmsHead pmsHead = new PmsHead();
                pmsHead.Vid = "PM";
                pmsHead.NoStageName = "'CLose','HardClosed'";

                //if (UserStatus == "PM")
                //    pmsHead.Pm = textboxPM.Text.ToString().Trim();
                //else
                //    pmsHead.UserName = textboxUserName.Text.ToString().Trim();

                pmsHead.UserName = textboxUserName.Text.Trim();
                pmsHead.CrId = textboxCrNo.Text.Trim();

                PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                IList<PmsHead> pmsHeadList = pmsHeadBiz.SelectPmsHeadOther(pmsHead);
                gridViewMain.DataSource = pmsHeadList;
                gridViewMain.DataBind();
                gridViewMain.Columns[13].Visible = false;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('InitGrid failure!');", true);
            }
        }

        public void InitUserRole(string pmsId)
        {
            try
            {
                CurrentUser.LoginName = LoginName;

                BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
                IList<BaseDataUser> baseDataUserList = baseDataUserBiz.SelectBaseDataUser(CurrentUser.LoginName, CurrentUser.Role);

                if (baseDataUserList != null || baseDataUserList.Count > 0)
                {
                    CurrentUser = baseDataUserList[0];
                }
                CurrentUser = baseDataUserBiz.SetUserOrgRole(CurrentUser);
                CurrentUser = baseDataUserBiz.SetUserProjectRole(CurrentUser, pmsId);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('InitUserRole failure!');", true);
            }
        }

        public void BindGrid(object sender, EventArgs e)
        {
            #region Bind DataTable to GridView
            try
            {
                if (sender == null)
                {
                    DataTable dt = new DataTable();
                    gridViewMain.DataSource = dt;
                    gridViewMain.DataBind();
                    gridViewMain.Columns[13].Visible = false;
                }
                else
                {
                    #region Get Values
                    PmsHead pmsHead = new PmsHead();
                    pmsHead.Vid = "PM";

                    if (this.dropdownlistDomain.SelectedItem != null && this.dropdownlistDomain.SelectedItem.Text.ToUpper() != "")
                        pmsHead.Domain = this.dropdownlistDomain.SelectedItem.Text.ToString();
                    else
                        pmsHead.Domain = "";

                    pmsHead.System = textboxSystem.Text.Trim();
                    pmsHead.CrId = textboxCrNo.Text.Trim();
                    pmsHead.PmsName = textboxPmsName.Text.Trim();

                    if (this.dropdownlistType.SelectedItem != null && this.dropdownlistType.SelectedItem.Text.ToUpper() != "")
                        pmsHead.Type = this.dropdownlistType.SelectedItem.Value.ToString();
                    else
                        pmsHead.Type = "";

                    if (this.dropdownlistSite.SelectedItem != null && this.dropdownlistSite.SelectedItem.Text.ToUpper() != "")
                        pmsHead.Site = this.dropdownlistSite.SelectedItem.Value.ToString();
                    else
                        pmsHead.Site = "";

                    if (this.dropdownlistStage.SelectedItem != null && this.dropdownlistStage.SelectedItem.Text.ToUpper() != "")
                        pmsHead.StageName = this.dropdownlistStage.SelectedItem.Text.ToString().Trim();
                    else
                        pmsHead.StageName = "";

                    // pmsHead.Pm = textboxPM.Text.ToString().Trim();
                    // pmsHead.UserName = textboxUserName.Text.ToString().Trim();


                    if (this.dropdownlistPriority.SelectedItem != null && this.dropdownlistPriority.SelectedItem.Text.ToUpper() != "")
                        pmsHead.Priority = this.dropdownlistPriority.SelectedItem.Text.ToString().Trim();
                    else
                        pmsHead.Priority = "";

                    pmsHead.UserName = textboxUserName.Text.ToString().Trim();


                    if (this.dateTextBoxCreateDateFrom.Text.ToString().Trim() != "")
                    {
                        pmsHead.CreateDateFrom = DateTime.Parse(dateTextBoxCreateDateFrom.Text.ToString().Trim());
                    }

                    if (this.dateTextBoxCreateDateTo.Text.ToString().Trim() != "")
                    {
                        pmsHead.CreateDateTo = DateTime.Parse(dateTextBoxCreateDateTo.Text.ToString().Trim());
                    }

                    if (this.dateTextBoxReleaseDateFrom.Text.ToString().Trim() != "")
                    {
                        pmsHead.ReleaseDateFrom = DateTime.Parse(dateTextBoxReleaseDateFrom.Text.ToString().Trim());
                    }

                    if (this.dateTextBoxReleaseDateTo.Text.ToString().Trim() != "")
                    {
                        pmsHead.ReleaseDateTo = DateTime.Parse(dateTextBoxReleaseDateTo.Text.ToString().Trim());
                    }

                    if (this.dateTextBoxDueDateFrom.Text.ToString().Trim() != "")
                    {
                        pmsHead.DueDateFrom = DateTime.Parse(dateTextBoxDueDateFrom.Text.ToString().Trim());
                    }

                    if (this.dateTextBoxDueDateTo.Text.ToString().Trim() != "")
                    {
                        pmsHead.DueDateTo = DateTime.Parse(dateTextBoxDueDateTo.Text.ToString().Trim());
                    }
                    #endregion



                    PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                    IList<PmsHead> pmsHeadList = pmsHeadBiz.SelectPmsHeadOther(pmsHead);

                    #region Sort Modify By Albee
                    List<PmsHead> SdpHeadList = (List<PmsHead>)pmsHeadList;

                    if (SdpHeadList != null && SdpHeadList.Count > 0)
                    {

                        #region Descending

                        if (GridViewSortDirection == SortDirection.Descending)
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "CrId":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.CrId.CompareTo(x.CrId); });
                                    break;

                                case "Type":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.Type.CompareTo(x.Type); });
                                    break;

                                case "PmsName":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.PmsName.CompareTo(x.PmsName); });
                                    break;

                                case "Progress":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.Progress.CompareTo(x.Progress); });
                                    break;

                                case "DueDate":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.DueDate.CompareTo(x.DueDate); });
                                    break;

                                case "ReleaseDate":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.ReleaseDate.CompareTo(x.ReleaseDate); });
                                    break;

                                case "CreateDate":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.CreateDate.CompareTo(x.CreateDate); });
                                    break;

                                case "StageName":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.StageName.CompareTo(x.StageName); });
                                    break;

                                case "Pm":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.Pm.CompareTo(x.Pm); });
                                    break;

                                case "Sd":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.Sd.CompareTo(x.Sd); });
                                    break;

                                case "System":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.System.CompareTo(x.System); });
                                    break;

                                default:

                                    break;

                            }
                        }
                        #endregion

                        #region Ascending
                        else
                        {
                            switch (ViewState["SortExpression"].ToString())
                            {
                                case "CrId":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.CrId.CompareTo(y.CrId); });
                                    break;

                                case "Type":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.Type.CompareTo(y.Type); });
                                    break;

                                case "PmsName":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.PmsName.CompareTo(y.PmsName); });
                                    break;

                                case "Progress":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.Progress.CompareTo(y.Progress); });
                                    break;

                                case "DueDate":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.DueDate.CompareTo(y.DueDate); });
                                    break;

                                case "ReleaseDate":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.ReleaseDate.CompareTo(y.ReleaseDate); });
                                    break;

                                case "CreateDate":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.CreateDate.CompareTo(y.CreateDate); });
                                    break;

                                case "StageName":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.StageName.CompareTo(y.StageName); });
                                    break;

                                case "Pm":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.Pm.CompareTo(y.Pm); });
                                    break;

                                case "Sd":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.Sd.CompareTo(y.Sd); });
                                    break;

                                case "System":
                                    SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.System.CompareTo(y.System); });
                                    break;

                                default:

                                    break;

                            }
                        }
                        #endregion
                    }



                    //gridViewDetailList.DataSource = pmsSdpHeadList; //mark by Albee 2010-08-03
                    gridViewMain.DataSource = SdpHeadList;
                    #endregion

                    //gridViewMain.DataSource = pmsHeadList;
                    gridViewMain.DataBind();
                    gridViewMain.Columns[13].Visible = false;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('BindGrid failure!');", true);
            }

            #endregion
        }

        protected void gridViewMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string crId = string.Empty;
                string project = string.Empty;
                string module = string.Empty;

                if (e.Row.DataItem != null)
                {
                    crId = DataBinder.Eval(e.Row.DataItem, "crid").ToString();
                    project = DataBinder.Eval(e.Row.DataItem, "BugFreeProject").ToString();
                    module = DataBinder.Eval(e.Row.DataItem, "BugFreeModule").ToString();
                    //bugfree系统获取url参数时，需要进行解码Server.UrlDecode(Request.QueryString["BugFreeProject"].ToString());
                }

                ImageButton imageButtonDetail = (ImageButton)e.Row.FindControl("imageButtonDetail");
                imageButtonDetail.OnClientClick = "javascript:window.location='" + "ProjectsInformation.aspx?PmsID=" + gridViewMain.DataKeys[e.Row.RowIndex][0] + "'; ";
                //imageButtonDetail.OnClientClick = "javascript:window.open('" + ConfigurationSettings.AppSettings["PMSExternalSystemViewUrl"] + "?Action=VIEW&PmsID=" + gridViewMain.DataKeys[e.Row.RowIndex][0] + "'); "; // changed window.location to window.open by Ename Wang 20111115

                ImageButton imageButtonBugfree = (ImageButton)e.Row.FindControl("imageButtonBugfree");

                imageButtonBugfree.OnClientClick = "javascript:window.open('" + BugCreateUrl + "&PmsID=" + Server.UrlEncode(GetBase64Encode(gridViewMain.DataKeys[e.Row.RowIndex][0].ToString())) + "&UserName=" + Server.UrlEncode(GetBase64Encode(LoginName.Replace(".", " "))) + "&CrID=" + Server.UrlEncode(GetBase64Encode(crId)) + "&BugFreeProject=" + Server.UrlEncode(GetBase64Encode(project)) + "&BugFreeModule=" + Server.UrlEncode(GetBase64Encode(module)) + "');";
                //ImageButton imageButtonBugfreeInquire = (ImageButton)e.Row.FindControl("imageButtonBugfree");
                //imageButtonBugfreeInquire.OnClientClick = "javascript:window.location='" + "BugInquiry.aspx?PmsID=" + gridViewMain.DataKeys[e.Row.RowIndex][0] + "&CrID=" + crId + "';return false;";

            }
        }

        //Base64加密
        private string GetBase64Encode(string str)
        {
            string temp = string.Empty;
            try
            {
                temp = QBase64.Base64Encrypt(str);
                return temp;
            }
            catch (Exception)
            {
                return "";
            }
        }


        protected void gridViewMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //

        }

        //added by Albee OnSorting
        protected void gridViewMain_OnSorting(object sender, GridViewSortEventArgs e)
        {

            string sortExpression = e.SortExpression;
            ViewState["SortExpression"] = sortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
                GridViewSortDirection = SortDirection.Descending;
            else
                GridViewSortDirection = SortDirection.Ascending;

            BindGrid(sender, e);

        }

        protected void buttonInquiry_Click(object sender, EventArgs e)
        {
            BindGrid(sender, e);
        }

        protected void buttonExport_Click(object sender, EventArgs e)
        {
            //ArrayList excludedColumnList = new ArrayList();
            //excludedColumnList.Add("Bugfree");
            //excludedColumnList.Add("Detail");

            ////QWeb.ExportGridViewToExcel(gridViewMain, "PMS.xls", excludedColumnList, false);
            //ExportGridViewToExcel(gridViewMain, "PMS.xls", excludedColumnList, true);

            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); ;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("PMS.xls", System.Text.Encoding.UTF8).ToString());
            Response.ContentType = "application/vnd.ms-excel;charset=utf-8";

            ////解决出现乱码关键问题
            ////GridView中导出Excel的方法，大部分都是导出一个文本的html文件，
            ////利用了office能够编辑网页的功能，来实现。只是有时候导出的中文显示是乱码
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");

            this.EnableViewState = false;

            gridViewMain.Columns[0].Visible = false;
            gridViewMain.Columns[1].Visible = false;
            System.IO.StringWriter tw = new System.IO.StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            gridViewMain.RenderControl(hw);
            Response.Output.Write(tw.ToString());
            Response.Flush();
            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void imageButtonDetail_Click(object sender, ImageClickEventArgs e)
        {
            BindGrid(sender, e);
        }

        protected void buttonCreate_Click(object sender, EventArgs e)
        {
            //if (Session["InquiryPage_Refresh"].ToString() == "Y")
            //{
            BindGrid(sender, e);
            // }
        }

        protected void buttonCreateService_Click(object sender, EventArgs e)
        {
            BindGrid(sender, e);
        }

        /// <summary>
        ///  用GridView中子控件的标题文本替换此子控件
        /// </summary>
        /// <param name="control">GridView中的子控件</param>
        private void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }

        private void ExportGridViewToExcel(GridView gv, string fileName, ArrayList excludedColumnList, bool includeStyle)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Remove unwanted columns (header text listed in removeColumnList arraylist)
            if (excludedColumnList != null)
            {
                foreach (DataControlField field in gv.Columns)
                {
                    if (excludedColumnList.Contains(field.HeaderText))
                    {
                        field.Visible = false;
                    }
                }
            }

            if (!includeStyle)
            {
                //  Create a form to contain the grid
                Table table = new Table();

                table.GridLines = gv.GridLines;

                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    PrepareControlForExport(row);
                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);
            }
            else
            {
                Page currentPage = HttpContext.Current.Handler as Page;
                if (currentPage == null)
                {
                    return;
                }
                HtmlForm hf = new HtmlForm();

                currentPage.Controls.Add(hf);
                hf.Controls.Add(gv);
                bool oldViewState = gv.EnableViewState;
                hf.RenderControl(htw);
            }

            //  render the htmlwriter into the response                    
            DownLoadFile(Encoding.UTF8.GetBytes(sw.ToString()), fileName);
            //string tempXlsFile = QFile.GetTempFileName("xls");
            //QFile.SaveTextToFile(tempXlsFile, sw.ToString());
            //DownLoadFile(tempXlsFile, fileName);
        }

        private void DownLoadFile(byte[] buffer, string downloadName)
        {
            if (buffer == null || buffer.Length == 0)
            {
                throw new Exception("SpecifiedParameterError,buffer");
            }
            if (string.IsNullOrEmpty(downloadName))
            {
                throw new Exception("SpecifiedParameterError,downloadName");
            }
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition",
                               "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(downloadName) + "\"");
            Response.AddHeader("Content-Length", buffer.Length.ToString());
            // 根据文件的扩展名获取ContentType, 从而避免文件下载时的类型错误或者直接被浏览器打开的问题。
            // 例如在IE6中使用application/octet-stream的ContentType下载内容为xhtml格式的Excel文件时会被当作文本文件直接打开。
            Response.ContentType = "application/vnd.ms-excel;charset=utf-8";
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
            Response.BinaryWrite(buffer);
            // 停止页面的执行
            HttpContext.Current.Response.End();
        }


        #region Ilist to List Albee 2010-08-03
        /// <summary>
        /// 转换IList<T>为List<T>
        /// </summary>
        /// <typeparam name="T">指定的集合中泛型的类型</typeparam>
        /// <param name="gbList">需要转换的IList</param>
        /// <returns></returns>
        private List<T> ConvertIListToList<T>(IList<T> gbList) where T : class
        {
            if (gbList != null && gbList.Count >= 1)
            {
                List<T> list = new List<T>();
                for (int i = 0; i < gbList.Count; i++)
                {
                    T temp = gbList[i] as T;
                    if (temp != null)
                        list.Add(temp);
                }
                return list;
            }
            return null;
        }
        #endregion


    }
}
