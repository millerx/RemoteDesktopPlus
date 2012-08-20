using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MillerX.RemoteDesktopPlus
{
    partial class SharedDrivesForm : Form
    {
        public SharedDrivesForm( )
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            m_DriveListBox.BeginUpdate();
            m_DriveListBox.Items.AddRange( System.IO.Directory.GetLogicalDrives() );
            m_DriveListBox.EndUpdate();
            base.OnLoad( e );
        }

        public char[] GetDrives( )
        {
            if ( m_DriveListBox.CheckedItems.Count == m_DriveListBox.Items.Count &&
                 m_DynamicDrivesCheckbox.Checked )
            {
                return new[] { MstscConfig.DriveAllChar };
            }

            var drives = new List<char>( 1 + m_DriveListBox.CheckedItems.Count );
            foreach ( object o in m_DriveListBox.CheckedItems )
            {
                drives.Add( (o as string)[0] );
            }

            if ( m_DynamicDrivesCheckbox.Checked )
                drives.Add( MstscConfig.DrivePlugChar );

            return drives.ToArray();
        }

        private void m_DriveListBox_LostFocus( object sender, EventArgs e )
        {
            if ( !m_DynamicDrivesCheckbox.Focused )
            {
                // Give us time to see if we clicked on the shared drive button.
                m_timer.Enabled = true;
            }
        }

        private void m_DynamicDrivesCheckbox_LostFocus( object sender, System.EventArgs e )
        {
            if ( !m_DriveListBox.Focused )
            {
                // Give us time to see if we clicked on the shared drive button.
                m_timer.Enabled = true;
            }
        }

        private void m_timer_Tick( object sender, EventArgs e )
        {
            m_timer.Enabled = false;
            this.Visible = false;
        }
    }
}
