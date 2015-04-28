/***********************************************************
 ** File Name : TmpSdpImportdetail.cs
 ** Copyright (C) 2014 Qisda Corporation. All rights reserved.
 **
 ** Creator : AIC01/ITO.Abel.Li
 ** Create Date : 2014-01-15
 ** Modifier :
 ** Modify Date :
 **
 ** Description:
 **
 ***********************************************************/
using System;

namespace Qisda.PMS.Entity
{
	/// <summary>
	///	Generated by MyGeneration using the IBatis Object Mapping template
	/// </summary>
	[Serializable]
	public sealed class TmpSdpImportdetail
	{
		#region Private Members
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private string m_Vid; 
		private int m_Serial; 
		private string m_Pmsid; 
		private string m_Parentno; 
		private string m_Wbs; 
		private string m_Phase; 
		private int m_TaskType; 
		private string m_TaskName; 
		private float m_Plancost; 
		private DateTime m_Planstartday; 
		private DateTime m_Planendday; 
		private string m_Role; 
		private string m_Resource; 
		private int m_FunctionType; 
		private int m_OperationType; 
		private int m_TaskComplexity; 
		private int m_ProgramLanguage; 
		private string m_Flag;
        private string m_FunctionTypeDesc;
        private string m_OperationTypeDesc;
        private string m_TaskTypeDesc;
        private string m_TaskComplexityDesc;
        private string m_ProgramLanguageDesc;

		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public TmpSdpImportdetail()
		{
			m_Vid = null; 
			m_Serial = 0; 
			m_Pmsid = null; 
			m_Parentno = null; 
			m_Wbs = null; 
			m_Phase = null; 
			m_TaskType = 0; 
			m_TaskName = null; 
			m_Plancost = 0; 
			m_Planstartday = new DateTime(); 
			m_Planendday = new DateTime(); 
			m_Role = null; 
			m_Resource = null; 
			m_FunctionType = 0; 
			m_OperationType = 0; 
			m_TaskComplexity = 0; 
			m_ProgramLanguage = 0; 
			m_Flag = null;
            m_FunctionTypeDesc = "";
            m_OperationTypeDesc = "";
            m_TaskTypeDesc = "";
            m_TaskComplexityDesc = "";
            m_ProgramLanguageDesc = ""; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public string Vid
		{
			get { return m_Vid; }
			set	
			{
				if( value!= null && value.Length > 2)
					throw new ArgumentOutOfRangeException("Invalid value for Vid", value, value.ToString());
				
				m_IsChanged |= (m_Vid != value); m_Vid = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public int Serial
		{
			get { return m_Serial; }
			set { m_IsChanged |= (m_Serial != value); m_Serial = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Pmsid
		{
			get { return m_Pmsid; }
			set	
			{
				if( value!= null && value.Length > 15)
					throw new ArgumentOutOfRangeException("Invalid value for Pmsid", value, value.ToString());
				
				m_IsChanged |= (m_Pmsid != value); m_Pmsid = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Parentno
		{
			get { return m_Parentno; }
			set	
			{
				if( value!= null && value.Length > 8)
					throw new ArgumentOutOfRangeException("Invalid value for Parentno", value, value.ToString());
				
				m_IsChanged |= (m_Parentno != value); m_Parentno = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Wbs
		{
			get { return m_Wbs; }
			set	
			{
				if( value!= null && value.Length > 8)
					throw new ArgumentOutOfRangeException("Invalid value for Wbs", value, value.ToString());
				
				m_IsChanged |= (m_Wbs != value); m_Wbs = value;
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
				if( value!= null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Phase", value, value.ToString());
				
				m_IsChanged |= (m_Phase != value); m_Phase = value;
			}
		}
			
		/// <summary>
		/// 任务类型
		/// </summary>		
		public int TaskType
		{
			get { return m_TaskType; }
			set { m_IsChanged |= (m_TaskType != value); m_TaskType = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string TaskName
		{
			get { return m_TaskName; }
			set	
			{
				if( value!= null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for TaskName", value, value.ToString());
				
				m_IsChanged |= (m_TaskName != value); m_TaskName = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public float Plancost
		{
			get { return m_Plancost; }
			set { m_IsChanged |= (m_Plancost != value); m_Plancost = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public DateTime Planstartday
		{
			get { return m_Planstartday; }
			set { m_IsChanged |= (m_Planstartday != value); m_Planstartday = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public DateTime Planendday
		{
			get { return m_Planendday; }
			set { m_IsChanged |= (m_Planendday != value); m_Planendday = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Role
		{
			get { return m_Role; }
			set	
			{
				if( value!= null && value.Length > 40)
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
				if( value!= null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Resource", value, value.ToString());
				
				m_IsChanged |= (m_Resource != value); m_Resource = value;
			}
		}
			
		/// <summary>
		/// 功能分类
		/// </summary>		
		public int FunctionType
		{
			get { return m_FunctionType; }
			set { m_IsChanged |= (m_FunctionType != value); m_FunctionType = value; }
		}
			
		/// <summary>
		/// 作业方式
		/// </summary>		
		public int OperationType
		{
			get { return m_OperationType; }
			set { m_IsChanged |= (m_OperationType != value); m_OperationType = value; }
		}
			
		/// <summary>
		/// 任务复杂度
		/// </summary>		
		public int TaskComplexity
		{
			get { return m_TaskComplexity; }
			set { m_IsChanged |= (m_TaskComplexity != value); m_TaskComplexity = value; }
		}
			
		/// <summary>
		/// 编程语言
		/// </summary>		
		public int ProgramLanguage
		{
			get { return m_ProgramLanguage; }
			set { m_IsChanged |= (m_ProgramLanguage != value); m_ProgramLanguage = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string Flag
		{
			get { return m_Flag; }
			set	
			{
				if( value!= null && value.Length > 2)
					throw new ArgumentOutOfRangeException("Invalid value for Flag", value, value.ToString());
				
				m_IsChanged |= (m_Flag != value); m_Flag = value;
			}
		}
        /// <summary>
        /// 功能分类说明
        /// </summary>		
        public string FunctionTypeDesc
        {
            get { return m_FunctionTypeDesc; }
            set { m_IsChanged |= (m_FunctionTypeDesc != value); m_FunctionTypeDesc = value; }
        }

        /// <summary>
        /// 作业方式说明
        /// </summary>		
        public string OperationTypeDesc
        {
            get { return m_OperationTypeDesc; }
            set { m_IsChanged |= (m_OperationTypeDesc != value); m_OperationTypeDesc = value; }
        }

        /// <summary>
        /// 任务类型说明
        /// </summary>		
        public string TaskTypeDesc
        {
            get { return m_TaskTypeDesc; }
            set { m_IsChanged |= (m_TaskTypeDesc != value); m_TaskTypeDesc = value; }
        }

        /// <summary>
        /// 任务复杂度说明
        /// </summary>		
        public string TaskComplexityDesc
        {
            get { return m_TaskComplexityDesc; }
            set { m_IsChanged |= (m_TaskComplexityDesc != value); m_TaskComplexityDesc = value; }
        }

        /// <summary>
        /// 编程语言说明
        /// </summary>		
        public string ProgramLanguageDesc
        {
            get { return m_ProgramLanguageDesc; }
            set { m_IsChanged |= (m_ProgramLanguageDesc != value); m_ProgramLanguageDesc = value; }
        }			
			
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