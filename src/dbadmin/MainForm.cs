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

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

using zuki.ronin.data;
using zuki.ronin.ui;
using zuki.ronin.util;

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
				DWMA_USE_IMMERSIVE_DARK_MODE = 20,
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
			public static extern long DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref uint pvAttribute, uint cbAttribute);
		}
		#endregion

		/// <summary>
		/// Instance Constructor
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			// Precalculate a DPI-based scaling factor to be applied as necessary
			using(Graphics graphics = CreateGraphics())
				ApplicationTheme.SetScalingFactor(new SizeF(graphics.DpiX / 96.0F, graphics.DpiY / 96.0F));

			// Wire up the application theme change handler
			m_appthemechanged = new EventHandler(OnApplicationThemeChanged);
			ApplicationTheme.Changed += m_appthemechanged;

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);

			// Set the custom professional renderer for the StatusStrip
			m_statusstrip.Renderer = new ToolStripProfessionalRenderer(ApplicationTheme.ProfessionalColorTable);

			// Enable rounded corners if supported by the OS
			this.EnableRoundedCorners(true);

			// Manual DPI scaling
			Padding = Padding.ScaleDPI(ApplicationTheme.ScalingFactor);

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
		/// Clean up any resources being used
		/// </summary>
		/// <param name="disposing">flag if managed resources should be disposed</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(m_appthemechanged != null) ApplicationTheme.Changed -= m_appthemechanged;
				if(m_thememonitor != null) m_thememonitor.Dispose();
				if(m_database != null) m_database.Dispose();
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
			this.EnableImmersiveDarkMode(ApplicationTheme.DarkMode);
			BackColor = ApplicationTheme.FormBackColor;
			ForeColor = ApplicationTheme.FormForeColor;
		}

		/// <summary>
		/// Invoked when the form has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnLoad(object sender, EventArgs args)
		{
			if(File.Exists("d:\\ronin.db")) m_opendatabase.FileName = "D:\\ronin.db";
			else
			{
				// TODO: Can OpenFileDialog support dark mode?
				var result = m_opendatabase.ShowDialog(this);
				if(result != DialogResult.OK) Close();
			}

			try
			{
				string canonicalizedpath = Path.GetFullPath(m_opendatabase.FileName);
				m_database = Database.Create(canonicalizedpath);
				m_statuslabel.Text = canonicalizedpath;
			}
			catch(Exception) { /* TODO - HANDLER */ }

			// TODO: TESTING
			if(m_database != null)
			{
				var child = new CardViewer(m_database);
				child.MdiParent = this;
				child.Show();
			}
		}

		/// <summary>
		/// Invoked when the system theme(s) have changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnSystemThemesChanged(object sender, EventArgs args)
		{
			// This comes in from another thread, Invoke() is required
			Invoke((MethodInvoker)(() =>
			{
				// This application will always just follow the system, there
				// will not be any light/dark mode setting to access
				ApplicationTheme.SetTheme(Theme.System);
			}));
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Event handler for application theme changes
		/// </summary>
		private readonly EventHandler m_appthemechanged;

		/// <summary>
		/// Event handler for system theme changes
		/// </summary>
		private readonly RegistryKeyValueChangeMonitor m_thememonitor;

		/// <summary>
		/// Database instance
		/// </summary>
		Database m_database = null;
	}
}
