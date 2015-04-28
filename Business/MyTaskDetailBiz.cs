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
    public class MyTaskDetailBiz : BaseBusiness
    {
        /// <summary>
        /// get sdp detail infromaton by serial ,then return object sdepdetail
        /// </summary>
        /// <param name="pmsSdpDetail"></param>
        /// <returns></returns>
        public SdpDetail GetDetailInfoBySerial(SdpDetail sdpDetail)
        {
            try
            {
                return m_PMSSqlConnection.QueryForObject<SdpDetail>("SelectSdpDetail", sdpDetail);
            }
            catch (Exception ex)
            {
                m_Logger.Error("MyTaskDetailBiz/GetPmsSdpDetailBySerial" + ex.Message.ToString());
                return null;
            }
        }
        public PmsHead SelectPmsHeadOther(PmsHead pmsHead)
        {
            try
            {
                PmsHead result = m_PMSSqlConnection.QueryForObject<PmsHead>("SelectPmsHeadOther", pmsHead);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("MyTaskDetailBiz/SelectPmsHeadOther:" + ex.ToString());
                return null;
            }
        }

        public bool UpdatePmsSapDesignDetailInfo(SdpDetail sdpDetail)
        {
            try
            {
                object obj = m_PMSSqlConnection.Update("UpdatePmsSapDesignDetailInfo", sdpDetail);

                return true;
            }
            catch (Exception ex)
            {
                m_Logger.Error("MyTaskDetailBiz/UpdatePmsSapDesignDetailInfo:" + ex.Message.ToString());
                return false;
            }

        }

    }
}
