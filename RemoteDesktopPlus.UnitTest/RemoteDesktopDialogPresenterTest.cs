using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class RemoteDesktopDialogPresenterTest
	{
		[Test]
		public void SmokeTest( )
		{
			RemoteDesktopDialogPresenter presenter = new RemoteDesktopDialogPresenter();
			Assert.IsTrue( true );
		}
	} // RemoteDesktopDialogPresenterTest
} // namespace MillerX.RemoteDesktopPlus.UnitTest
