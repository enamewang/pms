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
    public partial class AddUser : PageBase//System.Web.UI.Page
    {
        protected readonly BugInquiryBiz bugInquiryBiz = new BugInquiryBiz();
        protected readonly BaseDataDomainBiz baseDataDomainBiz = new BaseDataDomainBiz();
        protected readonly BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDown();
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

        public void AlertMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (baseDataUserBiz.CheckBaseDataUser(TextBoxEmpNo.Text.Trim()) == true)
            {
                AlertMessage("EMP No:" + TextBoxEmpNo.Text.Trim() + " has exist!");
                return;
            }
            if (baseDataUserBiz.GetTfsUserListUserNameCount(TextBoxEnglishName.Text.Trim()) == true)
            {
                AlertMessage("English Name:" + TextBoxEnglishName.Text.Trim() + " has exist!");
                return;
            }

            #region Insert PMS
            BaseDataUser baseDataUser = new BaseDataUser();
            baseDataUser.UserEmployeeNo = TextBoxEmpNo.Text.Trim();
            baseDataUser.Ntdomain = DropDownListNTDomain.SelectedValue;
            baseDataUser.UserName = TextBoxEnglishName.Text.Trim();
            baseDataUser.MailAddress = TextBoxEnglishName.Text.Trim() + "@qisda.com";
            baseDataUser.Password = "AIC0";
            baseDataUser.LoginName = TextBoxEnglishName.Text.Trim();
            baseDataUser.DomainId = Convert.ToInt32(DropDownListDomain.SelectedValue);
            baseDataUser.Active = "Y";
            baseDataUser.CreateDate = DateTime.Now;
            baseDataUser.CreateUser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            baseDataUser.MaintainUser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            baseDataUser.MaintainDate = DateTime.Now;
            baseDataUser.Extention = TextBoxextention.Text.Trim();
            baseDataUser.Role = TextBoxRole.Text.Trim();

            BaseDataDepartmentUser baseDataDepartmentUser = new BaseDataDepartmentUser();
            baseDataDepartmentUser.DepartmentId = Convert.ToInt32(DropDownListDepartment.SelectedValue);
            baseDataDepartmentUser.Type = "Master";
            baseDataDepartmentUser.Active = "Y";
            baseDataDepartmentUser.CreateDate = DateTime.Now;
            baseDataDepartmentUser.MaintainDate = DateTime.Now;
            baseDataDepartmentUser.CreateUser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            baseDataDepartmentUser.MaintainUser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");

            baseDataUserBiz.InsertBaseDataUserAndDepartmentUser(baseDataUser, baseDataDepartmentUser);
            #endregion

            #region insert TFS
            TfsUserList tfsUserListmodel = new TfsUserList();

            tfsUserListmodel.UserEmpNo = TextBoxEmpNo.Text.Trim();
            tfsUserListmodel.Domain = DropDownListNTDomain.SelectedValue;
            tfsUserListmodel.UserName = TextBoxEnglishName.Text.Trim();
            tfsUserListmodel.MailAddress = TextBoxEnglishName.Text.Trim() + "@qisda.com";
            tfsUserListmodel.Password = "cimsam";
            tfsUserListmodel.LoginName = TextBoxEnglishName.Text.Trim();
            tfsUserListmodel.Groupid = 1;

            if (TextBoxEnglishName.Text.Trim().Length > 4 && TextBoxEnglishName.Text.Trim().Substring(0, 4) == "ITO.")
            {
                tfsUserListmodel.Teamid = 2;
            }
            else
            {
                tfsUserListmodel.Teamid = baseDataUserBiz.GetTfsTeamForTeamid(DropDownListDepartment.SelectedItem.Text);
            }
            tfsUserListmodel.Active = "Y";
            tfsUserListmodel.Needaic1Approved = "Y";
            tfsUserListmodel.NeedDirectiveApproved = "Y";
            tfsUserListmodel.CreateDate = DateTime.Now;
            tfsUserListmodel.CreateUser = "Admin";
            tfsUserListmodel.MaintainDate = DateTime.Now;
            tfsUserListmodel.MaintainUser = "Admin";

            baseDataUserBiz.InsertTfsUserList(tfsUserListmodel);
            #endregion

            #region insert FramwWS
            if (baseDataUserBiz.GetWscUserRoleCount(TextBoxEnglishName.Text.Trim().Replace(" ", "").Replace(".", "")) == false)
            {
                WscUserRole wscUserRole = new WscUserRole();

                wscUserRole.SysId = "TFSSSM";
                wscUserRole.LoginName = TextBoxEnglishName.Text.Trim();
                wscUserRole.RoleId = "User";

                baseDataUserBiz.InsertWscUserRole(wscUserRole);
            }

            #endregion

            DropDownListDepartment.SelectedValue = "";
            DropDownListNTDomain.SelectedValue = "";
            DropDownListDomain.SelectedValue = "";
            TextBoxEmpNo.Text = "";
            TextBoxEnglishName.Text = "";
            TextBoxextention.Text = "";
            TextBoxRole.Text = "";

            AlertMessage("Insert success!");

        }
    }
}
