using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using PMS.PMS.AppCode;
using WSC;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;
using System.Collections.Generic;

namespace PMS.PMS.Maintain
{
    public partial class AttachFileMaintain : PageBase
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

                if (Request["System"] != null)
                {
                    System = Request["System"];
                }

                if (Request["NewVersion"] != null)
                {
                    NewVersion = Request["NewVersion"];
                }
                InitPage();
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

        private string FileName
        {
            get
            {
                object obj = ViewState["FileName"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["FileName"] = value; }
        }




        #endregion

        private void InitPage()
        {
            LoginName = GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            InitDocType();
        }

        private void InitDocType()
        {
            try
            {
                PmsDocumentType pmsDocType = new PmsDocumentType();
                pmsDocType.Vid = "PM";

                PmsDocumentTypeBiz pmsDocTypeBiz = new PmsDocumentTypeBiz();
                IList<PmsDocumentType> pmsDocTypeList = pmsDocTypeBiz.SelectDistinctDocType(pmsDocType);

                dropdownlistDocType.DataSource = pmsDocTypeList;
                dropdownlistDocType.DataTextField = "TypeName";
                dropdownlistDocType.DataValueField = "TypeId";
                dropdownlistDocType.DataBind();

                dropdownlistDocType.Items.Insert(0, new ListItem());
                dropdownlistDocType.Items[0].Text = "";
                dropdownlistDocType.Items[0].Value = "";
            }
            catch
            {
                Msgbox("Init Document Type failure!");
            }
        }

        protected void dropdownlistDocType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string crId = CrID;
            string system = System;
            string typeId = dropdownlistDocType.SelectedValue;
            int intTypeId;
            int.TryParse(typeId, out intTypeId);
            string docType = ((PmsCommonEnum.DocumentType)intTypeId).GetDescription();
            string newVersion = NewVersion;
            string date = DateTime.Now.ToString("yyyyMMdd");
            string fileName = GetFileName(crId, system, docType, newVersion, date);
            // add by Ename Wang on 20120522 // BACH 2.0 PIS 修改PIS文件提示
            if (typeId != string.Empty)
            {
                if (docType == PmsCommonEnum.DocumentType.PIS.GetDescription())
                {
                    labelFileNameUIPIS.Visible = true;
                    labelFileNameUIPISIuput.Visible = true;
                    labelFileNameUIPISIuput.Text = GetFileNameForBach(crId, system, docType, newVersion, date, "UI");
                    labelFileNameUIPISIuput.ReadOnly = true;

                    labelFileNameServicePIS.Visible = true;
                    labelFileNameServicePISIuput.Visible = true;
                    labelFileNameServicePISIuput.Text = GetFileNameForBach(crId, system, docType, newVersion, date, "Service");
                    labelFileNameServicePISIuput.ReadOnly = true;

                    labelFileNameBusinessPIS.Visible = true;
                    labelFileNameBusinessPISIuput.Visible = true;
                    labelFileNameBusinessPISIuput.Text = GetFileNameForBach(crId, system, docType, newVersion, date, "Business");
                    labelFileNameBusinessPISIuput.ReadOnly = true;
                }
                else
                {
                    labelFileNameUIPIS.Visible = false;
                    labelFileNameUIPISIuput.Visible = false;
                    labelFileNameServicePIS.Visible = false;
                    labelFileNameServicePISIuput.Visible = false;
                    labelFileNameBusinessPIS.Visible = false;
                    labelFileNameBusinessPISIuput.Visible = false;
                }

                labelFileName.Visible = true;
                labelFileNameIuput.Visible = true;
                labelFileNameIuput.Text = fileName;
                labelFileNameIuput.ReadOnly = true;
            }
            else
            {
                labelFileName.Visible = false;
                labelFileNameIuput.Visible = false;
                labelFileNameUIPIS.Visible = false;
                labelFileNameUIPISIuput.Visible = false;
                labelFileNameServicePIS.Visible = false;
                labelFileNameServicePISIuput.Visible = false;
                labelFileNameBusinessPIS.Visible = false;
                labelFileNameBusinessPISIuput.Visible = false;
            }
            // end add

            // mark by Ename Wang on 20120522 
            //labelFileName.Visible = true;
            //labelFileNameIuput.Visible = true;
            //labelFileNameIuput.Text = fileName;
            //labelFileNameIuput.ReadOnly = true;
            // end mark  
        }

        private string GetFileNameForBach(string crId, string system, string docType, string newVersion, string date, string layer)
        {
            string fileName = string.Empty;
            string fileExtension = string.Empty;
            string systemNew = string.Empty;

            fileExtension = ".doc";

            //显示时去掉
            if (system.IndexOf("(") != -1)
            {
                systemNew = system.Split('(')[0];
            }
            else
            {
                systemNew = system;
            }

            fileName = crId + "_" + systemNew + "_" + layer + "_" + docType + "_" + newVersion + "_" + date + fileExtension;
            FileName = fileName;
            return fileName;
        }

        private string GetFileName(string crId, string system, string docType, string newVersion, string date)
        {
            string fileName = string.Empty;
            string fileExtension = string.Empty;
            string systemNew = string.Empty;
            if (docType == PmsCommonEnum.DocumentType.STC.GetDescription())
            {
                fileExtension = ".xls";
            }
            else
            {
                fileExtension = ".doc";
            }
            //显示时去掉
            if (system.IndexOf("(") != -1)
            {
                systemNew = system.Split('(')[0];
            }
            else
            {
                systemNew = system;
            }
            //docType为min的时候，例如PES MIN变成PES_MIN
            if (docType == PmsCommonEnum.DocumentType.PES_MIN.GetDescription() || docType == PmsCommonEnum.DocumentType.PIS_MIN.GetDescription()
               || docType == PmsCommonEnum.DocumentType.RLN_MIN.GetDescription() | docType == PmsCommonEnum.DocumentType.STP_MIN.GetDescription()
               || docType == PmsCommonEnum.DocumentType.Study_Report.GetDescription())
            {
                docType = docType.Replace(" ", "_");
            }
            fileName = crId + "_" + systemNew + "_" + docType + "_" + newVersion + "_" + date + fileExtension;
            FileName = fileName;
            return fileName;
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Check Validity
                if (dropdownlistDocType != null && dropdownlistDocType.Items.Count > 0)
                {
                    if (dropdownlistDocType.SelectedItem.Text.ToString().Trim() == "")
                    {
                        Msgbox("Please select Document Type!");
                        dropdownlistDocType.Focus();
                        return;
                    }
                }

                if (textboxUrl.Text.Trim() == "")
                {
                    Msgbox("Please input URL!");
                    textboxUrl.Focus();
                    return;
                }
                if (textboxUrl.Text.Length > 500)
                {
                    Msgbox("The length of URL is larger than 500!");
                    textboxUrl.Focus();
                    return;

                }

                if (!ValidateUrl(textboxUrl.Text.Trim()))
                {
                    Msgbox("The Url is not legal!");
                    textboxUrl.Focus();
                    return;
                }

                #endregion

                #region For Insert PmsDocuments
                PmsDocuments pmsDocuments = new PmsDocuments();
                pmsDocuments.PmsId = PmsID;
                pmsDocuments.DocTypeId = int.Parse(dropdownlistDocType.SelectedValue);
                pmsDocuments.FileName = FileName;

                if (textboxUrl.Text.Trim().LastIndexOf("\\") != -1)
                {
                    pmsDocuments.FileName = Server.UrlDecode(textboxUrl.Text.Trim().Substring(textboxUrl.Text.Trim().LastIndexOf("\\") + 1));
                }
                else
                {
                    pmsDocuments.FileName = Server.UrlDecode(textboxUrl.Text.Trim().Substring(textboxUrl.Text.Trim().LastIndexOf("/") + 1));
                }

                int documentTypeId = int.Parse(dropdownlistDocType.SelectedValue);


                PmsHead pmsHead = new PmsHeadBiz().SelectCrIdSystemVersionByPmsId(PmsID);

                string errorInfo;
                if (!CheckFileName(pmsDocuments.FileName, documentTypeId, pmsHead.CrId, pmsHead.System, pmsHead.NewVersion, out  errorInfo))
                {
                    Msgbox(errorInfo);
                    return;
                }

                pmsDocuments.Path = textboxUrl.Text.Trim();
                DateTime dtCurDate = PmsSysBiz.GetDBDateTime();
                pmsDocuments.Creator = LoginName;
                pmsDocuments.CreateDate = dtCurDate;
                //默认给"0"
                pmsDocuments.Size = "0";

                PmsDocumentsBiz pmsDocBiz = new PmsDocumentsBiz();
                IList<PmsDocuments> pmsDocList = pmsDocBiz.SelectPmsDocuments(pmsDocuments);
                int saveResult = 0;
                if (pmsDocList != null && pmsDocList.Count > 0)
                {
                    Msgbox("File Exists!");
                    return;
                }

                saveResult = pmsDocBiz.InsertPmsDocuments(pmsDocuments);
                if (saveResult <= 0)
                {
                    Msgbox("Save failure!");
                    return;
                }
                #endregion

                Session["DocPage_Refresh"] = "Y";
                Msgbox("Save Successfully!");

                PageRegisterStartupScript("window.close();");

            }
            catch
            {
                Msgbox("Save failure!");
            }
        }

        //检查Url是否合法
        private bool ValidateUrl(string url)
        {
            try
            {
                Uri newUri = new Uri(url);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //url合法性检查正则表达式:
        // ^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)?((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.[a-zA-Z]{2,4})(\:[0-9]+)?(/[^/][a-zA-Z0-9\.\,\?\ '\\/\+&%\$#\=~_\-@]*)*$ 

        private bool CheckFileName(string fileName, int documentTypeId, string crId, string systemName, string version, out string errorInfo)
        {
            bool flag = true;
            #region  文件扩展名过滤
            errorInfo = string.Empty;

            if (documentTypeId != (int)PmsCommonEnum.DocumentType.Other)
            {
                //获取文件扩展名
                string fileExtension = Path.GetExtension(fileName).ToLower();

                //允许上传的文件类型
                IList<string> allowedExtensions = new List<string>() { ".xls", ".xlsx", ".doc", ".docx" };

                //检查上传的文件类型是否符合要求)
                flag = allowedExtensions.Any(t => t == fileExtension);
                if (!flag)
                {
                    errorInfo = "Cannot accept files of this type.";
                    return false;
                }
            }
            else
            {
                //获取文件扩展名
                string fileExtension = Path.GetExtension(fileName).ToLower();
                //允许上传的文件类型
                IList<string> unAllowedExtensions = new List<string>() { ".exe" };

                //检查上传的文件类型是否符合要求)
                flag = unAllowedExtensions.Any(t => t == fileExtension);
                if (flag)
                {
                    errorInfo = "Cannot accept files of this type.";
                    return false;
                }
            }

            #endregion

            #region 文件名称规范检查
            flag = CompareTemplateUploadDocumentFileName(crId, systemName, documentTypeId, version, fileName, out errorInfo);
            return flag;
            #endregion
        }


        private bool CompareTemplateUploadDocumentFileName(string crId, string systemName, int documentTypeId, string version, string fileNameUpload, out string errorInfo)
        {
            bool flag;
            errorInfo = "";
            // 比如ACP(e-Charge) 是System name ，但是file name中的System要把括号去掉，只保留ACP。
            // 比较时也要将(e-Charge)去掉再比较。
            if (systemName.IndexOf("(") != -1)
            {
                systemName = systemName.Split('(')[0];
            }
            string fileNamePart = crId + "_" + systemName;

            switch (documentTypeId)
            {
                case (int)PmsCommonEnum.DocumentType.PES:
                    fileNamePart = fileNamePart + "_PES_" + version;
                    flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    if (!flag)
                    {
                        //errorInfo += "Please Check the fileName,it could be like:" + fileNamePart + DateTime.Now.ToString("yyyyMMdd") + "\r\n";
                        errorInfo += "Helpful hints: PES FileName Format: [crId]_[systemName]_PES_[newVersionsion]_[date]";
                    }
                    break;

                case (int)PmsCommonEnum.DocumentType.PIS:
                    // mark by Ename Wang on 20130522
                    //fileNamePart = fileNamePart + "_PIS_" + version;
                    //flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    // end mark

                    // add by Ename Wang on 20130522
                    string fileNamePartNormal = fileNamePart + "_PIS_" + version;
                    string fileNamePartUI = fileNamePart + "_UI" + "_PIS_" + version;
                    string fileNamePartService = fileNamePart + "_Service" + "_PIS_" + version;
                    string fileNamePartBusiness = fileNamePart + "_Business" + "_PIS_" + version;

                    flag = (CheckUploadFileNameWithTemplate(fileNamePartNormal, fileNameUpload)
                            || CheckUploadFileNameWithTemplate(fileNamePartUI, fileNameUpload)
                            || CheckUploadFileNameWithTemplate(fileNamePartService, fileNameUpload)
                            || CheckUploadFileNameWithTemplate(fileNamePartBusiness, fileNameUpload)
                            );
                    // end add
                    if (!flag)
                    {
                        // errorInfo += "Please Check the fileName,it could be like:" + fileNamePart + DateTime.Now.ToString("yyyyMMdd") + "\r\n";
                        errorInfo += "Helpful hints: PIS FileName Format: [crId]_[systemName]_PIS_[newVersion]_[Date]";
                    }
                    break;

                case (int)PmsCommonEnum.DocumentType.STP:
                    fileNamePart = fileNamePart + "_STP_" + version;
                    flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    if (!flag)
                    {
                        // errorInfo += "Please Check the fileName,it could be like:" + fileNamePart + DateTime.Now.ToString("yyyyMMdd") + "\r\n";
                        errorInfo += "Helpful hints:STP FileName Format:[crId]_[systemName]_STP_[newVersion]_[Date]";
                    }

                    break;

                case (int)PmsCommonEnum.DocumentType.STC:
                    fileNamePart = fileNamePart + "_STC_" + version;
                    flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    if (!flag)
                    {

                        errorInfo += "Helpful hints:STC FileName Format:[crId]_[systemName]_STC_[newVersion]_[Date]";
                    }

                    break;

                case (int)PmsCommonEnum.DocumentType.RLN:
                    fileNamePart = fileNamePart + "_RLN_" + version;
                    flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    if (!flag)
                    {
                        errorInfo += "Helpful hints:RLN FileName Format:[crId]_[systemName]_RLN_[newVersion]_[Date]";
                    }

                    break;

                case (int)PmsCommonEnum.DocumentType.Study_Report:
                    fileNamePart = fileNamePart + "_Study_Report_" + version;
                    flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    if (!flag)
                    {


                        errorInfo += "Helpful hints:Study Report FileName Format:[crId]_[systemName]_Study_Report_[newVersion]_[Date]";
                    }

                    break;

                case (int)PmsCommonEnum.DocumentType.Other:


                    flag = true;
                    break;

                case (int)PmsCommonEnum.DocumentType.PES_MIN:

                    fileNamePart = fileNamePart + "_PES_MIN_" + version;
                    flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    if (!flag)
                    {


                        errorInfo += "Helpful hints:PES MIN FileName Format:[crId]_[systemName]_PES_MIN_[newVersion]_[Date]";
                    }

                    break;

                case (int)PmsCommonEnum.DocumentType.PIS_MIN:

                    fileNamePart = fileNamePart + "_PIS_MIN_" + version;
                    flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    if (!flag)
                    {


                        errorInfo += "Helpful hints:PIS MIN FileName Format:[crId]_[systemName]_PIS_MIN_[newVersion]_[Date]";
                    }

                    break;

                case (int)PmsCommonEnum.DocumentType.STP_MIN:

                    fileNamePart = fileNamePart + "_STP_MIN_" + version;
                    flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    if (!flag)
                    {

                        errorInfo += "Helpful hints:STP MIN FileName Format:[crId]_[systemName]_STP_MIN_[newVersion]_[Date]";


                    }
                    break;

                case (int)PmsCommonEnum.DocumentType.RLN_MIN:
                    fileNamePart = fileNamePart + "_RLN_MIN_" + version;
                    flag = CheckUploadFileNameWithTemplate(fileNamePart, fileNameUpload);
                    if (!flag)
                    {

                        errorInfo += "Helpful hints:RLN MIN FileName Format:[crId]_[systemName]_RLN_MIN_[newVersion]_[Date]";

                    }
                    break;
                default:

                    errorInfo = "Unknown document type!";
                    flag = false;
                    break;
            }
            return flag;

        }

        private bool CheckUploadFileNameWithTemplate(string fileNamePart, string fileNameUpload)
        {
            bool flag;
            try
            {
                string fileNameUploadPart = fileNameUpload.Substring(0, fileNamePart.Length);

                //获取文件扩展名
                string fileExtension = Path.GetExtension(fileNameUpload).ToLower();
                string fileNameUploadDatePart = fileNameUpload.Substring(fileNamePart.Length + 1, fileNameUpload.Length - (fileNamePart.Length + 1 + fileExtension.Length));

                if (fileNameUploadPart == fileNamePart && fileNameUploadDatePart.Length == 8)
                {
                    //强制转换，如果成功，曾OK，不成功，跑出错误，证明命名不正确。
                    DateTime temp = DateTime.Parse(fileNameUploadDatePart.Substring(0, 4) + "-" +
                                    fileNameUploadDatePart.Substring(4, 2) + "-" +
                                    fileNameUploadDatePart.Substring(6, 2));
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
    }
}
