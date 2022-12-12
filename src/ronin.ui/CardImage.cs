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

using zuki.ronin.data;
using zuki.ronin.renderer;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Implements a card image picture box
	/// </summary>
	public partial class CardImage : UserControlBase
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public CardImage()
		{
			InitializeComponent();

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		//---------------------------------------------------------------------
		// Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Sets the image from a Card instance
		/// </summary>
		/// <param name="card">Card for which to render and display the image</param>
		public void SetCard(Card card)
		{
			SetImage((card != null) ? Renderer.RenderCard(card) : null);
		}

		/// <summary>
		/// Sets the image from a Card instance with alternate text
		/// </summary>
		/// <param name="card">Card for which to render and display the image</param>
		/// <param name="text">Alternate text to render for the card</param>
		public void SetCard(Card card, string text)
		{
			SetImage((card != null) ? Renderer.RenderCard(card, text) : null);
		}

		/// <summary>
		/// Sets the image from a Print instance
		/// </summary>
		/// <param name="print">Print for which to render and display the image</param>
		public void SetPrint(Print print)
		{
			SetImage((print != null) ? Renderer.RenderPrint(print) : null);
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

			m_image.BackColor = ApplicationTheme.PanelBackColor;
		}

		//---------------------------------------------------------------------
		// Private Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Replaces the displayed card image
		/// </summary>
		/// <param name="image">Image to display</param>
		private void SetImage(Bitmap image)
		{
			Image old = m_image.Image;
			m_image.Image = image ?? null;
			old?.Dispose();
		}
	}
}
