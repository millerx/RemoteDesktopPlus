using System;
using System.Collections.Generic;
using System.Linq;

namespace MillerX.RemoteDesktopPlus
{
	public enum AppCmd
	{
		LoadInitData,
		MstscConnect,
	}

	public class DataEventArgs : EventArgs
	{
		public object Data { get; private set; }
		public DataEventArgs( object data )
		{
			this.Data = data;
		}
	}

	public class RemoteDesktopDialogPresenter : CmdThread<AppCmd>
	{
		/// <summary>
		/// List of computers we have connected to recently.
		/// </summary>
		private RecentComputerList m_RecentComputerList = new RecentComputerList();

		/// <summary>
		/// Runs mstsc (Remote Desktop) and waits for it to complete.
		/// </summary>
		private MstscApp m_MstscApp = new MstscApp();

		public event EventHandler<DataEventArgs> ComputerListUpdated;
		protected void OnComputerListUpdated( )
		{
			if ( ComputerListUpdated != null )
			{
				// We copy the list since this will run on another thread.
				var computers = new RecentComputerList( m_RecentComputerList );
				ComputerListUpdated( this, new DataEventArgs( computers ) );
			}
		}

		public event EventHandler MstscAppConnected;

		/// <summary>
		/// Message dispatcher.
		/// </summary>
		protected override void CmdProc( AppCmd command, object[] cparams )
		{
			switch ( command )
			{
				case AppCmd.LoadInitData:
					LoadInitData(); break;
				case AppCmd.MstscConnect:
					MstscConnect( (MstscSettings) cparams[0] ); break;
			}
		}

		/// <summary>
		/// Loads all initialization data and signals the UI with that data.
		/// </summary>
		private void LoadInitData( )
		{
			var config = new ConfigFileSerializer();
			config.Read();

			m_RecentComputerList = LoadRecentComputerList();

			OnComputerListUpdated();
		}

		/// <summary>
		/// Loads the recent computer list from our settings file or the MSTSC registry.
		/// </summary>
		private RecentComputerList LoadRecentComputerList( )
		{
			// Normally we read the computer list from our own setting file but on the first run
			// we will need import it from the registry.
			var listFile = new ComputerListFile();
			var computers = listFile.Read();
			if ( computers.Count == 0 )
				computers = listFile.ReadFromRegistry();
			return computers;
		}

		private void MstscConnect( MstscSettings settings )
		{
#if DEBUG
			m_MstscApp.TestMode = true;
#endif
			m_MstscApp.Connect( settings );

			if ( MstscAppConnected != null )
				MstscAppConnected( this, EventArgs.Empty );

			// It is important to read before we write in case we have two instances of RemoteDesktopPlus open.
			// However RemoteDesktop doesn't have special logic for adding an IP address to the list like we
			// do so we need to do an integrate step.
			m_RecentComputerList = LoadRecentComputerList();

			// We use our own logic to determine what the recent computer list should be after a
			// connection was made.
			m_RecentComputerList.Push( settings.Computer );

			ComputerListFile serializer = new ComputerListFile();
			serializer.Write( m_RecentComputerList );

			OnComputerListUpdated();
		}

		/// <summary>
		/// The computer list has been updated by an external application.  Load and display it.
		/// </summary>
		public void LoadUpdatedComputerList( )
		{
			// Get updates from any other RDP instance.
			m_RecentComputerList = LoadRecentComputerList();

			OnComputerListUpdated();
		}
	}
}
