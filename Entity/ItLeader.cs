/***********************************************************
 ** File Name : ItLeader.cs
 ** Copyright (C) 2011 Qisda Corporation. All rights reserved.
 **
 ** Creator : AIC01/Kite.Zhang
 ** Create Date : 2011-08-08
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
	public sealed class ItLeader
	{
		#region Private Members
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private string m_PmNo; 
		private string m_LeaderNo; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public ItLeader()
		{
			m_PmNo = null; 
			m_LeaderNo = null; 
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public string PmNo
		{
			get { return m_PmNo; }
			set	
			{
				if( value!= null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for PmNo", value, value.ToString());
				
				m_IsChanged |= (m_PmNo != value); m_PmNo = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string LeaderNo
		{
			get { return m_LeaderNo; }
			set	
			{
				if( value!= null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for LeaderNo", value, value.ToString());
				
				m_IsChanged |= (m_LeaderNo != value); m_LeaderNo = value;
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