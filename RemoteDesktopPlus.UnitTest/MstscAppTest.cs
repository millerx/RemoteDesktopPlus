using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using MillerX.RemoteDesktopPlus;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class MstscAppTest
	{
		class TestMstscApp : MstscApp
		{
            public new void SetStartInfo( ProcessStartInfo startInfo, MstscSettings settings )
            {
                base.SetStartInfo( startInfo, settings );
            }
		}

		/// <summary>
		/// Tests arguments to connect to a remote computer.
		/// </summary>
		[Test]
		public void MstscAguments( )
		{
			var mstscApp = new TestMstscApp();
            ProcessStartInfo startInfo = new ProcessStartInfo();
			var settings = new MstscSettings();

			settings.Computer = new ComputerName( "chrismillerpc", null, false );
            mstscApp.SetStartInfo( startInfo, settings );
            Assert.AreEqual( "/v:chrismillerpc", startInfo.Arguments );

			settings.Computer = new ComputerName( "1.1.1.1", "work", true );
            mstscApp.SetStartInfo( startInfo, settings );
            Assert.AreEqual( "/v:1.1.1.1 /console /admin", startInfo.Arguments );

			mstscApp.TestMode = true;
            mstscApp.SetStartInfo( startInfo, settings );
            Assert.AreEqual( "notepad.exe", startInfo.FileName );
            Assert.AreEqual( "", startInfo.Arguments );
		}
	}
}
