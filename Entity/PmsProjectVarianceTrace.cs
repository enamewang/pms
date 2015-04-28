/***********************************************************
 ** File Name : PmsProjectVarianceTrace.cs
 ** Copyright (C) 2014 Qisda Corporation. All rights reserved.
 **
 ** Creator : AIC01/Ename.Wang
 ** Create Date : 2014-01-20
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
	public sealed class PmsProjectVarianceTrace
	{
		#region Private Members
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private string m_Vid; 
		private string m_PmsId; 
		private DateTime m_CacDate; 
		private float m_Sv; 
		private float m_Cv; 
		private DateTime m_CreateDate; 
		private string m_Creator; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PmsProjectVarianceTrace()
		{
			m_Vid = null; 
			m_PmsId = null; 
			m_CacDate = new DateTime(); 
			m_Sv = 0; 
			m_Cv = 0; 
			m_CreateDate = new DateTime(); 
			m_Creator = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 有效位
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
		/// PMSId
		/// </summary>		
		public string PmsId
		{
			get { return m_PmsId; }
			set	
			{
				if( value!= null && value.Length > 15)
					throw new ArgumentOutOfRangeException("Invalid value for PmsId", value, value.ToString());
				
				m_IsChanged |= (m_PmsId != value); m_PmsId = value;
			}
		}
			
		/// <summary>
		/// 计算日期
		/// </summary>		
		public DateTime CacDate
		{
			get { return m_CacDate; }
			set { m_IsChanged |= (m_CacDate != value); m_CacDate = value; }
		}
			
		/// <summary>
		/// 进度偏差
		/// </summary>		
		public float Sv
		{
			get { return m_Sv; }
			set { m_IsChanged |= (m_Sv != value); m_Sv = value; }
		}
			
		/// <summary>
		/// 成本偏差
		/// </summary>		
		public float Cv
		{
			get { return m_Cv; }
			set { m_IsChanged |= (m_Cv != value); m_Cv = value; }
		}
			
		/// <summary>
		/// 创建时间
		/// </summary>		
		public DateTime CreateDate
		{
			get { return m_CreateDate; }
			set { m_IsChanged |= (m_CreateDate != value); m_CreateDate = value; }
		}
			
		/// <summary>
		/// 创建人
		/// </summary>		
		public string Creator
		{
			get { return m_Creator; }
			set	
			{
				if( value!= null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
				
				m_IsChanged |= (m_Creator != value); m_Creator = value;
			}
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