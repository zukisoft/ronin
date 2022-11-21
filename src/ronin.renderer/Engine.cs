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
		/// <param name="artwork">Artwork to be drawn</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void DrawArtwork(Graphics graphics, Layout layout, Bitmap artwork)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(artwork == null) throw new ArgumentNullException(nameof(artwork));

			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.DrawImage(artwork, layout.ArtworkPosition.X, layout.ArtworkPosition.Y, layout.ArtworkSize.Width,
				layout.ArtworkSize.Height);
		}

		/// <summary>
		/// Draws the attack and defense area of a monster card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the attribute</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="attack">Attack value</param>
		/// <param name="defense">Defense value</param>
		public static void DrawAttackDefense(Graphics graphics, Layout layout, int attack, int defense)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// Top/Center alignment
			StringFormat leftformat = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center
			};

			// Right/Center alignment
			StringFormat rightformat = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Far,
				LineAlignment = StringAlignment.Center
			};

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// Draw the line across the top of the text area
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
		/// <param name="attribute">Attribute to be drawn</param>
		public static void DrawAttribute(Graphics graphics, Layout layout, CardAttribute attribute)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			Bitmap bitmap = null;

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
		public static void DrawCopyright(Graphics graphics, Layout layout)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// Right/Center alignment
			StringFormat format = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Far,
				LineAlignment = StringAlignment.Center
			};

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// The copyright is always drawn in black text
			graphics.DrawString(layout.Copyright, layout.CopyrightFont, Brushes.Black, 
				layout.CopyrightBounds, format);
		}

		/// <summary>
		/// Draws the text for a fusion monster card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="materials">Fusion materials</param>
		/// <param name="text">Text to be drawn</param>
		public static void DrawFusionMonsterText(Graphics graphics, Layout layout, string materials, string text)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(materials == null) throw new ArgumentNullException(nameof(materials));
			if(text == null) throw new ArgumentNullException(nameof(text));

			// Top/Left alignment
			StringFormat format = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Near
			};

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// Fusion materials are rendered using a special method that keeps them on one line
			// but still drawn with the adjusted typeface size of the effect text
			JustifyText(graphics, layout.MonsterTextBounds, layout.MonsterTextFont, Brushes.Black, text, materials);
		}

		/// <summary>
		/// Draws the Eye of Anubis hologram onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the artwork</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="artwork">Artwork to be drawn</param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void DrawHologram(Graphics graphics, Layout layout)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			Bitmap bitmap = layout.Hologram;
			Debug.Assert(bitmap.Size == layout.HologramSize);
			graphics.DrawImageUnscaled(bitmap, layout.HologramPosition);
		}

		/// <summary>
		/// Draws a spell/trap card header onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the header</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="header">Header string</param>
		/// <param name="hasicon">Flag to leave space for the icon</param>
		public static void DrawHeader(Graphics graphics, Layout layout, string header, bool hasicon)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(header == null) throw new ArgumentNullException(nameof(header));

			// Right/Center alignment; measure trailing spaces
			StringFormat format = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Far,
				LineAlignment = StringAlignment.Center
			};
			format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// Determine the size of a bracket character and half of a space character for alignments
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

				// Right bracket
				int x = (int)bounds.Right - bracket.Width;
				int y = (int)bounds.Top + (int)((bounds.Height - bracket.Height) / 2.0F);
				y -= (int)layout.QuarterSpace;
				graphics.DrawImageUnscaled(bracket, new Point(x, y));
				bounds.Inflate(new SizeF(-(bracketsize.Width + halfspace), 0.0F));

				// Icon spacing
				if(hasicon) bounds.Inflate(new SizeF(-layout.IconSize.Width, 0.0F));

				// Header text
				SizeF textsize = graphics.MeasureString(header, layout.HeaderFont, int.MaxValue, format);
				graphics.DrawString(header, layout.HeaderFont, Brushes.Black, bounds, format);
				bounds.Inflate(new SizeF(-(textsize.Width + halfspace), 0.0F));

				// Left bracket
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
		/// <param name="icon">Icon to be drawn</param>
		public static void DrawIcon(Graphics graphics, Layout layout, CardIcon icon)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			Bitmap bitmap = null;

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
		/// <param name="level">Number of level stars to draw</param>
		public static void DrawLevelStars(Graphics graphics, Layout layout, int level)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// Render the level stars into a bitmap of the proper width
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
		/// <param name="text">Text to be drawn</param>
		public static void DrawMonsterText(Graphics graphics, Layout layout, string text)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(text == null) throw new ArgumentNullException(nameof(text));

			// Render the text using the justifier
			JustifyText(graphics, layout.MonsterTextBounds, layout.MonsterTextFont, Brushes.Black, text);
		}

		/// <summary>
		/// Draws the monster card type text
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="types">Array of types to be drawn</param>
		public static void DrawMonsterTypes(Graphics graphics, Layout layout, string[] types)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(types == null) throw new ArgumentNullException(nameof(types));

			// Top/Center alignment; measure trailing spaces
			StringFormat format = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center
			};
			format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// Determine the size of a bracket character
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

				// Left bracket
				int x = (int)bounds.Left;
				int y = (int)bounds.Top + (int)((bounds.Height - bracket.Height) / 2.0F);
				y -= (int)layout.QuarterSpace;
				graphics.DrawImageUnscaled(bracket, new Point(x, y));
				bounds = new RectangleF(bounds.Left + bracketsize.Width + layout.QuarterSpace, bounds.Top,
					bounds.Width - bracketsize.Width - layout.QuarterSpace, bounds.Height);

				// Types
				int index = 0;
				while(index < types.Length)
				{
					// Draw the current type/subtype
					SizeF size = graphics.MeasureString(types[index], layout.MonsterTypeFont, int.MaxValue, format);
					graphics.DrawString(types[index], layout.MonsterTypeFont, Brushes.Black, bounds, format);
					bounds = new RectangleF(bounds.Left + size.Width, bounds.Top, bounds.Width - size.Width, bounds.Height);

					// If there are more type/subtypes to draw, insert a slash between them
					if(++index < types.Length)
					{
						bounds = new RectangleF(bounds.Left + layout.QuarterSpace, bounds.Top, bounds.Width - layout.QuarterSpace,
							bounds.Height);
						SizeF slashsize = graphics.MeasureString("/", layout.MonsterTypeFont, int.MaxValue, format);
						graphics.DrawString("/", layout.MonsterTypeFont, Brushes.Black, bounds, format);
						bounds = new RectangleF(bounds.Left + slashsize.Width + layout.QuarterSpace, bounds.Top,
							bounds.Width - slashsize.Width - layout.QuarterSpace, bounds.Height);
					}
				}

				// Right bracket
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
		/// <param name="name">Card name to be drawn</param>
		/// <param name="brush">Brush to use when drawing the name</param>
		public static void DrawName(Graphics graphics, Layout layout, string name, Brush brush)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			// Top/Center alignment
			StringFormat format = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center
			};

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

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

				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.DrawImage(bmp, layout.NameBounds);
			}
		}

		/// <summary>
		/// Draws the text for a normal monster card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="text">Text to be drawn</param>
		public static void DrawNormalMonsterText(Graphics graphics, Layout layout, string text)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(text == null) throw new ArgumentNullException(nameof(text));

			// Render the text using the justifier
			JustifyText(graphics, layout.MonsterTextBounds, layout.NormalMonsterTextFont, Brushes.Black, text);
		}

		/// <summary>
		/// Draws the passcode string onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="passcode">Passcode string</param>
		public static void DrawPasscode(Graphics graphics, Layout layout, string passcode)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(passcode == null) throw new ArgumentNullException(nameof(passcode));

			// Left/Center alignment
			StringFormat format = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Center
			};

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			graphics.DrawString(passcode, layout.PasscodeFont, Brushes.Black, layout.PasscodeBounds, format);
		}

		/// <summary>
		/// Draws the set code string onto an existing background
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="setcode">Set code string</param>
		public static void DrawSetCode(Graphics graphics, Layout layout, string setcode)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(setcode == null) throw new ArgumentNullException(nameof(setcode));

			// Right/Center alignment
			StringFormat format = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Far,
				LineAlignment = StringAlignment.Center
			};

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			graphics.DrawString(setcode, layout.SetCodeFont, Brushes.Black, layout.SetCodeBounds, format);
		}

		/// <summary>
		/// Draws the text for a spell or trap card
		/// </summary>
		/// <param name="graphics">Graphics object on which to draw the text</param>
		/// <param name="layout">Renderer layout instance</param>
		/// <param name="text">Text to be drawn</param>
		public static void DrawSpellTrapText(Graphics graphics, Layout layout, string text)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(layout == null) throw new ArgumentNullException(nameof(layout));
			if(text == null) throw new ArgumentNullException(nameof(text));

			// Render the text using the justifier
			JustifyText(graphics, layout.SpellTrapTextBounds, layout.SpellTrapTextFont, Brushes.Black, text);
		}

		/// <summary>
		/// Renders the background image
		/// </summary>
		/// <param name="layout">Renderer Layout instance</param>
		/// <param name="background">Background to be rendered</param>
		public static Bitmap RenderBackground(Layout layout, Background background)
		{
			if(layout == null) throw new ArgumentNullException(nameof(layout));

			Bitmap bitmap;

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

		private static int CountSpaces(string str)
		{
			int count = 0;
			char[] testchars = str.ToCharArray();
			int length = testchars.Length;
			for(int n = 0; n < length; n++)
			{
				if(testchars[n] == ' ')
					count++;
			}
			return count;
		}

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
			JustifyText(graphics, bounds, font, brush, text, null);
		}

		/// <summary>
		/// Fully justifies text in a rectangle
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="bounds">Bounding rectangle</param>
		/// <param name="font">Base font</param>
		/// <param name="brush">Text color brush</param>
		/// <param name="text">Text to be justified</param>
		/// <param name="topline">Topline text to be justified (fusion monsters)</param>
		private static void JustifyText(Graphics graphics, RectangleF bounds, Font font, Brush brush, string text,
			string topline)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(font == null) throw new ArgumentNullException(nameof(font));
			if(brush == null) throw new ArgumentNullException(nameof(brush));
			if(text == null) throw new ArgumentNullException(nameof(text));

			// Remove any CR/LF/SPACE characters from the end of both the topline and text strings
			if(topline != null) topline = topline.TrimEnd(new char[] { '\r', '\n', ' ' });
			text = text.TrimEnd(new char[] { '\r', '\n', ' ' });

			// Top/Left alignment
			StringFormat format = new StringFormat(StringFormat.GenericTypographic)
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Near,
			};

			// Full justification relies on measurement of a single space character
			format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

			// Fusion monsters require the top line of text to be justified differently, accomodate
			// the necessary vertical space by adding a CRLF to the beginning of the effect text
			string measuretext = string.IsNullOrEmpty(topline) ? text : "\r\n" + text;

			// Calculate the available boundaries for the justified text
			RectangleF textbounds = new RectangleF(bounds.Left, bounds.Top, bounds.Width, bounds.Height);

			// Reduce the font size and increase the area until the unjustified text will fit in the required space
			SizeF required = graphics.MeasureString(measuretext, font, (int)Math.Ceiling(textbounds.Width), format);
			while(required.Height > textbounds.Height)
			{
				font = new Font(font.FontFamily, font.Size - 1, font.Style, GraphicsUnit.Pixel);
				required = graphics.MeasureString(measuretext, font, (int)Math.Ceiling(textbounds.Width), format);
				if(required.Height > textbounds.Height)
				{
					// This was a judgment call/SWAG; the text will not exactly match the layout of a real card,
					// but I found the effect it has produces more legible results for "screen" as opposed to "print"
					textbounds.Inflate(new SizeF(bounds.Width / 50.0F, 0.0F));
					required = graphics.MeasureString(measuretext, font, (int)Math.Ceiling(textbounds.Width), format);
				}
			}

			// Render the topline bitmap using the same font that will be used for the effect text
			Bitmap toplinebmp = null;
			if(!string.IsNullOrEmpty(topline))
			{
				SizeF toplinesize = graphics.MeasureString(topline, font, int.MaxValue, format);

				// Render the text into a transparent bitmap and smush it horizontally to fit
				toplinebmp = new Bitmap((int)Math.Ceiling(toplinesize.Width), (int)Math.Ceiling(toplinesize.Height),
					PixelFormat.Format32bppArgb);
				toplinebmp.SetResolution(96.0F, 96.0F);
				using(Graphics gr = Graphics.FromImage(toplinebmp))
				{
					gr.SmoothingMode = SmoothingMode.AntiAlias;
					gr.TextRenderingHint = TextRenderingHint.AntiAlias;
					gr.DrawString(topline, font, brush, new RectangleF(0, 0, toplinesize.Width, toplinesize.Height), format);
				}
			}

			// Render the text into a transparent bitmap that will be interpolated into the bounding region
			using(Bitmap bmp = new Bitmap((int)Math.Ceiling(textbounds.Width), (int)Math.Ceiling(required.Height),
				PixelFormat.Format32bppArgb))
			{
				bmp.SetResolution(96.0F, 96.0F);
				using(Graphics gr = Graphics.FromImage(bmp))
				{
					gr.SmoothingMode = SmoothingMode.AntiAlias;
					gr.TextRenderingHint = TextRenderingHint.AntiAlias;
					gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

					// If there is a top line bitmap draw that first; it only gets compressed horizontally
					if(toplinebmp != null)
					{
						if(toplinebmp.Width <= bmp.Width) gr.DrawImageUnscaled(toplinebmp, new Point(0, 0));
						else gr.DrawImage(toplinebmp, new Rectangle(0, 0, bmp.Width, toplinebmp.Height));
					}

					// Determine the height of a single line of text and how many lines are available to us
					float lineheight = gr.MeasureString("\r\n", font, int.MaxValue, format).Height;
					int numlines = (int)Math.Round(required.Height / lineheight);

					// Track the current line of text for formatting purposes
					int currentline = 0;

					// Break the text up into hard lines of text that are separated by CRLF
					foreach(string hardline in measuretext.Split(new string[] { "\r\n" }, StringSplitOptions.None))
					{
						string line = string.Empty;

						// Break up the hard line of text into individual words
						string[] words = hardline.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

						// Accumulate words until we run out of words or run out of width to draw them
						int wordindex = 0;
						while(wordindex < words.Length)
						{
							// Append the next word to the current line and calculate the new width
							string linetemp = line;
							linetemp += ((line.Length == 0) ? "" : " ") + words[wordindex];
							SizeF linesize = gr.MeasureString(linetemp, font, int.MaxValue, format);

							// If the new word would exceed the available width, terminate the line
							if(Math.Ceiling(linesize.Width) > bmp.Width)
							{
								// If the final word that will fit on the line contains a hypen, see if
								// breaking that word after the hyphen will allow it to fit and adjust
								if(words[wordindex].Contains("-"))
								{
									string[] hyphenated = words[wordindex].Split(new char[] { '-' }, 2);
									if(hyphenated.Length == 2)
									{
										string hyphentemp = line + ((line.Length == 0) ? "" : " ") + hyphenated[0] + "-";
										SizeF hyphensize = gr.MeasureString(hyphentemp, font, int.MaxValue, format);
										if(Math.Ceiling(hyphensize.Width) <= bmp.Width)
										{
											line = hyphentemp;
											words[wordindex] = hyphenated[1];
										}
									}
								}

								// Full justification is normally accomplished by adjusting the width of each space
								// character in the line to be wider than it should be, but I found the end result
								// to look better if there is also a minor increase in character spacing. Calcuate
								// the required metrics to favor wide spaces when there is a large gap to fill, but 
								// favor character spacing when there is a small gap to fill. Flooring the "space extra"
								// value and then transferring any leftover spacing to "character extra" seems to 
								// accomplish this very well, and provides a good match to how the physical cards look
								float left = 0.0F;
								float lineextra = bmp.Width - gr.MeasureString(line, font, int.MaxValue, format).Width;
								int numspaces = CountSpaces(line);
								float spaceextra = (float)Math.Floor(lineextra / numspaces);
								float charextra = (lineextra - (spaceextra * numspaces)) / line.Length;

								// Render each character in the line individually using the full justification metrics
								foreach(char character in line)
								{
									string str = new string(character, 1);
									float strwidth = gr.MeasureString(str, font, int.MaxValue, format).Width;
									gr.DrawString(str, font, brush, new PointF(left, lineheight * currentline), format);
									left += strwidth + ((character == ' ') ? spaceextra : charextra);
								}

								// Current line has now been rendered; move onto the next line
								currentline++;
								line = string.Empty;
							}

							else
							{
								// Still under the width limit; move onto the next word in this line
								line = linetemp;
								wordindex++;
							}
						}

						// The final line of text gets rendered with normal left-justification
						if(wordindex == words.Length)
						{
							gr.DrawString(line, font, brush, new PointF(0.0F, lineheight * currentline), format);
							currentline++;
						}
					}
				}

				// If the bitmap would fit in the original boundaries, draw it unscaled, otherwise
				// interpolate it into the original boundaries
				if((bmp.Width <= bounds.Width) && (bmp.Height <= bounds.Height))
					graphics.DrawImageUnscaled(bmp, new Point((int)bounds.Left, (int)bounds.Top));
				else graphics.DrawImage(bmp, bounds);
			}

			toplinebmp?.Dispose();
		}
	}
}
