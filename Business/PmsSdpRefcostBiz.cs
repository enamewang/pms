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
    public class PmsSdpRefcostBiz : BaseBusiness
    {

        public IList<PmsSdpRefcost> SelectPmsSdpRefcost(PmsSdpRefcost pmsSdpRefcost)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsSdpRefcost>("SelectPmsSdpRefcost", pmsSdpRefcost);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsSdpRefcostBiz/SelectPmsSdpRefcost:" + ex.ToString());
                return null;
            }
        }
    }
}
