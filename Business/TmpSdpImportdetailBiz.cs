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
    public class TmpSdpImportdetailBiz : BaseBusiness
    {
        public IList<TmpSdpImportdetail> GetTmpSdpDetail(TmpSdpImportdetail tmpSdpImportdetail)
        {
           try
            {
                return m_PMSSqlConnection.QueryForList<TmpSdpImportdetail>("SelectTmpSdpDetail", tmpSdpImportdetail);

            }
            catch (Exception ex)
            {
                m_Logger.Error("TmpSdpImportdetailBiz/InsertTmpSdpDetail" + ex.Message.ToString());
                return null;
            }
        }    

        public int InsertTmpSdpDetail(TmpSdpImportdetail tmpSdpImportdetail)
        {
            int returnSerial = 0;

            try
            {
                object objResult = m_PMSSqlConnection.Insert("InsertTmpSdpDetail", tmpSdpImportdetail);

                if (objResult != null)
                {
                    returnSerial = (int)objResult;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("TmpSdpImportdetailBiz/InsertTmpSdpDetail" + ex.Message.ToString());
            }

            return returnSerial;
        }

        public bool UpdateTmpSdpDetailFlag(TmpSdpImportdetail tmpSdpImportdetail)
        {
            bool updateResult = false;

            try
            {
                m_PMSSqlConnection.Update("UpdateTmpSdpDetailFlag", tmpSdpImportdetail);
                updateResult = true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("TmpSdpImportdetailBiz/UpdateTmpSdpDetailFlag:" + ex.ToString());
            }
            return updateResult;
        }

        public bool DeleteTmpSdpImportDetailByPmsId(string pmsid)
        {
            Hashtable hashtable = new Hashtable { { "Pmsid", pmsid } };
            try
            {
                m_PMSSqlConnection.Delete("DeleteTmpSdpImportDetailByPmsId", hashtable);

                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("TmpSdpImportdetailBiz/DeleteTmpSdpImportDetailByPmsId" + ex.Message.ToString());
                return false;
            }
        }

    }
}
