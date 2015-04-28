using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Qisda.PMS.Entity;
using System.Collections.Generic;


namespace PMS.PMS.UserControl
{
    public partial class MyTaskDataList : System.Web.UI.UserControl,INamingContainer, IRepeatInfoUser
    {
        private IList<MyTaskCondition> m_DataSource;

        public IList<MyTaskCondition> DataSource
        {
            get { return m_DataSource; }
            set { m_DataSource = value; }
        }
	
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindDataList()
        {
            DataList.DataSource = m_DataSource;
            DataList.DataBind();
        }

        #region IRepeatInfoUser Members

        public Style GetItemStyle(ListItemType itemType, int repeatIndex)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool HasFooter
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool HasHeader
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool HasSeparators
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int RepeatedItemCount
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
}