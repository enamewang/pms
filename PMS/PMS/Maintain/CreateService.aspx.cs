
#region -- Page Information --
//////////////////////////////////////////////////////////////////////////////////
// File name: CreateService.aspx.cs    
// Copyright (C) 2011 Qisda Corporation. All rights reserved.    
// Author:		  Ename Wang   
// ALTER  Date:   2011/11/16
// Current Version:  1.0
// Description:   behind code of AutoExecutionSetup.aspx
// History: 
// 
//      Date     |    Time    |    Author   |  Modification 
// 1  2011/11/16 |   08:30:16 |  Ename Wang |  Create
// 1  2012/03/21 |   10:37:30 |  Ename Wang |  add flowId
//////////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.Collections;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using PMS.PMS.AppCode;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using Qisda.PMS.Common;
using Qisda.Web;
using System.Configuration;
using System.Linq;


namespace PMS.PMS.Maintain
{
    public partial class CreateService : PageBase
    {
        protected PmsCRCreatBiz m_PmsCRCreatBiz = new PmsCRCreatBiz();

        #region View State

        private string LoginName
        {
            get
            {
                object obj = ViewState["LoginName"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["LoginName"] = value; }
        }

        private List<string> ListFileName
        {
            get
            {
                object obj = ViewState["ListFileName"];
                if (obj == null)
                {
                    List<string> listFileName = new List<string>();

                    return listFileName;
                }
                return (List<string>)obj;
            }
            set { ViewState["ListFileName"] = value; }
        }

        private Dictionary<string, string> Dictionary_FileNames
        {
            get
            {
                object obj = ViewState["Dictionary_FileNames"];

                if (obj == null)
                {
                    Dictionary<string, string> aDictionary_FileName = new Dictionary<string, string>();
                    ViewState["Dictionary_FileNames"] = aDictionary_FileName;
                    return aDictionary_FileName;
                }

                return (Dictionary<string, string>)obj;
            }

        }

        private Dictionary<string, string> Dictionary_FilePathSize
        {
            get
            {
                object obj = ViewState["FilePathSize"];

                if (obj == null)
                {
                    Dictionary<string, string> dictionary_FilePathSize = new Dictionary<string, string>();
                    ViewState["FilePathSize"] = dictionary_FilePathSize;
                    return dictionary_FilePathSize;
                }

                return (Dictionary<string, string>)obj;
            }

        }

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            LoginName = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            BindDropDownListPriority();
            BindDropDownListTeamDomain();
            BindDropDownListSite();
            BindDropDownListSystem();
        }
        private void BindDropDownListPriority()
        {
            try
            {
                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Priority");

                dropdownlistPriority.DataSource = pmsSysList;
                dropdownlistPriority.DataTextField = "Data1";
                dropdownlistPriority.DataValueField = "Data1";
                dropdownlistPriority.DataBind();
                QWeb.SelectItem(dropdownlistPriority, "Normal");
            }
            catch
            {
                Msgbox("Bind Priority failure!");
            }
        }
        private void BindDropDownListTeamDomain()
        {
            try
            {
                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Domain");

                dropdownlistTeamDomain.DataSource = pmsSysList;
                dropdownlistTeamDomain.DataTextField = "data1";
                dropdownlistTeamDomain.DataValueField = "data1";
                dropdownlistTeamDomain.DataBind();

                dropdownlistTeamDomain.Items.Insert(0, new ListItem());
                dropdownlistTeamDomain.Items[0].Text = "";
                dropdownlistTeamDomain.Items[0].Value = "";
            }
            catch
            {
                Msgbox("Bind Team Domain failure!");
            }
        }

        //TODO:Site换成多选DropDownList,联动时，多个site结果合并时，System 需要Distinct。
        private void BindDropDownListSite()
        {
            try
            {
                //TODO:?? 根据TeamDomain取得Site?
                dropdownlistSite.Items.Clear();
                string teamDomain = dropdownlistTeamDomain.SelectedValue;

                if (string.IsNullOrEmpty(teamDomain))
                {
                    return;
                }

                PmsCRCreatBiz pmsCRCreatBiz = new PmsCRCreatBiz();
                IList<PmsSystemVersion> pmsSystemVersionList = pmsCRCreatBiz.SelectPmsSystemVersionByTeamDomain(teamDomain) ?? new List<PmsSystemVersion>();
                IList<string> sitelist = pmsSystemVersionList.Select(t => t.Site).Distinct().ToList();



                //PmsSysBiz pmsSysBiz = new PmsSysBiz();
                //IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Site");

                dropdownlistSite.DataSource = sitelist;
                dropdownlistSite.DataBind();

                dropdownlistSite.Items.Insert(0, new ListItem());
                dropdownlistSite.Items[0].Text = "";
                dropdownlistSite.Items[0].Value = "";

            }
            catch
            {
                Msgbox("Bind Site failure!");
            }

        }
        private void BindDropDownListSystem()
        {
            try
            {
                dropdownlistSystem.Items.Clear();
                string teamDomain = dropdownlistTeamDomain.SelectedValue;
                string site = dropdownlistSite.SelectedValue;
                if (string.IsNullOrEmpty(teamDomain) || string.IsNullOrEmpty(site))
                {
                    return;
                }
                PmsCRCreatBiz pmsCRCreatBiz = new PmsCRCreatBiz();
                IList<PmsSystemVersion> pmsSystemVersionList = pmsCRCreatBiz.SelectPmsSystemVersionByTeamDomainSite(teamDomain, site) ?? new List<PmsSystemVersion>();
                IList<string> systemlist = pmsSystemVersionList.Select(t => t.System).Distinct().OrderBy(t => t).ToList();

                if (!systemlist.Any(t => (t ?? string.Empty).ToLower() == "other"))
                {
                    systemlist.Insert(0, "Other");
                }
                //对数据源做distinct()
                dropdownlistSystem.DataSource = systemlist;
                dropdownlistSystem.DataBind();

                dropdownlistSystem.Items.Insert(0, new ListItem());
                dropdownlistSystem.Items[0].Text = "";
                dropdownlistSystem.Items[0].Value = "";
            }
            catch
            {
                Msgbox("Bind System failure!");
            }

        }
        #endregion

        #region SelectedIndexChanged

        protected void dropdownlistSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDownListSystem();
        }

        protected void dropdownlistTeamDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDownListSite();
            BindDropDownListSystem();
        }
        #endregion

        #region Button Click

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //get creator,createDate,pmsID,crID
                string creator = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
                DateTime createDate;
                string pmsID = string.Empty;
                string crId = string.Empty;
                m_PmsCRCreatBiz.GetNewPmsIdTempCrId(out createDate, out pmsID, out crId);

                //get pmsHead,pmsflow,sdpDetail,getPmsChangeHistory,getPmsItarmMapping,getPmsFlow,itarmCrList
                PmsHead pmsHead = getPmsHead(creator, createDate, pmsID);
                SdpDetail sdpDetail = getSdpDetail(pmsID);
                PmsChangeHistory pmsChangeHistory = getPmsChangeHistory(creator, createDate, pmsID);
                PmsItarmMapping pmsItarmMapping = getPmsItarmMapping(creator, createDate, pmsID, crId);
                PmsFlow pmsFlow = getPmsFlow(creator, createDate, pmsID);
                ItarmCrList itarmCrList = getItarmCrList(creator, createDate, crId);
                IList<PmsDocuments> pmsDocuments = getPmsDocuments(creator, createDate, pmsID, crId);
                if (pmsHead == null)
                {
                    Msgbox("Save failed");
                    return;
                }
                //Insert 
                PmsHeadBiz pmsInsert = new PmsHeadBiz();
                string errorInfo;
                int insertResult = pmsInsert.InsertPmsHeadAndDoc(pmsHead, sdpDetail, pmsChangeHistory, pmsItarmMapping, pmsFlow, itarmCrList, pmsDocuments, out errorInfo);

                if (insertResult == 0)
                {
                    Msgbox(errorInfo);
                }
                else
                {
                    //将TempFile文件夹里的文件移动到UploadFile下以CRNO命名的文件夹里并删除Temp里的文件。
                    MoveUploadFileDeleteTempFile(crId);
                    LoginName.Replace(".", " ");
                    pmsHead.UserName = LoginName;
                    new MailBiz().SendCreateMail(pmsHead);
                 
                    // 更新Stage 自动把这个Cr推进到AssginMember
                    int oldStage = (int)PmsCommonEnum.ProjectStage.PES;
                    int newStage = (int)PmsCommonEnum.ProjectStage.AssignMember;
                    string strAction = Enum.Parse(typeof(PmsCommonEnum.ProjectStage), oldStage.ToString()).GetDescription();
                    bool blResult = new BasicInformationDetailBiz().UpdateStages(pmsID, LoginName, oldStage, newStage, strAction);
                    if (!blResult)
                    {
                        Msgbox("更新stage数据失败！");
                        return;
                    }
                    new MailBiz().SendPromoteMail(pmsHead, newStage);                          
                    Msgbox("Create Successful!");
                    PageRegisterStartupScript("window.close();");
                }
            }
            catch (Exception exception)
            {

                throw new Exception(exception.ToString());
            }
        }

        private void MoveUploadFileDeleteTempFile(string crId)
        {

            string path = Server.MapPath("UploadFile") + "\\" + crId + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (KeyValuePair<string, string> keyValuePair in Dictionary_FileNames)
            {
                File.Copy(keyValuePair.Value, path + keyValuePair.Key);
                File.Delete(keyValuePair.Value);
            }

        }

        #region  Save Entity

        protected PmsHead getPmsHead(string creator, DateTime createDate, string pmsID)
        {
            PmsHead pmsHead = new PmsHead();

            pmsHead.Vid = "PM";
            pmsHead.Creator = creator;
            pmsHead.CreateDate = createDate;
            pmsHead.PmsId = pmsID;
            pmsHead.PmsName = textboxServiceName.Text;
            pmsHead.Priority = dropdownlistPriority.SelectedValue;

            // 根据teamDomain,system,site 取得Domain
            pmsHead.Domain = getDomain(dropdownlistTeamDomain.SelectedValue, dropdownlistSystem.SelectedValue, dropdownlistSite.SelectedValue);
            pmsHead.Site = dropdownlistSite.SelectedValue;
            pmsHead.ImpactSite = dropdownlistSite.SelectedValue;
            pmsHead.System = dropdownlistSystem.SelectedValue;
            //pmsHead.Description = FreeTextBoxDescription.Text;
            pmsHead.Description = TextBoxDescription.Text;
            if (pmsHead.Description.Length > 2000)
            {
                Msgbox("Description is too long!");
                return null;
            }

            pmsHead.NeedSTP = "N";
            pmsHead.NeedSTC = "N";
            pmsHead.Category = "N";
            pmsHead.NewVersion = "";
            pmsHead.OldVersion = "";


            pmsHead.Type = "Service";
            pmsHead.Stage = GetStageID("Service");
            pmsHead.Pm = creator;
            pmsHead.Sd = "";
            pmsHead.Se = "";
            pmsHead.Qa = "";

            pmsHead.MaintainUser = "";
            pmsHead.OwnerDept = m_PmsCRCreatBiz.GetOwnerDept(creator) ?? string.Empty;
            pmsHead.RelatedCrNo = "";

            return pmsHead;
        }

        private string getDomain(string teamDomain, string system, string site)
        {
            PmsCRCreatBiz pmsCrCreatBiz = new PmsCRCreatBiz();
            PmsSystemVersion pmsSystemVersion = pmsCrCreatBiz.SelectPmsSystemVersionByTeamSystemSite(teamDomain, system, site);
            return pmsSystemVersion.SystemDomain;
        }

        private int GetStageID(string typeId)
        {
            PmsFlowTemplateBiz pmsFlowTemplateBiz = new PmsFlowTemplateBiz();
            IList<PmsFlowTemplate> pmsFlowTemplateList = pmsFlowTemplateBiz.SelectPmsFlowTemplateByTypeId(typeId);
            int stageId = pmsFlowTemplateList[0].Stageid;
            return stageId;
        }




        protected SdpDetail getSdpDetail(string pmsID)
        {
            SdpDetail sdpDetail = new SdpDetail();
            sdpDetail.Pmsid = pmsID;
            sdpDetail.Type = "Service";
            return sdpDetail;
        }

        protected PmsChangeHistory getPmsChangeHistory(string creator, DateTime createDate, string pmsID)
        {
            PmsChangeHistory pmsChangeHistory = new PmsChangeHistory();

            pmsChangeHistory.Creator = creator;
            pmsChangeHistory.CreateDate = createDate;
            pmsChangeHistory.PmsId = pmsID;
            pmsChangeHistory.ChangeContent = "";
            pmsChangeHistory.Action = "CREATE";

            return pmsChangeHistory;
        }

        protected PmsItarmMapping getPmsItarmMapping(string creator, DateTime createDate, string pmsID, string crID)
        {
            PmsItarmMapping pmsItarmMapping = new PmsItarmMapping();
            pmsItarmMapping.Creator = creator;
            pmsItarmMapping.CreateDate = createDate;
            pmsItarmMapping.PmsId = pmsID;
            pmsItarmMapping.CrId = crID;
            return pmsItarmMapping;

        }

        protected PmsFlow getPmsFlow(string creator, DateTime createDate, string pmsID)
        {
            PmsFlow pmsFlow = new PmsFlow();
            pmsFlow.Creator = creator;
            pmsFlow.CreateDate = createDate;
            PmsFlowTemplateBiz pmsFlowTemplateBiz = new PmsFlowTemplateBiz();
            IList<PmsFlowTemplate> pmsFlowTemplateList = pmsFlowTemplateBiz.SelectPmsFlowTemplateByTypeId("Service");
            pmsFlow.FlowId = pmsFlowTemplateList[0].FlowId;
            pmsFlow.PmsId = pmsID;
            return pmsFlow;
        }

        protected ItarmCrList getItarmCrList(string creator, DateTime createDate, string crID)
        {
            ItarmCrList itarmCrList = new ItarmCrList();

            itarmCrList.Creator = creator;
            itarmCrList.CreateDate = createDate;
            itarmCrList.CrId = crID;
            itarmCrList.CrName = textboxServiceName.Text.Trim().Replace("'", "");
            itarmCrList.Site = dropdownlistSite.SelectedValue;
            itarmCrList.Pm = creator; // in default creator is pm
            itarmCrList.System = dropdownlistSystem.SelectedValue;

            return itarmCrList;
        }

        protected IList<PmsDocuments> getPmsDocuments(string creator, DateTime createDate, string pmsID, string crId)
        {
            IList<PmsDocuments> pmsDocumentsList = new List<PmsDocuments>();
            foreach (KeyValuePair<string, string> keyValuePair in this.Dictionary_FileNames)
            {
                PmsDocuments pmsDocuments = new PmsDocuments();
                pmsDocuments.Creator = creator;
                pmsDocuments.CreateDate = createDate;
                pmsDocuments.PmsId = pmsID;
                string docTypeId = "Other";
                pmsDocuments.DocTypeId = getTypeIdByTypeName(docTypeId);

                pmsDocuments.FileName = keyValuePair.Key;
                pmsDocuments.Path = getNewPath(keyValuePair.Key, keyValuePair.Value, crId);
                pmsDocuments.Size = getSizeByPath(keyValuePair.Value);
                pmsDocumentsList.Add(pmsDocuments);

            }
            return pmsDocumentsList;
        }
        private int getTypeIdByTypeName(string typeName)
        {
            int typeId = 0;
            PmsDocumentTypeBiz pmsDocumentTypeBiz = new PmsDocumentTypeBiz();
            PmsDocumentType pmsDocumentType = new PmsDocumentType();
            pmsDocumentType.TypeName = typeName;
            IList<PmsDocumentType> listPmsDocumentType = pmsDocumentTypeBiz.SelectPmsDocumentType(pmsDocumentType);

            if (listPmsDocumentType.Count > 0)
            {
                typeId = listPmsDocumentType[0].TypeId;
            }
            return typeId;
        }

        private string getNewPath(string fileName, string filePath, string crId)
        {
            filePath = filePath.Replace("TempFile", "UploadFile");
            int startIndex = filePath.LastIndexOf("\\") + 1;

            string oldFileName = filePath.Substring(startIndex);
            string oldFilePath = filePath.Substring(0, filePath.Length - oldFileName.Length);
            string newPath = oldFilePath + crId + "\\" + fileName;
            return newPath;
        }

        private string getSizeByPath(string path)
        {
            string size = string.Empty;
            foreach (KeyValuePair<string, string> keyValuePair in Dictionary_FilePathSize)
            {
                if (path == keyValuePair.Key)
                {
                    size = keyValuePair.Value;
                }
            }
            return size;
        }
        #endregion

        #endregion

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            // 上传，显示，保存此次上传的文件名和路径。

            if (FileUpload.HasFile)
            {
                string fileName = FileUpload.FileName;
                string filePath = Server.MapPath("TempFile") + "\\" + DateTime.Now.ToString("yyyyMMddfff") + "_" + FileUpload.FileName;
                string fileSize = FileUpload.PostedFile.ContentLength.ToString();
                string fileType = Path.GetExtension(fileName);
                if (FileNameExistsInListFileName(fileName))
                {
                    Msgbox("File Exists!");
                    return;
                }
                if (!FileTypeIsAllowable(fileType))
                {
                    Msgbox("File Type is not allowable!");
                    return;
                }
                try
                {
                    FileUpload.SaveAs(filePath);
                    List<string> listFileName = new List<string>();
                    listFileName = ListFileName;
                    listFileName.Add(fileName);
                    this.ListFileName = listFileName;
                    Dictionary_FileNames.Add(fileName, filePath);
                    Dictionary_FilePathSize.Add(filePath, fileSize);
                    BindGridView();

                    PageRegisterStartupScript("Refresh();");
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

        private bool FileNameExistsInListFileName(string fileName)
        {
            bool result = false;
            foreach (string name in ListFileName)
            {
                if (fileName == name)
                {
                    result = true;
                }
            }
            return result;
        }

        private bool FileTypeIsAllowable(string fileType)
        {
            ////从web.config读取判断文件类型限制  
            //string strFileTypeLimit = ConfigurationManager.AppSettings["FileType"];
            ////当前文件扩展名是否包含在这个字符串中  
            //if (strFileTypeLimit.IndexOf(fileType.ToLower()) != -1)
            //{

            //    return true;
            //}
            //else
            //    return false;

            PmsSysBiz pmsSysBiz=new PmsSysBiz();
            IList<PmsSys> listPmsSys = pmsSysBiz.SelectData1ByType("PM", "AttachmentType");
            foreach(PmsSys pmsSys in listPmsSys)
            {
                if (fileType.ToLower().Trim().Equals(pmsSys.Data1)) 
                {
                    return true;
                }     
            }
            return false;


        }

        protected void GridViewResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string dataItem = (string)e.Row.DataItem;
                Label labelFileName = (Label)e.Row.FindControl("linkFileName");
                labelFileName.Text = dataItem;
            }
        }

        private void BindGridView()
        {

            this.GridViewResult.DataSource = ListFileName;
            this.GridViewResult.DataBind();
        }

        protected void Button_delete(object sender, EventArgs e)
        {
            ImageButton imageButton = sender as ImageButton;
            GridViewRow dr = (GridViewRow)imageButton.Parent.Parent;
            string fileName = ((Label)dr.FindControl("linkFileName")).Text;
            string path;
            Dictionary_FileNames.TryGetValue(fileName, out path);
            Dictionary_FilePathSize.Remove(path);
            ListFileName.Remove(fileName);
            Dictionary_FileNames.Remove(fileName);
            File.Delete(path);
            BindGridView();

        }



    }
}

