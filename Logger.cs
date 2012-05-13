using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MillerX.RemoteDesktopPlus
{
	/// <summary>
	/// Logs events to a text file.
	/// </summary>
	public class Logger
	{
		/// <summary>
		/// Logs the current computer list.
		/// </summary>
		public static void LogComputerList( string message, RecentComputerList computerList )
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat( "{0} {1}:\r\n", DateTime.Now, message );
			foreach ( ComputerName computer in computerList )
			{
				sb.AppendLine( computer.ToString() );
			}

			Logger logger = new Logger();
			logger.Log( sb.ToString() );
		}

		public static void LogAddComputer( ComputerName computer )
		{
			LogString( string.Format( "Adding computer: {0} ({1})",
				computer.Computer, computer.Alias ) );
		}

		public static void LogException( Exception ex )
		{
			LogString( ex.ToString() );
		}

		public static void LogString( string message )
		{
			Logger logger = new Logger();
			logger.Log( string.Format( "{0} {1}\r\n", DateTime.Now, message ) );
		}

		private const string Filename = "RemoteDesktopPlus.log";

		private const int MaxSize = 32767;

		private static object m_SyncObject = new object();

		private static string GetFilePath( )
		{
			return Path.Combine( Program.LocalAppPath, Filename );
		}

		protected Logger( )
		{
		}

		/// <summary>
		/// Writes a log entry.
		/// </summary>
		protected void Log( string eventStr )
		{
			lock ( m_SyncObject )
			{
				try
				{
					Directory.CreateDirectory( Program.LocalAppPath );

					using ( System.IO.FileStream stream = System.IO.File.Open(
						GetFilePath(), FileMode.OpenOrCreate, FileAccess.ReadWrite ) )
					{
						// Read enough from the log file so that when we write it again it doesn't exceed MaxSize.
						stream.Position = Math.Max( 0, stream.Length + eventStr.Length - MaxSize );
						string logFileStr = (new StreamReader( stream )).ReadToEnd();

						// Write the old contents of our log file plus our new event.
						stream.Position = 0;
						StreamWriter writer = new StreamWriter( stream );
						writer.Write( logFileStr );
						writer.Write( eventStr );
						writer.Flush();
					}
				}
				catch
				{
				}
			}
		}
	}
}
