#region -- Page Information --
//////////////////////////////////////////////////////////////////////////////////
// File name: CreateService.aspx.cs    
// Copyright (C) 2011 Qisda Corporation. All rights reserved.    
// Author:		  Ename Wang   
// ALTER  Date:   2012/04/09
// Current Version:  1.0
// Description:   behind code of AutoExecutionSetup.aspx
// History: 
// 
//      Date     |    Time    |    Author   |  Modification 
// 1  2012/03/21 |   18:01:39 |  Ename Wang |  Create
//////////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.IO;
using System.Web;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PMS.PMS.AppCode;
using WSC;
using WSC.Common;
using WSC.Framework;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;

using Qisda.Web;
using Qisda.IO;
using System.Data;
using System.Configuration;
using Qisda.Security.Cryptography;


namespace PMS.PMS.Maintain
{
    public partial class EditMeetingMinute : PageBase
    {
        #region View State
        private string PmsID
        {
            get
            {
                object obj = ViewState["PmsID"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["PmsID"] = value; }
        }

        private string CrID
        {
            get
            {
                object obj = ViewState["CrID"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["CrID"] = value; }
        }

        private string MinID
        {
            get
            {
                object obj = ViewState["MinID"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["MinID"] = value; }
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

        private IList<PmsMinconclution> InitPmsMinconclutionList
        {
            get
            {
                return (IList<PmsMinconclution>)ViewState["InitPmsMinconclutionList"];
            }
            set { ViewState["InitPmsMinconclutionList"] = value; }
        }


        private string PmsMinconclutionList
        {
            get
            {
                object obj = ViewState["PmsMinconclutionList"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["PmsMinconclutionList"] = value; }
        }

        #region GetLastContactWindows
        protected IList<string> GetLastContactWindows(int editIndex)
        {
            try
            {
                return GridViewConclusion.Rows.Cast<GridViewRow>()
                    .Select(row => row.RowIndex != editIndex
                                       ? ((Label)(row.Cells[1].FindControl("LabelDesc"))).Text
                                       : EditContactWindow)
                    .ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        protected string EditContactWindow
        {
            get { return ViewState["ListContactWindow"].ToString(); }
            set { ViewState["ListContactWindow"] = value; }
        }

        protected PmsMinconclution EditPmsMinconclution
        {
            get { return (PmsMinconclution)ViewState["EditSerial"]; }
            set { ViewState["EditSerial"] = value; }
        }
        #endregion

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["PmsID"] != null)
                {
                    PmsID = Request["PmsID"];
                }

                if (Request["CrID"] != null)
                {
                    CrID = Request["CrID"];
                }

                if (Request["MinID"] != null)
                {
                    MinID = Request["MinID"];
                }
                InitPage(); //初始化控件
                SetValue(); //初始化值
            }
        }

        private void SetValue()
        {
            try
            {
                PmsMinHead pmsMinHead = new PmsMinHeadBiz().SelectPmsMinHeadByPmsIdMinId(PmsID, MinID).FirstOrDefault();
                if (pmsMinHead == null)
                {
                    return;
                }
                this.DropDownListMeetingMinuteType.SelectedValue = GetMeetingMinuteTypeDesc(pmsMinHead.MeetingType);
                this.TextBoxHost.Text = pmsMinHead.Host;
                this.DropDownListVenue.SelectedValue = pmsMinHead.Venue;
                this.TextBoxRecorder.Text = pmsMinHead.Recorder;
                this.TextBoxMeetingStartDate.Text = PmsCommonBiz.FormatDate(pmsMinHead.StartTime, "yyyy-MM-dd");
                this.TextBoxMeetingEndDate.Text = PmsCommonBiz.FormatDate(pmsMinHead.EndTime, "yyyy-MM-dd");
                this.DropDownListStartHour.SelectedValue = GetHourFromDatetime(pmsMinHead.StartTime) + ":";
                this.DropDownListStartMinute.SelectedValue = GetMinuteFromDatetime(pmsMinHead.StartTime);
                this.DropDownListEndHour.SelectedValue = GetHourFromDatetime(pmsMinHead.EndTime) + ":";
                this.DropDownListEndMinute.SelectedValue = GetMinuteFromDatetime(pmsMinHead.EndTime);
                this.TextBoxSubject.Text = pmsMinHead.Subject;
                this.TextBoxAttendee.Text = pmsMinHead.Attendee;
            }
            catch
            {
                Msgbox("Set Value Failed!");
            }

        }

        private string GetMeetingMinuteTypeDesc(int type)
        {
            switch (type)
            {
                case (int)PmsCommonEnum.MeetingType.PESMeeting:
                    return "PES Meeting";
                case (int)PmsCommonEnum.MeetingType.PISMeeting:
                    return "PIS Meeting";
                case (int)PmsCommonEnum.MeetingType.STPMeeting:
                    return "STP Meeting";
                case (int)PmsCommonEnum.MeetingType.STCMeeting:
                    return "STC Meeting";
                case (int)PmsCommonEnum.MeetingType.RLNMeeting:
                    return "RLN Meeting";
                default:
                    return "Other";
            }

        }

        private string GetHourFromDatetime(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmm").Substring(8, 2);
        }

        private string GetMinuteFromDatetime(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmm").Substring(10, 2);
        }


        private void InitPage()
        {
            try
            {
                LoginName = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                BindDropDown();
                InitTextBox();
                BindGrid();
                GridViewIssueBind();
                ButtonSaveTop.Attributes.Add("onclick", "return CheckBeforeSave()");
                ButtonSaveUnder.Attributes.Add("onclick", "return CheckBeforeSave()");
                ButtonCancelTop.OnClientClick = "window.close()";
                ButtonCancelUnder.OnClientClick = "window.close()";

                //取得project，module
                PmsHead pmsHead = new PmsHead();
                pmsHead.Vid = "PM";
                pmsHead.UserName = LoginName;
                pmsHead.CrId = CrID;

                PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                IList<PmsHead> pmsHeadList = pmsHeadBiz.SelectPmsHeadOther(pmsHead);
                string project = pmsHeadList[0].BugFreeProject;
                string module = pmsHeadList[0].BugFreeModule;


                string IssueCreateUrl = ConfigurationManager.AppSettings["IssueCreateUrl"];
                string paraMeter = "&PmsID=" + Server.UrlEncode(GetBase64Encode(PmsID))
                                 + "&UserName=" + Server.UrlEncode(GetBase64Encode(LoginName.Replace(".", " ")))
                                 + "&CrID=" + Server.UrlEncode(GetBase64Encode(CrID))
                                 + "&BugFreeProject=" + Server.UrlEncode(GetBase64Encode(project))
                                 + "&BugFreeModule=" + Server.UrlEncode(GetBase64Encode(module))
                                 + "&MNID=" + Server.UrlEncode(GetBase64Encode(MinID));

                ButtonCreateIssue.OnClientClick = "javascript:window.open('" + IssueCreateUrl + paraMeter + "');";
            }
            catch
            {
                Msgbox("Init Page Failed !");
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


        private void BindDropDown()
        {
            PmsSysBiz pmsSysBiz = new PmsSysBiz();
            IList<PmsSys> listPmsSys = pmsSysBiz.SelectData2ByTypeData1("PM", "MeetingMinute", "MeetingType");
            DropDownListMeetingMinuteType.DataSource = listPmsSys;
            DropDownListMeetingMinuteType.DataTextField = "data2";
            DropDownListMeetingMinuteType.DataValueField = "data2";
            DropDownListMeetingMinuteType.DataBind();

            DropDownListMeetingMinuteType.Items.Insert(0, new ListItem());
            DropDownListMeetingMinuteType.Items[0].Text = "";
            DropDownListMeetingMinuteType.Items[0].Value = "";

            IList<PmsSys> listPmsSysVenue = pmsSysBiz.SelectData2ByTypeData1("PM", "MeetingMinute", "Venue");
            DropDownListVenue.DataSource = listPmsSysVenue;
            DropDownListVenue.DataTextField = "data2";
            DropDownListVenue.DataValueField = "data2";
            DropDownListVenue.DataBind();

            DropDownListVenue.Items.Insert(0, new ListItem());
            DropDownListVenue.Items[0].Text = "";
            DropDownListVenue.Items[0].Value = "";

            IList<PmsSys> listPmsSysHour = pmsSysBiz.SelectData2ByTypeData1("PM", "MeetingTime", "Hour");
            DropDownListStartHour.DataSource = listPmsSysHour;
            DropDownListStartHour.DataTextField = "data2";
            DropDownListStartHour.DataValueField = "data2";
            DropDownListStartHour.DataBind();

            DropDownListEndHour.DataSource = listPmsSysHour;
            DropDownListEndHour.DataTextField = "data2";
            DropDownListEndHour.DataValueField = "data2";
            DropDownListEndHour.DataBind();

            // 选中 10;

            IList<PmsSys> listPmsSysMinute = pmsSysBiz.SelectData2ByTypeData1("PM", "MeetingTime", "Minute");
            DropDownListStartMinute.DataSource = listPmsSysMinute;
            DropDownListStartMinute.DataTextField = "data2";
            DropDownListStartMinute.DataValueField = "data2";
            DropDownListStartMinute.DataBind();

            DropDownListEndMinute.DataSource = listPmsSysMinute;
            DropDownListEndMinute.DataTextField = "data2";
            DropDownListEndMinute.DataValueField = "data2";
            DropDownListEndMinute.DataBind();

            //选中 00;

        }

        private void InitTextBox()
        {
            TextBoxMeetingStartDate.Attributes.Add("onkeypress", "return false;");
            TextBoxMeetingEndDate.Attributes.Add("onkeypress", "return false;");
            RenderScript.RenderCalendarScript(TextBoxMeetingStartDate, "-");
            RenderScript.RenderCalendarScript(TextBoxMeetingEndDate, "-");
            TextBoxHost.Text = string.Empty;
            TextBoxRecorder.Text = LoginName;
            InitTextBoxAttendee();

        }

        private void InitTextBoxAttendee()
        {
            PmsHead head = new PmsHeadBiz().SelectPmsHeadByPmsId(PmsID)[0];
            string attendee = string.Empty;
            if (!string.IsNullOrEmpty(head.Pm))
            {
                attendee = attendee + head.Pm + ";";
            }
            if (!string.IsNullOrEmpty(head.Sd) && !attendee.Contains(head.Sd))
            {
                attendee = attendee + head.Sd + ";";
            }
            if (!string.IsNullOrEmpty(head.Qa) && !attendee.Contains(head.Qa))
            {
                attendee = attendee + head.Qa + ";";
            }
            if (!string.IsNullOrEmpty(head.Se) && !attendee.Contains(head.Se))
            {
                attendee = attendee + head.Se + ";";
            }
            if (attendee.Contains(";"))
            {
                attendee = attendee.TrimEnd(';');
            }
            TextBoxAttendee.Text = attendee;
        }

        #endregion

        #region DropDownListMeetingMinuteType_SelectedIndexChanged
        protected void DropDownListMeetingMinuteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeTextBoxHost();
        }

        private void ChangeTextBoxHost()
        {
            PmsHead head = new PmsHeadBiz().SelectPmsHeadByPmsId(PmsID)[0];
            if (DropDownListMeetingMinuteType.SelectedValue == PmsCommonEnum.MeetingType.PESMeeting.GetDescription())
            {
                TextBoxHost.Text = head.Pm;
            }
            if (DropDownListMeetingMinuteType.SelectedValue == PmsCommonEnum.MeetingType.PISMeeting.GetDescription())
            {
                TextBoxHost.Text = head.Sd;
            }
            if (DropDownListMeetingMinuteType.SelectedValue == PmsCommonEnum.MeetingType.STPMeeting.GetDescription())
            {
                TextBoxHost.Text = head.Qa;
            }
            if (DropDownListMeetingMinuteType.SelectedValue == PmsCommonEnum.MeetingType.STCMeeting.GetDescription())
            {
                TextBoxHost.Text = head.Qa;
            }
            if (DropDownListMeetingMinuteType.SelectedValue == PmsCommonEnum.MeetingType.RLNMeeting.GetDescription())
            {
                TextBoxHost.Text = head.Sd;
            }
            if (DropDownListMeetingMinuteType.SelectedValue == PmsCommonEnum.MeetingType.Other.GetDescription())
            {
                TextBoxHost.Text = head.Sd;
            }
        }

        private bool IsSd(string sd, string loginName)
        {
            return IsTheRole(sd, loginName);
        }

        private bool IsTheRole(string role, string loginName)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(role))
            {
                string[] roles = role.Split(';');
                foreach (string roleName in roles)
                {
                    if (roleName == loginName)
                    {
                        result = true;
                    }
                }

            }
            return result;
        }

        #endregion



        /// <summary>
        ///  For Page_Load
        /// </summary>
        private void BindGrid()
        {
            // 到数据库捞取
            PmsMinconclutionBiz pmsMinconclutionBiz = new PmsMinconclutionBiz();
            IList<PmsMinconclution> pmsMinconclutionList = pmsMinconclutionBiz.SelectPmsMinconclutionByMinId(MinID);
            InitPmsMinconclutionList = pmsMinconclutionList; //编辑前的数据保存到InitPmsMinconclutionList
            if (pmsMinconclutionList != null && pmsMinconclutionList.Count > 0)
            {
                GridViewConclusion.DataSource = pmsMinconclutionList;
                GridViewConclusion.DataBind();
            }
            else
            {
                pmsMinconclutionList = new List<PmsMinconclution>() { new PmsMinconclution() };
                GridViewConclusion.DataSource = pmsMinconclutionList;
                GridViewConclusion.DataBind();
                GridViewConclusion.Rows[0].Visible = false; //隐藏了一行需要特别注意，保存的时候排除掉
            }
        }

        private void GridViewBind(IEnumerable<PmsMinconclution> pmsMinconclutionList)
        {
            if (pmsMinconclutionList == null || pmsMinconclutionList.Count() == 0)
            {
                pmsMinconclutionList = new List<PmsMinconclution>() { new PmsMinconclution() };
                GridViewConclusion.DataSource = pmsMinconclutionList;
                GridViewConclusion.DataBind();
                GridViewConclusion.Rows[0].Visible = false; //隐藏了一行需要特别注意，保存的时候排除掉
            }
            else
            {
                GridViewConclusion.DataSource = pmsMinconclutionList;
                GridViewConclusion.DataBind();
            }

        }


        protected void GridViewConclusion_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        #region Row Command
        protected void GridViewConclusion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int currentRowIndex = int.Parse(e.CommandArgument.ToString());
            TextBox textBoxDesc;
            Label labelSerial;
            PmsMinconclution pmsMinconclution = new PmsMinconclution();
            PmsMinconclutionBiz pmsMinconclutionBiz = new PmsMinconclutionBiz();
            IList<string> listContactWindow;
            string currentDesc;

            IList<PmsMinconclution> pmsMinconclutionList;

            switch (e.CommandName)
            {
                case "Save":

                    #region Save
                    if (GridViewConclusion.EditIndex != -1)
                    {
                        Msgbox("Please complete the editing action!");
                        return;
                    }

                    textBoxDesc = (TextBox)GridViewConclusion.FooterRow.FindControl("TextBoxDesc");
                    currentDesc = Server.HtmlDecode(textBoxDesc.Text).Trim();
                    if (string.IsNullOrEmpty(currentDesc))
                    {
                        Msgbox("Please input Title!");
                        return;
                    }

                    listContactWindow = GetLastContactWindows(-1);
                    if (CheckContactWindow(currentDesc, listContactWindow))
                    {
                        Msgbox(currentDesc + " " + "already exist!");
                        return;
                    }

                    pmsMinconclutionList = GetPmsMinConlutionList(-1);
                    pmsMinconclution.Description = currentDesc;
                    pmsMinconclutionList.Add(pmsMinconclution);
                    GridViewBind(pmsMinconclutionList);

                    //pmsMinconclution.Mnid = MinID;
                    //pmsMinconclution.Description = Server.HtmlDecode(textBoxDesc.Text).Trim();
                    //pmsMinconclution.Creator = LoginName;
                    //pmsMinconclution.CreateDate = System.DateTime.Now;
                    //if (pmsMinconclutionBiz.InsertPmsMinconclutionByMinId(pmsMinconclution) == 0)
                    //{
                    //    Msgbox("Save Failed!");
                    //    return;
                    //}
                    //BindGrid();
                    PageRegisterStartupScript("Refresh();");
                    break;

                    #endregion

                case "Delete":

                    # region Delete
                    if (GridViewConclusion.EditIndex != -1)
                    {
                        Msgbox("Please complete the editing action!");
                        return;
                    }
                    listContactWindow = GetLastContactWindows(-1);
                    listContactWindow.RemoveAt(currentRowIndex);

                    pmsMinconclutionList = GetPmsMinConlutionList(-1);
                    pmsMinconclutionList.RemoveAt(currentRowIndex);
                    GridViewBind(pmsMinconclutionList);

                    //labelSerial = (Label)GridViewConclusion.Rows[currentRowIndex].FindControl("LabelSerial");
                    //if (pmsMinconclutionBiz.DeletePmsMinconclutionBySerial(labelSerial.Text) == 0)
                    //{
                    //    Msgbox("Delete Failed!");
                    //    return;
                    //}
                    //BindGrid();
                    PageRegisterStartupScript("Refresh();");
                    break;
                    # endregion

                case "Edit":

                    #region Edit
                    if (GridViewConclusion.EditIndex != -1)
                    {
                        Msgbox("Please complete the editing action!");
                        return;
                    }

                    listContactWindow = GetLastContactWindows(-1);
                    EditContactWindow = listContactWindow[currentRowIndex];

                    pmsMinconclutionList = GetPmsMinConlutionList(-1);
                    EditPmsMinconclution = pmsMinconclutionList[currentRowIndex];
                    GridViewConclusion.EditIndex = currentRowIndex;
                    GridViewBind(pmsMinconclutionList);

                   
                    break;
                    #endregion

                case "Update":

                    #region Update
                    textBoxDesc = (TextBox)GridViewConclusion.Rows[currentRowIndex].FindControl("TextBoxDesc");
                    currentDesc = Server.HtmlDecode(textBoxDesc.Text).Trim();
                    if (string.IsNullOrEmpty(Server.HtmlDecode(textBoxDesc.Text).Trim()))
                    {
                        Msgbox("Please input Title!");
                        return;
                    }

                    listContactWindow = GetLastContactWindows(GridViewConclusion.EditIndex);
                    if (CheckContactWindow(currentDesc, listContactWindow))
                    {
                        Msgbox(currentDesc + " " + "already exist!");
                        return;
                    }
                    pmsMinconclutionList = GetPmsMinConlutionList(currentRowIndex);
                    pmsMinconclutionList[currentRowIndex].Description = currentDesc;
                    GridViewConclusion.EditIndex = -1;
                    GridViewBind(pmsMinconclutionList);

                    //labelSerial = (Label)GridViewConclusion.Rows[currentRowIndex].FindControl("LabelSerial");
                    //int intSerial;
                    //int.TryParse(labelSerial.Text, out intSerial);
                    //pmsMinconclution.Serial = intSerial;
                    //pmsMinconclution.Description = Server.HtmlDecode(textBoxDesc.Text).Trim();
                    //pmsMinconclution.Creator = LoginName;
                    //pmsMinconclution.CreateDate = System.DateTime.Now;
                    //if (pmsMinconclutionBiz.UpdatePmsMinconclutionBySerial(pmsMinconclution) == 0)
                    //{
                    //    Msgbox("Update Failed!");
                    //    return;
                    //}
                    //GridViewConclusion.EditIndex = -1;
                    //BindGrid();
                    break;
                    #endregion

                case "Cancel":

                    #region Cancel

                    GridViewConclusion.EditIndex = -1;
                    pmsMinconclutionList = GetPmsMinConlutionList(currentRowIndex);
                    GridViewBind(pmsMinconclutionList);
                    break;
                    #endregion

                default:
                    break;
            }
        }

        /// <summary>
        /// 用于未保存之前，操作Grid绑定
        /// </summary>
        /// <param name="listContactWindow"></param>
        /// <returns></returns>
        private IList<PmsMinconclution> GetPmsMinConlutionList(int editRow)
        {
            IList<PmsMinconclution> pmsMinconclutionList = new List<PmsMinconclution>();

            foreach (GridViewRow row in GridViewConclusion.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (row.RowIndex != editRow) 
                    {
                        PmsMinconclution pmsMinconclution = new PmsMinconclution();
                        Label labelDesc = (Label)row.FindControl("LabelDesc");
                        Label labelSerial = (Label)row.FindControl("LabelSerial");
                        pmsMinconclution.Description = labelDesc.Text;
                        pmsMinconclution.Serial = int.Parse(labelSerial.Text);
                        pmsMinconclutionList.Add(pmsMinconclution);
                    }
                    else
                    {
                        pmsMinconclutionList.Add(EditPmsMinconclution);
                    }
                   
                }
                
            }
            return pmsMinconclutionList;
        }

        //检查ContactWindow是否合法
        protected bool CheckContactWindow(string contactWindow, IList<string> listContactWindow)
        {
            bool result = false;
            foreach (var window in listContactWindow)
            {
                if (string.Equals(contactWindow, window))
                {
                    result = true;
                }
            }
            return result;
        }

        protected void GridViewConclusion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridViewConclusion_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridViewConclusion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridViewConclusion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        #endregion

        #region GridViewIssue
        private void GridViewIssueBind()
        {
            IssueFreeBiz issueFreeBiz = new IssueFreeBiz();
            IList<BfIssueinfo> listBfIssueinfo = issueFreeBiz.GetIssueinfo(CrID, MinID);
            GridViewIssue.DataSource = listBfIssueinfo;
            GridViewIssue.DataBind();
        }
        #endregion


        protected void ButtonSaveTop_Click(object sender, EventArgs e)
        {
            SaveAndSendMail();
        }

        protected void ButtonSaveUnder_Click(object sender, EventArgs e)
        {
            SaveAndSendMail();
        }

        private void SaveAndSendMail()
        {
            try
            {
                if (GridViewConclusion.EditIndex != -1)
                {
                    Msgbox("Please complete the editing action!");
                    return;
                }

                string message;
                if (!CheckUser(out message))
                {
                    Msgbox(message);
                    return;
                }

                if (!CheckConclusionsIssues(out message))
                {
                    Msgbox(message);
                    return;
                }

                if (!Save(out message))
                {
                    Msgbox(message);
                    return;
                }

                if (!SendMail(out message))
                {
                    Msgbox(message);
                    return;
                }

                Session["MeetingPage_Refresh"] = "Y";
                Msgbox("Save Successfully!");
                PageRegisterStartupScript("window.close();");
            }

            catch
            {
                Msgbox("Save failed!");
            }

        }

        #region Check Before Save
        private bool CheckUser(out string message)
        {
            message = string.Empty;

            PmsCommonBiz pmsCommonBiz = new PmsCommonBiz();

            string[] hosts = this.TextBoxHost.Text.Trim().Split(';');
            foreach (string host in hosts)
            {
                if (host != string.Empty && (!pmsCommonBiz.CheckUser(host)))
                {
                    message = "The host name:" + host + " does not exist!";
                    TextBoxHost.Focus();
                    return false;
                }
            }


            string[] recorders = this.TextBoxRecorder.Text.Trim().Split(';');
            foreach (string recorder in recorders)
            {
                if (recorder != string.Empty && (!pmsCommonBiz.CheckUser(recorder)))
                {
                    message = "The recorder name:" + recorder + " does not exist!";
                    TextBoxRecorder.Focus();
                    return false;
                }
            }

            string[] attendees = this.TextBoxAttendee.Text.Trim().Split(';');
            foreach (string attendee in attendees)
            {
                if (attendee != string.Empty && (!pmsCommonBiz.CheckUser(attendee)))
                {
                    message = "The attendee name:" + attendee + " does not exist!";
                    return false;
                }
            }

            return true;
        }

        private bool CheckConclusionsIssues(out string message)
        {
            message = string.Empty;
            bool result = false;
            IList<BfIssueinfo> bfIssueinfoList = new IssueFreeBiz().GetIssueinfo(CrID, MinID);
            if (GridViewConclusion.Rows.Count > 2 || bfIssueinfoList.Count > 0)
            {
                result = true;
            }
            else
            {
                result = false;
                message = "Conclusions and Issues must not be empty at the same time!";
            }
            return result;
        }
        
        #endregion

        #region Save
        private bool Save(out string message)
        {
            message = string.Empty;
            PmsMinHeadBiz pmsMinHeadBiz = new PmsMinHeadBiz();
            string creator = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            DateTime createDate = System.DateTime.Now;
            string minID = MinID; //从ViewState中取得minID

            PmsMinHead PmsMinHead = GetPmsMinHead(createDate, creator, minID);

            IList<PmsMinconclution> pmsMinconclutionList = GetPmsMinConlution(createDate, creator, minID);

            if (!pmsMinHeadBiz.UpdatePmsMinHeadAndPmsMinConclution(PmsMinHead, pmsMinconclutionList,InitPmsMinconclutionList))
            {
                message = "Save failed!";
                return false;
            }
            return true;
        }

        private PmsMinHead GetPmsMinHead(DateTime createDate, string creator, string minID)
        {
            PmsMinHead pmsMinHead = new PmsMinHead();
            pmsMinHead.CreateDate = createDate;
            pmsMinHead.Creator = creator;
            pmsMinHead.Mnid = minID;
            pmsMinHead.PmsId = PmsID;
            pmsMinHead.MeetingType = GetMeetingMinuteType(DropDownListMeetingMinuteType.SelectedValue);
            pmsMinHead.Host = TextBoxHost.Text;
            pmsMinHead.Venue = DropDownListVenue.SelectedValue;
            pmsMinHead.Recorder = TextBoxRecorder.Text;
            pmsMinHead.StartTime = Convert.ToDateTime(TextBoxMeetingStartDate.Text + " " + DropDownListStartHour.SelectedValue + DropDownListStartMinute.SelectedValue + ":00");
            pmsMinHead.EndTime = Convert.ToDateTime(TextBoxMeetingEndDate.Text + " " + DropDownListEndHour.SelectedValue + DropDownListEndMinute.SelectedValue + ":00");
            pmsMinHead.Subject = TextBoxSubject.Text;
            pmsMinHead.Attendee = TextBoxAttendee.Text;
            return pmsMinHead;
        }


        private int GetMeetingMinuteType(string desc)
        {
            switch (desc)
            {
                case "PES Meeting":
                    return (int)PmsCommonEnum.MeetingType.PESMeeting;
                case "PIS Meeting":
                    return (int)PmsCommonEnum.MeetingType.PISMeeting;
                case "STP Meeting":
                    return (int)PmsCommonEnum.MeetingType.STPMeeting;
                case "STC Meeting":
                    return (int)PmsCommonEnum.MeetingType.STCMeeting;
                case "RLN Meeting":
                    return (int)PmsCommonEnum.MeetingType.RLNMeeting;
                default:
                    return (int)PmsCommonEnum.MeetingType.Other;
            }

        }
        /// <summary>
        ///  用于保存
        /// </summary>
        /// <param name="createDate"></param>
        /// <param name="creator"></param>
        /// <param name="minID"></param>
        /// <returns></returns>
        private IList<PmsMinconclution> GetPmsMinConlution(DateTime createDate, string creator, string minID)
        {
            IList<PmsMinconclution> pmsMinconclutionList = new List<PmsMinconclution>();

            foreach (GridViewRow row in GridViewConclusion.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow && row.Visible == true) // 为了排除隐藏的行
                {
                    PmsMinconclution pmsMinconclution = new PmsMinconclution();
                    pmsMinconclution.CreateDate = createDate;
                    pmsMinconclution.Creator = creator;
                    pmsMinconclution.Mnid = minID;

                    Label labelSerial = (Label)row.FindControl("LabelSerial");
                    pmsMinconclution.Serial = int.Parse(labelSerial.Text);

                    Label labelDesc = (Label)row.FindControl("LabelDesc");
                    pmsMinconclution.Description = labelDesc.Text;

                    pmsMinconclutionList.Add(pmsMinconclution);
                }

            }
            return pmsMinconclutionList;
        }
        #endregion

        #region Send Mail
        private bool SendMail(out string message)
        {
            message = string.Empty;
            if (!new MailBiz().SendCreateMeetingMinuteMail(PmsID, MinID, LoginName, "edited"))
            {
                message = "Send mail failed!";
                return false;
            }
            return true;
        }
        #endregion

        protected void ButtonCreateIssue_Click(object sender, EventArgs e)
        {
            GridViewIssueBind();
            PageRegisterStartupScript("Refresh();");
        }
    }
}
