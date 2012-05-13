using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MillerX.RemoteDesktopPlus
{
	public class AdvancedTools
	{
		private Process m_ComputerListProcess = null;

		public event EventHandler ComputerListUpdated;

		public void OpenComputerListFile( )
		{
			try
			{
				WaitForComputerListProcessToExit();

				m_ComputerListProcess = new Process();
				m_ComputerListProcess.StartInfo.FileName = "notepad.exe";
				m_ComputerListProcess.StartInfo.Arguments = ComputerListFile.GetComputerListFilePath();
				m_ComputerListProcess.EnableRaisingEvents = true;
				m_ComputerListProcess.Exited += new EventHandler( ComputerListProcess_Exited );
				m_ComputerListProcess.Start();
			}
			catch ( Exception ex )
			{
				Logger.LogException( ex );
			}
		}

		private void WaitForComputerListProcessToExit( )
		{
			try
			{
				if ( m_ComputerListProcess != null )
					m_ComputerListProcess.WaitForExit();
			}
			catch ( Exception )
			{
			}
		}

		private void ComputerListProcess_Exited( object sender, EventArgs e )
		{
			m_ComputerListProcess.Dispose();
			m_ComputerListProcess = null;

			if ( ComputerListUpdated != null )
				ComputerListUpdated( this, EventArgs.Empty );
		}

		public void ResetWindowPos( )
		{
			var config = MstscConfig.BuildMstscConfig();
			config.ResetWindowsPos();
		}
	}
}
