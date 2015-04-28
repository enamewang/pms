using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PMS.PMS.AppCode;
using PMS.PMS.Maintain;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;

namespace PMS.PMS.UserControls
{
    /// <summary>
    /// 继承于本类的自定义用户控件，必须放在ProjectsInformation.aspx页面上。
    /// </summary>
    public class ProjectsInformationUserControlBase : System.Web.UI.UserControl
    {
        protected readonly BasicInformationDetailBiz m_BasicInformationDetailBiz = new BasicInformationDetailBiz();

        protected readonly PmsCommonBiz m_PmsCommonBiz = new PmsCommonBiz();        

        public ProjectsInformation PageProjectsInformation
        {
            get
            {
                var page = this.Page as ProjectsInformation;
                if (page == null)
                {
                    throw new Exception("This Page Must Be ProjectsInformation!");
                }
                return page;
            }
        }
      
        public string PmsID
        {
            get
            {
                return PageProjectsInformation.PmsID;
            }
        }

        public string CrId
        {
            get
            {
                return PageProjectsInformation.CrId;
            }
            set
            {
                PageProjectsInformation.CrId = value;
            }
        }

        public string LoginName
        {
            get
            {
                return PageProjectsInformation.LoginName;
            }
        }

        public int Stage
        {
            get
            {
                return PageProjectsInformation.Stage;
            }
            set
            {
                PageProjectsInformation.Stage = value;
            }
        }

        public BaseDataUser CurrentUser
        {
            get
            {
                return PageProjectsInformation.CurrentUser;
            }
            set
            {
                PageProjectsInformation.CurrentUser=value;
            }
        }

        public IList<PmsFlowTemplate> ProjectTypeStageList
        {
            get
            {
                return PageProjectsInformation.ProjectTypeStageList;
            }            
        }
       
        public PmsHead InitPmsHead
        {
            get
            {
                return PageProjectsInformation.InitPmsHead;
            }
            set { PageProjectsInformation.InitPmsHead = value; }
        }
      
        public string ProjectType
        {
            get
            {
                return PageProjectsInformation.ProjectType;
            }
            set { PageProjectsInformation.ProjectType = value; }
        }

        public void PageRegisterStartupScript(string js)
        {
            var page = this.Page as PageBase;
            if (page != null)
            {
                page.PageRegisterStartupScript(js);
            }
        }

        public void Msgbox(string message)
        {
            var page = this.Page as PageBase;
            if (page != null)
            {
                page.Msgbox(message);
            }
        }

        public void SetDropDownListItem(DropDownList p_DropDownList1, string p_ItemValue)
        {
            PageBase.SetDropDownListItem(p_DropDownList1, p_ItemValue);
        }

        /// <summary>
        /// 判断TextBox控件值是否为空，如果为空，返回false,不为空，返回true
        /// </summary>
        /// <param name="textBox">待检查的TextBox控件</param>
        /// <param name="message">控件值为空时的提示信息</param>
        /// <returns></returns>
        public bool CheckControlTextBoxIsNull(TextBox textBox, string message)
        {
            if (textBox.Text.Trim() == "")
            {
                Msgbox(message);
                textBox.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断DropDownList控件值是否为空，如果为空，返回false,不为空，返回true
        /// </summary>
        /// <param name="dropDownList">待检查的DropDownList控件</param>
        /// <param name="message">控件值为空时的提示信息</param>
        /// <returns></returns>
        public bool CheckControlDropDownListIsNull(DropDownList dropDownList, string message)
        {
            if (string.IsNullOrEmpty(dropDownList.SelectedValue))
            {
                Msgbox(message);
                dropDownList.Focus();
                return false;
            }
            return true;
        }
    }
}
