using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Configuration;
using System.Data;

using Qisda.PMS.Entity;

using System.IO;
using System.Net;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml;

namespace Qisda.PMS.Business
{
    public class MyTaskBiz : BaseBusiness
    {

        private BaseBusiness pmsCommonBiz = new BaseBusiness();

        /// <summary>
        /// get my task by resource 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<MyTaskCondition> GetMyTaskData(SdpDetail SdpDetail, PmsCommonEnum.EnumTime type)
        {

            try
            {
                IList<MyTaskCondition> result = null;
                switch (type)
                {
                    case PmsCommonEnum.EnumTime.Past:
                        result = m_PMSSqlConnection.QueryForList<MyTaskCondition>("GetMyTaskPast", SdpDetail);

                        break;
                    case PmsCommonEnum.EnumTime.Today:
                        result = m_PMSSqlConnection.QueryForList<MyTaskCondition>("GetMyTaskByToday", SdpDetail);
                        break;
                    case PmsCommonEnum.EnumTime.Future:
                        result = m_PMSSqlConnection.QueryForList<MyTaskCondition>("GetMyTaskByFuture", SdpDetail);
                        break;
                }

                if (result != null)
                {
                    foreach (MyTaskCondition myTaskCondition in result)
                    {
                        if (myTaskCondition.Phase == "4")
                        {
                            myTaskCondition.Phase = "Design";
                        }       
                        else if (myTaskCondition.Phase =="5")
                        {
                            myTaskCondition.Phase = "Development";
                        }
                        else if (myTaskCondition.Phase == "6")
                        {
                            myTaskCondition.Phase = "Test";
                        }
                        else if (myTaskCondition.Phase == "7")
                        {
                            myTaskCondition.Phase = "Release";
                        }
                        else if (myTaskCondition.Phase == "8")
                        {
                            myTaskCondition.Phase = "Support";
                        }
                        else
                        {
                            myTaskCondition.Phase = string.Empty;
                        }

                        if (myTaskCondition.TaskStatus == "1")
                        {
                            myTaskCondition.TaskStatus = "未开始";
                        }
                        else if (myTaskCondition.TaskStatus == "2")
                        {
                            myTaskCondition.TaskStatus = "进行中";
                        }
                        else if (myTaskCondition.TaskStatus == "3")
                        {
                            myTaskCondition.TaskStatus = "已完成";
                        }
                        else if (myTaskCondition.TaskStatus == "4")
                        {
                            myTaskCondition.TaskStatus = "已关闭";
                        }
                        else if (myTaskCondition.TaskStatus == "5")
                        {
                            myTaskCondition.TaskStatus = "已取消";
                        }
                        else if (myTaskCondition.TaskStatus == "6")
                        {
                            myTaskCondition.TaskStatus = "已暂缓";
                        }
                        else
                        {
                            myTaskCondition.TaskStatus = string.Empty;
                        }

                    }
                }

                return result;

            }
            catch (Exception ex)
            {
                m_Logger.Error("SDPBusiness/GetMyTaskData" + ex.Message.ToString());
                return null;
            }
        }

    }
}
