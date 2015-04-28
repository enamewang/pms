using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
   public class PmsMinconclutionBiz:BaseBusiness
    {
       public IList<PmsMinconclution> SelectPmsMinconclutionByMinId(string minId)
       {
           try
           {
               return m_PMSSqlConnection.QueryForList<PmsMinconclution>("SelectPmsMinconclution", minId);
           }
           catch (Exception ex)
           {
               m_Logger.Error("PmsMinconclutionBiz/SelectPmsMinconclution:" + ex.ToString());
               return null;
           }
       }

       public int InsertPmsMinconclutionByMinId(PmsMinconclution pmsMinconclution)
       {
           int returnSerial = 0;
           try
           {
               returnSerial = (int)m_PMSSqlConnection.Insert("InsertPmsMinconclution", pmsMinconclution);
               return returnSerial;
           }
           catch (Exception ex)
           {
               m_Logger.Error("PmsMinconclutionBiz/InsertPmsMinconclution:" + ex.ToString());
               return returnSerial;
           }
       }

       public int DeletePmsMinconclutionBySerial(string serial)
       {
           int returnSerial = 0;
           try
           {
               returnSerial = (int)m_PMSSqlConnection.Delete("DeletePmsMinconclutionBySerial", serial);
               return returnSerial;
           }
           catch (Exception ex)
           {
               m_Logger.Error("PmsMinconclutionBiz/DeletePmsMinconclutionBySerial:" + ex.ToString());
               return returnSerial;
           }
       }

       public int UpdatePmsMinconclutionBySerial(PmsMinconclution pmsMinconclution)
       {
           int returnSerial = 0;
           try
           {
               returnSerial = (int)m_PMSSqlConnection.Update("UpdatePmsMinconclution", pmsMinconclution);
               return returnSerial;
           }
           catch (Exception ex)
           {
               m_Logger.Error("PmsMinconclutionBiz/UpdatePmsMinconclutionBySerial:" + ex.ToString());
               return returnSerial;
           }
       }
    }
}
