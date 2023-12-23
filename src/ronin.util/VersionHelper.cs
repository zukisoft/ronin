//---------------------------------------------------------------------------
// Copyright (c) 2004-2024 Michael G. Brehm
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//---------------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace zuki.ronin.util
{
	/// <summary>
	/// Provides support for determining the operating system version level
	/// 
	/// Based on:
	/// https://stackoverflow.com/questions/31550444/c-sharp-how-to-use-the-new-version-helper-api
	/// </summary>
	public static class VersionHelper
	{
		#region Win32 API Declarations
		private static class NativeMethods
		{
			public const byte VER_GREATER_EQUAL = 3;
			public const uint VER_MINORVERSION = 0x0000001;
			public const uint VER_MAJORVERSION = 0x0000002;
			public const uint VER_BUILDNUMBER = 0x0000004;
			public const ushort _WIN32_WINNT_WINTHRESHOLD = 0x0A00;

			public static byte LOBYTE(ushort word)
			{
				return (byte)(word & 0xff);
			}

			public static byte HIBYTE(ushort word)
			{
				return (byte)(word >> 8 & 0xff);
			}

			[DllImport("kernel32.dll")]
			public static extern ulong VerSetConditionMask(ulong ConditionMask, uint TypeMask, byte Condition);

			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool VerifyVersionInfoW(ref OSVERSIONINFOEXW lpVersionInformation, uint dwTypeMask, ulong dwlConditionMask);

			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			public struct OSVERSIONINFOEXW
			{
				public int dwOSVersionInfoSize;
				public int dwMajorVersion;
				public int dwMinorVersion;
				public int dwBuildNumber;
				public int dwPlatformId;
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
				public string szCSDVersion;
				public ushort wServicePackMajor;
				public ushort wServicePackMinor;
				public ushort wSuiteMask;
				public byte wProductType;
				public byte wReserved;
			}
		}
		#endregion

		/// <summary>
		/// Determines if the Operating System is Windows 10 (specific build) or newer
		/// </summary>
		/// <param name="build">Specific build number for Windows 10</param>
		public static bool IsWindows10OrGreater(int build)
		{
			NativeMethods.OSVERSIONINFOEXW osversioninfo = new NativeMethods.OSVERSIONINFOEXW
			{
				dwOSVersionInfoSize = Marshal.SizeOf(typeof(NativeMethods.OSVERSIONINFOEXW))
			};

			ulong mask = NativeMethods.VerSetConditionMask(
				NativeMethods.VerSetConditionMask(
				NativeMethods.VerSetConditionMask(
					0, NativeMethods.VER_MAJORVERSION, NativeMethods.VER_GREATER_EQUAL),
					   NativeMethods.VER_MINORVERSION, NativeMethods.VER_GREATER_EQUAL),
					   NativeMethods.VER_BUILDNUMBER, NativeMethods.VER_GREATER_EQUAL);

			osversioninfo.dwMajorVersion = NativeMethods.HIBYTE(NativeMethods._WIN32_WINNT_WINTHRESHOLD);
			osversioninfo.dwMinorVersion = NativeMethods.LOBYTE(NativeMethods._WIN32_WINNT_WINTHRESHOLD);
			if(build > 0) osversioninfo.dwBuildNumber = build;

			return NativeMethods.VerifyVersionInfoW(ref osversioninfo, NativeMethods.VER_MAJORVERSION | NativeMethods.VER_MINORVERSION |
				((build > 0) ? NativeMethods.VER_BUILDNUMBER : 0), mask) != false;
		}

		/// <summary>
		/// Determines if the Operating System is Windows 10 (any build) or newer
		/// </summary>
		/// <returns></returns>
		public static bool IsWindows10OrGreater()
		{
			return IsWindows10OrGreater(0);
		}

		/// <summary>
		/// Determines if the Operating System is Windows 11 or newer
		/// </summary>
		/// <returns></returns>
		public static bool IsWindows11OrGreater()
		{
			return IsWindows10OrGreater(22000);
		}
	}
}