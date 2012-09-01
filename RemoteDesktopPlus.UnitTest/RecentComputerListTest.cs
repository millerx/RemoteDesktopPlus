using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MillerX.RemoteDesktopPlus.UnitTest
{
	/// <summary>
	/// Tests remembering only a limited number of IP addresses
	/// </summary>
	[TestFixture]
	public class RecentComputerListTest
	{
		private RecentComputerList ListFromStrings( string[] cnames )
		{
			RecentComputerList computers = new RecentComputerList();

			foreach ( string cname in cnames )
			{
				computers.Add( new ComputerName( cname, null ) );
			}

			return computers;
		}

		private string[] GetComputers( RecentComputerList recentList )
		{
			string[] computers = new string[recentList.Count];

			int index = 0;
			foreach ( ComputerName name in recentList )
			{
				computers[index] = name.Computer;
				++index;
			}

			return computers;
		}

		[TestFixtureSetUp]
		public void TestFixtureSetUp( )
		{
			Config.MaxComputerCount = 10;
			Config.MaxIpAddressCount = 3;
		}

		/// <summary>
		/// Tests that we can create a RecentComputerList with at most 10 computers.
		/// </summary>
		[Test]
		public void CreateWithMaxCount( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj",
				"kkkk"
			} );

			// kkkk is not added because it is the 11th computer and we can only hold 10.
			Assert.AreEqual( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests pushing a computer onto a list that isn't full.  Expect no servers to be removed.
		/// </summary>
		[Test]
		public void PushComputerToNonFullList( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc"
			} );

			list.Push( new ComputerName( "xxxx", null ) );

			Assert.AreEqual( new string[]
			{
				"xxxx",
				"aaaa",
				"bbbb",
				"cccc"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests pushing a computer onto a full list.  Expect last server to be removed.
		/// </summary>
		[Test]
		public void PushComputerToFullList( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			} );

			list.Push( new ComputerName( "xxxx", null ) );

			Assert.AreEqual( new string[]
			{
				"xxxx",
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests pushing an IP address onto a full list of computer names.  Expect last server to be removed like normal.
		/// </summary>
		[Test]
		public void PushIpAddressToFullList( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			} );

			list.Push( new ComputerName( "9.9.9.9", null ) );

			Assert.AreEqual( new string[]
			{
				"9.9.9.9",
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests pushing an IP address to a full list.  There is already three IP address so one is
		/// removed instead of the last item in the list.
		/// </summary>
		[Test]
		public void PushIpAddressAndRemoveIpAddress( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"1.1.1.1",
				"cccc",
				"2.2.2.2",
				"eeee",
				"3.3.3.3",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			} );

			list.Push( new ComputerName( "9.9.9.9", null ) );

			// 3.3.3.3 is removed instead of jjjj
			Assert.AreEqual( new string[]
			{
				"9.9.9.9",
				"aaaa",
				"1.1.1.1",
				"cccc",
				"2.2.2.2",
				"eeee",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests that if the list is not full we can have more than three IP addresses.
		/// </summary>
		[Test]
		public void HaveMoreThanThreeIpAddressesIfListNotFull( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"1.1.1.1",
				"bbbb",
				"2.2.2.2",
				"cccc",
				"3.3.3.3"
			} );

			list.Push( new ComputerName( "9.9.9.9", null ) );

			Assert.AreEqual( new string[]
			{
				"9.9.9.9",
				"aaaa",
				"1.1.1.1",
				"bbbb",
				"2.2.2.2",
				"cccc",
				"3.3.3.3"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests pushing a computer that already exists in the list.  There should be no duplicates.
		/// Instead, we move the computer to the top of the list.
		/// </summary>
		[Test]
		public void PushDuplicateToNonFullList( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc"
			} );

			list.Push( new ComputerName( "bbbb", null ) );

			// bbbb moves to top.  No duplicates.
			Assert.AreEqual( new string[]
			{
				"bbbb",
				"aaaa",
				"cccc"
			},
			GetComputers( list ) );

			// Test comparison is case-insensitive.
			list.Push( new ComputerName( "AAAA", null ) );

			// bbbb moves to top.  No duplicates.
			Assert.AreEqual( new string[]
			{
				"AAAA",
				"bbbb",
				"cccc"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests pushing a computer that already exists in a full list.  Nothing should be removed.
		/// </summary>
		[Test]
		public void PushDuplicateToFullList( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			} );

			list.Push( new ComputerName( "dddd", null ) );

			// Even though the list was full nothing is removed.
			Assert.AreEqual( new string[]
			{
				"dddd",
				"aaaa",
				"bbbb",
				"cccc",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests that the duplicate is moved and no IP addresses are removed even though we have too many.
		/// </summary>
		[Test]
		public void PushDuplicateWhereTooManyIPs( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"1.1.1.1",
				"cccc",
				"2.2.2.2",
				"eeee",
				"3.3.3.3",
				"gggg",
				"4.4.4.4",
				"iiii",
				"5.5.5.5"
			} );

			list.Push( new ComputerName( "eeee", null ) );

			// No IP addresses removed even though we have too many.
			Assert.AreEqual( new string[]
			{
				"eeee",
				"aaaa",
				"1.1.1.1",
				"cccc",
				"2.2.2.2",
				"3.3.3.3",
				"gggg",
				"4.4.4.4",
				"iiii",
				"5.5.5.5"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests that the duplicate is moved and no IP addresses are removed.  The duplicate is an IP address.
		/// </summary>
		[Test]
		public void PushDuplicateIpWhereTooManyIPs( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"1.1.1.1",
				"cccc",
				"2.2.2.2",
				"eeee",
				"3.3.3.3",
				"gggg",
				"4.4.4.4",
				"iiii",
				"5.5.5.5"
			} );

			list.Push( new ComputerName( "3.3.3.3", null ) );

			// No IP addresses removed even though we have too many.
			Assert.AreEqual( new string[]
			{
				"3.3.3.3",
				"aaaa",
				"1.1.1.1",
				"cccc",
				"2.2.2.2",
				"eeee",
				"gggg",
				"4.4.4.4",
				"iiii",
				"5.5.5.5"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests pushing a named computer to a full list with max IP addresses.
		/// </summary>
		[Test]
		public void PushComputerToIpFullList( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"1.1.1.1",
				"2.2.2.2",
				"3.3.3.3",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			} );

			list.Push( new ComputerName( "xxxx", null ) );

			Assert.AreEqual( new string[]
			{
				"xxxx",
				"aaaa",
				"bbbb",
				"cccc",
				"1.1.1.1",
				"2.2.2.2",
				"3.3.3.3",
				"gggg",
				"hhhh",
				"iiii",
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests the add method - for initialization only.
		/// </summary>
		[Test]
		public void Add( )
		{
			RecentComputerList computers = new RecentComputerList();

			Assert.IsTrue( computers.Add( new ComputerName( "cpu1", null ) ) );
			Assert.AreEqual( new string[] {
				"cpu1" },
				GetComputers( computers ) );

			Assert.IsTrue( computers.Add( new ComputerName( "cpu2", null ) ) );
			Assert.AreEqual( new string[] {
				"cpu1",
				"cpu2" },
				GetComputers( computers ) );

			RecentComputerList fullList = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			} );

			Assert.IsFalse( fullList.Add( new ComputerName( "cpu1", null ) ) );

			Assert.AreEqual( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			},
			GetComputers( fullList ) );
		}

		/// <summary>
		/// Tests the IList.Add method.
		/// </summary>
		[Test]
		public void ICollectionAdd( )
		{
			ICollection<ComputerName> collection = new RecentComputerList();

			collection.Add( new ComputerName( "cpu1", null ) );
			Assert.AreEqual( new string[] {
				"cpu1" },
				GetComputers( (RecentComputerList) collection ) );

			collection.Add( new ComputerName( "cpu2", null ) );
			Assert.AreEqual( new string[] {
				"cpu1",
				"cpu2" },
				GetComputers( (RecentComputerList) collection ) );

			ICollection<ComputerName> fullCollection = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			} );

			fullCollection.Add( new ComputerName( "cpu1", null ) );

			Assert.AreEqual( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			},
			GetComputers( (RecentComputerList) fullCollection ) );
		}

		/// <summary>
		/// Tests the Clear method.
		/// </summary>
		[Test]
		public void Clear( )
		{
			RecentComputerList computers = new RecentComputerList();

			computers.Add( new ComputerName( "cpu1", null ) );
			Assert.AreEqual( new string[] {
				"cpu1" },
				GetComputers( computers ) );

			computers.Clear();

			Assert.AreEqual( new string[0],
				GetComputers( computers ) );
		}

		[Test]
		public void Contains( )
		{
			RecentComputerList computers = new RecentComputerList();

			computers.Add( new ComputerName( "cpu1", null ) );
			computers.Add( new ComputerName( "cpu2", "alias2" ) );

			// Basic check
			Assert.IsTrue( computers.Contains( new ComputerName( "cpu1", null ) ) );
			// Must match both computer and alias.
			Assert.IsFalse( computers.Contains( new ComputerName( "cpu1", "alias1" ) ) );
			Assert.IsTrue( computers.Contains( new ComputerName( "cpu2", "alias2" ) ) );
			// Case insensitive for computer name and alias.
			Assert.IsTrue( computers.Contains( new ComputerName( "CPU2", "alias2" ) ) );
			Assert.IsTrue( computers.Contains( new ComputerName( "cpu2", "ALIAS2" ) ) );
		}

		/// <summary>
		/// Tests ICollection.CopyTo.
		/// </summary>
		[Test]
		public void CopyTo( )
		{
			ICollection<ComputerName> collection = new RecentComputerList();
			collection.Add( new ComputerName( "cpu1", "alias1" ) );
			collection.Add( new ComputerName( "cpu2", "alias2" ) );

			ComputerName[] array = new ComputerName[3];
			array[0] = new ComputerName( "0.0.0.0", "zero" );
			collection.CopyTo( array, 1 );

			Assert2.AssertComputerNames( new string[]
			{
				"0.0.0.0", "zero",
				"cpu1", "alias1",
				"cpu2", "alias2",
			}, array );
		}

		/// <summary>
		/// Tests the ICollection.Count property.
		/// </summary>
		[Test]
		public void Count( )
		{
			RecentComputerList computers = new RecentComputerList();
			Assert.AreEqual( 0, computers.Count );

			computers.Add( new ComputerName( "cpu1", "alias1" ) );
			Assert.AreEqual( 1, computers.Count );

			computers.Add( new ComputerName( "cpu2", "alias2" ) );
			Assert.AreEqual( 2, computers.Count );
		}

		/// <summary>
		/// Tests the Remove method.
		/// </summary>
		[Test]
		public void Remove( )
		{
			RecentComputerList computers = new RecentComputerList();
			computers.Add( new ComputerName( "cpu1", "alias1" ) );
			computers.Add( new ComputerName( "cpu2", "alias2" ) );
			Assert.AreEqual( 2, computers.Count );

			computers.Remove( new ComputerName( "cpu1", "alias1" ) );
			Assert.AreEqual( 1, computers.Count );

			// Is Remove case insensitive?
			computers.Remove( new ComputerName( "CPU2", "ALIAS2" ) );
			Assert.AreEqual( 0, computers.Count );
		}

		/// <summary>
		/// ToString is used to display computers in the drop-down.  Tests that we display the alias
		/// and if it's not available then the computer name.
		/// </summary>
		[Test]
		public void ToStringTest( )
		{
			ComputerName withAlias = new ComputerName( "1.1.1.1", "alias" );
			Assert.AreEqual( "alias", withAlias.ToString() );

			ComputerName withoutAlias = new ComputerName( "work", null );
			Assert.AreEqual( "work", withoutAlias.ToString() );

			// Do we treat empty string as null?
			ComputerName emptyAlias = new ComputerName( "afar", "" );
			Assert.AreEqual( "afar", emptyAlias.ToString() );
		}

		/// <summary>
		/// Tests the Find method.
		/// </summary>
		[Test]
		public void Find( )
		{
			RecentComputerList computers = new RecentComputerList();
			ComputerName withAlias = new ComputerName( "1.1.1.1", "alias" ); 
			ComputerName justComputer = new ComputerName( "work", null );
			computers.Add( withAlias );
			computers.Add( justComputer );

			// Find by alias
			Assert.AreEqual( withAlias, computers.Find( "alias" ) );
			// Find by computer
			Assert.AreEqual( withAlias, computers.Find( "1.1.1.1" ) );
			// Find with no alias
			Assert.AreEqual( justComputer, computers.Find( "work" ) );
			// Find by alias, case insensitive
			Assert.AreEqual( withAlias, computers.Find( "ALIAS" ) );
			// Find by computer, case insensitive
			Assert.AreEqual( justComputer, computers.Find( "WORK" ) );
			// Not found
			Assert.IsNull( computers.Find( "foobar" ) );
		}

		/// <summary>
		/// Tests the copy constructor.
		/// </summary>
		[Test]
		public void CopyCtor( )
		{
			var computers = new RecentComputerList();
			computers.Add( new ComputerName( "1.1.1.1", "work", true ) );
			computers.Add( new ComputerName( "2.2.2.2", "home", false ) );

			var otherComps = new RecentComputerList( computers );

			// Did we copy?
			Assert.AreEqual( new ComputerName[]
				{
					new ComputerName( "1.1.1.1", "work", true ),
					new ComputerName( "2.2.2.2", "home", false )
				},
				otherComps.ToArray() );

			// Are the underlying lists different objects?
			computers.Remove( computers.Find( "home" ) );
			Assert.AreEqual( 1, computers.Count );
			Assert.AreEqual( 2, otherComps.Count );

			// Are the underlayng ComputerName different objects?
			foreach ( var name in computers )
			{
				name.Alias = null;
			}
			Assert.IsNull( computers.Find( "work" ) );
			Assert.IsNotNull( otherComps.Find( "work" ) );
		}

		/// <summary>
		/// Tests what happens when we add an aliased computer where the alias already exists in the list.
		/// </summary>
		[Test]
		public void DuplicateAlias( )
		{
			RecentComputerList computers = new RecentComputerList();
			computers.Add( new ComputerName( "1.1.1.1", "work" ) );
			computers.Add( new ComputerName( "2.2.2.2", "CASE" ) );

			computers.Push( new ComputerName( "3.3.3.3", "work" ) );

			// Alias is removed from the old item.
			Assert2.AssertComputerNames( new string[]
			{
				"3.3.3.3", "work",
				"1.1.1.1", null,
				"2.2.2.2", "CASE",
			}, computers.ToArray() );

			// Confirm case sensitive.
			computers.Push( new ComputerName( "4.4.4.4", "case" ) );

			Assert2.AssertComputerNames( new string[]
			{
				"4.4.4.4", "case",
				"3.3.3.3", "work",
				"1.1.1.1", null,
				"2.2.2.2", null,
			}, computers.ToArray() );
		}

		/// <summary>
		/// Tests the GetAliases method.
		/// </summary>
		[Test]
		public void GetAliases( )
		{
			RecentComputerList computers = new RecentComputerList();
			computers.Add( new ComputerName( "1.1.1.1", null ) );
			computers.Add( new ComputerName( "2.2.2.2", "work" ) );
			computers.Add( new ComputerName( "3.3.3.3", null ) );
			computers.Add( new ComputerName( "4.4.4.4", "home" ) );

			Assert.AreEqual( new string[] { "work", "home" }, computers.GetAliases().ToArray() );

			// Test with no aliases
			computers = new RecentComputerList();
			computers.Add( new ComputerName( "1.1.1.1", null ) );

			Assert.AreEqual( new string[0], computers.GetAliases().ToArray() );

			// Test with no items
			computers = new RecentComputerList();

			Assert.AreEqual( new string[0], computers.GetAliases().ToArray() );
		}

		/// <summary>
		/// Tests changing the max computer count of the list.
		/// </summary>
		[Test]
		public void VariableMaxComputerCount( )
		{
			RecentComputerList list = new RecentComputerList( 4, 3 );
			list.Push( new ComputerName( "eeee", null ) );
			list.Push( new ComputerName( "dddd", null ) );
			list.Push( new ComputerName( "cccc", null ) );
			list.Push( new ComputerName( "bbbb", null ) );
			list.Push( new ComputerName( "aaaa", null ) );

			Assert.AreEqual( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd"
			},
			GetComputers( list ) );

			list.MaxComputerCount = 5;
			list.Push( new ComputerName( "eeee", null ) );

			Assert.AreEqual( new string[]
			{
				"eeee",
				"aaaa",
				"bbbb",
				"cccc",
				"dddd"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests changing the max IP addresses allowed in the list.
		/// </summary>
		[Test]
		public void VariableMaxIpAddresses( )
		{
			RecentComputerList list = ListFromStrings( new string[]
			{
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			} );

			list.MaxIpAddressCount = 2;
			list.Push( new ComputerName( "1.1.1.1", null ) );
			list.Push( new ComputerName( "2.2.2.2", null ) );
			list.Push( new ComputerName( "3.3.3.3", null ) );

			Assert.AreEqual( new string[]
			{
				"3.3.3.3",
				"2.2.2.2",
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff",
				"gggg",
				"hhhh"
			},
			GetComputers( list ) );

			list.MaxIpAddressCount = 4;
			list.Push( new ComputerName( "4.4.4.4", null ) );
			list.Push( new ComputerName( "5.5.5.5", null ) );
			list.Push( new ComputerName( "6.6.6.6", null ) );

			Assert.AreEqual( new string[]
			{
				"6.6.6.6",
				"5.5.5.5",
				"4.4.4.4",
				"3.3.3.3",
				"aaaa",
				"bbbb",
				"cccc",
				"dddd",
				"eeee",
				"ffff"
			},
			GetComputers( list ) );
		}

		/// <summary>
		/// Tests that we have a max of 3 devlab servers in the list.  A devlab server is any
		/// server name that ends in a number.
		/// </summary>
		[Test]
		public void PushMaxDevlabServer()
		{
			RecentComputerList list = ListFromStrings(new string[]
			{
				"aaaa",
				"server1",
				"cccc",
				"server2",
				"eeee",
				"server3",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			});

			list.Push(new ComputerName("server9", null));

			// 3.3.3.3 is removed instead of jjjj
			Assert.AreEqual(new string[]
			{
				"server9",
				"aaaa",
				"server1",
				"cccc",
				"server2",
				"eeee",
				"gggg",
				"hhhh",
				"iiii",
				"jjjj"
			},
			GetComputers(list));
		}
	}
}
