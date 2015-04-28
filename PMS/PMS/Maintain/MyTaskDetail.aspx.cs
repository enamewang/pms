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
using Qisda.PMS.Entity;
using Titan.WebForm;

using WSC;
using WSC.Common;
using WSC.Framework;
using Qisda.PMS.Business;
using System.Collections.Generic;



namespace PMS.PMS.Maintain
{
    public partial class MyTaskDetail : WSC.FramePage
    {
        private MyTaskDetailBiz myTaskDetailBiz=new MyTaskDetailBiz();
        private PmsCommonBiz pmsCommonBiz = new PmsCommonBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Serial"] != null)
                {
                    InitData(Request.QueryString["Serial"].ToString());
                    this.TextBoxActualCost.Focus();
                }
            }
        }


        #region Bind Data

        protected void InitData(string detailSerial)
        {
            Qisda.PMS.Entity.SdpDetail detail = new SdpDetail();
            detail.Serial = Convert.ToInt32(detailSerial);
            detail = myTaskDetailBiz.GetDetailInfoBySerial(detail);
            if (detail != null)
            {
                this.TextBoxActualCost.Text = detail.Actualcost.ToString();
                this.TextBoxComplete.Text = (detail.Completedpercent == null) ? string.Empty : detail.Completedpercent.ToString();
                //add by Abel.Li 2014-01-06
                this.TextBoxTaskStatus.Text = (detail.TaskStatusDesc == null) ? string.Empty : detail.TaskStatusDesc;                
                //this.TextBoxForeTask.Text = (detail.PretaskNo == null) ? string.Empty : detail.PretaskNo.ToString();
                this.TextBoxPlanCost.Text = (detail.Plancost == null) ? string.Empty : detail.Plancost.ToString();
                this.TextBoxRemark.Text = (detail.Remark == null) ? string.Empty : detail.Remark.Trim();
                this.TextBoxTaskName.Text = (detail.TaskName == null) ? string.Empty : detail.TaskName.Trim();
                this.TextBoxPlanEnd.Text = pmsCommonBiz.ConvertDateToString(detail.Planendday);
                this.TextBoxPlanStart.Text = pmsCommonBiz.ConvertDateToString(detail.Planstartday);
                this.DateTextBoxActualEnd.Text = pmsCommonBiz.ConvertDateToString(detail.Actualendday);
                this.DateTextBoxActualStart.Text = pmsCommonBiz.ConvertDateToString(detail.Actualstartday);
                this.TextBoxRole.Text = (detail.Role == null) ? string.Empty : detail.Role.Trim();
                this.TextBoxResource.Text = (detail.Resource == null) ? string.Empty : detail.Resource.Trim();
            }   

           PmsHead head = new PmsHeadBiz().SelectPmsHeadByPmsId(detail.Pmsid)[0];
        
            if (head != null)
            {
                this.TextBoxProjectName.Text = (head.PmsName == null) ? string.Empty : head.PmsName.Trim();
                this.TextBoxSysName.Text = (head.System == null) ? string.Empty : head.System.Trim();
            }
        }
        #endregion


        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Serial"] != null)
            {

                Qisda.PMS.Entity.SdpDetail detail = new SdpDetail();
                detail.Serial = Convert.ToInt32(Request.QueryString["Serial"].ToString().Trim());
                detail = myTaskDetailBiz.GetDetailInfoBySerial(detail);
                detail.Actualstartday = pmsCommonBiz.ConvertDateTime(this.DateTextBoxActualStart.Text);
                detail.Actualendday = pmsCommonBiz.ConvertDateTime(this.DateTextBoxActualEnd.Text);
                detail.Actualcost = pmsCommonBiz.ConvertStringToFloat(this.TextBoxActualCost.Text);
                detail.Completedpercent = pmsCommonBiz.ConvertStringToFloat(this.TextBoxComplete.Text);
                detail.PretaskNo = pmsCommonBiz.ConvertStringToInt(this.TextBoxForeTask.Text);
                detail.Remark = this.TextBoxRemark.Text.Trim();
                detail.Maintaindate = DateTime.Now;
                detail.Maintainuser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                if (myTaskDetailBiz.UpdatePmsSapDesignDetailInfo(detail))
                {
                    ScriptManager.RegisterStartupScript(updateActualCost, this.GetType(),
                                        "saveScript", "alert('Save Successful!');", true);

                    //ScriptManager.RegisterStartupScript(updateActualCost, this.GetType(),
                    //                    "updateScript", "window.close();", true);

                    //ScriptManager.RegisterStartupScript(this.updateActualCost, this.GetType(), "deleteScript", "window.close();", true);

                    Response.Write("<script>window.close();</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(updateActualCost, this.GetType(),
                                       "saveScript", "alert('Save Failed,Please try later!');", true);
                }
            }


        }

        protected void TextBoxActualCost_TextChanged(object sender, EventArgs e)
        {
            if (!pmsCommonBiz.CheckControlEmpty(this.TextBoxActualCost.Text) && !pmsCommonBiz.CheckControlEmpty(this.TextBoxPlanCost.Text))
            {
                if (pmsCommonBiz.ConvertDecimal(this.TextBoxPlanCost.Text) < pmsCommonBiz.ConvertDecimal(this.TextBoxActualCost.Text))
                {
                    this.TextBoxComplete.Text = string.Empty;
                }
                else if (pmsCommonBiz.ConvertDecimal(this.TextBoxPlanCost.Text) > 0)
                {
                    this.TextBoxComplete.Text = string.Format("{0:0.0}", pmsCommonBiz.ConvertDecimal(TextBoxActualCost.Text) * 100 / pmsCommonBiz.ConvertDecimal(TextBoxPlanCost.Text));
                }
                this.updateActualCost.Update();
            }
            else
            {
                this.TextBoxComplete.Text = string.Empty;
                this.updateActualCost.Update();
            }

            //this.DateTextBoxActualStart.Focus();
        }


    }
}
