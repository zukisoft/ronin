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
using System.Diagnostics;
using System.Drawing;

using zuki.ronin.data;
using zuki.ronin.renderer.Properties;

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Implements the trap card rendering engine
	/// </summary>
	internal class TrapCardRenderer
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public TrapCardRenderer()
		{
		}

		/// <summary>
		/// Renders a trap card
		/// </summary>
		/// <param name="trapcard">TrapCard to be rendered</param>
		public Bitmap RenderCard(TrapCard trapcard)
		{
			return RenderCard(trapcard, null);
		}

		/// <summary>
		/// Renders a trap card
		/// </summary>
		/// <param name="trapcard">TrapCard to be rendered</param>
		/// <param name="alttext">Alternate text to be rendered</param>
		public Bitmap RenderCard(TrapCard trapcard, string alttext)
		{
			Bitmap bitmap = RenderBackground();
			using(Graphics graphics = Graphics.FromImage(bitmap))
			{
				// Render the common elements
				RenderCommon(graphics, trapcard, alttext);

				// Render the default artwork
				using(Artwork artwork = trapcard.GetDefaultArtwork())
				{
					Engine.DrawArtwork(graphics, s_layout, artwork.Image ?? Resources.defaultartwork);
				}

				// Draw the card name in solid white
				Engine.DrawName(graphics, s_layout, trapcard.Name, NameBrush.SolidWhite);
			}

			return bitmap;
		}

		/// <summary>
		/// Renders a specific print of a trap card
		/// </summary>
		/// <param name="trapcard">TrapCard to be rendered</param>
		/// <param name="print">Print information about the TrapCard</param>
		public Bitmap RenderPrint(TrapCard trapcard, Print print)
		{
			Bitmap bitmap = RenderBackground();
			using(Graphics graphics = Graphics.FromImage(bitmap))
			{
				// Render the common elements
				RenderCommon(graphics, trapcard);

				// Draw the card name in solid white
				Engine.DrawName(graphics, s_layout, trapcard.Name, NameBrush.SolidWhite);

				// Render the print artwork
				using(Artwork artwork = print.GetArtwork())
				{
					Engine.DrawArtwork(graphics, s_layout, artwork.Image ?? Resources.defaultartwork);
				}

				// Draw the set code for the print
				Engine.DrawSetCode(graphics, s_layout, print.ToString());
			}

			return bitmap;
		}

		//-------------------------------------------------------------------------
		// Private Member Functions

		/// <summary>
		/// Renders the background image for the spell card
		/// </summary>
		private Bitmap RenderBackground()
		{
			// Generate the background image for the spell card
			Bitmap background = Engine.RenderBackground(s_layout, Background.Trap);
			if(background == null) throw new Exception("Failed to render background image for card");
			Debug.Assert(background.Size == s_layout.BackgroundSize);

			return background;
		}

		/// <summary>
		/// Renders the common elements between a Card render and a Print render
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="trapcard">TrapCard instance</param>
		private void RenderCommon(Graphics graphics, TrapCard trapcard)
		{
			RenderCommon(graphics, trapcard, null);
		}

		/// <summary>
		/// Renders the common elements between a Card render and a Print render
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="trapcard">TrapCard instance</param>
		/// <param name="alttext">Alternate text to be rendered</param>
		private void RenderCommon(Graphics graphics, TrapCard trapcard, string alttext)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(trapcard == null) throw new ArgumentNullException(nameof(trapcard));

			// Attribute
			Engine.DrawAttribute(graphics, s_layout, CardAttribute.Trap);

			// Header / Icon
			bool hasicon = trapcard.Icon != CardIcon.None;
			Engine.DrawHeader(graphics, s_layout, "Trap Card", hasicon);
			if(hasicon) Engine.DrawIcon(graphics, s_layout, trapcard.Icon);

			// Text
			Engine.DrawSpellTrapText(graphics, s_layout, alttext ?? trapcard.Text);

			// Passcode
			Engine.DrawPasscode(graphics, s_layout, trapcard.Passcode);

			// Copyright
			Engine.DrawCopyright(graphics, s_layout);

			// Hologram
			Engine.DrawHologram(graphics, s_layout);
		}

		//-------------------------------------------------------------------------
		// Member Variables

		/// <summary>
		/// Only the 'medium' renderer is currently supported
		/// </summary>
		private static readonly Layout s_layout = new LayoutMedium();
	}
}
