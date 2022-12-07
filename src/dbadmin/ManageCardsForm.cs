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
using System.Windows.Forms;

using zuki.ronin.data;
using zuki.ronin.ui;

namespace zuki.ronin
{
	/// <summary>
	/// Implements the card viewer form
	/// </summary>
	public partial class ManageCardsForm : Form
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		private ManageCardsForm()
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
		/// Instance constructor
		/// </summary>
		/// <param name="database">Database instance to use</param>
		public ManageCardsForm(Database database) : this()
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
			// NOTE: This is not working for MDI child forms
			this.EnableImmersiveDarkMode(ApplicationTheme.DarkMode);

			BackColor = ApplicationTheme.FormBackColor;
			ForeColor = ApplicationTheme.FormForeColor;
			m_separator.BackColor = ApplicationTheme.InvertedPanelBackColor;
		}

		/// <summary>
		/// Invoked when the "Edit Card Text..." button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnEditText(object sender, EventArgs args)
		{
			if(m_selected == null) return;

			using(CardTextEditorDialog dialog = new CardTextEditorDialog(m_selected))
			{
				if(dialog.ShowDialog(this) == DialogResult.OK)
				{
					m_selected.UpdateText(dialog.CardText);
					OnSelectionChanged(this, m_selected);
				}
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
			m_cardselector.Cards = cards;

			List<RestrictionList> reslists = new List<RestrictionList>();
			m_database.EnumerateRestrictionLists(reslist => { reslists.Add(reslist); });

			int x = 123;

		}

		/// <summary>
		/// Invoked when the next button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnNext(object sender, EventArgs args)
		{
			if(m_selected == null) return;

			Print print = m_prints[++m_printindex];
			if(print == null) m_image.SetCard(m_selected);
			else m_image.SetPrint(print);

			m_previous.Enabled = true;
			m_next.Enabled = (m_printindex + 1) < m_prints.Count;
		}

		/// <summary>
		/// Invoked when the previous button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnPrevious(object sender, EventArgs args)
		{
			if(m_selected == null) return;

			Print print = m_prints[--m_printindex];
			if(print == null) m_image.SetCard(m_selected);
			else m_image.SetPrint(print);

			m_previous.Enabled = m_printindex > 0;
			m_next.Enabled = m_prints.Count > 1;
		}

		/// <summary>
		/// Invoked when the selected card has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="card">Selected Card instance</param>
		private void OnSelectionChanged(object sender, Card card)
		{
			m_selected = card;
			m_edittext.Enabled = m_selected != null;

			m_prints.Clear();
			m_printindex = 0;

			if(card != null)
			{
				m_prints.Add(null);         // Generic image
				m_prints.AddRange(card.GetPrints());
			}

			m_previous.Enabled = false;
			m_next.Enabled = m_prints.Count > 1;

			m_image.SetCard(m_selected);
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

		/// <summary>
		/// Currently selected Card instance
		/// </summary>
		private Card m_selected;

		/// <summary>
		/// List<> of available prints for the selected Card
		/// </summary>
		private List<Print> m_prints = new List<Print>();

		/// <summary>
		/// Index of the currently displayed print
		/// </summary>
		private int m_printindex;
	}
}
