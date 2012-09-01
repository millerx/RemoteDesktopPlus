using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class ComputerNameTest
	{
		[Test]
		public void Ctor( )
		{
			Assert.IsFalse( new ComputerName( "computer", "alias" ).AdminMode );
			Assert.IsNull( new ComputerName( "computer", "" ).Alias );
			Assert.IsNull( new ComputerName( "computer", null ).Alias );
		}

		[Test]
		public void EqualsCaseInsensitive( )
		{
			var computer = new ComputerName( "foo", "bar" );

			Assert.IsTrue( computer.EqualsComputer( "foo" ) );
			Assert.IsTrue( computer.EqualsComputer( "FOO" ) );
			Assert.IsFalse( computer.EqualsComputer( "bar" ) );

			Assert.IsTrue( computer.EqualsAlias( "bar" ) );
			Assert.IsTrue( computer.EqualsAlias( "BAR" ) );
			Assert.IsFalse( computer.EqualsAlias( "foo" ) );
		}

		[Test]
		public void Equals( )
		{
			var computer = new ComputerName( "1.1.1.1", "work", true );

			Assert.IsTrue( computer.Equals( new ComputerName( "1.1.1.1", "work", true ) ) );
			Assert.IsFalse( computer.Equals( new ComputerName( "2.2.2.2", "work", true ) ) );
			Assert.IsFalse( computer.Equals( new ComputerName( "1.1.1.1", "home", true ) ) );
			Assert.IsFalse( computer.Equals( new ComputerName( "1.1.1.1", "work", false ) ) );
		}

		[Test]
		public void TestToString( )
		{
			Assert.AreEqual( "computer", new ComputerName( "computer", null ).ToString() );
			Assert.AreEqual( "alias", new ComputerName( "computer", "alias" ).ToString() );
		}

		/// <summary>
		/// Tests IP address regex that use to be built-in.
		/// </summary>
		[Test]
		public void IsIpAddress( )
		{
			var regex = new Regex( ComputerName.IpAddressRegexPattern );

			Assert.IsTrue( regex.IsMatch( "1.1.1.1" ) );
			Assert.IsFalse( regex.IsMatch( "computer" ) );
			Assert.IsFalse( regex.IsMatch( "1.1" ) );
			Assert.IsFalse( regex.IsMatch( "1.1.1.a" ) );
			Assert.IsFalse( regex.IsMatch( "" ) );
		}

		/// <summary>
		/// Tests devlab server regex that use to be built-in.
		/// </summary>
		[Test]
		public void IsDevlabServer( )
		{
			var regex = new Regex( ComputerName.DevlabServerRegexPattern );

			Assert.IsFalse( regex.IsMatch( "computer" ) );
			Assert.IsTrue( regex.IsMatch( "clay1" ) );
			Assert.IsTrue( regex.IsMatch( "CLAY1" ) );
			Assert.IsFalse( regex.IsMatch( "" ) );
			Assert.IsFalse( regex.IsMatch( "1.1.1.1" ) );
		}
	}
}
