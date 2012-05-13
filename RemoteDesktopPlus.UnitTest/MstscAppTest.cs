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
			protected override void UpdateConnectionFile( MstscSettings settings )
			{
			}

			public ProcessStartInfo ProcessStartInfo = null;
			public Exception StartProcessException = null;

			protected override bool StartProcess( Process process )
			{
				this.ProcessStartInfo = process.StartInfo;

				if ( this.StartProcessException != null )
					throw this.StartProcessException;
				else
					return true;
			}

			protected override void WaitForExit( Process process )
			{
			}
		}

		/// <summary>
		/// Tests arguments to connect to a remote computer.
		/// </summary>
		[Test]
		public void MstscAguments( )
		{
			var mstscApp = new TestMstscApp();
			var settings = new MstscSettings();

			settings.Computer = new ComputerName( "chrismillerpc", null, false );
			mstscApp.Connect( settings );
			Assert.AreEqual( "/v:chrismillerpc", mstscApp.ProcessStartInfo.Arguments );

			settings.Computer = new ComputerName( "1.1.1.1", "work", true );
			mstscApp.Connect( settings );
			Assert.AreEqual( "/v:1.1.1.1 /console /admin", mstscApp.ProcessStartInfo.Arguments );

			mstscApp.TestMode = true;
			mstscApp.Connect( settings );
			Assert.AreEqual( "notepad.exe", mstscApp.ProcessStartInfo.FileName );
			Assert.AreEqual( "", mstscApp.ProcessStartInfo.Arguments );
		}

		/// <summary>
		/// Tests the behavior of the run thread.
		/// </summary>
		[Test]
		public void SuccessfulConnection( )
		{
			MstscEventArgs exitedArgs = null;
			var mstscApp = new TestMstscApp();
			mstscApp.Exited += new EventHandler<MstscEventArgs>(
				( o, e ) => exitedArgs = e );

			var settings = new MstscSettings();
			settings.Computer = new ComputerName( "chrismillerpc", null );
			mstscApp.Connect( settings );

			// Settings returned should be a clone of what is passed in.
			Assert.IsTrue( settings != exitedArgs.Settings );
			Assert.AreEqual( settings.Computer, exitedArgs.Settings.Computer );
		}

		/// <summary>
		/// Tests the behavior of the run thread if an exception is thrown.
		/// </summary>
		[Test]
		public void RunThreadException( )
		{
			MstscEventArgs exitedArgs = null;
			var mstscApp = new TestMstscApp();
			mstscApp.Exited += new EventHandler<MstscEventArgs>(
				( o, e ) => exitedArgs = e );

			var settings = new MstscSettings();
			settings.Computer = new ComputerName( "chrismillerpc", null );
			mstscApp.StartProcessException = new Exception();
			mstscApp.Connect( settings );

			// The exit event should still fire.
			Assert.IsNotNull( exitedArgs );
		}
	}
}
