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
				"MaxMatchCount:2\r\n" +
				"MaxComboItems:3\r\n" +
				"ResetSize:1280,1024\r\n" +
				"ResetPosition:0,1,-1280,150,-80,950\r\n" +
				"ResetBpp:16\r\n" +
				// "Regex*" should be added to the Regex string array.
				@"Regex_Ip:\d+\.\d+\.\d+\.\d+" + "\r\n" +
				@"Regex_Devlab:[a-zA-Z]+\d+" + "\r\n";

			using ( StringReader reader = new StringReader( inputStr ) )
			{
				ConfigFileSerializer serializer = new ConfigFileSerializer();
				serializer.Read( reader );
			}

			Assert.AreEqual( 1, Config.MaxComputerCount );
			Assert.AreEqual( 2, Config.MaxMatchCount );
			Assert.AreEqual( 3, Config.MaxComboItems );
			Assert.AreEqual( new System.Drawing.Size( 1280, 1024 ), Config.ResetSize );
			Assert.AreEqual( "0,1,-1280,150,-80,950", Config.ResetPosition );
			Assert.AreEqual( 16, Config.ResetBpp );
			Assert.AreEqual( @"\d+\.\d+\.\d+\.\d+", Config.Regex[0] );
			Assert.AreEqual( @"[a-zA-Z]+\d+", Config.Regex[1] );
		}
	}
}
