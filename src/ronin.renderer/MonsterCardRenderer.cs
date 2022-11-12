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
	/// Implements the monster card rendering engine
	/// </summary>
	internal class MonsterCardRenderer
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public MonsterCardRenderer() : this(RenderFlags.None)
		{
		}

		/// <summary>
		/// Instance Constructor
		/// </summary>
		/// <param name="flags">Rendering flags</param>
		public MonsterCardRenderer(RenderFlags flags)
		{
			m_flags = flags;
		}

		/// <summary>
		/// Renders a spell card
		/// </summary>
		/// <param name="card">MonsterCard to be rendered</param>
		public Bitmap RenderCard(MonsterCard card)
		{
			Bitmap bitmap = RenderBackground(card);
			using(Graphics graphics = Graphics.FromImage(bitmap))
			{
				// Render the common elements
				RenderCommon(graphics, card);

				// Draw the card name in solid black
				Engine.DrawName(graphics, s_layout, m_flags, card.Name, NameBrush.SolidBlack);
			}

			return bitmap;
		}

		/// <summary>
		/// Renders a specific print of a spell card
		/// </summary>
		/// <param name="monstercard">MonsterCard to be rendered</param>
		/// <param name="print">Print information about the MonsterCard</param>
		public Bitmap RenderPrint(MonsterCard monstercard, Print print)
		{
			return null;
		}

		//-------------------------------------------------------------------------
		// Private Member Functions

		/// <summary>
		/// Renders the background image for the monster card
		/// </summary>
		/// <param name="monstercard">MonsterCard instance</param>
		private Bitmap RenderBackground(MonsterCard monstercard)
		{
			// Select the proper background for the monster card
			Background backgroundtype = Background.EffectMonster;
			if(monstercard.Normal) backgroundtype = Background.NormalMonster;
			else if(monstercard.Fusion) backgroundtype = Background.FusionMonster;
			else if(monstercard.Ritual) backgroundtype = Background.RitualMonster;

			// Generate the background image for the monster card
			Bitmap background = Engine.RenderBackground(s_layout, m_flags, backgroundtype);
			if(background == null) throw new Exception("Failed to render background image for card");
			Debug.Assert(background.Size == s_layout.BackgroundSize);

			return background;
		}

		/// <summary>
		/// Renders the common elements between a Card render and a Print render
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="monstercard">MonsterCard instance</param>
		private void RenderCommon(Graphics graphics, MonsterCard monstercard)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(monstercard == null) throw new ArgumentNullException(nameof(monstercard));

			// Attribute
			Engine.DrawAttribute(graphics, s_layout, m_flags, monstercard.Attribute);

			// Artwork
			Bitmap artwork = monstercard.GetArtwork();
			if(artwork == null) artwork = Resources.defaultartwork;
			if(artwork != null) Engine.DrawArtwork(graphics, s_layout, m_flags, artwork);

			// Level Stars
			Engine.DrawLevelStars(graphics, s_layout, m_flags, monstercard.Level);

			// Text (TODO!)
			if(monstercard.Normal) Engine.DrawNormalMonsterText(graphics, s_layout, m_flags, monstercard.Text);
			else Engine.DrawMonsterText(graphics, s_layout, m_flags, monstercard.Text);

			// Passcode
			Engine.DrawPasscode(graphics, s_layout, m_flags, monstercard.Passcode);

			// Copyright
			Engine.DrawCopyright(graphics, s_layout, m_flags);

			// Hologram
			Engine.DrawHologram(graphics, s_layout, m_flags);
		}

		//-------------------------------------------------------------------------
		// Member Variables

		/// <summary>
		/// Only the 'medium' renderer is currently supported
		/// </summary>
		private static Layout s_layout = new LayoutMedium();

		/// <summary>
		/// Specified special rendering flags
		/// </summary>
		private readonly RenderFlags m_flags;
	}
}
