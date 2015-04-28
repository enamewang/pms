using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qisda.PMS.DataAccess;
using IBatisNet.DataMapper;
using log4net;
using MySql.Data;

namespace Qisda.PMS.Business
{
    public class BaseBusiness
    {
        protected static ISqlMapper m_PMSSqlConnection = SqlMapManager.Instance("Configuration/PMSSqlMap.config");
        protected static ISqlMapper m_WSCSqlConnection = SqlMapManager.Instance("Configuration/WSCSqlMap.config");
        protected static ISqlMapper m_PMSMSSqlConnection = SqlMapManager.Instance("Configuration/PMSMSSqlMap.config");
        protected static ISqlMapper m_ITARMSqlConnection = SqlMapManager.Instance("Configuration/ITARMSqlMap.config");
        protected static ISqlMapper m_BugFreeSqlConnection = SqlMapManager.Instance("Configuration/BugFreeSqlMap.config");
        protected static ISqlMapper m_TFSSqlConnection = SqlMapManager.Instance("Configuration/TFSSqlMap.config");
        
        protected static readonly ILog m_Logger = LogManager.GetLogger("LogPMSBiz");



    }
}
