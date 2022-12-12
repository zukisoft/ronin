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
using System.Linq;

using zuki.ronin.data;

namespace zuki.ronin.ui
{
	public partial class CardSelector : UserControlBase
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public CardSelector()
		{
			InitializeComponent();

			// Manual DPI scaling
			m_toppanel.Margin = m_toppanel.Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_toppanel.Padding = m_toppanel.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		//-------------------------------------------------------------------
		// Events
		//-------------------------------------------------------------------

		/// <summary>
		/// Fired when the selected item has changed
		/// </summary>
		[Browsable(true), Category("Behavior")]
		public event EventHandler<Card> SelectionChanged;

		//-------------------------------------------------------------------
		// Properties
		//-------------------------------------------------------------------

		/// <summary>
		/// Gets/sets the enumerable list of Card objects to display
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<Card> Cards
		{
			get { return m_cards; }
			set
			{
				// Clear out and reload the existing list of cards
				m_cards.Clear();
				m_cards.AddRange(value);

				// Reset the filter text to trigger an update to the listview
				if(m_filter.Text != string.Empty) m_filter.Text = string.Empty;
				else OnFilterTextChanged(this, EventArgs.Empty);
			}
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

			m_toppanel.BackColor = ApplicationTheme.FormBackColor;
			m_toppanel.ForeColor = ApplicationTheme.FormForeColor;
			m_filter.BackColor = ApplicationTheme.PanelBackColor;
			m_filter.ForeColor = ApplicationTheme.PanelForeColor;
		}

		/// <summary>
		/// Invoked when the filter text has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnFilterTextChanged(object sender, EventArgs args)
		{
			// Update the listview to only contain the subset of Card objects with matching names to the filter
			m_cardlistview.Cards = m_cards.Where(item => item.Name.IndexOf(m_filter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		/// <summary>
		/// Invoked when the listview selection has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="card">Card object that was selected</param>
		private void OnSelectionChanged(object sender, Card card)
		{
			SelectionChanged?.Invoke(this, card);
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Backing List<> for the virtual list view
		/// </summary>
		private readonly List<Card> m_cards = new List<Card>();
	}
}
