using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace MillerX.RemoteDesktopPlus
{
	partial class RemoteDesktopDialog : Form
	{
        private RemoteDesktopDialogPresenter m_Presenter = new RemoteDesktopDialogPresenter();

		public RemoteDesktopDialog( )
		{
			InitializeComponent();
			this.Icon = Properties.Resources.RemoteDesktop;
            m_AliasComboBox.ForeColor = UIItems.AliasColor;

			m_Presenter.ComputerListUpdated += new EventHandler<DataEventArgs>( m_Presenter_ComputerListUpdated );
			m_Presenter.MstscAppExited += new EventHandler( m_Presenter_MstscAppExited );

			this.Enabled = false;
            ThreadPool.QueueUserWorkItem( (o) => m_Presenter.LoadInitData() );
		}

		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
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

            var computers = (RecentComputerList) e.Data;

            m_ComputerComboBox.Populate( computers );
			// Put the first item in the combo's textbox
			if ( m_ComputerComboBox.Items.Count > 0 )
				m_ComputerComboBox.SelectedIndex = 0;

			m_AliasComboBox.BeginUpdate();
			m_AliasComboBox.Items.Clear();
			m_AliasComboBox.Items.AddRange( computers.GetAliases().ToArray() );
			m_AliasComboBox.MaxDropDownItems = Config.MaxComboItems;
			m_AliasComboBox.EndUpdate();

			// Alias has been applied so can clear the textbox.
			m_AliasComboBox.Text = "";

			this.Enabled = true;
		}

        private void m_ComputerComboBox_ComputerChanged( object sender, DataEventArgs e )
        {
            m_LogoPictureBox.AdminMode = ((ComputerName) e.Data).AdminMode;
            
            var computer = m_ComputerComboBox.SelectedItem as ComputerName;
            m_ExpandAliasButton.Enabled = (computer != null) && !string.IsNullOrEmpty( computer.Alias );
        }

        private void m_ComputerComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            // Alias is no longer valid if we selected another computer.
            m_AliasComboBox.Text = "";
        }

        private void m_ExpandAliasButton_Click( object sender, EventArgs e )
        {
            if ( m_ComputerComboBox.SelectedItem != null )
            {
                var computer = (ComputerName) m_ComputerComboBox.SelectedItem;
                if ( m_AliasComboBox.Text == "" )
                {
                    // Expand
                    m_ComputerComboBox.Text = computer.Computer;
                    m_AliasComboBox.Text = computer.Alias;
                }
                else
                {
                    // Collapse
                    m_ComputerComboBox.Text = computer.Alias;
                    m_AliasComboBox.Text = "";
                }
            }
        }

		private void m_ConnectButton_Click( object sender, EventArgs e )
		{
			try
			{
				this.Enabled = false;

                // Trim spaces from copy-and-paste operations.
                m_ComputerComboBox.Text = m_ComputerComboBox.Text.Trim();
                m_AliasComboBox.Text = m_AliasComboBox.Text.Trim();

                var settings = new MstscSettings();
                settings.Computer = m_Presenter.BuildComputerName( m_ComputerComboBox.Text, m_AliasComboBox.Text );
                settings.Computer.AdminMode = m_LogoPictureBox.AdminMode;
                settings.AudioMode = m_AudioButton.AudioMode;
                settings.SharedDrives = m_DrivesButton.GetDrives();

                ThreadPool.QueueUserWorkItem( ( o ) => m_Presenter.MstscConnect( settings ) );

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

		private void m_Presenter_MstscAppExited( object sender, EventArgs e )
		{
			if ( this.InvokeRequired )
			{
                this.BeginInvoke( new EventHandler( m_Presenter_MstscAppExited ), sender, e );
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

		private void m_ctxAdvEditComputerList_Click( object sender, EventArgs e )
		{
            ThreadPool.QueueUserWorkItem( (o) => m_Presenter.EditComputerList() );
		}

		private void m_ctxAdvResetWindowPos_Click( object sender, EventArgs e )
		{
            m_Presenter.ResetRDWindowPos();
		}

		private void m_ctxAbout_Click( object sender, EventArgs e )
		{
			using ( var dialog = new AboutDialog() )
			{
				dialog.ShowDialog( this );
			}
		}
	}

    class UIItems
    {
        public static readonly System.Drawing.Color AliasColor = System.Drawing.Color.DarkRed;
        public static System.Drawing.Brush AliasBrush = System.Drawing.Brushes.DarkRed;
    }
}
