using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MillerX.RemoteDesktopPlus
{
	/// <summary>
	/// String pair for a computer and it's alias.
	/// </summary>
	public class ComputerName
	{
		public const string IpAddressRegexPattern = @"\d+\.\d+\.\d+\.\d+";

		/// <summary>
		/// Name of the computer or an IP address.
		/// </summary>
		public string Computer;

		/// <summary>
		/// Alias to the computer name - typically when it's an IP address.
		/// </summary>
		public string Alias;

		/// <summary>
		/// Was the last time we logged in with admin mode?
		/// </summary>
		public bool AdminMode;

		public ComputerName( string computer, string alias )
		{
			Initialize( computer, alias, false );
		}

		public ComputerName( string computer, string alias, bool adminMode )
		{
			Initialize( computer, alias, adminMode );
		}

		private void Initialize( string computer, string alias, bool adminMode )
		{
			this.Computer = computer;
			// Treat empty string as null.
			this.Alias = string.IsNullOrEmpty( alias ) ? null : alias;
			this.AdminMode = adminMode;
		}

		public bool EqualsComputer( string computer )
		{
			return string.Equals( this.Computer, computer, StringComparison.OrdinalIgnoreCase );
		}

		public bool EqualsAlias( string alias )
		{
			return string.Equals( this.Alias, alias, StringComparison.OrdinalIgnoreCase );
		}

		public override bool Equals( object obj )
		{
			ComputerName name = obj as ComputerName;
			if ( name == null )
				return false;

			return EqualsComputer( name.Computer ) &&
					EqualsAlias( name.Alias ) &&
					this.AdminMode == name.AdminMode;
		}

		// Just overrode this to avoid the warning
		public override int GetHashCode( )
		{
			return base.GetHashCode();
		}

		public override string ToString( )
		{
			return this.Alias ?? this.Computer;
		}

		public static bool IsIpAddress( string str )
		{
			return Regex.IsMatch( str, IpAddressRegexPattern );
		}

		public static ComputerName BuildComputerName( string computerName, string aliasName, RecentComputerList computerList )
		{
			ComputerName computer = computerList.Find( computerName );
			if ( computer == null )
			{
				computer = new ComputerName( computerName, aliasName );

				if ( computer.Alias != null )
				{
					// If the alias has an IP address then we probably mistyped and intended for the
					// IP address to go in the computer combo box.
					if ( ComputerName.IsIpAddress( computer.Alias ) )
					{
						computer = new ComputerName( computer.Alias, computer.Computer );
					}
				}
			}
			else if ( computer.EqualsAlias( computerName ) )
			{
				// There is some ambiguity in the UI.  If you have an alias in the Computer Name drop-down
				// and a name in the Alias textbox then don't do anything with the Alias textbox.
			}
			else if ( aliasName != "" )
			{
				// We still set the alias in-case we are trying to change the computer name the alias is associated with.
				computer.Alias = aliasName;
			}

			return computer;
		}
	}
}