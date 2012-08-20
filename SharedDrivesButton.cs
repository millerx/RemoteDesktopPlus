using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MillerX.RemoteDesktopPlus
{
    class SharedDrivesButton : Button
    {
        private SharedDrivesForm m_Form = new SharedDrivesForm();

        public SharedDrivesButton( )
        {
            this.Image = Properties.Resources.Drive;
            m_Form.Show( this );
            m_Form.Visible = false;
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                m_Form.Dispose();
            }

            base.Dispose( disposing );
        }

        protected override void OnClick( EventArgs e )
        {
            // If the form is visible then we clicked this button in an attempt to hide the form.
            if ( !m_Form.Visible )
            {
                m_Form.Location = PointToScreen( new Point( 0, this.Height ) );
                m_Form.Visible = true;
            }

            base.OnClick( e );
        }

        public char[] GetDrives( )
        {
            return m_Form.GetDrives();
        }

        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public new Image Image
        {
            get { return base.Image; }
            set { base.Image = value; }
        }

        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
    }
}
