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
using Qisda.PMS.Common;

namespace Qisda.PMS.Business
{
    public class PmsDocumentsBiz : BaseBusiness
    {
        public IList<PmsDocuments> SelectPmsDocuments(PmsDocuments pmsDocuments)
        {
            try
            {
                IList<PmsDocuments> result =
                 m_PMSSqlConnection.QueryForList<PmsDocuments>("SelectPmsDocuments", pmsDocuments);
                return result;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentsBiz/SelectPmsDocuments:" + ex.ToString());
                return null;
            }
        }

        public IList<PmsDocuments> SelectPmsDocumentsOther(PmsDocuments pmsDocuments)
        {
            try
            {
                return m_PMSSqlConnection.QueryForList<PmsDocuments>("SelectPmsDocumentsOther", pmsDocuments);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentsBiz/SelectPmsDocumentsOther:" + ex.ToString());
                return null;
            }
        }

        //Get PmsDocuments from TFS add by Ename Wang on 20140221
        public IList<PmsDocuments> GetPmsDocuments(PmsHead pmsHead)
        {
            try
            {
                IList<PmsDocuments> listPmsDocuments = new List<PmsDocuments>();
                Hashtable hashtable = new Hashtable();
                hashtable.Add("CrNo", pmsHead.CrId);
                IList<VTfsDoc> listVTfsDoc = m_PMSMSSqlConnection.QueryForList<VTfsDoc>("SelectVTfsDoc", hashtable);
                foreach (VTfsDoc VTfsDoc in listVTfsDoc)
                {
                    PmsDocuments PmsDocuments = new PmsDocuments();
                    PmsDocuments.PmsId = pmsHead.PmsId;
                    PmsDocuments.CreateDate = pmsHead.CreateDate;
                    PmsDocuments.Creator = pmsHead.Creator;
                    PmsDocuments.DocTypeId = GetDocTypeIdByFileName(VTfsDoc.FileName);
                    PmsDocuments.FileName = VTfsDoc.FileName;
                    PmsDocuments.Path = VTfsDoc.FileUrl;
                    PmsDocuments.Size = "0";
                    listPmsDocuments.Add(PmsDocuments);
                }
                return listPmsDocuments;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentsBiz/GetPmsDocuments:" + ex.ToString());
                return null;
            }
        }
        //end add

        private int GetDocTypeIdByFileName(string fileName)
        {
            int docTypeId = 0;
            if (fileName.IndexOf(PmsCommonEnum.DocumentType.PES.GetDescription()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.PES;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.PIS.GetDescription()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.PIS;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.STP.GetDescription()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.STP;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.STC.GetDescription()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.STC;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.RLN.ToString()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.RLN;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.Study_Report.ToString()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.Study_Report;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.Other.ToString()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.Other;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.PES_MIN.ToString()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.PES_MIN;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.PIS_MIN.ToString()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.PIS_MIN;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.STP_MIN.ToString()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.STP_MIN;
            }
            else if (fileName.IndexOf(PmsCommonEnum.DocumentType.RLN_MIN.ToString()) != -1)
            {
                docTypeId = (int)PmsCommonEnum.DocumentType.RLN_MIN;
            }
            return docTypeId;
        }

        public int InsertPmsDocuments(PmsDocuments pmsDocuments)
        {
            int returnSerial = 0;

            try
            {
                object obj = m_PMSSqlConnection.Insert("InsertPmsDocuments", pmsDocuments);

                returnSerial = 1;
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentsBiz/InsertPmsDocuments" + ex.Message.ToString());
            }

            return returnSerial;
        }

        public int UpdatePmsDocuments(PmsDocuments pmsDocuments)
        {
            int returnSerial = 0;

            try
            {
                returnSerial = m_PMSSqlConnection.Update("UpdatePmsDocuments", pmsDocuments);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentsBiz/UpdatePmsDocuments" + ex.Message.ToString());
            }

            return returnSerial;
        }

        public int DeletePmsDocuments(PmsDocuments pmsDocuments)
        {
            int returnSerial = 0;

            try
            {
                returnSerial = m_PMSSqlConnection.Delete("DeletePmsDocuments", pmsDocuments);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentsBiz/DeletePmsDocuments" + ex.Message.ToString());
            }

            return returnSerial;
        }

        public IList<PmsDocuments> SelectPmsDocumentsDocTypeId(string pmsId, string docTypeId)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                hashtable.Add("PmsId", pmsId);
                hashtable.Add("DocTypeIds", docTypeId);
                return m_PMSSqlConnection.QueryForList<PmsDocuments>("SelectPmsDocumentsDocTypeId", hashtable);
            }
            catch (Exception ex)
            {
                m_Logger.Error("PmsDocumentsBiz/SelectPmsDocumentsDocTypeId:" + ex.ToString());
                return null;
            }
        }


    }
}
