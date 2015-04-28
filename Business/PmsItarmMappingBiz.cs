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
    public class PmsItarmMappingBiz : BaseBusiness
    {
        public IList<PmsItarmMapping> SelectPmsItarmMappingByTempCrIdPart(string crIdPart)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsItarmMapping>("SelectPmsItarmMappingByTempCrIdPart", crIdPart);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsItarmMappingBiz/SelectPmsItarmMappingByTempCrIdPart:" + ex.ToString());
                return null;
            }
        }

        public int InsertPmsItarmMapping(PmsItarmMapping pmsItarmMapping)
        {
            int returnSerial = 0;

            try
            {
                object objResult = m_PMSSqlConnection.Insert("InsertPmsItarmMapping", pmsItarmMapping);

                returnSerial = 1;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsItarmMappingBiz/InsertPmsItarmMapping" + ex.Message.ToString());
            }

            return returnSerial;
        }

        public IList<PmsItarmMapping> SelectPmsItarmMapping(string crId, string pmsId)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("CrId", crId);
                hashtable.Add("PmsId", pmsId);
                return m_PMSSqlConnection.QueryForList<PmsItarmMapping>("SelectPmsItarmMapping", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsItarmMappingBiz/SelectPmsItarmMapping:" + ex.ToString());
                return null;
            }
        }
        //add by Ename Wang
        public IList<PmsItarmMapping> SelectPmsItarmMappingOther(string pmsId)
        {
            try
            {

                return m_PMSSqlConnection.QueryForList<PmsItarmMapping>("SelectPmsItarmMappingOther", pmsId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsItarmMappingBiz/SelectPmsItarmMappingOther:" + ex.ToString());
                return null;
            }
        }
        //end add

        //add by Abel Li on 2014-01-16
        public IList<PmsItarmMapping> SelectPmsItarmMappingByPmsId(string crno)
        {
            try
            {

                return m_PMSSqlConnection.QueryForList<PmsItarmMapping>("SelectPmsItarmMappingByPmsId", crno);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsItarmMappingBiz/SelectPmsItarmMappingByPmsId:" + ex.ToString());
                return null;
            }
        }
        //end add

        public int DeletePmsItarmMapping(PmsItarmMapping pmsItarmMapping)
        {
            int returnSerial = 0;

            try
            {
                returnSerial = m_PMSSqlConnection.Delete("DeletePmsItarmMapping", pmsItarmMapping);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsItarmMappingBiz/DeletePmsItarmMapping" + ex.Message.ToString());
            }

            return returnSerial;
        }

        public bool UpdatePmsItarmMappingCrId(string oldCrId, string newCrId)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "NewCrId", newCrId }, { "OldCrId", oldCrId } };

                m_PMSSqlConnection.Update("UpdatePmsItarmMappingCrId", hashtable);

                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsItarmMappingBiz/UpdatePmsItarmMappingCrId" + ex.Message.ToString());
                return false;
            }

        }

        public bool DeletePmsItarmMappingCrId(string newCrId)
        {
            try
            {
                m_PMSSqlConnection.Delete("DeletePmsItarmMappingCrId", newCrId);

                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsItarmMappingBiz/DeletePmsItarmMappingCrId" + ex.Message.ToString());
                return false;
            }
        }

    }
}
