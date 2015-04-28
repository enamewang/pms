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
    public class PmsSdpAuditBiz : BaseBusiness
    {
        public bool InsertPmsSdpAudit(PmsSdpAudit pmsSdpAudit)
        {
            try
            {
                m_PMSSqlConnection.Insert("InsertPmsSdpAudit", pmsSdpAudit);
                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSdpAuditBiz/InsertPmsSdpAudit:" + ex.ToString());
                return false;
            }
        }

        public IList<PmsSdpAudit> SelectPmsSdpAuditByPmsId(string pmsid)
        {
            try
            {
                Hashtable hashtable = new Hashtable { { "Pmsid", pmsid } };
                return m_PMSSqlConnection.QueryForList<PmsSdpAudit>("SelectPmsSdpAuditByPmsId", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSdpAuditBiz/SelectPmsSdpAuditByPmsId:" + ex.ToString());
                return null;
            }
        }

    }
}
