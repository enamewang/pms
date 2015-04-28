using System;
using System.IO;
using System.Web;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PMS.PMS.AppCode;
using WSC;
using WSC.Common;
using WSC.Framework;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;

using Qisda.Web;
using Qisda.IO;

namespace PMS.PMS.Maintain
{
    public partial class Documents_Maintain : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["PmsID"] != null)
                {
                    PmsID = Request["PmsID"];
                }
                if (Request["CrID"] != null)
                {
                    CrID = Request["CrID"];
                }
                if (Request["ProjectType"] != null)
                {
                    ProjectType = Request["ProjectType"];
                }
                if (Request["System"] != null)
                {
                    System = Request["System"];
                }
                if (Request["NewVersion"] != null)
                {
                    NewVersion = Request["NewVersion"];
                }
                InitPage();
                BindGrid(sender, e);
            }
        }

        #region Define Variable

        private string PmsID
        {
            get
            {
                object obj = ViewState["PmsID"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["PmsID"] = value; }
        }

        private string CrID
        {
            get
            {
                object obj = ViewState["CrID"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["CrID"] = value; }
        }

        private string ProjectType
        {
            get
            {
                object obj = ViewState["ProjectType"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["ProjectType"] = value; }
        }

        private string System
        {
            get
            {
                object obj = ViewState["System"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["System"] = value; }
        }

        private string NewVersion
        {
            get
            {
                object obj = ViewState["NewVersion"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["NewVersion"] = value; }
        }

        private string LoginName
        {
            get
            {
                object obj = ViewState["LoginName"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["LoginName"] = value; }
        }

        #endregion

        private void InitPage()
        {
            Session["DocPage_Refresh"] = "N";
            LoginName = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");

            if (ProjectType == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
            {
                buttonAttachFile.OnClientClick = "showModalDialog('AttachFileServiceIframe.aspx?PmsID=" + PmsID +
                                                 "&CrID=" + CrID + "&RandomID=" + Guid.NewGuid().ToString() +
                                                 "','','dialogWidth=500px;dialogHeight=180px;center=yes;help=no;status=no;scroll=no');";
            }
            else
            {
                buttonAttachFile.OnClientClick = "showModalDialog('AttachFileMaintain.aspx?PmsID=" + PmsID + "&CrID=" + CrID + "&System=" + System + "&NewVersion=" + NewVersion +
                                                 "&RandomID=" + Guid.NewGuid().ToString() +
                                                 "','','dialogWidth=620px;dialogHeight=260px;center=yes;help=no;status=no;scroll=no');";
            }
            buttonGetFile.Visible = (ProjectType != PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription());
        }

        public void BindGrid(object sender, EventArgs e)
        {
            try
            {
                PmsDocuments pmsDoc = new PmsDocuments();
                pmsDoc.PmsId = PmsID;

                PmsDocumentsBiz pmsDocBiz = new PmsDocumentsBiz();
                IList<PmsDocuments> pmsDocList = pmsDocBiz.SelectPmsDocumentsOther(pmsDoc);

                // if projectType=="Service",绑定gridViewService,隐藏gridViewMain
                // else 绑定gridViewMain，隐藏gridViewService
                if (ProjectType == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                {
                    gridViewMain.Visible = false;
                    gridViewService.Visible = true;
                    gridViewService.DataSource = pmsDocList;
                    gridViewService.DataBind();
                }
                else
                {
                    gridViewMain.Visible = true;
                    gridViewService.Visible = false;
                    gridViewMain.DataSource = pmsDocList;
                    gridViewMain.DataBind();
                }

            }
            catch
            {
                Msgbox("BindGrid failure!");
            }
        }

        protected void buttonAttachFile_Click(object sender, EventArgs e)
        {
            string docPageRefresh = (Session["DocPage_Refresh"] ?? string.Empty).ToString();
            if (docPageRefresh == "Y")
            {
                BindGrid(sender, e); // BindGrid时需判断projectType

                Session["DocPage_Refresh"] = "N";
            }
        }

        protected void buttonGetFile_Click(object sender, EventArgs e)
        {
            PmsHead pmsHead = new PmsHead();
            DateTime dtCurDate = PmsSysBiz.GetDBDateTime();
            pmsHead.Creator = LoginName;
            pmsHead.CreateDate = dtCurDate;
            pmsHead.PmsId = PmsID;
            pmsHead.CrId = CrID;
            IList<PmsDocuments> listPmsDocuments = new PmsDocumentsBiz().GetPmsDocuments(pmsHead);
            int saveResult = 0;
            foreach (PmsDocuments pmsDocuments in listPmsDocuments)
            {
                IList<PmsDocuments> pmsDocList = new PmsDocumentsBiz().SelectPmsDocuments(pmsDocuments);
                if (pmsDocList != null && pmsDocList.Count > 0)
                {
                    continue;
                }
                saveResult = new PmsDocumentsBiz().InsertPmsDocuments(pmsDocuments);
                if (saveResult <= 0)
                {
                    Msgbox("Get File failure!");
                    return;
                }
            }
            BindGrid(sender, e); // BindGrid时需判断projectType
        }

        protected Hashtable FileExtensionExportFormat()
        {
            PmsSysBiz pmsSysBiz = new PmsSysBiz();
            IList<PmsSys> listPmsSys = pmsSysBiz.SelectData1Data2ByType("PM", "AttachmentType");
            Hashtable hashTableContentTypes = new Hashtable();
            foreach (PmsSys pmsSys in listPmsSys)
            {
                hashTableContentTypes.Add(pmsSys.Data1, pmsSys.Data2);
            }
            return hashTableContentTypes;
        }

        protected string GetExportFormatByFileExtension(string fileExtension)
        {
            Hashtable hashTableContentTypes = new Hashtable();
            hashTableContentTypes = FileExtensionExportFormat();
            string exportFormat = string.Empty;
            foreach (DictionaryEntry contentType in hashTableContentTypes)
            {
                if (contentType.Key.ToString().Equals(fileExtension.ToLower()))
                {
                    exportFormat = contentType.Value.ToString();
                }
            }
            return exportFormat;
        }

        protected void linkButton_OnCommand(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            string fileName = linkButton.Text;
            string filePath = linkButton.CommandArgument;
            if (File.Exists(filePath))
            {
                string fileExtension = Path.GetExtension(filePath);
                string exportFormat = GetExportFormatByFileExtension(fileExtension);
                if (exportFormat == string.Empty)
                {
                    Msgbox("fileExtension is Invalid!");
                }
                else
                {
                    ExportFile(fileName, filePath, exportFormat);
                }
            }

        }

        protected void ExportFile(string fileName, string filePath, string exportFormat)
        {
            FileInfo info = new FileInfo(filePath);

            if (!info.Exists)
            {
                throw new FileNotFoundException("File is not exists on the server.", filePath);
            }

            if ((HttpContext.Current.Request.Browser.Browser == "IE") &&

                (HttpContext.Current.Request.Browser.MajorVersion <= 5))
            {

                Response.Redirect(Server.MapPath(filePath));

                Response.End();

            }

            else
            {

                string MIME = exportFormat;
                Response.Clear();
                Response.ClearHeaders();
                Response.AppendHeader("content-disposition", "attachment; filename="
                  + HttpUtility.UrlEncode(fileName, Encoding.UTF8).Replace("+", "%20"));
                Response.AddHeader("Content-Length", info.Length.ToString());
                Response.Charset = "utf-8";
                Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
                //设置输出文件类型
                Response.ContentType = MIME + ";charset=utf-8";
                //把文件流发送到客户端
                Response.WriteFile(info.FullName);
                Response.Flush();
                Response.End();
                // HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

        }
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                int docTypeId = 0;
                string fileName = string.Empty;
                string owner = string.Empty;
                switch (ProjectType)
                {
                    // string service = PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription();
                    case "Service":
                        docTypeId = int.Parse(gridViewService.DataKeys[rowIndex]["DocTypeId"].ToString());
                        fileName = gridViewService.DataKeys[rowIndex]["FileName"].ToString();
                        owner = gridViewService.DataKeys[rowIndex]["Creator"].ToString();
                        break;
                    default:
                        docTypeId = int.Parse(gridViewMain.DataKeys[rowIndex]["DocTypeId"].ToString());
                        fileName = gridViewMain.DataKeys[rowIndex]["FileName"].ToString();
                        owner = gridViewMain.DataKeys[rowIndex]["Creator"].ToString();
                        break;
                }

                if (string.Compare(owner.ToLower(), LoginName.ToLower()) != 0)
                {
                    Msgbox("You can not delete it, you are not owner!");
                    return;
                }
                PmsDocuments pmsDoc = new PmsDocuments();
                pmsDoc.PmsId = PmsID;
                pmsDoc.DocTypeId = docTypeId;
                pmsDoc.FileName = fileName;
                PmsDocumentsBiz pmsDocBiz = new PmsDocumentsBiz();
                int deleteResult = pmsDocBiz.DeletePmsDocuments(pmsDoc);
                if (deleteResult <= 0)
                {
                    Msgbox("Delete failure!");
                }
                else
                {
                    if (ProjectType == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
                    {
                        string filePath = gridViewService.DataKeys[rowIndex]["Path"].ToString();

                        File.Delete(filePath); //删除数据库记录的同时，删除服务器上的文件
                    }
                    BindGrid(sender, e);
                }
            }
            catch
            {
                Msgbox("Delete failure!");
            }

        }

    }
}
