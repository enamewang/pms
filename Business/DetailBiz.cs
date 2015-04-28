using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class DetailBiz : BaseBusiness
    {
        #region Get CrID
        public bool GetCrID(string pmsId, out string crId)
        {
            crId = "";
            try
            {
                PmsItarmMappingBiz pmsItarmMappingBiz = new PmsItarmMappingBiz();
                IList<PmsItarmMapping> pmsItarmMappingList = pmsItarmMappingBiz.SelectPmsItarmMapping(null, pmsId);

                if (pmsItarmMappingList != null && pmsItarmMappingList.Count > 0)
                {
                    crId = pmsItarmMappingList[0].CrId.Trim();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
