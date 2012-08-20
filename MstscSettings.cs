using System;
using System.Collections.Generic;
using System.Text;

namespace MillerX.RemoteDesktopPlus
{
	enum AudioMode
	{
		ThisComputer = 0,
		RemoteComputer = 1,
		DoNotPlay = 2
	}

	class MstscSettings : ICloneable
	{
		public MstscSettings( )
		{
			this.AudioMode = AudioMode.DoNotPlay;
            this.SharedDrives = new char[0];
		}

		/// <summary>
		/// Computer to connect to.
		/// </summary>
		public ComputerName Computer { get; set; }

		/// <summary>
		/// Where do we play sounds from the remote computer?
		/// </summary>
		public AudioMode AudioMode { get; set; }

		/// <summary>
		/// Letters of drives we want to be shared.
		/// </summary>
		public char[] SharedDrives { get; set; }

		public MstscSettings Clone( )
		{
			return (MstscSettings) this.MemberwiseClone();
		}

		object ICloneable.Clone( )
		{
			return this.Clone();
		}
	}
}
