using System;
using System.Web;
using System.Text;
using System.Collections.Generic;
using PMS.PMS.AppCode;
using Qisda.PMS.Entity;
using Qisda.PMS.Business;
using System.IO;
using System.Configuration;

namespace PMS.PMS.Maintain
{
    public partial class AttachFileMaintainService : PageBase
    {

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


        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            
            // 上传，显示，保存此次上传的文件名和路径。

            if (FileUpload.HasFile)
            {
                PmsID = HiddenField3.Value;// move up by Ename Wang on 20120208
                CrID = HiddenField4.Value;// move up by Ename Wang on 20120208
                string fileName = FileUpload.FileName;
                string uploadFile = ConfigurationManager.AppSettings["UploadFile"];
                string fileFullPath = Server.MapPath(uploadFile) + "\\" + CrID + "\\" + fileName;
                string path = Server.MapPath(uploadFile) + "\\" + CrID + "\\";
                string fileSize = FileUpload.PostedFile.ContentLength.ToString();
                string fileType = Path.GetExtension(fileName);

                try
                {
                    //检查服务器对应CrId的文件夹中，是否已经存在要上传的文件
                    if (!CheckFileExist(path, fileFullPath))
                    {

                        if (!FileTypeIsAllowable(fileType))
                        {
                            Msgbox("File Type is not allowable!");
                            return;
                        }
                        int saveResult;
                        
                        InsertPmsDocuments(fileName, fileFullPath, fileSize, out saveResult); //更新DB
                        if (saveResult <= 0)
                        {
                            Msgbox("Save failure!");
                            ;
                        }
                        else
                        {
                            FileUpload.SaveAs(fileFullPath);
                            Session["DocPage_Refresh"] = "Y";
                            Msgbox("Save Successfully!");
                            PageRegisterStartupScript("window.close();");
                        }

                    }


                }

                catch (Exception ex)
                {

                    Msgbox("Upload file failed");
                }
            }
            else
            {

                Msgbox("please choose file to upload!");

            }

        }

        protected bool CheckFileExist(string path, string fileFullPath)
        {
            bool result = false;
            //check CrID命名的文件夹是否存在
            if (Directory.Exists(path))
            {
                if (File.Exists(fileFullPath))
                {
                    result = true;
                    Msgbox("File with the same name exists!");
                }

            }
            else
            {
                Directory.CreateDirectory(path);
            }
            return result;
        }

        private bool FileTypeIsAllowable(string fileType)
        {
            PmsSysBiz pmsSysBiz = new PmsSysBiz();
            IList<PmsSys> listPmsSys = pmsSysBiz.SelectData1ByType("PM", "AttachmentType");
            foreach (PmsSys pmsSys in listPmsSys)
            {
                if (fileType.ToLower().Trim().Equals(pmsSys.Data1))
                {
                    return true;
                }
            }
            return false;
        }


        protected void InsertPmsDocuments(string fileName, string fileFullPath, string fileSize, out int saveResult)
        {
            PmsDocuments pmsDocuments = new PmsDocuments();
            pmsDocuments.PmsId = PmsID;
            pmsDocuments.DocTypeId = (int)PmsCommonEnum.DocumentType.Other;
            pmsDocuments.FileName = fileName;
            pmsDocuments.Path = fileFullPath;
            pmsDocuments.Size = fileSize;

            pmsDocuments.Creator = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", "."); ;
            DateTime dtCurDate = PmsSysBiz.GetDBDateTime();
            pmsDocuments.CreateDate = dtCurDate;

            PmsDocumentsBiz pmsDocBiz = new PmsDocumentsBiz();
            saveResult = pmsDocBiz.InsertPmsDocuments(pmsDocuments);
        }

    }
}
