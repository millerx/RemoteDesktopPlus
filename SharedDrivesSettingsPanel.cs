using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MillerX.RemoteDesktopPlus
{
	public partial class SharedDrivesSettingsPanel : UserControl, ISettingsControl
	{
		public SharedDrivesSettingsPanel( )
		{
			InitializeComponent();
		}

		protected override void OnLoad( EventArgs e )
		{
			m_DriveListBox.Items.AddRange( System.IO.Directory.GetLogicalDrives() );

			base.OnLoad( e );
		}

		public void OnConnect( MstscSettings settings )
		{
			settings.SharedDrives.Clear();

			foreach ( object o in m_DriveListBox.CheckedItems )
			{
				settings.SharedDrives.Add( (o as string)[0] );
			}
		}
	}
}
