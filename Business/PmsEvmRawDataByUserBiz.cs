using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class PmsEvmRawDataByUserBiz:BaseBusiness
    {
        public IList<PmsEvmRawDataByUser> SelectPmsEvmRawDataByUser(PmsEvmRawDataByUser pmsEvmRawDataByUser)
        {
            try
            {
                IList<PmsEvmRawDataByUser> result =
                 m_PMSSqlConnection.QueryForList<PmsEvmRawDataByUser>("SelectPmsEvmRawDataByUser", pmsEvmRawDataByUser);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsEvmRawDataByUserBiz/SelectPmsEvmRawDataByUser:" + ex.ToString());
                return null;
            }
        }

    }
}
