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

namespace Qisda.PMS.Business
{
    public class PmsChangeHistoryBiz : BaseBusiness
    {
        public int InsertPmsChangeHistory(PmsChangeHistory pmsChangeHistory)
        {
            int returnSerial = 0;

            try
            {
                object objResult = m_PMSSqlConnection.Insert("InsertPmsChangeHistory", pmsChangeHistory);

                returnSerial = 1;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsChangeHistoryBiz/InsertPmsChangeHistory" + ex.Message.ToString());
            }

            return returnSerial;
        }
    }
}
