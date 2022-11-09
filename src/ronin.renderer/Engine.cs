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
using System.Drawing.Drawing2D;

using zuki.ronin.data;

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Implements the card rendering engine functions
	/// </summary>
	internal static class Engine
	{
		/// <summary>
		/// Draws artwork onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the artwork</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="artwork">Artwork to be drawn</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void DrawArtwork(Graphics graphics, Layout layout, RenderFlags flags, Bitmap artwork)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(artwork == null) throw new ArgumentNullException(nameof(artwork));

			// Use HighQualityBicubic interpolation and draw the artwork
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.DrawImage(artwork, layout.ArtworkPosition.X, layout.ArtworkPosition.Y,
				layout.ArtworkSize.Width, layout.ArtworkSize.Height);
		}

		/// <summary>
		/// Draws an attribute onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the attribute</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="attribute">Attribute to be drawn</param>
		public static void DrawAttribute(Graphics graphics, Layout layout, RenderFlags flags, CardAttribute attribute)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			Bitmap bitmap = null;

			if(false)
			{
				//
				// TODO: Fill with color for proof
				//
			}

			else switch(attribute)
			{
				case CardAttribute.Dark:
					bitmap = layout.AttributeDark;
					break;

				case CardAttribute.Earth:
					bitmap = layout.AttributeEarth;
					break;

				case CardAttribute.Fire:
					bitmap = layout.AttributeFire;
					break;

				case CardAttribute.Light:
					bitmap = layout.AttributeLight;
					break;

				case CardAttribute.Spell:
					bitmap = layout.AttributeSpell;
					break;

				case CardAttribute.Trap:
					bitmap = layout.AttributeTrap;
					break;

				case CardAttribute.Wind:
					bitmap = layout.AttributeWind;
					break;

				case CardAttribute.Water:
					bitmap = layout.AttributeWater;
					break;

			}

			if(bitmap != null)
			{
				Debug.Assert(bitmap.Size == layout.AttributeSize);
				graphics.DrawImageUnscaled(bitmap, layout.AttributePosition);
			}
		}

		/// <summary>
		/// Renders the background image
		/// </summary>
		/// <param name="layout">Renderer Layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="background">Background to be rendered</param>
		public static Bitmap RenderBackground(Layout layout, RenderFlags flags, Background background)
		{
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			Bitmap bitmap;

			// Force a "none" background if the render flags are set to overlay proof mode
			if(flags == RenderFlags.OverlayProof) background = Background.None;

			// Load the appropriate background image from the assembly resources
			switch(background)
			{
				case Background.NormalMonster:
					bitmap = layout.BackgroundNormalMonster;
					break;

				case Background.EffectMonster:
					bitmap = layout.BackgroundEffectMonster;
					break;

				case Background.FusionMonster:
					bitmap = layout.BackgroundFusionMonster;
					break;

				case Background.RitualMonster:
					bitmap = layout.BackgroundRitualMonster;
					break;

				case Background.Spell:
					bitmap = layout.BackgroundSpell;
					break;

				case Background.Trap:
					bitmap = layout.BackgroundTrap;
					break;

				case Background.Token:
					bitmap = layout.BackgroundToken;
					break;

				// The default is to generate a transparent background image, this
				// can be used for overlay proofs to compare against real cards
				default:
					bitmap = layout.BackgroundTransparent;
					break;
			}

			return bitmap;
		}
	}
}
