using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.PMS.AppCode;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.Security.Cryptography;

namespace PMS.PMS.Maintain
{
    public partial class BugInquiry : PageBase
    {
        protected readonly BugInquiryBiz bugInquiryBiz = new BugInquiryBiz();

        private string m_BugViewUrl;
        private string BugViewUrl
        {
            get
            {
                if (string.IsNullOrEmpty(m_BugViewUrl))
                {
                    m_BugViewUrl = ConfigurationManager.AppSettings["BugViewUrl"];
                }
                return m_BugViewUrl;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownList();
                //TextBoxCrNo.Text = Request["CrID"];
                //ButtonInquiry_Click(sender, e);
            }

        }

        protected void ButtonInquiry_Click(object sender, EventArgs e)
        {
            BindGrid(this, EventArgs.Empty);
        }

        protected void BindGrid(object sender, EventArgs e)
        {
            DateTime crCloseDataFrom = new DateTime();
            DateTime crCloseDataTo = new DateTime();
            DateTime bugCreateDateFrom = new DateTime();
            DateTime bugCreateDateTo = new DateTime();
            DateTime bugResolveDateFrom = new DateTime();
            DateTime bugResolveDateTo = new DateTime();
            DateTime bugCloseDateFrom = new DateTime();
            DateTime bugCloseDateTo = new DateTime();
            if (!string.IsNullOrEmpty(dateTextBoxCrCloseDataFrom.Text.Trim()))
                crCloseDataFrom = DateTime.Parse(dateTextBoxCrCloseDataFrom.Text.Trim());
            if (!string.IsNullOrEmpty(dateTextBoxCrCloseDataTo.Text.Trim()))
                crCloseDataTo = DateTime.Parse(dateTextBoxCrCloseDataTo.Text.Trim()).AddDays(1).AddSeconds(-1);
            if (!string.IsNullOrEmpty(dateTextBoxBugCreateDateFrom.Text.Trim()))
                bugCreateDateFrom = DateTime.Parse(dateTextBoxBugCreateDateFrom.Text.Trim());
            if (!string.IsNullOrEmpty(dateTextBoxBugCreateDateTo.Text.Trim()))
                bugCreateDateTo = DateTime.Parse(dateTextBoxBugCreateDateTo.Text.Trim()).AddDays(1).AddSeconds(-1);
            if (!string.IsNullOrEmpty(dateTextBoxBugResolveDateFrom.Text.Trim()))
                bugResolveDateFrom = DateTime.Parse(dateTextBoxBugResolveDateFrom.Text.Trim());
            if (!string.IsNullOrEmpty(dateTextBoxBugResolveDateTo.Text.Trim()))
                bugResolveDateTo = DateTime.Parse(dateTextBoxBugResolveDateTo.Text.Trim()).AddDays(1).AddSeconds(-1);
            if (!string.IsNullOrEmpty(dateTextBoxBugCloseDateFrom.Text.Trim()))
                bugCloseDateFrom = DateTime.Parse(dateTextBoxBugCloseDateFrom.Text.Trim());
            if (!string.IsNullOrEmpty(dateTextBoxBugCloseDateTo.Text.Trim()))
                bugCloseDateTo = DateTime.Parse(dateTextBoxBugCloseDateTo.Text.Trim()).AddDays(1).AddSeconds(-1);

            IList<BfBuginfo> resultBug = bugInquiryBiz.GetBugInfoCrInfor
                   (
                   TextBoxCrNo.Text.Trim(), DropDownListCrSd.SelectedValue.Trim(),
                   DropDownListCrQa.SelectedValue.Trim(), DropDownListBugOwner.SelectedValue.Trim(),
                   DropDownListTeam.SelectedValue.Trim(), DropDownListBugCreator.SelectedValue.Trim(),
                   crCloseDataFrom, crCloseDataTo, bugCreateDateFrom, bugCreateDateTo, bugResolveDateFrom,
                         bugResolveDateTo, bugCloseDateFrom, bugCloseDateTo
                         );

            GridViewBug.DataSource = resultBug;
            GridViewBug.DataBind();
        }

        private void BindDropDownList()
        {
            IList<BaseDataDepartment> baseDataDepartment = bugInquiryBiz.SelectBaseDataDepartmentNameId();
            DropDownListTeam.DataSource = baseDataDepartment;
            DropDownListTeam.DataTextField = "Name";
            DropDownListTeam.DataValueField = "Id";
            DropDownListTeam.DataBind();
            //DropDownListTeam.Items.Insert(0, "");

            IList<BaseDataUser> baseDataUser = bugInquiryBiz.SelectUserName();
            DropDownListCrSd.DataSource = baseDataUser;
            DropDownListCrSd.DataTextField = "UserName";
            DropDownListCrSd.DataValueField = "UserName";
            DropDownListCrSd.DataBind();
            DropDownListCrSd.Items.Insert(0, "");

            DropDownListCrQa.DataSource = baseDataUser;
            DropDownListCrQa.DataTextField = "UserName";
            DropDownListCrQa.DataValueField = "UserName";
            DropDownListCrQa.DataBind();
            DropDownListCrQa.Items.Insert(0, "");

            DropDownListBugOwner.DataSource = baseDataUser;
            DropDownListBugOwner.DataTextField = "UserName";
            DropDownListBugOwner.DataValueField = "UserName";
            DropDownListBugOwner.DataBind();
            DropDownListBugOwner.Items.Insert(0, "");

            DropDownListBugCreator.DataSource = baseDataUser;
            DropDownListBugCreator.DataTextField = "UserName";
            DropDownListBugCreator.DataValueField = "UserName";
            DropDownListBugCreator.DataBind();
            DropDownListBugCreator.Items.Insert(0, "");

        }

        protected void GridViewBug_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCrId = (Label)e.Row.FindControl("lblCrId");
                Label lblBugId = (Label)e.Row.FindControl("lblBugId");
                HiddenField HiddenFieldPmsId = (HiddenField) e.Row.FindControl("HiddenFieldPmsId");

                if (!string.IsNullOrEmpty(lblCrId.Text.Trim()))
                {
                    lblCrId.Font.Underline = true;
                    lblCrId.ForeColor = Color.Blue;
                    lblCrId.Style.Add("cursor", "hand");

                    lblCrId.Attributes.Add("onclick", "javascript:window.location='ProjectsInformation.aspx?PmsID=" +HiddenFieldPmsId.Value + "'; return false;");
                }

                if (!string.IsNullOrEmpty(lblBugId.Text.Trim()))
                {
                    lblBugId.Font.Underline = true;
                    lblBugId.ForeColor = Color.Blue;
                    lblBugId.Style.Add("cursor", "hand");
                    lblBugId.Attributes.Add("onclick", "javascript:window.open('" + BugViewUrl + lblBugId.Text.Trim() + "&UserName=" + Server.UrlEncode(GetBase64Encode(WSC.GlobalDefinition.Cookie_LoginUser.Replace(".", " "))) + "');");
                }
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



    }
}
