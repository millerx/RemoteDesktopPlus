using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MillerX.RemoteDesktopPlus
{
	public partial class ComputerSettingsPanel : UserControl, ISettingsControl
	{
		private static readonly System.Drawing.Color AliasColor = System.Drawing.Color.DarkRed;
		private static System.Drawing.Brush AliasBrush = System.Drawing.Brushes.DarkRed;

		private RecentComputerList m_RecentComputerList = new RecentComputerList();
		private bool m_AdminMode = false;

		public ComputerSettingsPanel( )
		{
			InitializeComponent();

			// "IntegralHeight = false" needed to make MaxDropDownItems work properly in Vista+.
			m_ComputerComboBox.IntegralHeight = false;
			m_AliasComboBox.IntegralHeight = false;
			m_AliasComboBox.ForeColor = AliasColor;
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

		public void OnConnect( MstscSettings settings )
		{
			// Trim spaces from copy-and-paste operations.
			m_ComputerComboBox.Text = m_ComputerComboBox.Text.Trim();
			m_AliasComboBox.Text = m_AliasComboBox.Text.Trim();

			settings.Computer = ComputerName.BuildComputerName( m_ComputerComboBox.Text, m_AliasComboBox.Text, m_RecentComputerList );
			settings.Computer.AdminMode = m_AdminMode;
		}

		public void SetRecentComputerList( RecentComputerList computers )
		{
			m_RecentComputerList = computers;

			m_ComputerComboBox.BeginUpdate();
			m_ComputerComboBox.Items.Clear();
			m_ComputerComboBox.Items.AddRange( computers.ToArray() );
			m_ComputerComboBox.MaxDropDownItems = Config.MaxComboItems;
			m_ComputerComboBox.EndUpdate();

			// Put the first item in the combo's textbox
			if ( m_ComputerComboBox.Items.Count > 0 )
			{
				m_ComputerComboBox.SelectedIndex = 0;
				// If the computer name and the alias are the same then it won't trigger the text changed event.
				m_ComputerComboBox_TextChanged( this, EventArgs.Empty );
			}

			m_AliasComboBox.BeginUpdate();
			m_AliasComboBox.Items.Clear();
			m_AliasComboBox.Items.AddRange( computers.GetAliases().ToArray() );
			m_AliasComboBox.MaxDropDownItems = Config.MaxComboItems;
			m_AliasComboBox.EndUpdate();

			// Alias has been applied so can clear the textbox.
			m_AliasComboBox.Text = "";
		}

		public void SetComputerName( ComputerName computer )
		{
			m_ComputerComboBox.Text = computer.Computer;
			m_AliasComboBox.Text = computer.Alias;
		}

		private void m_LogoPictureBox_Click( object sender, EventArgs e )
		{
			m_AdminMode = !m_AdminMode;
			UpdateLogo( m_AdminMode );
		}

		private void UpdateLogo( bool admin )
		{
			m_LogoPictureBox.Image = admin ?
				Properties.Resources.AdminLogo :
				Properties.Resources.UserLogo;
		}

		private void m_ComputerComboBox_DrawItem( object sender, DrawItemEventArgs e )
		{
			e.DrawBackground();

			ComputerName computer = (ComputerName) m_ComputerComboBox.Items[e.Index];
			Brush brush = GetComboBoxItemBrush( computer, e.State );
			e.Graphics.DrawString( computer.ToString(), e.Font, brush, e.Bounds.X, e.Bounds.Y );

			e.DrawFocusRectangle();
		}

		private Brush GetComboBoxItemBrush( ComputerName computer, DrawItemState state )
		{
			if ( (state & DrawItemState.Selected) != 0 )
				return SystemBrushes.HighlightText;
			else if ( computer.Alias != null )
				return AliasBrush;
			else
				return SystemBrushes.ControlText;
		}

		private void m_ComputerComboBox_TextChanged( object sender, EventArgs e )
		{
			ComputerName computer = m_RecentComputerList.Find( m_ComputerComboBox.Text );
			if ( computer != null )
			{
				if ( !string.IsNullOrEmpty( computer.Alias ) )
				{
					m_ToolTip.SetToolTip( m_ComputerComboBox, computer.Computer );
					m_ToolTip.Active = true;
					m_ComputerComboBox.ForeColor = AliasColor;
				}
				else
				{
					m_ToolTip.Active = false;
					m_ComputerComboBox.ForeColor = SystemColors.WindowText;
				}

				m_AdminMode = computer.AdminMode;
				UpdateLogo( m_AdminMode );
			}
		}

		private void m_ComputerComboBox_SelectedIndexChanged( object sender, EventArgs e )
		{
			// Alias is no longer valid if we selected another computer.
			m_AliasComboBox.Text = "";
		}
	}
}
