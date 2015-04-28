using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class PmsFlowTemplateBiz : BaseBusiness
    {
        public IList<PmsFlowTemplate> SelectPmsFlowTemplateByTypeId(string typeId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsFlowTemplate>("SelectPmsFlowTemplateByTypeId", typeId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsFlowTemplateBiz/SelectPmsFlowTemplateByTypeId:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsFlowTemplate> SelectPmsFlowTemplateType()
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsFlowTemplate>("SelectPmsFlowTemplateType", null);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsFlowTemplateBiz/SelectPmsFlowTemplateType:" + ex.ToString());
                return null;
            }
        }
    }
}
