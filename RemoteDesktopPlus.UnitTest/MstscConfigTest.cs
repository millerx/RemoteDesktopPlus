using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class MstscConfigTest
	{
		private string m_filePath = System.IO.Path.GetTempFileName();

		[TestFixtureTearDown]
		public void TestFixtureTearDown( )
		{
			File.Delete( m_filePath );
		}

		class TestMstscConfig : MstscConfig
		{
			protected override void SetUpdateDict( MstscSettings settings, Dictionary<string, string> dict )
			{
				dict["key:s:"] = "value";
			}
		}

		[Test]
		public void BasicReplacement( )
		{
			File.WriteAllLines( m_filePath, new string[]
			{
				"abc:s:def",
				"key:s:foo",
				"abc:s:def:xyz",
				"abc:s:",
				"abc::",
				"abc:",
				"abc",
				"",
			} );

			TestMstscConfig config = new TestMstscConfig();
			config.Update( m_filePath, new MstscSettings() );

			Assert.AreEqual(
				"abc:s:def\r\n" +
				"key:s:value\r\n" +
				"abc:s:def:xyz\r\n" +
				"abc:s:\r\n" +
				"abc::\r\n" +
				"abc:\r\n" +
				"abc\r\n" +
				"\r\n",
				File.ReadAllText( m_filePath ) );
		}

		[Test]
		public void AddKey( )
		{
			File.WriteAllLines( m_filePath, new string[]
			{
				"abc:s:def"
			} );

			TestMstscConfig config = new TestMstscConfig();
			config.Update( m_filePath, new MstscSettings() );

			Assert.AreEqual(
				"abc:s:def\r\n" +
				"key:s:value\r\n",
				File.ReadAllText( m_filePath ) );
		}

		[Test]
		public void CaseInsensitive( )
		{
			File.WriteAllLines( m_filePath, new string[]
			{
				"KEY:s:foo"
			} );

			TestMstscConfig config = new TestMstscConfig();
			config.Update( m_filePath, new MstscSettings() );

			Assert.AreEqual(
				"KEY:s:value\r\n",
				File.ReadAllText( m_filePath ) );
		}

		[Test]
		public void WriteInUnicode( )
		{
			File.WriteAllText( m_filePath, "" );

			TestMstscConfig config = new TestMstscConfig();
			config.Update( m_filePath, new MstscSettings() );

			Assert.AreEqual( "key:s:value\r\n",
				System.Text.Encoding.Unicode.GetString(
					File.ReadAllBytes( m_filePath ) ) );
		}

		[Test]
		public void AudioModeSetting( )
		{
			File.WriteAllLines( m_filePath, new string[]
			{
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:"
			} );

			MstscSettings settings = new MstscSettings();
			settings.Computer = new ComputerName( "chrismillerpc", null );
			settings.AudioMode = AudioMode.ThisComputer;

			XpMstscConfig config = new XpMstscConfig();
			config.Update( m_filePath, settings );

			Assert.AreEqual(
				"audiomode:i:0\r\n" +
				"full address:s:chrismillerpc\r\n" +
				"drivestoredirect:s:\r\n",
				File.ReadAllText( m_filePath ) );
		}

		[Test]
		public void ComputerNameSetting( )
		{
			File.WriteAllLines( m_filePath, new string[]
			{
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:"
			} );

			MstscSettings settings = new MstscSettings();
			settings.Computer = new ComputerName( "1.1.1.1", "work" );

			XpMstscConfig config = new XpMstscConfig();
			config.Update( m_filePath, settings );

			Assert.AreEqual(
				"audiomode:i:2\r\n" +
				"full address:s:1.1.1.1\r\n" +
				"drivestoredirect:s:\r\n",
				File.ReadAllText( m_filePath ) );
		}

		[Test]
		public void DriveSetting_Xp( )
		{
			File.WriteAllLines( m_filePath, new string[]
			{
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:"
			} );

			MstscSettings settings = new MstscSettings();
			settings.Computer = new ComputerName( "chrismillerpc", null );
			settings.SharedDrives.Add( 'C' );

			XpMstscConfig config = new XpMstscConfig();
			config.Update( m_filePath, settings );

			Assert.AreEqual(
				"audiomode:i:2\r\n" +
				"full address:s:chrismillerpc\r\n" +
				"drivestoredirect:s:C:;\r\n",
				File.ReadAllText( m_filePath ) );

			settings = new MstscSettings();
			settings.Computer = new ComputerName( "chrismillerpc", null );
			settings.SharedDrives.Add( 'C' );
			settings.SharedDrives.Add( 'D' );

			config = new XpMstscConfig();
			config.Update( m_filePath, settings );

			Assert.AreEqual(
				"audiomode:i:2\r\n" +
				"full address:s:chrismillerpc\r\n" +
				"drivestoredirect:s:C:;D:;\r\n",
				File.ReadAllText( m_filePath ) );
		}

		class DictionaryVolumeNameProvider : Win7MstscConfig.IVolumeNameProvider
		{
			private Dictionary<char, string> m_dictionary;

			public DictionaryVolumeNameProvider( Dictionary<char, string> dict )
			{
				m_dictionary = dict;
			}

			public void Dispose( )
			{
			}

			public string GetVolumeName( char driveLetter )
			{
				return m_dictionary[driveLetter];
			}
		}

		[Test]
		public void DriveSetting_Win7( )
		{
			File.WriteAllLines( m_filePath, new string[]
			{
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:"
			} );

			MstscSettings settings = new MstscSettings();
			settings.Computer = new ComputerName( "chrismillerpc", null );
			settings.SharedDrives.Add( 'C' );
			settings.SharedDrives.Add( 'D' );

			Dictionary<char, string> volumeNames = new Dictionary<char, string>();
			volumeNames.Add( 'C', "MillerX Stuff (C:)" );
			volumeNames.Add( 'D', "Local Disk (D:)" );

			Win7MstscConfig config = new Win7MstscConfig( new DictionaryVolumeNameProvider( volumeNames ) );
			config.Update( m_filePath, settings );

			Assert.AreEqual(
				"audiomode:i:2\r\n" +
				"full address:s:chrismillerpc\r\n" +
				"drivestoredirect:s:MillerX Stuff (C:);Local Disk (D:);\r\n",
				File.ReadAllText( m_filePath ) );
		}
	}
}
