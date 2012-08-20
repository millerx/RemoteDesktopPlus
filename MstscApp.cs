using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace MillerX.RemoteDesktopPlus
{
	/// <summary>
	/// Starts up and waits for Mstsc.exe (Microsoft Terminal Services Client (ie Remote Desktop).
	/// </summary>
	class MstscApp
	{
		public static readonly string MstscExecutablePath = System.IO.Path.Combine(
			System.Environment.GetFolderPath( System.Environment.SpecialFolder.System ),
			"mstsc.exe" );

		public bool TestMode { get; set; }

		public MstscApp( )
		{
			this.TestMode = false;
		}

		public void Run( MstscSettings settings )
		{
			using ( var process = new Process() )
			{
                UpdateMstscConfig( settings );

                SetStartInfo( process.StartInfo, settings );
                process.Start();

                process.WaitForExit();
			}
		}

        private void UpdateMstscConfig( MstscSettings settings )
        {
            var config = MstscConfig.BuildMstscConfig();
            try
            {
                if ( this.TestMode )
                {
                    var lines = File.ReadAllLines( MstscConfig.GetRdpConfigFilename() );
                    var stream = new MemoryStream();
                    config.Update( lines, settings, stream );
                    var reader = new StreamReader( new MemoryStream( stream.ToArray() ) );
                    Console.WriteLine( reader.ReadToEnd() );
                }
                else
                    config.Update( settings );
            }
            catch ( Exception ex )
            {
                Logger.LogException( ex );
            }
            finally { config.Dispose(); }
        }

		protected void SetStartInfo( ProcessStartInfo startInfo, MstscSettings settings )
		{
			if ( this.TestMode )
			{
				startInfo.FileName = "notepad.exe";
				startInfo.Arguments = "";
                Console.WriteLine( GetArgumentsString( settings ) );
			}
			else
			{
				startInfo.FileName = MstscExecutablePath;
				startInfo.Arguments = GetArgumentsString( settings );
			}
		}

		private static string GetArgumentsString( MstscSettings settings )
		{
            string args = "/v:" + settings.Computer.Computer;
            if ( settings.Computer.AdminMode )
                args += " /console /admin";
            return args;
		}
	} // class MstscApp
}
