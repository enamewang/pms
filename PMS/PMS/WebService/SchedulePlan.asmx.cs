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
    /// SchedulePlan 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class SchedulePlan : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool UpdateSdpDetailAuditStatus(string serials,int auditStatus)
        {
            SdpDetail sdpDetail = new SdpDetail();
            sdpDetail.Serials = "";
            sdpDetail.AuditStatus = auditStatus;
            return new SdpDetailBiz().UpdateSdpDetailAuditStatus(sdpDetail);
        }
        [WebMethod]
        public bool DeleteSdpDetailBySerials(string serials)
        {
            string[] serialArray = serials.Split(';');
            int result;
            foreach (string serial in serialArray)
            {
               result  = new SdpDetailBiz().DeleteSdpDetail(serials);
                if (result <= 0)
                {
                    return false;
                }
            }            
            return true;
        }        
    }
}
