using System;
using System.IO;
using System.Web;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PMS.PMS.AppCode;
using WSC;
using WSC.Common;
using WSC.Framework;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;

using Qisda.Web;
using Qisda.IO;
using Qisda.DateTime;
namespace PMS.PMS.Maintain
{
    public partial class ChangeHistory : PageBase
    {

        #region Define Variable

        private string PmsID
        {
            get
            {
                object obj = ViewState["PmsID"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["PmsID"] = value; }
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["PmsID"] != null)
                {
                    PmsID = Request["PmsID"];
                }
                BindGrid();
            }
        }
        #endregion

        #region InitPage
        private void InitPage()
        {
            BindGrid();
        }
        #endregion

        #region BindGrid
        public void BindGrid()
        {
            try
            {
                IList<PmsHeadH> resultListPmsHeadH = new List<PmsHeadH>();
                string description = "#DateTime# #User# Change #Item# from #ValueFrom# to #ValueTo#";

                IList<PmsHeadH> listPmsHeadH = new PmsHeadHBiz().SelectPmsHeadHByPmsId(PmsID);
                
                foreach (var pmsHeadH in listPmsHeadH)
                {
                    if (pmsHeadH.CrId.Trim() != string.Empty)
                    {
                        resultListPmsHeadH = GetResultListPmsHeadH(description, "CR No", pmsHeadH.CrId, pmsHeadH.CrIdNew, pmsHeadH, resultListPmsHeadH);
                    }
                    if (pmsHeadH.System.Trim() != string.Empty)
                    {
                        resultListPmsHeadH = GetResultListPmsHeadH(description, "Sytem", pmsHeadH.System, pmsHeadH.SystemNew, pmsHeadH, resultListPmsHeadH);
                    }
                    if (pmsHeadH.Type.Trim() != string.Empty)
                    {
                        resultListPmsHeadH = GetResultListPmsHeadH(description, "Type", pmsHeadH.Type, pmsHeadH.TypeNew, pmsHeadH, resultListPmsHeadH);
                    }
                    string dueDate = pmsHeadH.DueDate.ToString("yyyy-MM-dd");
                    string dueDateNew = pmsHeadH.DueDateNew.ToString("yyyy-MM-dd");
                    if (!dueDate.Equals("1900-01-01") && !dueDate.Equals("0001-01-01") && !dueDate.Equals("0000-00-00") && !dueDate.Equals("1899-12-30") && !dueDate.Equals("01-01-01"))
                    {
                        if (!string.IsNullOrEmpty(dueDate))
                        {
                            resultListPmsHeadH = GetResultListPmsHeadH(description, "Due Date", dueDate, dueDateNew, pmsHeadH, resultListPmsHeadH);
                        }
                    }
                    if (pmsHeadH.Pm.Trim() != string.Empty || pmsHeadH.PmNew.Trim() != string.Empty)
                    {
                        resultListPmsHeadH = GetResultListPmsHeadH(description, "PM", pmsHeadH.Pm, pmsHeadH.PmNew, pmsHeadH, resultListPmsHeadH);
                    }
                    if (pmsHeadH.Sd.Trim() != string.Empty || pmsHeadH.SdNew.Trim() != string.Empty)
                    {
                        resultListPmsHeadH = GetResultListPmsHeadH(description, "SD", pmsHeadH.Sd, pmsHeadH.SdNew, pmsHeadH, resultListPmsHeadH);
                    }
                    if (pmsHeadH.Se.Trim() != string.Empty || pmsHeadH.SeNew.Trim() != string.Empty)
                    {
                        resultListPmsHeadH = GetResultListPmsHeadH(description, "SE", pmsHeadH.Se, pmsHeadH.SeNew, pmsHeadH, resultListPmsHeadH);
                    }
                    if (pmsHeadH.Qa.Trim() != string.Empty || pmsHeadH.QaNew.Trim() != string.Empty)
                    {
                        resultListPmsHeadH = GetResultListPmsHeadH(description, "QA", pmsHeadH.Qa, pmsHeadH.QaNew, pmsHeadH, resultListPmsHeadH);
                    }
                }

                GridViewChangeHistory.DataSource = resultListPmsHeadH;
                GridViewChangeHistory.DataBind();
            }
            catch
            {
                Msgbox("BindGrid failure!");
            }
        }

        private IList<PmsHeadH> GetResultListPmsHeadH(string description, string item, string valueFrom, string valueTo, PmsHeadH pmsHeadH, IList<PmsHeadH> resultListPmsHeadH)
        {
            pmsHeadH = GetResultPmsHeadH(description, item, valueFrom, valueTo, pmsHeadH);
            resultListPmsHeadH.Add(pmsHeadH);

            return resultListPmsHeadH;
        }

        private PmsHeadH GetResultPmsHeadH(string description, string item, string valueFrom, string valueTo, PmsHeadH pmsHeadH)
        {
            PmsHeadH resultPmsHeadH = new PmsHeadH();

            description = description.Replace("#DateTime#", QDateTime.FormatLongDate(pmsHeadH.Maintaindate));
            description = description.Replace("#User#", pmsHeadH.Maintainuser);
            description = description.Replace("#Item#", item);
            description = description.Replace("#ValueFrom#", valueFrom);
            description = description.Replace("#ValueTo#", valueTo);

            if (item == "Due Date")
            {
                description = description + " for " + pmsHeadH.ReasonType + " (" + pmsHeadH.Reason + ")";
            }
            resultPmsHeadH.Description = description;

            return resultPmsHeadH;
        }
        #endregion
    }
}
