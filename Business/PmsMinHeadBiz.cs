using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class PmsMinHeadBiz : BaseBusiness
    {
        public IList<PmsMinHead> SelectPmsMinHeadByPmsId(string pmsId)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("PmsId", pmsId);
                return m_PMSSqlConnection.QueryForList<PmsMinHead>("SelectPmsMinHead", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsMinHeadBiz/SelectPmsMinHeadByPmsId:" + ex.ToString());
                return null;
            }
        }

        public void GetMinId(out string minId)
        {
            DateTime createDateTime = PmsSysBiz.GetDBDateTime();
            string creatDate = PmsCommonBiz.FormatDate(createDateTime, "yyyyMMdd");

            //获取minId
            string tempMinIdPart = "MN" + creatDate;

            IList<PmsMinHead> pmsMinHeadList = SelectPmsMinHeadByMinIdPart(tempMinIdPart);

            if (pmsMinHeadList != null && pmsMinHeadList.Count > 0)
            {
                //取最大的记录加1
                int maxCrIdNum = int.Parse(pmsMinHeadList[pmsMinHeadList.Count - 1].Mnid.Substring(9, 4));
                int tempCrIdNum;
                for (int j = 0; j < pmsMinHeadList.Count; j++)
                {
                    tempCrIdNum = int.Parse(pmsMinHeadList[j].Mnid.Substring(9, 4));
                    if (maxCrIdNum < tempCrIdNum)
                    {
                        maxCrIdNum = tempCrIdNum;
                    }
                }
                maxCrIdNum = maxCrIdNum + 1;

                string crIdNum = maxCrIdNum.ToString();

                //不足四位补“0”
                while (crIdNum.Length < 4)
                {
                    crIdNum = "0" + crIdNum;
                }
                //do
                //{
                //    crIdNum = "0" + crIdNum;

                //} while (crIdNum.Length < 4);
                minId = tempMinIdPart + crIdNum;
            }
            else
            {
                minId = tempMinIdPart + "0001";
            }

        }

        public IList<PmsMinHead> SelectPmsMinHeadByMinIdPart(string minIdPart)
        {
            try
            {
                string minId = minIdPart + "%";
                return m_PMSSqlConnection.QueryForList<PmsMinHead>("SelectPmsMinHeadByMinId", minId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsMinHeadBiz/SelectPmsMinHeadByMinIdPart:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsMinHead> SelectPmsMinHeadByPmsIdMinId(string pmsId, string minId)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "PmsId", pmsId }, { "MnId", minId } };

                return m_PMSSqlConnection.QueryForList<PmsMinHead>("SelectPmsMinHeadByPmsIdMinId", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsMinHeadBiz/SelectPmsMinHeadByPmsId:" + ex.ToString());
                return null;
            }
        }

        public bool InsertPmsMinHeadAndPmsMinConclution(PmsMinHead pmsMinHead, IList<PmsMinconclution> pmsMinconclutionList)
        {
            try
            {
                m_PMSSqlConnection.BeginTransaction();
                if (pmsMinHead != null)
                {
                    m_PMSSqlConnection.Insert("InsertPmsMinHead", pmsMinHead);
                }
                if (pmsMinconclutionList != null)
                {
                    if (pmsMinconclutionList.Count > 0)
                    {
                        foreach (PmsMinconclution pmsMinconclution in pmsMinconclutionList)
                        {
                            m_PMSSqlConnection.Insert("InsertPmsMinconclution", pmsMinconclution);

                        }

                    }
                }

                m_PMSSqlConnection.CommitTransaction();
                return true;

            }
            catch (Exception ex)
            {
                m_PMSSqlConnection.RollBackTransaction();
                m_Logger.Error("PmsHeadBiz/InsertPmsHeadAndDoc" + ex.Message.ToString());
                return false;
            }
        }

        public int DeletePmsMinHeadAndPmsMinConclution(string pmsId, string minId, out string message)
        {
            int returnSerial = 0;
            message = string.Empty;
            try
            {

                m_PMSSqlConnection.BeginTransaction();

                Hashtable hashtable = new Hashtable { { "PmsId", pmsId }, { "MnId", minId } };

                returnSerial = m_PMSSqlConnection.Delete("DeletePmsMinHead", hashtable);
                if (returnSerial == 0)
                {
                    message = "Delete PmsMinhead failed!";
                    return returnSerial;
                }

                // 先查询一下有没有数据，有则删除，无则跳过
                IList<PmsMinconclution> pmsMinconclutionList = new PmsMinconclutionBiz().SelectPmsMinconclutionByMinId(minId);

                if (pmsMinconclutionList != null)
                {
                    if (pmsMinconclutionList.Count > 0)
                    {
                        returnSerial = m_PMSSqlConnection.Delete("DeletePmsMinconclutionByMnId", minId);
                        if (returnSerial == 0)
                        {
                            message = "Delete PmsMinconclution failed!";
                            return returnSerial;
                        }
                    }

                }

                m_PMSSqlConnection.CommitTransaction();
                return returnSerial;
            }
            catch (Exception ex)
            {
                m_PMSSqlConnection.RollBackTransaction();
                m_Logger.Error("PmsMinHeadBiz/DeletePmsMinHeadAndPmsMinConclution" + ex.Message.ToString());
                message = "Delete failed!";
                return returnSerial;
            }
        }

        public bool UpdatePmsMinHeadAndPmsMinConclution(PmsMinHead pmsMinHead, IList<PmsMinconclution> pmsMinconclutionList, IList<PmsMinconclution> pmsMinconclutionListInit)
        {
            try
            {
                m_PMSSqlConnection.BeginTransaction();

                if (pmsMinHead != null)
                {
                    m_PMSSqlConnection.Update("UpdatePmsMinHead", pmsMinHead);
                }

                if (pmsMinconclutionListInit != null)
                {
                    if (pmsMinconclutionListInit.Count > 0)
                    {
                        foreach (PmsMinconclution pmsMinconclutionInit in pmsMinconclutionListInit)
                        {
                            m_PMSSqlConnection.Delete("DeletePmsMinconclution", pmsMinconclutionInit);
                        }
                    }
                }

                if (pmsMinconclutionList != null)
                {
                    if (pmsMinconclutionList.Count > 0)
                    {

                        foreach (PmsMinconclution pmsMinconclution in pmsMinconclutionList)
                        {
                            m_PMSSqlConnection.Insert("InsertPmsMinconclution", pmsMinconclution);
                        }
                    }
                }

                m_PMSSqlConnection.CommitTransaction();
                return true;

            }
            catch (Exception ex)
            {
                m_PMSSqlConnection.RollBackTransaction();
                m_Logger.Error("PmsHeadBiz/InsertPmsHeadAndDoc" + ex.Message.ToString());
                return false;
            }
        }
    }
}
