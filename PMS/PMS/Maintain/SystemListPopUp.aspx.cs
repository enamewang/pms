using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.PMS.AppCode;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;

namespace PMS.PMS.Maintain
{
    public partial class SystemListPopUp : PageBase
    {
        protected SystemListPopUpBiz m_SystemListPopUpBiz = new SystemListPopUpBiz();



        //public object ResultDataSource
        //{
        //    get
        //    {
        //        if (ViewState["ResultDataSource"] != null)
        //        {
        //            return ViewState["ResultDataSource"];
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        ViewState["ResultDataSource"] = value;
        //    }
        //}



        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Buffer = true;
            this.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            this.Response.Expires = 0;
            this.Response.CacheControl = "no-cache";

            // 绑定分页控件
            // PagerControler.CreatePagingControler(this.GridViewSystem, this.Pager1,new BindGridViewDelegate(BindGrid));

            if (!IsPostBack)
            {
                BindDropDownList();

                string site = Server.UrlDecode(Request["Site"].Trim());
                string pm = Server.UrlDecode(Request["PM"].Replace(".", " ").Trim());
                string domain = Server.UrlDecode(Request["Domain"].Trim());
                string systemName = Server.UrlDecode(Request["System"].Trim());

                SetDefultValue(site, pm, domain, systemName);

                BindGrid(this, EventArgs.Empty);
                // BindGrid();
            }

        }

        #region 绑定DropDownList
        private void BindDropDownList()
        {
            IList<ItarmSystem> resultSite = m_SystemListPopUpBiz.GetItarmSystemSite();
            DropDownListSite.DataSource = resultSite;
            DropDownListSite.DataTextField = "Site";
            DropDownListSite.DataValueField = "Site";
            DropDownListSite.DataBind();
            DropDownListSite.Items.Insert(0, "");

            IList<string> resultDomain = m_SystemListPopUpBiz.GetItarmSystemDomain();
            DropDownListDomain.DataSource = resultDomain;
            //DropDownListDomain.DataTextField = "value";
            //DropDownListDomain.DataValueField = "value";
            //DropDownListDomain.DataTextField = "SystemDomain";
            //DropDownListDomain.DataValueField = "SystemDomain";
            DropDownListDomain.DataBind();
            DropDownListDomain.Items.Insert(0, "");

            IList<ItarmUser> resultPM = m_SystemListPopUpBiz.GetItarmPmNoName();
            DropDownListPM.DataSource = resultPM;
            DropDownListPM.DataTextField = "EnglishName";
            DropDownListPM.DataValueField = "EnglishName";
            DropDownListPM.DataBind();
            DropDownListPM.Items.Insert(0, "");
        }
        #endregion

        private void SetDefultValue(string site, string pm, string domain, string systemName)
        {
            SetDropDownListItem(DropDownListSite, site);
            SetDropDownListItem(DropDownListPM, pm);
            SetDropDownListItem(DropDownListDomain, domain);
            TextBoxSysName.Text = systemName;
        }
        // public void BindGrid()
        public void BindGrid(object sender, EventArgs e)
        {
            if (sender == null)
            {
                DataTable dt = new DataTable();
                GridViewSystem.DataSource = dt;
                GridViewSystem.DataBind();
            }
            else
            {
                string bname = TextBoxSysName.Text.Trim();
                string ename = TextBoxSysName.Text.Trim();
                string cname = TextBoxSysName.Text.Trim();
                string domain = DropDownListDomain.SelectedValue;
                string pm = DropDownListPM.SelectedValue;
                string site = DropDownListSite.SelectedValue;

                IList<ItarmSystem> resultItarmSystemList = m_SystemListPopUpBiz.GetPersonsByName(bname, ename, cname,
                                                                                                 domain, pm, site);


                //if (resultItarmSystemList.Count > 0)
                //{
                //    this.Pager1.TotalRecords = resultItarmSystemList.Count;
                //    this.Pager1.Visible = true;    // 分页区不显示
                //    ResultDataSource = resultItarmSystemList;
                //    //this.GridViewResult.DataSource = result;
                //    //this.GridViewResult.DataBind();
                //}
                //else
                //{

                //}


                GridViewSystem.DataSource = resultItarmSystemList;
                GridViewSystem.DataBind();
            }
        }

        protected void ButtonInquiry_Click(object sender, EventArgs e)
        {
            BindGrid(this, EventArgs.Empty);
            // BindGrid();
        }

        protected void GridViewSystem_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header)
            {
                e.Row.Attributes["onclick"] = "selectRow(this)";
                e.Row.Attributes["ondblclick"] = "gridDbClick(this)";
            }
        }
        protected void GridViewSystem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='lightBlue'");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            }
        }


        //protected void GridViewSystem_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    GridViewSortExpression = e.SortExpression;
        //    int pageIndex = Pager1.PageIndex;
        //    GridViewSystem.DataSource = SortDataTable(ResultDataSource as DataTable, false);
        //    GridViewSystem.DataBind();
        //    Pager1.PageIndex = pageIndex;
        //}

        //private string GridViewSortExpression
        //{
        //    get { return ViewState["SortExpression"] as string ?? string.Empty; }
        //    set { ViewState["SortExpression"] = value; }
        //}

        //protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
        //{

        //    if (dataTable != null)
        //    {
        //        DataView dataView = new DataView(dataTable);
        //        if (GridViewSortExpression != string.Empty)
        //        {
        //            if (isPageIndexChanging)
        //            {
        //                dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
        //            }
        //            else
        //            {
        //                dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
        //            }
        //        }
        //        return dataView;
        //    }
        //    else
        //    {
        //        return new DataView();
        //    }
        //}
        //private string GridViewSortDirection
        //{
        //    get { return ViewState["SortDirection"] as string ?? "ASC"; }
        //    set { ViewState["SortDirection"] = value; }
        //}

        //private string GetSortDirection()
        //{
        //    switch (GridViewSortDirection)
        //    {
        //        case "ASC":
        //            GridViewSortDirection = "DESC";
        //            break;
        //        case "DESC":
        //            GridViewSortDirection = "ASC";
        //            break;
        //    }
        //    return GridViewSortDirection;
        //}

    }
}
