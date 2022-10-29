//---------------------------------------------------------------------------
// Copyright (c) 2004-2022 Michael G. Brehm
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
using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace zuki.ronin
{
	/// <summary>
	/// Implements the main application form
	/// </summary>
	public partial class MainForm : Form
	{
		#region Win32 API Declarations
		private static class NativeMethods
		{
			public const int WS_EX_COMPOSITED = 0x02000000;

			public enum DWMWINDOWATTRIBUTE
			{
				DWMWA_WINDOW_CORNER_PREFERENCE = 33
			}

			public enum DWM_WINDOW_CORNER_PREFERENCE
			{
				DWMWCP_DEFAULT = 0,
				DWMWCP_DONOTROUND = 1,
				DWMWCP_ROUND = 2,
				DWMWCP_ROUNDSMALL = 3
			}

			[DllImport("dwmapi.dll")]
			public static extern long DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);
		}
		#endregion

		/// <summary>
		/// Instance Constructor
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			// WM_PAINT-based background
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.ResizeRedraw, true);

			// Enable double-buffering
			this.EnableDoubleBuffering();

			// WINDOWS 11
			if(VersionHelper.IsWindows11OrGreater())
			{
				// Apply rounded corners to the form
				NativeMethods.DWMWINDOWATTRIBUTE attribute = NativeMethods.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
				NativeMethods.DWM_WINDOW_CORNER_PREFERENCE preference = NativeMethods.DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
				NativeMethods.DwmSetWindowAttribute(Handle, attribute, ref preference, sizeof(uint));
			}

			// Precalculate a DPI-based scaling factor that be applied to child controls
			using(Graphics graphics = CreateGraphics())
			{
				m_scalefactor = new SizeF(graphics.DpiX / 96.0F, graphics.DpiY / 96.0F);
			}

			// Manual DPI scaling
			Padding = Padding.ScaleDPI(m_scalefactor);
		}

		//---------------------------------------------------------------------
		// Event Handlers
		//---------------------------------------------------------------------

		/// <summary>
		/// Invoked when the File/Exit menu item has been selected
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnFileExit(object sender, EventArgs args)
		{
			Close();
		}

		/// <summary>
		/// Invoked when the Help/About menu item has been selected
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnHelpAbout(object sender, EventArgs args)
		{
			using(var dialog = new AboutDialog())
			{
				dialog.ShowDialog(this);
			}
		}

		//---------------------------------------------------------------------
		// Form Overrides
		//---------------------------------------------------------------------

		/// <summary>
		/// Overrides Form::OnPaint
		/// </summary>
		/// <param name="args">Paint event arguments</param>
		protected override void OnPaint(PaintEventArgs args)
		{
			base.OnPaint(args);				// Invoke base implementation first

			if((Width == 0) || (Height == 0)) return;

			// Paint the background of the form to match the linear gradient of the MenuStrip
			using(LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height),
				s_colortable.MenuStripGradientBegin, s_colortable.MenuStripGradientEnd, LinearGradientMode.Horizontal))
			{
				args.Graphics.FillRectangle(brush, new Rectangle(0, 0, Width, Height));
			}
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// DPI scaling factor to be applied across the application
		/// </summary>
		private readonly SizeF m_scalefactor = SizeF.Empty;

		/// <summary>
		/// Color table for painting the background of the form
		/// </summary>
		private static readonly ProfessionalColorTable s_colortable = new ProfessionalColorTable();
	}
}
