﻿//---------------------------------------------------------------------------
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
using System.Windows.Forms;

using zuki.ronin.data;
using zuki.ronin.ui;

namespace zuki.ronin
{
	/// <summary>
	/// Implements the card text editor dialog box
	/// </summary>
	internal partial class CardTextEditorDialog : FormBase
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		private CardTextEditorDialog()
		{
			InitializeComponent();

			// Reset the theme based on the current system settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Instance Constructor
		/// </summary>
		/// <param name="cardid">Card to be edited</param>
		public CardTextEditorDialog(Card card) : this()
		{
			m_card = card ?? throw new ArgumentNullException(nameof(card));
		}

		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------

		/// <summary>
		/// Gets the updated card text
		/// </summary>
		public string CardText { get => m_text.Text; }

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

			m_insertdot.ActiveLinkColor = ApplicationTheme.LinkColor;
			m_insertdot.LinkColor = ApplicationTheme.LinkColor;
			m_insertdot.DisabledLinkColor = ApplicationTheme.DisabledForeColor;
			m_text.BackColor = ApplicationTheme.PanelBackColor;
			m_text.ForeColor = ApplicationTheme.PanelForeColor;
		}

		/// <summary>
		/// Invoked when the "Insert ●" link has been clicked
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">LinkLabelLinkClicked event arguments</param>
		private void OnInsertDot(object sender, LinkLabelLinkClickedEventArgs args)
		{
			m_text.Text = m_text.Text.Insert(m_text.SelectionStart, "● ");
		}

		/// <summary>
		/// Invoked when the form has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnLoad(object sender, EventArgs args)
		{
			m_text.Text = m_card.Text;
			m_image.SetCard(m_card, m_text.Text);
			m_text.Focus();
		}

		/// <summary>
		/// Invoked when the text field has been validated
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnTextValidated(object sender, EventArgs args)
		{
			m_image.SetCard(m_card, m_text.Text);
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Card to be edited
		/// </summary>
		private Card m_card;
    }
}
