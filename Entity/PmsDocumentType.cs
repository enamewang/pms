/***********************************************************
 ** File Name : PmsDocumentType.cs
 ** Copyright (C) 2011 Qisda Corporation. All rights reserved.
 **
 ** Creator : HI1/CJ.Chen
 ** Create Date : 2011-01-17
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
	public sealed class PmsDocumentType
	{
		#region Private Members
		private bool m_IsChanged;
		private bool m_IsDeleted;
		private string m_Vid; 
		private int m_TypeId; 
		private string m_TypeName; 
		private string m_Creator; 
		private DateTime m_CreateDate; 		
		#endregion
		
		#region Default ( Empty ) Class Constuctor
		/// <summary>
		/// default constructor
		/// </summary>
		public PmsDocumentType()
		{
			m_Vid = null; 
			m_TypeId =0;
            m_TypeName = null;
            m_Creator = null; 
			m_CreateDate = new DateTime(); 
		}
		#endregion // End of Default ( Empty ) Class Constuctor
		
		#region Public Properties
			
		/// <summary>
		/// 
		/// </summary>		
		public string Vid
		{
			get { return m_Vid; }
			set { m_IsChanged |= (m_Vid != value); m_Vid = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public int TypeId
		{
			get { return m_TypeId; }
			set { m_IsChanged |= (m_TypeId != value); m_TypeId = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string TypeName
		{
			get { return m_TypeName; }
			set { m_IsChanged |= (m_TypeName != value); m_TypeName = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public string  Creator
		{
			get { return m_Creator; }
			set { m_IsChanged |= (m_Creator != value); m_Creator = value; }
		}
			
		/// <summary>
		/// 
		/// </summary>		
		public DateTime CreateDate
		{
			get { return m_CreateDate; }
			set { m_IsChanged |= (m_CreateDate != value); m_CreateDate = value; }
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
