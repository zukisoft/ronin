﻿//---------------------------------------------------------------------------
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

using System;
using System.Drawing;
using zuki.ronin.ui;

namespace zuki.ronin
{
	public partial class UpdateArtworkDialog : FormBase
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		private UpdateArtworkDialog()
		{
			InitializeComponent();

			// Reset the theme based on the current system settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Instance Constructor
		/// </summary>
		/// <param name="original">Original artwork image</param>
		/// <param name="updated">Updated artwork image</param>
		public UpdateArtworkDialog(Bitmap original, Bitmap updated) : this()
		{
			m_original.Image = original ?? throw new ArgumentNullException(nameof(original));
			m_updated.Image = updated ?? throw new ArgumentNullException(nameof(updated));
		}
	}
}
