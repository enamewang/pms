using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;


namespace Qisda.PMS.Business
{
    public class PmsDevelopmentProcessMeasurementBiz : BaseBusiness
    {
        public bool InsertPmsDevelopmentProcessMeasurement(PmsDevelopmentProcessMeasurement pmsDevelopmentProcessMeasurement)
        {
            try
            {
                m_PMSSqlConnection.Insert("InsertPmsDevelopmentProcessMeasurement", pmsDevelopmentProcessMeasurement);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDevelopmentProcessMeasurementBiz/InsertPmsDevelopmentProcessMeasurement:" + ex.ToString());
                return false;
            }
        }

        public bool DeletePmsDevelopmentProcessMeasurementAll()
        {
            try
            {
                object obj = new object();
                m_PMSSqlConnection.Delete("DeletePmsDevelopmentProcessMeasurementAll", obj);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDevelopmentProcessMeasurementBiz/DeletePmsDevelopmentProcessMeasurementAll:" + ex.ToString());
                return false;
            }
        }
    }
}
