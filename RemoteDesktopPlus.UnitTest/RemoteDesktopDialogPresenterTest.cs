﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	[TestFixture]
	public class RemoteDesktopDialogPresenterTest
	{
        class TestRemoteDesktopDialogPresenter : RemoteDesktopDialogPresenter
        {
            public new RecentComputerList RecentComputerList { get { return base.RecentComputerList; } }
        }

        /// <summary>
        /// Tests unaliasing the computer name.
        /// </summary>
        [Test]
        public void BuildComputerNameTest( )
        {
            var presenter = new TestRemoteDesktopDialogPresenter();
            presenter.RecentComputerList.Add( new ComputerName( "1.1.1.1", "alias" ) );

            // Unalias
            Assert.AreEqual( new ComputerName( "1.1.1.1", "alias" ),
                presenter.BuildComputerName( "alias", "" ) );

            // Retrieve computer and alias given just the computer name.
            Assert.AreEqual( new ComputerName( "1.1.1.1", "alias" ),
                presenter.BuildComputerName( "1.1.1.1", "" ) );

            // Change alias on an existing item.
            Assert.AreEqual( new ComputerName( "1.1.1.1", "foobar" ),
                presenter.BuildComputerName( "1.1.1.1", "foobar" ) );

            // Create a new value if not found in the RecentComputerList.
            Assert.AreEqual( new ComputerName( "2.2.2.2", "OtherAlias" ),
                presenter.BuildComputerName( "2.2.2.2", "OtherAlias" ) );
        }

        /// <summary>
        /// There is some ambiguity in the UI.  If you have an alias in the Computer Name drop-down
        /// and a name in the Alias textbox then don't do anything with the Alias textbox.
        /// </summary>
        [Test]
        public void DontAliasAnAlias( )
        {
            var presenter = new TestRemoteDesktopDialogPresenter();
            presenter.RecentComputerList.Add( new ComputerName( "1.1.1.1", "MyAlias" ) );

            Assert.AreEqual( new ComputerName( "1.1.1.1", "MyAlias" ),
                presenter.BuildComputerName( "MyAlias", "OtherAlias" ) );
        }

        /// <summary>
        /// Tests that if we put an IP address as an alias, RDP assumes user error and flips the computer name and alias.
        /// </summary>
        [Test]
        public void FlipComputerNameAndAlias( )
        {
            var presenter = new TestRemoteDesktopDialogPresenter();

            // 1.1.1.1 was intended to be the computer name but we mistyped.
            Assert.AreEqual( new ComputerName( "1.1.1.1", "alias" ),
                presenter.BuildComputerName( "alias", "1.1.1.1" ) );

            // Do we handle not having an alias.
            Assert.AreEqual( new ComputerName( "chrismillerpc", null ),
                presenter.BuildComputerName( "chrismillerpc", "" ) );
        }
    } // RemoteDesktopDialogPresenterTest
} // namespace MillerX.RemoteDesktopPlus.UnitTest
