using System;
using System.Text;
using System.Collections;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using MySql.Data;

namespace Qisda.PMS.DataAccess
{
    /// <summary>
    /// SqlMapper Manager
    /// </summary>
    public class SqlMapManager
    {

        private static Hashtable m_ISqlMappers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected static void Configure(object obj)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        static SqlMapManager()
        {
            if (m_ISqlMappers == null)
            {
                m_ISqlMappers = new Hashtable();
            }
        }
        
        /// <summary>
        /// 实例化SqlMapper
        /// </summary>
        /// <param name="configUrl"></param>
        protected static void InitMapper(string configUrl)
        {
            ConfigureHandler handler = new ConfigureHandler(Configure);
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            try
            {
               ISqlMapper mapper = builder.ConfigureAndWatch(configUrl, handler);
               m_ISqlMappers.Add(configUrl, mapper);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ISqlMapper Instance()
        {
            return Instance("SqlMap.config");
        }

        /// <summary>
        /// 获取SqlMapper
        /// </summary>
        /// <param name="configUrl"></param>
        /// <returns></returns>
        public static ISqlMapper Instance(string configUrl)
        {
            lock (typeof(Hashtable))
            {
                if (!m_ISqlMappers.ContainsKey(configUrl))
                {
                    InitMapper(configUrl);
                }
            }
            return (ISqlMapper)m_ISqlMappers[configUrl];
        }

        /// <summary>
        /// 清除SqlMapper
        /// </summary>
        /// <param name="configUrl"></param>
        public static void Remove(string configUrl)
        {
            if (m_ISqlMappers.ContainsKey(configUrl))
            {
                m_ISqlMappers.Remove(configUrl);
            }
        }

    }
}
