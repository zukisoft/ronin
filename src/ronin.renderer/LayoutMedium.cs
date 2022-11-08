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
		// Position of the artwork image
		public override Point ArtworkPosition => new Point(113, 265);

		/// <summary>
		/// Size of the artwork image
		/// </summary>
		public override Size ArtworkSize => new Size(624, 632);

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
				Size size = ImageSize;
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
		/// Image size
		/// </summary>
		public override Size ImageSize => new Size(846, 245);
	}
}
