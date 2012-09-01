using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using MillerX.RemoteDesktopPlus;

// TODO:  Look for calls to the serializer that are reading from disk.

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class ComputerListFileTest
	{
		[Test]
		public void WriteFile( )
		{
			RecentComputerList computers = new RecentComputerList();
			computers.Add( new ComputerName( "1.1.1.1", "alias", true ) );
			computers.Add( new ComputerName( "work", null, false ) );

			StringWriter writer = new StringWriter();
			ComputerListFile serializer = new ComputerListFile();
			serializer.Write( writer, computers );

			Assert.AreEqual(
				"1.1.1.1\talias\tTrue\r\n" +
				"work\t\tFalse\r\n",
				writer.ToString() );
		}

		[Test]
		public void ReadFile( )
		{
			StringReader reader = new StringReader(
				"1.1.1.1\talias\tTrue\r\n" +
				"work\t\tFalse\r\n" +
				// The "classic" style, computer \t alias
				"2.2.2.2\tclassic\r\n" +
				"3.3.3.3\t\r\n" +
				"\r\n" +
				"4.4.4.4" );

			ComputerListFile serializer = new ComputerListFile();
			RecentComputerList computers = serializer.Read( reader );

			ComputerName[] cnames = new ComputerName[]
			{
				new ComputerName( "1.1.1.1", "alias", true ),
				new ComputerName( "work", null, false ),
				new ComputerName( "2.2.2.2", "classic", false ),
				new ComputerName( "3.3.3.3", null, false ),
				new ComputerName( "4.4.4.4", null, false )
			};
			Assert.AreEqual( cnames, computers.ToArray() );
		}
	}
}
