﻿//---------------------------------------------------------------------------
// Copyright (c) 2004-2022 Michael G. Brehm
// Copyright (c) 2007-2009 Sean M. Patterson
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

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace zuki.ronin
{
	/// <summary>
	/// Implements the static application entry point
	/// </summary>
	internal static class Program
	{
		#region Win32 API Declarations
		private static class NativeMethods
		{
			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool SetProcessDPIAware();
		}
		#endregion

		/// <summary>
		/// Application entry point
		/// </summary>
		[STAThread]
		private static void Main()
		{
			if(Environment.OSVersion.Version.Major >= 6) NativeMethods.SetProcessDPIAware();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			using(var mainform = new MainForm()) Application.Run(mainform);
		}
	}
}
