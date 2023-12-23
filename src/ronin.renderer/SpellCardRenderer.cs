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
using System.Diagnostics;
using System.Drawing;

using zuki.ronin.data;
using zuki.ronin.renderer.Properties;

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Implements the spell card rendering engine
	/// </summary>
	internal class SpellCardRenderer
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public SpellCardRenderer()
		{
		}

		/// <summary>
		/// Renders a spell card
		/// </summary>
		/// <param name="spellcard">SpellCard to be rendered</param>
		public Bitmap RenderCard(SpellCard spellcard)
		{
			return RenderCard(spellcard, null);
		}

		/// <summary>
		/// Renders a spell card
		/// </summary>
		/// <param name="spellcard">SpellCard to be rendered</param>
		/// <param name="alttext">Alternate text to be rendered</param>
		public Bitmap RenderCard(SpellCard spellcard, string alttext)
		{
			Bitmap bitmap = RenderBackground();
			using(Graphics graphics = Graphics.FromImage(bitmap))
			{
				// Render the common elements
				RenderCommon(graphics, spellcard, alttext);

				// Draw the card name as if it were a Common print
				Engine.DrawName(graphics, s_layout, spellcard.Name, NameBrush.FromRarity(CardType.Spell, PrintRarity.Common));

				// Render the default artwork
				using(Artwork artwork = spellcard.GetDefaultArtwork())
				{
					Engine.DrawArtwork(graphics, s_layout, (artwork != null) ? artwork.ToBitmap() : Resources.defaultartwork);
				}
			}

			return bitmap;
		}

		/// <summary>
		/// Renders a specific print of a spell card
		/// </summary>
		/// <param name="spellcard">SpellCard to be rendered</param>
		/// <param name="print">Print information about the SpellCard</param>
		public Bitmap RenderPrint(SpellCard spellcard, Print print)
		{
			Bitmap bitmap = RenderBackground();
			using(Graphics graphics = Graphics.FromImage(bitmap))
			{
				// Render the common elements
				RenderCommon(graphics, spellcard);

				// Draw the card name based on the print rarity
				Engine.DrawName(graphics, s_layout, spellcard.Name, NameBrush.FromRarity(CardType.Spell, print.Rarity));


				// Render the print artwork
				using(Artwork artwork = print.GetArtwork())
				{
					Engine.DrawArtwork(graphics, s_layout, (artwork != null) ? artwork.ToBitmap() : Resources.defaultartwork);
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
			Bitmap background = Engine.RenderBackground(s_layout, Background.Spell);
			if(background == null) throw new Exception("Failed to render background image for card");
			Debug.Assert(background.Size == s_layout.BackgroundSize);

			return background;
		}

		/// <summary>
		/// Renders the common elements between a Card render and a Print render
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="spellcard">SpellCard instance</param>
		private void RenderCommon(Graphics graphics, SpellCard spellcard)
		{
			RenderCommon(graphics, spellcard, null);
		}

		/// <summary>
		/// Renders the common elements between a Card render and a Print render
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="spellcard">SpellCard instance</param>
		/// <param name="alttext">Alternate text to be rendered</param>
		private void RenderCommon(Graphics graphics, SpellCard spellcard, string alttext)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(spellcard));
			if(spellcard == null) throw new ArgumentNullException(nameof(spellcard));

			// Attribute
			Engine.DrawAttribute(graphics, s_layout, CardAttribute.Spell);

			// Header / Icon
			bool hasicon = spellcard.Icon != CardIcon.None;
			Engine.DrawHeader(graphics, s_layout, "Spell Card", hasicon);
			if(hasicon) Engine.DrawIcon(graphics, s_layout, spellcard.Icon);

			// Text
			Engine.DrawSpellTrapText(graphics, s_layout, alttext ?? spellcard.Text);

			// Passcode
			Engine.DrawPasscode(graphics, s_layout, spellcard.Passcode);

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
