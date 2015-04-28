using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PMS.PMS.AppCode;
using WSC;
using WSC.Common;
using WSC.Framework;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.Collections.Generic;
using Qisda.Web;
using Qisda.IO;
using Qisda.DateTime;
using Qisda.PMS.Common;

namespace PMS.PMS.Maintain
{
    public partial class Create1 : PageBase
    {
        protected PmsCRCreatBiz m_PmsCRCreatBiz = new PmsCRCreatBiz();

        #region Define Variable
        private string Action
        {
            get
            {
                object obj = ViewState["ACTION"];
                return (obj == null) ? "" : ((string)obj).Trim();
            }
            set { ViewState["ACTION"] = value; }
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

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Buffer = true;
            this.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            this.Response.Expires = 0;
            this.Response.CacheControl = "no-cache";

            if (!IsPostBack)
            {
                //if (Request["Action"] != null)
                //    Action = Request["Action"].ToString();

                InitPage();

                //switch (Action)
                //{
                //    case "ADD":
                //        //dropdownlistSite.Enabled = false;
                //        //dropdownlistImpactSite.Enabled = false;
                //        break;
                //    case "EDIT":
                //        //dropdownlistSite.Enabled = true;
                //        //dropdownlistImpactSite.Enabled = true;
                //        break;
                //}
                //给textboxSystem绑定点击事件
                textboxSystem.Attributes.Add("onClick", "ShowSystemListPopUp()");

            }
        }
        #endregion

        #region 初始化函数
        private void InitPage()
        {
            LoginName = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            BindDropDownListType();
            //BindDropDownListDoMain();
            BindDropDownListPriority();
            //BindDropDownListSiteAndImpactSite();
            BindDropDownListSiteImpactSiteDomain();
            // GetSystemVersion();

            //InitDoMain();
            textboxPM.Text = LoginName;
        }

        //private void InitDoMain()
        //{
        //    try
        //    {
        //        string strDoMainName = "";

        //        BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
        //        IList<BaseDataUser> baseDataUserList = baseDataUserBiz.SelectDoMainNameByLoginName(LoginName);

        //        if (baseDataUserList != null && baseDataUserList.Count > 0)
        //        {
        //            strDoMainName = baseDataUserList[0].DoMainName.ToString().Trim();
        //            dropdownlistDomain.SelectedIndex = dropdownlistDomain.Items.IndexOf(dropdownlistDomain.Items.FindByText(strDoMainName));
        //            //QWeb.SelectItem(dropdownlistDomain, strDoMainName);                    
        //        }
        //    }
        //    catch
        //    {
        //        Msgbox("Init DoMain failure!");
        //    }
        //}

        #region 绑定DropDownList Site ImpactSite Domain
        private void BindDropDownListSiteImpactSiteDomain()
        {
            PmsSysBiz pmsSysBiz = new PmsSysBiz();
            IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Site");
            dropdownlistSite.DataSource = pmsSysList;
            dropdownlistSite.DataTextField = "Data1";
            dropdownlistSite.DataValueField = "Data1";
            dropdownlistSite.DataBind();
            dropdownlistSite.Items.Insert(0, "");

            dropdownlistImpactSite.DataSource = pmsSysList;
            dropdownlistImpactSite.DataTextField = "Data1";
            dropdownlistImpactSite.DataValueField = "Data1";
            dropdownlistImpactSite.DataBind();
            dropdownlistImpactSite.Items.Insert(0, "");
            // QWeb.SelectItem(dropdownlistImpactSite, "QCS");


            SystemListPopUpBiz systemListPopUpBiz = new SystemListPopUpBiz();
            //IList<ItarmSystem> resultSite = systemListPopUpBiz.GetItarmSystemSite();
            //dropdownlistSite.DataSource = resultSite;
            //dropdownlistSite.DataTextField = "Site";
            //dropdownlistSite.DataValueField = "Site";
            //dropdownlistSite.DataBind();
            //dropdownlistSite.Items.Insert(0, "");

            //dropdownlistImpactSite.DataSource = resultSite;
            //dropdownlistImpactSite.DataTextField = "Site";
            //dropdownlistImpactSite.DataValueField = "Site";
            //dropdownlistImpactSite.DataBind();
            //QWeb.SelectItem(dropdownlistImpactSite, "QCS");

            IList<string> resultDomain = systemListPopUpBiz.GetItarmSystemDomain();
            dropdownlistDomain.DataSource = resultDomain;
            dropdownlistDomain.DataBind();
            dropdownlistDomain.Items.Insert(0, "");

        }
        #endregion

        #region BindDropDownList Type Priority
        private void BindDropDownListType()
        {
            try
            {
                //PmsSys pmsSys = new PmsSys();
                //pmsSys.Vid = "PM";
                //pmsSys.Type = "Type";

                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Type");
                var pmsService = pmsSysList.Where(t => t.Data1 == "Service").FirstOrDefault();
                pmsSysList.Remove(pmsService);
                dropdownlistType.DataSource = pmsSysList;
                dropdownlistType.DataTextField = "Data1";
                dropdownlistType.DataValueField = "Data1";
                dropdownlistType.DataBind();

                //dropdownlistType.Items.Insert(0, new ListItem());
                //dropdownlistType.Items[0].Text = "";
                //dropdownlistType.Items[0].Value = "";
                dropdownlistType.SelectedIndex = 0;
            }
            catch
            {
                Msgbox("Bind Type failure!");
            }
        }

        //private void BindDropDownListDoMain()
        //{
        //    try
        //    {
        //        BaseDataDomainBiz baseDataDomainBiz = new BaseDataDomainBiz();
        //        IList<BaseDataDomain> baseDataDomainList = baseDataDomainBiz.SelectBaseDataDomain(null);

        //        dropdownlistDomain.DataSource = baseDataDomainList;
        //        dropdownlistDomain.DataTextField = "Name";
        //        dropdownlistDomain.DataValueField = "Id";
        //        dropdownlistDomain.DataBind();
        //        dropdownlistDomain.SelectedIndex = 0;

        //        //dropdownlistDomain.Items.Insert(0, new ListItem());
        //        //dropdownlistDomain.Items[0].Text = "";
        //        //dropdownlistDomain.Items[0].Value = "";
        //    }
        //    catch
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind Domain failure');", true);
        //    }
        //}

        private void BindDropDownListPriority()
        {
            try
            {
                //PmsSys pmsSys = new PmsSys();
                //pmsSys.Vid = "PM";
                //pmsSys.Type = "Priority";

                PmsSysBiz pmsSysBiz = new PmsSysBiz();
                IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType("PM", "Priority");

                dropdownlistPriority.DataSource = pmsSysList;
                dropdownlistPriority.DataTextField = "Data1";
                dropdownlistPriority.DataValueField = "Data1";
                dropdownlistPriority.DataBind();

                QWeb.SelectItem(dropdownlistPriority, "Normal");

                //dropdownlistPriority.Items.Insert(0, new ListItem());
                //dropdownlistPriority.Items[0].Text = "";
                //dropdownlistPriority.Items[0].Value = "";
            }
            catch
            {
                Msgbox("Bind Priority failure!");
            }
        }

        //private void BindDropDownListSiteAndImpactSite()
        //{
        //    try
        //    {
        //        PmsSys pmsSys = new PmsSys();
        //        pmsSys.Vid = "PM";
        //        pmsSys.Type = "Site";

        //        PmsSysBiz pmsSysBiz = new PmsSysBiz();
        //        IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType(pmsSys);

        //        dropdownlistSite.DataSource = pmsSysList;
        //        dropdownlistSite.DataTextField = "Data1";
        //        dropdownlistSite.DataValueField = "Data1";
        //        dropdownlistSite.DataBind();

        //        dropdownlistSite.Items.Insert(0, "");

        //        dropdownlistImpactSite.DataSource = pmsSysList;
        //        dropdownlistImpactSite.DataTextField = "Data1";
        //        dropdownlistImpactSite.DataValueField = "Data1";
        //        dropdownlistImpactSite.DataBind();

        //        QWeb.SelectItem(dropdownlistImpactSite, "QCS");

        //        //dropdownlistSite.Items.Insert(0, new ListItem());
        //        //dropdownlistSite.Items[0].Text = "";
        //        //dropdownlistSite.Items[0].Value = "";

        //        //dropdownlistImpactSite.Items.Insert(0, new ListItem());
        //        //dropdownlistImpactSite.Items[0].Text = "";
        //        //dropdownlistImpactSite.Items[0].Value = "";                
        //    }
        //    catch
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Bind ImpactSite failure');", true);
        //    }

        //}
        #endregion
        #endregion

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //#region Define Variable
                ////string strSavePath = "";
                ////int iTypeID = 0;
                //#endregion

                //检查控件中的值
                if (!CheckControlValue())
                    return;

                //获取新的Pmsid，CrId以及他们的创建时间
                DateTime dtCurDate;
                string strPmsID = string.Empty;
                string strCrId = string.Empty;
                m_PmsCRCreatBiz.GetNewPmsIdTempCrId(out dtCurDate, out strPmsID, out strCrId);

                #region Get StageID
                PmsFlowTemplateBiz pmsFlowTemplateBiz = new PmsFlowTemplateBiz();
                IList<PmsFlowTemplate> pmsFlowTemplateList = pmsFlowTemplateBiz.SelectPmsFlowTemplateByTypeId(dropdownlistType.SelectedValue.Trim());
                int stageId = pmsFlowTemplateList[0].Stageid;
                #endregion

                #region For Insert PmsHead
                PmsHead pmsHeadInsert = new PmsHead();
                pmsHeadInsert.Vid = "PM";
                pmsHeadInsert.PmsId = strPmsID;
                pmsHeadInsert.PmsName = textboxPmsName.Text.Trim().Replace("'", "");
                pmsHeadInsert.Type = dropdownlistType.SelectedItem.Text.Trim();
                pmsHeadInsert.Description = textboxDescription.Text.Trim().Replace("'", "");
                pmsHeadInsert.System = textboxSystem.Text.Trim().Replace("'", "");
                pmsHeadInsert.Domain = dropdownlistDomain.SelectedItem.Text.Trim();
                pmsHeadInsert.Priority = dropdownlistPriority.SelectedItem.Text.Trim();
                pmsHeadInsert.Site = dropdownlistSite.SelectedValue;
                pmsHeadInsert.ImpactSite = dropdownlistImpactSite.SelectedItem.Text.Trim();
                pmsHeadInsert.NeedSTP = RadioButtonNeedSTPYes.Checked ? "Y" : "N";
                pmsHeadInsert.NeedSTC = RadioButtonNeedSTCYes.Checked ? "Y" : "N";
                //要插VB2Net add by ITO.Abel.Li 2014-01-06
                pmsHeadInsert.Category = RadioButtonVB2NetYes.Checked ? "Y" : "N";
                pmsHeadInsert.NewVersion = textboxNewVersion.Text.Trim().Replace("'", "");
                // pmsHeadInsert.OldVersion = textboxOldVersion.Text.Trim().Replace("'", "");
                pmsHeadInsert.OldVersion = HiddenFieldOldVersion.Value.Trim().Replace("'", "");

                pmsHeadInsert.DueDate = DateTime.Parse(dateTextBoxDueDate.Text.Trim());
                pmsHeadInsert.PlanStartDate = DateTime.Parse(dateTextBoxPlanStartDate.Text.Trim());
                //pmsHeadInsert.ReleaseDate = 0;
                //pmsHeadInsert.CloseDate = 0;
                pmsHeadInsert.Stage = stageId;
                pmsHeadInsert.Pm = textboxPM.Text.Trim().Replace("'", "");
                pmsHeadInsert.Sd = "";
                pmsHeadInsert.Se = "";
                pmsHeadInsert.Qa = "";
                pmsHeadInsert.AbnormalStage = 0;
                pmsHeadInsert.Rerver1 = "";
                pmsHeadInsert.Rerver2 = "";
                pmsHeadInsert.Rerver3 = "";
                pmsHeadInsert.Rerver4 = "";
                pmsHeadInsert.Rerver5 = "";
                pmsHeadInsert.CreateDate = dtCurDate;
                pmsHeadInsert.Creator = LoginName;
                //pmsHeadInsert.MaintainDate = dtCurDate;
                pmsHeadInsert.MaintainUser = "";
                pmsHeadInsert.OwnerDept = m_PmsCRCreatBiz.GetOwnerDept(LoginName) ?? string.Empty;
                pmsHeadInsert.RelatedCrNo = textboxRelatedCrNo.Text.Trim();
                #endregion

                #region For Insert PMS_ITARM_Mapping
                PmsItarmMapping pmsItarmMapping = new PmsItarmMapping();
                pmsItarmMapping.PmsId = strPmsID;
                pmsItarmMapping.CrId = strCrId;
                pmsItarmMapping.Creator = LoginName;
                pmsItarmMapping.CreateDate = dtCurDate;
                #endregion

                #region File(注释不用)
                #region Get Document Type
                //PmsDocumentType pmsDocType = new PmsDocumentType();
                //pmsDocType.Vid = "PM";
                //pmsDocType.TypeName = "PES";

                //PmsDocumentTypeBiz pmsDocTypeBiz = new PmsDocumentTypeBiz();
                //IList<PmsDocumentType> pmsDocTypeList = pmsDocTypeBiz.SelectPmsDocumentType(pmsDocType);

                //if (pmsDocTypeList != null && pmsDocTypeList.Count > 0)
                //{
                //    iTypeID = pmsDocTypeList[0].TypeId;
                //}
                #endregion

                #region Get UploadFilePath
                //PmsSys pmsSys = new PmsSys();
                //pmsSys.Vid = "PM";
                //pmsSys.Type = "UploadFilePath";

                //PmsSysBiz pmsSysBiz = new PmsSysBiz();
                //IList<PmsSys> pmsSysList = pmsSysBiz.SelectData1ByType(pmsSys);
                //if (pmsSysList != null && pmsSysList.Count > 0)
                //{
                //    strSavePath = pmsSysList[0].Data1.ToString().Trim();
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Please maintain UploadFilePath!');", true);
                //    return;
                //}
                #endregion

                #region For Insert PmsDocuments
                //PmsDocuments pmsDocuments = new PmsDocuments();
                //pmsDocuments.PmsId = strPmsID;
                //pmsDocuments.DocTypeId = iTypeID;
                //pmsDocuments.FileName = pesUpload.PostedFile.FileName.Substring(pesUpload.PostedFile.FileName.LastIndexOf("\\") + 1);

                //string strTmp = strSavePath + "\\" + strPmsID + "\\" + pmsDocuments.FileName;
                //if (strTmp.Length > 150)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('The length of SavePath is larger than 150!');", true);
                //    pesUpload.Focus();
                //    return;
                //}

                //pmsDocuments.Path = strSavePath + "\\" + strPmsID + "\\" + pmsDocuments.FileName;
                //pmsDocuments.Size = pesUpload.PostedFile.ContentLength.ToString().Trim();
                //pmsDocuments.Creator = LoginName;
                //pmsDocuments.CreateDate = dtCurDate;
                //#endregion

                //#region Upload PES File
                //if (pmsDocuments.FileName != "")
                //{
                //    strSavePath = strSavePath + "\\" + strPmsID;

                //    if (!QFile.CheckFolderExist(strSavePath))
                //    {
                //        QFile.CreateDirectory(strSavePath);
                //    }

                //    strSavePath = strSavePath + "\\" + pmsDocuments.FileName;

                //    pesUpload.PostedFile.SaveAs(strSavePath);
                //}
                #endregion
                #endregion

                #region For Insert SdpDetail
                SdpDetail sdpDetail = new SdpDetail();
                sdpDetail.Pmsid = strPmsID;
                sdpDetail.Type = dropdownlistType.SelectedValue.Trim();
                #endregion

                #region For Insert PmsFlow

                //pmsFlowTemplateList = pmsFlowTemplateBiz.SelectPmsFlowTemplateByTypeId(dropdownlistType.SelectedValue.Trim());

                PmsFlow pmsFlow = new PmsFlow();
                pmsFlow.FlowId = pmsFlowTemplateList[0].FlowId;
                pmsFlow.PmsId = strPmsID;
                pmsFlow.Creator = LoginName;
                pmsFlow.CreateDate = dtCurDate;
                #endregion

                #region For Insert PMSChangeHistory
                PmsChangeHistory pmsChangeHistory = new PmsChangeHistory();
                pmsChangeHistory.PmsId = strPmsID;
                pmsChangeHistory.ChangeContent = "";
                pmsChangeHistory.Action = "CREATE";
                pmsChangeHistory.CreateDate = dtCurDate;
                pmsChangeHistory.Creator = LoginName;
                #endregion

                #region For Insert Itarm_Cr_List
                ItarmCrList itarmCrList = new ItarmCrList();
                itarmCrList.CrId = strCrId;
                itarmCrList.CrName = textboxPmsName.Text.Trim().Replace("'", "");
                itarmCrList.Site = dropdownlistSite.SelectedValue;
                itarmCrList.Creator = LoginName;
                itarmCrList.CreateDate = dtCurDate;
                itarmCrList.Pm = textboxPM.Text.Trim().Replace("'", "");
                itarmCrList.System = textboxSystem.Text.Trim().Replace("'", "");

                #endregion

                #region Insert
                PmsHeadBiz pmsInsert = new PmsHeadBiz();
              
                string errorInfo;
                int insertResult = pmsInsert.InsertPmsHeadAndDoc(pmsHeadInsert, sdpDetail, pmsChangeHistory, pmsItarmMapping, pmsFlow, itarmCrList, null,out errorInfo);

                if (insertResult == 0)
                {
                    Msgbox("Save Fail!");
                }
                else
                {
                    #region 更新系统版本表
                    bool upVerResult =
                        m_PmsCRCreatBiz.UpdateSysVersion(dropdownlistDomain.SelectedValue, textboxSystem.Text.Trim(), dropdownlistSite.SelectedValue, textboxNewVersion.Text.Trim());

                    if (!upVerResult)
                    {
                        Msgbox("Updated system version number fail!");
                    }

                    #endregion

                    //  Session["InquiryPage_Refresh"] = "Y";
                    //发送创建成功的Mail

                    LoginName.Replace(".", " ");
                    pmsHeadInsert.UserName = LoginName;
                    new MailBiz().SendCreateMail(pmsHeadInsert);

                    Msgbox("Create Successful!");
                    PageRegisterStartupScript("window.close();");
                    
                }
                #endregion



            }
            catch (Exception exception)
            {
                Msgbox("Save Fail!");
            }

        }

        protected void dropdownlistType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (dropdownlistType.SelectedValue == PmsCommonEnum.ProjectTypeFlowId.Service.GetDescription())
            {
                labelRelatedCrNo.Visible = true;
                labelRelatedCrNoMark.Visible = true;
                textboxRelatedCrNo.Visible = true;
                labelRelatedCrNoBank.Visible = true;
            }
            else
            {
                labelRelatedCrNo.Visible = false;
                labelRelatedCrNoMark.Visible = false;
                textboxRelatedCrNo.Visible = false;
                labelRelatedCrNoBank.Visible = false;
            }
        }

        #region Check Validity
        /// <summary>
        /// 检查控件值是否为空，有任何一个为空，返回false,都不为空，返回true
        /// </summary>
        /// <returns></returns>
        private bool CheckControlValue()
        {
            try
            {
                if (!CheckControlTextBoxIsNull(textboxPmsName, "Please input CRName!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistType, "Please select Type!"))
                    return false;
                if (textboxRelatedCrNo.Visible && (!CheckControlTextBoxIsNull(textboxRelatedCrNo, "Please input  Related CR No for this service!")))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistPriority, "Please select Priority!"))
                    return false;
                if (!CheckControlDateTextBoxIsNull(dateTextBoxDueDate, "Please input DueDate!"))
                    return false;
                if (!CheckControlDateTextBoxIsNull(dateTextBoxPlanStartDate, "Please input Plan Start Date!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistDomain, "Please select Domain!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistSite, "Please select Site!"))
                    return false;
                if (!CheckControlTextBoxIsNull(textboxSystem, "Please input System!"))
                    return false;
                if (!CheckControlDropDownListIsNull(dropdownlistImpactSite, "Please select Impact Site!"))
                    return false;
                if (!CheckControlTextBoxIsNull(textboxPM, "Please input PM!"))
                    return false;
                else
                {
                    string[] pms = textboxPM.Text.Trim().Split(';');
                    foreach (string pm in pms)
                    {
                        if (pm != string.Empty && (!m_PmsCRCreatBiz.CheckUser(pm)))
                        {
                            Msgbox(pm + " Name does not exist");
                            textboxPM.Focus();
                            return false;
                        }
                    }

                }

                string message;
                string falg;
                // CheckVersion for test,remember to
                //if (!m_PmsCRCreatBiz.CheckVersion(HiddenFieldOldVersion.Value.Trim(), textboxNewVersion.Text.Trim(), out message, out falg))
                //{
                //    Msgbox(message);
                //    if (falg == "old")
                //        textboxOldVersion.Focus();
                //    else
                //        textboxNewVersion.Focus();
                //    return false;
                //}

                if (!CheckControlTextBoxIsNull(textboxDescription, "Please input Description!"))
                {
                    textboxDescription.Focus();
                    return false;
                }

                //if (DateTime.Parse(dateTextBoxPlanStartDate.Text.Trim()) < PmsSysBiz.GetDBDateTime())
                //{
                //    Msgbox("PlanStartDate must larger than CurrentDate!");
                //    //dateTextBoxPlanStartDate.Focus();
                //    return false;
                //}
                //if (DateTime.Parse(dateTextBoxDueDate.Text.Trim()) < PmsSysBiz.GetDBDateTime())
                //{
                //    Msgbox("DueDate must larger than CurrentDate!");
                //    //dateTextBoxDueDate.Focus();
                //    return false;
                //}
                if (DateTime.Parse(dateTextBoxDueDate.Text.Trim()) < DateTime.Parse(dateTextBoxPlanStartDate.Text.Trim()))
                {
                    Msgbox("DueDate must larger than PlanStartDate!");
                    //dateTextBoxDueDate.Focus();
                    return false;
                }
                return true;

            }
            catch (Exception)
            {
                Msgbox("Fill in the wrong data.Please check the data you have input!");
                return false;
            }
            finally
            {
                PmsSystemVersion pmsSystemVersion = GetVersionNewAndOld(dropdownlistDomain.SelectedValue, textboxSystem.Text.Trim(), dropdownlistSite.SelectedValue, dateTextBoxPlanStartDate.Text, dateTextBoxDueDate.Text);
                if (pmsSystemVersion != null)
                {
                    textboxOldVersion.Text = pmsSystemVersion.OldVersion;
                    //不可以给NewVersion赋值，不然会覆盖用户填写的值。
                    // textboxNewVersion.Text = pmsSystemVersion.NewVersion;
                    HiddenFieldOldVersion.Value = pmsSystemVersion.OldVersion;
                    //使用BugFreeModule栏位临时存储是否需要将NeedSTC,NeedSTP是否需要
                    if (pmsSystemVersion.BugFreeModule == "Y")
                    {
                        RadioButtonNeedSTPYes.Checked = true;
                        RadioButtonNeedSTCYes.Checked = true;
                        RadioButtonNeedSTPNo.Checked = false;
                        RadioButtonNeedSTCNo.Checked = false;
                    }
                }
                else
                {
                    textboxOldVersion.Text = "";
                    //不可以给NewVersion赋值，不然会覆盖用户填写的值。
                    // textboxNewVersion.Text = "";
                    HiddenFieldOldVersion.Value = "";
                }

            }



        }
        #endregion

        [System.Web.Services.WebMethod]
        public static PmsSystemVersion GetVersionNewAndOld(string systemDomain, string sysName, string site, string planStartDay, string dueDate)
        {
            PmsCRCreatBiz pmsCRCreatBiz = new PmsCRCreatBiz();
            IList<PmsSystemVersion> pmsSystemVersionList =
                pmsCRCreatBiz.SelectPmsSystemVersionByDomainSystem(systemDomain, sysName, site);

            if (pmsSystemVersionList != null && pmsSystemVersionList.Count > 0)
            {
                string flag = SetStcStp(pmsSystemVersionList[0].TeamDomain, planStartDay, dueDate);

                if (flag == "Y")
                {
                    //使用BugFreeModule栏位临时存储是否需要将NeedSTC,NeedSTP是否需要
                    pmsSystemVersionList[0].BugFreeModule = "Y";
                }
                return pmsSystemVersionList[0];
            }
            else
            {
                string flag = SetStcStp("", planStartDay, dueDate);

                PmsSystemVersion pmsSystemVersion = new PmsSystemVersion();

                if (flag == "Y")
                {
                    //使用BugFreeModule栏位临时存储是否需要将NeedSTC,NeedSTP是否需要
                    pmsSystemVersion.BugFreeModule = "Y";
                    pmsSystemVersion.NewVersion = "";
                    pmsSystemVersion.OldVersion = "";
                }
                else
                {
                    //使用BugFreeModule栏位临时存储是否需要将NeedSTC,NeedSTP是否需要
                    pmsSystemVersion.BugFreeModule = "N";
                    pmsSystemVersion.NewVersion = "";
                    pmsSystemVersion.OldVersion = "";
                    
                }
                return pmsSystemVersion;
                //return null;
            }

        }


        //检查是否需要将NeedSTC,NeedSTP设置为True
        [System.Web.Services.WebMethod]
        public static string SetStcStp(string teamDomain, string planStartDay, string dueDate)
        {
            if (teamDomain == "CIM")
            {
                return "Y";
            }
            else if (!string.IsNullOrEmpty(planStartDay.Trim()) && !string.IsNullOrEmpty(dueDate.Trim()))
            {
                DateTime planSDay = DateTime.Parse(planStartDay.Trim());

                DateTime dueDateTime = DateTime.Parse(dueDate.Trim());
                TimeSpan timeSpan = dueDateTime - planSDay;
                if (timeSpan.Days > 14)
                {
                    return "Y";
                }
                else
                {
                    return "N";
                }
            }
            else
            {
                return "N";
            }
        }

        protected void textboxRelatedCrNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textboxRelatedCrNo.Text.Trim()))
            {
                PmsHead pmsHead = new PmsHeadBiz().SelectSystemInforByCrId(textboxRelatedCrNo.Text.Trim());
                if (pmsHead == null)
                {
                    Msgbox("The Related CR No does not exist,Please input the right CR No!");
                    textboxRelatedCrNo.Text = "";
                    textboxRelatedCrNo.Focus();
                    return;
                }
                    
                SetDropDownListItem(dropdownlistSite, pmsHead.Site);
                SetDropDownListItem(dropdownlistDomain, pmsHead.Domain);
                SetDropDownListItem(dropdownlistImpactSite, pmsHead.ImpactSite);
                textboxSystem.Text = pmsHead.System;

                PmsSystemVersion pmsSystemVersion = GetVersionNewAndOld(dropdownlistDomain.SelectedValue, textboxSystem.Text.Trim(), dropdownlistSite.SelectedValue, dateTextBoxPlanStartDate.Text, dateTextBoxDueDate.Text);
                if (pmsSystemVersion != null)
                {
                    textboxOldVersion.Text = (pmsSystemVersion.OldVersion ?? string.Empty).Trim();
                    //不可以给NewVersion赋值，不然会覆盖用户填写的值。
                    textboxNewVersion.Text = (pmsSystemVersion.NewVersion ?? string.Empty).Trim();
                    HiddenFieldOldVersion.Value = (pmsSystemVersion.OldVersion ?? string.Empty).Trim();
                    //使用BugFreeModule栏位临时存储是否需要将NeedSTC,NeedSTP是否需要
                    if (pmsSystemVersion.BugFreeModule == "Y")
                    {
                        RadioButtonNeedSTPYes.Checked = true;
                        RadioButtonNeedSTCYes.Checked = true;
                        RadioButtonNeedSTPNo.Checked = false;
                        RadioButtonNeedSTCNo.Checked = false;
                    }
                }
                else
                {
                    textboxOldVersion.Text = "";
                    //不可以给NewVersion赋值，不然会覆盖用户填写的值。
                    // textboxNewVersion.Text = "";
                    HiddenFieldOldVersion.Value = "";
                }


            }
        }




        //protected void dateTextBoxDueDate_TextChanged(object sender, EventArgs e)
        //{
        //    PmsSystemVersion pmsSystemVersion = GetVersionNewAndOld(dropdownlistDomain.SelectedValue, textboxSystem.Text.Trim(), dropdownlistSite.SelectedValue, dateTextBoxPlanStartDate.Text, dateTextBoxDueDate.Text);
        //    if (pmsSystemVersion != null)
        //    {
        //        textboxOldVersion.Text = pmsSystemVersion.OldVersion;
        //        //不可以给NewVersion赋值，不然会覆盖用户填写的值。
        //        // textboxNewVersion.Text = pmsSystemVersion.NewVersion;
        //        HiddenFieldOldVersion.Value = pmsSystemVersion.OldVersion;
        //        //使用BugFreeModule栏位临时存储是否需要将NeedSTC,NeedSTP是否需要
        //        if (pmsSystemVersion.BugFreeModule == "Y")
        //        {
        //            RadioButtonNeedSTPYes.Checked = true;
        //            RadioButtonNeedSTCYes.Checked = true;
        //            RadioButtonNeedSTPNo.Checked = false;
        //            RadioButtonNeedSTCNo.Checked = false;
        //        }
        //    }
        //}

        //protected void dateTextBoxPlanStartDate_TextChanged(object sender, EventArgs e)
        //{
        //    PmsSystemVersion pmsSystemVersion = GetVersionNewAndOld(dropdownlistDomain.SelectedValue, textboxSystem.Text.Trim(), dropdownlistSite.SelectedValue, dateTextBoxPlanStartDate.Text, dateTextBoxDueDate.Text);
        //    if (pmsSystemVersion != null)
        //    {
        //        textboxOldVersion.Text = pmsSystemVersion.OldVersion;
        //        //不可以给NewVersion赋值，不然会覆盖用户填写的值。
        //        // textboxNewVersion.Text = pmsSystemVersion.NewVersion;
        //        HiddenFieldOldVersion.Value = pmsSystemVersion.OldVersion;
        //        //使用BugFreeModule栏位临时存储是否需要将NeedSTC,NeedSTP是否需要
        //        if (pmsSystemVersion.BugFreeModule == "Y")
        //        {
        //            RadioButtonNeedSTPYes.Checked = true;
        //            RadioButtonNeedSTCYes.Checked = true;
        //            RadioButtonNeedSTPNo.Checked = false;
        //            RadioButtonNeedSTCNo.Checked = false;
        //        }
        //    }

        //}







    }
}
