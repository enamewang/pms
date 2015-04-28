using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.Business;
using Qisda.PMS.DataAccess;
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
    public class SdpSysBiz : BaseBusiness 
    {
        //public IList<SdpSys> SelectSdpSys(SdpSys sdpSys)
        //{
        //    try
        //    {
        //        return m_PMSMSSqlConnection.QueryForList<SdpSys>("SelectSdpSys", sdpSys);
        //    }
        //    catch (Exception ex)
        //    {
        //        m_Logger.Error("SdpSysBiz/SelectSdpSys:" + ex.ToString());
        //        return null;
        //    }
        //}
    }
}
