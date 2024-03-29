﻿//---------------------------------------------------------------------------
// Copyright (c) 2004-2024 Michael G. Brehm
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
using System.Diagnostics;

using zuki.ronin.ui;

namespace zuki.ronin
{
	/// <summary>
	/// Implements the About dialog
	/// </summary>
	public partial class AboutDialog : FormBase
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public AboutDialog()
		{
			InitializeComponent();

			// Get the version information for the binary and update the label
			FileVersionInfo fileverinfo = FileVersionInfo.GetVersionInfo(typeof(AboutDialog).Assembly.Location);
			m_version.Text = "RONIN v" + fileverinfo.FileVersion;

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}
	}
}
