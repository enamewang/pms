using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS.PMS.Maintain
{
    public partial class RedirectPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["Action"] != null && Request["PMSID"] != null)
                {
                    string strAction = Request["Action"].ToString().Trim();
                    string PMSId = Request["PMSID"].ToString().Trim();
                    //if (!string.IsNullOrEmpty(strAction) && strAction.ToUpper() == "SDPEDIT")
                    if (!string.IsNullOrEmpty(strAction))
                    {
                        Session["EditPMSID"] = PMSId;
                        Session["ViewAction"] = strAction;
                        string url = System.Configuration.ConfigurationSettings.AppSettings["PMSUrl"].ToString();
                        Response.Redirect(url);
                    }

                }

            }
        }
    }
}
