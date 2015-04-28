using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;
using MySql.Data;

namespace Qisda.PMS.Business
{
    public class PmsHeadBiz : BaseBusiness
    {
        public IList<PmsHead> SelectPmsHeadOther(PmsHead pmsHead)
        {
            try
            {
                IList<PmsHead> result =
                 m_PMSSqlConnection.QueryForList<PmsHead>("SelectPmsHeadOther", pmsHead);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectPmsHeadOther:" + ex.ToString());
                return null;
            }
        }
      

            public IList<PmsHead> SelectPmsHeadForAgent(PmsHead pmsHead)
        {
            try
            {
                IList<PmsHead> result =
                 m_PMSSqlConnection.QueryForList<PmsHead>("SelectPmsHeadForAgent", pmsHead);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectPmsHeadForAgent:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsHead> SelectPmsHeadForAuditor(PmsHead pmsHead)
        {
            try
            {
                IList<PmsHead> result =
                 m_PMSSqlConnection.QueryForList<PmsHead>("SelectPmsHeadForAuditor", pmsHead);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectPmsHeadForAuditor:" + ex.ToString());
                return null;
            }
        }
        public IList<PmsHead> SelectPmsHeadForCheckNewVersion(PmsHead pmsHead)
        {
            try
            {
                IList<PmsHead> result =
                 m_PMSSqlConnection.QueryForList<PmsHead>("SelectPmsHeadForCheckNewVersion", pmsHead);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectPmsHeadOther:" + ex.ToString());
                return null;
            }
        }
        public IList<PmsHead> SelectPmsHeadByPmsId(string pmsIdPart)
        {
            Hashtable hashtable = new Hashtable { { "PmsIdPart", pmsIdPart } };
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsHead>("SelectPmsHeadByPmsId", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectPmsHeadByPmsId:" + ex.ToString());
                return null;
            }
        }

        //public IList<PmsHead> SelectPmsHeadByPmsId(string pmsIdPart)
        //{
        //    Hashtable hashtable = new Hashtable();
        //    hashtable.Add("PmsIdPart", pmsIdPart);
        //    try
        //    {
        //        return m_PMSSqlConnection.QueryForList<PmsHead>("SelectPmsHeadByPmsId", hashtable);
        //    }
        //    catch (Exception ex)
        //    {
        //        m_Logger.Error("PmsHeadBiz/SelectPmsHeadByPmsId:" + ex.ToString());
        //        return null;
        //    }
        //}


        public bool UpdatePmsHeadRerver1ByPmsId(string pmsId, string rerver)
        {
            Hashtable hashtable = new Hashtable { { "PmsId", pmsId }, { "Rerver1", rerver } };
            try
            {
                m_PMSSqlConnection.Update("UpdatePmsHeadRerver1ByPmsId", hashtable);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectPmsHeadByTempCrIdPart:" + ex.ToString());
                return false;
            }
        }

        //public IList<PmsHead> SelectPmsHeadByTempCrIdPart(string tempCrIdPart)
        //{
        //    Hashtable hashtable = new Hashtable { { "TempCrIdPart", tempCrIdPart } };
        //    try
        //    {
        //        return m_PMSSqlConnection.QueryForList<PmsHead>("SelectPmsHeadByTempCrIdPart", hashtable);
        //    }
        //    catch (Exception ex)
        //    {
        //        m_Logger.Error("PmsHeadBiz/SelectPmsHeadByTempCrIdPart:" + ex.ToString());
        //        return null;
        //    }
        //}

        public int InsertPmsHead(PmsHead pmsHead)
        {
            int returnSerial = 0;

            try
            {
                object obj = m_PMSSqlConnection.Insert("InsertPmsHead", pmsHead);

                if (obj != null)
                    returnSerial = (int)obj;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/InsertPmsHead" + ex.Message.ToString());
            }

            return returnSerial;
        }



        #region InsertServiceData
        /// <summary>
        /// Save Service Data 
        /// </summary>
        /// <param name="pmsHead"></param>
        /// <param name="sdpDetail"></param>
        /// <param name="pmsFlow"></param>
        /// <param name="pmsDocuments"></param>
        /// <returns></returns>
        public int InsertServiceDataToDB(PmsHead pmsHead, SdpDetail sdpDetail, PmsFlow pmsFlow, PmsDocuments pmsDocuments)
        {
            int returnResult = 0;
            try
            {
                m_PMSSqlConnection.BeginTransaction();

                // Insert PmsHead
                pmsHead.Vid = "PM";
                pmsHead.Stage = 1;
                pmsHead.Pm = "";
                pmsHead.Sd = "";
                pmsHead.Se = "";
                pmsHead.Qa = "";
                // m_PMSSqlConnection.Insert("InsertPmsHead", pmsHead);

                // Insert SdpDetail
                SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                returnResult = sdpDetailBiz.InsertSdpDetailByTemplate(sdpDetail);

                if (returnResult == 0)
                {
                    m_PMSSqlConnection.RollBackTransaction();
                    return returnResult;
                }

                // Insert PmsFlow
                PmsFlowBiz pmsFlowBiz = new PmsFlowBiz();
                pmsFlowBiz.InsertPmsFlow(pmsFlow);


                // Insert PmsDocuments
                if (pmsDocuments.FileName != string.Empty)
                {
                    m_PMSSqlConnection.Insert("InsertPmsDocuments", pmsDocuments);
                }

                m_PMSSqlConnection.CommitTransaction();

            }
            catch (Exception ex)
            {
                m_PMSSqlConnection.RollBackTransaction();
                m_Logger.Error("PmsHeadBiz/InsertServiceDataToDB" + ex.Message.ToString());
            }

            return returnResult;

        }
        #endregion

        public int InsertPmsHeadAndDoc(PmsHead pmsHead, SdpDetail sdpDetail, PmsChangeHistory pmsChangeHistory, PmsItarmMapping pmsItarmMapping,
            PmsFlow pmsFlow, ItarmCrList itarmCrList, IList<PmsDocuments> listPmsDocuments,out string errorInfo)
        {
            errorInfo = string.Empty;
            int returnResult = 0;

            try
            {
                m_PMSSqlConnection.BeginTransaction();

                #region Insert PmsHead
                //string type = pmsHead.PmsName.GetTypeCode;
                m_PMSSqlConnection.Insert("InsertPmsHead", pmsHead);
                #endregion

                #region Insert PmsItarmMapping
                PmsItarmMappingBiz pmsItarmMappingBiz = new PmsItarmMappingBiz();
                pmsItarmMappingBiz.InsertPmsItarmMapping(pmsItarmMapping);
                #endregion

                #region Insert PmsDocuments

                if (listPmsDocuments!=null)
                {
                    foreach (PmsDocuments pmsDocuments in listPmsDocuments)
                    {
                        if (pmsDocuments.DocTypeId == 0)
                        {
                            errorInfo = "DocTypeId is Invalid!";
                            return returnResult;
                        }

                        if (pmsDocuments.FileName == string.Empty)
                        {
                            errorInfo = "FileName is Empty!";
                            return returnResult;
                        }

                        if (pmsDocuments.FileName != string.Empty)
                        {
                            m_PMSSqlConnection.Insert("InsertPmsDocuments", pmsDocuments);
                        }

                    }
                }
                
                
                #endregion

                #region Insert SdpDetail
                SdpDetailBiz sdpDetailBiz = new SdpDetailBiz();
                returnResult = sdpDetailBiz.InsertSdpDetailByTemplate(sdpDetail);

                if (returnResult == 0)
                {
                    m_PMSSqlConnection.RollBackTransaction();
                    return returnResult;
                }
                #endregion

                #region Insert PmsChangeHistory
                PmsChangeHistoryBiz pmsChangeHistoryBiz = new PmsChangeHistoryBiz();
                pmsChangeHistoryBiz.InsertPmsChangeHistory(pmsChangeHistory);
                #endregion

                #region Insert PmsFlow
                PmsFlowBiz pmsFlowBiz = new PmsFlowBiz();
                pmsFlowBiz.InsertPmsFlow(pmsFlow);
                #endregion

                #region Insert ItarmCrList
                bool resultInsertItarm = new PmsCRCreatBiz().InsertItarmCrList(itarmCrList);
                if (!resultInsertItarm)
                {
                    m_PMSSqlConnection.RollBackTransaction();
                    errorInfo = "Save Fail";
                    return 0;
                }
                #endregion

                m_PMSSqlConnection.CommitTransaction();

            }
            catch (Exception ex)
            {
                m_PMSSqlConnection.RollBackTransaction();
                errorInfo = "Save Fail";
                m_Logger.Error("PmsHeadBiz/InsertPmsHeadAndDoc" + ex.Message.ToString());
            }

            return returnResult;
        }

        public IList<PmsHead> SelectPmsHead(string pmsId, string userName)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "PmsId", pmsId }, { "UserName", userName } };

                return m_PMSSqlConnection.QueryForList<PmsHead>("SelectPmsHead", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectPmsHead:" + ex.ToString());
                return null;
            }
        }

        public string SelectProcessByPmsID(string pmsId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject<string>("SelectProcessByPmsID", pmsId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectProcessByPmsID:" + ex.ToString());
                return null;
            }
        }

        public string SelectManPowerByPmsID(string pmsId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject<string>("SelectManPowerByPmsID", pmsId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectManPowerByPmsID:" + ex.ToString());
                return null;
            }
        }

        public bool UpdatePmsHead(PmsHead pmsHead)
        {
            bool updateResult = false;

            try
            {
                m_PMSSqlConnection.Update("UpdatePmsHead", pmsHead);
                updateResult = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/UpdatePmsHead:" + ex.ToString());
            }
            return updateResult;
        }
        

        public bool UpdatePmsHeadOther(PmsHead pmsHead)
        {
            try
            {
                m_PMSSqlConnection.Update("UpdatePmsHeadOther", pmsHead);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/UpdatePmsHeadOther:" + ex.ToString());
                return false;
            }

        }

        public bool UpdateStage(PmsHead pmsHead, PmsChangeHistory pmsChangeHistory)
        {
            try
            {
                m_PMSSqlConnection.BeginTransaction();

                if (!UpdatePmsHeadOther(pmsHead))
                {
                    m_PMSSqlConnection.RollBackTransaction();
                    m_Logger.Error("PmsHeadBiz/UpdateStage/UpdatePmsHeadOther");
                    return false;
                }
                PmsChangeHistoryBiz pmsChangeHistoryBiz = new PmsChangeHistoryBiz();
                int result = pmsChangeHistoryBiz.InsertPmsChangeHistory(pmsChangeHistory);
                if (result == 0)
                {
                    m_PMSSqlConnection.RollBackTransaction();
                    m_Logger.Error("PmsHeadBiz/UpdateStage/InsertPmsChangeHistory");
                    return false;
                }
                m_PMSSqlConnection.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                m_PMSSqlConnection.RollBackTransaction();
                m_Logger.Error("PmsHeadBiz/UpdateStage:" + ex.ToString());
                return false;
            }

        }
        //add by Abel on 2014-01-24
        public bool UpdatePmsHeadPlanStartDate(string pmsId)
        {
            try
            {
                m_PMSSqlConnection.Update("UpdatePmsHeadPlanStartDate", pmsId);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/UpdatePmsHeadPlanStartDate:" + ex.ToString());
                return false;
            }
        }

        public bool UpdatePmsHeadActualStartDate(string pmsId)
        {
            try
            {
                m_PMSSqlConnection.Update("UpdatePmsHeadActualStartDate", pmsId);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/UpdatePmsHeadPlanActualStartDate:" + ex.ToString());
                return false;
            }
        }

        public bool UpdatePmsHeadCloseDate(string pmsId, string loginName, DateTime closeDate)
        {
            try
            {
                Hashtable hashtable = new Hashtable
                                          {
                                              {"PmsId", pmsId},
                                              {"MaintainUser", loginName},
                                              {"CloseDate", closeDate},
                                              {"MaintainDate", PmsSysBiz.GetDBDateTime()}
                                          };

                m_PMSSqlConnection.Update("UpdatePmsHeadCloseDate", hashtable);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/UpdateStage:" + ex.ToString());
                return false;
            }
        }

        public IList<PmsHead> SelectPmSdSeQA(string pmsId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsHead>("SelectPmSdSeQA", pmsId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectPmSdSeQA:" + ex.ToString());
                return null;
            }
        }


        public PmsHead SelectSystemInforByCrId(string crId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject<PmsHead>("SelectSystemInforByCrId", crId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectSystemInforByCrId:" + ex.ToString());
                return null;
            }
        }


        public PmsHead SelectCrIdSystemVersionByPmsId(string pmsId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject<PmsHead>("SelectCrIdSystemVersionByPmsId", pmsId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsHeadBiz/SelectCrIdSystemVersionByPmsId:" + ex.ToString());
                return null;
            }
        }



    }
}
