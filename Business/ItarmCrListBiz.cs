
#region -- Page Information --
//////////////////////////////////////////////////////////////////////////////////
// File name: CreateService.aspx.cs    
// Copyright (C) 2012 Qisda Corporation. All rights reserved.    
// Author:		  Ename Wang   
// ALTER  Date:   2012/06/19
// Current Version:  1.0
// Description:   behind code of AutoExecutionSetup.aspx
// History: 
// 
//      Date     |    Time    |    Author   |  Modification 
// 1  2012/06/19 |   08:30:16 |  Ename Wang |  Create
//////////////////////////////////////////////////////////////////////////////////
#endregion

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
    public class ItarmCrListBiz : BaseBusiness
    {
        public int InsertItarmCrList(ItarmCrList itarmCrList)
        {
            int returnSerial = 0;

            try
            {
                object obj = m_PMSSqlConnection.Insert("InsertItarmCrList", itarmCrList);

                if (obj != null)
                {
                    returnSerial = (int)obj;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("ItArmCrListBiz/InsertItarmCrList" + ex.ToString());
            }
            return returnSerial;
        }

        public int InsertItarmCrListCo(ItarmCrListCo itarmCrListCo)
        {
            int returnSerial = 0;

            try
            {
                object obj = m_PMSSqlConnection.Insert("InsertItarmCrListCo", itarmCrListCo);

                if (obj != null)
                {
                    returnSerial = (int)obj;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error("ItArmCrListBiz/InsertItarmCrListCo" + ex.ToString());
            }
            return returnSerial;
        }

        public int InsertFromItarmRequirement(string oldCrId, string newCrId)
        {
            ItarmCrList itarmCrListInsert = new ItarmCrList();
            ItarmCrListCo itarmCrListCoInsert = new ItarmCrListCo();
            ItarmCrListBiz itarmCrListBiz = new ItarmCrListBiz();
            PmsItarmMappingBiz pmsItarmMappingBiz = new PmsItarmMappingBiz();
            string creator = "Itarm";
            DateTime createDate = System.DateTime.Now;
            int returnSerial = 0;
            try
            {
                #region Deal Requirement
                IList<SdpSys> sdpSysList = m_ITARMSqlConnection.QueryForList<SdpSys>("SelectRequirementList", newCrId);

                if (sdpSysList != null && sdpSysList.Count > 0)
                {
                    // 插到itarm_CR_List
                    m_PMSSqlConnection.BeginTransaction();

                    #region Insert itarm_cr_list
                    if (sdpSysList[0].CrId.ToString().Trim().Length > 15)
                        itarmCrListInsert.CrId = sdpSysList[0].CrId.ToString().Trim().Substring(0, 15);
                    else
                        itarmCrListInsert.CrId = sdpSysList[0].CrId.ToString().Trim();

                    if (sdpSysList[0].CRName.ToString().Trim().Length > 500)
                        itarmCrListInsert.CrName = sdpSysList[0].CRName.ToString().Trim().Substring(0, 500);
                    else
                        itarmCrListInsert.CrName = sdpSysList[0].CRName.ToString().Trim();

                    if (sdpSysList[0].Site.ToString().Trim().Length > 20)
                        itarmCrListInsert.Site = sdpSysList[0].Site.ToString().Trim().Substring(0, 20);
                    else
                        itarmCrListInsert.Site = sdpSysList[0].Site.ToString().Trim();

                    itarmCrListInsert.Creator = creator;
                    itarmCrListInsert.CreateDate = createDate;

                    if (sdpSysList[0].SystemName.ToString().Trim().Length > 100)
                        itarmCrListInsert.System = sdpSysList[0].SystemName.ToString().Trim().Substring(0, 100);
                    else
                        itarmCrListInsert.System = sdpSysList[0].SystemName.ToString().Trim();

                    if (sdpSysList[0].PM.ToString().Trim().Length > 100)
                        itarmCrListInsert.Pm = sdpSysList[0].PM.ToString().Trim().Substring(0, 100);
                    else
                        itarmCrListInsert.Pm = sdpSysList[0].PM.ToString().Trim();

                    int returnInsertResult = itarmCrListBiz.InsertItarmCrList(itarmCrListInsert);
                    if (returnInsertResult <= 0)
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return returnSerial;
                    }

                    #endregion

                    // 并更新PMS_Itarm_Mapping
                    #region Update PMS_ITARM_Mapping
                    bool mappingResult = pmsItarmMappingBiz.UpdatePmsItarmMappingCrId(oldCrId, newCrId);

                    if (mappingResult == false)
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return returnSerial;
                    }
                    #endregion

                    m_PMSSqlConnection.CommitTransaction();
                    returnSerial = 1;
                }
                #endregion
            }
            catch (Exception ex)
            {
                m_Logger.Error("ItArmCrListBiz/InsertFromItarmCr" + ex.ToString());
                return returnSerial;
            }

            try
            {
                #region Deal Requirement_Co
                IList<SdpSys> sdpSysList = m_ITARMSqlConnection.QueryForList<SdpSys>("SelectRequirementCoList", newCrId);
                if (sdpSysList != null && sdpSysList.Count > 0)
                {
                    // 插到itarm_CR_List
                    m_PMSSqlConnection.BeginTransaction();

                    #region Insert itarm_cr_list
                    if (sdpSysList[0].CrId.ToString().Trim().Length > 15)
                        itarmCrListCoInsert.CrId = sdpSysList[0].CrId.ToString().Trim().Substring(0, 15);
                    else
                        itarmCrListCoInsert.CrId = sdpSysList[0].CrId.ToString().Trim();

                    if (sdpSysList[0].CrId_co.ToString().Trim().Length > 15)
                        itarmCrListCoInsert.RelatedcrId = sdpSysList[0].CrId_co.ToString().Trim().Substring(0, 15);
                    else
                        itarmCrListCoInsert.RelatedcrId = sdpSysList[0].CrId_co.ToString().Trim();

                    if (sdpSysList[0].CRName.ToString().Trim().Length > 500)
                        itarmCrListCoInsert.CrName = sdpSysList[0].CRName.ToString().Trim().Substring(0, 500);
                    else
                        itarmCrListCoInsert.CrName = sdpSysList[0].CRName.ToString().Trim();

                    if (sdpSysList[0].CRName_co.ToString().Trim().Length > 100)
                        itarmCrListCoInsert.RelatedcrName = sdpSysList[0].CRName_co.ToString().Trim().Substring(0, 100);
                    else
                        itarmCrListCoInsert.RelatedcrName = sdpSysList[0].CRName_co.ToString().Trim();

                    if (sdpSysList[0].Site.ToString().Trim().Length > 20)
                        itarmCrListCoInsert.RelatedSite = sdpSysList[0].Site.ToString().Trim().Substring(0, 20);
                    else
                        itarmCrListCoInsert.RelatedSite = sdpSysList[0].Site.ToString().Trim();

                    itarmCrListCoInsert.Creator = creator;
                    itarmCrListCoInsert.CreateDate = createDate;

                    if (sdpSysList[0].SystemName.ToString().Trim().Length > 100)
                        itarmCrListCoInsert.System = sdpSysList[0].SystemName.ToString().Trim().Substring(0, 100);
                    else
                        itarmCrListCoInsert.System = sdpSysList[0].SystemName.ToString().Trim();

                    if (sdpSysList[0].PM.ToString().Trim().Length > 100)
                        itarmCrListCoInsert.Pm = sdpSysList[0].PM.ToString().Trim().Substring(0, 100);
                    else
                        itarmCrListCoInsert.Pm = sdpSysList[0].PM.ToString().Trim();

                    int returnInsertResult = itarmCrListBiz.InsertItarmCrListCo(itarmCrListCoInsert);
                    if (returnInsertResult <= 0)
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return returnSerial;
                    }
                    #endregion

                    // 并更新PMS_Itarm_Mapping
                    #region Update PMS_ITARM_Mapping
                    bool mappingResult = pmsItarmMappingBiz.UpdatePmsItarmMappingCrId(oldCrId, newCrId);

                    if (mappingResult == false)
                    {
                        m_PMSSqlConnection.RollBackTransaction();
                        return returnSerial;
                    }
                    #endregion

                    m_PMSSqlConnection.CommitTransaction();
                    returnSerial = 1;
                }
                #endregion
                return returnSerial;

            }
            catch (Exception ex)
            {
                m_Logger.Error("ItArmCrListBiz/InsertFromItarmCrCo" + ex.ToString());
                return returnSerial;
            }

        }
    }
}
