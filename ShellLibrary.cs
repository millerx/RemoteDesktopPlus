using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ShellLib
{
	//-------------------------------------------------------------------------

	class ShellFunctions
	{
		public static IShellFolder GetDesktopFolder( )
		{
			IntPtr ptrRet;
			ShellApi.SHGetDesktopFolder( out ptrRet );

			System.Type shellFolderType = System.Type.GetType( "ShellLib.IShellFolder" );
			Object obj = Marshal.GetTypedObjectForIUnknown( ptrRet, shellFolderType );
			IShellFolder ishellFolder = (IShellFolder) obj;

			return ishellFolder;
		}

		public static IMalloc GetMalloc( )
		{
			IntPtr ptrRet;
			ShellApi.SHGetMalloc( out ptrRet );

			System.Type mallocType = System.Type.GetType( "ShellLib.IMalloc" );
			Object obj = Marshal.GetTypedObjectForIUnknown( ptrRet, mallocType );
			IMalloc imalloc = (IMalloc) obj;

			return imalloc;
		}
	}

	//-------------------------------------------------------------------------

	class ShellApi
	{
		[DllImport( "shell32.dll" )]
		public static extern Int32 SHGetDesktopFolder(
			out IntPtr ppshf );

		[DllImport( "shell32.dll" )]
		public static extern Int32 SHGetMalloc(
			out IntPtr hObject );

		[DllImport( "shlwapi.dll" )]
		public static extern Int32 StrRetToBuf(
			ref STRRET pstr,
			IntPtr pidl,
			System.Text.StringBuilder pszBuf,
			UInt32 cchBuf );
	}

	//-------------------------------------------------------------------------

	[ComImport]
	[InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
	[Guid( "000214E6-0000-0000-C000-000000000046" )]
	interface IShellFolder
	{
		[PreserveSig]
		Int32 ParseDisplayName(
			IntPtr hwnd,
			IntPtr pbc,
			[MarshalAs( UnmanagedType.LPWStr )] 
			String pszDisplayName,
			ref UInt32 pchEaten,
			out IntPtr ppidl,
			ref UInt32 pdwAttributes );

		[PreserveSig]
		Int32 EnumObjects(
			IntPtr hwnd,
			Int32 grfFlags,
			out IntPtr ppenumIDList );

		[PreserveSig]
		Int32 BindToObject(
			IntPtr pidl,
			IntPtr pbc,
			Guid riid,
			out IntPtr ppv );

		[PreserveSig]
		Int32 BindToStorage(
			IntPtr pidl,
			IntPtr pbc,
			Guid riid,
			out IntPtr ppv );

		[PreserveSig]
		Int32 CompareIDs(
			Int32 lParam,
			IntPtr pidl1,
			IntPtr pidl2 );

		[PreserveSig]
		Int32 CreateViewObject(
			IntPtr hwndOwner,
			Guid riid,
			out IntPtr ppv );

		[PreserveSig]
		Int32 GetAttributesOf(
			UInt32 cidl,
			[MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 0 )]
			IntPtr[] apidl,
			ref UInt32 rgfInOut );

		[PreserveSig]
		Int32 GetUIObjectOf(
			IntPtr hwndOwner,
			UInt32 cidl,
			IntPtr[] apidl,
			Guid riid,
			ref UInt32 rgfReserved,
			out IntPtr ppv );

		[PreserveSig]
		Int32 GetDisplayNameOf(
			IntPtr pidl,
			UInt32 uFlags,
			out STRRET pName );

		[PreserveSig]
		Int32 SetNameOf(
			IntPtr hwnd,
			IntPtr pidl,
			[MarshalAs( UnmanagedType.LPWStr )] 
			String pszName,
			UInt32 uFlags,
			out IntPtr ppidlOut );
	}

	//-------------------------------------------------------------------------

	[StructLayout( LayoutKind.Explicit )]
	struct STRRET
	{
		[FieldOffset( 0 )]
		public UInt32 uType;

		[FieldOffset( 4 )]
		public IntPtr pOleStr;

		[FieldOffset( 4 )]
		public IntPtr pStr;

		[FieldOffset( 4 )]
		public UInt32 uOffset;

		[FieldOffset( 4 )]
		public IntPtr cStr;
	}

	//-------------------------------------------------------------------------

	[ComImport]
	[InterfaceType( ComInterfaceType.InterfaceIsIUnknown )]
	[Guid( "00000002-0000-0000-C000-000000000046" )]
	interface IMalloc
	{
		[PreserveSig]
		IntPtr Alloc(
			UInt32 cb );

		[PreserveSig]
		IntPtr Realloc(
			IntPtr pv,
			UInt32 cb );

		[PreserveSig]
		void Free(
			IntPtr pv );

		[PreserveSig]
		UInt32 GetSize(
			IntPtr pv );

		[PreserveSig]
		Int16 DidAlloc(
			IntPtr pv );

		[PreserveSig]
		void HeapMinimize( );
	}

	//-------------------------------------------------------------------------

	enum SHGNO
	{
		SHGDN_NORMAL = 0x0000,
		SHGDN_INFOLDER = 0x0001,
		SHGDN_FOREDITING = 0x1000,
		SHGDN_FORADDRESSBAR = 0x4000,
		SHGDN_FORPARSING = 0x8000
	}

	//-------------------------------------------------------------------------
}
