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
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using PMS.PMS.AppCode;

namespace PMS.PMS.Maintain
{
    public partial class CRNoUpdate : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {

            CRNoUpdateBiz cRNoUpdateBiz = new CRNoUpdateBiz();
            string tempCRNo = Server.HtmlDecode(TextBoxTempCrNo.Text).Trim();
            string infor = string.Empty;
            //check 临时CR No存不存在，不存在给出提示信息并返回
            if (!cRNoUpdateBiz.CheckCRNoExist(tempCRNo, out infor))
            {
                Msgbox(infor);
                return;
            }
            // 查正式的CR No是否有对应的PMSID
            string formalCRNo = Server.HtmlDecode(TextBoxFormalCrNo.Text).Trim();
            if (!cRNoUpdateBiz.CheckPmsIdExist(formalCRNo, out infor))
            {
                Msgbox(infor);
                return;
            }

            string currentUser = WSC.GlobalDefinition.Cookie_LoginUser.Replace(" ", ".");
            //update 正式的CR No
            if (!cRNoUpdateBiz.UpdateTempCrNoToFormalCrNo(formalCRNo, tempCRNo, currentUser, out infor))
            {
                Msgbox(infor);
                return;
            }
            Msgbox("Save sucessfully!");

        }
    }
}
