using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace MillerX.RemoteDesktopPlus
{
	class ComputerListFile
	{
		public static string GetComputerListFilePath( )
		{
			return Path.Combine( Program.LocalAppPath, "computers.txt" );
		}

		public void Write( TextWriter writer, RecentComputerList computers )
		{
			foreach ( ComputerName cname in computers )
			{
				writer.WriteLine( "{0}\t{1}\t{2}", cname.Computer, cname.Alias, cname.AdminMode );
			}
		}

		public void Write( string filePath, RecentComputerList computers )
		{
			using ( StreamWriter writer = File.CreateText( filePath ) )
			{
				Write( writer, computers );
			}
		}

		public void Write( RecentComputerList computers )
		{
			try
			{
				Write( GetComputerListFilePath(), computers );
			}
			catch ( Exception ex )
			{
				Logger.LogException( ex );
			}
		}

		public RecentComputerList Read( TextReader reader )
		{
			RecentComputerList computers = new RecentComputerList();

			while ( true )
			{
				string line = reader.ReadLine();
				if ( line == null )
					break;

				string[] pair = line.Split( '\t' );

				// Avoid processing an empty line
				if ( pair[0] == "" )
					continue;
				string computer = pair[0];

				string alias = pair.Length > 1 ? pair[1] : "";

				bool adminMode = false;
				if ( pair.Length > 2 )
					bool.TryParse( pair[2], out adminMode );

				computers.Add( new ComputerName( computer, alias, adminMode ) );
			}

			return computers;
		}

		public RecentComputerList Read( string path )
		{
			using ( StreamReader reader = new StreamReader( path ) )
			{
				return Read( reader );
			}
		}

		public RecentComputerList Read( )
		{
			try
			{
				return Read( GetComputerListFilePath() );
			}
			catch ( Exception ex )
			{
				Logger.LogException( ex );
				return new RecentComputerList();
			}
		}

		// Does not throw.
		public RecentComputerList ReadFromRegistry( )
		{
			RecentComputerList computers = new RecentComputerList();

			try
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey( "Software" )
					.OpenSubKey( "Microsoft" )
					.OpenSubKey( "Terminal Server Client" )
					.OpenSubKey( "Default", false );

				const int MaxComputerCount = 10;
				for ( int i = 0; i < MaxComputerCount; ++i )
				{
					string valueName = string.Format( "MRU{0}", i );
					string computer = (string) key.GetValue( valueName );
					if ( !string.IsNullOrEmpty( computer ) )
						computers.Add( new ComputerName( key.GetValue( valueName ).ToString(), null ) );
				}
			}
			catch ( Exception ex )
			{
				Logger.LogException( ex );
			}

			return computers;
		}
	}
}
