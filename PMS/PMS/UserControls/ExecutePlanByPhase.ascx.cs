using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Entity;
using Qisda.PMS.Business;

namespace PMS.PMS.UserControls
{
    public partial class ExecutePlanByPhase : ProjectsInformationUserControlBase
    {

        public int Phase
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            SetValueByPhase();
            BindGrid();
        }

        #region SetValueByPhase
        private void SetValueByPhase()
        {
            int phase = this.Phase;
            this.LabelPhaseName.Text = GetPhaseNameByPhase(phase);
        }
        #endregion

        #region GetPhaseNameByPhase
        private string GetPhaseNameByPhase(int phase)
        {
            string phaseName = "";
            switch (phase)
            {
                case 4:
                    phaseName = "设计阶段";
                    break;
                case 5:
                    phaseName = "开发阶段";
                    break;
                case 6:
                    phaseName = "测试阶段";
                    break;
                case 7:
                    phaseName = "Release阶段";
                    break;
                case 8:
                    phaseName = "Support阶段";
                    break;
            }
            return phaseName;
        }
        #endregion

        #region BindGrid
        private void BindGrid()
        {
            SdpDetail sdpDetail = new SdpDetail();
            sdpDetail.Pmsid = Request.QueryString["PmsId"];
            sdpDetail.Phase = this.Phase.ToString();
            sdpDetail.AuditStatus = (int)PmsCommonEnum.AuditStatus.HasApproved;

            IList<SdpDetail> sdpDetailList = new SdpDetailBiz().SelectSdpDetail(sdpDetail);

            if (sdpDetailList != null && sdpDetailList.Count > 0)
            {
                GridViewByPhase.DataSource = sdpDetailList;
                GridViewByPhase.DataBind();
            }
        }
        #endregion

        protected void GridViewByPhase_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SdpDetail sdpDetail = e.Row.DataItem as SdpDetail;
                if (sdpDetail != null)
                {
                    e.Row.Attributes.Add("serial", sdpDetail.Serial.ToString());
                    e.Row.Attributes.Add("createuser", sdpDetail.Createuser == null ? "" : sdpDetail.Createuser.ToUpper().ToString());
                    e.Row.Attributes.Add("phase", this.Phase.ToString());
                    e.Row.Attributes.Add("taskname", sdpDetail.TaskName.ToString());
                    e.Row.Attributes.Add("tasktypedesc", sdpDetail.TaskTypeDesc.ToUpper().ToString());
                    e.Row.Attributes.Add("taskstatus", sdpDetail.TaskStatus.ToString());
                    e.Row.Attributes.Add("resource", sdpDetail.Resource == null ? "" : sdpDetail.Resource.ToUpper().ToString());
                    e.Row.Attributes.Add("auditor", sdpDetail.Auditor == null ? "" : sdpDetail.Auditor.ToUpper().ToString());
                    e.Row.Attributes.Add("auditoragent", sdpDetail.AuditorAgent == null ? "" : sdpDetail.AuditorAgent.ToUpper().ToString());
                    e.Row.Attributes.Add("completepercent", sdpDetail.Completedpercent.ToString());
                    e.Row.Attributes.Add("plancost", sdpDetail.Plancost.ToString());
                    e.Row.Attributes.Add("actualcost", sdpDetail.Actualcost.ToString());
                }
            }
        }
    }
}