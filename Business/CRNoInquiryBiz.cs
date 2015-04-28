using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
   public class CRNoInquiryBiz:BaseBusiness
    {
       public IList<ItarmCrList> SelectCrIdNamePmSystemSite(string crId,string crName,string pm,string systemName)
       {
           try
           {
               Hashtable hashtable=new Hashtable();
               hashtable.Add("CrId",crId); 
               hashtable.Add("CrName",crName);
               hashtable.Add("Pm",pm);
               hashtable.Add("System",systemName);
               return m_PMSSqlConnection.QueryForList<ItarmCrList>("SelectCrIdNamePmSystemSite", hashtable);
           }
           catch (Exception)
           {
               return null;
           }
       }
    }
}
