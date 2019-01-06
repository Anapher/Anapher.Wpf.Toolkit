﻿using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Anapher.Wpf.Toolkit.Native
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SHSTOCKICONINFO
	{
		public UInt32 cbSize;
		public IntPtr hIcon;
		public Int32 iSysIconIndex;
		public Int32 iIcon;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260 /*MAX_PATH*/)] public string szPath;
	}
}