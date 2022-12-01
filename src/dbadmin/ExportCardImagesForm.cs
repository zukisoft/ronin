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
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

using zuki.ronin.data;
using zuki.ronin.renderer;
using zuki.ronin.ui;

namespace zuki.ronin
{
	public partial class ExportCardImagesForm : Form
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		private ExportCardImagesForm()
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
		public ExportCardImagesForm(Database database) : this()
		{
			m_database = database ?? throw new ArgumentNullException(nameof(database));
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

			m_folder.BackColor = ApplicationTheme.PanelBackColor;
			m_folder.ForeColor = ApplicationTheme.PanelForeColor;
		}

		/// <summary>
		/// Invoked when the "..." button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnBrowse(object sender, EventArgs args)
		{
			if(m_folderbrowser.ShowDialog(this) == DialogResult.OK)
				m_folder.Text = m_folderbrowser.SelectedPath;
		}

		/// <summary>
		/// Invoked when the "Close" button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnClose(object sender, EventArgs args)
		{
			Close();
		}

		/// <summary>
		/// Invoked when the "Render" button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnRender(object sender, EventArgs args)
		{
			Exception exception = null;

			// Action<> to perform as the background task
			void export()
			{
				// Iterate over all of the cards in the database
				m_database.EnumerateCards(card =>
				{
					using(Bitmap bmp = Renderer.RenderCard(card))
					{
						// "Dark Magician.png"
						string name = card.Name;
						foreach(char ch in Path.GetInvalidFileNameChars())
						{
							name = name.Replace(ch, '_');
						}
						string filename = Path.Combine(m_folder.Text, name + ".png");
						bmp.Save(filename, ImageFormat.Png);
					}
				});
			}

			// Use a background task dialog to execute the operation
			using(BackgroundTaskDialog dialog = new BackgroundTaskDialog("Rendering Cards", export))
			{
				dialog.ShowDialog(ParentForm);
			}

			// Throw up a message box with any exception that occurred
			if(exception != null)
			{
				// TODO: A common exception dialog is still something this needs
				MessageBox.Show(this, exception.Message, "Unable to render cards", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Invoked when the folder name changes
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnFolderChanged(object sender, EventArgs args)
		{
			m_render.Enabled = Directory.Exists(m_folder.Text);
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
