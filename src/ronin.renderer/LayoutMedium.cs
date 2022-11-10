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
			// Font used to draw the card name
			s_namefont = FontManager.Create(FontManager.EmbeddedFonts.MatrixSmallCaps, 98,
				FontStyle.Regular, GraphicsUnit.Pixel);

			// Font used to draw the passcode and copyright
			s_passcodefont = FontManager.Create(FontManager.EmbeddedFonts.StoneSerifLT, 23,
				FontStyle.Regular, GraphicsUnit.Pixel);
		}

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
		public override RectangleF CopyrightBounds => new Rectangle(302, 1198, 475, 28);

		/// <summary>
		/// Font used to draw the copyright
		/// </summary>
		public override Font CopyrightFont => s_passcodefont;

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
		/// Boundary of the card name
		/// </summary>
		public override RectangleF NameBounds => new Rectangle(77, 64, 602, 74);

		/// <summary>
		/// Font used to draw the card name
		/// </summary>
		public override Font NameFont => s_namefont;

		/// <summary>
		/// Boundary of the passcode
		/// </summary>
		public override RectangleF PasscodeBounds => new Rectangle(28, 1197, 250, 28);

		/// <summary>
		/// Font used to draw the passcode
		/// </summary>
		public override Font PasscodeFont => s_passcodefont;

		//-------------------------------------------------------------------
		// Member Variables

		/// <summary>
		/// Font to use when rendering the card name
		/// </summary>
		private static Font s_namefont;

		/// <summary>
		/// Font to use when rendering the passcode
		/// </summary>
		private static Font s_passcodefont;
	}
}
