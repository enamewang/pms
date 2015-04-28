using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PMS.PMS.AppCode;
using WSC;
using WSC.Common;
using WSC.Framework;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;

namespace PMS.PMS.Maintain
{
    public partial class NotCloseCRComments : PageBase
    {
        NotCloseCRCommentsBiz pagebiz = new NotCloseCRCommentsBiz();

        private string PmsID
        {
            get
            {
                object obj = ViewState["PmsID"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["PmsID"] = value; }
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

        private string paraDate
        {
            get
            {
                object obj = ViewState["ParaDate"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["ParaDate"] = value; }
        }

        private string IsLeader
        {
            get
            {
                object obj = ViewState["IsLeader"];
                return (obj == null) ? "N" : ((string)obj).Trim();
            }
            set { ViewState["IsLeader"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        #region 初始化函数
        private void InitPage()
        {
            //get login name
            LoginName = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            if (LoginName=="")
            {
                LoginName = pagebiz.GetNTUser();
            }
            //get PMS ID
            if (Request["PmsID"] != null)
            {
                PmsID = Request["PmsID"];
            }
            //get Parameter Date
            if (Request["ParaDate"] != null)
            {
                paraDate = Request["ParaDate"];
            }
            else
            {
                paraDate = pagebiz.GetMySQLDate();
            }
            //get IsLeader
            if (pagebiz.IsLeader(LoginName)==true)
            {
                IsLeader = "Y";
            }
            else
            {
                IsLeader = "N";
            }
            //ini CR List
            BindDropDownListCRID();
            if (DropDownListCRID.Items.Count > 0)
            {
                if (DropDownListCRID.Items.FindByValue(PmsID) != null)
                {
                    DropDownListCRID.SelectedValue = PmsID;
                }
                else
                {
                    DropDownListCRID.SelectedIndex = 0;
                }
                string strPMID = DropDownListCRID.SelectedValue;
                ShowCRInfo(strPMID);
            }
            else
            {
                ButtonSave.Enabled = false;
            }

        }
        private void BindDropDownListCRID()
        {
            string paramName = "";
            if (IsLeader != "Y")
            {
                paramName = LoginName;
            }
            IList<VPmsNotClosedcr> strCRList = pagebiz.GetCRIDList(paramName, paraDate);
            if (strCRList != null && strCRList.Count > 0)
            {
                DropDownListCRID.DataSource = strCRList;
                DropDownListCRID.DataTextField = "Crid";
                DropDownListCRID.DataValueField = "Pmsid";
                DropDownListCRID.DataBind();
            }
        }
        #endregion

        protected void DropDownListCRID_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowCRInfo(DropDownListCRID.SelectedValue);
        }
        private void ShowCRInfo(string strPMSID)
        {
            DropDownListCRID.SelectedValue = strPMSID;
            VPmsNotClosedcr crInfo = pagebiz.GetCRInfo(strPMSID);
            if(crInfo != null)
            {
                LabelCRNameD.Text = crInfo.Pmsname;
                LabelTypeD.Text = crInfo.Type;
                LabelSystemD.Text = crInfo.System;
                LabelPMD.Text = crInfo.Pm;
                LabelSDD.Text = crInfo.Sd;
                LabelReleaseDateD.Text = crInfo.Releasedate;
                LabelCRCostD.Text = crInfo.Cost.ToString();
                CRComments crComments = pagebiz.GetCRComments(strPMSID);
                if (crComments != null)
                {
                    TextBoxPMCom.Text = crComments.PMComments;
                    TextBoxSDCom.Text = crComments.SDComments;
                }
                else
                {
                    TextBoxPMCom.Text = "";
                    TextBoxSDCom.Text = "";
                }
                //Enable Comments TextBox by Role
                TextBoxPMCom.Enabled = true;
                TextBoxSDCom.Enabled = true;
                if (IsLeader != "Y")
                {
                    if (pagebiz.IsPMRole(strPMSID, LoginName) != true)
                    {
                        TextBoxPMCom.Enabled = false;
                    }
                    if (pagebiz.IsSDRole(strPMSID, LoginName) != true)
                    {
                        TextBoxSDCom.Enabled = false;
                    }
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            CRComments crComments = new CRComments();
            crComments.PMSID = DropDownListCRID.SelectedValue;
            crComments.CRID = DropDownListCRID.Text;
            if (TextBoxPMCom.Text == "")
            {
                crComments.PMComments = " ";
            }
            else
            {
                crComments.PMComments = TextBoxPMCom.Text.Trim();
            }if (TextBoxSDCom.Text == "")
            {
                crComments.SDComments = " ";
            }
            else
            {
                crComments.SDComments=TextBoxSDCom.Text.Trim();
            }
            if (pagebiz.SaveComments(LoginName, crComments) == true)
            {
                Msgbox("Save successfully !");
                ShowCRInfo(crComments.PMSID);
            }
            else
            {
                Msgbox("Save failed !");
            }
        }
    }
}
