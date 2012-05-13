using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using MillerX.RemoteDesktopPlus;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class LoggerTest
	{
		private static readonly string LogFilePath = BuildLogFilePath();

		private static string BuildLogFilePath( )
		{
			string localAppPath = Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData );
			return Path.Combine( localAppPath, "RemoteDesktopPlus\\RemoteDesktopPlus.log" );
		}

		class TestLogger : Logger
		{
			public new void Log( string str )
			{
				base.Log( str );
			}
		}

		[TearDown]
		public void TestTearDown( )
		{
			File.Delete( LogFilePath );
		}

		/// <summary>
		/// Tests writing a new log file.
		/// </summary>
		[Test]
		public void NewLogFile( )
		{
			TestLogger logger = new TestLogger();
			logger.Log( "Hello World" );

			Assert.AreEqual( "Hello World", File.ReadAllText( LogFilePath ) );
		}

		/// <summary>
		/// Tests appending to the log file.
		/// </summary>
		[Test]
		public void AppendToLogFile( )
		{
			File.WriteAllText( LogFilePath, "Existing logs." );

			TestLogger logger = new TestLogger();
			logger.Log( "New logs." );

			Assert.AreEqual( "Existing logs.New logs.", File.ReadAllText( LogFilePath ) );
		}

		/// <summary>
		/// Test that the log file continues to be 32767.
		/// </summary>
		[Test]
		public void LogFileMaxSize( )
		{
			const int MaxSize = 32767;

			// Fill the log file
			const string alpabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			using ( StreamWriter writer = File.CreateText( LogFilePath ) )
			{
				for ( int i = 0; i < MaxSize / alpabet.Length; ++i )
				{
					writer.Write( alpabet );
				}
				writer.Write( alpabet.Substring( 0, MaxSize % alpabet.Length ) );
			}

			const string logStr = "Hello World";
			TestLogger logger = new TestLogger();
			logger.Log( logStr );

			using ( FileStream stream = File.OpenRead( LogFilePath ) )
			{
				Assert.AreEqual( MaxSize, stream.Length );

				// Did we shift the existing contents?
				StreamReader reader = new StreamReader( stream );
				Assert.AreEqual( alpabet[logStr.Length], (char) reader.Read() );
				
				// Did we append the new log?
				stream.Position = MaxSize - logStr.Length;
				reader.DiscardBufferedData();
				Assert.AreEqual( logStr, reader.ReadToEnd() );
			}
		}
	}
}
