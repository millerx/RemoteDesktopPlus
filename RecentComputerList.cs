using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace MillerX.RemoteDesktopPlus
{
	/// <summary>
	/// Reads the list of computers we have seen before from the registry.
	/// </summary>
	public class RecentComputerList : ICollection<ComputerName>
	{
		private List<ComputerName> m_list;

		public int MaxComputerCount { get; set; }
		public int MaxIpAddressCount { get; set; }

		public RecentComputerList( )
			: this( Config.MaxComputerCount, Config.MaxIpAddressCount )
		{
		}

		public RecentComputerList( int maxComputers, int maxIpAddresses )
		{
			Initialize( maxComputers, maxIpAddresses );
		}

		private void Initialize( int maxComputers, int maxIpAddresses )
		{
			this.MaxComputerCount = maxComputers;
			this.MaxIpAddressCount = maxIpAddresses;
			m_list = new List<ComputerName>( this.MaxComputerCount );
		}

		public RecentComputerList( RecentComputerList computers )
		{
			Initialize( computers.MaxComputerCount, computers.MaxIpAddressCount );

			// Do a deep-copy of the elements.
			foreach ( ComputerName name in computers )
			{
				m_list.Add( new ComputerName( name.Computer, name.Alias, name.AdminMode ) );
			}
		}

		/// <summary>
		/// Adds a computer to the end of the list.  For initialization only.  Simple rule if the
		/// list is full.  The computer is not added and false is returned.
		/// </summary>
		public bool Add( ComputerName computer )
		{
			if ( m_list.Count >= MaxComputerCount )
				return false;

			m_list.Add( computer );
			return true;
		}

		/// <summary>
		/// Push a new computer into the list.  If list is full then MaxIpAddresses is enforced
		/// unless more were read from the registry.
		/// </summary>
		public void Push( ComputerName computer )
		{
			int removeIndex = GetRemoveIndex( computer );

			// Ideally we would keep the computers in a linked-list and just update the linked list
			// however it LinkedList<> class has no RemoveAt(index) method which is exactly what we
			// need when we have an IP adress to remove.  So instead I just build a new list.  It's
			// more efficient than manipulating the existing list.

			List<ComputerName> newList = new List<ComputerName>( MaxComputerCount );
			newList.Add( computer );

			for ( int i = 0; i < m_list.Count; ++i )
			{
				// Don't add the item at removeIndex
				if ( i == removeIndex )
					continue;

				// Remove duplicate alias
				if ( m_list[i].EqualsAlias( computer.Alias ) )
					m_list[i].Alias = null;

				newList.Add( m_list[i] );
			}

			m_list = newList;
		}

		/// <summary>
		/// Gets the index of the item that should be removed during Push().  Returns m_List.Count
		/// if nothing should be removed.
		/// </summary>
		private int GetRemoveIndex( ComputerName newComputer )
		{
			Regex regex = new Regex( ComputerName.IpAddressRegexPattern );
			int ipCount = 0;
			int lastIpIndex = -1;

			if ( regex.IsMatch( newComputer.Computer ) )
				++ipCount;

			for ( int i = 0; i < m_list.Count; ++i )
			{
				// Avoid duplicates
				if ( newComputer.EqualsComputer( m_list[i].Computer ) )
					return i;

				if ( regex.IsMatch( m_list[i].Computer ) )
				{
					++ipCount;
					lastIpIndex = i;
				}
			}

			if ( m_list.Count < MaxComputerCount )
				return m_list.Count; // Don't remove anything
			if ( ipCount > this.MaxIpAddressCount )
				return lastIpIndex;
			else
				return m_list.Count - 1;
		}

		/// <summary>
		/// Finds a ComputerName from the given name.  Tries to match alias first then computer.
		/// Returns null if not found.
		/// </summary>
		public ComputerName Find( string name )
		{
			foreach ( ComputerName computer in m_list )
			{
				if ( computer.EqualsAlias( name ) )
					return computer;
			}

			foreach ( ComputerName computer in m_list )
			{
				if ( computer.EqualsComputer( name ) )
					return computer;
			}

			return null;
		}

		/// <summary>
		/// Gets all the aliases in the list.
		/// </summary>
		public IEnumerable<string> GetAliases( )
		{
			return from n in m_list
				   where n.Alias != null
				   select n.Alias;
		}

		#region ICollection<ComputerName> Members
		void ICollection<ComputerName>.Add( ComputerName item )
		{
			// Calls the "bool Add( ComputerName )" signature.
			this.Add( item );
		}

		public void Clear( )
		{
			m_list.Clear();
		}

		public bool Contains( ComputerName item )
		{
			return m_list.Contains( item );
		}

		void ICollection<ComputerName>.CopyTo( ComputerName[] array, int arrayIndex )
		{
			m_list.CopyTo( array, arrayIndex );
		}

		public int Count
		{
			get { return m_list.Count; }
		}

		bool ICollection<ComputerName>.IsReadOnly
		{
			get { return false; }
		}

		public bool Remove( ComputerName item )
		{
			return m_list.Remove( item );
		}

		public IEnumerator<ComputerName> GetEnumerator( )
		{
			return m_list.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator( )
		{
			return m_list.GetEnumerator();
		}
		#endregion
	}
}
