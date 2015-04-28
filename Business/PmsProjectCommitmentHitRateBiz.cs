using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class PmsProjectCommitmentHitRateBiz : BaseBusiness
    {
        public IList<PmsProjectCommitmentHitRate> GetProjectCommitmentHitRatePmsHead(PmsProjectCommitmentHitRate pmsProjectCommitmentHitRate)
        {
            try
            {
                if (pmsProjectCommitmentHitRate.UserDept.Contains("AIC"))
                {
                    pmsProjectCommitmentHitRate.UserDept = "AIC%";
                }

                return m_PMSSqlConnection.QueryForList<PmsProjectCommitmentHitRate>("SelecProjectCommitmentHitRatePmsHead", pmsProjectCommitmentHitRate);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsRequirementBiz/SelectPmsRequirement:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsProjectCommitmentHitRate> GetPmsProjectCommitmentHitRateList(PmsProjectCommitmentHitRate pmsProjectCommitmentHitRate)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsProjectCommitmentHitRate>("SelectPmsProjectCommitmentHitRate", pmsProjectCommitmentHitRate);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsRequirementBiz/SelectPmsRequirement:" + ex.ToString());
                return null;
            }
        }

        public bool InsertPmsProjectCommitmentHitRate(IList<PmsProjectCommitmentHitRate> listPmsProjectCommitmentHitRate)
        {
            try
            {
                m_PMSSqlConnection.BeginTransaction();
                if (listPmsProjectCommitmentHitRate.Count > 0)
                {
                    PmsProjectCommitmentHitRate pmsProjectCommitmentHitRate = listPmsProjectCommitmentHitRate.FirstOrDefault();
                    m_PMSSqlConnection.Delete("DeletePmsProjectCommitmentHitRate", pmsProjectCommitmentHitRate);
                }
                foreach (var pmsProjectCommitmentHitRate in listPmsProjectCommitmentHitRate)
                {
                    if (pmsProjectCommitmentHitRate.UserDept == null)
                    {
                        pmsProjectCommitmentHitRate.UserDept = "";
                    }
                    m_PMSSqlConnection.Insert("InsertPmsProjectCommitmentHitRate", pmsProjectCommitmentHitRate);
                }
                m_PMSSqlConnection.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                m_PMSSqlConnection.RollBackTransaction();
                m_Logger.Error("PmsRequirementBiz/InsertPmsRequirement:" + ex.ToString());
                return false;
            }
        }

    }
}
