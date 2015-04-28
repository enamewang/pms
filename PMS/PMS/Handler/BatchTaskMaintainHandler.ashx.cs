using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Services;
using Qisda.PMS.Common;
using Qisda.PMS.Entity;
using Qisda.PMS.Business;

namespace PMS.PMS.Handler
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class BatchTaskMaintainHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            PageParameterManager pageParameterManager = new PageParameterManager(context);

            var tmpSdpImportdetail = new TmpSdpImportdetail
            {
                Pmsid = pageParameterManager.GetString("Pmsid"),

                //tmpSdpImportdetail.Pmsid = pageParameterManager.GetRequiredGuid("Pmsid").ToString(),
                //Subject_Category = pageParameterManager.GetRequiredString("subjectCategory"),
                //Subject_Id = pageParameterManager.GetRequiredGuid("subjectId"),
                //Resource_Org_Id = pageParameterManager.GetRequiredGuid("resourceOrgId"),
            };

            TmpSdpImportdetailBiz tmpSdpImportdetailBiz = new TmpSdpImportdetailBiz();
            var listTmpSdpImportdetail = tmpSdpImportdetailBiz.GetTmpSdpDetail(tmpSdpImportdetail);
            var serializer = EasyuiTreegridHelp.Serializer<TmpSdpImportdetail>(listTmpSdpImportdetail, null, "Parentno", string.Empty);


            context.Response.ContentType = "text/plain";
            context.Response.Write(serializer);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
