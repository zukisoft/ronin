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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using zuki.ronin.data;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Implements a card image picture box
	/// </summary>
	public partial class CardView : UserControlBase
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public CardView()
		{
			InitializeComponent();

			// Manual DPI scaling
			m_cardimage.Padding = m_cardimage.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_cardimage.Margin = m_cardimage.Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
			//cardInfo1.Padding = cardInfo1.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			//cardInfo1.Margin = cardInfo1.Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
			//printInfo1.Padding = printInfo1.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			//printInfo1.Margin = printInfo1.Margin.ScaleDPI(ApplicationTheme.ScalingFactor);

			tableLayoutPanel1.Padding = tableLayoutPanel1.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			tableLayoutPanel1.Margin = tableLayoutPanel1.Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
			//m_printlistview.Padding = m_printlistview.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		//---------------------------------------------------------------------
		// Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Sets the card instance to be viewed
		/// </summary>
		/// <param name="card">Card for which to render and display the image</param>
		public void SetCard(Card card)
		{
			Size size;

			m_cardimage.SetCard(card);

			cardName1.SetCard(card, out size);
			tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
			tableLayoutPanel1.RowStyles[0].Height = (tableLayoutPanel1.Margin.Horizontal * 2) + size.Height; //.ScaleDPI(ApplicationTheme.ScalingFactor);

			cardText1.SetCard(card);
			rulingsView1.SetCard(card);
			printListView1.SetPrints((card != null) ? card.GetPrints() : Enumerable.Empty<Print>());
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

			// TODO
			//m_image.BackColor = ApplicationTheme.FormBackColor;
		}
	}
}
