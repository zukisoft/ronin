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

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Describes the layout parameters for the rendering engine
	/// </summary>
	internal class Layout
	{
		/// <summary>
		/// Boundary of the monster card attack region
		/// </summary>
		public virtual RectangleF AttackBounds { get; }

		/// <summary>
		/// Boundary of the monster card attack/defense region
		/// </summary>
		public virtual RectangleF AttackDefenseBounds { get; }

		/// <summary>
		/// Font used to render ATK/DEF text
		/// </summary>
		public virtual Font AttackDefenseFont { get; }

		/// <summary>
		/// Position of the artwork image
		/// </summary>
		public virtual Point ArtworkPosition { get; }

		/// <summary>
		/// Size of the artwork image
		/// </summary>
		public virtual Size ArtworkSize { get; }

		/// <summary>
		/// DARK attribute image
		/// </summary>
		public virtual Bitmap AttributeDark { get; }

		/// <summary>
		/// EARTH attribute image
		/// </summary>
		public virtual Bitmap AttributeEarth { get; }

		/// <summary>
		/// FIRE attribute image
		/// </summary>
		public virtual Bitmap AttributeFire { get; }

		/// <summary>
		/// LIGHT attribute image
		/// </summary>
		public virtual Bitmap AttributeLight { get; }

		/// <summary>
		/// Position of the attribute image
		/// </summary>
		public virtual Point AttributePosition { get; }

		/// <summary>
		/// Size of the attribute image
		/// </summary>
		public virtual Size AttributeSize { get; }

		/// <summary>
		/// SPELL attribute image
		/// </summary>
		public virtual Bitmap AttributeSpell { get; }

		/// <summary>
		/// TRAP attribute image
		/// </summary>
		public virtual Bitmap AttributeTrap { get; }

		/// <summary>
		/// WATER attribute image
		/// </summary>
		public virtual Bitmap AttributeWater { get; }

		/// <summary>
		/// WIND attribute image
		/// </summary>
		public virtual Bitmap AttributeWind { get; }

		/// <summary>
		/// Background image for an effect monster card
		/// </summary>
		public virtual Bitmap BackgroundEffectMonster { get; }

		/// <summary>
		/// Background image for a fusion monster card
		/// </summary>
		public virtual Bitmap BackgroundFusionMonster { get; }

		/// <summary>
		/// Background image for a normal monster card
		/// </summary>
		public virtual Bitmap BackgroundNormalMonster { get; }

		/// <summary>
		/// Background image for a ritual monster card
		/// </summary>
		public virtual Bitmap BackgroundRitualMonster { get; }

		/// <summary>
		/// Background image size
		/// </summary>
		public virtual Size BackgroundSize { get; }

		/// <summary>
		/// Background image for a spell card
		/// </summary>
		public virtual Bitmap BackgroundSpell { get; }

		/// <summary>
		/// Background image for a token card
		/// </summary>
		public virtual Bitmap BackgroundToken { get; }

		/// <summary>
		/// Background image for a transparent card
		/// </summary>
		public virtual Bitmap BackgroundTransparent { get; }

		/// <summary>
		/// Background image for a trap card
		/// </summary>
		public virtual Bitmap BackgroundTrap { get; }

		/// <summary>
		/// Copyright string
		/// </summary>
		public virtual string Copyright { get; }

		/// <summary>
		/// Boundary of the copyright
		/// </summary>
		public virtual RectangleF CopyrightBounds { get; }

		/// <summary>
		/// Font used to draw the copyright
		/// </summary>
		public virtual Font CopyrightFont { get; }

		/// <summary>
		/// Boundary of the monster card defense region
		/// </summary>
		public virtual RectangleF DefenseBounds { get; }

		/// <summary>
		/// Width of a drawn line
		/// </summary>
		public virtual float LineWidth { get; }

		/// <summary>
		/// Font used when drawing non-normal monster card text
		/// </summary>
		public virtual Font MonsterTextFont { get; }

		/// <summary>
		/// Font used when drawing normal monster card text
		/// </summary>
		public virtual Font NormalMonsterTextFont { get; }

		/// <summary>
		/// Boundary of the header area above the image
		/// </summary>
		public virtual RectangleF HeaderBounds { get; }

		/// <summary>
		/// Font used to draw the header
		/// </summary>
		public virtual Font HeaderFont { get; }

		/// <summary>
		/// Eye of Anubis hologram image
		/// </summary>
		public virtual Bitmap Hologram { get; }

		/// <summary>
		/// Position of the hologram image
		/// </summary>
		public virtual Point HologramPosition { get; }

		/// <summary>
		/// Size of the hologram image
		/// </summary>
		public virtual Size HologramSize { get; }

		/// <summary>
		/// CONTINUOUS icon image
		/// </summary>
		public virtual Bitmap IconContinuous { get; }

		/// <summary>
		/// COUNTER icon image
		/// </summary>
		public virtual Bitmap IconCounter { get; }

		/// <summary>
		/// EQUIP icon image
		/// </summary>
		public virtual Bitmap IconEquip { get; }

		/// <summary>
		/// FIELD icon image
		/// </summary>
		public virtual Bitmap IconField { get; }

		/// <summary>
		/// Position of the spell/trap icon
		/// </summary>
		public virtual Point IconPosition { get; }

		/// <summary>
		/// QUICK-PLAY icon image
		/// </summary>
		public virtual Bitmap IconQuickPlay { get; }

		/// <summary>
		/// RITUAL icon image
		/// </summary>
		public virtual Bitmap IconRitual { get; }

		/// <summary>
		/// Size of the spell/trap icon
		/// </summary>
		public virtual Size IconSize { get; }

		/// <summary>
		/// Level star image
		/// </summary>
		public virtual Bitmap LevelStar { get; }

		/// <summary>
		/// Boundary of the level star area above the image
		/// </summary>
		public virtual RectangleF LevelStarBounds { get; }

		/// <summary>
		/// Padding between level stars
		/// </summary>
		public virtual int LevelStarPadding { get; }

		/// <summary>
		/// Size of a level star image
		/// </summary>
		public virtual Size LevelStarSize { get; }

		/// <summary>
		/// Boundary of the card name
		/// </summary>
		public virtual RectangleF NameBounds { get; }

		/// <summary>
		/// Font used to draw the card name
		/// </summary>
		public virtual Font NameFont { get; }
		
		/// <summary>
		/// Boundary of the passcode
		/// </summary>
		public virtual RectangleF PasscodeBounds { get; }

		/// <summary>
		/// Font used to draw the passcode
		/// </summary>
		public virtual Font PasscodeFont { get; }

		/// <summary>
		/// Width of a "quarter space" fudge factor for layout
		/// </summary>
		public virtual float QuarterSpace { get; }

		/// <summary>
		/// Font used when drawing spell and trap card text
		/// </summary>
		public virtual Font SpellTrapTextFont { get; }

		/// <summary>
		/// Boundary of spell and trap card text
		/// </summary>
		public virtual RectangleF SpellTrapTextBounds { get; }
	}
}
