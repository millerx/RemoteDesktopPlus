using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class ComputerNameTest
	{
		/// <summary>
		/// Tests unaliasing the computer name.
		/// </summary>
		[Test]
		public void BuildComputer( )
		{
			ComputerName computer;

			RecentComputerList computerList = new RecentComputerList();
			computerList.Add( new ComputerName( "1.1.1.1", "alias" ) );

			// Unalias
			computer = ComputerName.BuildComputerName( "alias", "", computerList );
			Assert.AreEqual( new ComputerName( "1.1.1.1", "alias" ), computer );

			// Retrieve computer and alias given just the computer name.
			computer = ComputerName.BuildComputerName( "1.1.1.1", "", computerList );
			Assert.AreEqual( new ComputerName( "1.1.1.1", "alias" ), computer );

			// Change alias on an existing item.
			computer = ComputerName.BuildComputerName( "1.1.1.1", "foobar", computerList );
			Assert.AreEqual( new ComputerName( "1.1.1.1", "foobar" ), computer );

			// Create a new value if not found in the RecentComputerList.
			computer = ComputerName.BuildComputerName( "2.2.2.2", "OtherAlias", computerList );
			Assert.AreEqual( new ComputerName( "2.2.2.2", "OtherAlias" ), computer );
		}

		[Test]
		public void Equals( )
		{
			var name = new ComputerName( "1.1.1.1", "work", true );

			Assert.IsTrue(  name.Equals( new ComputerName( "1.1.1.1", "work", true )));
			Assert.IsFalse( name.Equals( new ComputerName( "2.2.2.2", "work", true )));
			Assert.IsFalse( name.Equals( new ComputerName( "1.1.1.1", "home", true )));
			Assert.IsFalse( name.Equals( new ComputerName( "1.1.1.1", "work", false )));
		}

		/// <summary>
		/// There is some ambiguity in the UI.  If you have an alias in the Computer Name drop-down
		/// and a name in the Alias textbox then don't do anything with the Alias textbox.
		/// </summary>
		[Test]
		public void DontAliasAnAlias( )
		{
			RecentComputerList computerList = new RecentComputerList();
			computerList.Add( new ComputerName( "1.1.1.1", "MyAlias" ) );

			ComputerName computer = ComputerName.BuildComputerName( "MyAlias", "OtherAlias", computerList );
			Assert.AreEqual( new ComputerName( "1.1.1.1", "MyAlias" ), computer );
		}

		/// <summary>
		/// Tests that if we put an IP address as an alias, RDP assumes user error and flips the computer name and alias.
		/// </summary>
		[Test]
		public void FlipComputerNameAndAlias( )
		{
			ComputerName computer;
			RecentComputerList computerList = new RecentComputerList();

			// 1.1.1.1 was intended to be the computer name but we mistyped.
			computer = ComputerName.BuildComputerName( "alias", "1.1.1.1", computerList );
			Assert.AreEqual( new ComputerName( "1.1.1.1", "alias" ), computer );

			// Do we handle not having an alias.
			computer = ComputerName.BuildComputerName( "chrismillerpc", "", computerList );
			Assert.AreEqual( new ComputerName( "chrismillerpc", null ), computer );
		}
	}
}
