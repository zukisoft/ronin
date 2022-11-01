//---------------------------------------------------------------------------
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

using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
using System.Drawing;

using Microsoft.Win32;

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

			// Enable double-buffering
			this.EnableDoubleBuffering();

			// Wire up the application theme change handler
			m_appthemechanged = new EventHandler(OnApplicationThemeChanged);
			ApplicationTheme.Changed += m_appthemechanged;
			
			// Reset the theme based on the current system settings
			UpdateTheme();
			
			// Set the custom professional renderer for the MenuStrip
			m_menu.Renderer = new ToolStripProfessionalRenderer(ApplicationTheme.ProfessionalColorTable);

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

			// Create the system theme change monitor instance
			if(VersionHelper.IsWindows10OrGreater())
			{
				m_thememonitor = new RegistryKeyValueChangeMonitor(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
				m_thememonitor.ValueChanged += new EventHandler(OnSystemThemesChanged);
			}

			try { m_thememonitor.Start(); }
			catch(Exception) { /* DON'T CARE */ }
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">flag if managed resources should be disposed</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(m_appthemechanged != null) ApplicationTheme.Changed -= m_appthemechanged;
				if(m_thememonitor != null) m_thememonitor.Dispose();
				if(components != null) components.Dispose();
			}

			base.Dispose(disposing);
		}

		//---------------------------------------------------------------------
		// Event Handlers
		//---------------------------------------------------------------------

		/// <summary>
		/// Invoked when the application theme has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnApplicationThemeChanged(object sender, EventArgs args)
		{
			// This comes in from another thread, Invoke() is required
			Invoke((MethodInvoker)(() => { UpdateTheme(); }));
		}

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

		/// <summary>
		/// Invoked when the system theme(s) have changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnSystemThemesChanged(object sender, EventArgs args)
		{
			// Inform the ApplicationTheme class that the registry changed
			ApplicationTheme.SystemThemesChanged(this, EventArgs.Empty);
		}

		//---------------------------------------------------------------------
		// Private Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Updates the theme
		/// </summary>
		private void UpdateTheme()
		{
			BackColor = ApplicationTheme.FormBackColor;
			ForeColor = ApplicationTheme.FormForeColor;

			foreach(ToolStripMenuItem item in m_menu.Items)
			{
				item.ForeColor = ApplicationTheme.MenuForeColor;
				item.DropDown.BackColor = ApplicationTheme.MenuBackColor;
				item.DropDown.ForeColor = ApplicationTheme.MenuForeColor;
			}
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Event handler for application theme changes
		/// </summary>
		private readonly EventHandler m_appthemechanged;

		/// <summary>
		/// DPI scaling factor to be applied across the application
		/// </summary>
		private readonly SizeF m_scalefactor = SizeF.Empty;

		/// <summary>
		/// Registry change monitor for theme changes
		/// </summary>
		private readonly RegistryKeyValueChangeMonitor m_thememonitor;
	}
}
