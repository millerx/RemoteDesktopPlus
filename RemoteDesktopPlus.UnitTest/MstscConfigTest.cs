using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class MstscConfigTest
	{
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
            var stream = new MemoryStream();
            var config = new TestMstscConfig();
            config.Update( new[] {
                "abc:s:def",
				"key:s:foo",
				"abc:s:def:xyz",
				"abc:s:",
				"abc::",
				"abc:",
				"abc",
				"" },
                new MstscSettings(), stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "abc:s:def\r\n" +
                "key:s:value\r\n" +
                "abc:s:def:xyz\r\n" +
                "abc:s:\r\n" +
                "abc::\r\n" +
                "abc:\r\n" +
                "abc\r\n" +
                "\r\n",
                reader.ReadToEnd() );
		}

		[Test]
		public void AddKey( )
		{
            var stream = new MemoryStream();
            var config = new TestMstscConfig();
            config.Update( new[] {
                "abc:s:def" },
                new MstscSettings(), stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "abc:s:def\r\n" +
                "key:s:value\r\n",
                reader.ReadToEnd() );
        }

		[Test]
		public void CaseInsensitive( )
		{
            var stream = new MemoryStream();
            var config = new TestMstscConfig();
            config.Update( new[] {
                "KEY:s:foo" },
                new MstscSettings(), stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "KEY:s:value\r\n",
                reader.ReadToEnd() );
        }

		[Test]
		public void WriteInUnicode( )
		{
            var stream = new MemoryStream();
            var config = new TestMstscConfig();
            config.Update( new string[0],
                new MstscSettings(), stream );

            string readConfig = System.Text.Encoding.Unicode.GetString( stream.ToArray() );
            Assert.IsTrue( 0 == string.Compare( "key:s:value\r\n", readConfig ) );
        }

		[Test]
		public void AudioModeSetting( )
		{
            var settings = new MstscSettings();
            settings.Computer = new ComputerName( "chrismillerpc", null );
            settings.AudioMode = AudioMode.ThisComputer;

            var stream = new MemoryStream();
            var config = new XpMstscConfig();
            config.Update( new[] {
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:" },
                settings, stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "audiomode:i:0\r\n" +
                "full address:s:chrismillerpc\r\n" +
                "drivestoredirect:s:\r\n",
                reader.ReadToEnd() );
        }

		[Test]
		public void ComputerNameSetting( )
		{
            var settings = new MstscSettings();
            settings.Computer = new ComputerName( "1.1.1.1", "work" );

            var stream = new MemoryStream();
            var config = new XpMstscConfig();
            config.Update( new[] {
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:" },
                settings, stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "audiomode:i:2\r\n" +
                "full address:s:1.1.1.1\r\n" +
                "drivestoredirect:s:\r\n",
                reader.ReadToEnd() );
        }

		[Test]
		public void DriveSetting_Xp( )
		{
            var settings = new MstscSettings();
            settings.Computer = new ComputerName( "chrismillerpc", null );
            settings.SharedDrives = new[] { 'C' };

            var stream = new MemoryStream();
            var config = new XpMstscConfig();
            config.Update( new[] {
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:" },
                settings, stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "audiomode:i:2\r\n" +
                "full address:s:chrismillerpc\r\n" +
                "drivestoredirect:s:C:;\r\n",
                reader.ReadToEnd() );

            settings.SharedDrives = new[] { 'C', 'D' };

            stream = new MemoryStream();
            config.Update( new[] {
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:" },
                settings, stream );

            reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "audiomode:i:2\r\n" +
                "full address:s:chrismillerpc\r\n" +
                "drivestoredirect:s:C:;D:;\r\n",
                reader.ReadToEnd() );
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
            var settings = new MstscSettings();
            settings.Computer = new ComputerName( "chrismillerpc", null );
            settings.SharedDrives = new[] { 'C', 'D' };

            Dictionary<char, string> volumeNames = new Dictionary<char, string>();
            volumeNames.Add( 'C', "MillerX Stuff (C:)" );
            volumeNames.Add( 'D', "Local Disk (D:)" );

            var stream = new MemoryStream();
            var config = new Win7MstscConfig( new DictionaryVolumeNameProvider( volumeNames ) );
            config.Update( new[] {
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:" },
                settings, stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "audiomode:i:2\r\n" +
                "full address:s:chrismillerpc\r\n" +
                "drivestoredirect:s:MillerX Stuff (C:);Local Disk (D:);\r\n",
                reader.ReadToEnd() );
        }

        [Test]
        public void ResetWindowPos( )
        {
            var stream = new MemoryStream();
            var config = new XpMstscConfig();
            config.ResetWindowsPos( new string[0], stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "screen mode id:i:2\r\n" +
                "desktopwidth:i:1280\r\n" +
                "desktopheight:i:1024\r\n" +
                "session bpp:i:16\r\n" +
                "winposstr:s:0,1,-1280,150,-80,950\r\n",
                reader.ReadToEnd() );
        }

        [Test]
        public void DrivesPlugIn_Xp( )
        {
            var settings = new MstscSettings();
            settings.Computer = new ComputerName( "chrismillerpc", null );
            settings.SharedDrives = new[] { 'C', '&' };

            var stream = new MemoryStream();
            var config = new XpMstscConfig();
            config.Update( new[] {
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:" },
                settings, stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "audiomode:i:2\r\n" +
                "full address:s:chrismillerpc\r\n" +
                "drivestoredirect:s:C:;\r\n",
                reader.ReadToEnd() );
        }

        [Test]
        public void DrivesPlugIn_Win7( )
        {
            var settings = new MstscSettings();
            settings.Computer = new ComputerName( "chrismillerpc", null );
            settings.SharedDrives = new[] { 'C', '&' };

            Dictionary<char, string> volumeNames = new Dictionary<char, string>();
            volumeNames.Add( 'C', "MillerX Stuff (C:)" );
            volumeNames.Add( '&', "Not a drive" );

            var stream = new MemoryStream();
            var config = new Win7MstscConfig( new DictionaryVolumeNameProvider( volumeNames ) );
            config.Update( new[] {
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:" },
                settings, stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "audiomode:i:2\r\n" +
                "full address:s:chrismillerpc\r\n" +
                "drivestoredirect:s:MillerX Stuff (C:);DynamicDrives;\r\n",
                reader.ReadToEnd() );
        }

        [Test]
        public void DrivesAll_Xp( )
        {
            var settings = new MstscSettings();
            settings.Computer = new ComputerName( "chrismillerpc", null );
            settings.SharedDrives = new[] { '*' };

            var stream = new MemoryStream();
            var config = new XpMstscConfig();
            config.Update( new[] {
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:" },
                settings, stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreNotEqual(
                "audiomode:i:2\r\n" +
                "full address:s:chrismillerpc\r\n" +
                "drivestoredirect:s:*:;\r\n",
                reader.ReadToEnd() );
        }

        [Test]
        public void DrivesAll_Win7( )
        {
            var settings = new MstscSettings();
            settings.Computer = new ComputerName( "chrismillerpc", null );
            settings.SharedDrives = new[] { '*' };

            Dictionary<char, string> volumeNames = new Dictionary<char, string>();
            volumeNames.Add( 'C', "MillerX Stuff (C:)" );
            volumeNames.Add( '*', "Not a drive" );

            var stream = new MemoryStream();
            var config = new Win7MstscConfig( new DictionaryVolumeNameProvider( volumeNames ) );
            config.Update( new[] {
				"audiomode:i:2",
				"full address:s:",
				"drivestoredirect:s:" },
                settings, stream );

            var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
            Assert.AreEqual(
                "audiomode:i:2\r\n" +
                "full address:s:chrismillerpc\r\n" +
                "drivestoredirect:s:*\r\n",
                reader.ReadToEnd() );
        }
    }
}
