using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MillerX.RemoteDesktopPlus
{
	/// <summary>
	/// Starts up and waits for Mstsc.exe (Microsoft Terminal Services Client (ie Remote Desktop).
	/// </summary>
	public class MstscApp
	{
		private MstscSettings m_Settings = null;

		public static readonly string MstscExecutablePath = System.IO.Path.Combine(
			System.Environment.GetFolderPath( System.Environment.SpecialFolder.System ),
			"mstsc.exe" );

		public bool TestMode { get; set; }

		public event EventHandler<MstscEventArgs> Exited;

		public MstscApp( )
		{
			this.TestMode = false;
		}

		public void Connect( MstscSettings settings )
		{
			m_Settings = settings.Clone();

			Process process = new Process();
			try
			{
				UpdateConnectionFile( m_Settings );

				SetStartInfo( process.StartInfo );
				StartProcess( process );

				// Wait for the process to exit for real this time.
				WaitForExit( process );
			}
			catch ( Exception ex )
			{
				Trace.TraceError( ex.ToString() );
			}
			finally
			{ process.Dispose(); }

			if ( Exited != null )
				Exited( this, new MstscEventArgs( m_Settings ) );
		}

		private void SetStartInfo( ProcessStartInfo startInfo )
		{
			if ( this.TestMode )
			{
				startInfo.FileName = "notepad.exe";
				startInfo.Arguments = "";
			}
			else
			{
				startInfo.FileName = MstscExecutablePath;
				startInfo.Arguments = GetArgumentsString( m_Settings );
			}
		}

		private static string GetArgumentsString( MstscSettings settings )
		{
			return "/v:" + settings.Computer.Computer +
				(settings.Computer.AdminMode ? " /console /admin" : "");
		}

		protected virtual void UpdateConnectionFile( MstscSettings settings )
		{
			using ( MstscConfig config = MstscConfig.BuildMstscConfig() )
			{
				config.Update( settings );
			}
		}

		protected virtual bool StartProcess( Process process )
		{
			return process.Start();
		}

		protected virtual void WaitForExit( Process process )
		{
			process.WaitForExit();
		}
	} // class MstscApp

	public class MstscEventArgs : EventArgs
	{
		public MstscSettings Settings { get; private set; }

		public MstscEventArgs( MstscSettings settings )
		{
			this.Settings = settings;
		}
	}
}
