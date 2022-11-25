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
using System.ComponentModel;
using System.Windows.Forms;
using zuki.ronin.ui;

namespace zuki.ronin
{
	/// <summary>
	/// Dialog box that executes a background task and closes itself when
	/// the task has completed.  Uses a marquee progress bar to indicate
	/// that the task is running
	/// </summary>
	public partial class BackgroundTaskDialog : Form
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		private BackgroundTaskDialog()
		{
			InitializeComponent();

			// Wire up the application theme change handler
			m_appthemechanged = new EventHandler(OnApplicationThemeChanged);
			ApplicationTheme.Changed += m_appthemechanged;

			// Reset the theme based on the current system settings
			OnApplicationThemeChanged(this, EventArgs.Empty);

			// Manual DPI scaling
			Padding = Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_panel.Padding = m_panel.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_panel.Margin = m_panel.Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
		}

		/// <summary>
		/// Instance Constructor
		/// </summary>
		/// <param name="banner">Banner text to be displayed</param>
		/// <param name="task">Background task to be executed</param>
		public BackgroundTaskDialog(string banner, Action task) : this()
		{
			if(banner == null) banner = string.Empty;

			m_banner.Text = banner;
			m_task = task ?? throw new ArgumentNullException("task");
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
				components?.Dispose();
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
			m_panel.BackColor = ApplicationTheme.PanelBackColor;
			m_panel.ForeColor = ApplicationTheme.PanelForeColor;
		}

		/// <summary>
		/// Invoked when the form has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnLoad(object sender, EventArgs args)
		{
			// Enable the one-shot timer to kick off the task after
			// the form has been fully initialized
			m_oneshot.Enabled = true;
		}

		/// <summary>
		/// Invoked when the one shot timer has been fired
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnOneShot(object sender, EventArgs args)
		{
			// Disable the timer
			m_oneshot.Enabled = false;

			// Initialize the background worker with the work and completion callbacks
			m_worker.DoWork += OnDoWork;
			m_worker.RunWorkerCompleted += OnWorkerCompleted;
			m_worker.RunWorkerAsync();
		}

		/// <summary>
		/// Invoked by the background worker to do whatever needs to be done
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Worker event arguments</param>
		private void OnDoWork(object sender, DoWorkEventArgs args)
		{
			// Invoke the Action<> specified in the constructor inside a try/catch --
			// if the operation fails the Action<> itself should deal with that
			try { m_task(); }
			catch { /* DO NOTHING */ }
		}

		/// <summary>
		/// Invoked by the background worker when the work has completed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Worker event arguments</param>
		private void OnWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
		{
			// The work has been completed, close this dialog box
			Close();
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Event handler for application theme changes
		/// </summary>
		private readonly EventHandler m_appthemechanged;

		/// <summary>
		/// The task to be executed by the background worker instance
		/// </summary>
		private readonly Action m_task;
	}
}
