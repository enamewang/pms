using System;
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
using System.Collections;
using System.Xml;

namespace Qisda.PMS.Business
{
    public class SdpDetailBiz : BaseBusiness
    {
        public int InsertSdpDetailByTemplate(SdpDetail sdpDetail)
        {
            int returnSerial = 0;

            try
            {
                object objResult = m_PMSSqlConnection.Insert("InsertSdpDetailByTemplate", sdpDetail);

                if (objResult != null)
                {
                    returnSerial = (int)objResult;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/InsertSdpDetailByTemplate" + ex.Message.ToString());
            }

            return returnSerial;
        }

        public IList<SdpDetail> SelectSdpDetail(SdpDetail sdpDetail)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectSdpDetail", sdpDetail);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectSdpDetail:" + ex.ToString());
                return null;
            }
        }

        public IList<SdpDetail> SelectMinPlanSDate(string pmsId, string phase)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Pmsid", pmsId }, { "Phase", phase } };

                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectMinPlanSDate", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectMinPlanSDate:" + ex.ToString());
                return null;
            }
        }

        public IList<SdpDetail> SelectMaxPlanEDate(string pmsId, string phase)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Pmsid", pmsId }, { "Phase", phase } };
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectMaxPlanEDate", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectMaxPlanEDate:" + ex.ToString());
                return null;
            }
        }

        public IList<SdpDetail> SelectMinActualStartDate(string pmsId, string phase)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Pmsid", pmsId }, { "Phase", phase } };
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectMinActualStartDate", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectMinActualStartDate:" + ex.ToString());
                return null;
            }
        }

        public IList<SdpDetail> SelectMaxActualEndDate(string pmsId, string phase)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Pmsid", pmsId }, { "Phase", phase } };
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectMaxActualEndDate", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectMaxActualEndDate:" + ex.ToString());
                return null;
            }
        }

        //add by Abel li on 2014-01-08
        public IList<SdpDetail> SelectSdpDetailAndCrIdBySerial(string serial)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Serial", serial } };
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectSdpDetailAndCrIdBySerial", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectSdpDetailAndCrIdBySerial:" + ex.ToString());
                return null;
            }
        }

        public string GetDuration(string pmsId)
        {
            IList<SdpDetail> sdpDetailMinStartDateList = new List<SdpDetail>();
            sdpDetailMinStartDateList = SelectMinActualStartDate(pmsId, null);
            DateTime actualStartDate = new DateTime();
            if (sdpDetailMinStartDateList != null && sdpDetailMinStartDateList.Count > 0)
            {
                actualStartDate = sdpDetailMinStartDateList[0].Actualstartday;
            }
            IList<SdpDetail> sdpDetailMaxEndDateList = new List<SdpDetail>();
            sdpDetailMaxEndDateList = SelectMaxActualEndDate(pmsId, null);
            DateTime actualEndDate = new DateTime();
            if (sdpDetailMaxEndDateList != null && sdpDetailMaxEndDateList.Count > 0)
            {
                actualEndDate = sdpDetailMaxEndDateList[0].Actualendday;
            }
            TimeSpan result;
            string duration = string.Empty;
            int durationInt;
            string startDate = actualStartDate.ToString("yyyyMMdd");
            string endDate = actualEndDate.ToString("yyyyMMdd");
            if (!startDate.Equals("00010101")&&!startDate.Equals("19000101")&&!startDate.Equals("00000000"))
            {
                if (!endDate.Equals("00010101") && !endDate.Equals("19000101") && !endDate.Equals("00000000"))
                {
                    result = actualEndDate - actualStartDate;
                    durationInt = result.Days + 1;
                    duration = durationInt.ToString();

                }
                else
                {
                    result = DateTime.Now - actualStartDate;
                    durationInt = result.Days + 1;
                    duration = durationInt.ToString();
                }

            }

            return duration;

        }


        public int InsertSdpDetail(SdpDetail sdpDetail)
        {
            int returnSerial = 0;

            try
            {
                object objResult = m_PMSSqlConnection.Insert("InsertSdpDetail", sdpDetail);

                if (objResult != null)
                {
                    returnSerial = (int)objResult;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/InsertSdpDetail" + ex.Message.ToString());
            }

            return returnSerial;
        }

        public IList<SdpDetail> SelectPhaseByPmsID(SdpDetail sdpDetail)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectPhaseByPmsID", sdpDetail);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectPhaseByPmsID:" + ex.ToString());
                return null;
            }
        }

        public bool UpdateSdpDetail(SdpDetail sdpDetail)
        {
            bool updateResult = false;

            try
            {
                m_PMSSqlConnection.Update("UpdateSdpDetail", sdpDetail);
                updateResult = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/UpdateSdpDetail:" + ex.ToString());
            }

            return updateResult;
        }

        public bool UpdateSdpDetailResource(SdpDetail sdpDetail)
        {
            bool updateResult = false;

            try
            {
                m_PMSSqlConnection.Update("UpdateSdpDetailResource", sdpDetail);
                updateResult = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/UpdateSdpDetailResource:" + ex.ToString());
            }

            return updateResult;
        }
        //add by Ename Wang on 20131227
        public bool UpdateSdpDetailAuditStatus(SdpDetail sdpDetail)
        {
            bool updateResult = false;
            try
            {
                m_PMSSqlConnection.Update("UpdateSdpDetailAuditStatus", sdpDetail);
                updateResult = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/UpdateSdpDetail:" + ex.ToString());
            }

            return updateResult;
        }
        //end add
        public int DeleteSdpDetail(string item)
        {
            int returnSerial = 0;

            try
            {
                object objResult = m_PMSSqlConnection.Delete("DeleteSdpDetail", item);

                if (objResult != null)
                {
                    returnSerial = (int)objResult;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/DeleteSdpDetail:" + ex.ToString());
            }

            return returnSerial;
        }

        public IList<SdpDetail> SelectMinTaskNo(SdpDetail sdpDetail)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectMinTaskNo", sdpDetail);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectMinTaskNo:" + ex.ToString());
                return null;
            }
        }

        public IList<SdpDetail> SelectMaxTaskNo(SdpDetail sdpDetail)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectMaxTaskNo", sdpDetail);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectMaxTaskNo:" + ex.ToString());
                return null;
            }
        }

        public bool InsertCopyFromDevelopment(IList<SdpDetail> pmsSdpDetailIlist, string strPmsID, string strUser)
        {
            DateTime dateTime = PmsSysBiz.GetDBDateTime();

            PmsCommonBiz pmsCommonBiz = new PmsCommonBiz();

            try
            {
                m_PMSSqlConnection.BeginTransaction();

                for (int i = 0; i < pmsSdpDetailIlist.Count; i++)
                {
                    SdpDetail pmsSdpDetail = new SdpDetail();
                    pmsSdpDetail.Pmsid = strPmsID;

                    pmsSdpDetail.TaskName = pmsSdpDetailIlist[i].TaskName;
                    pmsSdpDetail.Phase = PmsCommonEnum.EnumSdpPhase.Test.ToString();
                    pmsSdpDetail.Role = pmsSdpDetailIlist[i].Role;
                    pmsSdpDetail.Resource = pmsSdpDetailIlist[i].Resource;
                    pmsSdpDetail.Taskno = int.Parse(pmsSdpDetailIlist[i].Taskno.ToString());
                    pmsSdpDetail.Plancost = double.Parse(pmsSdpDetailIlist[i].Plancost.ToString());
                    pmsSdpDetail.Actualcost = double.Parse(pmsSdpDetailIlist[i].Actualcost.ToString());
                    pmsSdpDetail.Completedpercent = double.Parse(pmsSdpDetailIlist[i].Completedpercent.ToString());
                    pmsSdpDetail.Planstartday = pmsSdpDetailIlist[i].Planstartday;
                    pmsSdpDetail.Planendday = pmsSdpDetailIlist[i].Planendday;
                    pmsSdpDetail.Actualstartday = pmsCommonBiz.ConvertDateTime("");
                    pmsSdpDetail.Actualendday = pmsCommonBiz.ConvertDateTime("");
                    pmsSdpDetail.PretaskNo = int.Parse(pmsSdpDetailIlist[i].PretaskNo.ToString());
                    pmsSdpDetail.Remark = pmsSdpDetailIlist[i].Remark;
                    pmsSdpDetail.Iseditable = "Y";
                    pmsSdpDetail.Deleteflag = "N";
                    pmsSdpDetail.Createdate = dateTime;
                    pmsSdpDetail.Createuser = strUser;
                    pmsSdpDetail.Maintaindate = dateTime;
                    pmsSdpDetail.Maintainuser = strUser;

                    int insertResult = InsertSdpDetail(pmsSdpDetail);

                    if (insertResult <= 0)
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return false;
                    }
                }

                m_PMSSqlConnection.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                m_PMSSqlConnection.RollBackTransaction();
                m_Logger.Error("SdpDetailBiz/InsertFromCopyFromDevelopment:" + ex.Message.ToString());
                return false;
            }

        }

        public IList<SdpDetail> SelectSdpDetailOther(SdpDetail sdpDetail)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectSdpDetailOther", sdpDetail);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectSdpDetailOther:" + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// get result from RLS webservice
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetResultFromRLS(string xml, string nodeName)
        {
            try
            {
                string result = string.Concat("<RLSReturn>", xml, "</RLSReturn>");
                XmlDocument dom = new XmlDocument();
                dom.LoadXml(result);
                return dom.SelectSingleNode(nodeName).InnerText.Trim();

            }
            catch (Exception ex)
            {

                m_Logger.Error("SdpDetailBiz/GetResultFromRLS" + ex.Message.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// Check if RLNS have Sent RLN Mail for this CR
        /// </summary>
        /// <param name="cr_id"></param>
        /// <returns></returns>
        public bool CheckRLN(string cr_id, string site)
        {
            try
            {
                Hashtable parameter = new Hashtable();
                parameter.Add("CR_ID", cr_id);
                parameter.Add("SITE", site);
                int count = Convert.ToInt32(m_PMSMSSqlConnection.QueryForObject<Int32>("Check_RLN_STATUS", parameter));
                if (count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/CheckRLN" + ex.Message.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// Get RLN Id
        /// </summary>
        /// <param name="cr_id"></param>
        /// <returns></returns>
        public string GetRLNID(string cr_id)
        {
            try
            {
                return Convert.ToString(m_PMSMSSqlConnection.QueryForObject<String>("GetRLNID", cr_id));
            }
            catch (Exception ex)
            {
                m_Logger.Error("SDPBusiness/GetRLNID" + ex.Message.ToString());
                throw ex;
            }
        }


        public bool InsertSdpDetailByTemplateOnTypeChange(string pmsId, string oldType, string newType)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("PmsId", pmsId);
                hashtable.Add("NewType", newType);
                hashtable.Add("OldType", oldType);

                m_PMSSqlConnection.Insert("InsertSdpDetailByTemplateOnTypeChange", hashtable);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/InsertSdpDetailByTemplateOnTypeChange:" + ex.ToString());
                return false;
            }
        }

        public static DateTime GetDBDateTime()
        {
            DateTime dateTime = DateTime.Now;
            try
            {
                dateTime = (DateTime)m_PMSSqlConnection.QueryForObject("GetDBDateTime", null);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/GetDBDateTime:" + ex.ToString());
            }

            return dateTime;
        }

        public IList<SdpDetail> SelectPmsSdpDetailInfo(SdpDetail pmsSdpDetail)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<SdpDetail>("SelectPmsSdpDetailInfo", pmsSdpDetail);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/SelectPmsSdpDetailInfo" + ex.Message.ToString());
                return null;
            }
        }

        public bool DeleteSDPForCRNoChange(string crId)
        {

            try
            {
                m_PMSSqlConnection.Update("DeleteOldCRSDP", crId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("SdpDetailBiz/DeleteSDPForCRNoChange:" + ex.ToString());
                return false;
            }

            return true;
        }
    }
}
