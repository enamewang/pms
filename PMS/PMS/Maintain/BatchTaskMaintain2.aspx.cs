using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Business;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PMS.PMS.AppCode;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;
using Qisda.Web;
using Titan.WebForm;

namespace PMS.PMS.Maintain
{
    public partial class BatchTaskMaintain2 : PageBase
    {
        #region  Define Variable
        public PmsHead objPmsHead;
        public PmsHead ObjPmsHead
        {
            get
            {
                return (objPmsHead == null) ? new PmsHead() : objPmsHead;
            }
            set
            {
                objPmsHead = value;
            }
        }
        public string Service
        {
            get
            {
                return PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription();
            }
        }
        public string Support
        {
            get
            {
                return ((int)PmsCommonEnum.EnumSdpPhase.Support).ToString();
            }
        }
        public bool Stop;
        public string pmsId;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        #endregion

        #region InitPage
        private void InitPage()
        {
             pmsId = Request.Params["PmsID"];
            string viewData = Request.Params["ViewData"];

            // Abel Test
            if (pmsId == null)
            {
                Msgbox("Data Bind Error!");
                Stop = false;
                return;
            }            
            //pmsId = "PMS201303010003";


            IList<PmsHead> pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(pmsId);
            if (pmsHead == null || pmsHead.Count == 0) 
            {
                Msgbox("Data Bind Error!");
                Stop = false;
                return;
            }
            //获取pmsHead dudate等信息
            ObjPmsHead = pmsHead[0];
            Stop = true;
            //BindDropDownListPhase();
        }
        #endregion

        //#region  BindDropDownListPhase
        //private void BindDropDownListPhase()
        //{
        //    try
        //    {
        //        Dictionary<string, string> phases = new PmsCommonEnum().GetEnumValueAndDesc(typeof(PmsCommonEnum.PlanPhase));
        //        this.DropDownListPhase.DataSource = phases;
        //        this.DropDownListPhase.DataTextField = "Key";
        //        this.DropDownListPhase.DataValueField = "Value";
        //        this.DropDownListPhase.DataBind();

        //        this.DropDownListPhase.Items.Insert(0, new ListItem());
        //        this.DropDownListPhase.Items[0].Text = "";
        //        this.DropDownListPhase.Items[0].Value = "";
        //        this.DropDownListPhase.SelectedIndex = 0;
        //    }
        //    catch
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind TaskType failure');", true);
        //        this.DropDownListPhase.Focus();
        //    }
        //}
        //#endregion

    }
}
