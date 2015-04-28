using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;


namespace PMS.PMS.Maintain
{
    public partial class ViewTask : System.Web.UI.Page
    {
        #region Define Variable
        public string LoginName
        {
            get
            {
                return WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string PmsId = Request.QueryString["PmsID"];
            string CrID = Request.QueryString["CrId"];
            string Phase = Request.QueryString["Phase"];
            int Serial = 0;
            int.TryParse(Request.QueryString["Serial"], out Serial);
            
            if (PmsId == null || CrID == null || Phase == null || Serial == 0)
            {
                return;
            }        
            IList<PmsHead> pmsHeadList = new PmsHeadBiz().SelectPmsHead(PmsId, "");
            PmsHead pmsHead;
            if (pmsHeadList != null && pmsHeadList.Count > 0)
                pmsHead = pmsHeadList[0];
            else
                return;

            SdpDetail sdpDetailParms = new SdpDetail();
            sdpDetailParms.Serial = Serial;
            sdpDetailParms.Pmsid = PmsId;
            IList<SdpDetail> sdpDetailList = new SdpDetailBiz().SelectSdpDetail(sdpDetailParms);
            SdpDetail sdpDetailResult = (sdpDetailList == null || sdpDetailList.Count == 0) ? new SdpDetail() : sdpDetailList[0];

            PmsCommonEnum.PlanPhase planPhase = (PmsCommonEnum.PlanPhase)System.Enum.Parse(typeof(PmsCommonEnum.PlanPhase), Phase);
            string phaseDescription = planPhase.GetDescription();
            string formatPlanstartday = new PmsCommonBiz().FormatDateTime(sdpDetailResult.Planstartday.ToString("yyyy-MM-dd").Trim());
            string formatPlanendday = new PmsCommonBiz().FormatDateTime(sdpDetailResult.Planendday.ToString("yyyy-MM-dd").Trim());            
            string resultHtml = "<table class='ViewTaskTable'>"
                                + "<tr><td><span class='ViewTaskSpan'>CR No</span></td><td colspan='3'><input type='text' class='ViewTaskOnlyTextBox' value='" + pmsHead.CrId + "' /></td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>CR Name</span></td><td colspan='3'><input type='text' class='ViewTaskOnlyTextBox' value='" + pmsHead.PmsName + "'style='width: 325px;'/></td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>任务阶段</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + phaseDescription + "'/></td>"
                                + "<td><span class='ViewTaskSpan'>审核状态</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.AuditStatusDesc + "' /></td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>任务状态</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.TaskStatusDesc + "' /></td><td>&nbsp;</td><td>&nbsp;</td></tr>" + "<tr><td><span class='ViewTaskSpan'>任务名称</span></td>"
                                + "<td colspan='3'><div style=' border-bottom: 1px solid #CCCCCC;width: 325px;  white-space: normal;text-overflow: ellipsis; text-align: Left'>" + sdpDetailResult.TaskName + "</div></td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>作业方式</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.OperationTypeDesc + "'/></td>"
                                + "<td><span class='ViewTaskSpan'>任务类型</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.TaskTypeDesc + "'/></td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>功能分类</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.FunctionTypeDesc + "'/></td>"
                                + "<td><span class='ViewTaskSpan'>语言</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.ProgramLanguageDesc + "'/></td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>复杂度</span></td>"
                                + "<td colspan='3'><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.TaskComplexityDesc + "'/></td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>参考工时</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.Actualcost + "' /></td>"
                                + "<td><span class='ViewTaskSpan'>计划工时</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.Plancost + "' /></td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>计划开始</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + formatPlanstartday + "' /></td>"
                                + "<td><span class='ViewTaskSpan'>计划结束</span></td><td><input type='text' class='ViewTaskOnlyTextBox' value='" + formatPlanendday + "' /> </td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>指派给</span></td>"
                                + "<td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.Resource + "'/></td><td><span class='ViewTaskSpan'>资源角色</span></td>"
                                + "<td><input type='text' class='ViewTaskOnlyTextBox' value='" + sdpDetailResult.Role + "'/></td></tr>"
                                + "<tr><td><span class='ViewTaskSpan'>说明</span></td>"
                                + "<td colspan='3'><div style='border-bottom: 1px solid #CCCCCC; width: 325px;  white-space: normal;text-overflow: ellipsis; text-align: Left'>" + sdpDetailResult.ScheduleRemark + "</div></td></tr>";

            Response.Write(resultHtml);

        }
    }
}
