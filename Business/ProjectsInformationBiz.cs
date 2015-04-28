using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class ProjectsInformationBiz : BaseBusiness
    {
        //获取当前登录者的信息
        public bool GetCurrentUser(ref BaseDataUser currentUser, string pmsId, string loginName)
        {
            try
            {
                currentUser.LoginName = loginName;

                BaseDataUserBiz baseDataUserBiz = new BaseDataUserBiz();
                IList<BaseDataUser> baseDataUserList = baseDataUserBiz.SelectBaseDataUser(loginName, null);

                if (baseDataUserList != null || baseDataUserList.Count > 0)
                {
                    currentUser = baseDataUserList[0];
                }
                currentUser = baseDataUserBiz.SetUserOrgRole(currentUser);
                currentUser = baseDataUserBiz.SetUserProjectRole(currentUser, pmsId);
                return true;
            }
            catch
            {
                return false;
                
            }
        }

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
