using System;
using System.Collections.Generic;
using System.Text;

namespace MillerX.RemoteDesktopPlus
{
	public enum AudioMode
	{
		ThisComputer = 0,
		RemoteComputer = 1,
		DoNotPlay = 2
	}

	public class MstscSettings : ICloneable
	{
		public MstscSettings( )
		{
			this.AudioMode = AudioMode.DoNotPlay;
			this.SharedDrives = new List<char>();
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
		public List<char> SharedDrives { get; private set; }

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
