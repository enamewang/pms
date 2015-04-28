#region -- Using NameSpace --
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMS.PMS.AppCode;
using System.IO;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Collections;
#endregion

namespace PMS.PMS.Maintain
{
    public partial class BatchTaskMaintain : PageBase
    {
        #region  Define Variable
        protected List<string> Template
        {
            get
            {
                List<string> template = new List<string>();
                template.Add("Role");
                template.Add("Phase");
                template.Add("TaskComplexity");
                template.Add("OperationType");
                template.Add("TaskType1");
                template.Add("ProgramLanguage");
                template.Add("FunctionType");
                return template;
            }

        }
        protected string pmsid;

        protected Hashtable resources;
        protected Hashtable Resources
        {
            get
            {
                return resources == null ? new Hashtable() : resources;

            }
            set
            {
                resources = value;
            }
        }

        protected Hashtable assignments;
        protected Hashtable Assignments
        {
            get
            {
                return assignments == null ? new Hashtable() : assignments;

            }
            set
            {
                assignments = value;
            }
        }

        protected Hashtable types;
        protected Hashtable Types
        {
            get
            {
                return types == null ? new Hashtable() : types;

            }
            set
            {
                types = value;
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
        private void InitPage()
        {

        }
        #endregion
        #region ButtonViewData_Click
        protected void ButtonViewData_Click(object sender, EventArgs e)
        {
            string crNo = this.TextBoxCrNo.Text.Trim();
            if (string.IsNullOrEmpty(crNo))
            {
                Msgbox("Please input CR No!");
                return;
            }
            IList<PmsItarmMapping> pmsItarmMappingresult = new PmsItarmMappingBiz().SelectPmsItarmMappingByPmsId(crNo);
            if (pmsItarmMappingresult != null && pmsItarmMappingresult.Count > 0)
            {
                pmsid = pmsItarmMappingresult[0].PmsId;
            }
            else
            {
                Msgbox("CR No Error!");
                return;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ButtonViewData_OnClientClick();", true);
        }
        #endregion

        #region ButtonNext_Click
        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            bool xmlSetResult = false;
            string crNo = this.TextBoxCrNo.Text.Trim();
            if (string.IsNullOrEmpty(crNo))
            {
                Msgbox("Please input CR No!");
                return;
            }
            IList<PmsItarmMapping> pmsItarmMappingresult = new PmsItarmMappingBiz().SelectPmsItarmMappingByPmsId(crNo);
            if (pmsItarmMappingresult != null && pmsItarmMappingresult.Count > 0)
            {
                pmsid = pmsItarmMappingresult[0].PmsId;
            }
            else
            {
                Msgbox("CR No Error!");
                return;
            }
            if (!FileUpload.HasFile)
            {
                Msgbox("please choose file to upload!");
                return;
            }
            try
            {
                string fileName = FileUpload.PostedFile.FileName;
                string subStr = fileName.Substring(fileName.Length - 4);
                if (subStr != ".xml")
                {
                    Msgbox("Please select a XML format file.");
                    return;
                }
                new TmpSdpImportdetailBiz().DeleteTmpSdpImportDetailByPmsId(pmsid);
                string fileFullPath = UpLoadFile();
                if (!SaveXml(fileFullPath))
                {
                    new TmpSdpImportdetailBiz().DeleteTmpSdpImportDetailByPmsId(pmsid);
                    return;
                }
                else xmlSetResult = true;
            }
            catch (Exception ex)
            {
                Msgbox("Import file to temporary table failed !");
            }

            //如果插入临时表成功就跳转到导入页面            
            if (xmlSetResult)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ButtonNext_OnClientClick();", true);
            }
            else return;
        }
        #endregion

        #region UpLoadFile
        private string UpLoadFile()
        {
            string fileName = FileUpload.FileName;
            string TempFile = ConfigurationManager.AppSettings["TempFile"];
            string fileFullPath = Server.MapPath(TempFile) + "\\" + fileName;
            string path = Server.MapPath(TempFile) + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(fileFullPath))
            {
                File.Delete(fileFullPath);
            }
            FileUpload.SaveAs(fileFullPath);
            return fileFullPath;
        }
        #endregion

        #region SaveXml
        private bool SaveXml(string xmlFileName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlFileName);
            Types = GetTypeTable(xDoc);
            //Check 是否使用模板
            if (!CheckUseTemplate(Types))
                return false;
            Resources = GetResourceTable(xDoc);
            Assignments = GetAssignmentTable(xDoc);

            XmlNodeList tasks = xDoc.GetElementsByTagName("Task");
            foreach (XmlNode node in tasks)
            {
                TmpSdpImportdetail tmpSdpDetail = new TmpSdpImportdetail();
                bool pass = false;
                List<string> list = Template;

                if (!GetTmpSdpDetail(node, ref tmpSdpDetail, ref pass, ref list))
                    return false;

                //没跳过并且没有选择全部新增栏位
                if (!pass && list.Count > 0)
                {
                    string columns = "";
                    foreach (string column in list)
                    {
                        if (columns != "")
                            columns += ",";
                        columns += column;
                    }
                    if (columns.Contains("TaskType1"))
                        columns = columns.Replace("TaskType1", "TaskType");
                    Msgbox("请完整填写列：" + columns);
                    return false;
                }
                if (!pass)
                {
                    tmpSdpDetail.Vid = "PM";
                    tmpSdpDetail.Pmsid = pmsid;
                    tmpSdpDetail.Flag = "N";
                    new TmpSdpImportdetailBiz().InsertTmpSdpDetail(tmpSdpDetail);
                }
            }
            return true;
        }
        #endregion

        #region CheckTaskType
        private bool CheckTaskType(string phase, string taskType, ref int taskTypeValue)
        {
            bool result = false;
            IList<PmsSys> pmsSysList = new PmsSysBiz().SelectData2Data3ByType("PM", "TaskType", phase);
            foreach (PmsSys pmsSys in pmsSysList)
            {
                if (pmsSys.Data3 == taskType)
                {
                    taskTypeValue = int.Parse(pmsSys.Data2);
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region GetResourceTable
        private Hashtable GetResourceTable(XmlDocument xDoc)
        {
            Hashtable resources = new Hashtable();
            XmlNodeList resourceList = xDoc.GetElementsByTagName("Resource");
            foreach (XmlNode node in resourceList)
            {
                string no = "";
                string name = "";
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    if (subNode.Name == "ID")
                    {
                        no = subNode.InnerText;
                    }
                    if (subNode.Name == "Name")
                    {
                        name = subNode.InnerText;
                    }
                }
                resources.Add(no, name);
            }
            return resources;
        }
        #endregion

        #region GetAssignmentTable
        private Hashtable GetAssignmentTable(XmlDocument xDoc)
        {
            Hashtable assignments = new Hashtable();
            XmlNodeList assignmentList = xDoc.GetElementsByTagName("Assignment");
            foreach (XmlNode node in assignmentList)
            {
                string taskId = "";
                string resourceId = "";
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    if (subNode.Name == "TaskUID")
                    {
                        taskId = subNode.InnerText;
                    }
                    if (subNode.Name == "ResourceUID")
                    {
                        resourceId = subNode.InnerText;
                    }
                }
                if (assignments.Contains(taskId))
                {
                    assignments[taskId] = assignments[taskId] + "," + resourceId;
                }
                else
                    assignments.Add(taskId, resourceId);
            }
            return assignments;
        }
        #endregion

        #region GetTypeTable
        private Hashtable GetTypeTable(XmlDocument xDoc)
        {
            Hashtable types = new Hashtable();
            XmlNode exAttrNode = xDoc.GetElementsByTagName("ExtendedAttributes")[0];
            XmlNodeList ExtendedAttributelist = exAttrNode.ChildNodes;
            foreach (XmlNode node in ExtendedAttributelist)
            {
                string alias = "";
                string fieldID = "";
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    if (subNode.Name == "Alias")
                    {
                        alias = subNode.InnerText;
                    }
                    if (subNode.Name == "FieldID")
                    {
                        fieldID = subNode.InnerText;
                    }
                }
                if (alias != "" && fieldID != "")
                    types.Add(fieldID, alias);
            }
            return types;
        }
        #endregion

        #region GetTaskTypeTable
        private List<string> GetTaskTypeTable(XmlNode node)
        {
            List<string> type = new List<string>();
            string value = "";
            string fieldID = "";
            foreach (XmlNode subNode in node.ChildNodes)
            {
                if (subNode.Name == "Value")
                {
                    value = subNode.InnerText;
                }
                if (subNode.Name == "FieldID")
                {
                    fieldID = subNode.InnerText;
                }
            }
            type.Add(fieldID);
            type.Add(value);
            return type;
        }
        #endregion

        #region CheckUseTemplate
        private bool CheckUseTemplate(Hashtable types)
        {
            string submit = "";
            foreach (string text in types.Values)
            {
                if (submit != "")
                    submit += ",";
                submit += text;
            }
            List<string> template = Template;
            foreach (string type in template)
            {
                if (!submit.Contains(type))
                {
                    Msgbox("Please use the project template!");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region GetTmpSdpDetail
        private bool GetTmpSdpDetail(XmlNode node, ref TmpSdpImportdetail tmpSdpDetail, ref bool pass, ref List<string> list)
        {
            foreach (XmlNode subNode in node.ChildNodes)
            {
                if (subNode.Name == "UID")
                {
                    string taskId = subNode.InnerText;
                    if (taskId == "0")
                    {
                        pass = true;
                        break;
                    }
                    if (Assignments[taskId] != null)
                    {
                        string resourceId = Assignments[taskId].ToString();
                        if (resourceId.Contains(","))
                        {
                            string[] resourceIds = resourceId.Split(',');
                            string resource = "";
                            foreach (string id in resourceIds)
                            {
                                if (resource != "")
                                    resource += ",";
                                resource += Resources[id].ToString();
                            }
                            tmpSdpDetail.Resource = resource;
                        }
                        else
                            tmpSdpDetail.Resource = Resources[Assignments[taskId]].ToString();
                    }
                    else
                    {
                        Msgbox("Resource names of all task can not be empty!");
                        return false;
                    }
                }
                if (subNode.Name == "ID")
                {
                    //如果没有任务名称跳过不保存
                    if (subNode.NextSibling.Name != "Name")
                    {
                        pass = true;
                        break;
                    }
                }
                if (subNode.Name == "WBS")
                {
                    string wbs = subNode.InnerText;
                    tmpSdpDetail.Wbs = wbs;
                    if (wbs == "0")
                    {
                        pass = true;
                        break;
                    }
                    if (wbs.Contains('.'))
                        tmpSdpDetail.Parentno = wbs.Remove(wbs.LastIndexOf("."));
                    else
                        tmpSdpDetail.Parentno = "";
                }
                if (subNode.Name == "Name")
                {
                    tmpSdpDetail.TaskName = subNode.InnerText;
                }
                if (subNode.Name == "Start")
                {
                    tmpSdpDetail.Planstartday = DateTime.Parse(subNode.InnerText);
                }
                if (subNode.Name == "Finish")
                {
                    tmpSdpDetail.Planendday = DateTime.Parse(subNode.InnerText);
                }
                if (subNode.Name == "Duration")
                {
                    string planCost = subNode.InnerText;
                    planCost = planCost.Replace("PT", "").Trim();
                    planCost = planCost.Replace("H0M0S", "").Trim();
                    tmpSdpDetail.Plancost = float.Parse(planCost);
                }
                if (subNode.Name == "ExtendedAttribute")
                {
                    List<string> typeInfo = GetTaskTypeTable(subNode);
                    string type = Types[typeInfo[0]].ToString();
                    list.Remove(type);
                    switch (type)
                    {
                        case "Role":
                            tmpSdpDetail.Role = typeInfo[1].ToString();
                            break;
                        case "TaskComplexity":
                            tmpSdpDetail.TaskComplexity = (int)Enum.Parse(typeof(PmsCommonEnum.TaskComplexity), typeInfo[1]);
                            break;
                        case "Phase":
                            Dictionary<string, string> phases = new PmsCommonEnum().GetEnumValueAndDesc(typeof(PmsCommonEnum.PlanPhase));
                            tmpSdpDetail.Phase = int.Parse(phases[typeInfo[1]]).ToString();
                            break;
                        case "TaskType1":

                            int taskTypeValue = -1;
                            if (!CheckTaskType(tmpSdpDetail.Phase, typeInfo[1], ref taskTypeValue))
                            {
                                Msgbox("TaskName-("+tmpSdpDetail.TaskName + ")：Pahse 与 TaskType 不匹配！");
                                return false;
                            }
                            else
                                tmpSdpDetail.TaskType = taskTypeValue;
                            break;
                        case "OperationType":
                            Dictionary<string, string> OperationTypes = new PmsCommonEnum().GetEnumValueAndDesc(typeof(PmsCommonEnum.OperationType));
                            tmpSdpDetail.OperationType = int.Parse(OperationTypes[typeInfo[1]]);
                            break;
                        case "ProgramLanguage":
                            Dictionary<string, string> ProgramLanguages = new PmsCommonEnum().GetEnumValueAndDesc(typeof(PmsCommonEnum.ProgramLanguage));
                            tmpSdpDetail.ProgramLanguage = int.Parse(ProgramLanguages[typeInfo[1]]);
                            break;
                        case "FunctionType":
                            tmpSdpDetail.FunctionType = (int)Enum.Parse(typeof(PmsCommonEnum.FunctionType), typeInfo[1]);
                            break;
                    }
                }
            }
            return true;
        }
        #endregion

        protected void BtnTemplateDownLoad_Click(object sender, EventArgs e)
        {
            string fileName = "BatchTaskImportTemplate.mpp";//客户端保存的文件名
            string filePath = Server.MapPath("../TemplateFile/BatchTaskImportTemplate.mpp");//路径

            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }

        protected void BtnSampleDownLoad_Click(object sender, EventArgs e)
        {
            string fileName = "APP_BACH2_Plan.mpp";//客户端保存的文件名
            string filePath = Server.MapPath("../TemplateFile/APP_BACH2_Plan.mpp");//路径

            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

       

    }
}
