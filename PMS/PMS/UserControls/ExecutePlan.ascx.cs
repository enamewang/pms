using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Business;

namespace PMS.PMS.UserControls
{
    public partial class ExecutePlan : ProjectsInformationUserControlBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ExecutePlanByPhaseDesign.Phase = (int)PmsCommonEnum.EnumSdpPhase.Design;
                this.ExecutePlanByPhaseDevelopment.Phase = (int)PmsCommonEnum.EnumSdpPhase.Development;
                this.ExecutePlanByPhaseTest.Phase = (int)PmsCommonEnum.EnumSdpPhase.Test;
                this.ExecutePlanByPhaseRelease.Phase = (int)PmsCommonEnum.EnumSdpPhase.Release;
                this.ExecutePlanByPhaseSupport.Phase = (int)PmsCommonEnum.EnumSdpPhase.Support;

                SetImageShowHideGrid(ExecutePlanByPhaseDesign);
                SetImageShowHideGrid(ExecutePlanByPhaseDevelopment);
                SetImageShowHideGrid(ExecutePlanByPhaseTest);
                SetImageShowHideGrid(ExecutePlanByPhaseRelease);
                SetImageShowHideGrid(ExecutePlanByPhaseSupport);

                SetButtonAll();
                SetImageShowHideAllGrid();

            }
        }

        private void SetButtonAll() 
        {
            //this.ButtonShowTaskAll.Attributes.Add("onclick",
            //   string.Format("return ShowTaskAll('{0}','{1}','{2}','{3}','{4}')",ExecutePlanByPhaseDesign.GridViewClientID,
            //   ExecutePlanByPhaseDevelopment.GridViewClientID, ExecutePlanByPhaseTest.GridViewClientID,
            //   ExecutePlanByPhaseRelease.GridViewClientID, ExecutePlanByPhaseSupport.GridViewClientID));

            //this.ButtonShowTaskAssignToMe.Attributes.Add("onclick",
            //   string.Format("return ShowTaskAssignToMe('{0}','{1}','{2}','{3}','{4}')", ExecutePlanByPhaseDesign.GridViewClientID,
            //   ExecutePlanByPhaseDevelopment.GridViewClientID, ExecutePlanByPhaseTest.GridViewClientID,
            //   ExecutePlanByPhaseRelease.GridViewClientID, ExecutePlanByPhaseSupport.GridViewClientID));
        }

        private void SetImageShowHideAllGrid()
        {
            //this.ImageShowHideAllGrid.Attributes.Add("onclick",
            //    string.Format("ShowHideExecutePlanAllGrid('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
            //    ImageShowHideAllGrid.ClientID, ExecutePlanByPhaseDesign.DivGridClientID, ExecutePlanByPhaseDesign.ImageShowHideGridClientID,
            //    ExecutePlanByPhaseDevelopment.DivGridClientID, ExecutePlanByPhaseDevelopment.ImageShowHideGridClientID,
            //    ExecutePlanByPhaseTest.DivGridClientID, ExecutePlanByPhaseTest.ImageShowHideGridClientID,
            //    ExecutePlanByPhaseRelease.DivGridClientID, ExecutePlanByPhaseRelease.ImageShowHideGridClientID,
            //    ExecutePlanByPhaseSupport.DivGridClientID, ExecutePlanByPhaseSupport.ImageShowHideGridClientID));
        }

        private void SetImageShowHideGrid(ExecutePlanByPhase executePlanByPhase)
        {
            Image image = executePlanByPhase.FindControl("ImageShowHideGrid") as Image;
            if (image != null)
            {
                //image.Attributes.Add("onclick", string.Format("ShowHideGrid('{0}','{1}')", executePlanByPhase.DivGridClientID, executePlanByPhase.ImageShowHideGridClientID));
            }
        }
    }
}