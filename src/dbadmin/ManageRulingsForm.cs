//---------------------------------------------------------------------------
// Copyright (c) 2004-2024 Michael G. Brehm
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
using System.Text;

using zuki.ronin.data;
using zuki.ronin.ui;

namespace zuki.ronin
{
	/// <summary>
	/// Implements the Manage Ruling form
	/// </summary>
	public partial class ManageRulingsForm : FormBase
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		private ManageRulingsForm()
		{
			InitializeComponent();

			// Manual DPI scaling
			m_cardselector.Padding = m_cardselector.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_rulingsview.Padding = m_rulingsview.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);

			// Reset the theme based on the current system settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Instance constructor
		/// </summary>
		/// <param name="database">Database instance to use</param>
		public ManageRulingsForm(Database database) : this()
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
			m_rulings.BackColor = ApplicationTheme.PanelBackColor;
			m_rulings.ForeColor = ApplicationTheme.PanelForeColor;
		}

		/// <summary>
		/// Invoked when the form has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event argumenets</param>
		private void OnLoad(object sender, EventArgs args)
		{
			List<Card> cards = new List<Card>();
			m_database.EnumerateCardsWithRulings(card => { cards.Add(card); });
			m_cardselector.SetCards(cards);
		}

		/// <summary>
		/// Invoked when the selected card changes
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="card">Card that has been selected</param>
		private void OnSelectionChanged(object sender, Card card)
		{
			m_update.Tag = card;

			if(card != null)
			{
				List<Ruling> rulings = card.GetRulings();
				m_rulingsview.SetCard(card);

				StringBuilder sb = new StringBuilder();
				foreach(Ruling ruling in rulings)
				{
					sb.Append(ruling.Text);
					sb.Append("\r\n\r\n");
				}

				m_rulings.Text = sb.ToString().TrimEnd(new char[] { '\r', '\n' });

				m_update.Enabled = true;
				m_rulings.Enabled = true;
			}

			else
			{
				m_rulingsview.SetCard(null);
				m_rulings.Text = string.Empty;
				m_update.Enabled = false;
				m_rulings.Enabled = false;
			}
		}

		/// <summary>
		/// Invoked when the Update button has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnUpdate(object sender, EventArgs args)
		{
			if(m_update.Tag == null) return;

			string all = m_rulings.Text.Replace("\r\n\r\n", "|");
			string[] rulings = all.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
			((Card)m_update.Tag).UpdateRulings(rulings);

			OnSelectionChanged(this, (Card)m_update.Tag);
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Underlying Database instance
		/// </summary>
		readonly Database m_database;
	}
}
