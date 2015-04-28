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
    public class BaseDataDomainBiz : BaseBusiness  
    {
        public IList<BaseDataDomain> SelectBaseDataDomain(string item)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<BaseDataDomain>("SelectBaseDataDomain", item);
            }
            catch (Exception ex)
            {
                m_Logger.Error("BaseDataDomainBiz/SelectBaseDataDomain:" + ex.ToString());
                return null;
            }
        }


    }
}
