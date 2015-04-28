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
    public partial class MaintainUser : PageBase//System.Web.UI.Page
    {
        protected readonly BugInquiryBiz bugInquiryBiz = new BugInquiryBiz();
        protected readonly BaseDataDomainBiz baseDataDomainBiz = new BaseDataDomainBiz();
        protected readonly BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindDropDown();
                this.Table_Visable.Attributes.Add("style", "display:none");
            }
        }

        public void BindDropDown()
        {

            IList<BaseDataDepartment> baseDataDepartment = bugInquiryBiz.SelectBaseDataDepartmentNameId();
            DropDownListDepartment.DataSource = baseDataDepartment;
            DropDownListDepartment.DataTextField = "Name";
            DropDownListDepartment.DataValueField = "Id";
            DropDownListDepartment.DataBind();
            DropDownListDepartment.Items.Insert(0, "");
            ViewState["baseDataDepartment"] = baseDataDepartment;

            IList<BaseDataDomain> baseDataDomainList = baseDataDomainBiz.SelectBaseDataDomain(null);
            DropDownListDomain.DataSource = baseDataDomainList;
            DropDownListDomain.DataTextField = "Name";
            DropDownListDomain.DataValueField = "Id";
            DropDownListDomain.DataBind();
            DropDownListDomain.Items.Insert(0, "");
            ViewState["baseDataDomainList"] = baseDataDomainList;
        }

        protected void ButtonInquiry_Click(object sender, EventArgs e)
        {
            BindGrid(sender, e);
        }

        public void BindGrid(object sender, EventArgs e)
        {
            string departMentId = DropDownListDepartment.SelectedValue;
            string englishName = TextBoxEnglishName.Text.Trim();
            string ntDomain = DropDownListNTDomain.SelectedValue;
            string domain = DropDownListDomain.SelectedValue;
            string empNo = TextBoxEmpNo.Text.Trim();
            string extention = TextBoxextention.Text.Trim();
            string role = TextBoxRole.Text.Trim();

            grdViewData.DataSource = baseDataUserBiz.SelectBaseDataUserByMainTainUser(departMentId, englishName, ntDomain, domain, empNo, extention, role);
            grdViewData.DataBind();

            if (grdViewData.Rows.Count > 0)
            {
                this.Table_Visable.Attributes.Add("style", "display:block");
            }
            else
            {
                this.Table_Visable.Attributes.Add("style", "display:none");
            }
        }

        protected void grdViewData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           
        }

        protected void grdViewData_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdViewData_RowEditing(object sender, GridViewEditEventArgs e)
        {
          
        }

        protected void grdViewData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string id = ((Label)grdViewData.Rows[rowIndex].FindControl("Labelid")).Text.Trim();

            switch (e.CommandName)
            {
                case "Delete":

                    baseDataUserBiz.DeleteBaseDataUserById(id);
                    BindGrid(this, EventArgs.Empty);
                    break;
                case "Edit":
                    this.grdViewData.EditIndex = rowIndex;
                    BindGrid(this, EventArgs.Empty);

                    string domainid = ((Label)grdViewData.Rows[rowIndex].FindControl("LbDomainid")).Text.Trim();
                    string departMentId = ((Label)grdViewData.Rows[rowIndex].FindControl("LbDepartMentId")).Text.Trim();
                    string ntdomainold = ((Label)grdViewData.Rows[rowIndex].FindControl("lblNtdomainold")).Text.Trim();

                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDepartment")).DataSource = ViewState["baseDataDepartment"] as List<BaseDataDepartment>;
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDepartment")).DataTextField = "Name";
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDepartment")).DataValueField = "Id";
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDepartment")).DataBind();
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDepartment")).Items.Insert(0, "");
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDepartment")).SelectedValue = departMentId;

                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDomain")).DataSource = ViewState["baseDataDomainList"] as List<BaseDataDomain>;
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDomain")).DataTextField = "Name";
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDomain")).DataValueField = "Id";
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDomain")).DataBind();
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDomain")).Items.Insert(0, "");
                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDomain")).SelectedValue = domainid;

                    ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListNTDomain")).SelectedValue = ntdomainold;

                    break;

                case "Update":

                    string ntDomainValue = ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListNTDomain")).SelectedValue;
                    string ntDomainText = ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListNTDomain")).SelectedItem.Text;

                    string domainText = ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDomain")).SelectedItem.Text;
                    string domainValue = ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDomain")).SelectedValue;

                    string departmentText = ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDepartment")).SelectedItem.Text;
                    string departmentValue = ((DropDownList)grdViewData.Rows[rowIndex].FindControl("DropDownListDepartment")).SelectedValue;

                    string userName = ((TextBox)grdViewData.Rows[rowIndex].FindControl("TextBoxUserName")).Text.Trim();
                    string mailAddress = ((TextBox)grdViewData.Rows[rowIndex].FindControl("TextBoxMailAddress")).Text.Trim();
                    string role = ((TextBox)grdViewData.Rows[rowIndex].FindControl("TextBoxRole")).Text.Trim();
                    string extention = ((TextBox)grdViewData.Rows[rowIndex].FindControl("TextBoxExtention")).Text.Trim();

                    if (ntDomainValue == "" || domainValue == "" || userName == "" || mailAddress == "" || role == "" || extention == "")
                    {
                        AlertMessage("Please enter the complete information.");
                        grdViewData.EditIndex = -1;
                        BindGrid(sender, e);
                        return;
                    }

                    string maintainUser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                    string maintainTime = DateTime.Now.ToString("yyyy-MM-dd");
                    baseDataUserBiz.UpdateUserInformation(id, ntDomainText, maintainUser, domainValue, departmentValue,
                                                           userName, mailAddress, role, extention, maintainTime);

                    grdViewData.EditIndex = -1;
                    BindGrid(sender, e);

                    break;

                case "Cancel":
                    grdViewData.EditIndex = -1;
                    BindGrid(this, EventArgs.Empty);
                    break;
            }

        }

        protected void grdViewData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void grdViewData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }


        public void AlertMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('"+message+"');", true);
        
        }
    }
}
