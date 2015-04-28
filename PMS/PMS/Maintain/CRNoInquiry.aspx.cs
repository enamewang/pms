using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.PMS.AppCode;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;

namespace PMS.PMS.Maintain
{
    public partial class CRNoInquiry :PageBase
    {
        protected readonly CRNoInquiryBiz m_CRNoInquiryBiz = new CRNoInquiryBiz();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Buffer = true;
            this.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            this.Response.Expires = 0;
            this.Response.CacheControl = "no-cache";

            if (!IsPostBack)
            {
               BindGrid(this, EventArgs.Empty);
            }
        }

        protected void ButtonInquiry_Click(object sender, EventArgs e)
        {
            BindGrid(this, EventArgs.Empty);
        }


        protected void GridViewCrId_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header)
            {
                e.Row.Attributes["onclick"] = "selectRow(this)";
                e.Row.Attributes["ondblclick"] = "gridDbClick(this)";
            }
        }
        protected void GridViewCrId_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='lightBlue'");
                e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            }
        }

        public void BindGrid(object sender, EventArgs e)
        {
            IList<ItarmCrList> itarmCrListResult = m_CRNoInquiryBiz.SelectCrIdNamePmSystemSite(TextBoxCRId.Text.Trim(), TextBoxCRName.Text.Trim(), TextBoxPM.Text.Trim(), TextBoxSystemName.Text.Trim());
            GridViewCrId.DataSource = itarmCrListResult;
            GridViewCrId.DataBind();
        }
    }
}
