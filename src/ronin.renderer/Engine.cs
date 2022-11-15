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
		/// Draws the attack and defense area of a monster card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the attribute</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="attack">Attack value</param>
		/// <param name="defense">Defense value</param>
		public static void DrawAttackDefense(Graphics graphics, Layout layout, RenderFlags flags, int attack, int defense)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.AttackDefenseBounds);
				return;
			}

			// Left-aligned string format (ATK/DEF labels)
			StringFormat leftformat = new StringFormat(StringFormat.GenericTypographic);
			leftformat.Alignment = StringAlignment.Near;
			leftformat.LineAlignment = StringAlignment.Center;

			// Right-aligned string format (ATK/DEF values)
			StringFormat rightformat = new StringFormat(StringFormat.GenericTypographic);
			rightformat.Alignment = StringAlignment.Far;
			rightformat.LineAlignment = StringAlignment.Center;

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// Draw the line across the text area
			graphics.DrawLine(new Pen(Brushes.Black, layout.LineWidth), layout.AttackDefenseBounds.Left, layout.AttackDefenseBounds.Top,
				layout.AttackDefenseBounds.Right, layout.AttackDefenseBounds.Top);

			// Copy the layout attack and defense boundaries so they can be modified
			RectangleF atkbounds = layout.AttackBounds;
			RectangleF defbounds = layout.DefenseBounds;

			// The position of the slash depends on the width of the ATK and DEF label strings
			SizeF atksize = graphics.MeasureString("ATK", layout.AttackDefenseFont, int.MaxValue, leftformat);
			SizeF defsize = graphics.MeasureString("DEF", layout.AttackDefenseFont, int.MaxValue, leftformat);

			// Draw the ATK strings
			graphics.DrawString("ATK", layout.AttackDefenseFont, Brushes.Black, atkbounds, leftformat);
			graphics.DrawString((attack < 0) ? "?" : attack.ToString(), layout.AttackDefenseFont, Brushes.Black, atkbounds, rightformat);
			atkbounds.Location = new PointF(atkbounds.Left + atksize.Width + layout.QuarterSpace, atkbounds.Top);
			graphics.DrawString("/", layout.AttackDefenseFont, Brushes.Black, atkbounds, leftformat);

			// Draw the DEF strings
			graphics.DrawString("DEF", layout.AttackDefenseFont, Brushes.Black, defbounds, leftformat);
			graphics.DrawString((defense < 0) ? "?" : defense.ToString(), layout.AttackDefenseFont, Brushes.Black, defbounds, rightformat);
			defbounds.Location = new PointF(defbounds.Left + defsize.Width + layout.QuarterSpace, defbounds.Top);
			graphics.DrawString("/", layout.AttackDefenseFont, Brushes.Black, defbounds, leftformat);
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
			StringFormat format = new StringFormat(StringFormat.GenericTypographic);
			format.Alignment = StringAlignment.Far;
			format.LineAlignment = StringAlignment.Center;

			// The copyright is always drawn in black text
			graphics.DrawString(layout.Copyright, layout.CopyrightFont, Brushes.Black, 
				layout.CopyrightBounds, format);
		}

		/// <summary>
		/// Draws the text for a fusion monster card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="text">Text to be drawn</param>
		public static void DrawFusionMonsterText(Graphics graphics, Layout layout, RenderFlags flags, string text)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(text == null) throw new ArgumentNullException(nameof(text));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.SpellTrapTextBounds);
				return;
			}
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
		/// Draws a spell/trap card header onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the header</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="header">Header string</param>
		/// <param name="hasicon">Flag to leave space for the icon</param>
		public static void DrawHeader(Graphics graphics, Layout layout, RenderFlags flags, string header,
			bool hasicon)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(header == null) throw new ArgumentNullException(nameof(header));

			// LayoutProof draws a colored rectange only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.HeaderBounds);
				return;
			}

			// The header is drawn right-aligned and vertically centered in the bounds
			StringFormat format = new StringFormat(StringFormat.GenericTypographic);
			format.Alignment = StringAlignment.Far;
			format.LineAlignment = StringAlignment.Center;
			format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			SizeF bracketsize = graphics.MeasureString("]", layout.HeaderFont, int.MaxValue, format);
			float halfspace = graphics.MeasureString(" ", layout.HeaderFont, int.MaxValue, format).Width / 2.0F;
			RectangleF bounds = layout.HeaderBounds;

			// Render the bracket character into a new bitmap
			using(Bitmap bracket = new Bitmap((int)Math.Ceiling(bracketsize.Width), (int)Math.Ceiling(bracketsize.Height),
				PixelFormat.Format32bppArgb))
			{
				bracket.SetResolution(96.0F, 96.0F);
				using(Graphics gr = Graphics.FromImage(bracket))
				{
					gr.SmoothingMode = SmoothingMode.AntiAlias;
					gr.TextRenderingHint = TextRenderingHint.AntiAlias;
					gr.DrawString("]", layout.HeaderFont, Brushes.Black, new RectangleF(0, 0, bracket.Width, bracket.Height), format);
				}

				int x = (int)bounds.Right - bracket.Width;
				int y = (int)bounds.Top + (int)((bounds.Height - bracket.Height) / 2.0F);
				y -= (int)layout.QuarterSpace;
				graphics.DrawImageUnscaled(bracket, new Point(x, y));
				bounds.Inflate(new SizeF(-(bracketsize.Width + halfspace), 0.0F));

				if(hasicon) bounds.Inflate(new SizeF(-layout.IconSize.Width, 0.0F));

				SizeF textsize = graphics.MeasureString(header, layout.HeaderFont, int.MaxValue, format);
				graphics.DrawString(header, layout.HeaderFont, Brushes.Black, bounds, format);
				bounds.Inflate(new SizeF(-(textsize.Width + halfspace), 0.0F));

				bracket.RotateFlip(RotateFlipType.RotateNoneFlipX);
				x = (int)bounds.Right - bracket.Width;
				graphics.DrawImageUnscaled(bracket, new Point(x, y));
			}
		}

		/// <summary>
		/// Draws an icon onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the icon</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="icon">Icon to be drawn</param>
		public static void DrawIcon(Graphics graphics, Layout layout, RenderFlags flags, CardIcon icon)
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

			switch(icon)
			{
				case CardIcon.Continuous:
					bitmap = layout.IconContinuous;
					break;

				case CardIcon.Counter:
					bitmap = layout.IconCounter;
					break;

				case CardIcon.Equip:
					bitmap = layout.IconEquip;
					break;

				case CardIcon.Field:
					bitmap = layout.IconField;
					break;

				case CardIcon.QuickPlay:
					bitmap = layout.IconQuickPlay;
					break;

				case CardIcon.Ritual:
					bitmap = layout.IconRitual;
					break;
			}

			if(bitmap != null)
			{
				Debug.Assert(bitmap.Size == layout.IconSize);
				graphics.DrawImageUnscaled(bitmap, layout.IconPosition);
			}
		}

		/// <summary>
		/// Draws the level stars onto a monster card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the level stars</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="level">Number of level stars to draw</param>
		public static void DrawLevelStars(Graphics graphics, Layout layout, RenderFlags flags, int level)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// LayoutProof draws a colored rectange only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.LevelStarBounds);
				return;
			}

			// For the level stars, create a transparent bitmap to draw into
			int cx = (level * layout.LevelStarSize.Width) + (layout.LevelStarPadding * (level - 1));
			using(Bitmap bmp = new Bitmap(cx, (int)Math.Ceiling(layout.LevelStarBounds.Height), PixelFormat.Format32bppArgb))
			{
				bmp.SetResolution(96.0F, 96.0F);
				using(Graphics gr = Graphics.FromImage(bmp))
				{
					int x = 0;
					for(int index = 0; index < level; index++)
					{
						gr.DrawImageUnscaled(layout.LevelStar, x, 0);
						x += layout.LevelStarSize.Width + layout.LevelStarPadding;
					}
				}

				// Calcluate the y offset for the image within the background
				int y = (int)((layout.LevelStarBounds.Height - layout.LevelStar.Height) / 2.0F) +
					(int)layout.LevelStarBounds.Top;

				// If the bitmap will fit in the header bounds, draw it right aligned
				if(cx <= (int)layout.LevelStarBounds.Width)
				{
					graphics.DrawImageUnscaled(bmp, new Point((int)layout.LevelStarBounds.Right - cx, y));
				}

				// Otherwise center the image horizontally on the background
				else
				{
					int x = (int)((layout.BackgroundSize.Width - (float)cx) / 2.0F);
					graphics.DrawImageUnscaled(bmp, new Point(x, y));
				}
			}

		}

		/// <summary>
		/// Draws the text for an effect monster card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="text">Text to be drawn</param>
		public static void DrawMonsterText(Graphics graphics, Layout layout, RenderFlags flags, string text)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(text == null) throw new ArgumentNullException(nameof(text));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.SpellTrapTextBounds);
				return;
			}
		}

		/// <summary>
		/// Draws the monster card type text
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="types">Array of types to be drawn</param>
		public static void DrawMonsterTypes(Graphics graphics, Layout layout, RenderFlags flags, string[] types)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(types == null) throw new ArgumentNullException(nameof(types));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.SpellTrapTextBounds);
				return;
			}

			// The types are drawn left-aligned and vertically centered in the bounds
			StringFormat format = new StringFormat(StringFormat.GenericTypographic);
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;
			format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			SizeF bracketsize = graphics.MeasureString("]", layout.MonsterTypeFont, int.MaxValue, format);
			RectangleF bounds = layout.MonsterTypeBounds;

			// Render the bracket character into a new bitmap
			using(Bitmap bracket = new Bitmap((int)Math.Ceiling(bracketsize.Width), (int)Math.Ceiling(bracketsize.Height),
				PixelFormat.Format32bppArgb))
			{
				bracket.SetResolution(96.0F, 96.0F);
				using(Graphics gr = Graphics.FromImage(bracket))
				{
					gr.SmoothingMode = SmoothingMode.AntiAlias;
					gr.TextRenderingHint = TextRenderingHint.AntiAlias;
					gr.DrawString("[", layout.MonsterTypeFont, Brushes.Black, new RectangleF(0, 0, bracket.Width, bracket.Height), format);
				}

				int x = (int)bounds.Left;
				int y = (int)bounds.Top + (int)((bounds.Height - bracket.Height) / 2.0F);
				y -= (int)layout.QuarterSpace;
				graphics.DrawImageUnscaled(bracket, new Point(x, y));
				bounds = new RectangleF(bounds.Left + bracketsize.Width + layout.QuarterSpace, bounds.Top,
					bounds.Width - bracketsize.Width - layout.QuarterSpace, bounds.Height);

				int index = 0;
				while(index < types.Length)
				{
					// Draw the current type/subtype
					SizeF size = graphics.MeasureString(types[index], layout.MonsterTypeFont, int.MaxValue, format);
					graphics.DrawString(types[index], layout.MonsterTypeFont, Brushes.Black, bounds, format);
					bounds = new RectangleF(bounds.Left + size.Width, bounds.Top, bounds.Width - size.Width, bounds.Height);

					// Move to the next type/subtype
					index++;

					// If there are more type/subtypes to draw, insert a slash between them
					if(index < types.Length)
					{
						bounds = new RectangleF(bounds.Left + layout.QuarterSpace, bounds.Top, bounds.Width - layout.QuarterSpace,
							bounds.Height);
						SizeF slashsize = graphics.MeasureString("/", layout.MonsterTypeFont, int.MaxValue, format);
						graphics.DrawString("/", layout.MonsterTypeFont, Brushes.Black, bounds, format);
						bounds = new RectangleF(bounds.Left + slashsize.Width + layout.QuarterSpace, bounds.Top,
							bounds.Width - slashsize.Width - layout.QuarterSpace, bounds.Height);
					}
				}

				bracket.RotateFlip(RotateFlipType.RotateNoneFlipX);
				bounds = new RectangleF(bounds.Left + layout.QuarterSpace, bounds.Top,
					bounds.Width - layout.QuarterSpace, bounds.Height);
				x = (int)bounds.Left;
				graphics.DrawImageUnscaled(bracket, new Point(x, y));
			}
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
			StringFormat format = new StringFormat(StringFormat.GenericTypographic);
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
		/// Draws the text for a normal monster card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="text">Text to be drawn</param>
		public static void DrawNormalMonsterText(Graphics graphics, Layout layout, RenderFlags flags, string text)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(text == null) throw new ArgumentNullException(nameof(text));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.SpellTrapTextBounds);
				return;
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
			if(passcode == null) throw new ArgumentNullException(nameof(passcode));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.PasscodeBounds);
				return;
			}

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// The passcode is drawn left-aligned and vertically centered in the bounds
			StringFormat format = new StringFormat(StringFormat.GenericTypographic);
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			// The Passcode is always drawn in black text
			graphics.DrawString(passcode, layout.PasscodeFont, Brushes.Black,
				layout.PasscodeBounds, format);
		}

		/// <summary>
		/// Draws the text for a spell or trap card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="flags">Renderer flags</param>
		/// <param name="text">Text to be drawn</param>
		public static void DrawSpellTrapText(Graphics graphics, Layout layout, RenderFlags flags, string text)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(text == null) throw new ArgumentNullException(nameof(text));

			// LayoutProof draws a colored rectangle only
			if(flags == RenderFlags.LayoutProof)
			{
				graphics.FillRectangle(Brushes.LightGreen, layout.SpellTrapTextBounds);
				return;
			}

			JustifyText(graphics, layout.SpellTrapTextBounds, layout.SpellTrapTextFont, Brushes.Black, text);
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

		//-------------------------------------------------------------------------
		// Private Member Functions
		//-------------------------------------------------------------------------

		/// <summary>
		/// Fully justifies text in a rectangle
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="bounds">Bounding rectangle</param>
		/// <param name="font">Base font</param>
		/// <param name="brush">Text color brush</param>
		/// <param name="text">Text to be justified</param>
		private static void JustifyText(Graphics graphics, RectangleF bounds, Font font, Brush brush, string text)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(font == null) throw new ArgumentNullException(nameof(font));
			if(brush == null) throw new ArgumentNullException(nameof(brush));
			if(text == null) throw new ArgumentNullException(nameof(text));

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			StringFormat format = new StringFormat(StringFormat.GenericTypographic);
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Near;

			// Reduce the font size until the text will fit in the bounding area
			SizeF required = graphics.MeasureString(text, font, (int)Math.Ceiling(bounds.Width), format);
			while(required.Height > bounds.Height)
			{
				font = new Font(font.FontFamily, font.Size - 1, font.Style, GraphicsUnit.Pixel);
				required = graphics.MeasureString(text, font, (int)Math.Ceiling(bounds.Width), format);
			}

			//
			// TODO: Actually justify the text, this is OK for now. MeasureCharacterRanges might be
			// useful to break up the string into lines for manual drawing as opposed to the legacy 
			// method, let GDI+ do the work ...
			//
			graphics.DrawString(text, font, brush, bounds, format);
		}
	}
}
