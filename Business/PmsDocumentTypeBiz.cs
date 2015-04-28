using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Business;
using Qisda.PMS.Entity;
using System.IO;
using System.Collections;
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
    public class PmsDocumentTypeBiz : BaseBusiness  
    {
        public IList<PmsDocumentType> SelectPmsDocumentType(PmsDocumentType pmsDocumentType)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsDocumentType>("SelectPmsDocumentType", pmsDocumentType);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentTypeBiz/SelectPmsDocumentType:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsDocumentType> SelectDistinctDocType(PmsDocumentType pmsDocumentType)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsDocumentType>("SelectDistinctDocType", pmsDocumentType);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentTypeBiz/SelectDistinctDocType:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsDocumentType> SelectDistinctDocTypeIdName(string pmsId,int stage)
        {
            try

            {
                Hashtable ht = new Hashtable();
                ht.Add("pmsId", pmsId);
                ht.Add("stage",stage);
                return m_PMSSqlConnection.QueryForList<PmsDocumentType>("SelectDistinctDocTypeIdName", ht);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentTypeBiz/SelectDistinctDocTypeIdName:" + ex.ToString());
                return null;
            }
        }


    }
}
