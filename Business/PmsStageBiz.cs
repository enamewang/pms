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
    public class PmsStageBiz : BaseBusiness
    {
        public IList<PmsStage> SelectStageNameByVID(string item)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsStage>("SelectStageNameByVID", item);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsStageBiz/SelectStageNameByVID:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsStage> SelectPmsStage(int stageId, string vid)
        {
            try
            {
                Hashtable hashtable = new Hashtable {{"StageId", stageId}, {"Vid", vid}};
                return m_PMSSqlConnection.QueryForList<PmsStage>("SelectPmsStage", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsStageBiz/SelectPmsStage:" + ex.ToString());
                return null;
            }
        }



    }
}
