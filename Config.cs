using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MillerX.RemoteDesktopPlus
{
	static class Config
	{
		public static int MaxComputerCount = 128;
		public static int MaxMatchCount = 5;
		public static int MaxComboItems = 10;
		public static Size ResetSize = new Size( 1280, 1024 );
		public static string ResetPosition = "0,1,-1280,150,-80,950";
		public static int ResetBpp = 16;
		public static string[] Regex = new string[0];
	}

	class ConfigFileSerializer
	{
		public static string GetConfigFilePath( )
		{
			var assem = System.Reflection.Assembly.GetExecutingAssembly();
			string assemblyDir = Path.GetDirectoryName( assem.Location );
			return Path.Combine( assemblyDir, "config.config" );
		}

		public void Read( TextReader reader )
		{
			string line;
			int lineNum = 0;
			var regex = new List<string>();
			var configType = typeof( Config );
			while (ReadLine( reader, out line ))
			{
				++lineNum;
				string[] lineParts = line.Split( ':' );
				string name = lineParts[0];
				string value = lineParts[1];
				var fieldInfo = configType.GetField( name, BindingFlags.Public | BindingFlags.Static );

				if (fieldInfo != null)
				{
					try
					{
						SetByMetaData( fieldInfo, value );
					}
					catch (Exception ex)
					{
						LogParseError( ex.Message, lineNum, line );
					}
				}
				else if (name.StartsWith( "Regex" ))  // We used StartsWith() so we can give each regex a personalized name.
				{
					regex.Add( value );
				}
				else
				{
					LogParseError( "Do not know how to parse field type.", lineNum, line );
				}
			}

			Config.Regex = regex.ToArray();
		}

		private static bool ReadLine( TextReader reader, out string line )
		{
			line = reader.ReadLine();
			return (line != null);
		}

		private static void LogParseError( string message, int lineNum, string line )
		{
			Logger.LogString( string.Format( "{0}  Line {1}.  \"{2}\"", message, lineNum, line ) );
		}

		private static void SetByMetaData( FieldInfo fieldInfo, string value )
		{
			if (fieldInfo.FieldType == typeof( string ))
				fieldInfo.SetValue( null, value );
			else if (fieldInfo.FieldType == typeof( int ))
				fieldInfo.SetValue( null, int.Parse( value ) );
			else if (fieldInfo.FieldType == typeof( bool ))
				fieldInfo.SetValue( null, bool.Parse( value ) );
			else if (fieldInfo.FieldType == typeof( Size ))
			{
				var converter = new SizeConverter();
				var sizeValue = (Size) converter.ConvertFromString( value );
				fieldInfo.SetValue( null, sizeValue );
			}
			else
				throw new Exception( string.Format( "Unrecogized field type: {0}", fieldInfo.FieldType ) );
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
