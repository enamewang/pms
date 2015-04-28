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
    public class ItarmCrListCoBiz : BaseBusiness  
    {
        public IList<ItarmCrListCo> SelectItarmCrListCo(string crId)
        {
            try
            {
                Hashtable hashtable=new Hashtable {{"CrId", crId}};
                return m_PMSSqlConnection.QueryForList<ItarmCrListCo>("SelectItarmCrListCo", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("ItarmCrListCoBiz/SelectItarmCrListCo:" + ex.ToString());
                return null;
            }
        }


        public IList<PmsItarmMapping> SelectPmsItarmMappingCoCrNoPmsIdByCrNo(string crId)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsItarmMapping>("SelectPmsItarmMappingCoCrNoPmsIdByCrNo", crId);
            }
            catch (Exception ex)
            {
                m_Logger.Error("ItarmCrListCoBiz/SelectPmsItarmMappingCoCrNoPmsIdByCrNo:" + ex.ToString());
                return null;
            }
        }

        //public IList<ItarmCrListCo> SelectItarmCrListCoByCrNo(string crId)
        //{
        //    try
        //    {
        //        Hashtable hashtable = new Hashtable { { "CrId", crId } };
        //        return m_PMSSqlConnection.QueryForList<ItarmCrListCo>("SelectItarmCrListCoByCrNo", hashtable);
        //    }
        //    catch (Exception ex)
        //    {
        //        m_Logger.Error("ItarmCrListCoBiz/SelectItarmCrListCoByCrNo:" + ex.ToString());
        //        return null;
        //    }
        //}
        




    }
}
