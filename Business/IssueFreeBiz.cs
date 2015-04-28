using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;
using System.Collections;


namespace Qisda.PMS.Business
{
    public class IssueFreeBiz:BaseBusiness
    {
        public IList<BfIssueinfo> GetIssueinfo(string crId,string mnId)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("CrId", crId);
                hashtable.Add("MnId", mnId);
                IList<BfIssueinfo> result = m_BugFreeSqlConnection.QueryForList<BfIssueinfo>("SelectBfIssueinfo", hashtable);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("BugInquiryBiz/SelectBfBugInfoPart:" + ex.ToString());
                return null;
            }
        }
    }
}
