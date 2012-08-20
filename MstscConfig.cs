using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MillerX.RemoteDesktopPlus
{
	/// <summary>
	/// Serialializing MstscSettings into Remote Desktop's config file.  This class does the work
	/// of reading and writing the config file.  Derived classes decide which keys we change and
	/// what we change them to.
	/// </summary>
	class MstscConfig : IDisposable
	{
        public const char DrivePlugChar = '&';
        public const char DriveAllChar = '*';

		private static readonly System.Diagnostics.FileVersionInfo AppVersionInfo =
			System.Diagnostics.FileVersionInfo.GetVersionInfo( MstscApp.MstscExecutablePath );

		protected MstscConfig( )
		{
		}

		public virtual void Dispose( )
		{
		}

		public static MstscConfig BuildMstscConfig( )
		{
			if ( AppVersionInfo.FileMajorPart >= 6 &&
				 AppVersionInfo.FileMinorPart >= 1 )
			{
				return new Win7MstscConfig();
			}
			else
			{
				return new XpMstscConfig();
			}
		}

		public static string GetRdpConfigFilename( )
		{
			string filePath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
			return Path.Combine( filePath, "Default.rdp" );
		}

		public void Update( MstscSettings settings )
		{
			Update( GetRdpConfigFilename(), settings );
		}

		public void Update( string filePath, MstscSettings settings )
		{
			var dict = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );
			SetUpdateDict( settings, dict );
			Update( filePath, dict );
		}

        public void Update( string[] inputLines, MstscSettings settings, Stream outputStream )
        {
            var dict = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );
            SetUpdateDict( settings, dict );
            Update( inputLines, dict, outputStream );
        }

		protected virtual void SetUpdateDict( MstscSettings settings, Dictionary<string, string> dict )
		{
		}

		public void ResetWindowsPos( )
		{
			ResetWindowsPos( GetRdpConfigFilename() );
		}

		public void ResetWindowsPos( string filePath )
		{
			var dict = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );
			SetResetWindowPosDict( dict );
			Update( filePath, dict );
		}

        public void ResetWindowsPos( string[] inputLines, Stream outputStream )
        {
            var dict = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );
            SetResetWindowPosDict( dict );
            Update( inputLines, dict, outputStream );
        }

		protected virtual void SetResetWindowPosDict( Dictionary<string, string> dict )
		{
		}

        private void Update( string filePath, Dictionary<string, string> settingsDir )
        {
            var lines = File.ReadAllLines( filePath );
            // File.CreateText failed for Default.rdp
            var stream = File.Open( filePath, FileMode.Truncate, FileAccess.Write );
            Update( lines, settingsDir, stream );
        }

        private void Update( string[] inputLines, Dictionary<string, string> settingsDir, Stream outputStream )
        {
            Dictionary<string, int> keysUpdated = new Dictionary<string, int>( StringComparer.OrdinalIgnoreCase );

            using ( StreamWriter writer = new StreamWriter( outputStream, Encoding.Unicode ) )
            {
                foreach ( string line in inputLines )
                {
                    string keyName = GetKeyName( line );

                    if ( settingsDir.ContainsKey( keyName ) )
                    {
                        writer.WriteLine( keyName + settingsDir[keyName] );
                        keysUpdated.Add( keyName, 0 );
                    }
                    else
                    {
                        writer.WriteLine( line );
                    }
                }

                // If the key wasn't read than it needs to be added.
                foreach ( string keyName in settingsDir.Keys )
                {
                    if ( !keysUpdated.ContainsKey( keyName ) )
                        writer.WriteLine( keyName + settingsDir[keyName] );
                }
            }
        }

		private static string GetKeyName( string line )
		{
			if ( line == null )
				return "";

			// Get the second index
			int index = line.IndexOf( ':' );
			if ( index >= 0 )
				index = line.IndexOf( ':', 1 + index );

			if ( index >= 0 )
				return line.Substring( 0, 1 + index );
			else
				return "";
		}
	}

	/// <summary>
	/// Configuration settings from Remote Desktop when I started writing this application.
	/// </summary>
	class XpMstscConfig : MstscConfig
	{
		protected const string AudioModeKey = "audiomode:i:";
		protected const string LastComputerKey = "full address:s:";
		protected const string SharedDrivesKey = "drivestoredirect:s:";

		protected const string ScreenModeIdKey = "screen mode id:i:";
		protected const string DesktopWidthKey = "desktopwidth:i:";
		protected const string DesktopHeightKey = "desktopheight:i:";
		protected const string SessionBbpKey = "session bpp:i:";
		protected const string WindowPosKey = "winposstr:s:";

		public XpMstscConfig( )
		{
		}

		protected override void SetUpdateDict( MstscSettings settings, Dictionary<string, string> dict )
		{
			dict[AudioModeKey] = ((int) settings.AudioMode).ToString();
			dict[LastComputerKey] = settings.Computer.Computer;
			dict[SharedDrivesKey] = SerializeSharedDrives( settings.SharedDrives );
		}

		private static string SerializeSharedDrives( char[] sharedDrives )
		{
            // I'm not sure if 'All Drives' are supported in XP.
            if ( sharedDrives.Length == 1 && sharedDrives[0] == DriveAllChar )
            {
                var drives = System.IO.Directory.GetLogicalDrives();
                sharedDrives = new char[drives.Length];
                for ( int i = 0; i < drives.Length; ++i )
                    sharedDrives[i] = drives[i][0];
            }

            StringBuilder valueString = new StringBuilder();
            foreach ( char drive in sharedDrives )
			{
                // 'Drives I plug in later' not supported in XP.
                if ( drive != DrivePlugChar )
				    valueString.AppendFormat( "{0}:;", char.ToUpper( drive ) );
			}

			return valueString.ToString();
		}

		protected override void SetResetWindowPosDict( Dictionary<string, string> dict )
		{
			dict[ScreenModeIdKey] = "2";
            dict[DesktopWidthKey] = Config.ResetSize.Width.ToString();
            dict[DesktopHeightKey] = Config.ResetSize.Height.ToString();
            dict[SessionBbpKey] = Config.ResetBpp.ToString();
            dict[WindowPosKey] = Config.ResetPosition;
		}
	}

	/// <summary>
	/// Remote Desktop changed sometime after Win7 (and ported back to Vista).
	/// </summary>
	class Win7MstscConfig : XpMstscConfig
	{
		public Win7MstscConfig( IVolumeNameProvider nameProvider )
		{
			m_nameProvider = nameProvider;
		}

		public Win7MstscConfig( )
			: this( new Shell32NameProvider() )
		{
		}

		public override void Dispose( )
		{
			if ( m_nameProvider != null )
				m_nameProvider.Dispose();

			base.Dispose();
		}

		protected override void SetUpdateDict( MstscSettings settings, Dictionary<string, string> dict )
		{
			// Get all the XP settings.
			base.SetUpdateDict( settings, dict );

			dict[SharedDrivesKey] = SerializeSharedDrives( settings.SharedDrives );
		}

		private string SerializeSharedDrives( char[] sharedDrives )
		{
			StringBuilder valueString = new StringBuilder();
			foreach ( char drive in sharedDrives )
			{
                if ( drive == DriveAllChar )
                {
                    // 'All drives' should be the only character and no semi-colon is intentional.
                    return "*";
                }
                else if ( drive == DrivePlugChar )
                    valueString.Append( "DynamicDrives;" );
                else
                {
                    valueString.Append( m_nameProvider.GetVolumeName( drive ) );
                    valueString.Append( ';' );
                }
			}

			return valueString.ToString();
		}

		private IVolumeNameProvider m_nameProvider;

		public interface IVolumeNameProvider : IDisposable
		{
			string GetVolumeName( char driveLetter );
		}

		private class Shell32NameProvider : IVolumeNameProvider, IDisposable
		{
			private ShellLib.IShellFolder ishellParent;
			private IntPtr pidlSystem;
			private ShellLib.IMalloc pMalloc;

			public Shell32NameProvider( )
			{
				ishellParent = ShellLib.ShellFunctions.GetDesktopFolder();
				pMalloc = ShellLib.ShellFunctions.GetMalloc();
			}

			public void Dispose( )
			{
				System.Runtime.InteropServices.Marshal.ReleaseComObject( ishellParent );
				pMalloc.Free( pidlSystem );
				System.Runtime.InteropServices.Marshal.ReleaseComObject( pMalloc );
			}

			public string GetVolumeName( char drive )
			{
				int result = 0;

				uint eaten = 0;
				uint attributes = 0;
				string drivePath = Char.ToUpper( drive ).ToString() + ":\\";
				result = ishellParent.ParseDisplayName( IntPtr.Zero, IntPtr.Zero, drivePath, ref eaten, out pidlSystem, ref attributes );
				if ( result != 0 )
					return "";

				ShellLib.STRRET ptrString;
				result = ishellParent.GetDisplayNameOf( pidlSystem, (uint) ShellLib.SHGNO.SHGDN_NORMAL, out ptrString );
				if ( result != 0 )
					return "";

				System.Text.StringBuilder strDisplay = new System.Text.StringBuilder( 260 );
				result = ShellLib.ShellApi.StrRetToBuf( ref ptrString, pidlSystem, strDisplay,
					(uint) strDisplay.Capacity );
				if ( result != 0 )
					return "";

				return strDisplay.ToString();
			}
		}
	}
}
