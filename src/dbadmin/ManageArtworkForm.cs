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
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using zuki.ronin.data;
using zuki.ronin.ui;

namespace zuki.ronin
{
	/// <summary>
	/// Implements the artwork manager form
	/// </summary>
	public partial class ManageArtworkForm : FormBase
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		private ManageArtworkForm()
		{
			InitializeComponent();

			// Reset the theme based on the current system settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Instance constructor
		/// </summary>
		/// <param name="database">Database instance to use</param>
		public ManageArtworkForm(Database database) : this()
		{
			m_database = database ?? throw new ArgumentNullException(nameof(database));
		}

		//---------------------------------------------------------------------
		// Event Handlers
		//---------------------------------------------------------------------

		/// <summary>
		/// Invoked when the application theme has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		protected override void OnApplicationThemeChanged(object sender, EventArgs args)
		{
			base.OnApplicationThemeChanged(sender, args);

			m_separator.BackColor = ApplicationTheme.InvertedPanelBackColor;
		}

		/// <summary>
		/// Invoked when the artwork for a card has been changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="artwork">Artwork that has changed</param>
		private void OnArtworkChanged(object sender, Artwork artwork)
		{
			if(m_selected == null) return;

			// Refresh the information for the Card associated with this Artwork
			m_selected.Refresh();

			// Refresh the artwork for selected card
			OnSelectionChanged(this, m_selected);
		}

		/// <summary>
		/// Invoked when the "Import..." button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnImport(object sender, EventArgs args)
		{
			if(m_selected == null) return;

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
					// Convert the raw data into a Bitmap object to get the dimensions
					using(Bitmap bmp = new Bitmap(memstream))
					{
						m_selected.AddArtwork(extension, bmp.Width, bmp.Height, filedata);
					}
				}

				// Refresh the selected card information
				m_selected.Refresh();
				OnSelectionChanged(this, m_selected);
			}
			catch(Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Unable to import new artwork");
			}
		}

		/// <summary>
		/// Invoked when the form has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnLoad(object sender, EventArgs args)
		{
			List<Card> cards = new List<Card>();
			m_database.EnumerateCards(card => { cards.Add(card); });
			m_cardselector.SetCards(cards);
		}

		/// <summary>
		/// Invoked when the selected card has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="card">Selected Card instance</param>
		private void OnSelectionChanged(object sender, Card card)
		{
			m_selected = card;
			m_import.Enabled = m_selected != null;
			m_layoutpanel.Controls.Clear();

			if(card != null)
			{
				List<Artwork> artworks = card.GetArtwork();

				if(artworks.Count == 1)
				{
					m_layoutpanel.RowCount = 1;
					m_layoutpanel.ColumnCount = 1;
				}

				else if(artworks.Count == 2)
				{
					m_layoutpanel.RowCount = 1;
					m_layoutpanel.ColumnCount = 2;
				}

				else if(artworks.Count <= 4)
				{
					m_layoutpanel.RowCount = 2;
					m_layoutpanel.ColumnCount = 2;
				}

				else
				{
					m_layoutpanel.RowCount = 4;
					m_layoutpanel.ColumnCount = 4;
				}

				m_layoutpanel.RowStyles.Clear();
				for(int index = 0; index < m_layoutpanel.RowCount; index++)
				{
					m_layoutpanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0F / m_layoutpanel.RowCount));
				}

				m_layoutpanel.ColumnStyles.Clear();
				for(int index = 0; index < m_layoutpanel.ColumnCount; index++)
				{
					m_layoutpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0F / m_layoutpanel.ColumnCount));
				}

				foreach(Artwork artwork in artworks)
				{
					ArtworkTileControl tile = new ArtworkTileControl();
					m_layoutpanel.Controls.Add(tile);
					tile.ArtworkChanged += new EventHandler<Artwork>(OnArtworkChanged);
					tile.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
					tile.SetArtwork(artwork);
				}
			}
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Database instance
		/// </summary>
		private readonly Database m_database;

		/// <summary>
		/// Currently selected Card instance
		/// </summary>
		private Card m_selected;
	}
}
