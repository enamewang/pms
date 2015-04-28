using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.Collections;

namespace PMS.PMS.WebService
{
    /// <summary>
    /// AddNewTask 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class AddNewTask : System.Web.Services.WebService
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
        public string GetResourceList(string pmsid)
        {
            PmsHead ObjPmsHead = new PmsHead();
            IList<PmsHead> pmsHead = new PmsHeadBiz().SelectPmsHeadByPmsId(pmsid);
            if (pmsHead != null && pmsHead.Count > 0)
            {
                ObjPmsHead = pmsHead[0];
                Hashtable rTable = new Hashtable();
                rTable.Add("PM", ObjPmsHead.Pm);
                rTable.Add("SD", ObjPmsHead.Sd);
                rTable.Add("SE", ObjPmsHead.Se);
                rTable.Add("QA", ObjPmsHead.Qa);
                
                string resourceList = "";
                foreach (string str in rTable.Values)
                {
                    string[] sArray = str.Split(';');
                    foreach (string name in sArray)
                    {
                        if (!resourceList.Contains(name))
                            resourceList += name + ",";
                    }
                }
                return resourceList;
            }
            else
                return "";
        }
        [WebMethod]
        public string GetRefCost(string complexity, string operationType, string taskType, string functionType, string programLanguage)
        {
            // RefCost            
            PmsSdpRefcost sdpRefcost = new PmsSdpRefcost();
            sdpRefcost.OperationType = int.Parse(operationType);
            sdpRefcost.TaskComplexity = int.Parse(complexity);
            sdpRefcost.Tasktype = int.Parse(taskType);
            sdpRefcost.Functiontype = int.Parse(functionType);
            sdpRefcost.Programlanguage = int.Parse(programLanguage);
            IList<PmsSdpRefcost> sdpRefcostlList = new PmsSdpRefcostBiz().SelectPmsSdpRefcost(sdpRefcost);
            if (sdpRefcostlList == null || sdpRefcostlList.Count == 0)
                return "";
            else
                return sdpRefcostlList[0].Refcost.ToString();
        }
        //Check TaskName和Resource不能重复
        [WebMethod]
        public string CheckTaskNameAndResource(string taskName, string resource, string pmsid, string phase, string role)
        {
            SdpDetail sdpDetail = new SdpDetail();
            sdpDetail.Pmsid = pmsid;
            sdpDetail.TaskName = taskName;
            sdpDetail.Phase = phase;
            sdpDetail.Role = role;
            sdpDetail.Resource = resource;

            IList<SdpDetail> sdpDetailList = new SdpDetailBiz().SelectSdpDetail(sdpDetail);
            if (sdpDetailList != null && sdpDetailList.Count > 0)
                if (sdpDetailList[0].TaskName == sdpDetail.TaskName && sdpDetailList[0].Resource == sdpDetail.Resource)
                    return "{'Exist':'Y','TaskStatus':'" + sdpDetailList[0].TaskStatus + "','serial':'" + sdpDetailList[0].Serial + "'}";
            return "{'Exist':'N','TaskStatus':'0','serial':'-1'}";
        }

        [WebMethod]
        public bool ImportSdpDetailBySerial(string serials)
        {

            string[] serialArray = serials.Split(';');
            foreach (string serial in serialArray)
            {
                TmpSdpImportdetail tmpSdpImportdetail = new TmpSdpImportdetail();
                tmpSdpImportdetail.Serial = int.Parse(serial.Trim());
                TmpSdpImportdetailBiz tmpSdpImportdetailBiz = new TmpSdpImportdetailBiz();
                var listTmpSdpImportdetail = tmpSdpImportdetailBiz.GetTmpSdpDetail(tmpSdpImportdetail);
                if (listTmpSdpImportdetail == null || listTmpSdpImportdetail.Count == 0)
                    return false;
                else
                    tmpSdpImportdetail = listTmpSdpImportdetail[0];


                //插入sdpDetail表
                SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                SdpDetail sdpDetail = new SdpDetail();
                DateTime dateTime = PmsSysBiz.GetDBDateTime();

                sdpDetail.Pmsid = tmpSdpImportdetail.Pmsid;
                sdpDetail.Phase = tmpSdpImportdetail.Phase;

                sdpDetail.TaskName = tmpSdpImportdetail.TaskName;
                sdpDetail.OperationType = tmpSdpImportdetail.OperationType;
                sdpDetail.TaskType = tmpSdpImportdetail.TaskType;
                sdpDetail.FunctionType = tmpSdpImportdetail.FunctionType;
                sdpDetail.ProgramLanguage = tmpSdpImportdetail.ProgramLanguage;
                sdpDetail.TaskComplexity = tmpSdpImportdetail.TaskComplexity;

                //根据条件取，为空就为planCost
                PmsSdpRefcost sdpRefcost = new PmsSdpRefcost();
                sdpRefcost.OperationType = sdpDetail.OperationType;
                sdpRefcost.TaskComplexity = sdpDetail.TaskComplexity;
                sdpRefcost.Tasktype = sdpDetail.TaskType;
                sdpRefcost.Functiontype = sdpDetail.FunctionType;
                sdpRefcost.Programlanguage = sdpDetail.ProgramLanguage;
                IList<PmsSdpRefcost> sdpRefcostlList = new PmsSdpRefcostBiz().SelectPmsSdpRefcost(sdpRefcost);
                if (sdpRefcostlList == null || sdpRefcostlList.Count == 0)
                    sdpDetail.Refcost = tmpSdpImportdetail.Plancost;
                else
                    sdpDetail.Refcost = sdpRefcostlList[0].Refcost;


                sdpDetail.Plancost = tmpSdpImportdetail.Plancost;
                sdpDetail.Auditor = new BaseDataUserBiz().SelectLeaderByLoginName(LoginName);
                sdpDetail.Planstartday = tmpSdpImportdetail.Planstartday;
                sdpDetail.Planendday = tmpSdpImportdetail.Planendday;
                sdpDetail.Resource = tmpSdpImportdetail.Resource;
                sdpDetail.Role = tmpSdpImportdetail.Role;
                sdpDetail.AuditStatus = 1;
                sdpDetail.TaskStatus = 1;
                sdpDetail.Taskno = 1;
                sdpDetail.Iseditable = "Y";
                sdpDetail.Deleteflag = "N";
                sdpDetail.Createdate = dateTime;
                sdpDetail.Createuser = LoginName;
                sdpDetail.Maintaindate = dateTime;
                sdpDetail.Maintainuser = LoginName;

                if (new SdpDetailBiz().InsertSdpDetail(sdpDetail) < 0)
                    return false;
                //更新临时表Falg栏位为“Y”
                TmpSdpImportdetail tmpSdpImportdetailForUpdate = new TmpSdpImportdetail();
                tmpSdpImportdetailForUpdate.Serial = int.Parse(serial.Trim());
                if (!tmpSdpImportdetailBiz.UpdateTmpSdpDetailFlag(tmpSdpImportdetailForUpdate))
                    return false;
            }
            return true;
        }

        [WebMethod]
        public bool UpdateSdpDetailBySerial(string serials, string sdpDetailSerials)
        {

            string[] serialArray = serials.Split(';');
            string[] sdpDetailSerialArray = sdpDetailSerials.Split(';');

            for (int i = 0; i < serialArray.Length; i++)
            //foreach (string serial in serialArray)
            {
                TmpSdpImportdetail tmpSdpImportdetail = new TmpSdpImportdetail();
                tmpSdpImportdetail.Serial = int.Parse(serialArray[i].Trim());
                TmpSdpImportdetailBiz tmpSdpImportdetailBiz = new TmpSdpImportdetailBiz();
                var listTmpSdpImportdetail = tmpSdpImportdetailBiz.GetTmpSdpDetail(tmpSdpImportdetail);
                if (listTmpSdpImportdetail == null || listTmpSdpImportdetail.Count == 0)
                    return false;
                else
                    tmpSdpImportdetail = listTmpSdpImportdetail[0];


                //更新sdpDetail表
                SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                SdpDetail sdpDetail = new SdpDetail();
                DateTime dateTime = PmsSysBiz.GetDBDateTime();


                //根据pmsid和serial 定位要跟新的任务
                sdpDetail.Pmsid = tmpSdpImportdetail.Pmsid;
                sdpDetail.Serial = int.Parse(sdpDetailSerialArray[i]);

                //获取已存在任务在 tmp_sdp_importdetail表中不存在的栏位
                SdpDetail SdpDetailResult = new SdpDetailBiz().SelectSdpDetail(sdpDetail)[0];
                sdpDetail = SdpDetailResult;

                sdpDetail.TaskName = tmpSdpImportdetail.TaskName;
                sdpDetail.OperationType = tmpSdpImportdetail.OperationType;
                sdpDetail.TaskType = tmpSdpImportdetail.TaskType;
                sdpDetail.FunctionType = tmpSdpImportdetail.FunctionType;
                sdpDetail.ProgramLanguage = tmpSdpImportdetail.ProgramLanguage;
                sdpDetail.TaskComplexity = tmpSdpImportdetail.TaskComplexity;

                //根据条件取，取值为空->refCost=planCost
                PmsSdpRefcost sdpRefcost = new PmsSdpRefcost();
                sdpRefcost.OperationType = sdpDetail.OperationType;
                sdpRefcost.TaskComplexity = sdpDetail.TaskComplexity;
                sdpRefcost.Tasktype = sdpDetail.TaskType;
                sdpRefcost.Functiontype = sdpDetail.FunctionType;
                sdpRefcost.Programlanguage = sdpDetail.ProgramLanguage;
                IList<PmsSdpRefcost> sdpRefcostlList = new PmsSdpRefcostBiz().SelectPmsSdpRefcost(sdpRefcost);
                if (sdpRefcostlList == null || sdpRefcostlList.Count == 0)
                    sdpDetail.Refcost = tmpSdpImportdetail.Plancost;
                else
                    sdpDetail.Refcost = sdpRefcostlList[0].Refcost;
                sdpDetail.Plancost = tmpSdpImportdetail.Plancost;
                sdpDetail.Auditor = new BaseDataUserBiz().SelectLeaderByLoginName(LoginName);
                sdpDetail.Planstartday = tmpSdpImportdetail.Planstartday;
                sdpDetail.Planendday = tmpSdpImportdetail.Planendday;
                sdpDetail.Resource = tmpSdpImportdetail.Resource;
                sdpDetail.Role = tmpSdpImportdetail.Role;
                sdpDetail.Maintaindate = dateTime;
                sdpDetail.Maintainuser = LoginName;

                if (!sdpDetailBiz.UpdateSdpDetail(sdpDetail))
                    return false;
                //更新临时表Falg栏位为“Y”
                TmpSdpImportdetail tmpSdpImportdetailForUpdate = new TmpSdpImportdetail();
                tmpSdpImportdetailForUpdate.Serial = int.Parse(serialArray[i].Trim());
                if (!tmpSdpImportdetailBiz.UpdateTmpSdpDetailFlag(tmpSdpImportdetailForUpdate))
                    return false;
            }
            return true;
        }
        [WebMethod]
        public bool UpdateParentFlag(string serial)
        {
            TmpSdpImportdetail tmpSdpImportdetailForUpdate = new TmpSdpImportdetail();
            tmpSdpImportdetailForUpdate.Serial = int.Parse(serial.Trim());
            TmpSdpImportdetailBiz tmpSdpImportdetailBiz = new TmpSdpImportdetailBiz();
            if (!tmpSdpImportdetailBiz.UpdateTmpSdpDetailFlag(tmpSdpImportdetailForUpdate))
                return false;

            return true;
        }
    }
}
