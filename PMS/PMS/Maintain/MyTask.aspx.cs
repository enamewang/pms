using System;
using System.Collections;
using System.Collections.Generic;
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
using PMS.PMS.AppCode;
using Qisda.PMS.Entity;
using Qisda.PMS.Business;
using Qisda.PMS.Common;
using WSC.Framework;

namespace PMS.PMS.Maintain
{
    public partial class MyTask : PageBase //:WSC.FramePage
    {
        protected ProjectsInformationBiz m_ProjectsInformationBiz = new ProjectsInformationBiz();

        private string m_PmsID;
        public string PmsID
        {
            get
            {
                if (string.IsNullOrEmpty(m_PmsID))
                {
                    m_PmsID = (Request.QueryString["PmsID"] ?? string.Empty).Trim();
                }
                return m_PmsID;
                //string pmsID = (Request.QueryString["PmsID"] ?? string.Empty).Trim();
                //return pmsID;
            }
            set
            {
                m_PmsID = value;
            }

        }

        private string m_LoginName;
        public string LoginName
        {
            get
            {
                if (string.IsNullOrEmpty(m_LoginName))
                {
                    m_LoginName = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                }
                return m_LoginName;
            }
            set { m_LoginName = value; }
        }

        private BaseDataUser m_CurrentUser;
        public BaseDataUser CurrentUser
        {
            get
            {
                if (m_CurrentUser == null)
                {
                    m_CurrentUser = new BaseDataUser();


                    //对当前的CurrentUser设置属性
                    // ProjectsInformationBiz projectsInformationBiz=new ProjectsInformationBiz();
                    if (!m_ProjectsInformationBiz.GetCurrentUser(ref m_CurrentUser, PmsID, LoginName))
                    {
                        Msgbox("InitUserRole failure!");
                    }

                }
                return m_CurrentUser;
            }
            set
            {
                m_CurrentUser = value;
                //对当前的CurrentUser的属性设置
                if (!m_ProjectsInformationBiz.GetCurrentUser(ref m_CurrentUser, PmsID, LoginName))
                {
                    Msgbox("InitUserRole failure!");
                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EditPMSID"] != null)
            {
                PmsID = Session["EditPMSID"].ToString();
                Session["EditPMSID"] = null;

                string strAction = "SDPEDIT";
                if (Session["ViewAction"] != null)
                {
                    strAction = Session["ViewAction"].ToString();
                    Session["ViewAction"] = null;
                }

                Response.Redirect("ProjectsInformation.aspx?Action=" + strAction + "&PmsID=" + PmsID);

            }

            ViewState["SortExpression"] = "CreateDate";
            BindDataList(this.DataListMyTaskPast, PmsCommonEnum.EnumTime.Past);
            BindDataList(this.DataListMyTaskToday, PmsCommonEnum.EnumTime.Today);
            BindDataList(this.DataListMyTaskFuture, PmsCommonEnum.EnumTime.Future);

            if (CurrentUser.IsOrgPMO || CurrentUser.IsOrgRDManager)
            {
                DivPmoTask.Visible = true;
                DivPmoTaskAudit.Visible = true;
                DivPmoTaskAgent.Visible = true;
                BindGrid(sender, e);
            }
            else
            {
                DivPmoTask.Visible = false;
            }
        }
        protected void BindDataList(DataList dataList, PmsCommonEnum.EnumTime type)
        {
            MyTaskBiz myTaskBusiness = new MyTaskBiz();
            SdpDetail detail = new SdpDetail();
            detail.Resource = WSC.GlobalDefinition.Cookie_LoginUser.Trim().Replace(" ", ".");
            dataList.DataSource = myTaskBusiness.GetMyTaskData(detail, type);
            //dataList.DataSource = new List<MyTaskCondition>()
            //{

            //};
            dataList.DataBind();
            if (dataList.Items.Count == 0)
            {
                dataList.ShowHeader = false;
            }
        }

        protected void DataListMyTaskPast_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            ItemDataBound(e);

        }
        protected void DataListMyTaskToday_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ItemDataBound(e);
        }

        protected void DataListMyTaskFuture_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ItemDataBound(e);
        }

        protected void ItemDataBound(DataListItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header)
            {
                HiddenField serial = e.Item.FindControl("HiddenFieldSerial") as HiddenField;
                HiddenField headSerial = e.Item.FindControl("HiddenFieldHeadSerial") as HiddenField;
                Label taskName = e.Item.FindControl("LabelTaskName") as Label;
                Label project = e.Item.FindControl("LabelProject") as Label;

                //var url = "../Maintain/EditApprovedTask.aspx?PmsID=" + pmsId + "&CrId=" + crId + "&Phase=" + phase + "&Serial=" + serial + "&Radom=" + Math.random();
                //var features = "dialogWidth=630px;dialogHeight=540px;center=yes;help=no;status=no;scroll=no";System.Guid.NewGuid().ToString()
                //var result = window.showModalDialog(url, "", features);
                PmsHead pmsHead = new PmsHeadBiz().SelectCrIdSystemVersionByPmsId(headSerial.Value.Trim());
                string crId = pmsHead.CrId;
                if (taskName != null)
                {
                    taskName.Attributes.Add("onmouseover", "TaskNameMouseOver(this)");
                    taskName.Attributes.Add("onmouseout", "TaskNameMouseOut(this)");
                    //taskName.Attributes.Add("onclick", "window.open('MyTaskDetail.aspx?Serial=" + serial.Value.Trim() + "')");
                    //taskName.Attributes.Add("onclick", "showModalDialog('MyTaskDetail.aspx?id=" + 
                    //                        + "&Action=Detail&Serial=" + serial.Value.Trim() + "','','dialogWidth=720px;dialogHeight=450px;center=yes;help=no;status=no;resizable=yes;scroll=no');");
                    taskName.Attributes.Add("onclick", "showModalDialog('EditApprovedTask.aspx?PmsID=" + headSerial.Value.Trim()
                                            + "&CrId=" + crId + "&Serial=" + serial.Value.Trim() + "&Radom=" + System.Guid.NewGuid().ToString() + "','','dialogWidth=630px;dialogHeight=545px;center=yes;help=no;status=no;resizable=yes;scroll=no');document.getElementById('ButtonRefresh').click()");

                    project.Attributes.Add("onmouseover", "TaskNameMouseOver(this)");
                    project.Attributes.Add("onmouseout", "TaskNameMouseOut(this)");
                    //project.Attributes.Add("onclick", "window.open('SDPDetail.aspx?Action=Detail&HeadSerial=" + headSerial.Value.Trim() + "')");
                    project.Attributes.Add("onclick", "javascript:window.location='ProjectsInformation.aspx?PmsID=" + headSerial.Value.Trim() + "'");
                    //" href is :" + (href == null) ? string.Empty : href.InnerText +


                }
            }
        }


        #region PMO Task

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

        protected void gridViewMain_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string crId = string.Empty;
                //string project = string.Empty;
                //string module = string.Empty;

                if (e.Row.DataItem != null)
                {
                    crId = Server.UrlEncode(DataBinder.Eval(e.Row.DataItem, "crid").ToString());
                }

                ImageButton imageButtonDetail = (ImageButton)e.Row.FindControl("imageButtonDetail");
                imageButtonDetail.OnClientClick = "javascript:window.location='" + "ProjectsInformation.aspx?PmsID=" + gridViewMain.DataKeys[e.Row.RowIndex][0] + "'; return false;";

            }
        }
        protected void gridViewTaskAudit_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string crId = string.Empty;

                if (e.Row.DataItem != null)
                {
                    crId = Server.UrlEncode(DataBinder.Eval(e.Row.DataItem, "crid").ToString());
                }

                ImageButton imageButtonDetail = (ImageButton)e.Row.FindControl("imageButtonDetail");
                imageButtonDetail.OnClientClick = "javascript:window.location='" + "ProjectsInformation.aspx?PmsID=" + gridViewTaskAudit.DataKeys[e.Row.RowIndex][0] + "'; return false;";

            }
        }

        protected void gridViewTaskAgent_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string crId = string.Empty;

                if (e.Row.DataItem != null)
                {
                    crId = Server.UrlEncode(DataBinder.Eval(e.Row.DataItem, "crid").ToString());
                }

                ImageButton imageButtonDetail = (ImageButton)e.Row.FindControl("imageButtonDetail");
                imageButtonDetail.OnClientClick = "javascript:window.location='" + "ProjectsInformation.aspx?PmsID=" + gridViewTaskAgent.DataKeys[e.Row.RowIndex][0] + "'; return false;";

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
                }
                else
                {
                    #region Get Values
                    PmsHead pmsHead = new PmsHead();
                    pmsHead.Vid = "PM";
                    pmsHead.StageName = PmsCommonEnum.ProjectStage.AssignMember.GetDescription();
                    #endregion

                    PmsHeadBiz pmsHeadBiz = new PmsHeadBiz();
                    IList<PmsHead> pmsHeadList = pmsHeadBiz.SelectPmsHeadOther(pmsHead);

                    #region Sort Modify By Albee
                    List<PmsHead> SdpHeadList = (List<PmsHead>)pmsHeadList;
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

                            case "Priority":
                                SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return y.Priority.CompareTo(x.Priority); });
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

                            case "Priority":
                                SdpHeadList.Sort(delegate(PmsHead x, PmsHead y) { return x.Priority.CompareTo(y.Priority); });
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

                    gridViewMain.DataSource = SdpHeadList;
                    #endregion

                    #region Get gridViewTaskAudit Values
                    //借用Creator传LoginName
                    pmsHead.Creator = LoginName;
                    IList<PmsHead> pmsHeadListAudit = pmsHeadBiz.SelectPmsHeadForAuditor(pmsHead);
                    List<PmsHead> SdpHeadListAudit = (List<PmsHead>)pmsHeadListAudit;
                    gridViewTaskAudit.DataSource = SdpHeadListAudit;
                    #endregion
                    IList<PmsHead> pmsHeadListAgent = pmsHeadBiz.SelectPmsHeadForAgent(pmsHead);
                    List<PmsHead> SdpHeadListAgent = (List<PmsHead>)pmsHeadListAgent;
                    gridViewTaskAgent.DataSource = SdpHeadListAgent;
                    #region Get gridViewTaskAgent Values

                    #endregion

                    gridViewMain.DataBind();
                    gridViewTaskAudit.DataBind();
                    gridViewTaskAgent.DataBind();
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('BindGrid failure!');", true);
            }

            #endregion
        }
        #endregion
    }
}
//gridViewTaskAudit  DivPmoTaskAudit  gridViewTaskAgent DivPmoTaskAgent