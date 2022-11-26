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
using System.Windows.Forms;

using zuki.ronin.data;
using zuki.ronin.ui;

namespace zuki.ronin
{
	public partial class DatabaseVacuumForm : Form
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		private DatabaseVacuumForm()
		{
			InitializeComponent();

			// Wire up the application theme change handler
			m_appthemechanged = new EventHandler(OnApplicationThemeChanged);
			ApplicationTheme.Changed += m_appthemechanged;

			// Reset the theme based on the current system settings
			OnApplicationThemeChanged(this, EventArgs.Empty);

			// Manual DPI scaling
			Padding = Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
		}

		/// <summary>
		/// Instance Constructor
		/// </summary>
		/// <param name="original">Original artwork image</param>
		/// <param name="updated">Updated artwork image</param>
		public DatabaseVacuumForm(Database database) : this()
		{
			m_database = database ?? throw new ArgumentNullException(nameof(database));
			m_original.Text = m_database.GetSize().ToString() + " Bytes";
			m_current.Text = m_original.Text;
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
		}

		/// <summary>
		/// Invoked when the "Vacuum" button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnVacuum(object sender, EventArgs args)
		{
			Exception exception = null;

			// Action<> to perform as the background task
			void vacuum()
			{
				try { m_database.Vacuum(); }
				catch(Exception ex) { exception = ex; }
			}

			using(BackgroundTaskDialog dialog = new BackgroundTaskDialog("Vacuuming database", vacuum))
			{
				dialog.ShowDialog(this);
			}

			// Throw up a message box with any exception that occurred
			if(exception != null)
			{
				// TODO: A common exception dialog is still something this needs
				MessageBox.Show(this, exception.Message, "Unable to vacuum database", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			m_current.Text = m_database.GetSize().ToString() + " Bytes";
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Event handler for application theme changes
		/// </summary>
		private readonly EventHandler m_appthemechanged;

		/// <summary>
		/// Database instance
		/// </summary>
		private readonly Database m_database;
	}
}
