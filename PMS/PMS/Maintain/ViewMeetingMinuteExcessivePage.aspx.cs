using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS
{
	public partial class ViewMeetingMinuteExcessivePage : System.Web.UI.Page
	{
        protected string PmsID
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

        protected string MinID
        {
            get
            {
                object obj = ViewState["MinID"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["MinID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["PmsID"] != null)
                {
                    PmsID = Request["PmsID"];
                }

                if (Request["CrID"] != null)
                {
                    CrID = Request["CrID"];
                }

                if (Request["MinID"] != null)
                {
                    MinID = Request["MinID"];
                }

            }
            string url = "ViewMeetingMinute.aspx?PmsID=" + PmsID + "&CrID=" + CrID + "&MinID=" + MinID;
            IFrameCreatPage.Attributes.Add("src", url);
        }
	}
}
