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

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Fixes a flicker and TopItem problem with the WinForms ListView
	/// 
	/// Source: https://objectlistview.sourceforge.net/cs/blog6.html
	/// </summary>
	internal class VirtualListView : ListView
	{
		#region Win32 API Declarations
		private static class NativeMethods
		{
			public const uint LVM_SETITEMCOUNT = 0x1000 + 47;
			public const uint LVSICF_NOSCROLL = 0x02;

			[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			public static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam = default, IntPtr lParam = default);
		}
		#endregion

		/// <summary>
		/// Static Constructor
		/// </summary>
		static VirtualListView()
		{
			s_fieldinfo = typeof(ListView).GetField("virtualListSize", BindingFlags.NonPublic | BindingFlags.Instance);
			Debug.Assert(s_fieldinfo != null);
		}

		//---------------------------------------------------------------------
		// Protected Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Replaces ListView::VirtualListSize
		/// </summary>
		protected new virtual int VirtualListSize
		{
			get { return base.VirtualListSize; }
			set
			{
				if(value == VirtualListSize || value < 0)return;

				// Set the base class private field
				s_fieldinfo.SetValue(this, value);

				// Send a raw message to change the virtual list size *without* changing the scroll position
				if(IsHandleCreated && !DesignMode)
					NativeMethods.SendMessageW(Handle, NativeMethods.LVM_SETITEMCOUNT, new IntPtr(value), new IntPtr(NativeMethods.LVSICF_NOSCROLL));
			}
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		private static readonly FieldInfo s_fieldinfo;
	}
}
