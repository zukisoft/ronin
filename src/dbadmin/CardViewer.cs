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
using zuki.ronin.renderer;
using zuki.ronin.ui;

namespace zuki.ronin
{
	/// <summary>
	/// Implements the card viewer form
	/// </summary>
	public partial class CardViewer : Form
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		private CardViewer()
		{
			InitializeComponent();

			// Wire up the application theme change handler
			m_appthemechanged = new EventHandler(OnApplicationThemeChanged);
			ApplicationTheme.Changed += m_appthemechanged;

			// Reset the theme based on the current system settings
			OnApplicationThemeChanged(this, EventArgs.Empty);

			// Manual DPI scaling
			Padding = Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_splitcontainer.Panel1.Padding = m_splitcontainer.Panel1.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_splitcontainer.Panel1.Margin = m_splitcontainer.Panel1.Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_splitcontainer.Panel2.Padding = m_splitcontainer.Panel2.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_splitcontainer.Panel2.Margin = m_splitcontainer.Panel2.Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
		}

		/// <summary>
		/// Instance constructor
		/// </summary>
		/// <param name="database">Database instance to use</param>
		public CardViewer(Database database) : this()
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
			// NOTE: This is not working for MDI child forms
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
			// TODO: TESTING
			m_cardselector.Cards = m_database.SelectCards(null);
		}

		/// <summary>
		/// Invoked when the selected card has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="card">Selected Card instance</param>
		private void OnSelectionChanged(object sender, Card card)
		{
			// TODO: TESTING
			Bitmap b = (card != null) ? Renderer.RenderCard(card, RenderFlags.None) : null;
			Image old = pictureBox1.Image;
			pictureBox1.Image = b ?? null;
			if(old != null) old.Dispose();
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
