using System;
using System.Collections;
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

namespace PMS.PMS.Maintain
{
    public partial class AttachFileServiceIframe : PageBase
    {

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["PmsID"] != null)
                {
                    PmsID = Request["PmsID"];
                }
                else
                {
                    Msgbox("PmsID is empty");
                }

                if (Request["CrID"] != null)
                {
                    CrID = Request["CrID"];
                }
                else
                {
                    Msgbox("CrID is empty");
                }
            }
            this.HiddenField1.Value = PmsID;
            this.HiddenField2.Value = CrID;
        }
    }
}
