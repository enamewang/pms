
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.PMS.AppCode;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace PMS.PMS.Maintain
{
    public partial class MeetingMinuteMaintain : PageBase
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
                InitPage();
                BindGrid();
            }
        }

        private void InitPage()
        {
            Session["MeetingPage_Refresh"] = "N";
            LoginName = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            ButtonCreateMeetingMinute.OnClientClick = "showModalDialog('CreateMeetingMinuteExcessivePage.aspx?Action=ADD&PmsID=" + PmsID
                                                    + "&CrID=" + CrID
                                                    + "&RandomID=" + Guid.NewGuid().ToString()
                                                    + "','','dialogWidth=750px;dialogHeight=auto;center=yes;help=no;status=no');";
        }

        public void BindGrid()
        {
            try
            {
                PmsMinHeadBiz pmsMinHeadBiz = new PmsMinHeadBiz();
                IList<PmsMinHead> pmsMinHeadList = pmsMinHeadBiz.SelectPmsMinHeadByPmsId(PmsID);
                this.gridViewMeetingMinute.DataSource = pmsMinHeadList;
                this.gridViewMeetingMinute.DataBind();

            }
            catch
            {
                Msgbox("BindGrid failure!");
            }
        }
        #endregion

        #region Button Create MeetingMinute
        protected void ButtonCreateMeetingMinute_Click(object sender, EventArgs e)
        {
            string docPageRefresh = (Session["MeetingPage_Refresh"] ?? string.Empty).ToString();
            if (docPageRefresh == "Y")
            {
                BindGrid();
                Session["MeetingPage_Refresh"] = "N";
            }

        }
        #endregion

        #region Button Edit MeetingMinute
        protected void ButtonEditMeetingMinute_Click(object sender, EventArgs e)
        {
            string docPageRefresh = (Session["MeetingPage_Refresh"] ?? string.Empty).ToString();
            if (docPageRefresh == "Y")
            {
                BindGrid();
                Session["MeetingPage_Refresh"] = "N";
            }

        }
        #endregion

        #region Grid View Event
        protected void gridViewMeetingMinute_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gridView = sender as GridView;
                if (gridView == null)
                {
                    return;
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                PmsMinHead dataItem = (PmsMinHead)e.Row.DataItem;
                if (dataItem == null)
                {
                    return;
                }

                e.Row.Attributes["PmsId"] = dataItem.PmsId.ToString();
                e.Row.Attributes["Mnid"] = dataItem.Mnid.ToString();

                Label labelTypeName = e.Row.FindControl("labelTypeName") as Label;
                labelTypeName.Text = GetMeetingMinuteTypeDesc(dataItem.MeetingType);



                PmsMinHead pmsMinHead = new PmsMinHeadBiz().SelectPmsMinHeadByPmsIdMinId(dataItem.PmsId.ToString(), dataItem.Mnid.ToString()).FirstOrDefault();
                PmsHead pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(dataItem.PmsId.ToString()).FirstOrDefault();

                PmsItarmMapping pmsItarmMapping = new PmsItarmMappingBiz().SelectPmsItarmMappingOther(dataItem.PmsId.ToString()).FirstOrDefault();

                string crId = pmsItarmMapping.CrId;

                if (LoginName.Equals(pmsMinHead.Creator) || LoginName.Equals(pmsHead.Sd))
                {
                    ImageButton imageButtonEdit = e.Row.FindControl("imageButtonEdit") as ImageButton;
                    if (imageButtonEdit != null)
                    {
                        imageButtonEdit.Attributes.Add("onclick", string.Format("return OpenEditMeetingMinute('{0}','{1}','{2}');", dataItem.PmsId.ToString(), crId, dataItem.Mnid.ToString()));
                    }
                }

                ImageButton imageButtonDetail = e.Row.FindControl("imageButtonDetail") as ImageButton;
                if (imageButtonDetail != null)
                {
                    imageButtonDetail.Attributes.Add("onclick", string.Format("return OpenViewMeetingMinute('{0}','{1}','{2}');", dataItem.PmsId.ToString(), crId, dataItem.Mnid.ToString()));
                }


                IssueFreeBiz issueFreeBiz = new IssueFreeBiz();
                IList<BfIssueinfo> listBfIssueinfo = issueFreeBiz.GetIssueinfo(crId, dataItem.Mnid);
                string returnIssueID = string.Empty;
                int i = 1;
                string issueViewUrl= ConfigurationManager.AppSettings["IssueViewUrl"];
                if(listBfIssueinfo.Count>0)
                {
                    returnIssueID+= "<table cellSpacing='0' cellPadding='0' border='0'>";
                    foreach (BfIssueinfo bfIssueinfo in listBfIssueinfo)
                    {
                       string issueID = bfIssueinfo.Issueid.ToString();
                       string url = issueViewUrl+issueID;
                       returnIssueID = returnIssueID + "<tr><td>"
                                    + "<a id='FileList" + i + "' style='color:Blue;cursor:hand;white-space:nowrap;text-overflow:ellipsis;overflow:hidden' "
									+ "HREF = '"+url+"' Target='_blank'>"+ issueID +"</a></td></tr>";
								i++;
                    }
           
				    returnIssueID += "</table>";
                    HtmlContainerControl divOpenIssue = e.Row.FindControl("divOpenIssue") as HtmlContainerControl;
                    divOpenIssue.InnerHtml = returnIssueID;
                }
            }

            catch (Exception)
            {

                Msgbox("GridView Bind Fail!");
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
        protected void gridViewMeetingMinute_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gridViewMeetingMinute_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int currentRowIndex = int.Parse(e.CommandArgument.ToString());
            string minID = gridViewMeetingMinute.Rows[currentRowIndex].Attributes["Mnid"];
            string pmsID = gridViewMeetingMinute.Rows[currentRowIndex].Attributes["PmsId"];
            string message = string.Empty;
            PmsMinHead pmsMinHead = new PmsMinHeadBiz().SelectPmsMinHeadByPmsIdMinId(pmsID, minID).FirstOrDefault();
            switch (e.CommandName)
            {
                case "Delete":
                    # region Delete

                    if (!LoginName.Equals(pmsMinHead.Creator))
                    {
                        Msgbox("this item only can be deleted by creator!");
                        return;
                    }
                    int returnSerial = new PmsMinHeadBiz().DeletePmsMinHeadAndPmsMinConclution(pmsID, minID, out message);
                    if (returnSerial == 0)
                    {
                        Msgbox(message);
                        return;
                    }
                    BindGrid();
                    break;
                    # endregion
                case "EditPage":
                    # region Edit
                    PmsHead pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(pmsID).FirstOrDefault();
                    if (!LoginName.Equals(pmsMinHead.Creator) && !LoginName.Equals(pmsHead.Sd))
                    {
                        Msgbox("this item only can be edit by creator or sd!");
                        return;
                    }
                    break;
                    # endregion
                default:
                    break;
            }

        }

        protected void gridViewMeetingMinute_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        #endregion



    }
}
