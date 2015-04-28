using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;


namespace Qisda.PMS.Business
{
    public class PmsRequirementBiz : BaseBusiness
    {
        public IList<PmsRequirement> GetPmsRequirementByWeekPeriod(PmsRequirement pmsRequirement)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsRequirement>("SelectPmsRequirement", pmsRequirement);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsRequirementBiz/SelectPmsRequirement:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsRequirement> GetPmsRequirement(PmsRequirement pmsRequirement)
        {
            try
            {
                pmsRequirement = SetPmsRequirementStatus(pmsRequirement);
                if (pmsRequirement.Status == "Released")
                {
                    return GetPmsHeadReleased(pmsRequirement);
                }
                return m_PMSSqlConnection.QueryForList<PmsRequirement>("SelectPmsRequirement", pmsRequirement);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsRequirementBiz/SelectPmsRequirement:" + ex.ToString());
                return null;
            }
        }
        private PmsRequirement SetPmsRequirementStatus(PmsRequirement pmsRequirement)
        {
            if (pmsRequirement == null || pmsRequirement.Status == null)
            {
                return new PmsRequirement();
            }
            switch (pmsRequirement.Status.Trim().ToUpper())
            {
                case "NEW":
                    pmsRequirement.Status = "Scheduled";
                    break;
                case "ON-GOING":
                    pmsRequirement.Status = "Ongoing";
                    break;
                case "QUEUE":
                    pmsRequirement.Status = "Deferred";
                    break;
                default: break;
            }
            return pmsRequirement;
        }
        public IList<PmsRequirement> GetPmsHeadReleased(PmsRequirement pmsRequirement)
        {
            try
            {
                if (pmsRequirement.UserDept.Contains("AIC"))
                {
                    pmsRequirement.UserDept = "AIC%";
                }
                return m_PMSSqlConnection.QueryForList<PmsRequirement>("SelectPmsHeadReleased", pmsRequirement);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsRequirementBiz/SelectPmsRequirement:" + ex.ToString());
                return null;
            }
        }
        public bool InsertPmsRequirement(IList<PmsRequirement> listPmsRequirement, IList<PmsHeadCount> listPmsHeadCount, IList<PmsHeadCountByContent> listPmsHeadCountByContent)
        {
            try
            {
                m_PMSSqlConnection.BeginTransaction();
                if (listPmsRequirement.Count > 0)
                {
                    PmsRequirement pmsRequirement = listPmsRequirement.FirstOrDefault();
                    m_PMSSqlConnection.Delete("DeletePmsRequirement", pmsRequirement);
                }
                if (listPmsHeadCount.Count > 0)
                {
                    PmsHeadCount pmsHeadCount = listPmsHeadCount.FirstOrDefault();
                    m_PMSSqlConnection.Delete("DeletePmsHeadCount", pmsHeadCount);
                }
                if (listPmsHeadCountByContent.Count > 0)
                {
                    PmsHeadCountByContent pmsHeadCountByContent = listPmsHeadCountByContent.FirstOrDefault();
                    m_PMSSqlConnection.Delete("DeletePmsHeadCountByContent", pmsHeadCountByContent);
                }
                foreach (var pmsRequirement in listPmsRequirement)
                {
                    m_PMSSqlConnection.Insert("InsertPmsRequirement", pmsRequirement);
                }
                foreach (var pmsHeadCount in listPmsHeadCount)
                {
                    m_PMSSqlConnection.Insert("InsertPmsHeadCount", pmsHeadCount);
                }
                foreach (var pmsHeadCountByContent in listPmsHeadCountByContent)
                {
                    m_PMSSqlConnection.Insert("InsertPmsHeadCountByContent", pmsHeadCountByContent);
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

        public bool DeletePmsRequirement(PmsRequirement pmsRequirement)
        {
            bool result = false;
            try
            {
                m_PMSSqlConnection.Update("UpdatePmsRequirementInvalid", pmsRequirement);
                result = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsRequirementBiz/UpdatePmsRequirement:" + ex.ToString());
            }
            return result;
        }

        public bool UpdatePmsRequirement(PmsRequirement pmsRequirement)
        {
            bool result = false;
            try
            {
                m_PMSSqlConnection.Update("UpdatePmsRequirement", pmsRequirement);
                result = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsRequirementBiz/UpdatePmsRequirement:" + ex.ToString());
            }
            return result;
        }
    }
}
