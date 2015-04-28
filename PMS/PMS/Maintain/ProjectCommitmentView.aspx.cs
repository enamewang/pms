using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using PMS.PMS.AppCode;

namespace PMS.PMS.Maintain
{
    public partial class ProjectCommitmentView : PageBase
    {
        #region View State
        private string Month
        {
            get
            {
                object obj = ViewState["Month"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["Month"] = value; }
        }

        private string Week
        {
            get
            {
                object obj = ViewState["Week"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["Week"] = value; }
        }

        private string UserDept
        {
            get
            {
                object obj = ViewState["UserDept"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["UserDept"] = value; }
        }

        private string Status
        {
            get
            {
                object obj = ViewState["Status"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["Status"] = value; }
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["Month"] != null)
                {
                    Month = Request["Month"].Trim();
                }

                if (Request["Week"] != null)
                {
                    Week = Request["Week"].Trim();
                }

                if (Request["UserDept"] != null)
                {
                    UserDept = Request["UserDept"].Trim();
                }

                if (Request["Status"] != null)
                {
                    Status = Request["Status"].Trim();
                }
                InitPage();
            }
        }
        #endregion

        #region InitPage
        private void InitPage()
        {
            BindGrid();
        }
        #endregion

        private void BindGrid()
        {

            PmsProjectCommitmentHitRateBiz pmsProjectCommitmentHitRateBiz = new PmsProjectCommitmentHitRateBiz();
            PmsProjectCommitmentHitRate pmsProjectCommitmentHitRate = new PmsProjectCommitmentHitRate();
            pmsProjectCommitmentHitRate.Month = Month;
            pmsProjectCommitmentHitRate.Week = Week;
            pmsProjectCommitmentHitRate.UserDept = UserDept;
            pmsProjectCommitmentHitRate.Status = Status;

            GridViewRequirement.EmptyDataText = "No Data.";
            IList<PmsProjectCommitmentHitRate> pmsProjectCommitmentHitRateList = pmsProjectCommitmentHitRateBiz.GetPmsProjectCommitmentHitRateList(pmsProjectCommitmentHitRate);
            if (pmsProjectCommitmentHitRateList != null)
            {
                foreach (var projectCommitmentHitRate in pmsProjectCommitmentHitRateList)
                {
                    if (projectCommitmentHitRate.Status == "OnSchedule")
                    {
                        projectCommitmentHitRate.Status = "On-Schedule";
                    }
                }
            }
            GridViewRequirement.DataSource = pmsProjectCommitmentHitRateList;
            GridViewRequirement.DataBind();

        }
    }
}
