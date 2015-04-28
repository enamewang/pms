using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;
using System.Collections;

namespace Qisda.PMS.Business
{
    public class PmsHeadHBiz : BaseBusiness
    {
        public IList<PmsHeadH> SelectPmsHeadHByPmsId(string pmsId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsHeadH>("SelectPmsHeadH", pmsId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectPmsHeadByPmsId:" + ex.ToString());
                return null;
            }
        }

        public bool InsertPmsHeadH(PmsHeadH pmsHeadH)
        {
            bool result = false;

            try
            {
                m_PMSSqlConnection.Insert("InsertPmsHeadH", pmsHeadH);
                result = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadHBiz/InsertPmsHeadH" + ex.Message.ToString());
            }

            return result;
        }


        public bool UpdatePmsHeadH(PmsHeadH pmsHeadH)
        {
            bool result = false;

            try
            {
                m_PMSSqlConnection.Update("UpdatePmsHeadH", pmsHeadH);
                result = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadHBiz/UpdatePmsHeadH" + ex.Message.ToString());
            }

            return result;
        }

    }
}
