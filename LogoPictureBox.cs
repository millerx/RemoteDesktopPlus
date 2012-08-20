using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MillerX.RemoteDesktopPlus
{
    class LogoPictureBox : PictureBox
    {
        private bool m_adminMode;

        public LogoPictureBox( )
        {
            this.AdminMode = false;
        }

        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public bool AdminMode
        {
            get { return m_adminMode; }
            set
            {
                m_adminMode = value;
                this.Image = value ? Properties.Resources.AdminLogo : Properties.Resources.UserLogo;
            }
        }

        protected override void OnClick( EventArgs e )
        {
            this.AdminMode = !this.AdminMode;
            base.OnClick( e );
        }

        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public new Image Image
        {
            get { return base.Image; }
            set { base.Image = value; }
        }
    }
}
