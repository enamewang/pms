using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;


namespace PMS.PMS.AppCode
{
    public class PageBase : WSC.FramePage // //// MsdnPage
    {
        // protected SQLHelper sqlHelper = new SQLHelper(WSC.Common.Security.DecryptInner(ConfigurationManager.AppSettings["wscConnectionString"]));

        //public void Msgbox(string message)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), Guid.NewGuid().ToString(), "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>", false);

        //}

        //#region Ajax开启时使用的MessageBox

        //public void PageRegisterStartupScript(string js)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, typeof(Page), Guid.NewGuid().ToString(), "<script type='text/javascript' language='javascript'>" + js + "</script>", false);
        //}

        public void Msgbox(string message)
        {
            string message2 = message.Replace("\\n", "\n").Replace("\\\n", "\n").Replace("\\\\n", "\n").Replace("\\\\\n", "\n").Replace("\\\\\\n", "\n")
            .Replace("\\r", "\r").Replace("\\\r", "\r").Replace("\\\\r", "\r").Replace("\\\\\r", "\r").Replace("\\\\\\r", "\r")
            .Replace("\\f", "\f").Replace("\\\f", "\f").Replace("\\\\f", "\f").Replace("\\\\\f", "\f").Replace("\\\\\\f", "\f")
            .Replace("\\b", "\b").Replace("\\\b", "\b").Replace("\\\\b", "\b").Replace("\\\\\b", "\b").Replace("\\\\\\b", "\b")
            .Replace("\\t", "\t").Replace("\\\t", "\t").Replace("\\\\t", "\t").Replace("\\\\\t", "\t").Replace("\\\\\\t", "\t")
            .Replace("\n", "\\n").Replace("\r", "\\r").Replace("\b", "\\b").Replace("\f", "\\f").Replace("\t", "\\t")
            .Replace("\"", "'");

            message2 = Server.HtmlEncode(message2);
            PageRegisterStartupScript("alert('" + message2 + "');");
        }
        //#endregion

        public static void SetDropDownListItem(DropDownList p_DropDownList1, string p_ItemValue)
        {
            p_ItemValue = string.IsNullOrEmpty(p_ItemValue) ? "" : p_ItemValue.Trim();
            var item = p_DropDownList1.Items.FindByValue(p_ItemValue);
            if (item == null)
            {
                item = new ListItem(p_ItemValue);
                p_DropDownList1.Items.Add(item);
            }
            p_DropDownList1.ClearSelection();
            item.Selected = true;
        }

        //public static void SetDropDownListItem(DropDownList p_DropDownList1, string p_ItemValue, string p_ItemText)
        //{
        //    p_ItemValue = string.IsNullOrEmpty(p_ItemValue) ? "" : p_ItemValue.Trim();
        //    var item = p_DropDownList1.Items.FindByValue(p_ItemValue);
        //    if (item == null)
        //    {
        //        item = new ListItem(p_ItemText, p_ItemValue);
        //        p_DropDownList1.Items.Add(item);
        //    }
        //    p_DropDownList1.ClearSelection();
        //    item.Selected = true;
        //}


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

        public bool CheckControlDateTextBoxIsNull(TextBox textBox, string message)
        {
            if (textBox.Text.Trim() == "")
            {
                Msgbox(message);
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



        public void PageRegisterStartupScript(string js)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), Guid.NewGuid().ToString(), "<script type='text/javascript' language='javascript'>" + js + "</script>", false);
        }














        // //识别是F5刷新还是点击按钮提交
        // private static string MaskKey = "____MASKKEY"; //注册全局的MaskKey

        // private void InitialMask()
        // {
        //     this.Session[MaskKey] = this.ViewState[MaskKey] = Guid.NewGuid().ToString();//重新发号 给Session 和 ViewState 新的Guid Session在服务器端标志 ViewState放在客户端标志
        // }
        // protected override void OnPreRender(EventArgs e)
        // {
        //     base.OnPreRender(e);
        //     InitialMask();//发号
        // }
        // public bool IsRefresh
        // {
        //     get
        //     {
        //         if (this.Session[MaskKey] == null || this.ViewState[MaskKey] == null) return false;
        //         return !this.Session[MaskKey].ToString().Equals(this.ViewState[MaskKey].ToString());//检查Key是否相同
        //     }
        // }
        // //protected void btnTest_Click(object sender, EventArgs e)
        // //{
        // //    this.lblGuid.Text = this.Session[MaskKey].ToString() + " " + this.ViewState[MaskKey].ToString();//打印Key
        // //}
        // //private void ShowStatus()
        // //{
        // //    if (this.IsRefresh)
        // //    {
        // //        this.lblShow.Text = "F5 Refresh";
        // //    }
        // //    else
        // //    {
        // //        this.lblShow.Text = "Html Dom submit";
        // //    }
        // //}
        ////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Bind Data DropDown
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="dataList"></param>
        /// <param name="isALL"></param>
        public void DropDownListDataBind(System.Web.UI.WebControls.DropDownList ddl, IList<string> dataList, bool isEmpty)
        {
            if (ddl == null) throw new ArgumentNullException("ddl");

            ddl.Items.Clear();

            if (isEmpty)
            {
                ddl.Items.Add("");
            }

            if (dataList.Count > 0)
            {
                foreach (string data in dataList)
                {
                    if (!string.IsNullOrEmpty(data))
                    {
                        ddl.Items.Add(data.Trim());
                    }
                }
            }
        }





















    }
}
