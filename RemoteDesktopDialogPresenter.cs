using System;
using System.Collections.Generic;
using System.Linq;

namespace MillerX.RemoteDesktopPlus
{
	class DataEventArgs : EventArgs
	{
		public object Data { get; private set; }
		public DataEventArgs( object data )
		{
			this.Data = data;
		}
	}

	class RemoteDesktopDialogPresenter
	{
		private RecentComputerList m_RecentComputerList = new RecentComputerList();

        protected RecentComputerList RecentComputerList { get { return m_RecentComputerList; } }

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

		public event EventHandler MstscAppExited;
        protected void OnMstscAppExited( )
        {
            if ( MstscAppExited != null )
                MstscAppExited( this, EventArgs.Empty );
        }

		public void LoadInitData( )
		{
			var config = new ConfigFileSerializer();
			config.Read();

			m_RecentComputerList = LoadRecentComputerList();

			OnComputerListUpdated();
		}

		private RecentComputerList LoadRecentComputerList( )
		{
			// Normally we read the computer list from our own setting file but on the first run
			// we will need import it from the registry.
			var listFile = new ComputerListFile();
			var computers = listFile.Read();
            if ( computers.Count == 0 )
            {
                Logger.LogString( "Computer list file not found.  Reading from registry." );
                computers = listFile.ReadFromRegistry();
            }
			return computers;
		}

        public ComputerName BuildComputerName( string computerName, string aliasName )
        {
            ComputerName computer = m_RecentComputerList.Find( computerName );
            if (computer == null)
            {
                computer = new ComputerName( computerName, aliasName );
            }
            else if (computer.EqualsAlias( computerName ))
            {
                // There is some ambiguity in the UI.  If you have an alias in the Computer Name drop-down
                // and a name in the Alias textbox then don't do anything with the Alias textbox.
            }
            else if (aliasName != "")
            {
                // We still set the alias in-case we are trying to change the computer name the alias is associated with.
                computer.Alias = aliasName;
            }

            return computer;
        }

		public void MstscConnect( MstscSettings settings )
		{
            var mstscApp = new MstscApp();
#if DEBUG
            mstscApp.TestMode = true;
#endif
            try
            {
                mstscApp.Run( settings );
            }
            catch ( Exception ex )
            {
                Logger.LogException( ex );
            }

            OnMstscAppExited();

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

        public void EditComputerList( )
        {
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = ComputerListFile.GetComputerListFilePath();
            process.Start();

            process.WaitForExit();

            m_RecentComputerList = LoadRecentComputerList();
            OnComputerListUpdated();
        }

        public void ResetRDWindowPos( )
        {
            MstscConfig.BuildMstscConfig().ResetWindowsPos();
        }
    }
}
