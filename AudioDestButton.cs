using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MillerX.RemoteDesktopPlus
{
    class AudioDestButton : Button
    {
        private ToolTip m_ToolTip = new ToolTip();

        public AudioDestButton( )
        {
            SetAudioMode( RemoteDesktopPlus.AudioMode.DoNotPlay );
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                m_ToolTip.Dispose();
            }

            base.Dispose( disposing );
        }

        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public AudioMode AudioMode { get; private set; }

        private void SetAudioMode( AudioMode mode )
        {
            this.AudioMode = mode;

            string tooltip = "";
            switch ( mode )
            {
                case AudioMode.ThisComputer:
                    this.Image = Properties.Resources.Audio_Here;
                    tooltip = "Bring audio to this computer.";
                    break;
                case AudioMode.RemoteComputer:
                    this.Image = Properties.Resources.Audio_Remote;
                    tooltip = "Leave audio at remote computer.";
                    break;
                case AudioMode.DoNotPlay:
                    this.Image = Properties.Resources.Audio_None;
                    tooltip = "Do not play audio.";
                    break;
            }

            m_ToolTip.SetToolTip( this, tooltip );
        }

        protected override void OnClick( EventArgs e )
        {
            int mode = (int) this.AudioMode;
            mode = ++mode % 3;
            SetAudioMode( (AudioMode) mode );

            base.OnClick( e );
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
