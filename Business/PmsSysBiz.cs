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
    public class PmsSysBiz : BaseBusiness
    {
        public static DateTime GetDBDateTime()
        {
            DateTime dateTime = DateTime.Now;
            try
            {
                dateTime = (DateTime)m_PMSSqlConnection.QueryForObject<DateTime>("SelectSysDateTime", string.Empty);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSysBiz/GetDBDateTime:" + ex.ToString());
            }

            return dateTime;
        }

        public IList<PmsSys> SelectData1ByType(string vid, string type)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Vid", vid }, { "Type", type } };
                return m_PMSSqlConnection.QueryForList<PmsSys>("SelectData1ByType", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSysBiz/SelectData1ByType:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsSys> SelectData1Data2ByType(string vid, string type)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Vid", vid }, { "Type", type } };
                return m_PMSSqlConnection.QueryForList<PmsSys>("SelectData1Data2ByType", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSysBiz/SelectData1Data2ByType:" + ex.ToString());
                return null;
            }
        }
        public IList<PmsSys> SelectData2Data3ByType(string vid, string type, string Data1)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Vid", vid }, { "Type", type }, { "Data1", Data1 } };
                return m_PMSSqlConnection.QueryForList<PmsSys>("SelectData2Data3ByType", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSysBiz/SelectData2Data3ByType:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsSys> SelectData2ByTypeData1(string vid, string type, string data1)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Vid", vid }, { "Type", type }, { "Data1", data1 } };
                return m_PMSSqlConnection.QueryForList<PmsSys>("SelectData2ByTypeData1", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSysBiz/SelectData1Data2ByType:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsSys> SelectData1ByTypeData5(string vid, string type, string data5)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Vid", vid }, { "Type", type }, { "Data5", data5 } };
                return m_PMSSqlConnection.QueryForList<PmsSys>("SelectData1ByTypeData5", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSysBiz/SelectData1ByTypeData5:" + ex.ToString());
                return null;
            }
        }


    }
}
