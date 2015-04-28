using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS.PMS.UserControls
{
    public partial class ListBoxSelect : System.Web.UI.UserControl
    {
        #region  Property and Field
        protected JavaScriptSerializer m_JavaScriptSerializer = new JavaScriptSerializer();

        public char SplitCode
        {
            get
            {
                object o = ViewState["SplitCode"];
                return o == null ? ';' : (char)o;
            }
            set
            {
                ViewState["SplitCode"] = value;
            }
        }

        public bool Enabled
        {
            set
            {
                this.lstLeft.Enabled = value;
                this.lstRight.Enabled = value;
                this.dblArrowRight.Enabled = value;
                this.ArrowRight.Enabled = value;
                this.ArrowLeft.Enabled = value;
                this.dblArrowLeft.Enabled = value;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public bool Sort
        {
            get
            {
                string sort = HiddenFieldSort.Value ?? string.Empty;
                if (string.IsNullOrEmpty(sort))
                {
                    return true;
                }

                bool result;
                return bool.TryParse(sort, out result) && result;
            }
            set
            {
                HiddenFieldSort.Value = value.ToString();
            }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public string Width
        {
            get
            {
                return divSelectExt.Style["width"];
            }
            set
            {
                divSelectExt.Style["width"] = value;
            }
        }

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 绑定OnClientClick属性
                dblArrowRight.OnClientClick = string.Format("return dblArrowRight_OnClientClick('{0}','{1}','{2}','{3}',{4});", lstLeft.ClientID, lstRight.ClientID, HiddenFieldSelected.ClientID, HiddenFieldUnSelected.ClientID, Sort.ToString().ToLower());
                ArrowRight.OnClientClick = string.Format("return ArrowRight_OnClientClick('{0}','{1}','{2}','{3}',{4});", lstLeft.ClientID, lstRight.ClientID, HiddenFieldSelected.ClientID, HiddenFieldUnSelected.ClientID, Sort.ToString().ToLower());
                ArrowLeft.OnClientClick = string.Format("return ArrowLeft_OnClientClick('{0}','{1}','{2}','{3}',{4});", lstLeft.ClientID, lstRight.ClientID, HiddenFieldSelected.ClientID, HiddenFieldUnSelected.ClientID, Sort.ToString().ToLower());
                dblArrowLeft.OnClientClick = string.Format("return dblArrowLeft_OnClientClick('{0}','{1}','{2}','{3}',{4});", lstLeft.ClientID, lstRight.ClientID, HiddenFieldSelected.ClientID, HiddenFieldUnSelected.ClientID, Sort.ToString().ToLower());
                #endregion
            }
            else
            {
                //注意DateBind()函数中的代码
                string pageLoadJs = string.Format("listBoxSelectExt_PageLoad('{0}','{1}','{2}','{3}',{4});", lstLeft.ClientID, lstRight.ClientID, HiddenFieldSelected.ClientID, HiddenFieldUnSelected.ClientID, Sort.ToString().ToLower());
                PageRegisterStartupScript(pageLoadJs);
            }
        }
        #endregion

        #region 绑定数据源

        private object m_dataSource;
        public object DataSource
        {
            get
            {
                return this.m_dataSource;
            }
            set
            {
                if (((value != null) && !(value is IListSource)) && !(value is IEnumerable))
                {
                    throw new ArgumentException("Invalid DataSource Type");
                }
                this.m_dataSource = value;
            }
        }

        public string DataTextField
        {
            get
            {
                object obj = this.ViewState["DataTextField"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DataTextField"] = value;
            }
        }

        public string DataValueField
        {
            get
            {
                object obj = this.ViewState["DataValueField"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DataValueField"] = value;
            }
        }

        public override void DataBind()
        {
            this.lstLeft.Items.Clear();
            this.lstRight.Items.Clear();

            this.lstLeft.DataSource = DataSource;
            this.lstLeft.DataTextField = DataTextField;
            this.lstLeft.DataValueField = DataValueField;
            this.lstLeft.DataBind();

            if (lstLeft.Items.Count > 0)
            {
                var list = this.lstLeft.Items.Cast<ListItem>().Select(t => new { t.Value, t.Text }).ToList();

                if (Sort)
                {
                    list = list.OrderBy(t => t.Text).ToList();
                    this.lstLeft.DataSource = list;
                    this.lstLeft.DataTextField = "Text";
                    this.lstLeft.DataValueField = "Value";
                    this.lstLeft.DataBind();
                }

                #region 设置初始值
                string serializerlist = m_JavaScriptSerializer.Serialize(list);

                HiddenFieldUnSelected.Value = serializerlist;
                HiddenFieldSelected.Value = string.Empty;
                #endregion
            }
            else
            {
                HiddenFieldUnSelected.Value = string.Empty;
                HiddenFieldSelected.Value = string.Empty;
            }
        }

        /// <summary>
        /// 返回以SplitCode=";"间隔的字符串(字符串最后面也有SplitCode=";")
        /// 赋值时，最后一个一定是以SplitCode结尾
        /// </summary>
        public string SelectedValue
        {
            get
            {
                string selected = (HiddenFieldSelected.Value ?? string.Empty).Trim();
                if (string.IsNullOrEmpty(selected))
                {
                    return string.Empty;
                }

                IList<ListItem> listSelected = m_JavaScriptSerializer.Deserialize<List<ListItem>>(selected);

                string selecteds = listSelected.Aggregate(string.Empty, (current, listItem) => current + (listItem.Value + SplitCode));
                if (!string.IsNullOrEmpty(selecteds))
                {
                    selecteds = selecteds.TrimEnd(SplitCode);
                }

                return selecteds;
            }
            set
            {
                #region 如果没有绑定数据，就抛出异常

                string selected = (HiddenFieldSelected.Value ?? string.Empty).Trim();
                string unselected = (HiddenFieldUnSelected.Value ?? string.Empty).Trim();

                if (string.IsNullOrEmpty(selected) && string.IsNullOrEmpty(unselected))
                {
                    throw new Exception("No data bind!");
                }
                #endregion


                //#region 如果给SelectedValue
                IList<ListItem> listSelected = m_JavaScriptSerializer.Deserialize<List<ListItem>>(selected);
                IList<ListItem> listUnselected = m_JavaScriptSerializer.Deserialize<List<ListItem>>(unselected);

                //当listSelected或者listUnselected为null时，listSelected.Concat(listUnselected)会抛出异常
                listSelected = listSelected ?? new List<ListItem>();
                listUnselected = listUnselected ?? new List<ListItem>();

                var list1 = listSelected.Concat(listUnselected);
                var list = list1.Select(t => new { t.Value, t.Text });

                if (Sort)
                {
                    list = list.OrderBy(t => t.Text);
                }

                this.lstLeft.Items.Clear();
                this.lstRight.Items.Clear();

                if (string.IsNullOrEmpty(value))
                {
                    this.lstLeft.DataSource = list;
                    this.lstLeft.DataTextField = "Text";
                    this.lstLeft.DataValueField = "Value";
                    this.lstLeft.DataBind();

                    HiddenFieldUnSelected.Value = m_JavaScriptSerializer.Serialize(list);
                    HiddenFieldSelected.Value = string.Empty;

                }
                else
                {
                    var selecteds = value.Split(new[] { SplitCode }, StringSplitOptions.RemoveEmptyEntries);
                    var listLeft = list.Where(t => selecteds.Count(s => s == t.Value) == 0).OrderBy(t => t.Text);
                    var listRight = list.Where(t => selecteds.Count(s => s == t.Value) > 0).OrderBy(t => t.Text);

                    this.lstLeft.DataSource = listLeft;
                    this.lstLeft.DataTextField = "Text";
                    this.lstLeft.DataValueField = "Value";
                    this.lstLeft.DataBind();

                    this.lstRight.DataSource = listRight;
                    this.lstRight.DataTextField = "Text";
                    this.lstRight.DataValueField = "Value";
                    this.lstRight.DataBind();

                    HiddenFieldUnSelected.Value = m_JavaScriptSerializer.Serialize(listLeft);
                    HiddenFieldSelected.Value = m_JavaScriptSerializer.Serialize(listRight);
                }
            }
        }

        /// <summary>
        /// 返回以SplitCode=";"间隔的字符串(字符串最后面也有SplitCode=";")
        /// </summary>
        public string SelectedText
        {
            get
            {
                string selected = (HiddenFieldSelected.Value ?? string.Empty).Trim();
                if (string.IsNullOrEmpty(selected))
                {
                    return string.Empty;
                }

                IList<ListItem> listSelected = m_JavaScriptSerializer.Deserialize<List<ListItem>>(selected);

                string selecteds = listSelected.Aggregate(string.Empty, (current, listItem) => current + (listItem.Text + SplitCode));
                if (!string.IsNullOrEmpty(selecteds))
                {
                    selecteds = selecteds.TrimEnd(SplitCode);
                }
                return selecteds;
            }
        }

        #endregion

        protected void PageRegisterStartupScript(string js)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), Guid.NewGuid().ToString(), js, true);
        }
    }
}