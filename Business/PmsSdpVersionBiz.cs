using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class PmsSdpVersionBiz : BaseBusiness
    {
        public bool InsertPmsSdpVersion(PmsSdpVersion pmsSdpVersion)
        {
            try
            {
                m_PMSSqlConnection.Insert("InsertPmsSdpVersion", pmsSdpVersion);                
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSdpVersionBiz/InsertPmsSdpVersion:" + ex.ToString());
                return false;
            }
        }

        public IList<PmsSdpVersion> SelectPmsSdpVersionByTaskno(string taskno)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Taskno", taskno } };
                return m_PMSSqlConnection.QueryForList<PmsSdpVersion>("SelectPmsSdpVersionByTaskno", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSdpVersionBiz/SelectPmsSdpVersionByTaskno:" + ex.ToString());
                return null;
            }
        }

        public bool UpdatePmsSdpVersionByTaskno(PmsSdpVersion pmsSdpVersion)
        {
            bool updateResult = false;
            try
            {                
                m_PMSSqlConnection.Update("UpdatePmsSdpVersionByTaskno", pmsSdpVersion);
                updateResult = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/UpdatePmsSdpVersionByTaskno:" + ex.ToString());
            }
            return updateResult;
        }            
    }
}
