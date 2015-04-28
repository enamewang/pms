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
    public partial class ViewMeetingMinute : PageBase
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

                InitPage();
                SetValue();
                SetDisable();
            }
        }

        private void SetDisable()
        {
            this.DropDownListMeetingMinuteType.Enabled = false;
            this.TextBoxHost.ReadOnly = true;
            this.DropDownListVenue.Enabled = false;
            this.TextBoxRecorder.ReadOnly = true;
            this.TextBoxMeetingStartDate.ReadOnly = true;
            this.TextBoxMeetingEndDate.ReadOnly = true;
            this.DropDownListStartHour.Enabled = false;
            this.DropDownListStartMinute.Enabled = false;
            this.DropDownListEndHour.Enabled = false;
            this.DropDownListEndMinute.Enabled = false;
            this.TextBoxSubject.ReadOnly = true;
            this.TextBoxAttendee.ReadOnly = true;
           // this.GridViewConclusion.Enabled = false;

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

            }
            catch
            {
                Msgbox("Init Page Failed !");
            }
        }

        #region GridViewIssue
        private void GridViewIssueBind()
        {
            IssueFreeBiz issueFreeBiz = new IssueFreeBiz();
            IList<BfIssueinfo> listBfIssueinfo = issueFreeBiz.GetIssueinfo(CrID, MinID);
            GridViewIssue.DataSource = listBfIssueinfo;
            GridViewIssue.DataBind();
        }
        #endregion

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

            IList<PmsSys> listPmsSysMinute = pmsSysBiz.SelectData2ByTypeData1("PM", "MeetingTime", "Minute");
            DropDownListStartMinute.DataSource = listPmsSysMinute;
            DropDownListStartMinute.DataTextField = "data2";
            DropDownListStartMinute.DataValueField = "data2";
            DropDownListStartMinute.DataBind();

            DropDownListEndMinute.DataSource = listPmsSysMinute;
            DropDownListEndMinute.DataTextField = "data2";
            DropDownListEndMinute.DataValueField = "data2";
            DropDownListEndMinute.DataBind();

        }

        private void InitTextBox()
        {
            TextBoxMeetingStartDate.Attributes.Add("onkeypress", "return false;");
            TextBoxMeetingEndDate.Attributes.Add("onkeypress", "return false;"); 
        }

        private void BindGrid()
        {
            // 到数据库捞取
            PmsMinconclutionBiz pmsMinconclutionBiz = new PmsMinconclutionBiz();
            IList<PmsMinconclution> pmsMinconclutionList = pmsMinconclutionBiz.SelectPmsMinconclutionByMinId(MinID);
            if (pmsMinconclutionList != null)
            {
                GridViewConclusion.DataSource = pmsMinconclutionList;
                GridViewConclusion.DataBind();
            }
        }
        #endregion
    }
}
