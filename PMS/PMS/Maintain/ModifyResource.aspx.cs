#region -- Using NameSpace --
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Entity;
using Qisda.PMS.Business;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PMS.PMS.AppCode;
using Qisda.PMS.Common;
using Qisda.Web;
using Titan.WebForm;
#endregion

namespace PMS.PMS.Maintain
{
    public partial class ModifyResource : PageBase
    {
        #region  Define Variable        
        public string Serials
        {
            get
            {
                object obj = ViewState["Serials"];
                return (obj == null) ? "-1" : ViewState["Serials"].ToString();
            }
            set
            {
                ViewState["Serials"] = value;
            }
        }
        public string PmsId
        {
            get
            {
                object obj = ViewState["PmsId"];
                return (obj == null) ? "" : ViewState["PmsId"].ToString();
            }
            set
            {
                ViewState["PmsId"] = value;
            }
        }
        #endregion
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        #endregion

        #region InitPage
        private void InitPage()
        {
            PmsId = Request.Params["PmsID"];
            string resource = Request.Params["resource"];
            Serials = Request.Params["serials"];
            //var url = "../Maintain/ModifyResource.aspx?PmsID=" + pmsId + "&serials=" 
            //    + serials + "&resource=" + resource + "&Radom=" + Math.random();
            IList<PmsHead> pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(PmsId);
            PmsHead objPmsHead = new PmsHead();
            if (pmsHead != null && pmsHead.Count > 0)
                objPmsHead = pmsHead[0];
            else
            {
                Msgbox("Data Bind Error!");
                return;
            }
            Hashtable rTable = new Hashtable();
            rTable.Add("PM", objPmsHead.Pm);
            rTable.Add("SD", objPmsHead.Sd);
            rTable.Add("SE", objPmsHead.Se);
            rTable.Add("QA", objPmsHead.Qa);
            this.TextBoxOldResource.Text = resource.Trim();
            BindDropDownListNewResource(rTable);
        }
        #endregion

        #region BindDropDownListNewResource
        private void BindDropDownListNewResource(Hashtable rTable)
        {
            List<string> resourceList = new List<string>();
            try
            {
                foreach (string str in rTable.Values)
                {
                    string[] sArray = str.Split(';');
                    foreach (string name in sArray)
                    {
                        if (!resourceList.Contains(name))
                            resourceList.Add(name);
                    }
                }
                this.DropDownListNewResource.DataSource = resourceList;
                this.DropDownListNewResource.DataBind();
                this.DropDownListNewResource.Items.Insert(0, new ListItem());
                this.DropDownListNewResource.Items[0].Text = "";
                this.DropDownListNewResource.Items[0].Value = "";
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind Resource failure');", true);
                this.DropDownListNewResource.Focus();
            }
        }
        #endregion

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SdpDetail sdpDetail = new SdpDetail();
            SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
            bool result = true;
            sdpDetail.Resource = this.DropDownListNewResource.Text.Trim();
            sdpDetail.Pmsid = PmsId;
            string[] serialArray = Serials.Split(';');
             foreach (string serial in serialArray)
             {
                 sdpDetail.Serial = int.Parse(serial);
                 if (!sdpDetailBiz.UpdateSdpDetailResource(sdpDetail))
                     result = false;
             }
             if (!result) 
             {
                 Msgbox("Update resource failed!");
                 return;
             }
             else
                 ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "SaveSuccess();", true);
        }
    }
}
