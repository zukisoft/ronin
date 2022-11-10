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
using System.Drawing.Imaging;
using System.Drawing.Text;

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

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, 
					new Rectangle(layout.AttributePosition, layout.AttributeSize));
				return;
			}

			switch(attribute)
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
		/// Draws the copyright string onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		public static void DrawCopyright(Graphics graphics, Layout layout, RenderFlags flags)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.CopyrightBounds);
				return;
			}

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// The copyright is drawn right-aligned and vertically centered in the bounds
			StringFormat format = StringFormat.GenericTypographic;
			format.Alignment = StringAlignment.Far;
			format.LineAlignment = StringAlignment.Center;

			// The copyright is always drawn in black text
			graphics.DrawString(layout.Copyright, layout.CopyrightFont, Brushes.Black, 
				layout.CopyrightBounds, format);
		}

		/// <summary>
		/// Draws the Eye of Anubis hologram onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the artwork</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="artwork">Artwork to be drawn</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void DrawHologram(Graphics graphics, Layout layout, RenderFlags flags)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen,
					new Rectangle(layout.HologramPosition, layout.HologramSize));
				return;
			}

			Bitmap bitmap = layout.Hologram;
			Debug.Assert(bitmap.Size == layout.HologramSize);
			graphics.DrawImageUnscaled(bitmap, layout.HologramPosition);
		}

		/// <summary>
		/// Draws the card name onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="name">Card name to be drawn</param>
		/// <param name="brush">Brush to use when drawing the name</param>
		public static void DrawName(Graphics graphics, Layout layout, RenderFlags flags, string name,
			Brush brush)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.NameBounds);
				return;
			}

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// The name is drawn left-aligned and vertically centered in the bounds
			StringFormat format = StringFormat.GenericTypographic;
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			// If the name will fit in the bounding rectangle, draw it directly
			SizeF size = graphics.MeasureString(name, layout.NameFont, int.MaxValue, format);
			if(size.Width <= layout.NameBounds.Width)
			{
				graphics.DrawString(name, layout.NameFont, brush, layout.NameBounds, format);
				return;
			}

			// The card name is too wide for the boundaries, it needs to be drawn onto a 
			// transparent bitmap and then compressed horizontally
			using(Bitmap bmp = new Bitmap((int)Math.Ceiling(size.Width), (int)Math.Ceiling(layout.NameBounds.Height), 
				PixelFormat.Format32bppArgb))
			{
				bmp.SetResolution(96.0F, 96.0F);
				using(Graphics gr = Graphics.FromImage(bmp))
				{
					gr.SmoothingMode = SmoothingMode.AntiAlias;
					gr.TextRenderingHint = TextRenderingHint.AntiAlias;
					gr.DrawString(name, layout.NameFont, brush, new RectangleF(0, 0, bmp.Width, bmp.Height), format);
				}

				// Use HighQualityBicubic interpolation and scale the bitmap onto the background
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.DrawImage(bmp, layout.NameBounds);
			}
		}

		/// <summary>
		/// Draws the passcode string onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="passcode">Passcode string</param>
		public static void DrawPasscode(Graphics graphics, Layout layout, RenderFlags flags, string passcode)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.PasscodeBounds);
				return;
			}

			// OverlayProof has a transparent background, use AntiAlias
			if(flags == RenderFlags.OverlayProof) graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			else graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;

			// The passcode is drawn left-aligned and vertically centered in the bounds
			StringFormat format = StringFormat.GenericTypographic;
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			// The Passcode is always drawn in black text
			graphics.DrawString(passcode, layout.PasscodeFont, Brushes.Black,
				layout.PasscodeBounds, format);
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
