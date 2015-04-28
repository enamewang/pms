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
    public partial class SchedulePlanByPhase : ProjectsInformationUserControlBase
    {
        public int Phase
        {
            get;
            set;
        }

        public string ImageShowHideGridClientID
        {
            get { return this.ImageShowHideGrid.ClientID; }
        }

        public string DivGridClientID
        {
            get { return this.divGrid.ClientID; }
        }

        public string GridViewClientID
        {
            get { return this.GridViewByPhase.ClientID; }
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
            SetClientEvent();
            BindGrid();
        }

        #region SetValueByPhase
        private void SetValueByPhase()
        {
            int phase = this.Phase;
            this.ButtonAddTask.Text = GetPhaseNameByPhase(phase);           
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
                    this.HiddenPhase.Value = phase.ToString();
                    break;
                case 5:
                    phaseName = "开发阶段";
                    this.HiddenPhase.Value = phase.ToString();
                    break;
                case 6:
                    phaseName = "测试阶段";
                    this.HiddenPhase.Value = phase.ToString();
                    break;
                case 7:
                    phaseName = "Release阶段";
                    this.HiddenPhase.Value = phase.ToString();
                    break;
                case 8:
                    phaseName = "Support阶段";
                    this.HiddenPhase.Value = phase.ToString();
                    break;
            }
            return phaseName;
        }
        #endregion

        #region BindGrid
        private void BindGrid()
        {
            SdpDetail sdpDetail = new SdpDetail();
            sdpDetail.Pmsid = this.PmsID;
            sdpDetail.Phase = this.Phase.ToString();

            IList<SdpDetail> sdpDetailList = new SdpDetailBiz().SelectSdpDetail(sdpDetail);

            if (sdpDetailList != null && sdpDetailList.Count > 0)
            {
                GridViewByPhase.DataSource = sdpDetailList;
                GridViewByPhase.DataBind();
            }
        }
        #endregion

        private void SetClientEvent()
        {
            int phase = this.Phase;
            switch (phase)
            {
                case 4:
                    ButtonAddTask.OnClientClick = string.Format("return InsertDesignTableRow('{0}')",GridViewClientID);
                    break;
                case 5:
                   ButtonAddTask.OnClientClick = string.Format("return InsertDevelopmentTableRow('{0}')",GridViewClientID);
                    break;
                case 6:
                   ButtonAddTask.OnClientClick = string.Format("return InsertTestTableRow('{0}')",GridViewClientID);
                    break;
                case 7:
                   ButtonAddTask.OnClientClick = string.Format("return InsertReleaseTableRow('{0}')",GridViewClientID);
                    break;
                case 8:
                    ButtonAddTask.OnClientClick = string.Format("return InsertSupportTableRow('{0}')",GridViewClientID);
                    break;
            }                            
        }

        #region GridViewByPhase_OnRowDataBound
        protected void GridViewByPhase_OnRowDataBound(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               SdpDetail sdpDetail= e.Row.DataItem as SdpDetail;
               if (sdpDetail!=null)
               {
                   e.Row.Attributes.Add("serial", sdpDetail.Serial.ToString());
                   e.Row.Attributes.Add("createuser", sdpDetail.Createuser == null ? "" : sdpDetail.Createuser.ToUpper());
                   e.Row.Attributes.Add("phase",this.Phase.ToString());
                   e.Row.Attributes.Add("auditstatus", sdpDetail.AuditStatus.ToString());
                   e.Row.Attributes.Add("taskstatus", sdpDetail.TaskStatus.ToString());
                   e.Row.Attributes.Add("resource", sdpDetail.Resource == null ? "" : sdpDetail.Resource.ToUpper());
                   e.Row.Attributes.Add("auditor", sdpDetail.Auditor == null ? "" : sdpDetail.Auditor.ToUpper());
                   e.Row.Attributes.Add("auditoragent", sdpDetail.AuditorAgent == null ? "" : sdpDetail.AuditorAgent.ToUpper());
               }               
            }
        }
        #endregion
    }
}