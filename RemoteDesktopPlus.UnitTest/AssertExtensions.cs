using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	public static class Assert2
	{
		public static void Throws( Type exceptionType, Action a )
		{
			try
			{
				a();
			}
			catch ( Exception ex )
			{
				if ( ex.GetType() == exceptionType )
					return;
				throw;
			}

			Assert.Fail( string.Format( "{0} not thrown.", exceptionType ) );
		}

		public static void AssertComputerNames( string[] expected, ComputerName[] actual )
		{
			if ( expected.Length % 2 > 0 )
				throw new ArgumentException( "Number of expected strings should be divisable by two.  Expecting name,alias pairs." );

			ComputerName[] names = new ComputerName[expected.Length / 2];
			for ( int i = 0; i < expected.Length; ++i )
			{
				names[i / 2] = new ComputerName( expected[i], expected[++i] );
			}

			Assert.AreEqual( names, actual );
		}
	}
}
