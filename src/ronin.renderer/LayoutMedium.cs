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

using System.Drawing;

using zuki.ronin.renderer.Properties;

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Describes the layout parameters for the MEDIUM artifacts
	/// </summary>
	internal class LayoutMedium : Layout
	{
		/// <summary>
		/// Static Constructor
		/// </summary>
		static LayoutMedium()
		{
			// Font used to render ATK/DEF text
			s_atkdeftext = FontManager.Create(FontManager.EmbeddedFonts.MatrixBoldSmallCaps, 40,
				FontStyle.Regular, GraphicsUnit.Pixel);

			// Font used to render effect text
			s_effecttext = FontManager.Create(FontManager.EmbeddedFonts.MatrixBook, 26,
				FontStyle.Regular, GraphicsUnit.Pixel);

			// Font used to render flavor text
			s_flavortext = FontManager.Create(FontManager.EmbeddedFonts.StoneSerifLTItalic, 26,
				FontStyle.Regular, GraphicsUnit.Pixel);

			// Font used to render a spell/trap card header
			s_headerfont = FontManager.Create(FontManager.EmbeddedFonts.StoneSerifBoldSmallCaps, 42,
				FontStyle.Regular, GraphicsUnit.Pixel);

			// Font used to draw the card name
			s_namefont = FontManager.Create(FontManager.EmbeddedFonts.MatrixSmallCaps, 98,
				FontStyle.Regular, GraphicsUnit.Pixel);

			// Font used to draw the passcode and copyright
			s_passcodefont = FontManager.Create(FontManager.EmbeddedFonts.StoneSerifLT, 23,
				FontStyle.Regular, GraphicsUnit.Pixel);

			// Font used to draw the passcode and copyright
			s_setcodefont = FontManager.Create(FontManager.EmbeddedFonts.StoneSerifLT, 26,
				FontStyle.Regular, GraphicsUnit.Pixel);

			// Font used to draw a monster card type
			s_typefont = FontManager.Create(FontManager.EmbeddedFonts.StoneSerifBoldSmallCaps, 33,
				FontStyle.Regular, GraphicsUnit.Pixel);
		}

		/// <summary>
		/// Boundary of the monster card attack region
		/// </summary>
		public override RectangleF AttackBounds => new Rectangle(434, 1147, 160, 35);

		/// <summary>
		/// Boundary of the monster card attack/defense region
		/// </summary>
		public override RectangleF AttackDefenseBounds => new Rectangle(76, 1142, 694, 31);

		/// <summary>
		/// Font used to render ATK/DEF text
		/// </summary>
		public override Font AttackDefenseFont => s_atkdeftext;

		/// <summary>
		/// Position of the artwork image 
		/// </summary>
		public override Point ArtworkPosition => new Point(113, 265);

		/// <summary>
		/// Size of the artwork image
		/// </summary>
		public override Size ArtworkSize => new Size(624, 632);

		/// <summary>
		/// DARK attribute image
		/// </summary>
		public override Bitmap AttributeDark => Resources.attrdark;

		/// <summary>
		/// EARTH attribute image
		/// </summary>
		public override Bitmap AttributeEarth => Resources.attrearth;

		/// <summary>
		/// FIRE attribute image
		/// </summary>
		public override Bitmap AttributeFire => Resources.attrfire;

		/// <summary>
		/// LIGHT attribute image
		/// </summary>
		public override Bitmap AttributeLight => Resources.attrlight;

		/// <summary>
		/// Position of the attribute image
		/// </summary>
		public override Point AttributePosition => new Point(689, 68);

		/// <summary>
		/// Size of the attribute image
		/// </summary>
		public override Size AttributeSize => new Size(85, 85);

		/// <summary>
		/// SPELL attribute image
		/// </summary>
		public override Bitmap AttributeSpell => Resources.attrspell;

		/// <summary>
		/// TRAP attribute image
		/// </summary>
		public override Bitmap AttributeTrap => Resources.attrtrap;

		/// <summary>
		/// WATER attribute image
		/// </summary>
		public override Bitmap AttributeWater => Resources.attrwater;

		/// <summary>
		/// WIND attribute image
		/// </summary>
		public override Bitmap AttributeWind => Resources.attrwind;

		/// <summary>
		/// Background image for an effect monster card
		/// </summary>
		public override Bitmap BackgroundEffectMonster => Resources.cardeffect;

		/// <summary>
		/// Background image for a fusion monster card
		/// </summary>
		public override Bitmap BackgroundFusionMonster => Resources.cardfusion;

		/// <summary>
		/// Background image for a normal monster card
		/// </summary>
		public override Bitmap BackgroundNormalMonster => Resources.cardmonster;

		/// <summary>
		/// Background image for a ritual monster card
		/// </summary>
		public override Bitmap BackgroundRitualMonster => Resources.cardritual;

		/// <summary>
		/// Background image size
		/// </summary>
		public override Size BackgroundSize => new Size(846, 1245);

		/// <summary>
		/// Background image for a spell card
		/// </summary>
		public override Bitmap BackgroundSpell => Resources.cardspell;

		/// <summary>
		/// Background image for a token card
		/// </summary>
		public override Bitmap BackgroundToken => Resources.cardtoken;

		/// <summary>
		/// Background image for a transparent card
		/// </summary>
		public override Bitmap BackgroundTransparent
		{
			get
			{
				// Create a new transparent bitmap based on the layout image size
				Size size = BackgroundSize;
				Bitmap bitmap = new Bitmap(size.Width, size.Height);
				bitmap.SetResolution(96.0F, 96.0F);
				bitmap.MakeTransparent();
				return bitmap;
			}
		}

		/// <summary>
		/// Background image for a trap card
		/// </summary>
		public override Bitmap BackgroundTrap => Resources.cardtrap;

		/// <summary>
		/// Copyright string
		/// </summary>
		public override string Copyright => "©1996 KAZUKI TAKAHASHI";

		/// <summary>
		/// Boundary of the copyright
		/// </summary>
		public override RectangleF CopyrightBounds => new Rectangle(302, 1198, 475, 26);

		/// <summary>
		/// Font used to draw the copyright
		/// </summary>
		public override Font CopyrightFont => s_passcodefont;

		/// <summary>
		/// Boundary of the monster card defense region
		/// </summary>
		public override RectangleF DefenseBounds => new Rectangle(613, 1147, 158, 35);

		/// <summary>
		/// Boundary of the header area above the image
		/// </summary>
		public override RectangleF HeaderBounds => new Rectangle(96, 172, 658, 52);

		/// <summary>
		/// Font used to draw the header
		/// </summary>
		public override Font HeaderFont => s_headerfont;

		/// <summary>
		/// Eye of Anubis hologram image
		/// </summary>
		public override Bitmap Hologram => Resources.eyeofanubis;

		/// <summary>
		/// Position of the hologram image
		/// </summary>
		public override Point HologramPosition => new Point(792, 1189);

		/// <summary>
		/// Size of the hologram image
		/// </summary>
		public override Size HologramSize => new Size(42, 43);

		/// <summary>
		/// CONTINUOUS icon image
		/// </summary>
		public override Bitmap IconContinuous => Resources.iconcontinuous;

		/// <summary>
		/// COUNTER icon image
		/// </summary>
		public override Bitmap IconCounter => Resources.iconcounter;

		/// <summary>
		/// EQUIP icon image
		/// </summary>
		public override Bitmap IconEquip => Resources.iconequip;

		/// <summary>
		/// FIELD icon image
		/// </summary>
		public override Bitmap IconField => Resources.iconfield;

		/// <summary>
		/// Position of the spell/trap icon
		/// </summary>
		public override Point IconPosition => new Point(688, 180);

		/// <summary>
		/// QUICK-PLAY icon image
		/// </summary>
		public override Bitmap IconQuickPlay => Resources.iconquickplay;

		/// <summary>
		/// RITUAL icon image
		/// </summary>
		public override Bitmap IconRitual => Resources.iconritual;

		/// <summary>
		/// Size of the spell/trap icon
		/// </summary>
		public override Size IconSize => new Size(47, 47);

		/// <summary>
		/// Level star image
		/// </summary>
		public override Bitmap LevelStar => Resources.levelstar;

		/// <summary>
		/// Boundary of the level star area above the image
		/// </summary>
		public override RectangleF LevelStarBounds => new Rectangle(96, 172, 656, 62);

		/// <summary>
		/// Padding between level stars
		/// </summary>
		public override int LevelStarPadding => 5;

		/// <summary>
		/// Size of a level star image
		/// </summary>
		public override Size LevelStarSize => new Size(56, 56);

		/// <summary>
		/// Width of a drawn line
		/// </summary>
		public override float LineWidth => 2.0F;

		/// <summary>
		/// Boundary of monster card text
		/// </summary>
		public override RectangleF MonsterTextBounds => new Rectangle(78, 996, 692, 144);

		/// <summary>
		/// Font used when drawing non-normal monster card text
		/// </summary>
		public override Font MonsterTextFont => s_effecttext;

		/// <summary>
		/// Boundary of the monster type/subtype area
		/// </summary>
		public override RectangleF MonsterTypeBounds => new Rectangle(76, 960, 694, 29);

		/// <summary>
		/// Font used when drawing monster type card text
		/// </summary>
		public override Font MonsterTypeFont => s_typefont;

		/// <summary>
		/// Boundary of the card name
		/// </summary>
		public override RectangleF NameBounds => new Rectangle(77, 64, 602, 74);

		/// <summary>
		/// Font used to draw the card name
		/// </summary>
		public override Font NameFont => s_namefont;

		/// <summary>
		/// Font used when drawing normal monster card text
		/// </summary>
		public override Font NormalMonsterTextFont => s_flavortext;

		/// <summary>
		/// Boundary of the passcode
		/// </summary>
		public override RectangleF PasscodeBounds => new Rectangle(28, 1197, 250, 27);

		/// <summary>
		/// Font used to draw the passcode
		/// </summary>
		public override Font PasscodeFont => s_passcodefont;

		/// <summary>
		/// Width of a "quarter space" fudge factor for layout
		/// </summary>
		public override float QuarterSpace => 3.0F;

		/// <summary>
		/// Boundary of the set code
		/// </summary>
		public override RectangleF SetCodeBounds => new Rectangle(556, 918, 198, 26);

		/// <summary>
		/// Font used to draw the set code
		/// </summary>
		public override Font SetCodeFont => s_setcodefont;

		/// <summary>
		/// Font used when drawing spell and trap card text
		/// </summary>
		public override Font SpellTrapTextFont => s_effecttext;

		/// <summary>
		/// Boundary of spell and trap card text
		/// </summary>
		public override RectangleF SpellTrapTextBounds => new Rectangle(78, 966, 692, 207);

		//-------------------------------------------------------------------
		// Member Variables

		/// <summary>
		/// Font to use when rendering ATK/DEF text
		/// </summary>
		private static readonly Font s_atkdeftext;

		/// <summary>
		/// Font to use when rendering effect text
		/// </summary>
		private static readonly Font s_effecttext;

		/// <summary>
		/// Font to use when rendering flavor text
		/// </summary>
		private static readonly Font s_flavortext;

		/// <summary>
		/// Font to use when rendering a spell/trap card header
		/// </summary>
		private static readonly Font s_headerfont;

		/// <summary>
		/// Font to use when rendering the card name
		/// </summary>
		private static readonly Font s_namefont;

		/// <summary>
		/// Font to use when rendering the passcode
		/// </summary>
		private static readonly Font s_passcodefont;

		/// <summary>
		/// Font to use when rendering the set code
		/// </summary>
		private static readonly Font s_setcodefont;

		/// <summary>
		/// Font to use when rendering a monster type
		/// </summary>
		private static readonly Font s_typefont;
	}
}
