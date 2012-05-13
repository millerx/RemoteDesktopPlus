using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using MillerX.RemoteDesktopPlus;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class ConfigFileTest
	{
		[Test]
		public void ReadTest( )
		{
			string inputStr =
				"MaxComputerCount:1\r\n" +
				"MaxIpAddressCount:2\r\n" +
				"MaxComboItems:3\r\n";

			using ( StringReader reader = new StringReader( inputStr ) )
			{
				ConfigFileSerializer serializer = new ConfigFileSerializer();
				serializer.Read( reader );
			}

			Assert.AreEqual( 1, Config.MaxComputerCount );
			Assert.AreEqual( 2, Config.MaxIpAddressCount );
			Assert.AreEqual( 3, Config.MaxComboItems );
		}
	}
}
