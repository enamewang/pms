#region -- Using NameSpace --
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Qisda.PMS.Entity;
using Qisda.PMS.Business;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PMS.PMS.AppCode;
using Qisda.PMS.Common;
using Qisda.Web;
using Titan.WebForm;
#endregion

namespace PMS.PMS.Maintain
{
    public partial class AuditComment : PageBase
    {
        #region  Define Variable
        public string LoginName
        {
            get
            {
                return WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            }
        }
        public string Serials
        {
            get
            {
                object obj = ViewState["Serials"];
                return (obj == null) ? "-1" : ViewState["Serials"].ToString();
            }
            set
            {
                ViewState["Serials"] = value;
            }
        }
        public string PmsId
        {
            get
            {
                object obj = ViewState["PmsId"];
                return (obj == null) ? "" : ViewState["PmsId"].ToString();
            }
            set
            {
                ViewState["PmsId"] = value;
            }
        }
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
        protected void InitPage()
        {
            PmsId = Request.Params["PmsID"];
            Serials = Request.Params["serials"];
        }
        #endregion

        //protected void ButtonSave_Click(object sender, EventArgs e)
        //{
        //    bool result = false;
        //    int auditStatus = 4;
        //    string[] serialArray = Serials.Split(';');
        //    string serialAll = serialArray.Aggregate(string.Empty, (current, next) => current + "'" + next + "',").TrimEnd(',');
        //    SdpDetail sdpDetail = new SdpDetail();
        //    PmsSdpAudit pmsSdpAudit = new PmsSdpAudit();
        //    sdpDetail.Serials = serialAll;
        //    sdpDetail.AuditStatus = auditStatus;
        //    if (new SdpDetailBiz().UpdateSdpDetailAuditStatus(sdpDetail))
        //    {
        //        //同意或拒绝会新增版本
        //        if ((auditStatus == (int)PmsCommonEnum.AuditStatus.HasApproved) || auditStatus == (int)PmsCommonEnum.AuditStatus.BeenRejected)
        //        {
        //            PmsSdpVersion pmsSdpVersion = new PmsSdpVersion();
        //            foreach (var serial in serialArray)
        //            {
        //                IList<PmsSdpVersion> PmsSdpVersionList = new PmsSdpVersionBiz().SelectPmsSdpVersionByTaskno(serial);
        //                IList<SdpDetail> listSdpDetail = new SdpDetailBiz().SelectSdpDetail(new SdpDetail { Serial = int.Parse(serial) });
        //                if (listSdpDetail == null || listSdpDetail.Count == 0)
        //                {
        //                    break;
        //                }
        //                sdpDetail = listSdpDetail.FirstOrDefault();
        //                pmsSdpVersion.Taskno = int.Parse(serial);
        //                pmsSdpVersion.Pmsid = sdpDetail.Pmsid;
        //                pmsSdpVersion.PlanStartDay = sdpDetail.Planstartday;
        //                pmsSdpVersion.PlanEndDay = sdpDetail.Planendday;
        //                pmsSdpVersion.PlanCost = (float)sdpDetail.Plancost;
        //                pmsSdpVersion.RefCost = (float)sdpDetail.Refcost;
        //                pmsSdpVersion.Creator = LoginName;
        //                pmsSdpVersion.CreateDate = DateTime.Now;
        //                pmsSdpVersion.Version = (PmsSdpVersionList == null || PmsSdpVersionList.Count == 0) ? 1.0f : PmsSdpVersionList.Max(t => t.Version) + 0.1f;
        //                new PmsSdpVersionBiz().InsertPmsSdpVersion(pmsSdpVersion);
                       
        //            }
        //            IList<PmsSdpAudit> pmsSdpAuditList = new PmsSdpAuditBiz().SelectPmsSdpAuditByPmsId(sdpDetail.Pmsid);
        //            pmsSdpAudit.Pmsid = sdpDetail.Pmsid;
        //            pmsSdpAudit.SdpVersion = (pmsSdpAuditList == null || pmsSdpAuditList.Count == 0) ? 1.0f : pmsSdpAuditList.Max(t => t.SdpVersion) + 0.1f;
        //            pmsSdpAudit.Auditor = LoginName;
        //            pmsSdpAudit.AuditResult = "Reject";
        //            pmsSdpAudit.AuditComment = this.TextBoxAuditComment.Text.Trim();
        //            pmsSdpAudit.CreateDate = DateTime.Now;
        //            pmsSdpAudit.Creator = LoginName;
        //            new PmsSdpAuditBiz().InsertPmsSdpAudit(pmsSdpAudit);
        //        }

        //        //发mail通知相关人员(提交人或者审核人)
        //        Msgbox("发mail前");
        //        result = new MailBiz().TaskMail(serialArray[0], auditStatus);
        //        Msgbox("发mail后"+result);
        //    }
        //    if (result)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "SaveSuccess();", true);
        //    }
        //    else
        //    {
        //        Msgbox("Save failed!");
        //        return;
        //    }


        //}
    }
}
