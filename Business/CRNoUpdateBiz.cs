using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Entity;

namespace Qisda.PMS.Business
{
    public class CRNoUpdateBiz : BaseBusiness
    {
        public bool CheckCRNoExist(string crNoTemp, out string infor)
        {
            bool result = true;
            infor = string.Empty;
            try
            {
                IList<ItarmCrList> listItarmCrList = m_PMSSqlConnection.QueryForList<ItarmCrList>("SelectItarmCrList", crNoTemp);
                if (listItarmCrList != null && listItarmCrList.Count > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                    infor = "The Temp CRNo is not exist,please check it!";
                }

                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("CRNoUpdateBiz/CheckCRNoExist:" + ex.ToString());
                infor = "Check Temp CRNo error,please contact PMS director!";
                return result;
            }

        }

        public bool CheckPmsIdExist(string crNoFormal, out string infor)
        {
            bool result = false;
            infor = string.Empty;
            try
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("CrId", crNoFormal);
                hashtable.Add("PmsId", null);
                IList<PmsItarmMapping> listPmsItarmMapping = m_PMSSqlConnection.QueryForList<PmsItarmMapping>("SelectPmsItarmMapping", hashtable);
                if (listPmsItarmMapping != null && listPmsItarmMapping.Count > 0 && listPmsItarmMapping.FirstOrDefault().PmsId != string.Empty)
                {
                    //
                    string pmsId = listPmsItarmMapping.FirstOrDefault().PmsId;
                    int count = (int)m_PMSSqlConnection.QueryForObject("SelectPmsHeadCount", pmsId);
                    if (count > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = false;
                    infor = "PmsId for Formal CRNo is not exist,please contact PMS director!";
                }
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("CRNoUpdateBiz/CheckPmsIdExist:" + ex.ToString());
                infor = "Check PmsId error,please contact PMS director!";
                return result;
            }

        }

        public bool UpdateTempCrNoToFormalCrNo(string formalCRNo, string tempCRNo, string currentUser, out string infor)
        {
            m_PMSSqlConnection.BeginTransaction();
            if (!UpDateCrIdPmsId(formalCRNo, tempCRNo, out infor))
            {
                m_PMSSqlConnection.RollBackTransaction();
                return false;
            }

            if (!InsertFormalCrNo(formalCRNo, tempCRNo, out infor))
            {
                m_PMSSqlConnection.RollBackTransaction();
                return false;
            }
            
            if (!InsertPMSChangeHistory(formalCRNo, tempCRNo, currentUser, out infor))
            {
                m_PMSSqlConnection.RollBackTransaction();
                return false;
            }

            m_PMSSqlConnection.CommitTransaction();
            return true;
        }

        public bool UpDateCrIdPmsId(string newCrId, string oldCrId, out string infor)
        {
            infor = string.Empty;
            try
            {
                Hashtable newHashtable = new Hashtable { { "NewCrId", newCrId }, { "OldCrId", oldCrId } };
                m_PMSSqlConnection.Update("UpdatePmsItarmMappingCrId", newHashtable);
                IList<PmsItarmMapping> listPmsItarmMapping = m_PMSSqlConnection.QueryForList<PmsItarmMapping>("SelectPmsItarmMapping", new Hashtable { { "CrId", newCrId }, { "PmsId", null } });
                string pmsId = listPmsItarmMapping.FirstOrDefault().PmsId;
                PmsHead pmsHead = new PmsHead();
                pmsHead.PmsId = pmsId;
                pmsHead.Vid = "PM";
                m_PMSSqlConnection.Update("UpdatePmsHeadVID", pmsHead);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("CRNoUpdateBiz/UpDateCrIdPmsId:" + ex.Message.ToString());
                infor = "Update failed!";
                return false;
            }
        }



        public bool InsertFormalCrNo(string formalCrNo, string tempCrNo, out string infor)
        {
            infor = string.Empty;
            try
            {
                //临时CR改为正式的CR有两种情况：协同CR,正常CR
                if (formalCrNo.Length > 11)
                {
                    IList<ItarmCrListCo> itarmCrListCo = m_PMSSqlConnection.QueryForList<ItarmCrListCo>("SelectItarmCrListCo", new Hashtable { { "CrId", formalCrNo } });
                    if (itarmCrListCo != null && itarmCrListCo.Count > 0 && itarmCrListCo.FirstOrDefault().CrId != string.Empty)
                    {
                        //FormalCrNo exists 只需要删除临时的CrNo，不需要插入正式的CrNo
                        if (!DeleteItarmCrList(tempCrNo, out infor))
                        {
                            m_PMSSqlConnection.RollBackTransaction();
                            return false;
                        }

                    }
                    else
                    {
                        //取出临时CR的信息
                        IList<ItarmCrList> itarmCrList = m_PMSSqlConnection.QueryForList<ItarmCrList>("SelectItarmCrList", tempCrNo);
                        if (itarmCrList != null && itarmCrList.Count > 0)
                        {
                            ItarmCrListCo ItarmCrListCoInsert = new ItarmCrListCo();

                            if (formalCrNo.IndexOf('_') != -1)
                            {
                                if (formalCrNo.Split('_')[0] != string.Empty)//formalCrNo以‘_’开始的情况
                                {
                                    ItarmCrListCoInsert.RelatedcrId = formalCrNo.Split('_')[0];
                                }
                                else
                                {
                                    ItarmCrListCoInsert.RelatedcrId = formalCrNo;
                                }
                            }
                            else
                            {
                                ItarmCrListCoInsert.RelatedcrId = formalCrNo;
                            }

                            ItarmCrListCoInsert.RelatedcrName = string.Empty;
                            ItarmCrListCoInsert.RelatedSite = itarmCrList.FirstOrDefault().Site;

                            ItarmCrListCoInsert.CrId = formalCrNo;
                            ItarmCrListCoInsert.CrName = itarmCrList.FirstOrDefault().CrName;
                            ItarmCrListCoInsert.Pm = itarmCrList.FirstOrDefault().Pm;
                            ItarmCrListCoInsert.System = itarmCrList.FirstOrDefault().System;
                            ItarmCrListCoInsert.Creator = itarmCrList.FirstOrDefault().Creator;
                            ItarmCrListCoInsert.CreateDate = itarmCrList.FirstOrDefault().CreateDate;
                            m_PMSSqlConnection.Insert("InsertItarmCrListCo", ItarmCrListCoInsert);
                        }

                        if (!DeleteItarmCrList(tempCrNo, out infor))
                        {
                            m_PMSSqlConnection.RollBackTransaction();
                            return false;
                        }

                    }


                }
                else
                {
                    IList<ItarmCrList> itarmCrListCo = m_PMSSqlConnection.QueryForList<ItarmCrList>("SelectItarmCrList", formalCrNo);
                    if (itarmCrListCo != null && itarmCrListCo.Count > 0 && itarmCrListCo.FirstOrDefault().CrId != string.Empty)
                    {
                        //FormalCrNo exists 只需要删除临时的CrNo，不需要插入正式的CrNo
                        if (!DeleteItarmCrList(tempCrNo, out infor))
                        {
                            m_PMSSqlConnection.RollBackTransaction();
                            return false;
                        }
                    }
                    else
                    {
                        IList<ItarmCrList> listItarmCrList = m_PMSSqlConnection.QueryForList<ItarmCrList>("SelectItarmCrList", tempCrNo);
                        if (listItarmCrList != null && listItarmCrList.Count > 0)
                        {
                            listItarmCrList.FirstOrDefault().CrId = formalCrNo;
                            m_PMSSqlConnection.Update("UpdateItarmCrList", listItarmCrList.FirstOrDefault());
                        }
                        if (!DeleteItarmCrList(tempCrNo, out infor))
                        {
                            m_PMSSqlConnection.RollBackTransaction();
                            return false;
                        }

                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("CRNoUpdateBiz/UpDateCrIdPmsId:" + ex.Message.ToString());
                infor = "Insert failed!";
                return false;
            }
        }

        public bool DeleteItarmCrList(string oldCrId, out string infor)
        {
            infor = string.Empty;
            try
            {
                m_PMSSqlConnection.Delete("DeleteItarmCrListCo", oldCrId);
                m_PMSSqlConnection.Delete("DeleteItarmCrList", oldCrId);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("CRNoUpdateBiz/UpDateCrIdPmsId:" + ex.Message.ToString());
                infor = "Delete failed!";
                return false;
            }

        }

        public bool InsertPMSChangeHistory(string newCrId, string oldCrId, string currentUser, out string infor)
        {
            infor = string.Empty;
            try
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("CrId", newCrId);
                IList<PmsItarmMapping> listPmsItarmMapping = m_PMSSqlConnection.QueryForList<PmsItarmMapping>("SelectPmsItarmMapping", hashtable);
                if (listPmsItarmMapping != null && listPmsItarmMapping.Count > 0 && listPmsItarmMapping.FirstOrDefault().PmsId != string.Empty)
                {
                    PmsChangeHistory pmsChangeHistory = new PmsChangeHistory();
                    pmsChangeHistory.PmsId = listPmsItarmMapping.FirstOrDefault().PmsId;
                    pmsChangeHistory.ChangeContent = "CR ID from" + " " + oldCrId + " " + newCrId;
                    pmsChangeHistory.Action = "CREATE";
                    pmsChangeHistory.Creator = currentUser;
                    pmsChangeHistory.CreateDate = PmsSysBiz.GetDBDateTime();
                    m_PMSSqlConnection.Insert("InsertPmsChangeHistory", pmsChangeHistory);
                    return true;
                }
                infor = "InsertPMSChangeHistory failed!";
                return false;

            }
            catch (Exception ex)
            {
                m_Logger.Error("CRNoUpdateBiz/UpDateCrIdPmsId:" + ex.Message.ToString());
                infor = "InsertPMSChangeHistory failed!";
                return false;
            }

        }

    }
}
