using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace MillerX.RemoteDesktopPlus
{
	public partial class RemoteDesktopDialog : Form
	{
		private RemoteDesktopDialogPresenter m_Presenter = new RemoteDesktopDialogPresenter();
		private AdvancedTools m_AdvancedTools = new AdvancedTools();

		public RemoteDesktopDialog( )
		{
			InitializeComponent();
			this.Icon = Properties.Resources.RemoteDesktop;
			m_CancelButton.Click += (s, e) => { Close(); };
			m_AdvancedTools.ComputerListUpdated += (s, e) => { m_Presenter.Execute( AppCmd.LoadInitData ); };
			m_Presenter.ComputerListUpdated += new EventHandler<DataEventArgs>( m_Presenter_ComputerListUpdated );
			m_Presenter.MstscAppConnected += new EventHandler( m_Presenter_MstscAppConnected );

			m_Presenter.Start();
			this.Enabled = false;
			m_Presenter.Execute( AppCmd.LoadInitData );
		}

		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				m_Presenter.Dispose();

				if ( components != null )
					components.Dispose();
			}

			base.Dispose( disposing );
		}

		private void m_Presenter_ComputerListUpdated( object sender, DataEventArgs e )
		{
			if ( this.InvokeRequired )
			{
				this.BeginInvoke( new EventHandler<DataEventArgs>( m_Presenter_ComputerListUpdated ), sender, e );
				return;
			}

			m_ComputerSettingsPanel.SetRecentComputerList( (RecentComputerList) e.Data );
			this.Enabled = true;
		}

		private void m_ConnectButton_Click( object sender, EventArgs e )
		{
			try
			{
				this.Enabled = false;

				var settings = new MstscSettings();
				foreach ( object control in Controls )
				{
					if ( control is ISettingsControl )
						(control as ISettingsControl).OnConnect( settings );
				}

				m_Presenter.Execute( AppCmd.MstscConnect, settings );

				// If we hide the window right away, sometimes it does not get removed from the taskbar.
				// To avoid this we wait a few secs before hiding the window.
				m_Timer.Enabled = true;
			}
			catch ( Exception ex )
			{
				m_Timer.Enabled = false;
				this.Enabled = true;
				Logger.LogException( ex );
				ShowErrorMessage( ex.ToString() );
			}
		}

		private void ShowErrorMessage( string message )
		{
			MessageBox.Show( message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
		}

		private void m_Presenter_MstscAppConnected( object sender, EventArgs e )
		{
			if ( this.InvokeRequired )
			{
				this.BeginInvoke( new EventHandler( m_Presenter_MstscAppConnected ), sender, e );
				return;
			}

			// We disable the timer here in case MSTSC returned before we hid the window.
			m_Timer.Enabled = false;
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}

		private void m_Timer_Tick( object sender, EventArgs e )
		{
			this.WindowState = FormWindowState.Minimized;
			this.Hide();
			m_Timer.Enabled = false;
		}

		private void m_ctxAdvOpenComputerList_Click( object sender, EventArgs e )
		{
			m_AdvancedTools.OpenComputerListFile();
		}

		private void m_ctxAdvResetWindowPos_Click( object sender, EventArgs e )
		{
			m_AdvancedTools.ResetWindowPos();
		}

		private void m_ctxAbout_Click( object sender, EventArgs e )
		{
			using ( var dialog = new AboutDialog() )
			{
				dialog.ShowDialog( this );
			}
		}
	}

	// Interface for controls that let the user edit MSTSC settings.
	public interface ISettingsControl
	{
		void OnConnect( MstscSettings settings );
	}
}
