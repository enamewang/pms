using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;

namespace PMS.PMS.WebService
{
    /// <summary>
    /// SchedulePlanByPhase 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class SchedulePlanByPhase : System.Web.Services.WebService
    {
        public string LoginName
        {
            get
            {
                return WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            }
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool DeleteSdpDetail(string serial)
        {
            int result = new SdpDetailBiz().DeleteSdpDetail(serial);

            if (result <= 0)
            {
                return false;
            }
            return true;
        }

        [WebMethod]
        public bool DeleteSdpDetailBySerials(string serials)
        {
            string[] serialArray = serials.Split(';');
            int result;
            foreach (string serial in serialArray)
            {
                result = new SdpDetailBiz().DeleteSdpDetail(serials);
                if (result <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        [WebMethod]
        public bool UpdateSdpDetailAuditStatus(string serials, int auditStatus, string auditComment)
        {
            bool result = false;
            string[] serialArray = serials.Split(';');
            string serialAll = serialArray.Aggregate(string.Empty, (current, next) => current + "'" + next + "',").TrimEnd(',');
            SdpDetail sdpDetail = new SdpDetail();
            PmsSdpAudit pmsSdpAudit = new PmsSdpAudit();
            sdpDetail.Serials = serialAll;
            sdpDetail.AuditStatus = auditStatus;
            if (new SdpDetailBiz().UpdateSdpDetailAuditStatus(sdpDetail))
            {
                //同意或拒绝会新增版本
                if ((auditStatus == (int)PmsCommonEnum.AuditStatus.HasApproved) || auditStatus == (int)PmsCommonEnum.AuditStatus.BeenRejected)
                {
                    PmsSdpVersion pmsSdpVersion = new PmsSdpVersion();
                    foreach (var serial in serialArray)
                    {
                        IList<PmsSdpVersion> PmsSdpVersionList = new PmsSdpVersionBiz().SelectPmsSdpVersionByTaskno(serial);
                        IList<SdpDetail> listSdpDetail = new SdpDetailBiz().SelectSdpDetail(new SdpDetail { Serial = int.Parse(serial) });
                        if (listSdpDetail == null || listSdpDetail.Count == 0)
                        {
                            return result;
                        }
                        sdpDetail = listSdpDetail.FirstOrDefault();
                        pmsSdpVersion.Taskno = int.Parse(serial);
                        pmsSdpVersion.Pmsid = sdpDetail.Pmsid;
                        pmsSdpVersion.PlanStartDay = sdpDetail.Planstartday;
                        pmsSdpVersion.PlanEndDay = sdpDetail.Planendday;
                        pmsSdpVersion.PlanCost = (float)sdpDetail.Plancost;
                        pmsSdpVersion.RefCost = (float)sdpDetail.Refcost;
                        pmsSdpVersion.Creator = LoginName;
                        pmsSdpVersion.CreateDate = DateTime.Now;
                        pmsSdpVersion.Version = (PmsSdpVersionList == null || PmsSdpVersionList.Count == 0) ? 1.0f : PmsSdpVersionList.Max(t => t.Version) + 0.1f;
                        new PmsSdpVersionBiz().InsertPmsSdpVersion(pmsSdpVersion);
                    }
                    IList<PmsSdpAudit> pmsSdpAuditList = new PmsSdpAuditBiz().SelectPmsSdpAuditByPmsId(sdpDetail.Pmsid);
                    pmsSdpAudit.Pmsid = sdpDetail.Pmsid;
                    pmsSdpAudit.SdpVersion = (pmsSdpAuditList == null || pmsSdpAuditList.Count == 0) ? 1.0f : pmsSdpAuditList.Max(t => t.SdpVersion) + 0.1f;
                    pmsSdpAudit.Auditor = LoginName;
                    if (auditComment == "")
                        pmsSdpAudit.AuditResult = "Approve";
                    else
                        pmsSdpAudit.AuditResult = "Reject";
                    pmsSdpAudit.AuditComment = auditComment;
                    pmsSdpAudit.CreateDate = DateTime.Now;
                    pmsSdpAudit.Creator = LoginName;
                    new PmsSdpAuditBiz().InsertPmsSdpAudit(pmsSdpAudit);
                }

                //发mail通知相关人员(提交人或者审核人)
                result = new MailBiz().TaskMail(serialArray[0], auditStatus);

            }
            return result;
        }

    }
}
