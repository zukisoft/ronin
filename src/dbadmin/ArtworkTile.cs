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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using zuki.ronin.data;
using zuki.ronin.ui;

namespace zuki.ronin
{
	public partial class ArtworkTile : UserControl
	{
		/// <summary>
		/// Instance constructor
		/// </summary>
		public ArtworkTile()
		{
			InitializeComponent();

			// Wire up the application theme change handler
			m_appthemechanged = new EventHandler(OnApplicationThemeChanged);
			ApplicationTheme.Changed += m_appthemechanged;

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);

			// Manual DPI scaling
			Margin = Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
			Padding = Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_lowerpanel.Margin = m_lowerpanel.Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_lowerpanel.Padding = m_lowerpanel.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
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

		//-------------------------------------------------------------------
		// Events
		//-------------------------------------------------------------------

		/// <summary>
		/// Fired when the artwork was changed
		/// </summary>
		[Browsable(true), Category("Behavior")]
		public event EventHandler<Artwork> ArtworkChanged;

		//---------------------------------------------------------------------
		// Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Sets the artwork instance
		/// </summary>
		/// <param name="artwork">Artwork instance to display</param>
		public void SetArtwork(Artwork artwork)
		{
			SetArtwork(artwork, false);
		}

		/// <summary>
		/// Sets the artwork instance
		/// </summary>
		/// <param name="artwork">Artwork instance to display</param>
		/// <param name="isdefault">Flag indicating if the artwork is the default</param>
		public void SetArtwork(Artwork artwork, bool isdefault)
		{
			m_artwork = artwork ?? throw new ArgumentNullException(nameof(artwork));

			// Hide the Set Default link if the artwork is already the default
			if(isdefault) m_setdefault.Visible = false;

			// Set the artwork image
			m_image.Image = artwork.Image;
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
			BackColor = ApplicationTheme.PanelBackColor;
			ForeColor = ApplicationTheme.PanelForeColor;
		}

		/// <summary>
		/// Invoked when the "Set Default" button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnSetDefault(object sender, EventArgs args)
		{
			m_artwork.SetDefault();
			ArtworkChanged?.Invoke(this, m_artwork);
		}

		/// <summary>
		/// Invoked when the "Update..." button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnUpdate(object sender, EventArgs args)
		{
			if(m_openfile.ShowDialog(this) != DialogResult.OK) return;
			string filename = m_openfile.FileName;
			m_openfile.FileName = string.Empty;

			try
			{
				string extension = Path.GetExtension(filename).ToLower().TrimStart(new char[] { '.' });

				// Only support JPG, PNG, and BMP
				if(!new List<string>(new string[] { "jpg", "png", "bmp" }).Contains(extension))
					throw new Exception("Unsupported file type. File must be JPG, PNG, or BMP.");

				// Read all of the raw data from the image file
				byte[] filedata = File.ReadAllBytes(filename);
				using(MemoryStream memstream = new MemoryStream(filedata))
				{
					// Convert the raw data into a Bitmap object to preview
					using(Bitmap bmp = new Bitmap(memstream))
					{
						using(UpdateArtworkDialog preview = new UpdateArtworkDialog(m_artwork.Image, bmp))
						{
							if(preview.ShowDialog(ParentForm) == DialogResult.OK)
							{
								m_artwork.UpdateImage(extension, bmp.Width, bmp.Height, filedata);
								ArtworkChanged?.Invoke(this, m_artwork);
							}
						}
					}
				}
			}
			catch(Exception ex)
			{
				// TODO: Make an error message dialog, go back and try/catch a bunch
				// of things that aren't protected yet
				MessageBox.Show(this, ex.Message, "Unable to update artwork");
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
		/// The displayed Artwork instance
		/// </summary>
		private Artwork m_artwork;
	}
}
