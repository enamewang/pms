using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class PmsFlowBiz :BaseBusiness
    {
        public IList<PmsFlow> InsertPmsFlow(PmsFlow pmsFlow)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsFlow>("InsertPmsFlow", pmsFlow);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsFlowBiz/InsertPmsFlow:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsStage> SelectPmsFlowByPmsId(string pmsId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsStage>("SelectPmsFlowByPmsId", pmsId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsFlowBiz/SelectPmsFlowByPmsId:" + ex.ToString());
                return null;
            }
        }
    }
}
