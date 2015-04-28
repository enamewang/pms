using System;
using System.Collections.Generic;
using System.Text;

namespace Qisda.PMS.Entity
{
    public class MyTaskCondition
    {
        #region Private Members

        private bool m_IsChanged;
        private bool m_IsDeleted;
        private string m_PMSName;
        private string m_PMSId;
        private int m_Serial;
        private int m_TaskNo;
        private string m_TaskName;
        private double m_PlanCost;
        private double m_ActualCost;
        private DateTime m_PlanStartDay;
        private DateTime m_PlanEndDay;
        private DateTime m_ActualStartDay;
        private DateTime m_ActualEndDay;
        private int m_PreTaskNo;
        private string m_Role;
        private string m_Resource;
        private string m_Remark;
        private string m_Phase;
        private string m_TaskStatus;
        private double m_CompletedPercent;


        #endregion

        #region Default ( Empty ) Class Constuctor
        /// <summary>
        /// default constructor
        /// </summary>
        public MyTaskCondition()
        {
            m_PMSName = "";
            m_PMSId = "";
            m_Serial = 0;
            m_TaskNo = 0;
            m_TaskName = "";
            m_PlanCost = 0;
            m_ActualCost = 0;
            m_PlanStartDay = new DateTime();
            m_PlanEndDay = new DateTime();
            m_ActualStartDay = new DateTime();
            m_ActualEndDay = new DateTime();
            m_PreTaskNo = 0;
            m_Role = "";
            m_Resource = "";
            m_Remark = "";
            m_Phase = "";
            m_TaskStatus="";
            m_CompletedPercent = 0;

        }
        #endregion // End of Default ( Empty ) Class Constuctor

        #region Public Properties
        /// <summary>
        /// 
        /// </summary>		
        public int Serial
        {
            get { return m_Serial; }
            set { m_IsChanged |= (m_Serial != value); m_Serial = value; }
        }


        // <summary>
        /// 
        /// </summary>		
        public string PMSId
        {
            get { return m_PMSId; }
            set { m_IsChanged |= (m_PMSId != value); m_PMSId = value; }
        }


        /// <summary>
        /// 
        /// </summary>		
        public int TaskNo
        {
            get { return m_TaskNo; }
            set { m_IsChanged |= (m_TaskNo != value); m_TaskNo = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TaskName
        {
            get { return m_TaskName; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for TaskName", value, value.ToString());

                m_IsChanged |= (m_TaskName != value); m_TaskName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double PlanCost
        {
            get { return m_PlanCost; }
            set { m_IsChanged |= (m_PlanCost != value); m_PlanCost = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double ActualCost
        {
            get { return m_ActualCost; }
            set { m_IsChanged |= (m_ActualCost != value); m_ActualCost = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public double CompletedPercent
        {
            get { return m_CompletedPercent; }
            set { m_IsChanged |= (m_CompletedPercent != value); m_CompletedPercent = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime PlanStartDay
        {
            get { return m_PlanStartDay; }
            set { m_IsChanged |= (m_PlanStartDay != value); m_PlanStartDay = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime PlanEndDay
        {
            get { return m_PlanEndDay; }
            set { m_IsChanged |= (m_PlanEndDay != value); m_PlanEndDay = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime ActualStartDay
        {
            get { return m_ActualStartDay; }
            set { m_IsChanged |= (m_ActualStartDay != value); m_ActualStartDay = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public DateTime ActualEndDay
        {
            get { return m_ActualEndDay; }
            set { m_IsChanged |= (m_ActualEndDay != value); m_ActualEndDay = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public int PreTaskNo
        {
            get { return m_PreTaskNo; }
            set { m_IsChanged |= (m_PreTaskNo != value); m_PreTaskNo = value; }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Role
        {
            get { return m_Role; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Role", value, value.ToString());

                m_IsChanged |= (m_Role != value); m_Role = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Resource
        {
            get { return m_Resource; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Resource", value, value.ToString());

                m_IsChanged |= (m_Resource != value); m_Resource = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Remark
        {
            get { return m_Remark; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for Remark", value, value.ToString());

                m_IsChanged |= (m_Remark != value); m_Remark = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string Phase
        {
            get { return m_Phase; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Phase", value, value.ToString());

                m_IsChanged |= (m_Phase != value); m_Phase = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string TaskStatus
        {
            get { return m_TaskStatus; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for TaskStatus", value, value.ToString());

                m_IsChanged |= (m_TaskStatus != value); m_TaskStatus = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>		
        public string PMSName
        {
            get { return m_PMSName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CrName", value, value.ToString());

                m_IsChanged |= (m_PMSName != value); m_PMSName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>		


        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public bool IsChanged
        {
            get { return m_IsChanged; }
        }

        /// <summary>
        /// Returns whether or not the object has changed it's values.
        /// </summary>
        public bool IsDeleted
        {
            get { return m_IsDeleted; }
        }

        #endregion


        #region Public Functions

        /// <summary>
        /// mark the item as deleted
        /// </summary>
        public void MarkAsDeleted()
        {
            m_IsDeleted = true;
            m_IsChanged = true;
        }

        #endregion
    }
}
