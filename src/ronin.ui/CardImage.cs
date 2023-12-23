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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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

			// The background has to be completely redrawn when resized
			ResizeRedraw = true;

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

		/// <summary>
		/// Invoked when the control has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnLoad(object sender, EventArgs args)
		{
			// Reposition/resize the PictureBox after the control has been loaded
			m_image.Location = new Point(12.ScaleDPI(ApplicationTheme.ScalingFactor), 12.ScaleDPI(ApplicationTheme.ScalingFactor));
			m_image.Size = new Size(Width - 2 * 12.ScaleDPI(ApplicationTheme.ScalingFactor), Height - 2 * 12.ScaleDPI(ApplicationTheme.ScalingFactor));
		}

		/// <summary>
		/// Invoked when the UserControl requires painting
		/// </summary>
		/// <param name="args">Paint event arguments</param>
		protected override void OnPaint(PaintEventArgs args)
		{
			base.OnPaint(args);

			// Fill with the background color first
			args.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

			// Render the item inside a GraphicsPath to round the corners
			using(GraphicsPath gp = new GraphicsPath())
			{
				Rectangle bounds = args.ClipRectangle.InflateDPI(-4, -4, ApplicationTheme.ScalingFactor);
				float CornerRadius = 8.ScaleDPI(ApplicationTheme.ScalingFactor) * 2.0F;
				gp.AddArc(bounds.Left - 1, bounds.Top - 1, CornerRadius, CornerRadius, 180, 90);
				gp.AddArc(bounds.Left + bounds.Width - CornerRadius, bounds.Top - 1, CornerRadius, CornerRadius, 270, 90);
				gp.AddArc(bounds.Left + bounds.Width - CornerRadius, bounds.Top + bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
				gp.AddArc(bounds.Left - 1, bounds.Top + bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
				args.Graphics.SetClip(gp);

				// Draw the background color
				args.Graphics.FillRectangle(new SolidBrush(ApplicationTheme.PanelBackColor), ClientRectangle);
				args.Graphics.ResetClip();
			}
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
