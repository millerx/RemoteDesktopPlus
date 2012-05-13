using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MillerX.RemoteDesktopPlus
{
	public partial class AudioDestinationSettingsPanel : UserControl, ISettingsControl
	{
		public AudioDestinationSettingsPanel( )
		{
			InitializeComponent();
		}

		public void OnConnect( MstscSettings settings )
		{
			switch ( m_Slider.Value )
			{
				case 0:
					settings.AudioMode = AudioMode.RemoteComputer;
					break;
				case 1:
					settings.AudioMode = AudioMode.DoNotPlay;
					break;
				case 2:
					settings.AudioMode = AudioMode.ThisComputer;
					break;
				default:
					Logger.LogString( string.Format( "Error: Unknown audio slider value: {0}", m_Slider.Value ) );
					break;
			}
		}
	}
}
