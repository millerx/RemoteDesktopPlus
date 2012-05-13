using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MillerX.RemoteDesktopPlus
{
	static class Program
	{
		/// <summary>
		/// Path where RemoteDesktopPlus files are written.
		/// </summary>
		public static readonly string LocalAppPath = BuildLocalAppPath();

		private static string BuildLocalAppPath( )
		{
			string localAppPath = Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData );
			return Path.Combine( localAppPath, "RemoteDesktopPlus" );
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main( )
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new RemoteDesktopDialog() );
		}
	}
}
