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
using System.Collections.Generic;
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
		public MonsterCardRenderer()
		{
		}

		/// <summary>
		/// Renders a monster card
		/// </summary>
		/// <param name="monstercard">MonsterCard to be rendered</param>
		public Bitmap RenderCard(MonsterCard monstercard)
		{
			return RenderCard(monstercard, null);
		}

		/// <summary>
		/// Renders a monster card
		/// </summary>
		/// <param name="monstercard">MonsterCard to be rendered</param>
		/// <param name="alttext">Alternate text to be rendered</param>
		public Bitmap RenderCard(MonsterCard monstercard, string alttext)
		{
			Bitmap bitmap = RenderBackground(monstercard);
			using(Graphics graphics = Graphics.FromImage(bitmap))
			{
				// Render the common elements
				RenderCommon(graphics, monstercard, alttext);

				// Draw the card name in solid black
				Engine.DrawName(graphics, s_layout, monstercard.Name, NameBrush.SolidBlack);

				// Render the default artwork
				using(Artwork artwork = monstercard.GetDefaultArtwork())
				{
					Engine.DrawArtwork(graphics, s_layout, artwork.Image ?? Resources.defaultartwork);
				}
			}

			return bitmap;
		}

		/// <summary>
		/// Renders a specific print of a monster card
		/// </summary>
		/// <param name="monstercard">MonsterCard to be rendered</param>
		/// <param name="print">Print information about the MonsterCard</param>
		public Bitmap RenderPrint(MonsterCard monstercard, Print print)
		{
			Bitmap bitmap = RenderBackground(monstercard);
			using(Graphics graphics = Graphics.FromImage(bitmap))
			{
				// Render the common elements
				RenderCommon(graphics, monstercard);

				// Draw the card name in solid black
				Engine.DrawName(graphics, s_layout, monstercard.Name, NameBrush.SolidBlack);

				// Render the print artwork
				using(Artwork artwork = print.GetArtwork())
				{
					Engine.DrawArtwork(graphics, s_layout, artwork.Image ?? Resources.defaultartwork);
				}

				// Draw the set code for the print
				Engine.DrawSetCode(graphics, s_layout, print.ToString());
			}

			return bitmap;
		}

		//-------------------------------------------------------------------------
		// Private Member Functions

		/// <summary>
		/// Generates the array of type strings required to render the card
		/// </summary>
		/// <param name="monstercard">MonsterCard instance</param>
		private static string[] GetTypes(MonsterCard monstercard)
		{
			// All monster cards list their primary type first
			List<string> types = new List<string>
			{
				Extensions.ToString(monstercard.Type)
			};

			// Fusion/Ritual types come next when applicable
			if(monstercard.Fusion) types.Add("Fusion");
			else if(monstercard.Ritual) types.Add("Ritual");

			// The older card format only specified the subtype
			// and omitted "/ Effect" unless it was just "/ Effect"
			if(monstercard.Effect)
			{
				if(monstercard.Gemini) types.Add("Gemini");
				else if(monstercard.Spirit) types.Add("Spirit");
				else if(monstercard.Toon) types.Add("Toon");
				else if(monstercard.Union) types.Add("Union");
				else types.Add("Effect");
			}

			return types.ToArray();
		}

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
			Bitmap background = Engine.RenderBackground(s_layout, backgroundtype);
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
			RenderCommon(graphics, monstercard, null);
		}

		/// <summary>
		/// Renders the common elements between a Card render and a Print render
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="monstercard">MonsterCard instance</param>
		/// <param name="alttext">Alternate text to be rendered</param>
		private void RenderCommon(Graphics graphics, MonsterCard monstercard, string alttext)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(monstercard == null) throw new ArgumentNullException(nameof(monstercard));

			// Attribute
			Engine.DrawAttribute(graphics, s_layout, monstercard.Attribute);

			// Level Stars
			Engine.DrawLevelStars(graphics, s_layout, monstercard.Level);

			// Type
			Engine.DrawMonsterTypes(graphics, s_layout, GetTypes(monstercard));

			// Text
			if(monstercard.Fusion)
			{
				if(monstercard.Effect)
				{
					string materials = string.Empty;
					string effect = alttext ?? monstercard.Text;

					// Effect fusion monsters don't always have materials listed, use the presence
					// a CRLF pair in the string to detect
					int linebreak = effect.IndexOf("\r\n");
					if(linebreak > 0)
					{
						materials = effect.Substring(0, linebreak + 2).TrimEnd(new char[] { '\r', '\n' });
						effect = effect.Substring(linebreak + 2);
					}

					Engine.DrawFusionMonsterText(graphics, s_layout, materials, effect);
				}
				else
				{
					// Normal fusion monsters only have the materials listed in text
					Engine.DrawFusionMonsterText(graphics, s_layout, alttext ?? monstercard.Text, string.Empty);
				}
			}

			else if(monstercard.Normal) Engine.DrawNormalMonsterText(graphics, s_layout, alttext ?? monstercard.Text);
			else Engine.DrawMonsterText(graphics, s_layout, alttext ?? monstercard.Text);


			// Attack/Defense
			Engine.DrawAttackDefense(graphics, s_layout, monstercard.Attack, monstercard.Defense);

			// Passcode
			Engine.DrawPasscode(graphics, s_layout, monstercard.Passcode);

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
