using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MillerX.RemoteDesktopPlus
{
	public static class Config
	{
		public static int MaxComputerCount = 128;
		public static int MaxIpAddressCount = 5;
		public static int MaxComboItems = 10;
	}

	public class ConfigFileSerializer
	{
		public static string GetConfigFilePath( )
		{
			var assem = System.Reflection.Assembly.GetExecutingAssembly();
			string assemblyDir = Path.GetDirectoryName( assem.Location );
			return Path.Combine( assemblyDir, "config.config" );
		}

		private static bool ReadLine( TextReader reader, out string line )
		{
			line = reader.ReadLine();
			return (line != null);
		}

		public void Read( TextReader reader )
		{
			string line;
			int lineNum = 0;
			while ( ReadLine( reader, out line ) )
			{
				++lineNum;
				string[] lineParts = line.Split( ':' );
				string name = lineParts[0];
				string value = lineParts[1];

				Type configType = typeof( Config );
				FieldInfo fieldInfo = configType.GetField( name, BindingFlags.Public | BindingFlags.Static );
				if ( fieldInfo == null )
				{
					Logger.LogString( string.Format( "Unknown config field.  Line {0}.  \"{1}\"", lineNum, line ) );
					continue;
				}

				const string logBadParseValue = "Unable to parse value.  Line {0}.  \"{1}\"";
				if ( fieldInfo.FieldType == typeof( string ) )
				{
					fieldInfo.SetValue( null, value );
				}
				else if ( fieldInfo.FieldType == typeof(int) )
				{
					int intValue = 0;
					if ( !int.TryParse( value, out intValue ) )
					{
						Logger.LogString( string.Format( logBadParseValue, lineNum, line ) );
						continue;
					}

					fieldInfo.SetValue( null, intValue );
				}
				else if ( fieldInfo.FieldType == typeof( bool ) )
				{
					bool boolValue = false;
					if ( !bool.TryParse( value, out boolValue ) )
					{
						Logger.LogString( string.Format( logBadParseValue, lineNum, line ) );
						continue;
					}

					fieldInfo.SetValue( null, boolValue );
				}
				else
				{
					Logger.LogString( string.Format( "Do not know how to parse field type.  Line {0}.  \"{1}\"", lineNum, line ) );
					continue;
				}
			}
		}

		public void Read( string path )
		{
			using ( var reader = new StreamReader( path ) )
			{
				Read( reader );
			}
		}

		public void Read( )
		{
			this.Read( GetConfigFilePath() );
		}
	}
}
