using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Business;
using Qisda.PMS.Common;

namespace PMS.PMS.UserControls
{
    public partial class SchedulePlan : ProjectsInformationUserControlBase
    {
        protected string IsRDLeader { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IsRDLeader = new BaseDataUserBiz().SelectBaseDataUser(this.LoginName, PmsCommonEnum.OrgRole.RD_LEADER.GetDescription()).Count == 0 ? "N" : "Y";
                this.SchedulePlanByPhaseDesign.Phase = (int)PmsCommonEnum.EnumSdpPhase.Design;
                this.SchedulePlanByPhaseDevelopment.Phase = (int)PmsCommonEnum.EnumSdpPhase.Development;
                this.SchedulePlanByPhaseTest.Phase = (int)PmsCommonEnum.EnumSdpPhase.Test;
                this.SchedulePlanByPhaseRelease.Phase = (int)PmsCommonEnum.EnumSdpPhase.Release;
                this.SchedulePlanByPhaseSupport.Phase = (int)PmsCommonEnum.EnumSdpPhase.Support;

                SetImageShowHideGrid(SchedulePlanByPhaseDesign);
                SetImageShowHideGrid(SchedulePlanByPhaseDevelopment);
                SetImageShowHideGrid(SchedulePlanByPhaseTest);
                SetImageShowHideGrid(SchedulePlanByPhaseRelease);
                SetImageShowHideGrid(SchedulePlanByPhaseSupport);

                SetButtonAll();
                SetImageShowHideAllGrid();
            }
        }

        private void SetButtonAll()
        {
            //this.ButtonShowTaskAll.Attributes.Add("onclick",
            //   string.Format("return ShowTaskAll('{0}','{1}','{2}','{3}','{4}')", SchedulePlanByPhaseDesign.GridViewClientID,
            //   SchedulePlanByPhaseDevelopment.GridViewClientID, SchedulePlanByPhaseTest.GridViewClientID,
            //   SchedulePlanByPhaseRelease.GridViewClientID, SchedulePlanByPhaseSupport.GridViewClientID));

            //this.ButtonShowTaskAssignToMe.Attributes.Add("onclick",
            //   string.Format("return ShowTaskAssignToMe('{0}','{1}','{2}','{3}','{4}')", SchedulePlanByPhaseDesign.GridViewClientID,
            //   SchedulePlanByPhaseDevelopment.GridViewClientID, SchedulePlanByPhaseTest.GridViewClientID,
            //   SchedulePlanByPhaseRelease.GridViewClientID, SchedulePlanByPhaseSupport.GridViewClientID));

            //this.DropDownListCondition.Attributes.Add("onchange",
            //   string.Format("DropDownListConditionOnChange('{0}','{1}','{2}','{3}','{4}','{5}')", DropDownListCondition.ClientID, SchedulePlanByPhaseDesign.GridViewClientID,
            //   SchedulePlanByPhaseDevelopment.GridViewClientID, SchedulePlanByPhaseTest.GridViewClientID,
            //   SchedulePlanByPhaseRelease.GridViewClientID, SchedulePlanByPhaseSupport.GridViewClientID));
        }

        private void SetImageShowHideAllGrid()
        {
            this.ImageShowHideAllGrid.Attributes.Add("onclick",
                string.Format("ShowHideAllGrid('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                ImageShowHideAllGrid.ClientID, SchedulePlanByPhaseDesign.DivGridClientID, SchedulePlanByPhaseDesign.ImageShowHideGridClientID,
                SchedulePlanByPhaseDevelopment.DivGridClientID, SchedulePlanByPhaseDevelopment.ImageShowHideGridClientID,
                SchedulePlanByPhaseTest.DivGridClientID, SchedulePlanByPhaseTest.ImageShowHideGridClientID,
                SchedulePlanByPhaseRelease.DivGridClientID, SchedulePlanByPhaseRelease.ImageShowHideGridClientID,
                SchedulePlanByPhaseSupport.DivGridClientID, SchedulePlanByPhaseSupport.ImageShowHideGridClientID));
        }


        private void SetImageShowHideGrid(SchedulePlanByPhase SchedulePlanByPhase)
        {
            Image image = SchedulePlanByPhase.FindControl("ImageShowHideGrid") as Image;
            if (image != null)
            {
                image.Attributes.Add("onclick", string.Format("ShowHideGrid('{0}','{1}')", SchedulePlanByPhase.DivGridClientID, SchedulePlanByPhase.ImageShowHideGridClientID));
            }
        }
    }
}